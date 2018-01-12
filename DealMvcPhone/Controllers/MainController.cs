using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealMvc.Model;
using DealMvc.Common.Net;
using System.Text;
using System.Web.Mvc;
using DealMvc.Filter;
using System.Collections;
using DealMvc;

namespace DealMvcPhone.Controllers
{
    [HandleError]
    public class MainController : DealMvc.ControllerBase
    {

        #region 业务逻辑

        #endregion

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

      


    }
}
