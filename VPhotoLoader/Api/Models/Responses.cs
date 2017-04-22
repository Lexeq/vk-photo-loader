using System.Collections.Generic;
using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    public class ResponseWithCount<T>
    {
        public int Count { get; set; }
        public List<T> Items { get; set; }
    }
    public class Root<T>
    {
        public ResponseWithCount<T> Response { get; set; }
    }
    public class Response<T>
    {
        [JsonProperty("response")]
        public List<T> Items { get; set; }
    }
}
