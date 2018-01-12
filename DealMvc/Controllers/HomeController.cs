using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Linq.Expressions;
using DealMvc.Model;
using System.Text;
using System.Xml;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using DealMvc.Common;

namespace DealMvc.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [HandleError]
    public class HomeController : ControllerBase
    {
        #region 业务逻辑
        #endregion

        #region 网站首页

        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            AddTitle("首页");
            return View();
        }

        #endregion

        


    }


}
