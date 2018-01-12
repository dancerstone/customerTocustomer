using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Collections;
using System.Web.Mvc;
using System.Net;
using System.Drawing.Imaging;

namespace DealMvc.Common
{
    #region 图片截取模式

    [Flags]
    public enum ImageSizeMode
    {
        /// <summary>
        /// 指定高宽缩放,可能变形
        /// </summary>
        WH = 0,

        /// <summary>
        /// 指定宽度,高度按照比例缩放
        /// </summary>
        W = 1,

        /// <summary>
        /// 指定高度,宽度按照等比例缩放
        /// </summary>
        H = 2,

        /// <summary>
        /// 截取指定大小,图片不会变形
        /// </summary>
        Size = 3
    }

    #endregion

    /// <summary>
    /// CoustomerView扩展
    /// </summary>
    public static class CoustomerViewExtensions
    {
        #region 图片自动大小扩展

        /// <summary>
        /// 图片自动大小扩展,支持网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="source"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string ImageAutoSize(this UrlHelper url, int width, int height, string source, ImageSizeMode mode)
        {
            int _thumHeight = 0;
            //return url.Action("ImageAutoSize", "ImageAutoSize", new { w = width, h = height, s = source, i = (int)mode });
            return new ImageAutoSizeController().ImageAutoSize(source, width, height, (Int16?)mode, ref _thumHeight).Content.ToString2();
        }

        /// <summary>
        /// 图片自动大小扩展,支持网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ImageAutoSize(this UrlHelper url, int width, int height, string source)
        {
            int _thumHeight = 0;
            //return url.Action("ImageAutoSize", "ImageAutoSize", new { w = width, h = height, s = source, i = 1 });
            return new ImageAutoSizeController().ImageAutoSize(source, width, height, 1, ref _thumHeight).Content.ToString2();
        }

        #endregion

        #region 图片自动大小扩展2

        /// <summary>
        /// 图片自动大小扩展,支持网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="source"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string ImageAutoSize(this UrlHelper url, int width, int height, string source, ImageSizeMode mode, ref int _thumHeight)
        {
            //return url.Action("ImageAutoSize", "ImageAutoSize", new { w = width, h = height, s = source, i = (int)mode });
            return new ImageAutoSizeController().ImageAutoSize(source, width, height, (Int16?)mode, ref _thumHeight).Content.ToString2();
        }

        /// <summary>
        /// 图片自动大小扩展,支持网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ImageAutoSize(this UrlHelper url, int width, int height, string source, ref int _thumHeight)
        {
            //return url.Action("ImageAutoSize", "ImageAutoSize", new { w = width, h = height, s = source, i = 1 });
            return new ImageAutoSizeController().ImageAutoSize(source, width, height, 1, ref _thumHeight).Content.ToString2();
        }

        #endregion

    }

    /// <summary>
    /// 图片自动大小
    /// </summary>
    public class ImageAutoSizeController : Controller
    {
        #region 图片自动大小,支持网络图片
        /// <summary>
        /// 图片自动大小,支持网络图片
        /// </summary>
        /// <param name="s"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public ContentResult ImageAutoSize(string s, int? w, int? h, Int16? i, ref int _thumHeight)
        {
            //DateTime dt1 = DateTime.Now;

            string _s = s;
            string s2 = "";
            if (s.ToLower().IndexOf("http://") == 0)
            {
                string phyPath = "/UploadFolder/" + "NetworkPic" + "/";
                s2 = GetNetPic(phyPath, s);
                s = s2;
            }
            if (string.IsNullOrEmpty(s))
                return Content("");

            if (!i.HasValue)
                i = 1;

            if (!w.HasValue)
                w = 48;

            if (!h.HasValue)
                h = 48;

            try
            {

                string newname = "";
                if (s.ToLower().IndexOf("http://") != 0)
                {
                    s2 = s.Substring(0, 1) == "/" ? "" + s : "/" + s;

                    //s = Server.MapPath(s.Substring(0, 1) == "/" ? "~" + s : "~/" + s);
                    s = Common.Globals.GetMapPath(s.Substring(0, 1) == "/" ? "~" + s : "~/" + s);


                    if (!System.IO.File.Exists(s))
                        return Content("");

                    newname = GetNewName(s, w, h, i, ref  s2);

                    if (!string.IsNullOrEmpty(newname) && System.IO.File.Exists(newname))
                    //return File(newname, "image/jpeg");
                    {
                        _thumHeight = System.Drawing.Image.FromFile(newname).Height;
                        //DateTime dt2 = DateTime.Now;

                        //string ActionExcuteDetails_Log = string.Format("1111(ms)：总时间[{0}]" + s2, (dt2 - dt1).TotalMilliseconds);
                        //Common.ExceptionEx.MyExceptionLog.AddLogError(ActionExcuteDetails_Log);

                        return Content(s2);
                    }
                }

                System.Drawing.Image originalImage;

                if (s.ToLower().IndexOf("http://") == 0)
                {
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(s);
                    System.IO.Stream inStream = request.GetResponse().GetResponseStream();
                    originalImage = System.Drawing.Image.FromStream(inStream);
                }
                else { originalImage = System.Drawing.Image.FromFile(s); }

                int thumWidth = w.Value;     //缩略图的宽度
                int thumHeight = h.Value;    //缩略图的高度

                int x = 0;
                int y = 0;

                int originalWidth = originalImage.Width;    //原始图片的宽度
                int originalHeight = originalImage.Height;  //原始图片的高度

                switch (i)
                {
                    case (int)ImageSizeMode.WH:      //指定高宽缩放,可能变形
                        break;
                    case (int)ImageSizeMode.W:       //指定宽度,高度按照比例缩放
                        thumHeight = originalImage.Height * w.Value / originalImage.Width;
                        if (h.Value < thumHeight) return ImageAutoSize(_s, w, h, (int)ImageSizeMode.H, ref _thumHeight);

                        _thumHeight = thumHeight;

                        break;
                    case (int)ImageSizeMode.H:       //指定高度,宽度按照等比例缩放
                        thumWidth = originalImage.Width * h.Value / originalImage.Height;
                        if (w.Value < thumWidth) return ImageAutoSize(_s, w, h, (int)ImageSizeMode.W, ref _thumHeight);
                        break;
                    case (int)ImageSizeMode.Size:
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
                        {
                            originalHeight = originalImage.Height;
                            originalWidth = originalImage.Height * thumWidth / thumHeight;
                            y = 0;
                            x = (originalImage.Width - originalWidth) / 2;
                        }
                        else
                        {
                            originalWidth = originalImage.Width;
                            originalHeight = originalWidth * h.Value / thumWidth;
                            x = 0;
                            y = (originalImage.Height - originalHeight) / 2;
                        }
                        break;
                    default:
                        break;
                }

                //新建一个bmp图片
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);

                //新建一个画板
                System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量查值法
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量，低速度呈现平滑程度
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                graphic.Clear(System.Drawing.Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);

                //开始返回图片
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                ImageCodecInfo ici = this.getImageCoderInfo("image/png");

                System.Drawing.Imaging.Encoder ecd = System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters eptS = new EncoderParameters(1);

                EncoderParameter ept = new EncoderParameter(ecd, 80L);

                eptS.Param[0] = ept;

                if (s.ToLower().IndexOf("http://") != 0 && !string.IsNullOrEmpty(newname))
                {//本地图片,保存缩略图
                    try
                    { bitmap.Save(newname, ici, eptS); }
                    catch { }
                }

                originalImage.Dispose();
                bitmap.Dispose();
                graphic.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionEx.MyExceptionLog.AddLogError(s + "==" + ex.Message);
                s2 = "";
            }

            //return File(ms.GetBuffer(), "image/jpeg");
            //DateTime dt3 = DateTime.Now;

            //string ActionExcuteDetails_Log2 = string.Format("222(ms)：总时间[{0}]" + s2, (dt3 - dt1).TotalMilliseconds);
            //Common.ExceptionEx.MyExceptionLog.AddLogError(ActionExcuteDetails_Log2);

            return Content(s2);

        }


        /// <summary>   
        /// 获取图片编码类型信息   
        /// </summary>   
        /// <param name="coderType">编码类型</param>   
        /// <returns>ImageCodecInfo</returns>   
        private ImageCodecInfo getImageCoderInfo(string coderType)
        {
            ImageCodecInfo[] iciS = ImageCodecInfo.GetImageEncoders();

            ImageCodecInfo retIci = null;

            foreach (ImageCodecInfo ici in iciS)
            {
                if (ici.MimeType.Equals(coderType))
                {
                    retIci = ici;
                }
            }

            return retIci;
        }

        private string GetNewName(string oldname, int? w, int? h, Int16? i, ref string s2)
        {
            string newname = "";

            string Extension = oldname.JgetExtension();
            if (!string.IsNullOrEmpty(Extension))
            {
                string si = "";
                switch (i)
                {
                    case (int)ImageSizeMode.WH: si = "_" + w.ToString2() + "_" + h.ToString2();
                        break;
                    case (int)ImageSizeMode.W: si = "_w_" + w.ToString2();
                        break;
                    case (int)ImageSizeMode.H: si = "_h_" + h.ToString2();
                        break;
                    case (int)ImageSizeMode.Size: si = "-" + w.ToString2() + "-" + h.ToString2();
                        break;
                    default:
                        break;
                }
                if (Extension == oldname)
                {
                    Extension = "jpg";
                }
                newname = oldname.Replace("." + Extension, "") + si + "." + Extension;
                s2 = s2.Replace("." + Extension, "") + si + "." + Extension;

            }

            return newname;
        }

        public string GetNetPic(string phyPath, string url)
        {
            try
            {
                string AllFolderPath = System.Web.HttpContext.Current.Server.MapPath("~" + phyPath);
                string name_hz = url.Substring(url.LastIndexOf("/") + 1);
                if (!Directory.Exists(AllFolderPath))
                    Directory.CreateDirectory(AllFolderPath);
                //string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + Common.Net.MathRandom.RandomNumber(4).ToString() + name_hz;
                string filename = name_hz;
                string filepath = System.Web.HttpContext.Current.Server.MapPath("~" + phyPath + filename);
                if (!string.IsNullOrEmpty(filepath) && System.IO.File.Exists(filepath))
                { }
                else
                {
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream reader = response.GetResponseStream();
                    FileStream writer = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] buff = new byte[512];
                    int c = 0; //实际读取的字节数
                    while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, c);
                    }
                    writer.Close();
                    writer.Dispose();
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                }
                return phyPath + filename;
            }
            catch (Exception)
            {
                return url;
            }



        }
        #endregion
    }
}
