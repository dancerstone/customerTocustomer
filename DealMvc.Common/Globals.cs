using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Mail;
using DealMvc.Common.Config;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DealMvc.Common
{
    public class Globals
    {
        #region 生成唯一编号
        /// <summary>
        /// 产生一个新的唯一编号
        /// </summary>
        /// <returns></returns>
        public static string CreateNewUniqueID()
        {
            return Net.MathRandom.RandomDateTime(true, true).ToString2() + RandomCode.GetNumChar(5).ToString2();
        }
        /// <summary>
        /// 生产唯一编号
        /// </summary>
        /// <returns></returns>
        public static string CreateNumber()
        {
            return Net.MathRandom.RandomDateTime(true, false).ToString2() + RandomCode.GetNumChar(5).ToString2();
        }

        #endregion

        #region 验证码

        /// <summary>
        /// 验证码Cookies键值Key
        /// </summary>
        public static string verifyCodeCookieName = "Deal_verifyCodeCookieName";
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string getCode()
        {
            HttpContext context = HttpContext.Current;
            HttpCookie cookie = context.Request.Cookies[verifyCodeCookieName];
            string str2 = null;
            if ((cookie != null) && !string.IsNullOrEmpty(cookie.Value))
            {
                try
                {
                    str2 = cookie.Value;
                    if (context.Response.Cookies[verifyCodeCookieName] == null)
                    {
                        HttpCookie cookie2 = new HttpCookie(verifyCodeCookieName);
                        cookie2.Expires = DateTime.Now.AddDays(-1.0);
                        context.Response.Cookies.Add(cookie2);
                    }
                    else
                    {
                        context.Response.Cookies[verifyCodeCookieName].Expires = DateTime.Now.AddDays(-0.1);
                    }
                }
                catch
                {
                }
            }
            if (str2 != null)
            {
                return str2;
            }
            return DateTime.UtcNow.Ticks.ToString();
        }

        #endregion

        #region 获取客户端IP地址

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP()
        {
            System.Web.HttpContext context = HttpContext.Current;
            string userIP = string.Empty;
            if (context.Request.ServerVariables["HTTP_VIA"] == null)
            {
                userIP = context.Request.UserHostAddress;
            }
            else
            {
                userIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (userIP == null) userIP = "";
            return userIP;
        }

        public static string GetAbsoluteDomian()
        {
            string Host = HttpContext.Current.Request.Url.Host.Trim('/');
            int Port = HttpContext.Current.Request.Url.Port;
            if (Port != 80)
            {
                return Host + ":" + Port;
            }
            return Host;
        }
        /// <summary>
        /// 是否开发模式
        /// </summary>
        private static bool DevelopCode = true;
        /// <summary>
        /// 页面需要在head内使用的base
        /// </summary>
        /// <returns>http://www.g.cn:80</returns>
        public static string GetHostUrlWeb()
        {
            Model.SiteInfo _model = Model.SiteInfo.GetModel(t => t.id != 0);
            string stringBaseUrl = string.Empty;
            string webdomain = string.Empty;
            if (DevelopCode)
                webdomain = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port == 80 ? "" : ":" + HttpContext.Current.Request.Url.Port.ToString());
            else
            {
                string HomeUrl = _model.WebAddress;
                webdomain = HomeUrl.Length > 0 ? HomeUrl + "/" : stringBaseUrl;
            }
            return webdomain;
        }

        #endregion

        #region 操作Cookie

        public static string CookieDESEncryptKey = "Cookie123456";

        /// <summary>
        /// 获取Cookie值, 不存在返回string.Empty
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getCookie(string key)
        {
            string value = string.Empty;
            try
            {
                System.Web.HttpContext context = HttpContext.Current;
                HttpCookie cookie = context.Request.Cookies[key];
                if (!(cookie == null || string.IsNullOrEmpty(cookie.Value))) value = DESEncrypt.Decrypt(cookie.Value, CookieDESEncryptKey);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool setCookie(string key, string value)
        {
            try
            {
                System.Web.HttpContext context = HttpContext.Current;
                HttpCookie cookie = new HttpCookie(key);
                cookie.Value = DESEncrypt.Encrypt(value, CookieDESEncryptKey);
                cookie.Expires = DateTime.Now.AddDays(1.0);//一天
                //cookie.Domain = ".lc-demo.com";
                context.Response.Cookies.Add(cookie);
            }
            catch { return false; }
            return true;

            //try
            //{
            //    System.Web.HttpContext context = HttpContext.Current;

            //    //删除
            //    try
            //    {
            //        if (context.Request.Cookies[key] != null)
            //        {
            //            HttpCookie mycookie;
            //            mycookie = context.Request.Cookies[key];
            //            TimeSpan ts = new TimeSpan(0, 0, 0, 0);//跨度
            //            mycookie.Expires = DateTime.Now.Add(ts);//立即过期
            //            context.Response.Cookies.Remove(key);//清除
            //            context.Response.Cookies.Add(mycookie);//写入立即过期的*/
            //            context.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
            //        }
            //    }
            //    catch { }

            //    if (!string.IsNullOrEmpty(value))
            //    {
            //        HttpCookie cookie = new HttpCookie(key);
            //        cookie.Value = DealMvc.DBUtility.DESEncrypt.Encrypt(value, CookieDESEncryptKey);
            //        cookie.Expires = DateTime.Now.AddHours(1.0);//一天
            //        context.Response.Cookies.Add(cookie);
            //    }
            //}
            //catch { return false; }
            //return true;

        }

        #endregion

        #region 获取后台或前台登录用户的信息

        private static string CmsAdminCookieKey = "CmsAdminCookieKey";
        public static string getAdminName()
        {
            return getCookie(CmsAdminCookieKey);
        }
        public static bool setAdminName(string value)
        {
            return setCookie(CmsAdminCookieKey, value);
        }
        public static bool isAdminLogin()
        {
            if (getAdminName() == string.Empty)
                return false;
            else
                return true;
        }

        private static string WebUserCookieKey = "WebUserCookieKey";
        public static string getUserName()
        {
            return getCookie(WebUserCookieKey);
        }
        public static bool setUserName(string value)
        {
            return setCookie(WebUserCookieKey, value);
        }
        public static bool isUserLogin()
        {
            if (getUserName() == string.Empty)
                return false;
            else
                return true;
        }

        #endregion

        #region 上传文件

        /// <summary>
        /// 上传返回结果类
        /// </summary>
        public class UpFileResult
        {
            private ArrayList _returnfilename = new ArrayList();
            /// <summary>
            /// 保存的文件路径名
            /// </summary>
            public ArrayList returnfilename
            {
                get { return _returnfilename; }
                set { _returnfilename = value; }
            }

            private ArrayList _returnerror = new ArrayList();
            /// <summary>
            /// 保存失败的描述信息
            /// </summary>
            public ArrayList returnerror
            {
                get { return _returnerror; }
                set { _returnerror = value; }
            }

            private bool _isEmpty = false;
            /// <summary>
            /// 上传文件是否为空
            /// </summary>
            public bool isEmpty
            {
                get { return _isEmpty; }
                set { _isEmpty = value; }
            }
        }

        public static UpFileResult Upload()
        {
            return Upload("", null);
        }

        /// <summary>
        /// 指定上传的控件名
        /// </summary>
        /// <param name="input_name"></param>
        /// <returns></returns>
        public static UpFileResult Upload(string input_name)
        {
            return Upload(input_name, null);
        }

        public static UpFileResult UploadSize(string input_name, int? UploadSize)
        {
            return Upload(input_name, null);
        }
        /// <summary>
        /// 指定上传的控件名和文件后缀名
        /// </summary>
        /// <param name="input_name"></param>
        /// <param name="file_extension">如：jpg|png|bmp|gif</param>
        /// <param name="UploadSize">MB 如：1 则代表1MB</param>
        /// <returns></returns>
        public static UpFileResult UploadSize(string input_name, string file_extension, int? UploadSize)
        {
            string path = "/UploadFolder/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            return Upload(input_name, path, file_extension, UploadSize);
        }
        /// <summary>
        /// 指定上传的控件名和文件后缀名
        /// </summary>
        /// <param name="input_name"></param>
        /// <param name="file_extension"></param>
        /// <returns></returns>
        public static UpFileResult Upload(string input_name, string file_extension)
        {
            string path = "/UploadFolder/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            return Upload(input_name, path, file_extension);
        }


        public static UpFileResult Upload(string input_name, string folder_path, string file_extension)
        {
            return Upload(input_name, folder_path, file_extension, null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input_name"></param>
        /// <param name="folder_path"></param>
        /// <param name="file_extension"></param>
        /// <param name="UploadSize"></param>
        /// <returns></returns>
        public static UpFileResult Upload(string input_name, string folder_path, string file_extension, int? UploadSize)
        {
            string UploadExtension = "png|jpg|gif|bmp|txt|doc|xls|rar|jpeg";
            if (!UploadSize.HasValue || UploadSize.ToInt32() <= 0)
                UploadSize = 300;
            ////保存的文件路径名
            //ArrayList returnfilename = new ArrayList();
            ////保存失败的描述信息
            //ArrayList returnerror = new ArrayList();
            ////上传文件是否为空
            //bool isEmpty = false;

            UpFileResult _UpFileResult = new UpFileResult();

            try
            {
                List<HttpPostedFile> fileList = new List<HttpPostedFile>();

                if (string.IsNullOrEmpty(input_name))
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                        fileList.Add(HttpContext.Current.Request.Files[i]);
                }
                else
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Keys.Count; i++)
                    {
                        if (input_name == HttpContext.Current.Request.Files.Keys[i].ToString())
                            fileList.Add(HttpContext.Current.Request.Files[i]);
                    }
                }

                foreach (HttpPostedFile file in fileList)
                {
                    if (file == null || file.ContentLength == 0)
                    {
                        _UpFileResult.isEmpty = true;
                    }
                    else if (Convert.ToDouble(file.ContentLength) > Convert.ToDouble(UploadSize) * 1024 * 1024)
                        _UpFileResult.returnerror.Add(string.Format("文件不能大于 {0}M", UploadSize));
                    else
                    {
                        //当前格式
                        string Extension = System.IO.Path.GetExtension(file.FileName).Replace(".", "");

                        //判断格式
                        bool isExtensionRight = false;
                        string[] Extensions = UploadExtension.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < Extensions.Length; i++)
                        {
                            if (Extensions[i].Trim().ToLower() == Extension.ToLower()) { isExtensionRight = true; break; }
                        }

                        //格式正确
                        if (isExtensionRight)
                        {
                            string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + DealMvc.Common.Net.MathRandom.RandomNumber(4).ToString() + "." + Extension;

                            string AllFolderPath = HttpContext.Current.Server.MapPath("~" + folder_path);
                            //string AllFolderPath = HttpContext.Current.Server.MapPath(folder_path);
                            if (!Directory.Exists(AllFolderPath))
                                Directory.CreateDirectory(AllFolderPath);

                            //保存文件
                            file.SaveAs(HttpContext.Current.Server.MapPath("~" + folder_path + filename));
                            //file.SaveAs(HttpContext.Current.Server.MapPath(folder_path + filename));
                            System.Threading.Thread.Sleep(10);

                            _UpFileResult.returnfilename.Add(folder_path + filename);
                        }
                        else
                            _UpFileResult.returnerror.Add(string.Format("格式不在允许的范围内,只允许格式为:{0}", UploadExtension));
                    }
                }

            }
            catch
            {
                _UpFileResult.returnerror.Add("上传文件出错");
            }

            //ArrayList output = new ArrayList();
            //output.Add(returnfilename);//在returnerror不为空的时候才能把此值当成正确的上传文件位置值
            //output.Add(returnerror);//上传出错描述信息,不为空表示上传存在问题
            //output.Add(isEmpty);//上传文件是否为空

            return _UpFileResult;
        }



        #endregion

        #region System.Data.DataTable 导出为 Excel报表

        /// <summary>
        /// System.Data.DataTable 导出为 Excel报表
        /// </summary>
        /// <param name="_DataTable"></param>
        /// <param name="Filename"></param>
        public static void DataTableToExcel(System.Data.DataTable _DataTable, string Filename)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            if ((_DataTable != null))
            {
                context.Response.Clear();
                context.Response.Charset = "GB2312";
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                context.Response.ContentType = "application/ms-excel";
                context.Response.AppendHeader("content-disposition", "attachment;filename=\"" + Filename + ".xls\"");
                CultureInfo cult = new CultureInfo("zh-CN", true);
                StringWriter sw = new StringWriter(cult);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                DataGrid dgrid = new DataGrid();
                dgrid.DataSource = _DataTable.DefaultView;
                dgrid.AllowPaging = false;
                dgrid.DataBind();
                htw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");

                dgrid.RenderControl(htw);
                context.Response.Write(sw.ToString());
                context.Response.End();
            }
        }

        #endregion

        #region 下载网页源代码

        /// <summary>
        /// 返回指定网页代码
        /// </summary>
        /// <param name="WebPath"></param>
        /// <returns></returns>
        public static string DownloadStringFromUrl(string Url)
        {
            return DownloadStringFromUrl(Url, "utf-8");
        }

        /// <summary>
        /// 返回指定网页代码
        /// </summary>
        /// <param name="Encod"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string DownloadStringFromUrl(string Url, string Encod)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Encoding = System.Text.Encoding.GetEncoding(Encod);
            string output = "";
            try
            {
                output = wc.DownloadString(Url);
            }
            catch
            {
                output = "NULL";
            }
            finally
            {
                wc.Dispose();
            }
            return output;
        }

        #endregion

        #region 写入和读取文本文件

        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static bool WriteText(string Text, string Path)
        {
            bool s = true;

            try
            {
                FileStream fs = new FileStream(Path, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);
                writer.Write(Text);
                writer.Flush(); writer.Dispose();
                fs.Close(); fs.Dispose();
            }
            catch { s = false; }
            return s;
        }

        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="_Encoding"></param>
        /// <returns></returns>
        public static string ReadText(string Path, System.Text.Encoding _Encoding)
        {
            StreamReader objReader = new StreamReader(Path, _Encoding);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close(); objReader.Dispose();
            return arrText.ToString();
        }

        #endregion

        #region 获取服务端和物理目录地址

        /// <summary>
        /// 获取服务端地址
        /// </summary>
        /// <param name="forumPath"></param>
        /// <returns></returns>
        public static string GetRootUrl(string forumPath)
        {
            forumPath = forumPath.IndexOf("/") == 0 ? forumPath.Substring(1) : forumPath;
            string ApplicationPath = HttpContext.Current.Request.ApplicationPath != "/" ? HttpContext.Current.Request.ApplicationPath : string.Empty;
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}/{4}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host,
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 ApplicationPath,
                                 forumPath);
        }

        /// <summary>
        /// 获取物理目录地址
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        #endregion

        #region 把汉字转换成拼音
        /// <summary>
        /// 把汉字转换成拼音
        /// </summary>
        public static class ChineseSpell
        {
            #region 属性数据定义 pyValue
            /// <summary>
            /// 属性数据定义
            /// </summary>
            private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };
            /// <summary>
            /// 属性数据定义pyName
            /// </summary>
            private static string[] pyName = new string[]
        {
            "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
            "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
            "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
            "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
            "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
            "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
            "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
            "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
            "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
            "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
            "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
            "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
            "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
            "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
            "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
            "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
            "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
            "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
            "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
            "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
            "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
            "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
            "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
            "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
            "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
            "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
            "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
            "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
            "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
            "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
            "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
            "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
            "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };

            #endregion

            #region 把汉字转换成拼音(全拼)无间隔符号
            /// <summary>
            /// 把汉字转换成拼音(全拼)无间隔符号
            /// </summary>
            /// <param name="hzString"></param>
            /// <returns></returns>
            public static string Convert(string hzString)
            {
                // 匹配中文字符
                Regex regex = new Regex("^[\u4e00-\u9fa5]$");
                byte[] array = new byte[2];
                string pyString = "";
                int chrAsc = 0;
                int i1 = 0;
                int i2 = 0;
                char[] noWChar = hzString.ToCharArray();

                for (int j = 0; j < noWChar.Length; j++)
                {
                    // 中文字符
                    if (regex.IsMatch(noWChar[j].ToString()))
                    {
                        array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                        i1 = (short)(array[0]);
                        i2 = (short)(array[1]);
                        chrAsc = i1 * 256 + i2 - 65536;
                        if (chrAsc > 0 && chrAsc < 160)
                        {
                            pyString += noWChar[j];
                        }
                        else
                        {
                            // 修正部分文字
                            if (chrAsc == -9254) // 修正“圳”字
                                pyString += "Zhen";
                            else
                            {
                                for (int i = (pyValue.Length - 1); i >= 0; i--)
                                {
                                    if (pyValue[i] <= chrAsc)
                                    {
                                        pyString += pyName[i];
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    // 非中文字符
                    else
                    {
                        pyString += noWChar[j].ToString();
                    }
                }
                return pyString;
            }
            #endregion

            #region 把汉字转换成拼音(全拼) 用空格间隔
            /// <summary>
            /// 把汉字转换成拼音(全拼) 用空格间隔
            /// </summary>
            /// <param name="hzString"></param>
            /// <returns></returns>
            public static string ConvertWithBlank(string hzString)
            {
                // 匹配中文字符
                Regex regex = new Regex("^[\u4e00-\u9fa5]$");
                byte[] array = new byte[2];
                string pyString = "";
                int chrAsc = 0;
                int i1 = 0;
                int i2 = 0;
                char[] noWChar = hzString.ToCharArray();

                for (int j = 0; j < noWChar.Length; j++)
                {
                    // 中文字符
                    if (regex.IsMatch(noWChar[j].ToString()))
                    {
                        array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                        i1 = (short)(array[0]);
                        i2 = (short)(array[1]);
                        chrAsc = i1 * 256 + i2 - 65536;
                        if (chrAsc > 0 && chrAsc < 160)
                        {
                            pyString = pyString + " " + noWChar[j];
                        }
                        else
                        {
                            // 修正部分文字
                            if (chrAsc == -9254) // 修正“圳”字
                                pyString = pyString + " " + "Zhen";
                            else
                            {
                                for (int i = (pyValue.Length - 1); i >= 0; i--)
                                {
                                    if (pyValue[i] <= chrAsc)
                                    {
                                        pyString = pyString + " " + pyName[i];
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    // 非中文字符
                    else
                    {
                        pyString = pyString + " " + noWChar[j].ToString();
                    }
                }
                return pyString.Trim();
            }

            #endregion

            #region 汉字转拼音缩写 (字符串) (小写)
            /// <summary>
            /// 汉字转拼音缩写 (字符串) (小写)
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string GetSpellStringLower(string str)
            {
                string tempStr = "";
                foreach (char c in str)
                {
                    if ((int)c >= 33 && (int)c <= 126)
                    {
                        //字母和符号原样保留
                        tempStr += c.ToString();
                    }
                    else
                    {
                        //累加拼音声母
                        tempStr += GetSpellCharLower(c.ToString());
                    }

                }
                return tempStr;
            }

            #endregion

            #region 汉字转拼音缩写 (字符串) (小写) (空格间隔)
            /// <summary>
            /// 汉字转拼音缩写 (字符串) (小写) (空格间隔)
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string GetSpellStringLowerSplitWithBlank(string str)
            {
                string tempStr = "";
                foreach (char c in str)
                {
                    if ((int)c >= 33 && (int)c <= 126)
                    {
                        //字母和符号原样保留
                        tempStr = tempStr + " " + c.ToString();
                    }
                    else
                    {
                        //累加拼音声母
                        tempStr = tempStr + " " + GetSpellCharLower(c.ToString());
                    }

                }
                return tempStr.Trim();
            }

            #endregion

            #region 取单个字符的拼音声母(字符)(大写)
            /// <summary>
            /// 取单个字符的拼音声母(字符)(大写)
            /// </summary>
            /// <param name="c"></param>
            /// <returns></returns>
            public static string GetSpellCharSupper(string c)
            {
                byte[] array = new byte[2];
                array = System.Text.Encoding.Default.GetBytes(c);
                int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

                if (i < 0xB0A1) return c;
                if (i < 0xB0C5) return "A";
                if (i < 0xB2C1) return "B";
                if (i < 0xB4EE) return "C";
                if (i < 0xB6EA) return "D";
                if (i < 0xB7A2) return "E";
                if (i < 0xB8C1) return "F";
                if (i < 0xB9FE) return "G";
                if (i < 0xBBF7) return "H";
                if (i < 0xBFA6) return "J";
                if (i < 0xC0AC) return "K";
                if (i < 0xC2E8) return "L";
                if (i < 0xC4C3) return "M";
                if (i < 0xC5B6) return "N";
                if (i < 0xC5BE) return "O";
                if (i < 0xC6DA) return "P";
                if (i < 0xC8BB) return "Q";
                if (i < 0xC8F6) return "R";
                if (i < 0xCBFA) return "S";
                if (i < 0xCDDA) return "T";
                if (i < 0xCEF4) return "W";
                if (i < 0xD1B9) return "X";
                if (i < 0xD4D1) return "Y";
                if (i < 0xD7FA) return "Z";
                return c;
            }
            #endregion

            #region 取单个字符的拼音声母(字符)(小写)
            /// <summary>
            /// 取单个字符的拼音声母(字符)(小写)
            /// </summary>
            /// <param name="c"></param>
            /// <returns></returns>
            public static string GetSpellCharLower(string c)
            {
                byte[] array = new byte[2];
                array = System.Text.Encoding.Default.GetBytes(c);
                int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

                if (i < 0xB0A1) return c;
                if (i < 0xB0C5) return "a";
                if (i < 0xB2C1) return "b";
                if (i < 0xB4EE) return "c";
                if (i < 0xB6EA) return "d";
                if (i < 0xB7A2) return "e";
                if (i < 0xB8C1) return "f";
                if (i < 0xB9FE) return "g";
                if (i < 0xBBF7) return "h";
                if (i < 0xBFA6) return "j";
                if (i < 0xC0AC) return "k";
                if (i < 0xC2E8) return "l";
                if (i < 0xC4C3) return "m";
                if (i < 0xC5B6) return "n";
                if (i < 0xC5BE) return "o";
                if (i < 0xC6DA) return "p";
                if (i < 0xC8BB) return "q";
                if (i < 0xC8F6) return "r";
                if (i < 0xCBFA) return "s";
                if (i < 0xCDDA) return "t";
                if (i < 0xCEF4) return "w";
                if (i < 0xD1B9) return "x";
                if (i < 0xD4D1) return "y";
                if (i < 0xD7FA) return "z";
                return c;
            }



            #endregion


            #region 取首字母遇到非字母则返回其他
            /// <summary>
            /// 取首字母遇到非字母则返回其他
            /// </summary>
            /// <param name="inTxt">需要获取首字母的字符串</param>
            /// <returns></returns>
            public static string GetFirstLetter(string inTxt)
            {
                string outTxt = "";
                string firtstchar = Common.Globals.ChineseSpell.Convert(inTxt).ToUpper().Substring(0, 1);
                char fc = char.Parse(firtstchar);
                if (fc >= 'A' && fc <= 'Z')
                {
                    outTxt = firtstchar;
                }
                else
                {
                    outTxt = "其它";
                }
                return outTxt;
            }
            #endregion

        }
        #endregion

        #region unicode转中文
        /// <summary>
        /// unicode转中文
        /// </summary>
        /// <returns></returns>
        public static string unicode_1(string str)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("/", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符  
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }
            return outStr;

        }
        #endregion

        #region 裁剪图片
        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="x">右偏移像素</param>
        /// <param name="y">下偏移像素</param>
        /// <param name="width">实际截取宽度</param>
        /// <param name="height">实际截取高度</param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        public static string CutImage(int x, int y, int width, int height, string ImageUrl)
        {

            if (x < 0 || y < 0 || width <= 0 || height <= 0 || string.IsNullOrEmpty(ImageUrl))
            {
                return string.Empty;
            }
            try
            {
                System.Drawing.Image img = System.Drawing.Bitmap.FromFile(HttpContext.Current.Server.MapPath("/") + ImageUrl.Trim('/'));
                System.Drawing.Image simg = new System.Drawing.Bitmap(img, img.Width, img.Height);
                //System.Drawing.Image simg = new System.Drawing.Bitmap(img, 570, 320);
                System.Drawing.Image nimg = new System.Drawing.Bitmap(width, height);
                System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(nimg);
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量，低速度呈现平滑程度
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                graphic.Clear(System.Drawing.Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分
                graphic.DrawImage(simg, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                string FileName = Path.GetFileName(ImageUrl);
                FileName = new Random().Next(0, 999999) + "_" + FileName;
                string FolderName = Path.GetDirectoryName(ImageUrl).Trim('/') + "/" + FileName;
                string FullSavePath = HttpContext.Current.Server.MapPath("/") + FolderName;
                nimg.Save(FullSavePath);
                return FolderName.Replace("\\", "/");
            }
            catch (Exception ex)
            {

            }
            return string.Empty;

        }
        #endregion

    }
}
