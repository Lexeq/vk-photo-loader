using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPhotoLoader.Api;
using System.IO;

namespace VPhotoLoader
{
    public static class VKSession
    {
        private static VKApi _api;

        public static event EventHandler ApiChanged;

        private static void OnApiChanged()
        {
            if (ApiChanged != null) ApiChanged(typeof(VKSession), EventArgs.Empty);
        }

        public static VKApi API
        {
            get
            {
                return _api;
            }
            set
            {
                _api = value;
                OnApiChanged();
            }
        }

        public static void ToFile(string path)
        {
            File.WriteAllLines(path, new[] { _api.UserId, _api.Token }, Encoding.UTF8);
        }

        public static VKApi FromFile(string path)
        {
            try
            {
                var file = File.ReadAllLines(path, Encoding.UTF8);
                return new VKApi(file[0], file[1]);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
