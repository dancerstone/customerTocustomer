using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DealMvc.Common.Base
{
    public class CookieClass
    {
        #region 操作Cookie

        public static string CookieDESEncryptKey = "sdx123456";

        private static void SetCookieDomain(ref HttpCookie cookie)// = ".cdleichi.com";
        {
            string domain = string.Empty;
            string host = HttpContext.Current.Request.Url.Host;
            if (!string.IsNullOrEmpty(host))
                host = host.ToString().ToLower().Trim();
            else
                return;
            string[] host_s = host.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (host_s.Length >= 2)
                domain = "." + host_s[host_s.Length - 2] + "." + host_s[host_s.Length - 1];

            if (!string.IsNullOrEmpty(domain))
            cookie.Domain = ".lc-demo.com";
        }

        /// <summary>
        /// 获取Cookie值, 不存在返回string.Empty
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getCookie(string key)
        {
            string value = string.Empty;
            try
            {
                System.Web.HttpContext context = HttpContext.Current;
                HttpCookie cookie = context.Request.Cookies[key];
                if (!(cookie == null || string.IsNullOrEmpty(cookie.Value)))
                    value = DESEncrypt.Decrypt(cookie.Value, CookieDESEncryptKey);
            }
            catch { }
            return value;
        }

        /// <summary>
        ///  登录-注册页面 记录用户名和密码 过期时间默认30天
        /// </summary> 
        /// <param name="value">记录用户名</param>
        /// <param name="d_day">天</param>
        public static void LoginRegSetCook(string value, double? d_day)
        {
            try
            {
                System.Web.HttpContext context = HttpContext.Current;
                HttpCookie cookie = new HttpCookie(WebUserCookieKey);
                cookie.Value = DESEncrypt.Encrypt(value, CookieDESEncryptKey);
                cookie.Expires = DateTime.Now.AddDays(d_day ?? 1);//一天
                SetCookieDomain(ref cookie);
                context.Response.Cookies.Add(cookie);
            }
            catch { }
        }

        /// <summary>
        /// 登录-注册页面 记录用户名和密码 过期时间默认30天
        /// </summary>
        /// <param name="value"></param>
        public static void LoginRegSetCook(string value)
        {
            LoginRegSetCook(value, 30);
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setCookie(string key, string value)
        {
            return setCookie(key, value, 360.0);
        }
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setCookie(string key, string value, double? timeHours)
        {
            try
            {
                System.Web.HttpContext context = HttpContext.Current;
                HttpCookie cookie = new HttpCookie(key);
                cookie.Value = DESEncrypt.Encrypt(value, CookieDESEncryptKey);
                cookie.Expires = DateTime.Now.AddHours(timeHours ?? 1.0);//2小时
                SetCookieDomain(ref cookie);
                context.Response.Cookies.Add(cookie);
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region 获取后台或前台登录用户的信息

        private static string CmsAdminCookieKey = "CmsAdminCookieKey";
        /// <summary>
        /// 提示语句
        /// </summary>
        private static string GiftCookieKey = "GiftCookieKey";

        /// <summary>
        /// 提示语句--会员升级
        /// </summary>
        private static string JiFenCookieKey = "JiFenCookieKey";

        private static string WebUserCookieKey = "WebUserCookieKey";
        private static string WebPlaceUserCookieKey = "WebPlaceUserCookieKey";

        public static string getAdminName()
        {
            return getCookie(CmsAdminCookieKey);
        }
        /// <summary>
        /// 读取值
        /// </summary>
        /// <returns></returns>
        public static string getGiftName()
        {
            return getCookie(GiftCookieKey);
        }

        /// <summary>
        /// 读取值
        /// </summary>
        /// <returns></returns>
        public static string getJiFenName()
        {
            return getCookie(JiFenCookieKey);
        }

        public static string getUserName()
        {
            return getCookie(WebUserCookieKey);
        }

        public static string getPlaceUserName()
        {
            return getCookie(WebPlaceUserCookieKey);
        }

        public static bool isAdminLogin()
        {
            if (getAdminName() == string.Empty)
                return false;
            else
                return true;
        }

        public static bool isUserLogin()
        {
            if (getUserName() == string.Empty)
                return false;
            else
                return true;
        }

        public static bool setAdminName(string value)
        {
            return setCookie(CmsAdminCookieKey, value, 24 * 2);
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setGiftName(string value)
        {
            return setCookie(GiftCookieKey, value);
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setJiFenName(string value)
        {
            return setCookie(JiFenCookieKey, value);
        }
        /// <summary>
        /// 设置个人/组织账号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setUserName(string value)
        {
            return setCookie(WebUserCookieKey, value, 24 * 360);
        }
        /// <summary>
        /// 设置场所管理员账号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setPlaceUserName(string value)
        {
            return setCookie(WebPlaceUserCookieKey, value, 24 * 360);
        }


        #region 登录名
        private static string CmsLoginNameCookieKey = "CmsLoginNameCookieKey";
        public static string getLoginNameName()
        {
            return getCookie(CmsLoginNameCookieKey);
        }
        public static bool isLoginNameLogin()
        {
            if (getLoginNameName() == string.Empty)
                return false;
            else
                return true;
        }

        public static bool setLoginNameName(string value)
        {
            return setCookie(CmsLoginNameCookieKey, value, 24 * 7);
        }
        #endregion
        #endregion

        #region 获取城市id
        /// <summary>
        /// 获取城市id
        /// </summary>
        /// <returns></returns>
        public static string GetCityId()
        {
            return DealMvc.Common.Base.CookieClass.getCookie("CityID123");
        }
        #endregion


    }
}
