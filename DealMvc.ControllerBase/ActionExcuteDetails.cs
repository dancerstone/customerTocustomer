using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO.Compression;

namespace DealMvc
{
    /// <summary>
    /// 监控页面执行效率 和 Zip压缩
    /// </summary>
    public class ActionExcuteDetailsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DateTime dt1 = DateTime.Now;
            filterContext.Controller.ViewData["ActionExcuteDetails_dt1"] = dt1;
        }
        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            DateTime dt2 = DateTime.Now;
            filterContext.Controller.ViewData["ActionExcuteDetails_dt2"] = dt2;
        }
        /// <summary>
        /// 在执行操作结果之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            DateTime dt3 = DateTime.Now;
            filterContext.Controller.ViewData["ActionExcuteDetails_dt3"] = dt3;

            //GZip
            string acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (String.IsNullOrEmpty(acceptEncoding)) return;
            var response = filterContext.HttpContext.Response;
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-Encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-Encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
            //GZip_End
        }
        /// <summary>
        /// 在执行操作结果后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            DateTime dt1 = (DateTime)filterContext.Controller.ViewData["ActionExcuteDetails_dt1"];
            DateTime dt2 = (DateTime)filterContext.Controller.ViewData["ActionExcuteDetails_dt2"];
            DateTime dt3 = (DateTime)filterContext.Controller.ViewData["ActionExcuteDetails_dt3"];
            DateTime dt4 = DateTime.Now;
            string ActionExcuteDetails_Log = string.Format("页面执行时间监控(ms)：页面打开总时间[{0}]，后台执行时间[{1}]，前台执行时间[{2}] URL:{3}", (dt4 - dt1).TotalMilliseconds, (dt2 - dt1).TotalMilliseconds, (dt4 - dt3).TotalMilliseconds, filterContext.HttpContext.Request.Url.ToString());
            //ExceptionEx.MyExceptionLog.AddLogError(ActionExcuteDetails_Log);
        }
    }

}
