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

        private PhotoCollection[] newPhotos;

        public Controller(IMainView view, IChooseView chooseViev, VKEngine vk)
        {
            _mainViev = view;
            SubscribeToView(_mainViev);

            _chooseView = chooseViev;

            _vk = vk;
            _vk.OwnerChanged += new EventHandler(_vk_OwnerChanged);
            _vk_OwnerChanged(null, null);

            _photoSources = new PhotoSourceCollection();
            _photoSources.CollectionChanged += new NotifyCollectionChangedEventHandler(_photoSources_CollectionChanged);
        }

        void _photoSources_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _mainViev.Sources = _photoSources.ToArray();
        }

        private void SubscribeToView(IMainView view)
        {
            view.AddFromFriensPressed += new EventHandler(view_AddFromFriensPressed);
            view.AddFromGroupsPressed += new EventHandler(view_AddFromGroupsPressed);
            view.AddFromLinkPressed += new EventHandler<AddFromLinkEventArgs>(view_AddFromLinkPressed);
            view.CancelPressed += new EventHandler(view_CancelPressed);
            view.ClearSourcesPressed += new EventHandler(view_ClearSourcesPressed);
            view.ExportSourcesPressed += new EventHandler(view_ExportSourcesPressed);
            view.GetImagesPressed += new EventHandler(view_GetImagesPressed);
            view.ImportSourcesPressed += new EventHandler(view_ImportSourcesPressed);
            view.LoadImagesPressed += new EventHandler(view_LoadImagesPressed);
            view.LoginPressed += new EventHandler(view_LoginPressed);
            view.LogoutPressed += new EventHandler(view_LogoutPressed);
            view.RemoveSourcePressed += new EventHandler<IndexEventArgs>(view_RemoveSourcePressed);
            view.SelectAlbumsPressed += new EventHandler<IndexEventArgs>(view_SelectAlbumsPressed);
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

        void view_AddFromLinkPressed(object sender, AddFromLinkEventArgs e)
        {
            if (VKSession.API == null)
            {
                _mainViev.ShowMessage("Необходимо выполнить вход!");
                return;
            }

            IVkPage page;
            Album album;
            if (_vk.TryParsePage(e.Link, out page))
            {
                _photoSources.Add(page, _vk.GetAlbums(page.ID));
            }
            else if (_vk.TryParseAlbum(e.Link, out album))
            {
                _photoSources.Add(album);
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












        void view_ExportSourcesPressed(object sender, EventArgs e)
        {
#warning FilePathChoooe
            using (StreamWriter sw = new StreamWriter("src.imp", false, Encoding.UTF8))
            {
                foreach (var item in _photoSources)
                {
                    sw.WriteLine(string.Format("{0} {1}",
                        item.Id,
                        string.Join("|", item.Albums.Checked().Select(a => a.ID))));
                }
            }
        }

        void view_ImportSourcesPressed(object sender, EventArgs e)
        {
#warning FilePathChoooe
            var file = File.ReadAllLines("src.imp", Encoding.UTF8);
            _photoSources.Clear();

            foreach (var str in file)
            {
                var parts = str.Split();
                var id = parts[0];
                var albums = parts[1].Split('|');
                 IVkPage pg = _vk.GetByID(id);
                PhotoSourceItem psi = new PhotoSourceItem(pg.ID, pg.ToString(), _vk.GetAlbums(pg.ID));

                foreach (var item in psi.Albums)
                {
                    bool z = Array.IndexOf(albums, item.Item.ID.ToString()) >= 0;
                    item.Check = z;
                }

                _photoSources.Add(psi);

            }
        }

        void view_GetImagesPressed(object sender, EventArgs e)
        {
            if (!currentTask.IsCompleted)
            {
                _mainViev.ShowMessage("Отсутствуют или не выбраны источники изображений");
                return;
            }

            var sources = _photoSources.Checked().Cast<PhotoSourceItem>().ToArray();

            _mainViev.LockInterface();
            currentTaskTokenSource = new CancellationTokenSource();

            currentTask = _vk.GetUnloadedPhotosAsynch(sources, currentTaskTokenSource.Token, _reporter);
            var t1 = currentTask.ContinueWith((ct) =>
                {
                    var task = (Task<PhotoCollection[]>)ct;
                    if (task.IsFaulted)
                    {
#warning logit
                    }
                    else if (!task.IsCanceled)
                    {
                        newPhotos = task.Result;
                    }

                }, System.Threading.Tasks.TaskContinuationOptions.None);

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
            catch (Authorization.AuthorizationException) { }

        }

        void view_LoadImagesPressed(object sender, EventArgs e)
        {

           
        }






    }

    public static class CheckableItemExt
    {
        public static IEnumerable<CheckableItem<T>> ToCheckable<T>(this IEnumerable<T> items, bool check)
        {
            foreach (var item in items)
            {
                yield return new CheckableItem<T>(item, check);
            }
        }

        public static IEnumerable<T> Checked<T>(this IEnumerable<CheckableItem<T>> citems)
        {
            return citems.Where(c => c.Check).Select(c => c.Item);
        }

        public static IEnumerable<ICheckable> Checked(this IEnumerable<ICheckable> citems)
        {
            return citems.Where(c => c.Check).Select(c => c);
        }
    }
}
