using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.Mail;

namespace DealMvc.Common
{
    /// <summary>
    /// 页面使用的代码
    /// </summary>
    public static class HtmlStrHelper
    {

        /// <summary>
        /// 正则表达式截取字符,返回string
        /// </summary>
        /// <param name="_String">源字符串</param>
        /// <param name="StartString">开始(不包括)</param>
        /// <param name="EndString">结尾(不包括)</param>
        /// <returns>返回截字符串</returns>
        public static string RegForString(string _String, string StartString, string EndString)
        {
            string RegExp = StartString + "(.*?)" + EndString;
            Regex _Regex = new Regex(RegExp, RegexOptions.IgnoreCase);

            while (_Regex.IsMatch(_String))
            {
                _String = _Regex.Match(_String).Value.ToString();
                _String = _String.Substring(StartString.Length);
            }

            return Regex.Replace(_String, StartString + "|" + EndString, "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 正则表达式截取字符,返回ArrayList
        /// </summary>
        /// <param name="_String">源字符串</param>
        /// <param name="StartString">开始(不包括)</param>
        /// <param name="EndString">结尾(不包括)</param>
        /// <returns>返回截字符串</returns>
        public static ArrayList RegForArrayList(string _String, string StartString, string EndString)
        {
            string RegExp = StartString + "(.*?)" + EndString;
            Regex _Regex = new Regex(RegExp, RegexOptions.IgnoreCase);
            ArrayList _ArrayList = new ArrayList();
            MatchCollection _MatchCollection = _Regex.Matches(_String);
            for (int i = 0; i < _MatchCollection.Count; i++)
            {
                _ArrayList.Add(_MatchCollection[i].Value.ToString());
            }
            for (int u = 0; u < _ArrayList.Count; u++)
            {
                string str = _ArrayList[u].ToString().Substring(StartString.Length);
                str = RegForString(str, StartString, EndString);
                str = Regex.Replace(str, StartString + "|" + EndString, "", RegexOptions.IgnoreCase);
                _ArrayList[u] = str;
            }
            return _ArrayList;
        }

        public static string GetDateSearchAge(ref int? ckid)
        {
            int i_Year = DateTime.Now.Year;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //18-100
            ckid = ckid.ToInt32();
            if (ckid < i_Year - 100 || ckid > i_Year - 12)
                ckid = null;
            for (int i = 12; i <= 100; i++)
            {
                sb.AppendFormat("<option value='{1}' {2}>{0}</option>", i, i_Year - i, i_Year - i == ckid ? " selected='selected'" : "");
            }

            return sb.ToString2();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showhtml"></param>
        /// <returns></returns>
        public static string ShowValueBrSpan(string showhtml)
        {
            string ps_show = showhtml;
            return string.IsNullOrEmpty(ps_show) ? "" : "<br/><span class=\"red\">" + ps_show + "</span>";
        }

        #region 经常使用的方法
        /// <summary>
        /// 根据用户名搜索给定表信息
        /// 如：//Member_UniqueID in(select M_UniqueID from XShop_Member where M_UserName like '%{0}%')
        /// 应用：DealMvc.Common.HtmlString.SqlMemberByWhere(Common.HtmlString.TableInColumnName.Member_UniqueID,Common.HtmlString.Member_ReturnColumn.M_UniqueID,XShop_Member_UserName));
        /// </summary>        
        /// <param name="TableInColumnName">In 之前的字段</param>
        /// <param name="XShop_Member_ReturnColumn">会员表{XShop_Member} 查询的字段</param>
        /// <param name="XShop_Member_UserName">会员表{XShop_Member}的会员名{M_UserName}</param>
        /// <returns></returns>
        public static string SqlMemberByWhere(TableInColumnName TableInColumnName, Member_ReturnColumn Member_ReturnColumn, string Member_UserName)
        {
            StringBuilder sb_sql = new StringBuilder();
            if (!string.IsNullOrEmpty(Member_UserName.ToString2().Trim()))
            {
                sb_sql.AppendFormat(" {0} in(select {1} from XShop_Member where M_UserName like '%{2}%')",
                    new object[]{
                    TableInColumnName.ToString2(), 
                    Member_ReturnColumn.ToString2(),
                    Member_UserName.ToString2().Trim()
                });
            }
            return sb_sql.ToString();
        }
        /// <summary>
        /// 表字段 外键名
        /// </summary>
        public enum TableInColumnName { Member_UniqueID = 1, Member_ID }
        /// <summary>
        /// 会员表{XShop_Member} 查询的字段
        /// </summary>
        public enum Member_ReturnColumn { M_UniqueID = 1, id }
        #endregion

        /// <summary>
        /// C# 获取文件大小的单位换算  
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string FormatFileSize(Int64 fileSize)
        {
            if (fileSize == null || fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSize >= 1024 * 1024 * 1024)
            {
                return string.Format("{0:########0.00} GB", ((Double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} MB", ((Double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0.00} KB", ((Double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0} bytes", fileSize);
            }
        }
        /// <summary>
        /// KindEditor.ready js
        /// </summary>
        /// <param name="TextAreaName">控件名称</param>
        /// <returns></returns>
        public static string KindEditorScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<link rel='stylesheet' href='/KindEditor/themes/default/default.css' />");
            sb.AppendLine("<script src='/KindEditor/kindeditor.js' type='text/javascript'></script>");
            sb.AppendLine("<script src='/KindEditor/lang/zh_CN.js' type='text/javascript'></script>");
            return sb.ToString();
        }

        /// <summary>
        /// KindEditor.ready
        /// </summary>
        /// <param name="TextAreaName">控件名称</param>
        /// <returns></returns>
        public static string KindEditorReady(string TextAreaName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine(" KindEditor.ready(function (K) { window.EditorObject = K.create('textarea[name=\"" + TextAreaName.ToString2() + "\"]', { resizeType: 1, uploadJson: '/Comm/UploadImage', fileManagerJson: '/Comm/ProcessRequest', allowFileManager: true, themeType: 'simple' }); });");
            sb.AppendLine("</script>");
            return sb.ToString();
        }
        /// <summary>
        /// KindEditor.ready
        /// </summary>
        /// <param name="TextAreaName">控件名称</param>
        /// <returns></returns>
        public static string KindEditorReady只读(string TextAreaName) {
            StringBuilder sb=new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine(" KindEditor.ready(function (K) { window.EditorObject = K.create('textarea[name=\"" + TextAreaName.ToString2() + "\"]', { resizeType: 1, uploadJson: '/Comm/UploadImage', fileManagerJson: '/Comm/ProcessRequest', allowFileManager: true, themeType: 'simple' });EditorObject.readonly(true); });");
            sb.AppendLine("</script>");
            return sb.ToString();
        }
        /// <summary>
        /// KindEditor.ready min
        /// </summary>
        /// <param name="TextAreaName">控件名称</param>
        /// <returns></returns>
        public static string KindEditorReadyMin(string TextAreaName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("  KindEditor.ready(function (K) {window.EditorObject = K.create('textarea[name=\"" + TextAreaName.ToString2() + "\"]', { resizeType: 1, allowPreviewEmoticons: false, allowImageUpload: false,allowFileUpload: false,allowFileManager :false, items: ['fontname', 'preview', 'undo', 'redo', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', 'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', 'lineheight', '|', 'emoticons','flash', 'image', 'link', 'fullscreen', '|', 'about'], themeType: 'simple', pasteType: 1, filterMode: true }); });");
            sb.AppendLine("</script><style>.ke-inline-block{display: none;}</style>");
            return sb.ToString();
        }
        /// <summary>
        /// Radio TrueAndFalse 数据库字段是：Bit 页面编辑
        /// 范例如：<%=DealMvc.Common.HtmlString.HtmlInputRadioTrueAndFalse("MR_IsDefault", true, "是", "否")%>
        /// </summary>
        /// <param name="inputname">控件名称</param>
        /// <param name="checkedval">选中</param>
        /// <returns></returns>
        public static string HtmlInputRadioTrueAndFalse(string inputname, bool? checkedval)
        {
            return HtmlInputRadioTrueAndFalse(inputname, checkedval, "是", "否");
        }
        /// <summary>
        /// Radio TrueAndFalse 数据库字段是：Bit 页面编辑
        /// 范例如：<%=DealMvc.Common.HtmlString.HtmlInputRadioTrueAndFalse("MR_IsDefault", true, "是", "否")%>
        /// </summary>
        /// <param name="inputname">控件名称</param>
        /// <param name="checkedval">选中</param>
        /// <param name="TrueString">True文字</param>
        /// <param name="FalseString">False文字</param>
        /// <returns></returns>
        public static string HtmlInputRadioTrueAndFalse(string inputname, bool? checkedval, string TrueString, string FalseString)
        {
            StringBuilder sb_new = new StringBuilder();
            sb_new.AppendFormat("<input type=\"radio\" {1} value=\"True\" name=\"{0}\" id=\"{0}_1\">", inputname, checkedval ?? true ? "checked=\"checked\"" : "");
            sb_new.AppendFormat("<label for=\"{0}_1\" style='cursor: pointer;'  title='{1}'>{1}</label>&nbsp;&nbsp;", inputname, TrueString);
            sb_new.AppendFormat("<input type=\"radio\" {1} value=\"False\" name=\"{0}\" id=\"{0}_0\">", inputname, checkedval ?? false ? "" : "checked=\"checked\"");
            sb_new.AppendFormat("<label for=\"{0}_0\" style='cursor: pointer;' title='{1}'>{1}</label>", inputname, FalseString);
            return sb_new.ToString();
        }
        // <summary>
        /// Html自定义下拉(控件名称, "", "全部", "true", "开启", "false", "关闭")
        /// </summary>
        /// <param name="控件名称"></param>
        /// <returns></returns>
        public static string Html自定义下拉(string 控件名称)
        {
            return Html自定义下拉(控件名称, "", "全部", "true", "开启", "false", "关闭");
        }

        /// <summary>
        /// Html自定义下拉
        /// </summary>
        /// <param name="控件名称"></param>
        /// <param name="默认值"></param>
        /// <param name="默认文本"></param>
        /// <param name="第一值"></param>
        /// <param name="第一文本"></param>
        /// <param name="第二值"></param>
        /// <param name="第二文本"></param>
        /// <returns></returns>
        public static string Html自定义下拉(string 控件名称, string 默认值, string 默认文本, string 第一值, string 第一文本, string 第二值, string 第二文本)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<select name=\"{0}\">", 控件名称);
            if (!string.IsNullOrEmpty(默认文本))
                sb.AppendFormat("<option value=\"{1}\">{0}</option>", 默认文本, 默认值);
            sb.AppendFormat("<option value=\"{1}\">{0}</option>", 第一文本, 第一值);
            sb.AppendFormat("<option value=\"{1}\">{0}</option>", 第二文本, 第二值);

            sb.AppendFormat("</select>");
            return sb.ToString();
        }

        /// <summary>
        /// img TrueAndFalse 数据库字段是：Bit 页面显示
        /// 范例如：<%=DealMvc.Common.HtmlString.HtmlImgTrueAndFalse(m_xsmr.MR_IsDefault) %>
        /// </summary>
        /// <param name="TrueAndFalse">TrueAndFalse 默认：false</param>
        /// <returns></returns>
        public static string HtmlImgTrueAndFalse(bool? TrueAndFalse)
        {
            return string.Format("<img src=\"/App_Themes/Cms/Pic/{0}.gif\" />", (TrueAndFalse ?? false).ToString2().ToLower());
        }

        /// <summary>
        /// img TrueAndFalse 数据库字段是：Bit 页面显示
        /// 范例如：<%=DealMvc.Common.HtmlString.HtmlImgTrueAndFalse(m_xsmr.MR_IsDefault) %>
        /// </summary>
        /// <param name="TrueAndFalse">TrueAndFalse 默认：false</param>
        /// <returns></returns>
        public static string HtmlImgTrueAndFalse_Reception(bool? TrueAndFalse)
        {
            return string.Format("<img src=\"/App_Themes/Cms/receptionPic/{0}.png\" />", (TrueAndFalse ?? false).ToString2().ToLower());
        }
        /// <summary>
        /// 返回时间差[返回秒]
        /// 程序调用：TDiff(DateTime.Now, DateTime.Now.AddHours(1)) 
        /// 运行结果：3600
        /// </summary>
        /// <param name="DateTimex">时间1（小）</param>
        /// <param name="DateTimed">时间2（大）</param>
        /// <returns>返回秒</returns>
        public static double TDiffTotalSeconds(DateTime? DateTimin, DateTime? DateTimemax)
        {
            DateTimin = DateTimin ?? DateTime.Now;
            DateTimemax = DateTimemax ?? DateTime.Now;
            double d_time = ((TimeSpan)(DateTimemax - DateTimin)).TotalSeconds;
            return d_time > 0 ? d_time : 0;
        }
        /// <summary>
        /// 返回时间差[返回天]
        /// 程序调用：TDiff(DateTime.Now, DateTime.Now.AddHours(1)) 
        /// 运行结果：3600
        /// </summary>
        /// <param name="DateTimex">时间1（小）</param>
        /// <param name="DateTimed">时间2（大）</param>
        /// <returns>返回秒</returns>
        public static int TDiffTotalDays(DateTime? DateTimin, DateTime? DateTimemax)
        {
            DateTimin = DateTimin ?? DateTime.Now;
            DateTimemax = DateTimemax ?? DateTime.Now;
            double d_time = ((TimeSpan)(DateTimemax - DateTimin)).TotalDays;
            return d_time > 0 ? d_time.ToInt32() : 0;
        }
        /// 页面显示图片
        /// </summary>
        /// <param name="imgstr">图片链接</param>
        /// <param name="errmsg"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static string ImgNone(string imgstr, string errmsg, int? w, int? h)
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
        public static string ImgNone(string imgstr, int? w, int? h)
        {
            return ImgNone(imgstr, "【暂无图】", w, h);
        }

        #region 上传图片并获取路径  当没有的时候 string.Empty
        /// <summary>
        /// 上传图片并获取路径 当没有的时候 string.Empty
        /// </summary>
        /// <param name="inputname"></param>
        /// <returns></returns>
        public static string UpFileClassExt(string inputname, string file_extension)
        {
            return UpFileClass(inputname, null, file_extension);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputname"></param>
        /// <param name="defaultPic"></param>
        /// <returns></returns>
        public static string UpFileClass(string inputname, string defaultPic)
        {
            return UpFileClass(inputname, defaultPic, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputname"></param>
        /// <returns></returns>
        public static string UpFileClass(string inputname)
        {
            return UpFileClass(inputname, null, null);
        }
        /// <summary>
        /// 上传图片并获取路径
        /// </summary>
        /// <param name="inputname"></param>
        /// <param name="defaultPic"></param>
        /// <param name="file_extension"></param>
        /// <returns></returns>
        public static string UpFileClass(string inputname, string defaultPic, string file_extension)
        {
            string str_url = string.Empty;
            try
            {
                Globals.UpFileResult _UpFileClass = Globals.Upload(inputname, file_extension);
                if (_UpFileClass.isEmpty || _UpFileClass.returnfilename.Count == 0)
                {
                    if (!string.IsNullOrEmpty(defaultPic.ToString2()))
                        return defaultPic;
                }
                else
                {
                    if (_UpFileClass.returnerror.Count == 0)
                        str_url = _UpFileClass.returnfilename[0].ToString2();
                }
            }
            catch
            {
                return defaultPic;
            }
            return str_url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputname"></param>
        /// <param name="defaultPic"></param>
        /// <returns></returns>
        public static string UpAttachmentClass(string inputname, string defaultPic, ref string a_title)
        {
            return UpAttachmentClass(inputname, defaultPic, null, ref a_title);
        }
        /// <summary>
        /// 上传附件并获取路径
        /// </summary>
        /// <param name="inputname"></param>
        /// <param name="defaultPic"></param>
        /// <param name="file_extension"></param>
        /// <returns></returns>
        public static string UpAttachmentClass(string inputname, string defaultPic, string file_extension, ref string a_title)
        {
            string str_url = string.Empty;
            try
            {
                Globals.UpFileResult _UpFileClass = null;

                if (_UpFileClass.isEmpty || _UpFileClass.returnfilename.Count == 0)
                {
                    if (!string.IsNullOrEmpty(defaultPic.ToString2()))
                        return defaultPic;
                }
                else
                {
                    if (_UpFileClass.returnerror.Count == 0)
                        str_url = _UpFileClass.returnfilename[0].ToString2();
                }
            }
            catch
            {
                return defaultPic;
            }
            return str_url;
        }
        #endregion




        /// <summary>
        /// 评价显示小图片
        /// </summary>
        /// <param name="i_ict"></param>
        /// <param name="ico1"></param>
        /// <param name="ico2"></param>
        /// <returns></returns>
        public static string HtmlGoodsComment(int? i_ict, string ico1, string ico2)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                if (i < i_ict)
                    sb.Append("<img src=\"" + ico1 + "\" />");
                else
                    sb.Append("<img src=\"" + ico2 + "\" />");
            }
            return sb.ToString();
        }
        /*
        int? star = 0;
        foreach (XShop_OrdersProductEvaluate item in ProductPingLun)
        {
            star += item.OPE_Rating;
        }
         */
        /// <summary>
        /// 评价显示小图片
        /// </summary>
        /// <returns></returns>
        public static string ShowHtmlGoodsComment(string ico1, string ico2, int? star, int? ProductPingLun_Count)
        {

            if (star == null || star.ToInt32() <= 0)
                return HtmlGoodsComment(5, ico1, ico2);
            int? g_star = star / ProductPingLun_Count.ToInt32();

            return HtmlGoodsComment(g_star, ico1, ico2);
        }



        #region 会员中心调用的方法--LastNew

        /// <summary>
        /// 会员中心调用的方法--LastNew
        /// </summary>
        /// <param name="aname"></param>
        /// <param name="aurl"></param>
        /// <param name="ckstring"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string TopRightNav(string aname, string aurl, string ckstring, string action, string Controller, string ckController)
        {
            bool b_IsBool = false;
            if (string.IsNullOrEmpty(aurl) || aurl == "#")
                b_IsBool = false;
            else
            {
                ckController = ckController.ToString2().ToLower();
                //action =action.ToString2().ToLower();
                string[] splist = ckstring.ToString2().ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in splist)
                {
                    if (ckController + "@" + item == Controller + "@" + action)
                    {
                        b_IsBool = true;
                        break;
                    }

                }
            }
            /*
             
            <div class="childmodel">
                <span class="pic"></span>
                <span class="word"><a href="#">我的团购</a></span>
            </div>
             */
            return string.Format(" <h5><a href=\"{0}\" class=\"{2}\">{1}</a></h5>", new object[] {
                aurl,
                aname,
                b_IsBool?"aNow":""
            });
        }

        /// <summary>
        /// 会员中心调用的方法--LastNew
        /// </summary>
        /// <param name="aname"></param>
        /// <param name="aurl"></param>
        /// <param name="ckstring"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string SDXRightNav(string aname,string aurl,string ckstring,string action,string Controller,string ckController)
        {
            bool b_IsBool = false;
            if (string.IsNullOrEmpty(aurl) || aurl == "#")
                b_IsBool = false;
            else {
                ckController=ckController.ToString2().ToLower();
                //action =action.ToString2().ToLower();
                string[] splist = ckstring.ToString2().ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in splist)
                {
                    if(ckController+"@"+item==Controller+"@"+action)
                    {
                        b_IsBool = true;
                        break;
                    }

                }
            }
            /*
             
            <div class="childmodel">
                <span class="pic"></span>
                <span class="word"><a href="#">我的团购</a></span>
            </div>
             */
            return string.Format("<li><a class=\"{2}\" href='{0}'>{1}</a></li>", new object[] {
                aurl,
                aname,
                b_IsBool?"aNow":""
            });
        }

        #endregion


        


    }

    /// <summary>
    /// 人民币格式转换
    /// </summary>
    public class RmbHelper
    {
        /// <summary>
        /// 转换人民币大小金额
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns>返回大写形式</returns>
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";           //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str3 = "";    //从原num值中取出的值
            string str4 = "";    //数字的字符串形式
            string str5 = "";    //人民币大写金额形式
            int i;               //循环变量
            int j;               //num的值乘以100的字符串长度
            string ch1 = "";     //数字的汉语读法
            string ch2 = "";     //数字位的汉字读法
            int nzero = 0;       //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            str4 = ((long)(num * 100)).ToString(); //将num乘100并转换成字符串形式
            j = str4.Length;                       //找出最高位
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);         //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);       //取出需转换的某一位的值
                temp = Convert.ToInt32(str3);      //转换为数字
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /// <summary>
        /// 转换人民币大小金额(一个重载,将字符串先转换成数字)
        /// </summary>
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param>
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
    }
}



