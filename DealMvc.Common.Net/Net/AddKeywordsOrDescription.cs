using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// ��ҳ�ؼ���
    /// </summary>
    public enum WebCi
    {
        /// <summary>
        /// �ؼ���
        /// </summary>
        Keywords = 0,
        /// <summary>
        /// ������
        /// </summary>
        Description = 1
    }
      
    /// <summary>
    /// �����ҳ�ؼ��ֻ�����ҳ������
    /// </summary>
    public class AddKeywordsOrDescription
    {
        /// <summary>
        /// ת���ɹؼ��ʻ���������
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
                throw new Exception("ö��ֵ����ȷ");
            }
            else
            {
                return Style;
            }
        }

        /// <summary>
        /// ����ؼ��ʻ�������
        /// </summary>
        /// <param name="_HtmlHead">ҳ��HtmlHead����(Header)</param>
        /// <param name="_WebCi">�ؼ��ʻ�������</param>
        /// <param name="Words">��������</param>
        public static void Add(System.Web.UI.HtmlControls.HtmlHead _HtmlHead, WebCi _WebCi, string Words)
        {
            HtmlMeta HtmlMeta1 = new HtmlMeta();
            HtmlMeta1.Name = getS((int)_WebCi);
            HtmlMeta1.Content = Words;
            _HtmlHead.Controls.Add(HtmlMeta1);
        }
    }
}
