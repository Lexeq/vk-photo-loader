using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Core;
using VPhotoLoader.Forms;

namespace VPhotoLoader
{
    class Chooser : IChooseView
    {
        public void Choose(ICheckable[] items)
        {
            using (ChooseForm form = new ChooseForm(items))
            {
                form.ShowDialog();
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            }
        }

    }
}
