using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    public class Album
    {
        public int ID { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        public string Title { get; set; }

        public int Size { get; set; }

        public override bool Equals(object obj)
        {
            Album other = obj as Album;
            return other == null ? false : other.ID == this.ID && other.OwnerId == this.OwnerId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ID.GetHashCode() * OwnerId.GetHashCode()) ^ 431;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Title, Size);
        }
    }
}
