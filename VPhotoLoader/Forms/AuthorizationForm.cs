using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VPhotoLoader.Api;
using System.Security.Policy;

namespace VPhotoLoader.Forms
{
    public partial class WebForm : Form
    {
     //   public bool AuthSuccessful { get; set; }

    //    public VKApi API { get; set; }

        public WebBrowser Browser { get { return webBrowser; } }

        public WebForm()
        {
            InitializeComponent();
           // AuthSuccessful = false;
        }
    }
}
