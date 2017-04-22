using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VPhotoLoader
{
    static class AppPaths
    {
        const string DBFILENAME = "photos.vpl";
        const string SESSIONFILENAME = "session.vpl";
        const string IMAGESFOLDER = "Images";
        const string LOGFILENAME = "log";
        const string OLDDB = "loaded.imp";


        static string _basePath;

        static AppPaths()
        {
            _basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static string DatabaseFilePath { get { return Path.Combine(_basePath, DBFILENAME); } }

        public static string SessionFilePath { get { return Path.Combine(_basePath, SESSIONFILENAME); } }

        public static string ImageFolder { get { return Path.Combine(_basePath, IMAGESFOLDER); } }

        public static string LogFile { get { return Path.Combine(_basePath, LOGFILENAME); } }

        [Obsolete]
        public static string OldFile { get { return Path.Combine(_basePath, OLDDB); } }
    }
}
