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
        private object locker;

        private List<PhotoSourceItem> _srcs;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count { get { return _srcs.Count; } }

        public PhotoSourceCollection()
        {
            locker = new object();
            _srcs = new List<PhotoSourceItem>();
        }

        public bool Add(IVkPage page, IEnumerable<Album> albums)
        {
            return Add(new PhotoSourceItem(page.ID, page.ToString(), albums));
        }

        public bool Add(PhotoSourceItem item)
        {
            lock (locker)
            {
                if (!this.Contains(item.Id))
                {
                    _srcs.Add(item);
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, item));
                    return true;
                }
                return false;
            }
        }

        public bool Contains(int id)
        {
            return _srcs.Exists(p => p.Id == id);
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
