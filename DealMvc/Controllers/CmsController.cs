using System;
using System.Web.Mvc;

namespace DealMvc.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class CmsController : ControllerBase
    {

        /// <summary>
        /// 后台登陆页面
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public ActionResult Index(string username, string userpwd, string usercode)
        {
            if (IsPost)
            {
                try
                {
                    string code = DealMvc.Common.Globals.getCode().ToString2();

                    if (code.ToLower() != usercode.ToLower()) 
                        throw new ExceptionEx.MyExceptionMessageBox("验证码不正确");
                    Model.Admin m_XShop_Admins = Model.Admin.GetModel(t => t.AdminID == username.Trim() && t.AdminPwd == userpwd.Jmd5());
                    if (m_XShop_Admins == null || m_XShop_Admins.IsNull)
                        throw new ExceptionEx.MyExceptionMessageBox("帐号或密码不正确");

                    //跟新帐号的登录时间
                    m_XShop_Admins.UpTime = DateTime.Now;
                    m_XShop_Admins.Update();

                    DealMvc.Common.Globals.setAdminName(m_XShop_Admins.AdminID);
                    return RedirectToAction("do");
                }
                catch (Exception ce)
                {
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            }

            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Do()
        {
            try
            {
                if (!DealMvc.Common.Globals.isAdminLogin()) return RedirectToAction("Index");

                Model.Admin m_Admin = DealMvc.Model.Admin.getAdminByAdminID(DealMvc.Common.Globals.getAdminName());
                if (m_Admin == null) return RedirectToAction("index", "cms");

                ViewData["model"] = m_Admin;
            }
            catch { }

            return View();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            DealMvc.Common.Globals.setAdminName("");
            return RedirectToAction("index");
        }

        /// <summary>
        /// 没有权限提示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult NoAuthorize()
        {
            return View();
        }

        /// <summary>
        /// 清空所有的数据缓存
        /// </summary>
        /// <returns></returns>
        public ContentResult ClearWebCache()
        {
            if (IsPost)
            {
                try
                {
                    //数据缓存
                    DealMvc.WebCache.WebCache.Clear();
                }
                catch { return Content("0"); }
                return Content("1");
            }
            return Content("0");
        }
    }
}