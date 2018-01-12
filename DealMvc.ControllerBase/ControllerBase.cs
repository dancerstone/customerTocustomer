using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using ExceptionEx;

namespace DealMvc
{
    /// <summary>
    /// NameValueCollection扩展
    /// </summary>
    public class NameValueCollectionEx : NameValueCollection
    {
        /// <summary>
        /// 增加一个Add方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="str_s"></param>
        public void Add(string name, string[] str_s)
        {
            for (int i = 0; i < str_s.Length; i++)
            {
                string val = str_s[i].ToString();
                if (!string.IsNullOrEmpty(val))
                { base.Add(name, val); }
            }
        }

        /// <summary>
        /// 增加一个Add方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public void Add(string name, object obj)
        {
            if (obj != null)
                base.Add(name, obj.ToString());
        }
    }

    /// <summary>
    /// Controller基类
    /// </summary>
    [ActionExcuteDetails()]
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public ControllerBase()
            : base()
        {
            this.ValidateRequest = false;

        }

        #region 提交方式

        private string H_key = "___R_hidd";
        private string S_key = "___R";
        private bool UserCookies = true;

        /// <summary>
        /// 是否是Post方式
        /// </summary>
        public bool IsPostMvc
        {
            get { return HttpContext.Request.HttpMethod.ToString().ToUpper().Trim() == "POST"; }
        }
        /// <summary>
        /// 是否是Post方式
        /// </summary>
        public bool IsPost
        {
            get { return HttpContext.Request.HttpMethod.ToString().ToUpper().Trim() == "POST"; }
            //get
            //{
            //    string ControllerName = ControllerContext.RouteData.Values["controller"].ToString().Trim().ToLower();
            //    string ActionName = ControllerContext.RouteData.Values["action"].ToString().Trim().ToLower();
            //    string _S_key = S_key;
            //    //if (ControllerName.IndexOf("cms") == 0)
            //    _S_key += ControllerName + ActionName;

            //    bool s = false;
            //    if (HttpContext.Request.HttpMethod.ToString().ToUpper().Trim() == "POST")
            //    {
            //        //防止重复提交
            //        if (!string.IsNullOrEmpty(GetZ(_S_key)) && Request[H_key] != null)
            //        {
            //            string R = Request[H_key];
            //            string R2 = GetZ(_S_key);
            //            if (R == R2) s = true;
            //        }
            //        else
            //            s = true;

            //        if (!s) IsSaveForm = true;

            //    }
            //    Re_R_Value(_S_key, H_key);

            //    return s;
            //}
        }
        ///// <summary>
        ///// 跟新值
        ///// </summary>
        //private void Re_R_Value(string S_key, string H_key)
        //{
        //    string val = new Random().Next(1, 9999).ToString();
        //    SetZ(S_key, val);
        //    ViewData[H_key] = val;
        //}
        //private string GetZ(string S_key)
        //{
        //    if (UserCookies)
        //        return Common.Globals.getCookie(S_key);
        //    else
        //        return Session[S_key].ToString2();
        //}
        //private void SetZ(string S_key, string value)
        //{
        //    if (UserCookies)
        //        Common.Globals.setCookie(S_key, value);
        //    else
        //        Session[S_key] = value;
        //}

        /// <summary>
        /// 是否是Get方式
        /// </summary>
        public bool IsGet
        {
            get { return HttpContext.Request.HttpMethod.ToString().ToUpper().Trim() == "GET"; }
        }

        #endregion

        #region 保存表单值

        /// <summary>
        /// 是否保存forms值
        /// </summary>
        public bool IsSaveForm
        {
            set
            {
                if (value)
                {
                    ViewData["S_From"] = Request.Form;
                    ViewData["S_From2"] = Request.QueryString;
                    for (int i = 0; i < Request.Form.Count; i++)
                    {
                        string key = Request.Form.GetKey(i).ToString();
                        string[] values = Request.Form.GetValues(i);
                        if (values == null || values.Length < 1 || key == H_key) { continue; }
                        ViewData[key] = values.Length == 1 ? (object)values[0] : values;
                    }
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        string key = Request.QueryString.GetKey(i).ToString();
                        string[] values = Request.QueryString.GetValues(i);
                        if (values == null || values.Length < 1 || key == H_key) { continue; }
                        ViewData[key] = values.Length == 1 ? (object)values[0] : values;
                    }
                }
            }
        }

        /// <summary>
        /// 设置forms集合
        /// </summary>
        public NameValueCollectionEx SetSaveFormCollection
        {
            set
            {
                ViewData["S_From"] = value;
                for (int i = 0; i < value.Count; i++)
                {
                    string key = value.GetKey(i).ToString();
                    string[] values = value.GetValues(i);
                    if (values == null || values.Length < 1 || key == H_key) { continue; }
                    ViewData[key] = values.Length == 1 ? (object)values[0] : values;
                }
            }
        }

        #endregion

        #region 清空表单值

        /// <summary>
        /// 重新以Get方式载入当前Action
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAndEmpty()
        {
            return Redirect(this.Request.Url.ToString());
        }

        #endregion

        #region Head

        /// <summary>
        /// 增加Title
        /// </summary>
        /// <param name="Tit"></param>
        public void AddTitle(string Tit)
        {
            Common.HeaderHelper.AddTitle(Tit);
        }
        /// <summary>
        /// 增加KeyWord
        /// </summary>
        /// <param name="KeyWord"></param>
        public void AddKeyWord(string KeyWord)
        {
            Common.HeaderHelper.AddKeyWord(KeyWord);
        }
        /// <summary>
        /// 增加Description
        /// </summary>
        /// <param name="Description"></param>
        public void AddDescription(string Description)
        {
            Common.HeaderHelper.AddDescription(Description);
        }
        /// <summary>
        /// 修改当前SiteMapTitle名称
        /// </summary>
        /// <param name="SiteMapTitle">当前SiteMapTitle名称</param>
        public void AddSiteMapTitle(string SiteMapTitle)
        {
            ViewData["___Now_SiteMap_Title"] = SiteMapTitle;
        }
        #endregion
        #region 常用方法-Demo

        /// <summary>
        /// 是否跳转弹出提示 ClassCode.AlertMsg
        /// </summary>
        /// <param name="_Controller"></param>
        /// <param name="msg"></param>
        /// <param name="IsRedirect"></param>
        /// <param name="RedirectUrl"></param>
        /// <returns></returns>
        public ActionResult ViewAlert(Controller _Controller, string msg, bool IsRedirect, string RedirectUrl)
        {
            MsgClass.AlertMsg(this, msg, IsRedirect);
            if (IsRedirect)
                return Redirect(RedirectUrl);
            else
                return View();
        }
        /// <summary>
        /// 不跳转，立即调整到本页 ClassCode.AlertMsg
        /// </summary>
        /// <param name="_Controller"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult ViewAlert(Controller _Controller, string msg)
        {
            return ViewAlert(_Controller, msg, false, null);
        }
        /// <summary>
        ///  ClassCode.AlertMsg(this, msg);
        ///  return View(model);
        /// </summary>
        /// <param name="_Controller"></param>
        /// <param name="msg"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ViewAlert(Controller _Controller, string msg, object model)
        {
            MsgClass.AlertMsg(this, msg);
            return View(model);
        }
        public ActionResult ViewAlert(Controller _Controller, string msg, string ViewName)
        {
            MsgClass.AlertMsg(this, msg);
            return View(ViewName);
        }

        #endregion

        #region 自定义JSON MSG
        #region JSON msg:'err',alt:'" + alt + "
        /// <summary>
        /// 
        /// </summary>'
        public enum JsonMsgType
        {
            err = 1, success
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_JsonMsgType"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public ActionResult JsonMsgAlt(JsonMsgType _JsonMsgType, string alt)
        {
            return Json("{msg:'" + _JsonMsgType + "',alt:'" + alt + "'}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alt"></param>
        /// <returns></returns>
        public ActionResult JsonMsgAltErr(string alt)
        {
            return Json("{msg:'" + JsonMsgType.err + "',alt:'" + alt + "'}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_JsonMsgType"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public ActionResult JsonMsgAltSBuilder(JsonMsgType _JsonMsgType, string StringBuilder)
        {
            return Json("{msg:'" + _JsonMsgType + "'" + StringBuilder + "}");
        }
        /// <summary>
        /// 提示：请您稍后再试！
        /// </summary>
        /// <returns></returns>
        public ActionResult JsonMsgAltErrMsg()
        {
            return Json("{msg:'" + JsonMsgType.err + "',alt:'提示：请您稍后再试！'}");
        }
        /// <summary>
        /// 提示：图片加载失败！
        /// </summary>
        /// <returns></returns>
        public ActionResult JsonMsgAltErrMsg_Img()
        {
            return Json("{msg:'" + JsonMsgType.err + "',alt:'提示：图片加载失败！'}");
        }

        #endregion
        #endregion
    }

}
