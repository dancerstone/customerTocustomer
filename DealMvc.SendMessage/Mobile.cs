using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealMvc.Common.Config;
using System.Net.Mail;
using DealMvc.Common;
using System.Net;
using System.Web;

namespace DealMvc.SendMessage
{
    public class Message
    {
        private static DealMvc.SendMessage.SDK.Service webService = null;
        //短信帐号
        private static Common.Config.MobileAccount _MobileAccount = Common.Config.ConfigInfo<Common.Config.MobileAccount>.Instance();

        static Message()
        {
            if (webService == null)
            {
                webService = new DealMvc.SendMessage.SDK.Service();
                webService.Url = "http://www.82009668.com:888/SDK/Service.asmx";

            }
        }



        #region Email发送

        public static bool SendEmail(string subject, string content, string email)
        {
            try
            {
                Model.SiteEmail m_em = Model.SiteEmail.GetModel(t => t.id != 0);

                MailMessage mailmessage = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = m_em.Smtp;
                smtp.Port = Convert.ToInt32(m_em.Port);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(m_em.Email, DESEncrypt.Decrypt(m_em.Emailpwd));
                smtp.EnableSsl = false;
                mailmessage.To.Clear();
                mailmessage.To.Add(email);
                mailmessage.From = new MailAddress(m_em.Email, m_em.Emailname, System.Text.Encoding.UTF8);
                mailmessage.Subject = subject;
                mailmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailmessage.Body = content;
                mailmessage.IsBodyHtml = true;
                mailmessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailmessage.Priority = MailPriority.High;

                smtp.Send(mailmessage);
            }
            catch (Exception ex)
            {

                ExceptionEx.MyExceptionLog.AddLogError("发送邮件出错:" + ex.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region 发送短信
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phones"></param>
        /// <param name="content"></param>
        public void NewSendMessage(string phones, string content)
        {
            if (!string.IsNullOrEmpty(phones) && !string.IsNullOrEmpty(content))
            {
                Model.SiteMessage sm = Model.SiteMessage.GetModel(t => t.id != 0);
                WebClient client = new WebClient();
                String ct = HttpUtility.UrlEncode(content, Encoding.GetEncoding("gbk"));
                string Message = string.Format("http://221.122.112.136:8080/sms/mt.jsp?cpName={0}&cpPwd={1}&phones={2}&msg={3}", sm.UserName, sm.UserPwd, phones, ct);
                String ret = client.DownloadString(Message);
                Console.WriteLine(ret);
                Console.ReadLine();
            }
        }
        #endregion

    }
}
