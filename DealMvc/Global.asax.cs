using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DealMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                               // Route name
                "",                                                      // URL with parameters
                new { controller = "Home", action = "Index" }            // Parameter defaults
            );

            routes.MapRoute(
                "Default1",                                              // Route name
                "{controller}/{action}.htm",                             // URL with parameters
                new { controller = "Home", action = "Index" }            // Parameter defaults
            );

            routes.MapRoute(
                "Default2",                                              // Route name
                "{controller}/{action}",                                 // URL with parameters
                new { controller = "Home", action = "Index" }            // Parameter defaults
            );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            Timer.Main.Do();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //if ((DateTime.Now - new DateTime(2011, 4, 29)).TotalSeconds > 0)
            //{
            //    Response.Redirect("http://www.cdleichi.com/");
            //    Response.End();
            //}

            //获取当前用户所在的IP城市
            //string NowLoginCityName = new DealMvc.Core.Base.BLL_SiteCity.GetCityModel_New().city;
            //if (string.IsNullOrEmpty(DealMvc.Common.Globals.getCookie("SiteCityNewID")))
            //{
            //    Model.SiteCity m_site = Model.SiteCity.GetModel(t => t.C_Title.lb_Like(NowLoginCityName) && t.C_Level==2);
            //    if (m_site.C_Title != "西安市")
            //    {
            //        m_site = Model.SiteCity.GetModel(t => t.C_Title == "西安市");
            //    }
            //    DealMvc.Common.Globals.setCookie("SiteCityNewID", m_site.id.ToString2());
            //}


            Model.SiteInfo _SiteInfo = WebCacheHelper.GetSiteInfo();

            if (DealMvc.WebCache.WebCache.IsUseWebCache != _SiteInfo.IsOpenDataCache)
            {
                DealMvc.WebCache.WebCache.IsUseWebCache = _SiteInfo.IsOpenDataCache;
            }
            if (DealMvc.WebCache.WebCache.WebCacheTime != (_SiteInfo.DataCacheTime ?? 0))
            {
                DealMvc.WebCache.WebCache.WebCacheTime = _SiteInfo.DataCacheTime ?? 0;
            }

        }

    }
}