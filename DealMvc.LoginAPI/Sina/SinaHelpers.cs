using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;

using DealMvc.Common.Config;

namespace DealMvc.API.Sina
{
    public class SinaHelpers
    {
        // private string apiKey = m_entity.App_key;//申请的App Key
        // private string apiKeySecret = m_entity.App_secret;//申请的App Secret
        //private string apiCallback = "http://aixin.cdleichi.com";//申请的oauth_callback

        private string requestTokenUri = "http://api.t.sina.com.cn/oauth/request_token";
        private string AUTHORIZE = "http://api.t.sina.com.cn/oauth/authorize";
        private string ACCESS_TOKEN = "http://api.t.sina.com.cn/oauth/access_token";

        string APP_Keys = "3949206357";
        string APP_Secrets = "ca7fe2d2a93347ad92b5156507cdf200";
        #region 第一步

        /// <summary>
        /// 第一步
        /// </summary>
        public string getRequestToken(string s_oauth_callback)
        {

            OAuthBase oAuth = new OAuthBase();
            Uri uri = new Uri(requestTokenUri);
            string nonce = oAuth.GenerateNonce();//获取随机生成的字符串，防止攻击
            string timeStamp = oAuth.GenerateTimeStamp();//发起请求的时间戳
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = oAuth.GenerateSignature(
                uri,
                APP_Keys,
                APP_Secrets,
                string.Empty,
                string.Empty, "GET",
                timeStamp, nonce, string.Empty,
                out normalizeUrl, out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);
            //构造请求Request Token的url
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", APP_Keys);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_version={0}", "1.0");
            //请求Request Token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            int intOTS = responseBody.IndexOf("oauth_token=");
            int intOTSS = responseBody.IndexOf("&oauth_token_secret=");
            string str_oauth_token = responseBody.Substring(intOTS + 12, intOTSS - (intOTS + 12));
            Session.Add("oauth_token", str_oauth_token);
            Session.Add("oauth_token_secret", responseBody.Substring((intOTSS + 20), responseBody.Length - (intOTSS + 20)));
            return AUTHORIZE + "?oauth_token=" + str_oauth_token + "&oauth_callback=" + s_oauth_callback;
        }
        #endregion

        #region 第二步

        public string getAccessToken(string requestToken, string oauth_verifier)
        {
            OAuthBase oAuth = new OAuthBase();
            Uri uri = new Uri(ACCESS_TOKEN);
            string nonce = oAuth.GenerateNonce();//获取随机生成的字符串，防止攻击
            string timeStamp = oAuth.GenerateTimeStamp();//发起请求的时间戳
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = oAuth.GenerateSignature(
            uri,
            APP_Keys,
            APP_Secrets,
            requestToken,
            Session.Get("oauth_token_secret").ToString(),
            "Get",
            timeStamp,
            nonce,
            oauth_verifier,
            out normalizeUrl,
            out normalizedRequestParameters);
            sig = oAuth.UrlEncode(sig);
            //构造请求Access Token的url
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", APP_Keys);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_version={0}&", "1.0");
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_token={0}&", requestToken);
            sb.AppendFormat("oauth_verifier={0}", oauth_verifier);
            //请求Access Token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            int intOTS = responseBody.IndexOf("oauth_token=");
            int intOTSS = responseBody.IndexOf("&oauth_token_secret=");
            int intUser = responseBody.IndexOf("&user_id=");

            //会员ID
            string User_Id = responseBody.Substring((intUser + 9), responseBody.Length - (intUser + 9));

            Session.Add("User_Id", User_Id);
            return User_Id;

            //return verify_credentials(
            //     responseBody.Substring(intOTS + 12, intOTSS - (intOTS + 12)),
            //     responseBody.Substring((intUser + 9), responseBody.Length - (intUser + 9))
            //     );
        }
        #endregion

        #region 第三步
        public SinaEntity.SinaWeiBoUser verify_credentials(string oauth_token, string oauth_token_secret)
        {
            OAuthBase oAuth = new OAuthBase();
            Uri uri = new Uri("http://api.t.sina.com.cn/account/verify_credentials.xml");
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string normalizeUrl, normalizedRequestParameters;
            // 签名
            string sig = oAuth.GenerateSignature(
                uri,
                APP_Keys,
                APP_Secrets,
                oauth_token,
                oauth_token_secret,
                "Get",
                timeStamp,
                nonce,
                string.Empty,
                out normalizeUrl,
                out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);
            StringBuilder sb = new StringBuilder(uri.ToString());
            sb.AppendFormat("?oauth_consumer_key={0}&", APP_Keys);
            sb.AppendFormat("oauth_nonce={0}&", nonce);
            sb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            sb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            sb.AppendFormat("oauth_version={0}&", "1.0");
            sb.AppendFormat("oauth_signature={0}&", sig);
            sb.AppendFormat("oauth_token={0}&", oauth_token);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string responseBody = stream.ReadToEnd();
            stream.Close();
            response.Close();
            return GetSinaUserModel(responseBody);
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// 获取登陆用户实体 
        /// </summary>
        /// <param name="xmlResponseBody"></param>
        /// <returns></returns>
        public SinaEntity.SinaWeiBoUser GetSinaUserModel(string xmlResponseBody)
        {
            if (!string.IsNullOrEmpty(xmlResponseBody) && xmlResponseBody.IndexOf("screen_name") > -1)
            {
                SinaEntity.SinaWeiBoUser user = new SinaEntity.SinaWeiBoUser();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponseBody);

                user.id = GetVal(xmlDoc, "id").ToString2();
                user.screen_name = GetVal(xmlDoc, "screen_name").ToString2();
                user.name = GetVal(xmlDoc, "name");
                user.province = GetVal(xmlDoc, "province").ToInt32();
                user.city = GetVal(xmlDoc, "city").ToInt32();
                user.location = GetVal(xmlDoc, "location");
                user.description = GetVal(xmlDoc, "description");
                user.url = GetVal(xmlDoc, "url");
                user.profile_image_url = GetVal(xmlDoc, "profile_image_url");
                user.domain = GetVal(xmlDoc, "domain");
                user.gender = GetVal(xmlDoc, "gender");
                user.followers_count = GetVal(xmlDoc, "followers_count").ToInt32();
                user.friends_count = GetVal(xmlDoc, "friends_count").ToInt32();
                user.statuses_count = GetVal(xmlDoc, "statuses_count").ToInt32();
                user.created_at = GetVal(xmlDoc, "created_at");
                user.following = GetVal(xmlDoc, "following").ToBoolean2();
                user.verified = GetVal(xmlDoc, "verified").ToBoolean2();
                user.allow_all_act_msg = GetVal(xmlDoc, "allow_all_act_msg").ToBoolean2();
                user.geo_enabled = GetVal(xmlDoc, "followers_count").ToBoolean2();
                return user;

            }
            return null;
        }
        /// <summary>
        /// XmlDocument GetElementsByTagName Value
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="sname"></param>
        /// <returns></returns>
        public string GetVal(XmlDocument xmlDoc, string sname)
        {
            return xmlDoc.GetElementsByTagName(sname)[0].InnerText;
        }
        #endregion
    }
}
