using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.VPL
{
    public class IndexEventArgs : EventArgs
    {
        public int Index { get; private set; }

        public IndexEventArgs(int index)
        {
            Index = index;
        }
    }
}
