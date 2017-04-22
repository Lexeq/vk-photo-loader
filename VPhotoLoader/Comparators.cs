using System.Collections.Generic;
using System.Linq;
using VPhotoLoader.Api;

namespace VPhotoLoader
{
    class OldPhotoComparer : IEqualityComparer<Photo>
    {
        public bool Equals(Photo x, Photo y)
        {
            var xn = x.Link.Split('/').Reverse().ToArray();
            var yn = y.Link.Split('/').Reverse().ToArray();
            return
                xn[0].GetHashCode() == yn[0].GetHashCode() &&
                xn[1].GetHashCode() == yn[1].GetHashCode() &&
                xn[2].GetHashCode() == yn[2].GetHashCode();
        }

        public int GetHashCode(Photo obj)
        {
            var n = obj.Link.Split('/').Reverse().ToArray();
            unchecked
            {
                return n[0].GetHashCode() * n[1].GetHashCode() * n[2].GetHashCode();
            }
        }
    }

    class NewPhotoComparer : IEqualityComparer<Photo>
    {
        public bool Equals(Photo x, Photo y)
        {
            if (x.Link != null && y.Link != null)
            {
                return x.Equals(y.Link);
            }
            else
                return x.ID == y.ID && x.OwnerID == y.OwnerID;
        }

        public int GetHashCode(Photo obj)
        {
            if (obj.Link != null)
            {
                return obj.Link.GetHashCode();
            }
            else
            {
                unchecked
                {
                    return (obj.ID.GetHashCode() ^ obj.OwnerID.GetHashCode()) * 1433;
                }
            }
        }
    }
}
