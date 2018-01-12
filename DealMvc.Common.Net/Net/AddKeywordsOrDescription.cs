using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 网页关键词
    /// </summary>
    public enum WebCi
    {
        /// <summary>
        /// 关键词
        /// </summary>
        Keywords = 0,
        /// <summary>
        /// 描述词
        /// </summary>
        Description = 1
    }
      
    /// <summary>
    /// 添加网页关键字或者网页描述词
    /// </summary>
    public class AddKeywordsOrDescription
    {
        /// <summary>
        /// 转换成关键词或者描述词
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        private static string getS(int S)
        {
            string Style = String.Empty;
            switch (S)
            {
                case 0:
                    Style = "keywords";
                    break;
                case 1:
                    Style = "description";
                    break;
                default :
                    break;
            }
            if (Style == String.Empty)
            {
                throw new Exception("枚举值不正确");
            }
            else
            {
                return Style;
            }
        }

        /// <summary>
        /// 加入关键词或者描述
        /// </summary>
        /// <param name="_HtmlHead">页面HtmlHead对象(Header)</param>
        /// <param name="_WebCi">关键词或者描述</param>
        /// <param name="Words">具体内容</param>
        public static void Add(System.Web.UI.HtmlControls.HtmlHead _HtmlHead, WebCi _WebCi, string Words)
        {
            HtmlMeta HtmlMeta1 = new HtmlMeta();
            HtmlMeta1.Name = getS((int)_WebCi);
            HtmlMeta1.Content = Words;
            _HtmlHead.Controls.Add(HtmlMeta1);
        }
    }
}
