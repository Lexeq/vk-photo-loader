using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Api;

namespace VPhotoLoader.SqLiteManager
{
    public interface ISqlProvider
    {
        IEnumerable<Photo> GetPhotos(int ownerId, int albumId);
        void Insert(IEnumerable<Photo> photos);
    }
}
