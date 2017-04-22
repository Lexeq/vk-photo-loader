using System;
using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    public class PhotoExtended
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
