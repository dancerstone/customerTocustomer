using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealMvc
{
    /// <summary>
    /// VerifyCodeImage 的摘要说明
    /// </summary>
    public class VerifyCodeImage : IHttpHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            Common.Code _Code = new Common.Code();
            _Code.ProcessRequest(context);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}