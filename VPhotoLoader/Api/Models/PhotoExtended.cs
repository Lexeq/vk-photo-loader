using System;
using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    internal class PhotoExtended
    {
        private string _maxRes = null;

        public int Id { get; set; }

        [JsonProperty("album_id")]
        public int AlbumId { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("photo_75")]
        public string Photo75 { get; set; }

        [JsonProperty("photo_130")]
        public string Photo130 { get; set; }

        [JsonProperty("photo_604")]
        public string Photo604 { get; set; }

        [JsonProperty("photo_807")]
        public string Photo807 { get; set; }

        [JsonProperty("photo_1280")]
        public string Photo1280 { get; set; }

        [JsonProperty("photo_2560")]
        public string Photo2560 { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Date { get; set; }

        public static Photo ToPhoto(PhotoExtended photo)
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

        public override int GetHashCode()
        {
            return (Id.GetHashCode() ^ OwnerId.GetHashCode() ^ AlbumId.GetHashCode()) * 1433;
        }
        public override bool Equals(object obj)
        {
            var other = obj as PhotoExtended;
            return obj == null ? false : this.Id == other.Id &&
                                         this.AlbumId == other.AlbumId &&
                                         this.OwnerId == other.OwnerId;
        }
    }
}
