using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VPhotoLoader.Api
{

    //  public delegate string CaptchaHandler(string img);

    public class VKApi
    {
        static private DateTime lastApiInvoke = DateTime.MinValue;

        private const int apiRequestInterval = 350;
        private const string apiVersion = "5.28";

        /*
        private CaptchaHandler _captchaHandler;
        public VKApi(string userId, string token, CaptchaHandler handler)
        {
            this.Token = token;
            this.UserId = userId;
            _captchaHandler = handler;
        }
        */

        public string UserId { get; private set; }

        public string Token { get; set; }

        public VKApi(string userId, string token)
        {
            this.Token = token;
            this.UserId = userId;
        }

        /// <summary>
        /// Группы в которых состоит пользователь.
        /// </summary>
        public Group[] GetGroups()
        {
            //TODO Make possible get more then 1K groups
            string apiReq = String.Format(@"https://api.vk.com/method/groups.get?user_id={0}&extended=1&v=5.28&access_token={1}", UserId, Token);
            var result = Invoke<Root<Group>>(apiReq);
            return result.Response.Items.ToArray();
        }

        /// <summary>
        /// Возвращает информацию о заданных сообществах. 
        /// </summary>
        /// <param name="ids">Идентифмкаторы сообществ (максимум 500).</param>
        public Group[] GetGroupsByIds(params string[] ids)
        {
            if (ids.Length > 500) throw new ArgumentException("Число групп для запроса не должно превышать 500");
            string apiReq = String.Format(@"https://api.vk.com/method/groups.getById?group_ids={0}&v=5.28&access_token={1}", string.Join(",", ids), Token);
            var result = Invoke<Response<Group>>(apiReq);
            return result.Items.ToArray();
        }

        /// <summary>
        /// Друзья текужего пользователя
        /// </summary>
        public User[] GetFriends()
        {
            string apiReq = String.Format(@"https://api.vk.com/method/friends.get?user_id={0}&fields=sex&v=5.28&access_token={1}", UserId, Token);
            var result = Invoke<Root<User>>(apiReq);
            return result.Response.Items.ToArray();
        }

        /// <summary>
        /// Возвращает информацию о заданных пользователях. 
        /// </summary>
        /// <param name="ids">Идентифмкаторы пользователей (максимум 1000).</param>
        public User[] GetUsers(params string[] ids)
        {
            if (ids.Length > 1000) throw new ArgumentException("Число групп для запроса не должно превышать 500");
            string apiReq = String.Format(@"https://api.vk.com/method/users.get?user_ids={0}&fields=sex&v=5.28&access_token={1}", string.Join(",", ids), Token);
            var result = Invoke<Response<User>>(apiReq);
            return result.Items.ToArray();
        }

        /// <summary>
        /// Возвращает пользователей на которых подписан текущий пользователь.
        /// </summary>
        public User[] GetSubscriptions()
        {
            //TODO Make possible get more then 200 Subscriptions
            string apiReq = String.Format(@"https://api.vk.com/method/users.getSubscriptions?user_id={0}&v=5.28&access_token={1}", UserId, Token);
            string json = GetJsonResponse(apiReq);
            var err = JsonConvert.DeserializeObject<ErrorResponse>(json);
            if (err.Error != null) throw new ApiException(err.Error.Message) { Params = err.Error.RequestParams.ToArray() };
            JObject jobj = JObject.Parse(json);
            var subs = jobj["response"]["users"]["items"].Select(n => n.ToString()).ToArray();
            return GetUsers(subs);
        }

        /// <summary>
        /// Возвращает альбомы пользователя.
        /// </summary>
        public Album[] GetAlbums(User user)
        {
            return GetAlbums(user.ID);
        }

        /// <summary>
        /// Возвращает альбомы группы.
        /// </summary>
        public Album[] GetAlbums(Group group)
        {
            var id = group.ID;
            return GetAlbums(id > 0 ? -id : id);
        }

        /// <summary>
        /// Возвращает альбомы пользователя.
        /// </summary>
        /// <param name="ownerId">Идентификатор владельца альбома</param>
        /// <param name="albumIds">Идентификаторы альбомов</param>
        /// <returns></returns>
        public Album[] GetAlbums(int ownerId, params int[] albumIds)
        {
            string apiReq = string.Format(@"https://api.vk.com/method/photos.getAlbums?owner_id={0}&need_system={1}&album_ids={2}&v=5.28&access_token={3}", ownerId, 0, string.Join(",", albumIds), Token);
            var result = Invoke<Root<Album>>(apiReq);
            return result.Response.Items.ToArray();
        }

        /// <summary>
        /// Возврвщает фотографии, находящиеся в альбоме.
        /// </summary>
        public Photo[] GetPhotos(Album album)
        {
            const int count = 1000;
            List<PhotoExtended> photos = new List<PhotoExtended>();
            int offset = 0;
            int photoCount = 0;

            do
            {
                string apiReq = string.Format(@"https://api.vk.com/method/photos.get?owner_id={0}&album_id={1}&count={2}&offset={3}&v=5.28&access_token={4}",
                    album.OwnerId, album.ID, count, offset, Token);
                var result = Invoke<Root<PhotoExtended>>(apiReq);
                photoCount = result.Response.Count;
                offset += count;
                photos.AddRange(result.Response.Items);
            } while (offset < photoCount);
            return photos.ConvertAll<Photo>(pe => Photo.FromPhotoExt(pe)).ToArray();
        }

        public bool TryParseAlbum(string link, out Album result)
        {
            Regex regexAlbum = new Regex(@"vk\.com\/album(?<pageid>[0-9-]+)_(?<albumid>\d+)");
            var match = regexAlbum.Match(link);
            if (match.Success)
            {
                try
                {
                    var album = this.GetAlbums(
                        int.Parse(match.Groups["pageid"].Value),
                        int.Parse(match.Groups["albumid"].Value))
                        .First();
                    result = album;
                    return true;
                }
                catch { }
            }

            result = null;
            return false;
        }

        public bool TryParsePage(string link, out IVkPage result)
        {

            Match friendMatch = Regex.Match(link, @"vk\.com\/id(?<ID>\d+)$");
            Match groupMatch = Regex.Match(link, (@"vk\.com\/(club|public)(?<ID>\d+)$"));
            Match shortNameMatch = Regex.Match(link, @"vk\.com\/(?<ID>[\w\.\d_]+)$");

            try
            {
                if (friendMatch.Success)
                {
                    var id = friendMatch.Groups["ID"].Value;
                    result = this.GetUsers(id)[0];
                    return true;
                }
                else if (groupMatch.Success)
                {
                    var id = groupMatch.Groups["ID"].Value;
                    result = this.GetGroupsByIds(id)[0];
                    return true;
                }
                else if (shortNameMatch.Success)
                {
                    var id = shortNameMatch.Groups["ID"].Value;

                    try
                    {
                        result = this.GetGroupsByIds(id).First();
                        return true;
                    }
                    catch (ApiException)
                    {
                        try
                        {
                            result = this.GetUsers(id).First();
                            return true;
                        }
                        catch (ApiException) { }
                    }
                }
            }
            catch { }

            result = null;
            return false;
        }

        private T Invoke<T>(string requestString)
        {
            string req = requestString;
            /*if (captcha != null)
            {
                req = string.Concat(requestString, "&captcha_sid=", captcha.Sid, "&captcha_key=", captcha.Key);
            }
            else
            {
                req = requestString;
            }*/
            var json = GetJsonResponse(req);
            var err = JsonConvert.DeserializeObject<ErrorResponse>(json);
            if (err.Error != null)
            {
             /*Captcha
              * if (err.Error.Code == 14)
                {
                    JObject obj = JObject.Parse(json);
                    var sid = obj["error"]["captcha_sid"].Value<string>();
                    var img = obj["error"]["captcha_img"].Value<string>();
                    var captcaResult = _captchaHandler(img);
                    return Invoke<T>(requestString, new CaptchaSolve(sid, captcaResult));
                }
                else
                {
                    throw new ApiException(err.Error.Message) { Params = err.Error.RequestParams.ToArray() };
                }*/
                throw new ApiException(err.Error.Message) { Params = err.Error.RequestParams.ToArray() };
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        [Obsolete("Dont use it", true)]
        private T TryDesialize<T>(string json)
        {
            var err = JsonConvert.DeserializeObject<ErrorResponse>(json);
            if (err.Error != null)
            {
                throw new ApiException(err.Error.Message) { Params = err.Error.RequestParams.ToArray() };
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private string GetJsonResponse(string uri)
        {
            TimeSpan diff = DateTime.Now.Subtract(lastApiInvoke);
            if (diff.Milliseconds < apiRequestInterval)
                Thread.Sleep(apiRequestInterval - diff.Milliseconds);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            string json;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }
            }
            lastApiInvoke = DateTime.Now;
            return json;
        }
    }
}
