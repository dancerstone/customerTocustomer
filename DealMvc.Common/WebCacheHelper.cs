using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using DealMvc.Model;

namespace DealMvc
{
    /// <summary>
    /// 【缓存处理类】
    /// </summary>
    public class WebCacheHelper
    {
        /// <summary>
        /// 一个月的定时器
        /// </summary>
        public static int WebCacheTime = 2592000;

        /// <summary>
        /// 【网站配置】
        /// </summary>
        /// <returns></returns>
        public static Model.SiteInfo GetSiteInfo()
        {
            SiteInfo _SiteInfo = new Model.SiteInfo();

            string key_gc = "GetSiteInfoByModel";
            object o_model = DealMvc.WebCache.WebCache.Get(key_gc);
            if (o_model == null)
            {
                _SiteInfo = Model.SiteInfo.GetModel(t => t.id != 0);
                DealMvc.WebCache.WebCache.Add(key_gc, _SiteInfo, WebCacheTime);
            }
            else
                _SiteInfo = (Model.SiteInfo)o_model;

            return (_SiteInfo ?? new Model.SiteInfo());
        }
        
    }
}
