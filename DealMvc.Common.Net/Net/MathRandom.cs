using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 随机数类
    /// </summary>
    public class MathRandom
    {
        /// <summary>
        /// 返回在min和max之间的随机数(包括min和max)
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns>随机数</returns>
        public static int RandomNumber(int min, int max)
        {
            Random _Random = new Random();
            //线程休眠20毫秒
            System.Threading.Thread.Sleep(20);
            return _Random.Next(min, max + 1);
        }

        /// <summary>
        /// 返回length位数的随机数(0-9)
        /// </summary>
        /// <param name="length">位数</param>
        /// <returns>随机数</returns>
        public static int RandomNumber(int length)
        {
            Random _Random = new Random();
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    output.Append(_Random.Next(1, 10).ToString());
                }
                else
                {
                    output.Append(_Random.Next(0, 10).ToString());
                }
            }
            return int.Parse(output.ToString());
        }

        /// <summary>
        /// 返回日期的数字形式
        /// </summary>
        /// <param name="_Date">是否输出日期(年,月,日)部分</param>
        /// <param name="_Time">是否输出时间(时,分,秒,毫秒)部分</param>
        /// <returns>随机数</returns>
        public static long RandomDateTime(bool _Date, bool _Time)
        {
            if ((!_Date) && (!_Time))
            {
                throw new Exception("参数_Date和_Time必须有一个为true");
            }
            else
            {
                StringBuilder output = new StringBuilder();
                if (_Date)
                {
                    output.Append(DateTime.Now.Year.ToString());
                    output.Append(DateTime.Now.Month.ToString());
                    output.Append(DateTime.Now.Day.ToString());
                }
                if (_Time)
                {
                    output.Append(DateTime.Now.Hour.ToString());
                    output.Append(DateTime.Now.Minute.ToString());
                    output.Append(DateTime.Now.Second.ToString());
                    output.Append(DateTime.Now.Millisecond.ToString());
                }
                return long.Parse(output.ToString());
            }

        }

        #region 验证码

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="RandomLength">验证码的位数</param>
        public static void RandomPic(System.Web.UI.Page _Page, int RandomLength)
        {
            //随机数
            int Num = MathRandom.RandomNumber(RandomLength);
            _Page.Session["WRandomNum"] = Num;

            //创建Bmp位图
            Bitmap bitMapImage = new Bitmap(RandomLength * 13 + 16, 24);

            Graphics graphicImage = Graphics.FromImage(bitMapImage);

            //设置画笔的输出质量
            graphicImage.SmoothingMode = SmoothingMode.HighSpeed;

            //添加文本字符串
            graphicImage.DrawString(Num.ToString(), new Font("黑体", 18, FontStyle.Bold), new SolidBrush(Color.Red), new Point(2, 0));

            //设置图像输出的格式
            _Page.Response.ContentType = "image/jpeg";
            //清空输出数据
            _Page.Response.Clear();
            //保存数据流
            bitMapImage.Save(_Page.Response.OutputStream, ImageFormat.Jpeg);

            //释放占用的资源
            graphicImage.Dispose();
            bitMapImage.Dispose();
        }

        /// <summary>
        /// 判断输入验证码和图片验证码是否相等,必须先用RandomPic生成验证码图片
        /// </summary>
        /// <param name="_Page">Page</param>
        /// <param name="MyNumber">输入的验证码</param>
        /// <returns>不相等返回true,相等返回false</returns>
        public static bool IsRandomWrong(System.Web.UI.Page _Page, string MyNumber)
        {
            if (_Page.Session["WRandomNum"] != null)
            {
                if (MyNumber.Trim().Equals(_Page.Session["WRandomNum"].ToString()))
                {
                    //相等
                    return false;
                }
                else
                {
                    //不相等
                    return true;
                }
            }
            else
            {
                //异常
                return false;
            }
        }

        #endregion
    }
}
