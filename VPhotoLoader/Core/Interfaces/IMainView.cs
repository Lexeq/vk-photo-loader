﻿using System;
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
        event EventHandler<AddFromLinkEventArgs> AddFromLinkPressed;
        event EventHandler ClearSourcesPressed;
        event EventHandler<IndexEventArgs> RemoveSourcePressed;
        event EventHandler<IndexEventArgs> SelectAlbumsPressed;
        event EventHandler CancelPressed;
        event EventHandler GetImagesPressed;
        event EventHandler LoadImagesPressed;
        event EventHandler ImportSourcesPressed;
        event EventHandler ExportSourcesPressed;

        //Login, logout, getphotos and loadphotos buttons should be disabled
        void LockInterface();
        //Any buttons can be enabled
        void UnlockInterface();

        string InfoLabel { set; }
        int TaskProgress { set; }
        string LoginStatus { set; }

        void ShowMessage(string message);

        event EventHandler<CheckEventArgs> ItemCheck;

        CheckableItem<object>[] Sources { set; }
    }
}
