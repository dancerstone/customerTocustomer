using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealMvc.Common.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DealMvc
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class StaticFunction
    {
        #region 配置方法
        /// <summary>
        /// 比较并判断是否在变量内，如果不在则把默认值给变量
        /// </summary>
        /// <param name="Intobj"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="ErrDefault"></param>
        /// <returns></returns>
        public static int? JIntComparisonByDefault(this int? Intobj, int? min, int? max, int? ErrDefault)
        {
            int i_val = Intobj.ToInt32();
            int i_min = min.ToInt32();
            int i_max = max.ToInt32();
            if (i_val < i_min || i_val > i_max)
                i_val = ErrDefault.ToInt32();
            return i_val;
        }
        /// <summary>
        /// 比较并判断是否在变量内，如果不在则把默认值给变量
        /// </summary>
        /// <param name="doubleobj"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="ErrDefault"></param>
        /// <returns></returns>
        public static double? JDoubleComparisonByDefault(this double? doubleobj, double? min, double? max, double? ErrDefault)
        {
            double? d_val = doubleobj.ToDouble2();
            if (d_val < min || d_val > max)
                d_val = ErrDefault;
            return d_val;
        }
        /// <summary>
        /// 比较并判断是否在变量内，如果不在则把默认值给变量
        /// </summary>
        /// <param name="doubleobj"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="ErrDefault"></param>
        /// <returns></returns>
        public static double JDoubleComparisonByDefault(this double doubleobj, double min, double max, double ErrDefault)
        {
            double d_val = doubleobj.ToDouble2();
            if (d_val < min || d_val > max)
                d_val = ErrDefault;
            return d_val;
        }

        /// <summary>
        /// 数据库字段
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string JIsNullOrEmptyText(this object obj, string showtext)
        {
            return string.IsNullOrEmpty(obj.ToString2()) ? showtext : obj.ToString2().JHtmlEncode();
        }
        /// <summary>
        /// 数据库字段
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string JshowWebText(this object obj, string err)
        {
            return string.IsNullOrEmpty(obj.ToString2()) ? (string.IsNullOrEmpty(err) ? "[暂无]" : err) : obj.ToString2().JHtmlEncode();
        }
        /// <summary>
        /// 数据库字段
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string JshowWebText(this object obj)
        {
            return string.IsNullOrEmpty(obj.ToString2()) ? "[暂无]" : obj.ToString2().JHtmlEncode();
        }

        /// <summary>
        /// img TrueAndFalse 数据库字段是：Bit 页面显示
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ToBoolIco(this object obj)
        {
            return string.Format("<img src=\"/App_Themes/Cms/Pic/{0}.gif\" />", (obj ?? false).ToString2().ToLower());
        }



        /// <summary>
        /// img TrueAndFalse 数据库字段是：Bit 页面显示
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ToBoolIco2(this object obj)
        {
            if (obj.Equals("true") || obj.Equals("True"))
            {
                return string.Format("<img src=\"/App_Themes/Cms/Pic/true.gif\" />");
            }
            else
            {
                return string.Format("<img src=\"/App_Themes/Cms/Pic/false.gif\" />");
            }



            //return string.Format("<img src=\"/App_Themes/Cms/Pic/{0}.gif\" />", (obj ?? false).ToString2().ToLower());
        }

        /// <summary>
        /// 显示swf文件
        /// </summary>
        /// <param name="swfurl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string JSwfHtml(this object swfurl, int? width, int? height)
        {
            return JSwfHtml(swfurl, width, height, "", "");
        }
        /// <summary>
        /// 显示swf文件
        /// </summary>
        /// <param name="swfurl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="swfid"></param>
        /// <returns></returns>
        public static string JSwfHtml(this object swfurl, int? width, int? height, string swfobjectnameid)
        {
            return JSwfHtml(swfurl, width, height, swfobjectnameid, "");
        }
        /// <summary>
        /// 显示swf文件
        /// </summary>
        /// <param name="swfurl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="swfid"></param>
        /// <returns></returns>
        public static string JSwfHtml(this object swfurl, int? width, int? height, string swfobjectnameid, string errmsg)
        {
            string AllFolderPath = string.Empty;
            if (swfurl.ToString2().ToLower().IndexOf("http://") == 0)
            {
                AllFolderPath = swfurl.ToString2Trim();
            }
            else
            {
                AllFolderPath = System.Web.HttpContext.Current.Server.MapPath("~" + swfurl);
                if (!System.IO.File.Exists(AllFolderPath))
                    return errmsg.ToString2();
            }

            string str_width = (width ?? 120).ToString2();
            string str_height = (height ?? 120).ToString2();

            return string.Format(
                "<embed src=\"{0}\" width=\"{1}\" height=\"{2}\" allowFullScreen=\"true\" quality=\"high\" align=\"middle\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" style=\"visibility: visible; text-align: center;\"></embed>",
            new object[]{
                swfurl.ToString2(), 
                str_width,
                str_height
            });
            //<embed src="http://player.youku.com/player.php/sid/XNDI1NjczNDI4/v.swf" allowFullScreen="true" quality="high" align="middle" allowScriptAccess="always" type="application/x-shockwave-flash"></embed>
        }
        /// <summary>
        /// 显示flv文件
        /// </summary>
        /// <param name="swfurl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string JFlvHtml(this object swfurl, int? width, int? height)
        {
            return JFlvHtml(swfurl, width, height, "", "");
        }
        /// <summary>
        /// 显示flv文件
        /// </summary>
        /// <param name="swfurl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="swfid"></param>
        /// <returns></returns>
        public static string JFlvHtml(this object swfurl, int? width, int? height, string swfobjectnameid)
        {
            return JFlvHtml(swfurl, width, height, swfobjectnameid, "");
        }
        /// <summary>
        /// 显示flv文件
        /// </summary>
        /// <param name="swfurl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="swfid"></param>
        /// <returns></returns>
        public static string JFlvHtml(this object swfurl, int? width, int? height, string swfobjectnameid, string errmsg)
        {

            string AllFolderPath = string.Empty;
            if (swfurl.ToString2().ToLower().IndexOf("http://") == 0)
            {
                AllFolderPath = swfurl.ToString2Trim();
            }
            else
            {
                AllFolderPath = System.Web.HttpContext.Current.Server.MapPath("~" + swfurl);
                if (!System.IO.File.Exists(AllFolderPath))
                    return errmsg.ToString2();
            }

            string str_width = (width ?? 120).ToString2();
            string str_height = (height ?? 120).ToString2();

            return string.Format("<embed  width=\"{1}\" height=\"{2}\" flashvars=\"file={0}\" wmode=\"Opaque\" allowfullscreen=\"true\" allowscriptaccess=\"always\" bgcolor=\"#000000\" quality=\"high\" src=\"/Style/Public/flv/player.swf\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" id=\"FlvDialog{3}\">", swfurl.ToString2(), width ?? 0, height ?? 0, swfobjectnameid.ToString2Trim());

        }
        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="imgstr"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static string JImgHtml(this object imgstr, int? w, int? h)
        {
            return JImgHtml(imgstr, "[暂无图片]", w, h);
        }
        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="imgstr"></param>
        /// <param name="errmsg"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static string JImgHtml(this object imgstr, string errmsg, int? w, int? h)
        {
            if (string.IsNullOrEmpty(imgstr.ToString2()))
                return errmsg.ToString2();

            string w_str = string.Empty;
            string h_str = string.Empty;
            try
            {
                System.Drawing.Image Img;

                if (imgstr.ToString2().ToLower().IndexOf("http://".ToLower()) == 0)
                {
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(imgstr.ToString2());
                    System.IO.Stream inStream = request.GetResponse().GetResponseStream();
                    Img = System.Drawing.Image.FromStream(inStream);
                }
                else
                {
                    string AllFolderPath = System.Web.HttpContext.Current.Server.MapPath("~" + imgstr);
                    if (!System.IO.File.Exists(AllFolderPath))
                        return errmsg.ToString2();
                    Img = System.Drawing.Image.FromFile(AllFolderPath);
                }

                if (Img != null)
                {
                    if (w.ToInt32() > 0)
                    {
                        w_str = string.Format("width:{0}px;", w > Img.Width ? Img.Width : w);
                    }
                    if (h.ToInt32() > 0)
                    {
                        h_str = string.Format("height:{0}px;", h > Img.Height ? Img.Height : h);
                    }
                }
            }
            catch
            {
                return errmsg.ToString2();
            }

            return string.Format("<img src=\"{0}\" alt=\" \"  style=\"{1}{2}\"/>", new object[]{ 
                imgstr, 
               w_str,
                h_str
            });
        }

        //private int GetUrlError(string curl)
        //{
        //    int num = 200;

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(curl));
        //    ServicePointManager.Expect100Continue = false;
        //    try
        //    {
        //        ((HttpWebResponse)request.GetResponse()).Close();
        //    }
        //    catch (WebException exception)
        //    {
        //        if (exception.Status != WebExceptionStatus.ProtocolError)
        //        {
        //            return num;
        //        }
        //        if (exception.Message.IndexOf("500 ") > 0)
        //        {
        //            return 500;
        //        }
        //        if (exception.Message.IndexOf("401 ") > 0)
        //        {
        //            return 401;
        //        }
        //        if (exception.Message.IndexOf("404") > 0)
        //        {
        //            num = 404;
        //        }
        //    }
        //    return num;
        //}

        #endregion
        #region 扩展方法
        #region 补充
        /// <summary>
        /// 枚举还原
        /// time:10:41 2011-6-6
        /// </summary>
        /// <param name="Str">源字符串</param>
        /// <returns></returns>
        public static object EnumReplace(this object Str)
        {
            return Str.ToString().Replace("0_0", "/").Replace("1_1", "、").Replace("2_2", "（").Replace("3_3", "）").Replace("4_4", "，").Replace("5_5", "-").Replace("k_k", "").Replace("c_c", "×").Replace("w_w", "?").Replace("Multiply_Symbol", "*").Replace("Minus_Symbol", "-").Replace("Plus_Symbol", "+").Replace("Except_Symbol", "/");

        }
        /// <summary>
        /// 判断参数是否为登录名
        /// </summary>
        /// <param name="param"></param>
        public static bool isLoginName(this object param)
        {
            Regex regex = new Regex("(^[a-zA-Z]{1}([a-zA-Z0-9]|[_])+$)");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;

            if (regex.IsMatch(param.ToString2()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断参数是否为邮箱
        /// </summary>
        /// <param name="param"></param>
        public static bool isEmail(this object param)
        {
            Regex regex = new Regex("(^[_.0-9A-Za-z-]+@[0-9A-Za-z-]+.(com|cc|cn|tv|hk|name|mobi|net|biz|org|info|gov.cn|com.cn|net.cn|org.cn)$)");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;

            if (regex.IsMatch(param.ToString2()) && param.ToString2().Length <= 32)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断参数是否为QQ号码
        /// </summary>
        /// <param name="param">是true </param>
        public static bool isQQ(this object param)
        {
            Regex regex = new Regex("(^[^0]d{4,11}$)");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;

            if (regex.IsMatch(param.ToString2()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断参数是否为手机号码
        /// </summary>
        /// <param name="param">是true </param>
        public static bool isMobileNumber(this object param)
        {
            Regex regex = new Regex("(^1[3|5|8][0-9]{9}$)");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;

            if (regex.IsMatch(param.ToString2()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断是否为日期格式
        /// </summary>
        /// <param name="param"></param>
        public static bool isDate(this object param)
        {
            Regex regex = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;
            if (regex.IsMatch(param.ToString2()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// DateTime?格式化,如何DateTime?为null返回Empty
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStrDateTime(this DateTime? dt, Estatic.ForDateTime _ForDateTime)
        {
            string val = string.Empty;
            string format = string.Empty;
            switch (_ForDateTime)
            {
                case DealMvc.Estatic.ForDateTime.类型一:
                    format = "yyyy-MM-dd HH:mm";
                    break;
                case DealMvc.Estatic.ForDateTime.类型二:
                    format = "yyyy-MM-dd";
                    break;
                case DealMvc.Estatic.ForDateTime.类型三:
                    format = "yyyy年MM月dd日 HH时mm分";
                    break;
                case DealMvc.Estatic.ForDateTime.类型四:
                    format = "yyyy年MM月dd日";
                    break;
                case DealMvc.Estatic.ForDateTime.类型五:
                    format = "yy-MM-dd HH:mm";
                    break;
                case DealMvc.Estatic.ForDateTime.类型六:
                    format = "MM-dd HH:mm";
                    break;
                case DealMvc.Estatic.ForDateTime.类型七:
                    format = "MM-dd";
                    break;
                case DealMvc.Estatic.ForDateTime.类型八:
                    format = "yyyy-MM-dd HH:mm:ss";
                    break;
                case DealMvc.Estatic.ForDateTime.类型九:
                    format = "MM月dd日";
                    break;
                case DealMvc.Estatic.ForDateTime.类型十:
                    format = "yyyy.MM.dd    HH:mm";
                    break;
            }

            try { val = ((DateTime)dt).ToString(format, System.Globalization.DateTimeFormatInfo.InvariantInfo); }
            catch { }
            return val;
        }


        /// <summary>
        /// XLY 美学杂志 日期显示
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDateTimeXLY(this DateTime? dt)
        {
            DateTime dt_time = dt ?? DateTime.Now;
            DateTime m_new_time = Convert.ToDateTime(dt_time); //
            string val = string.Empty;
            switch (m_new_time.Month)
            {
                case 1:
                    val = "一月";
                    break;
                case 2:
                    val = "二月";
                    break;
                case 3:
                    val = "三月";
                    break;
                case 4:
                    val = "四月";
                    break;
                case 5:
                    val = "五月";
                    break;
                case 6:
                    val = "六月";
                    break;
                case 7:
                    val = "七月";
                    break;
                case 8:
                    val = "八月";
                    break;
                case 9:
                    val = "九月";
                    break;
                case 10:
                    val = "十月";
                    break;
                case 11:
                    val = "十一月";
                    break;
                case 12:
                    val = "十二月";
                    break;
                default:
                    break;
            }
            return m_new_time.Day+" "+val+" "+m_new_time.Year;
        }
        #endregion

        #region 常用方法
        /// <summary>
        /// object转String,如何object为null返回Empty
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ToString2Trim(this object obj)
        {
            //string Val = string.Empty;
            //try { Val = obj.ToString(); }
            //catch { Val = ""; }
            return obj.ToString2().Trim();
        }

        /// <summary>
        /// object转String,如何object为null返回Empty
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ToString2(this object obj)
        {
            //string Val = string.Empty;
            //try { Val = obj.ToString(); }
            //catch { Val = ""; }
            return obj == null ? string.Empty : obj.ToString();
        }
        /// <summary>
        /// object转Int,失败返回0
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static int ToInt32(this object obj)
        {
            int Val = 0;
            try { Val = Convert.ToInt32(obj.ToString2().Split('.')[0]); }
            catch { Val = 0; }
            return Val;
        }

        /// <summary>
        /// object转Boolean,失败返回flase
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool ToBoolean2(this object obj)
        {
            bool Val = false;
            try { Val = Convert.ToBoolean(obj); }
            catch { Val = false; }
            return false;
        }
        /// <summary>
        /// object转Double,失败返回0
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static double ToDouble2(this object obj)
        {
            double Val = 0;
            try { Val = Convert.ToDouble(obj); }
            catch { Val = 0; }
            return Val;
        }

        /// <summary>
        /// object转价格0.00
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ToJiaGe(this object obj)
        {

            double Val = 0;
            try { Val = Convert.ToDouble(obj); }
            catch { Val = 0; }
            string jiage = Val.ToString("0.00");
            return jiage;
        }


        /// <summary>
        /// DateTime?格式化,如何DateTime?为null返回Empty
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringForDateTime(this DateTime? dt, string format)
        {
            string val = string.Empty;
            try { val = ((DateTime)dt).ToString(format, System.Globalization.DateTimeFormatInfo.InvariantInfo); }
            catch { }
            return val;
        }
        /// <summary>
        /// 判断URL参数是否为正整数
        /// </summary>
        /// <param name="param"></param>
        public static bool isInt(this object param)
        {
            Regex regex = new Regex("(^0$)|(^-?[1-9][0-9]*$)");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;
            if (regex.IsMatch(param.ToString2()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断参数是否为Double（比如价格）
        /// </summary>
        /// <param name="param">参数</param>
        public static bool isDouble(this object param)
        {
            Regex regex3 = new Regex(@"(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)|(^0$)");
            if (string.IsNullOrEmpty(param.ToString2()))
                return false;
            if (regex3.IsMatch(param.ToString2()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="Str">源字符串</param>
        /// <param name="length">保留长度后面加...</param>
        /// <returns></returns>
        public static string JSubString(this object Str, int length)
        {
            return Str.JSubString(length, "...");
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="Str">源字符串</param>
        /// <param name="length">保留长度</param>
        /// <param name="Sig">后面的符号 例如...</param>
        /// <returns></returns>
        public static string JSubString(this object Str, int length, string Sig)
        {
            return DealString.getSubString(Str.ToString2(), length, Sig).JHtmlEncode();
        }
        /// <summary>
        /// 去除字符串里面的HTML标签
        /// </summary>
        /// <param name="strHtml">字符串</param>
        /// <returns></returns>
        public static string JreplaceHTML(this object obj)
        {
            string strHtml = obj.ToString2();
            return DealString.replaceHTML(strHtml);
        }
        /// <summary>
        /// 去掉字符串里面的换行符
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <returns></returns>
        public static string JclearString_R_N(this object Str)
        {
            return DealString.clearString_R_N(Str.ToString2());
        }
        /// <summary>
        /// 把字符串转换成16进制
        /// </summary>
        /// <param name="mStr"></param>
        /// <returns></returns>
        public static string JStrToHex(this string mStr)
        {
            return DealString.StrToHex(mStr);

        }
        /// <summary>
        /// 把16进制转换成字符串
        /// </summary>
        /// <param name="mHex"></param>
        /// <returns></returns>
        public static string JHexToStr(this string mHex)
        {
            return DealString.HexToStr(mHex);
        }
        /// <summary>
        /// 对字符串进行md5加密
        /// </summary>
        /// <param name="_String">字符串对象</param>
        /// <returns>加密有的字符串</returns>
        public static string Jmd5(this string _String)
        {
            return DealString.MD5(_String);
        }
        /// <summary>
        /// 取得此字符串的后缀名(针对字符串为文件名的情况,不包括.)
        /// </summary>
        /// <param name="_String">字符串对象</param>
        /// <returns>截取后的子字符串</returns>
        public static string JgetExtension(this string _String)
        {
            return DealString.getExtension(_String);
        }
        /// <summary>
        /// 格式化字符串(,1,2,3,4,5,,2,32,,)为(1,2,3,4,5,2,32), 如果为空则返回字符串0
        /// </summary>
        /// <param name="Str">原字符串</param>
        /// <param name="Sign">分割标识</param>
        /// <param name="IsMustInt">必须为Int</param>
        /// <returns></returns>
        public static string JFormatIdsValues(this string Str, string Sign, bool IsMustInt)
        {
            string[] s = Str.Split(new string[] { Sign }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length == 0) return "0";
            ArrayList output = new ArrayList();
            Regex reg = new Regex("^-?\\d+$");
            foreach (string _s in s)
            {
                if (!string.IsNullOrEmpty(_s) && (reg.IsMatch(_s) || !IsMustInt))
                    output.Add(_s);
            }
            return output.JArrayListToString(Sign, true);
        }
        /// <summary>
        /// 将双字节字符转换成 ww
        /// </summary>
        /// <param name="_string">待转换的字符串</param>
        /// <returns>返回转换后的字符串</returns>
        public static string JDoubleByteToTwoByte(this string Str)
        {
            Regex reg = new Regex(@"[^\x00-\xff]");
            return reg.Replace(Str, "ww");
        }
        /// <summary>
        ///  解码字符串
        /// </summary>
        /// <param name="textToFormat"></param>
        /// <returns></returns>
        public static string JHtmlDecode(this string textToFormat)
        {
            if (string.IsNullOrEmpty(textToFormat))
            {
                return textToFormat;
            }
            return HttpUtility.HtmlDecode(textToFormat);
        }
        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="textToFormat"></param>
        /// <returns></returns>
        public static string JHtmlEncode(this object textToFormat)
        {
            string _textToFormat = textToFormat.ToString2();
            if (string.IsNullOrEmpty(_textToFormat))
            {
                return _textToFormat;
            }
            return HttpUtility.HtmlEncode(_textToFormat);
        }
        /// <summary>
        /// 是安全的字符串，在<%:%>里面不转义
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MvcHtmlString IsHtmlSafe(this string content)
        {
            return MvcHtmlString.Create(content);
        }
        /// <summary>
        /// 编码 Microsoft.JScript.GlobalObject.escape
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Jescape(this string str)
        {
            return Microsoft.JScript.GlobalObject.escape(str);
        }
        /// <summary>
        /// 将中文利用gb2312的编码方式，进行编码。该方法主要用于在百度中，无法识别utf-8的url编码问题。
        /// </summary>
        public static string JGetUrlEncode_Gb2312(this string oldName)
        {
            try
            {
                Byte[] encodedBytes = new byte[oldName.Length * 2];
                int iCount = Encoding.GetEncoding("gb2312").GetBytes(oldName, 0, oldName.Length, encodedBytes, 0);
                string GetGB22312String = "";
                foreach (Byte b in encodedBytes)
                {
                    string strValue = Convert.ToString(b, 16);
                    strValue = strValue.ToUpper();
                    GetGB22312String += "%" + strValue;
                }
                return GetGB22312String;
            }
            catch (Exception ex) { return oldName; }
        }
        #endregion

        #region ArrayList Int[] String[] String

        /// <summary>
        /// ArrayList转字符串,每项以,分隔
        /// </summary>
        /// <param name="_ArrayList">源ArrayList</param>
        /// <returns></returns>
        public static string JArrayListToString(this ArrayList _ArrayList)
        {
            return JArrayListToString(_ArrayList, ",", false);
        }
        /// <summary>
        /// ArrayList转字符串,每项以,分隔
        /// </summary>
        /// <param name="_ArrayList">源ArrayList</param>
        /// <param name="RemoveEmptyEntries">是否去掉 空 值</param>
        /// <returns></returns>
        public static string JArrayListToString(this ArrayList _ArrayList, bool RemoveEmptyEntries)
        {
            return JArrayListToString(_ArrayList, ",", RemoveEmptyEntries);
        }
        /// <summary>
        ///  ArrayList转字符串,每项以Sig分隔
        /// </summary>
        /// <param name="_ArrayList">源ArrayList</param>
        /// <param name="Sig">分隔符</param>
        /// <returns></returns>
        public static string JArrayListToString(this ArrayList _ArrayList, string Sig, bool RemoveEmptyEntries)
        {
            for (int i = 0; i < _ArrayList.Count; i++)
                _ArrayList[i] = _ArrayList[i].ToString2();

            string[] _s = (string[])_ArrayList.ToArray(typeof(string));
            if (RemoveEmptyEntries)
            {
                string Mstr = string.Join(Sig, _s);
                _s = Mstr.Split(new string[] { Sig }, StringSplitOptions.RemoveEmptyEntries);
            }
            return string.Join(Sig, _s);
        }
        /// <summary>
        /// Double?[] 转 Double[]
        /// </summary>
        /// <param name="_Arr"></param>
        /// <returns></returns>
        public static double[] JDoublesNotHasNull(this double?[] _Arr)
        {
            ArrayList List = new ArrayList();
            foreach (double? s in _Arr)
            {
                if (s != null)
                    List.Add(s);
            }
            return (double[])List.ToArray(typeof(double));
        }
        /// <summary>
        /// Int?[] 转 Int[]
        /// </summary>
        /// <param name="_Arr"></param>
        /// <returns></returns>
        public static int[] JIntsNotHasNull(this int?[] _Arr)
        {
            ArrayList List = new ArrayList();
            foreach (int? s in _Arr)
            {
                if (s != null)
                    List.Add(s);
            }
            return (int[])List.ToArray(typeof(int));
        }
        /// <summary>
        /// string[] 转 ArrayList
        /// </summary>
        /// <param name="_Arr"></param>
        /// <returns></returns>
        public static ArrayList JStringsToArrayList(this string[] _Arr)
        {
            ArrayList List = new ArrayList();
            foreach (string s in _Arr)
            {
                List.Add(s);
            }
            return List;
        }
        /// <summary>
        /// int?[] 转 ArrayList
        /// </summary>
        /// <param name="_Arr"></param>
        /// <returns></returns>
        public static ArrayList JIntsToArrayList(this int?[] _Arr)
        {
            ArrayList List = new ArrayList();
            foreach (int? s in _Arr)
            {
                List.Add(s);
            }
            return List;
        }
        /// <summary>
        /// string[] 转 int?[]
        /// </summary>
        /// <param name="_Arr"></param>
        /// <returns></returns>
        public static int[] JStringsToInts(this string[] _Arr)
        {
            ArrayList List = new ArrayList();
            foreach (string s in _Arr)
            {
                if (s.isInt())
                    List.Add(s.ToInt32());
            }
            return (int[])List.ToArray(typeof(int));
        }
        /// <summary>
        /// int?[] 转 string[]
        /// </summary>
        /// <param name="_Arr"></param>
        /// <param name="RemoveEmptyEntries"></param>
        /// <returns></returns>
        public static string[] JIntsToStrings(this int?[] _Arr, bool RemoveEmptyEntries)
        {
            ArrayList List = new ArrayList();
            foreach (int? s in _Arr)
            {
                if (s == null && !RemoveEmptyEntries)
                    List.Add(string.Empty);
                else
                    List.Add(s.ToString2());
            }
            return (string[])List.ToArray(typeof(string));
        }

        #endregion

        #region 四舍五入

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="Num">源double</param>
        /// <param name="Wei">保留位数,位数不够会加0</param>
        /// <returns></returns>
        public static double JRound(this double Num, int Wei)
        {
            return Num._JRound(Wei, "=").ToDouble2();
        }

        /// <summary>
        /// 强入 +1
        /// </summary>
        /// <param name="Num">源double</param>
        /// <param name="Wei">保留位数,位数不够会加0</param>
        /// <returns></returns>
        public static double JRound1(this double Num, int Wei)
        {
            return Num._JRound(Wei, "+").ToDouble2();
        }

        /// <summary>
        /// 强舍 -1
        /// </summary>
        /// <param name="Num">源double</param>
        /// <param name="Wei">保留位数,位数不够会加0</param>
        /// <returns></returns>
        public static double JRound2(this double Num, int Wei)
        {
            return Num._JRound(Wei, "-").ToDouble2();
        }

        /// <summary>
        /// 四舍五入,强入,强舍
        /// </summary>
        /// <param name="Num">源double</param>
        /// <param name="Wei">保留位数,位数不够会加0</param>
        /// <param name="Sign">取舍标识( =:四舍五入, -:强舍, +:强入)</param>
        /// <returns>返回字符串</returns>
        private static string _JRound(this double Num, int Wei, string Sign)
        {
            /*
            string s = Num.ToString();
            string[] ss = s.Split('.');
            if (ss.Length < 2)
            {
                string bu = "";
                for (int i = 0; i < Wei; i++) bu += "0";
                return !string.IsNullOrEmpty(bu) ? Num.ToString() + "." + bu : Num.ToString();
            }
            else
            {
                if (ss[1].Length > Wei)
                {
                    string x = ss[1].Substring(Wei, 1);
                    bool Add = false;
                    if (Sign == "+" && int.Parse(x) > 0) Add = true;
                    else if (Sign == "-") Add = false;
                    else if (int.Parse(x) >= 5 && Sign == "=") Add = true;
                    if (Wei == 0)
                        return Add ? (int.Parse(ss[0]) + 1).ToString() : ss[0];
                    else
                        return ss[0] + "." + (Add ? int.Parse(ss[1].Substring(0, Wei)) + 1 : int.Parse(ss[1].Substring(0, Wei))).ToString();
                }
                else
                {
                    string bu = "";
                    for (int i = 0; i < Wei - ss[1].Length; i++)
                        bu += "0";
                    return Num.ToString() + bu;
                }
            }
            */
            double f = Num;
            Wei = Math.Abs(Wei);
            try
            {
                if (Sign == "-")
                {//强舍
                    double _Wei = Wei == 0 ? 0.1 : Wei;
                    int i = (int)(f * 10 * _Wei);
                    f = (double)(i * 1.0) / 10 * _Wei;
                }
                else if (Sign == "+")
                {//强入
                    double _Wei = Wei == 0 ? 0.1 : Wei;
                    int i = (int)(f * 10 * _Wei);
                    if (f - i > 0.1) i++;
                    f = (double)(i * 1.0) / 10 * _Wei;
                }
                else
                {//四舍五入
                    //fN 保留N位，四舍五入
                    f = f.ToString("F" + Wei.ToString()).ToDouble2();
                }
            }
            catch { return "0"; }
            return f.ToString2();
        }

        #endregion

        #region 枚举的静态方法

        /// <summary>
        /// 获取枚举的options字符串
        /// </summary>
        /// <param name="_Type">枚举类型</param>
        /// <param name="i">0:option 1:radio 2:checkbox</param>
        /// <param name="name">名称 只对 i==1,2 时有效</param>
        /// <returns></returns>
        public static string JGetEnumOptions(this Type _Type, int i, string name)
        {
            Type t = _Type;
            StringBuilder Output = new StringBuilder();
            foreach (string s in Enum.GetNames(t))
            {
                string val = s.JEnumRelaceTitle();
                if (i == 0)
                    Output.Append(string.Format("<option value='{1}'>{1}</option>", val, val));
                else if (i == 1)
                    Output.Append(string.Format("<input type='radio' name='{0}' value='{1}' />{2}&nbsp;", name, val, val));
                else if (i == 2)
                    Output.Append(string.Format("<input type='checkbox' name='{0}' value='{1}' />{2}&nbsp;", name, val, val));
            }
            return Output.ToString();
        }

        /// <summary>
        /// 获取枚举的遍历
        /// </summary>
        /// <param name="_Type">枚举类型</param>
        /// <param name="FormatString">格式化字符串</param>
        /// <returns></returns>
        public static string JGetEnumEachString(this Type _Type, string FormatString)
        {
            Type t = _Type;
            StringBuilder Output = new StringBuilder();
            foreach (string s in Enum.GetNames(t))
            {
                string val = s.JEnumRelaceTitle();
                Output.Append(string.Format(FormatString, ((int)Enum.Parse(t, s)).ToString(), val));
            }
            return Output.ToString();
        }

        /// <summary>
        /// 根据值获取枚举的另一个值
        /// </summary>
        /// <param name="_Type">枚举类型</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static int JGetEnumIntByString(this Type _Type, string value)
        {
            Type t = _Type;
            int i = 0;
            foreach (string s in Enum.GetNames(t))
            {
                if (value == Enum.Parse(t, s).ToString()) { i = (int)Enum.Parse(t, s); break; }
            }
            return i;
        }

        /// <summary>
        /// 根据值获取枚举的另一个值
        /// </summary>
        /// <param name="_Type">枚举类型</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string JGetEnumStringByInt(this Type _Type, int value)
        {
            Type t = _Type;
            string v = "";
            foreach (string s in Enum.GetNames(t))
            {
                if (value == (int)Enum.Parse(t, s)) { v = Enum.Parse(t, s).ToString(); break; }
            }
            return v.JEnumRelaceTitle();
        }

        /// <summary>
        /// 替换枚举固定的开头标识符
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public static string JEnumRelaceTitle(this string Title)
        {
            if (Title.IndexOf("M_") == 0)
                return Title.Substring(3);
            return Title;
        }

        #endregion

        #region HTML IMAGE

        /// <summary>
        /// 获取html中的img, 主要用于在ckEditor中获取img
        /// </summary>
        /// <param name="Html">HTML代码</param>
        /// <returns></returns>
        public static string[] JGetImages(this string Html)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(Html);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }

        #endregion

        #region 处理图片

        /// <summary>
        /// 随机第一张图片
        /// </summary>
        /// <param name="Paths"></param>
        /// <returns></returns>
        public static string JGetOnePic(this string Paths)
        {
            string[] s = Paths.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length > 0) return s[0].ToString();
            return "_";
        }

        /// <summary>
        /// 如果图片未加载成功,则换成系统默认图片
        /// </summary>
        /// <param name="PicPath"></param>
        /// <returns></returns>
        public static string JEP(this string PicPath)
        {
            try
            {
                if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~" + PicPath)))
                {
                    return PicPath;
                }
            }
            catch { }
            return "/App_Themes/UI/image/no_pic.jpg";
        }

        /// <summary>
        /// 如果图片地址为空,则放回_, 目的不重复访问当前action
        /// </summary>
        /// <param name="PicPath"></param>
        /// <returns></returns>
        public static string JEP2(this object PicPath)
        {
            try
            {
                if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~" + PicPath)))
                {
                    return PicPath.ToString2();
                }
            }
            catch { }
            return "/App_Themes/UI/image/no_pic.jpg";
        }


        /// <summary>
        /// 如果图片地址为空,则放回_, 目的不重复访问当前action - 适用于手机端
        /// </summary>
        /// <param name="PicPath"></param>
        /// <returns></returns>
        public static string JEP3(this object PicPath)
        {
            try
            {
                if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~" + PicPath)))
                {
                    return PicPath.ToString2();
                }
            }
            catch { }
            return "/App_Themes/images/no_pic.jpg";
        }

        #endregion

        #region 计算 时间   几分钟前/几小时前
        /// <summary>
        /// 计算 时间   几分钟前/几小时前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateStringFromNow(this string dt_string)
        {
            try
            {
                DateTime dt = DateTime.Parse(dt_string);
                TimeSpan span = DateTime.Now - dt;
                if (span.TotalDays > 60)
                {
                    return dt.ToShortDateString();
                }
                else
                    if (span.TotalDays > 30)
                    {
                        return "1个月前";
                    }
                    else
                        if (span.TotalDays > 14)
                        {
                            return "2周前";
                        }
                        else
                        {
                            if (span.TotalDays > 7)
                            {
                                //return "1周前";
                                return string.Format("{0}月{1}日 {2}:{3}", dt.Month, dt.Day, dt.Hour, dt.Minute);
                                //return string.Format("{0}月{1}日 {2}点", dt.Month, dt.Day, dt.Hour);
                            }
                            else
                            {
                                if (span.TotalDays > 1)
                                {
                                    return string.Format("{0}月{1}日 {2}:{3}", dt.Month, dt.Day, dt.Hour, dt.Minute);
                                }
                                else
                                {
                                    if (span.TotalHours > 1)
                                    {
                                        return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                    }
                                    else
                                    {
                                        if (span.TotalMinutes > 1)
                                        {
                                            return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                        }
                                        else
                                        {
                                            if (span.TotalSeconds >= 10)
                                            {
                                                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                            }
                                            else
                                            {
                                                return "刚刚";
                                            }
                                        }
                                    }
                                }
                            }
                        }
            }
            catch
            {
                return "系统错误";
            }
        }


        public static string DateStringFromNow2(this string dt_string)
        {
            try
            {
                DateTime dt = DateTime.Parse(dt_string);
                TimeSpan span = DateTime.Now - dt;
                if (span.TotalDays > 60)
                {
                    return dt.ToShortDateString();
                }
                else
                    if (span.TotalDays > 30)
                    {
                        return "1个月前";
                    }
                    else
                        if (span.TotalDays > 14)
                        {
                            return "2周前";
                        }
                        else
                        {
                            if (span.TotalDays > 7)
                            {
                                //return "1周前";
                                return string.Format("{0}月{1}日", dt.Month, dt.Day);
                                //return string.Format("{0}月{1}日 {2}点", dt.Month, dt.Day, dt.Hour);
                            }
                            else
                            {
                                if (span.TotalDays > 1)
                                {
                                    return string.Format("{0}月{1}日", dt.Month, dt.Day);
                                }
                                else
                                {
                                    if (span.TotalHours > 1)
                                    {
                                        return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                    }
                                    else
                                    {
                                        if (span.TotalMinutes > 1)
                                        {
                                            return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                        }
                                        else
                                        {
                                            if (span.TotalSeconds >= 10)
                                            {
                                                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                            }
                                            else
                                            {
                                                return "刚刚";
                                            }
                                        }
                                    }
                                }
                            }
                        }
            }
            catch
            {
                return "系统错误";
            }
        }
        #endregion
        #endregion
    }

    public static class Estatic
    {
        #region pulic
        /// <summary>
        /// 时间格式
        /// </summary>
        public enum ForDateTime
        {
            /// <summary>
            /// 2011-10-10 10:23
            /// </summary>
            类型一 = 1,
            /// <summary>
            /// 2011-12-23
            /// </summary>
            类型二,
            /// <summary>
            /// 2011年12月23日 10时20分
            /// </summary>
            类型三,
            /// <summary>
            /// 2011年12月23日
            /// </summary>
            类型四,
            /// <summary>
            /// 11-10-10 10:23
            /// </summary>
            类型五,
            /// <summary>
            /// 10-10 10:23
            /// </summary>
            类型六,
            /// <summary>
            /// 10-10
            /// </summary>
            类型七,
            /// <summary>
            /// 2011-01-01 12:00:00
            /// </summary>
            类型八,
            /// <summary>
            /// 10月12日
            /// </summary>
            类型九,
            /// <summary>
            /// 2013.12.10
            /// </summary>
            类型十
        }
        #endregion
    }
}
