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

        public static Photo FromPhotoExt(PhotoExtended photo)
        {
            string maxResolution;
            if (photo.Photo2560 != null) maxResolution = photo.Photo2560;
            else if (photo.Photo1280 != null) maxResolution = photo.Photo1280;
            else if (photo.Photo807 != null) maxResolution = photo.Photo807;
            else if (photo.Photo604 != null) maxResolution = photo.Photo604;
            else if (photo.Photo130 != null) maxResolution = photo.Photo130;
            else maxResolution = photo.Photo75;

            return new Photo(photo.Id, photo.AlbumId, photo.OwnerId, maxResolution);
        }
    }
}
