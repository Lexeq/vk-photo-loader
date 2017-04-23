using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Api;

namespace VPhotoLoader.Core
{
    public class CheckableItem<T> : ICheckable
    {
        public bool Check { get; set; }

        public T Item { get; private set; }

        public CheckableItem(T item) : this(item, true) { }

        public CheckableItem(T item, bool check)
        {
            Item = item;
            Check = check;
        }

        public override string ToString()
        {
            return Item.ToString();
        }
    }
}
