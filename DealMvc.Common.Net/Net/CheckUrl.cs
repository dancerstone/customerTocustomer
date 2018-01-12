using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 检查网页Url地址是否含有非法字符
    /// </summary>
    public class CheckUrl
    {
        /// <summary>
        /// 检查网页Url地址是否含有非法字符(防注入),有非法字符串就跳转到首页
        /// </summary>
        /// <param name="_Page">Page对象</param>
        /// <returns></returns>
        public static void HasSQL(System.Web.UI.Page _Page)
        {
            //网页地址路径
            string _URL = _Page.Request.Url.AbsoluteUri.ToString();
            if (checkSQL(_URL.ToLower()))
            {
                _Page.Response.Redirect("~/");
            }
        }

        /// <summary>
        /// 检查字符串是否含有非法字符,true表示有非法字符
        /// </summary>
        /// <param name="_String"></param>
        /// <returns></returns>
        public static bool checkSQL(string _String)
        {
            bool HaveSQL = false;

            string SQL = Z_String.SQL_String3;

            string[] SQLArr = SQL.Split(new char[] { '|' });

            for (int I = 0; I < SQLArr.Length; I++)
            {
                if (_String.IndexOf(SQLArr[I].ToString()) > -1)
                {
                    HaveSQL = true;
                    break;
                }
            }
            return HaveSQL;
        }
    }
}
