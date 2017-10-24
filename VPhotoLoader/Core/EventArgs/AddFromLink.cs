using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.VPL
{
    public class PathEventArgs : EventArgs
    {
        public string Path { get; private set; }

        public PathEventArgs(string path)
        {
            Path = path;
        }
    }
}
