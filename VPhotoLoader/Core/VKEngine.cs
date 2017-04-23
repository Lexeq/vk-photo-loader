#define OLD
using System;
using System.Collections.Generic;
using System.Linq;
using VPhotoLoader.Api;
using System.Collections.ObjectModel;
using VPhotoLoader.SqLiteManager;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Text;


namespace VPhotoLoader.Core
{
    class VKEngine
    {
        private VKApi _api;
        private ISqlProvider _db;

        private VKApi Api
        {
            get
            {
                return _api;
            }
            set
            {
                if (value != null)
                {
                    _api = value;
                    Owner = GetUserInfo();
                }
                OnOwnerChanged();
            }
        }

        public OwnerInfo Owner { get; private set; }

        public event EventHandler OwnerChanged;

        public VKEngine(ISqlProvider dbProvider, VKApi api)
        {
            _db = dbProvider;
            Api = api;
        }

        public VKEngine(ISqlProvider dbProvider) : this(dbProvider, null) { }

        public Album[] GetAlbums(int id, bool needSystem = true)
        {
            if (needSystem)
            {
                return Api.GetAlbums(id, -7, -8, -15); //-7 -8 -15 ids for system albums
            }
            else
            {
                return Api.GetAlbums(id);
            }
        }

        public Task<PhotoCollection[]> GetUnloadedPhotosAsynch(PhotoSourceItem[] photoSources, CancellationToken ctoken, ITaskProgress reporter = null)
        {

            return Task.Factory.StartNew<PhotoCollection[]>((o) =>
                {
                    List<PhotoCollection> unloaded = new List<PhotoCollection>();
                    PhotoSourceItem[] sources = (PhotoSourceItem[])o;
                    VKApi api = Api;

                    for (int psIndex = 0; psIndex < sources.Length; psIndex++)
                    {
                        var source = sources[psIndex];
                        var albums = source.Albums.Checked().ToArray();
                        for (int albumIndex = 0; albumIndex < albums.Length; albumIndex++)
                        {
                            ctoken.ThrowIfCancellationRequested();
                            var album = albums[albumIndex];

                            var inAlbum = api.GetPhotos(album);
                            var photos = GetUnloadedPhotos(albums[albumIndex], inAlbum);
                            if (photos.Length > 0)
                            {
                                unloaded.Add(new PhotoCollection(source.Title, album.Title, photos));
                                if (reporter != null)
                                {
                                    var progress = (100d / sources.Length) * (psIndex + (double)albumIndex / albums.Length);
                                    reporter.Report((int)progress, string.Format("{0}: {1}", source.Title, album.Title));
                                }
                            }
                        }
                    }
                    return unloaded.ToArray();
                }, photoSources, ctoken);
        }

        public Task<int> LoadPhotos(PhotoCollection[] photos, CancellationToken ctoken, ITaskProgress reporter = null)
        {
            return Task.Factory.StartNew<int>((o) =>
            {
                PhotoCollection[] pcs = (PhotoCollection[])o;

                var successfuls = new List<Photo>();
                var photoCount = pcs.Sum(n => n.Photos.Length);
                var reportStep = photoCount / 100 + 1;
                var counter = 0;

                WebClient webClient = new WebClient();
                try
                {
                    foreach (var item in pcs)
                    {
                        foreach (var photo in item.Photos)
                        {
                            ctoken.ThrowIfCancellationRequested();

                            string albumFolder = Path.Combine(AppPaths.ImageFolder, item.Owner, item.Album);
                            if (!Directory.Exists(albumFolder))
                            {
                                Directory.CreateDirectory(albumFolder);
                            }

                            if (TryLoadPhoto(webClient, photo.Link, Path.Combine(albumFolder, Path.GetFileName(photo.Link))))
                            {
                                successfuls.Add(photo);
                            }

                            if (++counter % reportStep == 0 && reporter != null)
                            {
                                reporter.Report(counter * 100 / photoCount, string.Format("{0}/{1}", counter, photoCount));
                            }
                        }
                    }
                    return successfuls.Count;
                }

                finally
                {
                    webClient.Dispose();
                    _db.Insert(successfuls);
                }

            }, photos, ctoken);
        }

        public bool TryParsePage(string link, out IVkPage page)
        {
            return Api.TryParsePage(link, out page);
        }

        public bool TryParseAlbum(string link, out Album album)
        {
            return Api.TryParseAlbum(link, out album);
        }

        public void SetNewApi(VKApi api)
        {
            Api = api;
        }

        private Photo[] GetUnloadedPhotos(Album album, IEnumerable<Photo> photos)
        {
#if OLD
            //Compatibility with old version
            var _oldFilePath = AppPaths.OldFile;
            if (File.Exists(_oldFilePath))
            {
                HashSet<Photo> old = new HashSet<Photo>(File.ReadAllLines(_oldFilePath, Encoding.Default)
                    .Select(s => new Photo(0, 0, 0, s)));
                var ta = photos.Intersect(old, new OldPhotoComparer());
                _db.Insert(ta);
                File.WriteAllLines(_oldFilePath, old.Except(ta, new OldPhotoComparer()).Select(n => n.Link));
            }
#endif
            var inDB = _db.GetPhotos(album.OwnerId, album.ID);
            return photos.Except(inDB).ToArray();
        }

        private bool TryLoadPhoto(WebClient webClient, string url, string path)
        {
            try
            {
                webClient.DownloadFile(url, path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private OwnerInfo GetUserInfo()
        {
            var owner = new OwnerInfo(
                Api.GetUsers(Api.UserId)[0].ToString(),
                Api.GetFriends(),
                Api.GetSubscriptions(),
                Api.GetGroups());
            return owner;
        }

        private void OnOwnerChanged()
        {
            if (OwnerChanged != null) OwnerChanged(this, EventArgs.Empty);
        }
    }

    class OwnerInfo
    {
        private IList<User> _friends;
        private IList<User> _subs;
        private IList<Group> _groups;
        private string _name;

        public ReadOnlyCollection<User> Friends { get { return new ReadOnlyCollection<User>(_friends); } }

        public ReadOnlyCollection<User> Subscriptions { get { return new ReadOnlyCollection<User>(_subs); } }

        public ReadOnlyCollection<Group> Groups { get { return new ReadOnlyCollection<Group>(_groups); } }

        public string Name { get { return _name; } }

        public OwnerInfo(string name, User[] friends, User[] subs, Group[] groups)
        {
            _name = name;
            _friends = friends;
            _groups = groups;
            _subs = subs;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
