using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExceptionEx;


namespace DealMvc.Common
{
    /// <summary>
    /// 页面使用的代码
    /// </summary>
    public static class HtmlString
    {
        

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
            sb_new.AppendFormat("<input type=\"radio\" {1} value=\"True\" name=\"{0}\" id=\"{0}_0\">", inputname, checkedval ?? true ? "checked=\"checked\"" : "");
            sb_new.AppendLine("");
            sb_new.AppendFormat("<label for=\"{0}_0\">{1}</label>", inputname, TrueString);
            sb_new.AppendFormat("<input type=\"radio\" {1} value=\"False\" name=\"{0}\" id=\"{0}_0\">", inputname, checkedval ?? false ? "" : "checked=\"checked\"");
            sb_new.AppendLine("");
            sb_new.AppendFormat("<label for=\"{0}_0\">{1}</label>", inputname, FalseString);
            return sb_new.ToString();
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
        /// img TrueAndFalse 数据库字段是：Bit 页面显示 图标比较大
        /// 范例如：<%=DealMvc.Common.HtmlString.HtmlImgDaTrueAndFalse(m_xsmr.MR_IsDefault) %>
        /// </summary>
        /// <param name="TrueAndFalse">TrueAndFalse 默认：false</param>
        /// <returns></returns>
        public static string HtmlImgDaTrueAndFalse(bool? TrueAndFalse)
        {
            return string.Format("<img src=\"/App_Themes/Cms/Pic/icon-msg_{0}.png\" />", (TrueAndFalse ?? false).ToString2().ToLower());
        }

        /// <summary>
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
            {
                return errmsg.ToString2();
            }
            string AllFolderPath = "";
            if (imgstr.Contains("http://"))
            {
                AllFolderPath = imgstr;
            }
            else
            {
                AllFolderPath = System.Web.HttpContext.Current.Server.MapPath("~" + imgstr);
            }

            if (Directory.Exists(AllFolderPath))
            {
                return errmsg.ToString2();
            }

            return string.Format("<img src=\"{0}\" style=\"width:{1}px; height:{2}px;\"/>", imgstr, w ?? 20, h ?? 20);
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
                Common.Globals.UpFileResult _UpFileClass = Common.Globals.Upload(inputname, file_extension);
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

        #region 经常使用的方法用户名以及编号查询
        /// <summary>
        /// 根据用户名搜索给定表信息
        /// 如：//Member_UniqueID in(select M_UniqueID from XShop_Member where M_UserName like '%{0}%')
        /// 应用：DealMvc.Common.HtmlString.SqlMemberByWhere(Common.HtmlString.TableInColumnName.Member_UniqueID,Common.HtmlString.Member_ReturnColumn.M_UniqueID,XShop_Member_UserName));
        /// </summary>        
        /// <param name="TableInColumnName">In 之前的字段</param>
        /// <param name="XShop_Member_ReturnColumn">会员表{XShop_Member} 查询的字段</param>
        /// <param name="XShop_Member_UserName">会员表{XShop_Member}的会员名{M_UserName}</param>
        /// <returns></returns>
        public static string SqlMemberByWhereGetUNameAndUnid(TableInColumnName TableInColumnName, Member_ReturnColumn Member_ReturnColumn, string Member_UserName, string Member_UniqueID)
        {
            StringBuilder sb_sql = new StringBuilder();
            if (!string.IsNullOrEmpty(Member_UniqueID.ToString2().Trim()))
            {
                sb_sql.AppendFormat("{0} in (select {1} from XShop_Member where M_UniqueID = '{3}' and M_UserName like '%{2}%')",
                    new object[]{
                    TableInColumnName.ToString2(), 
                    Member_ReturnColumn.ToString2(),
                    Member_UserName.ToString2().Trim(),
                    Member_UniqueID.ToString2().Trim()
                });
            }
            else
            {
                sb_sql.AppendFormat("{0} in (select {1} from XShop_Member where M_UserName like '%{2}%')",
                    new object[]{
                    TableInColumnName.ToString2(), 
                    Member_ReturnColumn.ToString2(),
                    Member_UserName.ToString2().Trim()
                });
            }
            return sb_sql.ToString();
        }
        #endregion

        /// <summary>
        /// 会员中心调用的方法
        /// </summary>
        /// <param name="aname"></param>
        /// <param name="aurl"></param>
        /// <param name="ckstring"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Ml(string aname, string aurl, string ckstring, string action)
        {
            bool b_IsBool = ckstring.ToLower().Contains(action.ToLower());
            return string.Format("<dd {2}><a href='{0}'>>> {1}&nbsp;{3}</a></dd>", new object[] {
                aurl,
                aname,
                (b_IsBool?"class='current'":""),
                (b_IsBool?Common.HtmlString.HtmlImgTrueAndFalse(true):"")
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
        public static string XShop(string aname, string aurl, string ckstring, string action)
        {
            bool b_IsBool = false;
            if (string.IsNullOrEmpty(aurl) || aurl == "#")
                b_IsBool = false;
            else
            {
                action = action.ToString2().ToLower();
                string[] splist = ckstring.ToString2().ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in splist)
                {
                    if (item == action)
                    {
                        b_IsBool = true;
                        break;
                    }

                }
            }

            return string.Format("<dd><a href=\"{0}\" class=\"{2}\">{1}</a></dd>", new object[] {
                aurl,
                aname,
                b_IsBool?"aNow":""
            });
        }



        /// <summary>
        ///帮助中心调用的方法--LastNew
        /// </summary>
        /// <param name="aname"></param>
        /// <param name="aurl"></param>
        /// <param name="ckstring"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string XShopHelp(string aname, string aurl, string ckstring, string action)
        {
            bool b_IsBool = false;
            if (string.IsNullOrEmpty(aurl) || aurl == "#")
                b_IsBool = false;
            else
            {
                action = action.ToString2().ToLower();
                string[] splist = ckstring.ToString2().ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in splist)
                {
                    if (item == action)
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

            return string.Format("<li class=\"li1\">> <a  class=\" {2} a\" href='{0}'>{1}</a></li>", new object[] {
                aurl,
                aname,
                b_IsBool?"aNow":""
            });
        }


        public static string PlIST(Type t, string selectValue, string InputName)
        {
            string temps = "";
            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue.ToString2())
                {
                    temps = string.Format("<div class=\"CardModel\"><span class=\"word\">{0}</span> <span class=\"pic CloseModel\" catename='{1}'></span></div>", s, InputName);
                    break;
                }
            }

            return temps.EnumReplace().ToString2();

        }
        public static string PlISThtml(Type t, string selectValue, string InputName)
        {
            string Temp = string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", InputName.ToString2(), selectValue.ToString2());
            foreach (string s_Gg in Enum.GetNames(t))
            {
                Temp += string.Format("<a class=\"CommClick {0}\" cateid='{2}'catename=\"{3}\">{1}</a>",
                    (int)Enum.Parse(t, s_Gg) == selectValue.ToInt32() ? "Ahover" : "", s_Gg, ((int)Enum.Parse(t, s_Gg)).ToString(), InputName);
            }

            return Temp.EnumReplace().ToString2();

        }
    }
}

namespace DealMvc
{
    /// <summary>
    /// 提示信息 日志写入方法
    /// </summary>
    public static class MsgClass
    {
        #region 记录
        /// <summary>
        /// 提示信息-不跳转到其它页面
        /// </summary>
        /// <param name="_MvcController">MvcController</param>
        /// <param name="msg">msg</param>
        public static void AlertMsg(System.Web.Mvc.Controller _MvcController, string msg)
        {
            MyExceptionLog.AlertMessage(_MvcController, msg, false);
        }
        /// <summary>
        /// 提示信息-是否跳转到其它页面
        /// </summary>
        /// <param name="_MvcController">MvcController</param>
        /// <param name="msg">msg</param>
        /// <param name="IsRedirect">是否跳转到其它页面</param>
        public static void AlertMsg(System.Web.Mvc.Controller _MvcController, string msg, bool IsRedirect)
        {
            MyExceptionLog.AlertMessage(_MvcController, msg, IsRedirect);
        }
        #endregion

        #region 记录日志错误
        /// <summary>
        /// 日志写入方法
        /// </summary>
        /// <param name="_MvcController">MvcController</param>
        /// <param name="ex">Exception ex</param>
        public static void LogMsg(System.Web.Mvc.Controller _MvcController, Exception ex)
        {
            MyExceptionLog.WriteLog(_MvcController, ex);
        }
        /// <summary>
        /// 写入异常信息
        /// ("GetTopList:" + ex.Message)
        /// </summary>
        /// <param name="message">信息</param>
        public static void LogErrors(string message)
        {
            MyExceptionLog.AddLogError("异常信息-" + message);
        }
        /// <summary>
        /// 写入异常信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void LogErrors(Exception _ex)
        {
            MyExceptionLog.AddLogError("异常信息-" + _ex.Message);
        }
        #endregion
    }

    #region 得到数字加英文的随机数\得到英文的随机数\得到数字的随机数\得到中文的随机数
    /// <summary>
    /// 随机处理类
    /// 得到数字加英文的随机数\得到英文的随机数\得到数字的随机数\得到中文的随机数
    /// </summary>
    public static class RandomCode
    {
        private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 得到数字加英文的随机数，参数：随机数长度，返回值：一个字符串！
        /// </summary>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public static string pxkt_GetCharFont(int strLength)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < strLength; i++)
            {
                //线程休眠20毫秒
                System.Threading.Thread.Sleep(20);
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }


        private static char[] englishchar = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 得到英文的随机数，参数：随机数长度，返回值：一个字符串！
        /// </summary>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public static string GetEnglishChar(int strLength)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(52);
            Random rd = new Random();
            for (int i = 0; i < strLength; i++)
            {
                //线程休眠20毫秒
                System.Threading.Thread.Sleep(20);
                newRandom.Append(englishchar[rd.Next(52)]);
            }
            return newRandom.ToString();

        }


        private static char[] numchar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// 得到数字的随机数，参数：随机数长度，返回值：一个字符串！
        /// </summary>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public static string GetNumChar(int strLength)
        {

            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < strLength; i++)
            {
                //线程休眠20毫秒
                System.Threading.Thread.Sleep(20);
                newRandom.Append(numchar[rd.Next(10)]);
            }
            return newRandom.ToString();

        }



        /// <summary>
        /// 得到中文的随机数，参数：随机数长度，返回值：一个字符串！
        /// </summary>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public static string GetChineseChar(int strLength)
        {

            System.Text.StringBuilder newRandom = new System.Text.StringBuilder();
            //获取GB2312编码页（表）
            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
            //调用函数产生I个随机中文汉字编码
            object[] bytes = CreateRegionCode(strLength);

            for (int i = 0; i < strLength; i++)
            {
                //根据汉字编码的字节数组解码出中文汉字
                string str = gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
                newRandom.Append(str);

            }
            return newRandom.ToString();
        }


        private static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            //定义一个object数组用来
            object[] bytes = new object[strlength];

            /**/
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
             每个汉字有四个区位码组成
             区位码第1位和区位码第2位作为字节数组第一个元素
             区位码第3位和区位码第4位作为字节数组第二个元素
            */
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(str_r, i);

            }

            return bytes;

        }
    }
    #endregion
}
