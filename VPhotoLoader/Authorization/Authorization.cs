using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VPhotoLoader.Api;
using VPhotoLoader.Forms;
using System.Text.RegularExpressions;

namespace VPhotoLoader.Authorization
{
    static class Authorization
    {
        private const string authpattern = @"access_token=(?<token>[a-z0-9]{0,})&expires_in=\d{1,}&user_id=(?<id>\d{1,})";
        private const string appid = "4377803";
        public static VKApi Authorize()
        {
            string authURI = @"https://oauth.vk.com/authorize?client_id=" + appid + "&scope=335878&redirect_uri=" +
                @"https://oauth.vk.com/blank.html&display=mobile&response_type=token&v=5.28";
            AuthorizationForm aform = new AuthorizationForm();
            WebBrowser browser = (WebBrowser)aform.Controls["webBrowser"];
            browser.Navigated += new WebBrowserNavigatedEventHandler(browser_Navigated);
            browser.Navigate(authURI);
            aform.ShowDialog();
            if (aform.AuthSuccessful)
            {
                var api = aform.API;
                aform.Dispose();
                return api;
            }
            else
            {
                aform.Dispose();
                throw new AuthorizationException();
            }
        }

        private static void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string uri = e.Url.AbsoluteUri;
            WebBrowser browser = (WebBrowser)sender;
            AuthorizationForm aform = (AuthorizationForm)browser.Parent;
            if (uri.StartsWith(@"https://oauth.vk.com/blank.html"))
            {
                var reg = Regex.Match(uri, authpattern);
                var token = reg.Groups["token"].Value;
                var user = reg.Groups["id"].Value;

                aform.AuthSuccessful = true;
                aform.API = new VKApi(user, token);
            }

            else
            {
                aform.AuthSuccessful = false;
                aform.API = null;
            }

            aform.Close();
        }
        /* else
         {
             parts = e.Url.AbsoluteUri.Split('?');
             if (parts[0] == "https://api.vkontakte.ru/oauth/grant_access")
                 browser.Navigate("https://api.vkontakte.ru/oauth/authorize?client_id=" + "4377803" + "&scope=335878&redirect_uri=http://api.vkontakte.ru/blank.html&display=mobile&response_type=token");
         }*/

    }
}
