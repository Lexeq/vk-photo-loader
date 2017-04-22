using System;

namespace VPhotoLoader.Api
{
    [Serializable]
    public class ApiException : ApplicationException
    {
        public RequestParam[] Params { get; internal set; }

        public ApiException() : base() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, System.Exception inner) : base(message, inner) { }
    }
}
