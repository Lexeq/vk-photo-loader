using System.Collections.Generic;
using Newtonsoft.Json;

namespace VPhotoLoader.Api
{
    public class ErrorResponse
    {
        public ApiError Error { get; set; }
    }

    public class ApiError
    {
        [JsonProperty(PropertyName = "error_code")]
        public int Code { get; set; }
        [JsonProperty(PropertyName = "error_msg")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "request_params")]
        public List<RequestParam> RequestParams { get; set; }
    }
}
