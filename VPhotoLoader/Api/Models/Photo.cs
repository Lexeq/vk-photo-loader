using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.Api
{
    public class Photo
    {
        public int ID { get; private set; }

        public int AlbumID { get; private set; }

        public int OwnerID { get; private set; }

        public string Link { get; private set; }

        public Photo(int id, int albumId, int ownerId, string link)
        {
            ID = id;
            AlbumID = albumId;
            OwnerID = ownerId;
            Link = link;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Photo p = obj as Photo;
            return p != null && p.ID.Equals(this.ID);
        }
    }
}
