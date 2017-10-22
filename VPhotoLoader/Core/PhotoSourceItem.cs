using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Api;
using System.Windows.Forms;

namespace VPhotoLoader.Core
{
    public class PhotoSourceItem : IEquatable<PhotoSourceItem>, ICheckable
    {
        public bool Check { get; set; }

        public string Title { get; private set; }

        public int Id { get; private set; }

        public CheckableItem<Album>[] Albums { get; private set; }

        public PhotoSourceItem(int id, string title, IEnumerable<Album> albums)
        {
            Id = id;
            Title = title;
            Albums = albums.Distinct().Select(a => new CheckableItem<Album>(a, true)).ToArray();
            Check = true;
        }

        public bool Equals(PhotoSourceItem other)
        {
            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            PhotoSourceItem si = obj as PhotoSourceItem;
            return si != null && this.Equals(si);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 31;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
