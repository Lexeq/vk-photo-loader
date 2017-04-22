using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.Core
{
    interface ICaptchaSolver
    {
        string Solve(string imageLink);
    }
}
