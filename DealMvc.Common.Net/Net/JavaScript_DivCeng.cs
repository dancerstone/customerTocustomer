using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 信息提示(层)
    /// </summary>
    public class JavaScript_DivCeng
    {
        /// <summary>
        /// JavaScript显示提示信息(层)
        /// </summary>
        ///<param name="_Page">Page</param>
        /// <param name="_String">提示信息</param>
        public static void JavaScript_Msg(System.Web.UI.Page _Page, string _String)
        {
            string Sign = JavaScript_DivCeng.Create_Sign();

            StringBuilder CSS = JavaScript_DivCeng.DivCSS();
            StringBuilder output = JavaScript_DivCeng.DivString(_String, CSS, 0, 0, false);
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        /// <summary>
        /// JavaScript显示提示信息(层),自定义Top和Left距离
        /// </summary>
        ///<param name="_Page">Page</param>
        /// <param name="_String">提示信息</param>
        /// <param name="_Left">左边距离</param>
        /// <param name="_Top">右边距离</param>
        public static void JavaScript_Msg(System.Web.UI.Page _Page, string _String, int _Top, int _Left)
        {
            string Sign = JavaScript_DivCeng.Create_Sign();

            StringBuilder CSS = JavaScript_DivCeng.DivCSS();
            StringBuilder output = JavaScript_DivCeng.DivString(_String, CSS, _Top, _Left, false);
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        /// <summary>
        /// JavaScript显示提示信息(层),两秒后自动隐藏
        /// </summary>
        ///<param name="_Page">Page</param>
        /// <param name="_String">提示信息</param>
        public static void JavaScript_Msg_ToVisible(System.Web.UI.Page _Page, string _String)
        {
            string Sign = JavaScript_DivCeng.Create_Sign();

            StringBuilder CSS = JavaScript_DivCeng.DivCSS();
            StringBuilder output = JavaScript_DivCeng.DivString(_String, CSS, 0, 0, true);
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        /// <summary>
        /// JavaScript显示提示信息(层),自定义Top和Left距离,两秒后自动隐藏
        /// </summary>
        ///<param name="_Page">Page</param>
        /// <param name="_String">提示信息</param>
        /// <param name="_Left">左边距离</param>
        /// <param name="_Top">右边距离</param>
        public static void JavaScript_Msg_ToVisible(System.Web.UI.Page _Page, string _String, int _Top, int _Left)
        {
            string Sign = JavaScript_DivCeng.Create_Sign();

            StringBuilder CSS = JavaScript_DivCeng.DivCSS();
            StringBuilder output = JavaScript_DivCeng.DivString(_String, CSS, _Top, _Left, true);
            //输出脚本
            _Page.ClientScript.RegisterStartupScript(_Page.GetType(), Sign, output.ToString(), true);
        }

        /// <summary>
        /// div组合
        /// </summary>
        /// <returns></returns>
        private static StringBuilder DivString(string _String, StringBuilder _CSS, int _Top, int _Left, bool _CanVisible)
        {
            StringBuilder output = new StringBuilder();

            if (_Top == 0 && _Left == 0)
            {
                output.Append("$_$Top$_$ = 220;");
                output.Append("$_$Left$_$ = parseInt(document.body.clientWidth)/2 - 105;");
            }
            else
            {
                output.Append("$_$Top$_$ = " + _Top + ";");
                output.Append("$_$Left$_$ = " + _Left + ";");
            }
            output.Append("$_$DivID$_$ = '$_$MyDiv$_$';");
            output.Append("$_$DivIDCeng$_$ = 'mybg_mybg_mybg';");
            output.Append("$_$none$_$ = 'none';");
            output.Append("$_$block$_$ = 'block';");

            output.Append(bjCeng());

            output.Append("if(document.getElementById($_$DivID$_$))");
            output.Append("{");
            output.Append("document.getElementById($_$DivID$_$).innerHTML = '" + _String + "';");
            output.Append("document.getElementById($_$DivID$_$).style.display = $_$block$_$;");
            output.Append("}else{");
            output.Append("$_$DivString$_$ = \"<div id='\" + $_$DivID$_$ + \"' style='" + _CSS.ToString() + "'>\";");
            output.Append("$_$DivString$_$ += \"<div style='width:158px;height:60px; border:1px solid #DAAD23;background:#FFF0C1;padding-top:8px;color:#F86300;text-align:center;margin-left:24px;'>" + _String + "</div>\";");
            output.Append("$_$DivString$_$ += \"<div style='padding:5px 0;'>\";");
            if (!_CanVisible)
            {
                output.Append("$_$DivString$_$ += \"<input id='$_$Button$_$' type='button' value='确 定' onclick='document.getElementById($_$DivID$_$).style.display = $_$none$_$;document.getElementById($_$DivIDCeng$_$).style.display = $_$none$_$;' border='0' style='width:85px; background:#F3DE9C;border:1px solid #DAAD23;padding-top:4px;cursor:pointer;color:#745A0C;margin-left:60px;' />\";");
            }
            else
            {
                output.Append("$_$DivString$_$ += \"<input id='$_$Button$_$' type='button' value='自动关闭' border='0' disabled='disabled' style='width:85px;background:#F3DE9C;border:1px solid #DAAD23;padding-top:4px;color:#745A0C;margin-left:60px;' />\";");
            }
            output.Append("$_$DivString$_$ += '</div>';");
            output.Append("$_$DivString$_$ += '</div>';");
            output.Append("browStr = navigator.userAgent;");
            output.Append("if(browStr.indexOf('Firefox') != '-1')");
            output.Append("{");
            output.Append("document.getElementsByTagName(\"BODY\")[0].innerHTML += $_$DivString$_$;");
            output.Append("}else{");
            output.Append("document.body.firstChild.innerHTML += $_$DivString$_$;");
            output.Append("}");
            output.Append("try{document.getElementById('$_$Button$_$').focus();}catch(e){}");
            output.Append("}");
            if (_CanVisible)
            {
                output.Append("setTimeout('new function(){document.getElementById($_$DivID$_$).style.display = $_$none$_$;document.getElementById($_$DivIDCeng$_$).style.display = $_$none$_$;}',2000);");   
            }

            return output;
        }

        /// <summary>
        /// divCSS
        /// </summary>
        /// <returns></returns>
        private static StringBuilder DivCSS()
        {
            StringBuilder CSS = new StringBuilder();
            CSS.Append("position:absolute;");
            CSS.Append("top:\" + $_$Top$_$ + \"px;");
            CSS.Append("left:\" + $_$Left$_$ + \"px;");
            CSS.Append("z-index:1000;");
            CSS.Append("width:208px;");
            CSS.Append("height:72px !importent height:94px;");
            CSS.Append("background:#FCE08A;");
            CSS.Append("border:1px solid #BE9002;");
            CSS.Append("font-size:14px;");
            CSS.Append("text-align:left;");
            CSS.Append("padding-top:22px;");
            CSS.Append("word-break:break-all;");
            return CSS;
        }

        /// <summary>
        /// body背景层
        /// </summary>
        /// <returns></returns>
        private static string bjCeng()
        {
            StringBuilder output = new StringBuilder();
            output.Append("window.onload = function(){");
            output.Append("$_mybg_$ = document.createElement('div');");
            output.Append("$_mybg_$.setAttribute('id', $_$DivIDCeng$_$);");
            output.Append("$_mybg_$.style.background = '#000';");
            output.Append("$_mybg_$.style.width = parseInt(document.documentElement.clientWidth)+'px';");
            output.Append("$_mybg_$.style.height =parseInt(document.documentElement.clientHeight)+'px';");
            output.Append("$_mybg_$.style.position = 'absolute';");
            output.Append("$_mybg_$.style.top = '0';");
            output.Append("$_mybg_$.style.left = '0';");
            output.Append("$_mybg_$.style.zIndex = '500';");
            output.Append("$_mybg_$.style.opacity = '0.3';");
            output.Append("$_mybg_$.style.filter = 'Alpha(opacity=30)';");
            output.Append("document.body.appendChild($_mybg_$);");
            output.Append("document.body.style.overflow = 'hidden';");
            output.Append("};");
            return output.ToString();
        }

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
    }
}
