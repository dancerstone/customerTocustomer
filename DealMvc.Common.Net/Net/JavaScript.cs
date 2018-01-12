using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 网页脚本调用类(JavaScript)
    /// </summary>
    public class JavaScript
    {
        /// <summary>
        /// 浏览器类别分类
        /// </summary>
        public enum WebBrowserType
        {
            /// <summary>
            /// 仅IE
            /// </summary>
            IsIE = 0,
            /// <summary>
            /// 仅火狐
            /// </summary>
            IsFirefox = 1,
            /// <summary>
            /// 非IE
            /// </summary>
            NotIE = 2
        }

        #region 调用JavaScript脚本

        /// <summary>
        /// JavaScript脚本弹窗
        /// </summary>
        ///<param name="_Page">Page</param>
        /// <param name="_String">弹框字符串</param>
        public static void JavaScript_Alert(System.Web.UI.Page _Page, string _String)
        {
            _String = DealString.clearString_R_N(_String);
            string Sign = JavaScript.Create_Sign();
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, "alert(\"" + _String + "\");", true);
        }

        /// <summary>
        /// JavaScript脚本弹窗(手动输入标识符,合适在一次程序中多次弹窗使用)
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="_String">弹框字符串</param>
        /// <param name="M_Sign">标识符(一个字符串,可自定)</param>
        public static void JavaScript_Alert(System.Web.UI.Page _Page, string _String, string M_Sign)
        {
            _String = DealString.clearString_R_N(_String);
            string Sign = M_Sign;
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, "alert(\"" + _String + "\");", true);
        }

        /// <summary>
        /// JavaScript脚本执行程序,JS代码(Programe)可加可不加分号
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="_String">代码字符串</param>
        public static void JavaScript_Programe(System.Web.UI.Page _Page, string _String)
        {
            string Sign = JavaScript.Create_Sign();
            if (_String.LastIndexOf(";") != _String.Length - 1)
            {
                _String += ";";
            }
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, _String, true);
        }

        /// <summary>
        /// JavaScript脚本执行程序,可以区分浏览器类型来执行代码,JS代码(Programe)可加可不加分号
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="_String">代码字符串</param>
        /// <param name="_WebBrowserType">浏览器类型赛选(枚举)</param>
        public static void JavaScript_Programe(System.Web.UI.Page _Page, string _String, WebBrowserType _WebBrowserType)
        {
            if (_String.LastIndexOf(";") != _String.Length - 1)
            {
                _String += ";";
            }

            bool s = true;

            switch ((int)_WebBrowserType)
            {
                case 0://仅IE
                    if (!isIE(_Page))
                    {
                        s = false;
                    }
                    break;
                case 1://仅火狐
                    if (!isFireFox(_Page))
                    {
                        s = false;
                    }
                    break;
                case 2://非IE
                    if (isIE(_Page))
                    {
                        s = false;
                    }
                    break;
            }

            if (s)
            {
                JavaScript_Programe(_Page, _String);
            }
        }

        /// <summary>
        /// JavaScript定义或者修改对象的属性值,JS代码(Programe)可加可不加分号
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="ObjID">对象id</param>
        /// <param name="Programe">
        /// 程序代码
        /// [例如:实现document.getElementById('Mid').style.color = 'Red';]
        /// [写成:JavaScript_Programe_dom(Page, 'Mid', 'style.color = 'Red'')]
        /// </param>
        public static void JavaScript_Programe_dom(System.Web.UI.Page _Page, string ObjID, string Programe)
        {
            string Sign = JavaScript.Create_Sign();
            StringBuilder output = new StringBuilder();
            output.Append("document.getElementById('" + ObjID + "').");
            output.Append(Programe);
            if (Programe.LastIndexOf(";") != Programe.Length - 1)
            {
                output.Append(";");
            }
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        /// <summary>
        /// JavaScript对象是否隐藏
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="ObjID">对象id</param>
        /// <param name="Visible">设置隐藏或显示true为显示</param>
        public static void JavaScript_Programe_domDisplay(System.Web.UI.Page _Page, string ObjID, bool Visible)
        {
            string Sign = JavaScript.Create_Sign();
            StringBuilder output = new StringBuilder();
            if (Visible)
            {
                output.Append("document.getElementById('" + ObjID + "').");
                output.Append("style.display = 'block'");
                output.Append(";");
            }
            else
            {
                output.Append("document.getElementById('" + ObjID + "').");
                output.Append("style.display = 'none'");
                output.Append(";");
            }
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        /// <summary>
        /// JavaScript页面跳转
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="WebPath">页面转向地址</param>
        public static void JavaScript_Location_Href(System.Web.UI.Page _Page, string WebPath)
        {
            string Sign = JavaScript.Create_Sign();
            StringBuilder output = new StringBuilder();
            output.Append("location.href = '" + WebPath + "'");
            output.Append(";");
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        #endregion

        #region 自动转向error的图片,兼容虚拟目录

        /// <summary>
        /// 自动转向error的图片,兼容虚拟目录,调用此方法后整个页面的img标签都会增加onerror事件,如果不想某特定的img标签加上此统一事件,可在给此img标签增加一个 name="no-err" 的属性. 
        /// </summary>
        /// <param name="_Page">Page</param>
        public static void IMAGE_ERROR(System.Web.UI.Page _Page)
        {

            string output = "function IMAGE_ERROR(locationPath, xuniPath)";
            output += "{";
            output += "    var C_imgs = document.getElementsByTagName('img'); ";
            output += "    for(var i=0;i<C_imgs.length;i++) ";
            output += "    {";
            output += "        var CC_img = C_imgs[i];";
            output += "        if(CC_img.name != 'no-err'){";
            output += "        CC_img.onerror = function ()";
            output += "        {";
            output += "             if(this.src.indexOf(locationPath + xuniPath) < 0)";
            output += "             {";
            output += "                 this.src = this.src.replace(locationPath, locationPath + xuniPath)";
            output += "             }";
            output += "             ";
            output += "        }}";
            output += "    }";
            output += "}";
            string output2 = "IMAGE_ERROR('" + _Page.Request.Url.GetLeftPart(System.UriPartial.Authority) + "','" + _Page.Request.ApplicationPath + "');";
            JavaScript_Programe(_Page, output);
            JavaScript_Programe(_Page, output2);
        }

        #endregion

        #region 随机数

        /// <summary>
        /// 赋值给随机函数
        /// </summary>
        private static int _Sign = 0;

        /// <summary>
        /// 生成一个随机数
        /// </summary>
        /// <returns>string</returns>
        private static string Create_Sign()
        {
            //生成一个不相同的随机字符串
            string Sign = "alertType_";
            Sign += DateTime.Now.Ticks.ToString();
            Sign += new Random().Next(1, 99999).ToString();
            Sign += _Sign.ToString();
            _Sign++;
            return Sign;
        }

        #endregion

        #region 浏览器类型

        /// <summary>
        /// 返回浏览器类型
        /// </summary>
        /// <param name="_Page"></param>
        /// <returns></returns>
        public static string BrowserType(System.Web.UI.Page _Page)
        {
            System.Web.HttpBrowserCapabilities brObject = _Page.Request.Browser;
            return brObject.Type;
        }
        /// <summary>
        /// 是否是ie
        /// </summary>
        /// <param name="_Page"></param>
        /// <returns></returns>
        public static bool isIE(System.Web.UI.Page _Page)
        {
            if (BrowserType(_Page).ToLower().IndexOf("ie") >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否是火狐
        /// </summary>
        /// <param name="_Page"></param>
        /// <returns></returns>
        public static bool isFireFox(System.Web.UI.Page _Page)
        {
            if (BrowserType(_Page).ToLower().IndexOf("firefox") >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
