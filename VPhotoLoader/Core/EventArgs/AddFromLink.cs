using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.VPL
{
    public class AddFromLinkEventArgs : EventArgs
    {
        public string Link { get; private set; }

        public AddFromLinkEventArgs(string link)
        {
            Link = link;
        }
    }
}
