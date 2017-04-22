using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.VPL
{
    public class CheckEventArgs : IndexEventArgs
    {
        public bool Value { get; private set; }

        public CheckEventArgs(int index, bool value)
            : base(index)
        {
            Value = value;
        }
    }
}
