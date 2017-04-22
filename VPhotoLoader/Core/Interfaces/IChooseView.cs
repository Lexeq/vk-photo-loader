using System.Collections.Generic;

namespace VPhotoLoader.Core
{
    interface IChooseView
    {
        /// <summary>
        /// Allows the user to check items and set Checked property
        /// </summary>
        void Choose(ICheckable[] items);
    }
}
