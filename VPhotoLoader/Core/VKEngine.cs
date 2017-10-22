#define OLD
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VPhotoLoader.Api;
using VPhotoLoader.SqLiteManager;


namespace VPhotoLoader.Core
{
    class VKEngine
    {
        private HashSet<char> UnallowabledChars = new HashSet<char>() { '*', '|', '\\', ':', '"', '<', '>', '?', '/' };
        private ISqlProvider _db;

        public OwnerInfo Owner { get; private set; }

        public event EventHandler OwnerChanged;

        public VKEngine(ISqlProvider dbProvider)
        {
            _db = dbProvider;
            if (VKSession.API != null) OnOwnerChanged();
            VKSession.ApiChanged += (o, e) => OnOwnerChanged();
        }

        public Album[] GetAlbums(int id, bool needSystem = true)
        {
            if (needSystem)
            {
                var alb = VKSession.API.GetAlbums(id, -7, -8, -15); //-7 -8 -15 ids for system albums
                return alb.Where(a => !((a.ID == -7 || a.ID == -8 || a.ID == -15) && a.Size == 0)).ToArray();
            }
            else
            {
                return VKSession.API.GetAlbums(id);
            }
        }

        public Task<PhotoCollection[]> GetUnloadedPhotosAsynch(PhotoSourceItem[] photoSources, CancellationToken ctoken, ITaskProgress reporter = null)
        {

            return Task.Factory.StartNew<PhotoCollection[]>((o) =>
                {
                    List<PhotoCollection> unloaded = new List<PhotoCollection>();
                    PhotoSourceItem[] sources = (PhotoSourceItem[])o;
                    VKApi api = VKSession.API;

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

        public Task<int> LoadPhotosAsynch(PhotoCollection[] photos, CancellationToken ctoken, ITaskProgress reporter = null)
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
#warning delIt
                            //Thread.Sleep(900);
                            ctoken.ThrowIfCancellationRequested();

                            string ownerName = item.Owner.Any(c => UnallowabledChars.Contains(c)) ? new string(item.Owner.Where(c => !UnallowabledChars.Contains(c)).ToArray()) : item.Owner;
                            string albumFolder = Path.Combine(AppPaths.ImageFolder, ownerName, item.Album);
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
            return VKSession.API.TryParsePage(link, out page);
        }

        public bool TryParseAlbum(string link, out Album album)
        {
            return VKSession.API.TryParseAlbum(link, out album);
        }

        public IVkPage GetByID(int id)
        {
            if (id < 0) return VKSession.API.GetGroupsByIds((Math.Abs(id)).ToString())[0];
            else return VKSession.API.GetUsers(id.ToString())[0];
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
                VKSession.API.UserId,
                VKSession.API.GetUsers(VKSession.API.UserId)[0].ToString(),
                VKSession.API.GetFriends(),
                VKSession.API.GetSubscriptions(),
                VKSession.API.GetGroups());
            return owner;
        }

        private void OnOwnerChanged()
        {
            if (VKSession.API == null) Owner = null;
            else Owner = GetUserInfo();

            if (OwnerChanged != null) OwnerChanged(this, EventArgs.Empty);
        }
    }

    class OwnerInfo
    {
        private IList<User> _friends;
        private IList<User> _subs;
        private IList<Group> _groups;
        private string _name;
        private string _id;

        public ReadOnlyCollection<User> Friends { get { return new ReadOnlyCollection<User>(_friends); } }

        public ReadOnlyCollection<User> Subscriptions { get { return new ReadOnlyCollection<User>(_subs); } }

        public ReadOnlyCollection<Group> Groups { get { return new ReadOnlyCollection<Group>(_groups); } }

        public string Name { get { return _name; } }

        public string ID { get { return _id; } }

        public OwnerInfo(string id, string name, User[] friends, User[] subs, Group[] groups)
        {
            _name = name;
            _friends = friends;
            _groups = groups;
            _subs = subs;
            _id = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
