using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    class Z_String
    {
        #region 防注入字符串

        /// <summary>
        /// 替换字符串
        /// </summary>
        public static string Tihuan_String = "<|>";

        /// <summary>
        /// 替换字符串F
        /// </summary>
        public static string Tihuan_StringF = "&lt;|&gt;";

        /// <summary>
        /// 防注入字符串-单词
        /// </summary>
        public static string SQL_String = "insert|delete|select|update|like|where|drop|and|exec|count|chr|mid|master|truncate|declare|case|varchar|nvarchar|char|nchar|ntext|text|int|fetch|deallocate|convert|trim|set|0x";

        /// <summary>
        /// 
        /// </summary>
        public static string MSQL = "@_N@O_@";

        /// <summary>
        /// 防注入字符串-符号
        /// </summary>
        public static string SQL_String2 = "*|%|\"|'|,|;|/|\\";

        /// <summary>
        /// 防注入字符串-替换符号
        /// </summary>
        public static string SQL_StringF2 = "$_10_$|$_20_$|$_SY_$|$_DY_$|$_DH_$|$_MH_$|$_ZX_$|$_FX_$";


        /// <summary>
        /// ***网页***地址防止SQL注入
        /// </summary>
        public static string SQL_String3 = SQL_String + "|" + Tihuan_String + "|" + "'";

        #endregion

    }
}
