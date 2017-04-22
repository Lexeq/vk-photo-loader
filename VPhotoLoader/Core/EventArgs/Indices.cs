using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.VPL
{
        public class IndicesEventArgs : EventArgs
        {
            public int[] Indices { get; private set; }

            public IndicesEventArgs(IEnumerable<int> indices)
            {
                Indices = indices.ToArray();
            }
        }
}
