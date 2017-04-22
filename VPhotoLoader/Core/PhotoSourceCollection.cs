using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Api;
using System.Collections.Specialized;

namespace VPhotoLoader.Core
{
    public class PhotoSourceCollection : INotifyCollectionChanged, IEnumerable<PhotoSourceItem>
    {
        private readonly string DefaultPSName = "Albums";
        private int defaultId = int.MaxValue;
        private object locker;

        private List<PhotoSourceItem> _srcs;

        public void Add(Album album)
        {
            Add(new PhotoSourceItem(album.OwnerId, "[A] " + album.Title, new[] { album }));
        }

        public void Add(IVkPage page, IEnumerable<Album> albums)
        {
            Add(new PhotoSourceItem(page.ID, page.ToString(), albums));
        }

        private void Add(PhotoSourceItem item)
        {
            lock (locker)
            {
                if (!_srcs.Contains(item))
                {
                    _srcs.Add(item);
                    defaultId--;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, item));
                }
            }
        }

        public void Clear()
        {
            _srcs.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }

        public void Remove(PhotoSourceItem item)
        {
            lock (locker)
            {
                bool rem = _srcs.Remove(item);
                if (rem)
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove, item));
                }
            }
        }

        public void RemoveAt(int index)
        {
            var item = _srcs[index];
            Remove(item);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null) CollectionChanged(this, e);
        }

        public PhotoSourceItem this[int index]
        {
            get
            {
                return _srcs[index];
            }
        }

        public IEnumerator<PhotoSourceItem> GetEnumerator()
        {
            return _srcs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
