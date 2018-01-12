using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace ExceptionEx
{
    /// <summary>
    /// class name:ApplicationLog       
    /// name space:MyEventLog
    /// purpose:事件日志记录类，提供事件日志记录支持
    /// </summary>
    /// date:2009-11-19
    /// time:17:51
    /// author:ZKZX-XA025
    /// ----------------------------------------------------------------------------------------
    public class MyExceptionLog
    {
        private static object MyExceptionLog_obj = new object();

        private static string ErrorFolder = AppDomain.CurrentDomain.BaseDirectory + "errorlog";
        private static string ErrorFile;

        static MyExceptionLog()
        {
            ErrorFile = ErrorFolder + @"\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".Log";
            if (!Directory.Exists(ErrorFolder))
            {
                Directory.CreateDirectory(ErrorFolder);
            }
            if (!File.Exists(ErrorFile))
            {
                //异常日志不存在创建
                //StreamWriter write = File.CreateText(ErrorFile);
                //write.WriteLine("----------异常日志----------");
                //write.Close();
                WriteFile("----------异常日志----------");
            }
        }
        /// <summary>
        /// 实际事件日志写入方法
        /// </summary>
        /// <param name="level">TraceLevel 要记录信息的级别</param>
        /// <param name="messageText">String 要记录的文本</param>
        /// date:2009-11-19
        /// time:17:34
        /// author:ZKZX-XA025
        /// ----------------------------------------------------------------------------------------
        public static void WriteLog(System.Web.Mvc.Controller _MvcController, Exception ex)
        {
            string _method = _MvcController.Url.ToString(); //_Page.Request.Url.ToString();

            string C_Z = "";
            try
            {
                string ControllerName = _MvcController.ControllerContext.RouteData.Values["controller"].ToString().Trim().ToLower();
                string ActionName = _MvcController.ControllerContext.RouteData.Values["action"].ToString().Trim().ToLower();
                C_Z = string.Format("[Controller:{0}][Action:{1}]", ControllerName, ActionName);
            }
            catch { }

            try
            {

                Type extype = ex.GetType();

                string pathAndLine = "";
                try
                {
                    pathAndLine = ex.StackTrace.ToString();
                }
                catch { }

                switch (extype.Name)
                {
                    case "MyExceptionMessageBox":
                        //弹出消息框

                        //Deal.JavaScript.JavaScript_Programe(_Page, "$(\"body\").showMessage(\"" + msg + "\");");
                        //Deal.JavaScript.JavaScript_Alert(_Page, msg);

                        AlertMessage(_MvcController, ex.Message.ToString()); return;
                        break;
                    case "ExceptionRegister":
                        DefinedExceptionWriteLog(C_Z + ex.Message, ex.InnerException.Message, (ex as ExceptionRegister).Method, (ex as ExceptionRegister).Rank, (ex as ExceptionRegister).ExType, ex.StackTrace);
                        break;
                    case "ExceptionIniFile":
                        DefinedExceptionWriteLog(C_Z + ex.Message, ex.InnerException.Message, (ex as ExceptionRegister).Method, (ex as ExceptionIniFile).Rank, (ex as ExceptionIniFile).ExType, ex.StackTrace);
                        break;
                    case "ExceptionXml":
                        DefinedExceptionWriteLog(C_Z + ex.Message, ex.InnerException.Message, (ex as ExceptionRegister).Method, (ex as ExceptionXml).Rank, (ex as ExceptionXml).ExType, ex.StackTrace);
                        break;
                    case "ExceptionMe":
                        DefinedExceptionWriteLog(C_Z + ex.Message, ex.InnerException.Message, (ex as ExceptionRegister).Method, (ex as ExceptionMe).Rank, (ex as ExceptionMe).ExType, ex.StackTrace);
                        break;
                    case "ArgumentException":
                        SystemExceptionWriteLog("ZKZX-10001", C_Z + ex.Message, _method, "严重", "参数异常", ex.StackTrace);
                        break;
                    default:
                        SystemExceptionWriteLog("ZKZX-00000", C_Z + ex.Message, _method, "严重", "未知异常", ex.StackTrace);
                        break;
                }
                AlertMessage(_MvcController, "系统错误,请稍后重试"); return;
            }
            catch
            {

            }
        }


        /// <summary>
        /// 自定义异常写入 Defineds the exception write log.
        /// </summary>
        /// <param name="message">String</param>
        /// <param name="innertext">String</param>
        /// <param name="source">String</param>
        /// <param name="rank">ExRank</param>
        /// <param name="extype">ExceptionType</param>
        /// date:2009-11-20
        /// time:10:41
        /// author:ZKZX-XA025
        /// ----------------------------------------------------------------------------------------
        private static void DefinedExceptionWriteLog(string message, string innertext, string source, ExRank rank, ExceptionType extype, string UrlPathAndLine)
        {
            string strrank = null;
            switch (rank)
            {
                case ExRank.Serious:
                    strrank = "严重";
                    break;
                case ExRank.Emergency:
                    strrank = "紧急";
                    break;
                case ExRank.General:
                    strrank = "一般";
                    break;
                case ExRank.Warning:
                    strrank = "提示";
                    break;
                default:
                    strrank = "提示警告";
                    break;
            }

            string strextype = null;
            switch (extype)
            {
                case ExceptionType.System:
                    strextype = "系统异常";
                    break;
                case ExceptionType.Register:
                    strextype = "注册表异常";
                    break;
                case ExceptionType.IO:
                    strextype = "文件操作异常";
                    break;
                case ExceptionType.None:
                    strextype = "未知异常";
                    break;
                default:
                    strextype = "系统异常";
                    break;
            }
            WriteFile("[" + DateTime.Now.ToString() + "]" + message + "     " + source + "     " + innertext + "[" + UrlPathAndLine + "]" + "     " + strrank + "     " + strextype);
        }

        /// <summary>
        /// 系统异常写入日志
        /// </summary>
        /// <param name="message">String</param>
        /// <param name="innertext">String</param>
        /// <param name="source">String</param>
        /// <param name="rank">String</param>
        /// <param name="type">String</param>
        /// <param name="UrlPathAndLine">String</param>
        /// ----------------------------------------------------------------------------------------
        private static void SystemExceptionWriteLog(string message, string innertext, string source, string rank, string type, string UrlPathAndLine)
        {
            WriteFile("[" + DateTime.Now.ToString() + "]" + message + "     " + source + "     " + innertext + "[" + UrlPathAndLine + "]" + "     " + rank + "     " + type);
        }

        /// <summary>
        /// 写入异常信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void AddLogError(string message)
        {
            WriteFile("[" + DateTime.Now.ToString() + "]" + message);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        private static void WriteFile(string text)
        {
            lock (MyExceptionLog_obj)
            {
                StreamWriter writer = new StreamWriter(ErrorFile, true);
                writer.WriteLine(text);
                writer.Flush();
                writer.Close();
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="_MvcController">MvcController</param>
        /// <param name="msg">msg</param>
        public static void AlertMessage(System.Web.Mvc.Controller _MvcController, string msg)
        {
            AlertMessage(_MvcController, msg, false);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="_MvcController">MvcController</param>
        /// <param name="msg">msg</param>
        /// <param name="IsRedirect">是否跳转到其它页面</param>
        public static void AlertMessage(System.Web.Mvc.Controller _MvcController, string msg, bool IsRedirect)
        {

            msg = msg.Replace("\"", "");
            if (!IsRedirect)
            {
                _MvcController.ViewData["AlertMessage"] = msg;
            }
            else
            {
                _MvcController.TempData["AlertMessage"] = msg;
            }
        }
    }
}