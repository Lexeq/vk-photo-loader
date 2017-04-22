using System;
using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    [Serializable]
    public class Group : IVkPage
    {
        private int _id;

        public int ID
        {
            get { return -_id; }
            set { _id = value; }
        }

        public string Name { get; set; }

        public string Type { get; set; }

        [JsonProperty(PropertyName = "deactivated")]
        public string DeactivatedReason { get; set; }

        public bool Deactivated { get { return DeactivatedReason != null; } }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            Group other = obj as Group;
            return other == null ? false : other.ID == this.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
