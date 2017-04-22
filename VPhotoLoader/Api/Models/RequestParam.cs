using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.Api
{
    public class RequestParam
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return Key + "=" + Value;
        }
    }
}
