using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Collections;
using DealMvc.Model;

namespace DealMvc.Filter
{

    /// <summary>
    /// 前台用户登录验证控制器属性（组织）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class MemberIsLoginAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            string Url = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString();//当前页面地址

            string ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString();

            ActionResult Result =
                new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Login",
                    ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString()//当前页面地址
                }));
            ActionResult Result_Org =
                new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "Member",
                    action = "Index",
                    //ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString()//当前页面地址
                }));

            //判断是否登录
            //Model.Member m_UUser = new Model.Member();
            //if (!Common.Globals.isUserLogin())
            //{
            //    filterContext.Result = Result;
            //    return;
            //}
            //else
            //{
            //    m_UUser = Model.Member.GetModel(t => t.M_UID == Common.Globals.getUserName() && t.M_IsLock == false);
            //}

            //if (m_UUser.IsNull)
            //{//判断是否登录
            //    filterContext.Result = Result;
            //    return;
            //}
            //else if (m_UUser.M_IsBusinesses)
            //{
            //    filterContext.Result = Result_Org;
            //    return;
            //}


        }
    }

    /// <summary>
    /// 前台用户登录验证控制器属性（个人）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class StoreIsLoginAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            string Url = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString();//当前页面地址

            string ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString();

            //ToUrl = DealMvc.DBUtility.DESEncrypt.Encrypt(ToUrl);

            ActionResult Result =
                new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Login",
                    ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString()//当前页面地址
                }));
            ActionResult Result_Org =
                new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "Member",
                    action = "Index",
                    //ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString()//当前页面地址
                }));

            //判断是否登录
            //Model.Member m_UUser = new Model.Member();
            //if (!Common.Globals.isUserLogin())
            //{
            //    filterContext.Result = Result;
            //    return;
            //}
            //else
            //{
            //    m_UUser = Model.Member.GetModel(t => t.M_UID == Common.Globals.getUserName() && t.M_IsLock == false);
            //}

            //if (m_UUser.IsNull)
            //{//判断是否登录
            //    filterContext.Result = Result;
            //    return;
            //}
            //else if (!m_UUser.M_IsBusinesses)
            //{
            //    filterContext.Result = Result_Org;
            //    return;
            //}
        }
    }

    /// <summary>
    /// 前台用户登录验证控制器属性（公用）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class CommonIsLoginAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            string Url = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString();//当前页面地址

            string ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString();

            //ToUrl = DealMvc.DBUtility.DESEncrypt.Encrypt(ToUrl);

            ActionResult Result =
                new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Login",
                    ToUrl = ((System.Web.HttpContextBase)filterContext.RequestContext.HttpContext).Request.Url.ToString()//当前页面地址
                }));

            //判断是否登录
            //Model.Member m_UUser = new Model.Member();
            //if (!Common.Globals.isUserLogin())
            //{
            //    filterContext.Result = Result;
            //    return;
            //}
            //else
            //{
            //    m_UUser = Model.Member.GetModel(t => t.M_UID == Common.Globals.getUserName() && t.M_IsLock == false);
            //}

            //if (m_UUser.IsNull)
            //{//判断是否登录
            //    filterContext.Result = Result;
            //    return;
            //}
        }
    }

    #region 权限过滤

    /// <summary>
    /// 控制器属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ControlInfoAttribute : Attribute
    {
        /// <summary>
        /// 控制器描述
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控制器描述信息
        /// </summary>
        /// <param name="info">控制器描述信息</param>
        public ControlInfoAttribute(string info)
        {
            Name = info;
        }

    }

    /// <summary>
    /// 控制器属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class RoleAttribute : FilterAttribute, IAuthorizationFilter
    {

        #region 反射用的属性

        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否要进行权限的反射
        /// </summary>
        public bool IsAuthorize { get; set; }

        /// <summary>
        /// 反射出来的Action Url
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// 默认：Description=Name , IsAuthorize=true
        /// </summary>
        /// <param name="_S_Name">权限名</param>
        public RoleAttribute(string _S_Name)
        {
            Name = _S_Name;
            Description = _S_Name;
            IsAuthorize = true;
        }


        #endregion

        #region 判断权限

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            #region 初始判断

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            #endregion

            #region 登陆的判断
            bool _isLogin = false;
            Model.Admin m_Admin = null;

            if (Common.Globals.isAdminLogin())
            {
                m_Admin = DealMvc.Model.Admin.getAdminByAdminID(Common.Globals.getAdminName());
                if (m_Admin != null) _isLogin = true;
            }

            if (_isLogin)
            {//已登录

                //是否启用了权限模块
                Model.SiteInfo m_SiteInfo = WebCacheHelper.GetSiteInfo();
                if (!m_SiteInfo.WebCompetence) return;

                string ControllerName = filterContext.RouteData.Values["controller"].ToString().Trim().ToLower();
                string ActionName = filterContext.RouteData.Values["action"].ToString().Trim().ToLower();

                string AdminPowerValues = "|" + m_Admin.AdminPowerValues.ToLower();
                if (AdminPowerValues.IndexOf("|" + ControllerName + "/" + ActionName + "#") >= 0 || m_Admin.AdminPowerValues.ToLower() == "all" || !IsAuthorize)
                {//有权限
                    return;
                }
                else
                {
                    //  /CmsAdmin/AEAdmin
                    //  /CmsAdmin/EAdminPwd
                    bool s = !string.IsNullOrEmpty(filterContext.RouteData.Values["a_i_id"] == null ? "" : filterContext.RouteData.Values["a_i_id"].ToString());

                    if (ControllerName == "CmsAdmin".ToLower() && ActionName == "AEAdmin".ToLower() && s)
                    {

                    }
                    else if (ControllerName == "CmsAdmin".ToLower() && ActionName == "EAdminPwd".ToLower() && s)
                    {

                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Cms", action = "NoAuthorize" }));
                    }
                    return;
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Cms", action = "Index" }));
                return;
            }

            #endregion

        }
        #endregion
    }

    #endregion

    #region 权限反射

    /// <summary>
    /// 对要进行权限反射的DLL
    /// </summary>
    public class RoleFactory
    {
        public static Dictionary<string, List<RoleAttribute>> GetAllAction()
        {
            //action 列表
            Dictionary<string, List<RoleAttribute>> ActionList = new Dictionary<string, List<RoleAttribute>>();

            SiteInfo m_SiteInfo = DealMvc.WebCacheHelper.GetSiteInfo();
            if (!m_SiteInfo.WebCompetence) return ActionList;

            try
            {
                //要进行反射的DLL
                string[] S_Ref_Dll = DealMvc.Common.Config.ConfigInfo<DealMvc.Common.Config.RoleDLLInfo>.Instance().path.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries); //new string[] { "~/bin/DealMvc.dll" };

                //遍历DLL文件
                foreach (string S_Dll in S_Ref_Dll)
                {
                    //获得DLL里的类型
                    Type[] ActionType = Assembly.LoadFile(System.Web.HttpContext.Current.Server.MapPath(S_Dll)).GetTypes();

                    //遍历DLL里的类型
                    foreach (Type Action_Ref in ActionType)
                    {
                        //获取类上的描述信息
                        object[] ClassObj = Action_Ref.GetCustomAttributes(typeof(ControlInfoAttribute), false);

                        if (ClassObj.Length < 1)
                            continue;

                        ControlInfoAttribute ClassObjAttr = ClassObj[0] as ControlInfoAttribute;


                        //获得类Action的Name属性
                        List<RoleAttribute> Type_Action_Lit = new List<RoleAttribute>();

                        //遍历Action
                        foreach (MethodInfo MI in Action_Ref.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                        {
                            object[] ActionObj = MI.GetCustomAttributes(typeof(RoleAttribute), false);

                            if (ActionObj.Length < 1)
                                continue;

                            RoleAttribute ActionRoleAttr = ActionObj[0] as RoleAttribute;

                            if (ActionRoleAttr.IsAuthorize)
                            {
                                //给Action Url赋值
                                ActionRoleAttr.ActionUrl = string.Format("{0}/{1}", Action_Ref.Name.Replace("Controller", ""), MI.Name);

                                Type_Action_Lit.Add(ActionRoleAttr);
                            }
                        }

                        //将类信息加到返回集里
                        ActionList.Add(ClassObjAttr.Name, Type_Action_Lit);
                    }
                }
            }
            catch
            {
                ExceptionEx.MyExceptionLog.AddLogError("反射权限dll出错");
            }

            return ActionList;
        }
    }

    #endregion


}
