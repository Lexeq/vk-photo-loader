using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using VPhotoLoader.VPL;

namespace VPhotoLoader.Core
{
    interface IMainView
    {
        //Buttons
        event EventHandler LoginPressed;
        event EventHandler LogoutPressed;
        event EventHandler AddFromFriensPressed;
        event EventHandler AddFromGroupsPressed;
        event EventHandler<PathEventArgs> AddFromLinkPressed;
        event EventHandler ClearSourcesPressed;
        event EventHandler<IndexEventArgs> RemoveSourcePressed;
        event EventHandler<IndexEventArgs> SelectAlbumsPressed;
        event EventHandler CancelPressed;
        event EventHandler GetImagesPressed;
        event EventHandler LoadImagesPressed;
        event EventHandler<PathEventArgs> ImportSourcesPressed;
        event EventHandler<PathEventArgs> ExportSourcesPressed;

        bool TaskRunning { set; }

        string InfoLabel { set; }
        int TaskProgress { set; }
        string LoginStatus { set; }

        void ShowMessage(string message);

        event EventHandler<CheckEventArgs> ItemCheckStateChanged;

        ICheckable[] Sources { set; }
    }
}
