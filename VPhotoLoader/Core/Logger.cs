using System;
using System.IO;

namespace VPhotoLoader
{
    static class Logger
    {
        public static bool Write(string message, Exception exception)
        {
            try
            {
                File.AppendAllText(
                    AppPaths.LogFile,
                    string.Format("[0] {1}{2}{3}{2}", DateTime.Now.ToString("d MMM yyyy HH:mm:ss"), message, Environment.NewLine, exception.ToString()));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
