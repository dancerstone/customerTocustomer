using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace DealMvc.Timer
{
    /// <summary>
    /// 首页静态化
    /// </summary>
    public class StaticHomePage : ITimer
    {

        string ITimer.Name
        {
            get;
            set;
        }

        DateTime? ITimer.StartTime
        {
            get;
            set;
        }

        DateTime? ITimer.EndTime
        {
            get;
            set;
        }

        int ITimer.GSeconds
        {
            get;
            set;
        }

        void ITimer.Execute()
        {
            //***********首页静态化要执行成功，需要把global.asax里面的下面部分隐藏掉*************
            //###################################################################################
            //routes.maproute(
            //    "default",                                               // route name
            //    "",                                                      // url with parameters
            //    new { controller = "home", action = "index" }            // parameter defaults
            //);

            //routes.maproute(
            //    "default1",                                              // route name
            //    "{controller}/{action}",                                 // url with parameters
            //    new { controller = "home", action = "index" }            // parameter defaults
            //);
            //###################################################################################

            //dealmvc.common.config.siteinfo m_siteinfo = dealmvc.common.config.configinfo<dealmvc.common.config.siteinfo>.instance();
            //string homeurl = m_siteinfo.homeurl + "/home/index.htm";
            //string text = common.globals.downloadstringfromurl(homeurl.replace("//home", "/home"));
            //common.Globals.WriteText(text, HostingEnvironment.MapPath("~/index.htm"));
        }
    }
}
