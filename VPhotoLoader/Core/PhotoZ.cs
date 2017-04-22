using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Api;

namespace VPhotoLoader.Core
{
    class PhotoCollection
    {
        public string Owner { get; private set; }

        public string Album { get; private set; }

        public Photo[] Photos { get; private set; }

        public PhotoCollection(string owner, string albumName, IEnumerable<Photo> photos)
        {
            Owner = owner;
            Album = albumName;
            Photos = photos.ToArray();
        }
    }


}
