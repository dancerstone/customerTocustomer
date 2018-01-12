using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 处理字符串类
    /// </summary>
    public static class DealString
    {
        /// <summary>
        /// 取得此字符串的后缀名(针对字符串为文件名的情况,不包括.)
        /// </summary>
        /// <param name="_String">字符串对象</param>
        /// <returns>截取后的子字符串</returns>
        public static string getExtension(string _String)
        {
            int S = _String.LastIndexOf(".");
            string subString = _String.Substring(S + 1);
            return subString;
        }

        /// <summary>
        /// 从字符串开始处截取指定长度的子字符串(一个汉字算两个字符串)
        /// 如果截取指定的长度大于字符串本身,则返回字符串本身
        /// 否则返回截取的字符串加...
        /// </summary>
        /// <param name="_String">字符串对象</param>
        /// <param name="length">截取的长度(为偶数)</param>
        /// <returns>截取后的子字符串</returns>
        public static string getSubString(string _String, int length)
        {
            return getSubString(_String, length, "...");
        }
        /// <summary>
        /// 从字符串开始处截取指定长度的子字符串(一个汉字算两个字符串)
        /// 如果截取指定的长度大于字符串本身,则返回字符串本身
        /// 否则返回截取的字符串加...
        /// </summary>
        /// <param name="_String">字符串对象</param>
        /// <param name="length">截取的长度(为偶数)</param>
        /// <param name="Sig">省略符号</param>
        /// <returns>截取后的子字符串</returns>
        public static string getSubString(string _String, int length, string Sig)
        {
            Regex reg = new Regex(@"[^\x00-\xff]");
            string newString = ZhuanHuanStringASIIC(_String);
            if (newString.Length > length)
            {
                StringBuilder output = new StringBuilder();
                int u = 0;
                for (int i = 0; i < _String.Length; i++)
                {
                    string _subString = _String.Substring(i, 1);
                    if (reg.IsMatch(_subString))
                    {
                        u += 2;
                    }
                    else
                    {
                        u++;
                    }
                    output.Append(_subString);
                    if (u >= length)
                    {
                        break;
                    }
                }
                return output.ToString() + Sig;
            }
            else
            {
                return _String;
            }
        }

        /// <summary>
        /// 对字符串进行md5加密
        /// </summary>
        /// <param name="_String">字符串对象</param>
        /// <returns>加密有的字符串</returns>
        public static string MD5(string _String)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(_String, "md5");
        }

        /// <summary>
        /// 将双字节字符转换成ww
        /// </summary>
        /// <param name="_string">待转换的字符串</param>
        /// <returns>返回转换后的字符串</returns>
        public static string ZhuanHuanStringASIIC(string _string)
        {
            Regex reg = new Regex(@"[^\x00-\xff]");
            return reg.Replace(_string, "ww");
        }

        /// <summary>
        /// 把带有注入性的字符串转换成普通字符
        /// </summary>
        /// <param name="_string">待检查的字符串</param>
        /// <returns></returns>
        public static string ChangeSQL(string _string)
        {
            return ChangeSQL(_string, true);
        }

        /// <summary>
        /// 把带有注入性的字符串转换成普通字符(第三方编辑区一般不能转换为false)
        /// </summary>
        /// <param name="_string">待检查的字符串</param>
        /// <param name="_tihuanB">是否要替换&lt;和&gt;(第三方编辑区一般不能转换为false)</param>
        /// <returns>返回转换后的字符串</returns>
        public static string ChangeSQL(string _string, bool _tihuanB)
        {
            //防注入
            string[] _yangben = Z_String.SQL_String.Split(new char[] { '|' });

            for (int i = 0; i < _yangben.Length; i++)
            {
                Regex _Regex = new Regex(_yangben[i].ToString(), RegexOptions.IgnoreCase);
                _string = _Regex.Replace(_string, Z_String.MSQL + "$0" + Z_String.MSQL);
            }

            string[] _yangben1 = Z_String.SQL_String2.Split(new char[] { '|' });
            string[] _yangben12 = Z_String.SQL_StringF2.Split(new char[] { '|' });

            for (int i = 0; i < _yangben1.Length; i++)
            {
                _string = _string.Replace(_yangben1[i].ToString(), _yangben12[i].ToString());
            }

            if (_tihuanB)
            {
                //替换
                string[] _tihuan = Z_String.Tihuan_String.Split(new char[] { '|' });

                string[] _tihuan2 = Z_String.Tihuan_StringF.Split(new char[] { '|' });

                for (int u = 0; u < _tihuan.Length; u++)
                {
                    _string = _string.Replace(_tihuan[u].ToString(), _tihuan2[u].ToString());
                }
            }

            return _string;
        }

        /// <summary>
        /// 把普通字符转换成带有注入性的字符串
        /// </summary>
        /// <param name="_string">待检查的字符串</param>
        /// <returns>返回转换后的字符串</returns>
        public static string ChangeSQLF(string _string)
        {
            string[] _yangben = Z_String.SQL_String2.Split(new char[] { '|' });

            string[] _yangben2 = Z_String.SQL_StringF2.Split(new char[] { '|' });

            for (int i = 0; i < _yangben2.Length; i++)
            {
                _string = _string.Replace(_yangben2[i].ToString(), _yangben[i].ToString());
            }

            _string = _string.Replace(Z_String.MSQL, "");

            return _string;
        }

        /// <summary>
        /// 分割字符串，保存为数组
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <param name="strSplit">分隔字符或字符串</param>
        /// <returns>结果数组</returns>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, @strSplit.Replace(".", @"\."), RegexOptions.IgnoreCase);
        }

        #region 字符串转 与 16进制 互转

        /// <summary>
        /// 把字符串转换成16进制
        /// </summary>
        /// <param name="mStr"></param>
        /// <returns></returns>
        public static string StrToHex(string mStr)
        {
            return BitConverter.ToString(System.Text.ASCIIEncoding.Default.GetBytes(mStr)).Replace("-", " ");

        }
        /// <summary>
        /// 把16进制转换成字符串
        /// </summary>
        /// <param name="mHex"></param>
        /// <returns></returns>
        public static string HexToStr(string mHex)
        {
            mHex = mHex.Replace(" ", "");
            if (mHex.Length <= 0) { return ""; }
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
            {
                if (!byte.TryParse(mHex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, null, out vBytes[i / 2]))
                {
                    vBytes[i / 2] = 0;
                }
            }
            return System.Text.ASCIIEncoding.Default.GetString(vBytes);
        }

        #endregion

        /// <summary>
        /// 取出字符串里面的HTML标签
        /// </summary>
        /// <param name="strHtml">字符串</param>
        /// <returns></returns>
        public static string replaceHTML(string strHtml)
        {
            string strhtml = System.Text.RegularExpressions.Regex.Replace(strHtml, "<[^>]+?>|\"|'|\\|/", "");
            strhtml = System.Text.RegularExpressions.Regex.Replace(strhtml, "<br>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return strhtml.Replace("&nbsp;", "");
        }

        /// <summary>
        /// 去掉字符串里面的换行符
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <returns></returns>
        public static string clearString_R_N(string Str)
        {
            string tempStr = Str.Replace((char)13, (char)0);
            return tempStr.Replace((char)10, (char)0);
        }
    }
}
