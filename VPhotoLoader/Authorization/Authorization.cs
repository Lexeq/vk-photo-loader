using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VPhotoLoader.Api;
using VPhotoLoader.Forms;

namespace VPhotoLoader.Authorization
{
    static class Authorization
    {

        public static VKApi Authorize()
        {
            string authURI = @"https://oauth.vk.com/authorize?client_id=" + "4377803" + "&scope=335878&redirect_uri=" +
                @"https://oauth.vk.com/blank.html&display=mobile&response_type=token";
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
            string[] parts = e.Url.AbsoluteUri.Split('#');
            WebBrowser browser = (WebBrowser)sender;
            AuthorizationForm aform = (AuthorizationForm)browser.Parent;
            if (parts[0] == "http://api.vkontakte.ru/blank.html" && parts.Length > 1)
            {
                if (parts[1].Substring(0, 12) == "access_token")
                {
                    parts = parts[1].Split('&');

                    var token = parts[0].Split('=')[1];
                    var user = parts[2].Split('=')[1];

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
            else
            {
                parts = e.Url.AbsoluteUri.Split('?');
                if (parts[0] == "http://api.vkontakte.ru/oauth/grant_access")
                    browser.Navigate("http://api.vkontakte.ru/oauth/authorize?client_id=" + "4377803" + "&scope=335878&redirect_uri=http://api.vkontakte.ru/blank.html&display=mobile&response_type=token");
            }
        }
    }
}
