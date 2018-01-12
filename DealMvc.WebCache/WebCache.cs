using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.Web.Caching;
using System.Collections;

namespace DealMvc.WebCache
{
    public delegate void WebCacheClearEventHandler();

    /// <summary>
    /// 缓存
    /// </summary>
    public class WebCache
    {
        /// <summary>
        /// 保存缓存状态
        /// </summary>
        private static bool WebCacheState;

        /// <summary>
        /// 缓存开关
        /// </summary>
        private static bool _IsUseWebCache;

        public static bool IsUseWebCache
        {
            get { return WebCache._IsUseWebCache; }
            set { WebCache._IsUseWebCache = value; }
        }

        /// <summary>
        /// 缓存时间
        /// </summary>
        private static double _WebCacheTime;

        public static double WebCacheTime
        {
            get { return WebCache._WebCacheTime; }
            set { WebCache._WebCacheTime = value; }
        }

        /// <summary>
        /// 关闭缓存
        /// </summary>
        public static void Close()
        {
            WebCacheState = _IsUseWebCache;
            _IsUseWebCache = false;//关闭缓存
        }
        /// <summary>
        /// 开始缓存
        /// </summary>
        public static void Open()
        {
            _IsUseWebCache = true;//开启缓存
        }


        /// <summary>
        /// 复位缓存
        /// </summary>
        public static bool Reset()
        {
            _IsUseWebCache = WebCacheState;
            return _IsUseWebCache;
        }

        private static readonly System.Web.Caching.Cache _cache;
        //public static readonly int DayFactor = 0x4380;
        //private static int Factor = 5;
        //public static readonly int HourFactor = 720;
        //public static readonly int MinuteFactor = 12;
        //private static readonly double SecondFactor = 0.2;

        public static event WebCacheClearEventHandler WebCacheClear;

        static WebCache()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                _cache = current.Cache;//当前web中的缓存
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        private WebCache()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="_Obj">对象</param>
        /// <param name="_SecondsBase">缓存时间(秒)</param>
        public static void Add(string _Key, object _Obj, int _SecondsBase)
        {
            Add(_Key, _Obj, null, _SecondsBase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="_Obj">对象</param>
        /// <param name="_SecondsBase">缓存时间(秒)</param>
        /// <param name="_Priority">指定 System.Web.Caching.Cache 对象中存储的项的相对优先级</param>
        public static void Add(string _Key, object _Obj, int _SecondsBase, CacheItemPriority _Priority)
        {
            Add(_Key, _Obj, null, _SecondsBase, _Priority);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="_Obj">对象</param>
        /// <param name="_Dep">在存储于 ASP.NET 应用程序的 System.Web.Caching.Cache 对象中的项与文件、缓存键、文件或缓存键的数组或另一个 System.Web.Caching.CacheDependency对象之间建立依附性关系。System.Web.Caching.CacheDependency 类监视依附性关系，以便在任何这些对象更改时，该缓存项都会自动移除。</param>
        /// <param name="_SecondsBase">缓存时间(秒)</param>
        public static void Add(string _Key, object _Obj, CacheDependency _Dep, int _SecondsBase)
        {
            Add(_Key, _Obj, _Dep, _SecondsBase, CacheItemPriority.Normal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="_Obj">对象</param>
        /// <param name="_Dep">在存储于 ASP.NET 应用程序的 System.Web.Caching.Cache 对象中的项与文件、缓存键、文件或缓存键的数组或另一个 System.Web.Caching.CacheDependency对象之间建立依附性关系。System.Web.Caching.CacheDependency 类监视依附性关系，以便在任何这些对象更改时，该缓存项都会自动移除。</param>
        /// <param name="_SecondsBase">缓存时间(秒)</param>
        /// <param name="_Priority">指定 System.Web.Caching.Cache 对象中存储的项的相对优先级。</param>
        public static void Add(string _Key, object _Obj, CacheDependency _Dep, int _SecondsBase, CacheItemPriority _Priority)
        {
            if (_Obj != null)
            {
                _cache.Add(_Key, _Obj, _Dep, DateTime.Now.AddSeconds((double)(_SecondsBase)), TimeSpan.Zero, _Priority, null);
            }
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            List<string> list = new List<string>();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key.ToString());
            }
            foreach (string str in list)
            {
                _cache.Remove(str);
            }
            OnWebCacheClear();
        }

        /// <summary>
        /// 清除以 TitleKey 开头的缓存
        /// </summary>
        /// <param name="TitleKey">Key头部</param>
        /// <param name="Sign">Key分割标识</param>
        public static void Clear(string TitleKey, string Sign)
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            List<string> list = new List<string>();
            while (enumerator.MoveNext())
            {
                string Key = enumerator.Key.ToString();
                string[] Keys = Key.Split(new string[] { Sign }, StringSplitOptions.RemoveEmptyEntries);
                if (Keys.Length > 0)
                {
                    if (Keys[0].ToString2() == TitleKey) { list.Add(Key); }
                }
            }
            foreach (string str in list)
            {
                _cache.Remove(str);
            }
            OnWebCacheClear();
        }

        protected static void OnWebCacheClear()
        {
            if (WebCacheClear != null)
            {
                WebCacheClear();
            }
        }

        /// <summary>
        /// 获取缓存对象,无则返回null
        /// </summary>
        /// <param name="_Key">键</param>
        /// <returns></returns>
        public static object Get(string _Key)
        {
            return _cache[_Key];
        }

        /// <summary>
        /// 获取指定Key键的对象,如果对象为null,就将Obj放入此Key键缓存和返回
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="Obj">对象</param>
        /// <param name="_SecondsBase">缓存时间(秒)</param>
        /// <returns></returns>
        public static object JGet(string _Key, object Obj, int _SecondsBase)
        {
            object _obj = Get(_Key);
            if (_obj != null) return _obj;
            Add(_Key, Obj, _SecondsBase);
            return Obj;
        }

        /// <summary>
        /// 永久缓存对象
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="_Obj">对象</param>
        public static void Max(string _Key, object _Obj)
        {
            Max(_Key, _Obj, null);
        }

        /// <summary>
        /// 永久缓存对象
        /// </summary>
        /// <param name="_Key">键</param>
        /// <param name="_Obj">对象</param>
        /// <param name="_Dep">在存储于 ASP.NET 应用程序的 System.Web.Caching.Cache 对象中的项与文件、缓存键、文件或缓存键的数组或另一个 System.Web.Caching.CacheDependency对象之间建立依附性关系。System.Web.Caching.CacheDependency 类监视依附性关系，以便在任何这些对象更改时，该缓存项都会自动移除。</param>
        public static void Max(string _Key, object _Obj, CacheDependency _Dep)
        {
            if (_Obj != null)
            {
                _cache.Add(_Key, _Obj, _Dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        /// <summary>
        /// 移除指定键的对象
        /// </summary>
        /// <param name="_Key"></param>
        public static void Remove(string _Key)
        {
            _cache.Remove(_Key);
        }

        public static System.Web.Caching.Cache Cache
        {
            get
            {
                return _cache;
            }
        }
    }
}
