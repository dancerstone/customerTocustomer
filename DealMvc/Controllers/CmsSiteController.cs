using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealMvc.Common;
using DealMvc.Filter;
using DealMvc.Model;
namespace DealMvc.Controllers
{
    [ControlInfo("系统模块")]
    public class CmsSiteController : ControllerBase
    {
        #region 业务逻辑
        /// <summary>
        /// 实例化 网站配置信息业务逻辑
        /// </summary>
        public DealMvc.Core.Base.BLL_SiteInfo b_BLL_SiteInfo = new DealMvc.Core.Base.BLL_SiteInfo();
        /// <summary>
        /// 实例化 网站邮箱配置业务逻辑
        /// </summary>
        public DealMvc.Core.Base.BLL_SiteEmail b_BLL_SiteEmail = new DealMvc.Core.Base.BLL_SiteEmail();
        /// <summary>
        /// 实例化 网站上传信息业务逻辑
        /// </summary>
        public DealMvc.Core.Base.BLL_SiteUpLoad b_BLL_SiteUpLoad = new DealMvc.Core.Base.BLL_SiteUpLoad();
        /// <summary>
        /// 实例化 网站短信配置业务逻辑
        /// </summary>
        public DealMvc.Core.Base.BLL_SiteMessage b_BLL_SiteMessage = new DealMvc.Core.Base.BLL_SiteMessage();
        /// <summary>
        /// 实例化 网站支付信息业务逻辑
        /// </summary>
        public DealMvc.Core.Base.BLL_SitePayAPI b_BLL_SitePayAPI = new DealMvc.Core.Base.BLL_SitePayAPI();
        
        /// <summary>
        /// 实例化 积分设置业务逻辑
        /// </summary>
        public DealMvc.Core.Base.BLL_PointsSet b_BLL_PointsSet = new DealMvc.Core.Base.BLL_PointsSet();
        #endregion

        #region 网站基本信息设置
        /// <summary>
        /// 网站基本信息设置
        /// </summary>
        /// <param name="p_si"></param>
        /// <returns></returns>
        [Role("站点设置", Description = "站点设置", IsAuthorize = true)]
        public ActionResult SiteBase(SiteInfo p_si)
        {
            SiteInfo m_site = SiteInfo.GetModel(t => t.id != 0);
            if (IsGet)
            {
                SetSaveFormCollection = b_BLL_SiteInfo.NameValueCollectionEx(ref m_site);
            }
            if (IsPost)
                try
                {
                    p_si.id = m_site.id;
                    b_BLL_SiteInfo.AESiteInfo(this, true, ref p_si);
                }
                catch (Exception ce)
                {
                    IsSaveForm = true;
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            try
            {
                ViewData["_WebFriendLinks"] = string.IsNullOrEmpty(p_si.WebFriendLinks) ? m_site.WebFriendLinks : p_si.WebFriendLinks;
                ViewData["_WebWeiXinImage"] = string.IsNullOrEmpty(p_si.WebWeiXinImage) ? m_site.WebWeiXinImage : p_si.WebWeiXinImage;
                ViewData["_WebWeiBoImage"] = string.IsNullOrEmpty(p_si.WebWeiBoImage) ? m_site.WebWeiBoImage : p_si.WebWeiBoImage;
            }
            catch { }
            return View(p_si ?? new SiteInfo());
        }
        #endregion

        #region 邮箱信息设置
        /// <summary>
        /// 邮箱信息设置
        /// </summary>
        /// <param name="smtp"></param>
        /// <param name="emailname"></param>
        /// <param name="email"></param>
        /// <param name="emailpwd"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        [Role("邮箱设置", Description = "邮箱设置", IsAuthorize = true)]
        public ActionResult EmailBase(SiteEmail p_se)
        {
            SiteEmail m_se = SiteEmail.GetModel(t => t.id != 0);
            if (IsGet)
            {
                SetSaveFormCollection = b_BLL_SiteEmail.NameValueCollectionEx(ref m_se);
            }
            if (IsPost)
                try
                {
                    p_se.id = m_se.id;
                    b_BLL_SiteEmail.AESiteEmail(this, true, ref p_se);
                }
                catch (Exception ce)
                {
                    IsSaveForm = true;
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            return View(p_se ?? new SiteEmail());
        }


        public ActionResult EmailBaseTest(string TestEmail)
        {
            if (IsPostMvc)
                try
                {

                    DealMvc.SendMessage.Message.SendEmail("测试标题", "测试内容", TestEmail.ToString2());

                    MsgClass.AlertMsg(this, "邮箱信息已发送，请注意查看", true);
                }
                catch (Exception ex)
                {
                    ExceptionEx.MyExceptionLog.WriteLog(this, ex);
                }
            return RedirectToAction("EmailBase");
        }



        #endregion

        //#region 上传文件保存目录设置
        ///// <summary>
        ///// 上传文件保存目录设置
        ///// </summary>
        ///// <param name="UploadFolder"></param>
        ///// <param name="UploadExtension"></param>
        ///// <param name="UploadSize"></param>
        ///// <returns></returns>
        //[Role("上传文件设置", Description = "上传文件设置", IsAuthorize = true)]
        //public ActionResult UpLoadFileBase(SiteUpLoad p_sul)
        //{
        //    SiteUpLoad m_su = SiteUpLoad.GetModel(t => t.id != 0);
        //    if (IsGet)
        //        try
        //        {
        //            SetSaveFormCollection = b_BLL_SiteUpLoad.NameValueCollectionEx(ref m_su);
        //        }
        //        catch { return RedirectToAction("SiteUpLoadList"); }
        //    if (IsPost)
        //        try
        //        {
        //            p_sul.id = m_su.id;
        //            b_BLL_SiteUpLoad.AESiteUpLoad(this, true, ref p_sul);
        //        }
        //        catch (Exception ce)
        //        {
        //            IsSaveForm = true;
        //            ExceptionEx.MyExceptionLog.WriteLog(this, ce);
        //        }
        //    return View(p_sul ?? new SiteUpLoad());
        //}
        //#endregion

        #region 短信设置
        /// <summary>
        /// 短信设置
        /// </summary>
        /// <param name="UploadFolder"></param>
        /// <param name="UploadExtension"></param>
        /// <param name="UploadSize"></param>
        /// <returns></returns>
        [Role("短信设置", Description = "短信设置", IsAuthorize = true)]
        public ActionResult MessageBase(SiteMessage p_sul)
        {
            SiteMessage m_su = SiteMessage.GetModel(t => t.id != 0);
            if (IsGet)
            {
                SetSaveFormCollection = b_BLL_SiteMessage.NameValueCollectionEx(ref m_su);
            }
            if (IsPost)
                try
                {
                    p_sul.id = m_su.id;
                    b_BLL_SiteMessage.AESiteMessage(this, true, ref p_sul);
                }
                catch (Exception ce)
                {
                    IsSaveForm = true;
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            return View(p_sul ?? new SiteMessage());
        }
        #endregion

        #region 支付宝信息设置
        /// <summary>
        /// 支付宝信息设置
        /// </summary>
        /// <returns></returns>
        [Role("支付宝信息设置", Description = "支付宝信息设置", IsAuthorize = true)]
        public ActionResult AlipayBase(SitePayAPI p_spapi)
        {
            SitePayAPI m_sp = SitePayAPI.GetModel(t => t.ApiType == "支付宝");
            if (IsGet)
            {
                SetSaveFormCollection = b_BLL_SitePayAPI.NameValueCollectionEx(ref m_sp);
            }
            if (IsPost)
                try
                {
                    p_spapi.id = m_sp.id;
                    b_BLL_SitePayAPI.AESitePayAPI(this, true, ref p_spapi);
                }
                catch (Exception ce)
                {
                    IsSaveForm = true;
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            return View(p_spapi ?? new SitePayAPI());
        }
        #endregion

        #region 网银在线信息设置
        /// <summary>
        /// 网银在线设置
        /// </summary>
        /// <returns></returns>
        [Role("网银在线信息设置", Description = "网银在线信息设置", IsAuthorize = true)]
        public ActionResult TenpayBase(SitePayAPI p_spapi)
        {
            SitePayAPI m_sp = SitePayAPI.GetModel(t => t.ApiType == "财付通");
            if (IsGet)
            {
                SetSaveFormCollection = b_BLL_SitePayAPI.NameValueCollectionEx(ref m_sp);
            }
            if (IsPost)
                try
                {
                    p_spapi.id = m_sp.id;
                    b_BLL_SitePayAPI.AESitePayAPI(this, true, ref p_spapi);
                }
                catch (Exception ce)
                {
                    IsSaveForm = true;
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            return View(p_spapi ?? new SitePayAPI());
        }
        #endregion

        //#region 城市
        //[Role("城市列表", Description = "城市列表", IsAuthorize = true)]
        //public ActionResult SiteCityList(int? Page, string SearchLevel)
        //{
        //    DealMvc.Orm.PagerEx.Pager<SiteCity> _Pager = b_BLL_SiteCity.PagerList(Page, SearchLevel);
        //    ViewData["Pager"] = _Pager;
        //    IsSaveForm = true;
        //    return View();
        //}

        //[Role("添加/编辑城市", Description = "添加/编辑城市", IsAuthorize = true)]
        //public ActionResult SiteCityAE(SiteCity p_sc, int? sheng, int? shi)
        //{
        //    bool isEdit = p_sc.id != null;
        //    ViewData["isEdit"] = isEdit;
        //    if (IsGet && isEdit)
        //        try
        //        {
        //            SetSaveFormCollection = b_BLL_SiteCity.NameValueCollectionEx(ref p_sc);
        //        }
        //        catch { return RedirectToAction("SiteCityList"); }
        //    if (IsPost)
        //        try
        //        {
        //            if (p_sc.C_Level == 1)
        //            {
        //                p_sc.CitysTopID = 0;
        //            }
        //            else if (p_sc.C_Level == 2)
        //            {
        //                p_sc.CitysTopID = sheng.ToInt32();
        //            }
        //            else if (p_sc.C_Level == 3)
        //            {
        //                p_sc.CitysTopID = shi.ToInt32();
        //            }
        //            b_BLL_SiteCity.AESiteCity(this, isEdit, ref p_sc);
        //            if (!isEdit) return ViewAndEmpty();
        //        }
        //        catch (Exception ce)
        //        {
        //            IsSaveForm = true;
        //            ExceptionEx.MyExceptionLog.WriteLog(this, ce);
        //        }
        //    return View(p_sc ?? new SiteCity());
        //}

        //[Role("城市信息", Description = "城市信息", IsAuthorize = true)]
        //public ActionResult SiteCityV(int? id)
        //{
        //    SiteCity m_sc = SiteCity.GetModel(id ?? 0);
        //    if (m_sc.IsNull)
        //        return RedirectToAction("SiteCityList");
        //    return View(m_sc ?? new SiteCity());
        //}

        //[Role("删除城市", Description = "删除城市", IsAuthorize = true)]
        //public ContentResult DeleteSiteCity(string ids)
        //{
        //    return Content(b_BLL_SiteCity.DeleteSiteCity(ids, null).ToString2().ToLower());
        //}
        //#endregion

        //#region 商圈
        //[Role("商圈列表", Description = "商圈列表", IsAuthorize = true)]
        //public ActionResult BusinessCircleList(int? Page)
        //{
        //    DealMvc.Orm.PagerEx.Pager<BusinessCircle> _Pager = b_BLL_BusinessCircle.PagerList(Page);
        //    ViewData["Pager"] = _Pager;
        //    IsSaveForm = true;
        //    return View();
        //}

        //[Role("添加/编辑商圈", Description = "添加/编辑商圈", IsAuthorize = true)]
        //public ActionResult BusinessCircleAE(BusinessCircle p_bc)
        //{
        //    bool isEdit = p_bc.id != null;
        //    ViewData["isEdit"] = isEdit;
        //    if (IsGet && isEdit)
        //        try
        //        {
        //            SetSaveFormCollection = b_BLL_BusinessCircle.NameValueCollectionEx(ref p_bc);
        //        }
        //        catch { return RedirectToAction("BusinessCircleList"); }
        //    if (IsPost)
        //        try
        //        {
        //            b_BLL_BusinessCircle.AEBusinessCircle(this, isEdit, ref p_bc);
        //            if (!isEdit) return ViewAndEmpty();
        //        }
        //        catch (Exception ce)
        //        {
        //            IsSaveForm = true;
        //            ExceptionEx.MyExceptionLog.WriteLog(this, ce);
        //        }
        //    return View(p_bc ?? new BusinessCircle());
        //}

        //[Role("商圈信息", Description = "商圈信息", IsAuthorize = true)]
        //public ActionResult BusinessCircleV(int? id)
        //{
        //    BusinessCircle m_bc = BusinessCircle.GetModel(id ?? 0);
        //    if (m_bc.IsNull)
        //        return RedirectToAction("BusinessCircleList");
        //    return View(m_bc ?? new BusinessCircle());
        //}

        //[Role("删除商圈", Description = "删除商圈", IsAuthorize = true)]
        //public ContentResult DeleteBusinessCircle(string ids)
        //{
        //    return Content(b_BLL_BusinessCircle.DeleteBusinessCircle(ids, null).ToString2().ToLower());
        //}
        //#endregion

        //#region 积分设置
        //[Role("积分设置", Description = "积分设置", IsAuthorize = true)]
        //public ActionResult PointsSetAE(PointsSet p_ps)
        //{
        //    PointsSet m_ps = PointsSet.GetModel(t => t.id != null);
        //    bool isEdit = p_ps.id != null;
        //    ViewData["isEdit"] = isEdit;
        //    if (IsGet && isEdit)
        //        try
        //        {
        //            SetSaveFormCollection = b_BLL_PointsSet.NameValueCollectionEx(ref m_ps);
        //        }
        //        catch { return RedirectToAction("PointsSetAE"); }
        //    if (IsPost)
        //        try
        //        {
        //            b_BLL_PointsSet.AEPointsSet(this, isEdit, ref p_ps);
        //            if (!isEdit) return ViewAndEmpty();
        //        }
        //        catch (Exception ce)
        //        {
        //            IsSaveForm = true;
        //            ExceptionEx.MyExceptionLog.WriteLog(this, ce);
        //        }
        //    return View(m_ps ?? new PointsSet());
        //}
        //#endregion

    }
}
