using System;
using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    [Serializable]
    public class User : IVkPage
    {
        public int ID { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        public Sex Sex { get; set; }

        [JsonProperty(PropertyName = "deactivated")]
        public string DeactivatedReason { get; set; }

        public bool Deactivated { get { return DeactivatedReason != null; } }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        public override bool Equals(object obj)
        {
            User other = obj as User;
            return other == null ? false : other.ID == this.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
