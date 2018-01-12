using System;
using System.IO;
using System.Web;

namespace DealMvc
{
    /// <summary>
    /// Ajax 的摘要说明
    /// </summary>
    public class Ajax : IHttpHandler
    {
        #region 定义目录和初始

        private static Model.SiteUpLoad uploadinfo = Model.SiteUpLoad.GetModel(t => t.id != 0);

        /// <summary>
        /// 临时目录
        /// </summary>
        private static string temppath = "/" + uploadinfo.UploadFolder.Replace("/", "") + "/" + "temp" + "/";

        /// <summary>
        /// 保存文件目录
        /// </summary>
        private static string path = "/" + uploadinfo.UploadFolder.Replace("/", "") + "/" + "pic" + "/";

        public void ProcessRequest(HttpContext context)
        {
            //区分参数
            string AjaxSign = context.Request.QueryString["input"];
            //返回上次后的地址
            string overpath = "";
            //上传
            string result = fajaxUpLoad(context, out overpath);

            DealAjax(AjaxSign, overpath);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(result);
            HttpContext.Current.Response.End();
        }

        #endregion 定义目录和初始

        #region 修改部分

        /// <summary>
        /// 处理程序
        /// </summary>
        /// <param name="AjaxSign">区分参数</param>
        /// <param name="path">文件地址</param>
        private void DealAjax(string AjaxSign, string path)
        {
            if (AjaxSign == "member_logo")
            {
                //path存储到用户头像字段
                //Model.Member m_member = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
                //if (m_member != null && !m_member.IsNull && !string.IsNullOrEmpty(path))
                //{
                //    m_member.M_Avatar = path;        //MP_Avatar[Type=string] - 头像
                //    m_member.Update();
                //}
            }
        }

        #endregion 修改部分

        #region 处理程序

        private string fajaxUpLoad(HttpContext context, out string overpath)
        {
            overpath = "";
            if (!string.IsNullOrEmpty(context.Request["Filename"]) && !string.IsNullOrEmpty(context.Request["Upload"]))
            {
                DealMvc.Common.Globals.UpFileResult _UFR = DealMvc.Common.Globals.Upload("", temppath);
                string filepath = DealMvc.Common.Globals.GetRootUrl(_UFR.returnfilename.Count > 0 ? _UFR.returnfilename[0].ToString() : "");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(filepath);
                HttpContext.Current.Response.End();
            }
            if (!string.IsNullOrEmpty(context.Request["avatar1"]) && !string.IsNullOrEmpty(context.Request["avatar2"]) && !string.IsNullOrEmpty(context.Request["avatar3"]))
            {
                string result = string.Empty;

                //if (!(SaveAvatar("avatar1", uid) && SaveAvatar("avatar2", uid) && SaveAvatar("avatar3", uid)))
                if (!SaveAvatar("avatar1", path, out overpath))
                {//失败
                    result = "<?xml version=\"1.0\" ?><root><face success=\"0\"/></root>";
                }
                else
                {
                    result = "<?xml version=\"1.0\" ?><root><face success=\"1\"/></root>";
                }
                return result;
            }
            return null;
        }

        private byte[] FlashDataDecode(string s)
        {
            byte[] r = new byte[s.Length / 2];
            int l = s.Length;
            for (int i = 0; i < l; i = i + 2)
            {
                int k1 = ((int)s[i]) - 48;
                k1 -= k1 > 9 ? 7 : 0;
                int k2 = ((int)s[i + 1]) - 48;
                k2 -= k2 > 9 ? 7 : 0;
                r[i / 2] = (byte)(k1 << 4 | k2);
            }
            return r;
        }

        private bool SaveAvatar(string avatar, string path, out string overpath)
        {
            overpath = "";
            byte[] b = FlashDataDecode(HttpContext.Current.Request[avatar]);
            if (b.Length == 0)
                return false;
            string size = "";
            if (avatar == "avatar1")
                size = "large";
            else if (avatar == "avatar2")
                size = "medium";
            else
                size = "small";

            string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + DealMvc.Common.Net.MathRandom.RandomNumber(4).ToString() + "_{0}." + "jpg";
            filename = string.Format(filename, size);
            string AllFolderPath = DealMvc.Common.Globals.GetMapPath(path);
            if (!Directory.Exists(AllFolderPath))
                Directory.CreateDirectory(AllFolderPath);
            FileStream fs = new FileStream(AllFolderPath + filename, FileMode.Create);
            fs.Write(b, 0, b.Length);
            fs.Close();
            overpath = path + filename;
            return true;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion 处理程序
    }
}

/*
 *
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flash上次图片</title>
</head>
<body>
    <div>
        <script>
            function updateavatar()
            {
                //Flash完成后,回调函数
            }
        </script>
        <div>
            <embed height="253" width="540" type="application/x-shockwave-flash" allowscriptaccess="always" swliveconnect="true" menu="false" wmode="transparent" bgcolor="#ffffff" quality="high" name="mycamera" src="/App_Themes/Public/fla/camera.swf?nt=1&amp;inajax=1&amp;appid=1&amp;input=member_logo&amp;ucapi=<%=DealMvc.Common.Globals.GetRootUrl("/Ajax.ashx")%>"
                scale="exactfit">
        </div>
    </div>
</body>
</html>

 */