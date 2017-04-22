using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using VPhotoLoader.Authorization;
using VPhotoLoader.Api;
using System.IO;
using VPhotoLoader.Core;
using VPhotoLoader.Forms;

namespace VPhotoLoader
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Init Db
            SqLiteManager.SQLiteProvider db;

            if(File.Exists(AppPaths.DatabaseFilePath))
            {
                db = new SqLiteManager.SQLiteProvider(AppPaths.DatabaseFilePath);
            }
            else
            {
                db = SqLiteManager.SQLiteProvider.CreateDataBase(AppPaths.DatabaseFilePath);
            }

            //Init Api
            VKApi api = null;

            var args = ParseArgs(Environment.GetCommandLineArgs());
            VKEngine eng = new VKEngine(new SqLiteManager.SQLiteProvider(AppPaths.DatabaseFilePath));
            if (args.ContainsKey("token") && args.ContainsKey("user"))
            {
                api = new VKApi(args["user"], args["token"]);
            }
            else if (File.Exists(AppPaths.SessionFilePath))
            {
                    var file = File.ReadAllLines(AppPaths.SessionFilePath);
                    api = new VKApi(file[0], file[1]);
            }

            //Other
            VKEngine e = new VKEngine(db, api);

            var mainForm = new MainForm();
            Controller c = new Controller(mainForm, e);
            Application.Run(mainForm);
        }

        static Dictionary<string, string> ParseArgs(string[] args)
        {
            string pattern = @"^-(?<Key>[a-zA-Z]+)=(?<Value>[a-zA-Z0-9]+)";
            Regex r =new Regex(pattern);
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var item in args)
            {
                var match = r.Match(item.Trim());
                if (match.Success)
                {
                    res[match.Groups["Key"].ToString()] = match.Groups["Value"].ToString();
                }
            }
            return res;
        }
    }
}
