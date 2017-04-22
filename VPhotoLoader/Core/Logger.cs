using System;
using System.IO;

namespace VPhotoLoader
{
    class Logger
    {
        private string _path;

        public Logger(string logFilePath)
        {
            _path = logFilePath;
        }

        public bool Write(string message, Exception exception)
        {
            try
            {
                File.AppendAllText(_path,
                    string.Format("[0] {1}/r/n{2}", DateTime.Now.ToString("d MMM yyyy HH:mm:ss"), exception.ToString()));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
