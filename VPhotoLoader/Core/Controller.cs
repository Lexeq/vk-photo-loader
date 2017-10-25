using System;
using System.Collections.Generic;
using System.Linq;
using VPhotoLoader.Api;
using System.Threading;
using System.Threading.Tasks;
using VPhotoLoader.VPL;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.ComponentModel;

namespace VPhotoLoader.Core
{
    class Controller
    {
        private IMainView _mainViev;
        private IChooseView _chooseView;
        private VKEngine _vk;

        private Task currentTask;
        private CancellationTokenSource currentTaskTokenSource;

        private readonly PhotoSourceCollection _photoSources;
        private readonly TaskProgressReporter _reporter;

        private PhotoCollection[] _newPhotos;

        public Controller(IMainView view, IChooseView chooseViev, VKEngine vk)
        {
            _mainViev = view;
            SubscribeToView(_mainViev);

            _chooseView = chooseViev;

            _reporter = new TaskProgressReporter();
            _reporter.ProgressChanged += new EventHandler<ProgressChangedEventArgs>(_reporter_ProgressChanged);
            _vk = vk;
            _vk.OwnerChanged += new EventHandler(_vk_OwnerChanged);
            _vk_OwnerChanged(null, null);

            _photoSources = new PhotoSourceCollection();
            _photoSources.CollectionChanged += new NotifyCollectionChangedEventHandler(_photoSources_CollectionChanged);
        }

        void _reporter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _mainViev.TaskProgress = e.ProgressPercentage;
            _mainViev.InfoLabel = e.UserState.ToString();
        }

        void _photoSources_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _mainViev.Sources = _photoSources.ToArray();
        }

        private void SubscribeToView(IMainView view)
        {
            view.AddFromFriensPressed += new EventHandler(view_AddFromFriensPressed);
            view.AddFromGroupsPressed += new EventHandler(view_AddFromGroupsPressed);
            view.AddFromLinkPressed += new EventHandler<PathEventArgs>(view_AddFromLinkPressed);
            view.CancelPressed += new EventHandler(view_CancelPressed);
            view.ClearSourcesPressed += new EventHandler(view_ClearSourcesPressed);
            view.ExportSourcesPressed += new EventHandler<PathEventArgs>(view_ExportSourcesPressed);
            view.GetImagesPressed += new EventHandler(view_GetImagesPressed);
            view.ImportSourcesPressed += new EventHandler<PathEventArgs>(view_ImportSourcesPressed);
            view.LoadImagesPressed += new EventHandler(view_LoadImagesPressed);
            view.LoginPressed += new EventHandler(view_LoginPressed);
            view.LogoutPressed += new EventHandler(view_LogoutPressed);
            view.RemoveSourcePressed += new EventHandler<IndexEventArgs>(view_RemoveSourcePressed);
            view.SelectAlbumsPressed += new EventHandler<IndexEventArgs>(view_SelectAlbumsPressed);
            view.ItemCheckStateChanged += new EventHandler<CheckEventArgs>(view_ItemCheckStateChanged);
        }

        void view_ItemCheckStateChanged(object sender, CheckEventArgs e)
        {
            _photoSources[e.Index].Check = e.Value;
        }

        void _vk_OwnerChanged(object sender, EventArgs e)
        {
            _mainViev.LoginStatus = _vk.Owner == null ? "Вход не выполнен" : _vk.Owner.ToString();
        }

        void view_ClearSourcesPressed(object sender, EventArgs e)
        {
            _photoSources.Clear();
        }

        void view_SelectAlbumsPressed(object sender, IndexEventArgs e)
        {
            _chooseView.Choose(_photoSources[e.Index].Albums);
        }

        void view_RemoveSourcePressed(object sender, IndexEventArgs e)
        {

            _photoSources.RemoveAt(e.Index);
        }

        void view_CancelPressed(object sender, EventArgs e)
        {
            currentTaskTokenSource.Cancel();
        }

        void view_AddFromLinkPressed(object sender, PathEventArgs e)
        {
            if (VKSession.API == null)
            {
                _mainViev.ShowMessage("Необходимо выполнить вход!");
                return;
            }

            IVkPage page;
            Album album;
            if (_vk.TryParsePage(e.Path, out page))
            {
                _photoSources.Add(page, _vk.GetAlbums(page.ID));
            }
            else if (_vk.TryParseAlbum(e.Path, out album))
            {
                page = _vk.GetByID(album.OwnerId);
                if (_photoSources.Contains(page.ID))
                {
                    _photoSources.First(p => p.Id == page.ID)
                        .Albums.First(a => a.Item.ID == album.ID)
                        .Check = true;
                }
                else
                {
                    var psi = new PhotoSourceItem(page.ID, page.ToString(), _vk.GetAlbums(page.ID));
                    for (int i = 0; i < psi.Albums.Length; i++)
                    {
                        psi.Albums[i].Check = psi.Albums[i].Item.ID == album.ID;
                    }
                    _photoSources.Add(psi);
                }
            }
            else
            {
                _mainViev.ShowMessage("Не удалось распознать ссылку.");
            }
        }

        void view_AddFromGroupsPressed(object sender, EventArgs e)
        {
            if (VKSession.API == null)
            {
                _mainViev.ShowMessage("Необходимо выполнить вход!");
                return;
            }

            try
            {
                var groups = _vk.Owner.Groups;
                var cgroups = groups.ToCheckable(false).ToArray();
                _chooseView.Choose(cgroups);

                foreach (var item in cgroups.Checked())
                {
                    _photoSources.Add(item, _vk.GetAlbums(item.ID, true));
                }
            }
            catch (Exception ex)
            {
                Logger.Write("AddFromGroups", ex);
                _mainViev.ShowMessage("Не удалось выполнить операцию");
            }
        }

        void view_AddFromFriensPressed(object sender, EventArgs e)
        {
            if (VKSession.API == null)
            {
                _mainViev.ShowMessage("Необходимо выполнить вход!");
                return;
            }

            try
            {
                var friends = _vk.Owner.Friends;
                var cfriends = friends.ToCheckable(false).ToArray();
                _chooseView.Choose(cfriends);

                foreach (var item in cfriends.Checked())
                {
                    _photoSources.Add(item, _vk.GetAlbums(item.ID, true));
                }
            }
            catch (Exception ex)
            {
                Logger.Write("AddFromFriends", ex);
                _mainViev.ShowMessage("Не удалось выполнить операцию");
            }
        }

        void view_GetImagesPressed(object sender, EventArgs e)
        {
            _newPhotos = null;

            if (_photoSources.Checked().Count() == 0)
            {
                _mainViev.ShowMessage("Отсутствуют или не выбраны источники изображений.");
                return;
            }

            if (currentTask != null && !currentTask.IsCompleted)
            {
                _mainViev.ShowMessage("Дождитесь завершения операции.");
                return;
            }

            var sources = _photoSources.Checked().Cast<PhotoSourceItem>().ToArray();

            currentTaskTokenSource = new CancellationTokenSource();
            _reporter_ProgressChanged(this, new ProgressChangedEventArgs(0, "Starting"));

            _mainViev.LockInterface();

            currentTask = _vk.GetUnloadedPhotosAsynch(sources, currentTaskTokenSource.Token, _reporter);

            var t1 = currentTask.ContinueWith((ct) =>
            {
                var task = (Task<PhotoCollection[]>)ct;
                if (task.IsFaulted)
                {
                    Logger.Write("GetImages", task.Exception);
                }
                else if (!task.IsCanceled)
                {
                    _newPhotos = task.Result;
                }

                _mainViev.UnlockInterface();
                _mainViev.InfoLabel = string.Format("Новых изображений: {0}", _newPhotos == null ? 0 : _newPhotos.Sum(p => p.Photos.Length));

            }, System.Threading.Tasks.TaskContinuationOptions.None);

        }

        void view_LoadImagesPressed(object sender, EventArgs e)
        {
            if (_newPhotos == null || _newPhotos.Length == 0) 
            {
                _mainViev.ShowMessage("Отсутствуют изображения для загрузки.");
                return;
            }

            if (currentTask != null && !currentTask.IsCompleted)
            {
                _mainViev.ShowMessage("Дождитесь завершения операции.");
                return;
            }

            currentTaskTokenSource = new CancellationTokenSource();
            _reporter_ProgressChanged(this, new ProgressChangedEventArgs(0, "Starting"));

            _mainViev.LockInterface();

            currentTask = _vk.LoadPhotosAsynch(_newPhotos, currentTaskTokenSource.Token, _reporter);

            var t1 = currentTask.ContinueWith((ct) =>
            {
                var task = (Task<int>)ct;
                if (task.IsFaulted)
                {
                    Logger.Write("LoadImages", task.Exception);
                }
                if (task.IsCanceled)
                {
                    _mainViev.InfoLabel = "Отменено";
                }
                _newPhotos = null;
                _mainViev.UnlockInterface();


            }, System.Threading.Tasks.TaskContinuationOptions.None);

        }

        void view_ExportSourcesPressed(object sender, PathEventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(e.Path, false, Encoding.UTF8))
                {
                    foreach (var item in _photoSources)
                    {
                        sw.WriteLine(string.Format("{0} {1}",
                            item.Id,
                            string.Join("|", item.Albums.Checked().Select(a => a.ID))));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("ExportSources", ex);
                _mainViev.ShowMessage("Не удалось сохранить спиок.");
            }
        }

        void view_ImportSourcesPressed(object sender, PathEventArgs e)
        {
            try
            {
                var file = File.ReadAllLines(e.Path, Encoding.UTF8);
                _photoSources.Clear();

                foreach (var str in file)
                {
                    var parts = str.Split();
                    var id = parts[0];
                    var albums = parts[1].Split('|');
                    IVkPage pg = _vk.GetByID(int.Parse(id));
                    PhotoSourceItem psi = new PhotoSourceItem(pg.ID, pg.ToString(), _vk.GetAlbums(pg.ID));

                    foreach (var item in psi.Albums)
                    {
                        bool z = Array.IndexOf(albums, item.Item.ID.ToString()) >= 0;
                        item.Check = z;
                    }

                    _photoSources.Add(psi);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("ImportSources", ex);
                _mainViev.ShowMessage("Не удалось загрузить спиок.");
            }
        }

        void view_LogoutPressed(object sender, EventArgs e)
        {
            File.Delete(AppPaths.SessionFilePath);
            VKSession.API = null;
        }

        void view_LoginPressed(object sender, EventArgs e)
        {
            try
            {
                var api = Authorization.Authorization.Authorize();
                VKSession.API = api;
                VKSession.ToFile(AppPaths.SessionFilePath);
            }
            catch (Exception ex)
            {
                Logger.Write("Login", ex);
            }
        }
    }

}
