using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DealMvc.Common.Config
{
    public class ConfigInfo<T> where T : IConfigInfo, new()
    {
        private static T _instance;

        //static ConfigInfo()
        //{ WebCache.WebCacheClear += new WebCacheClearEventHandler(Clear); }

        public static T Instance()
        {
            if (_instance == null)
            {
                _instance = new T();
                WeikeConfig.Instance().Node(_instance, typeof(T).Name);
            }
            return _instance;
        }
        public static void Save() { WeikeConfig.Instance().Save(_instance, typeof(T).Name); }

        public static void Clear() { _instance = default(T); }

    }
}