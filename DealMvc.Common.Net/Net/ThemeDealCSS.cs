using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// ����Ƥ��CSS
    /// </summary>
    public class ThemeDealCSS
    {
        private static StringBuilder _hasFile = new StringBuilder();

        /// <summary>
        /// ����Ƥ��ָ����CSS,�����ȫ�����˵�
        /// </summary>
        /// <param name="_HtmlHead">ҳ��HtmlHead����(Header)</param>
        /// <param name="CssNames">������css�ļ�,����Ļ���"|"�ָ�, ��׺��.css�ɼӿɲ���</param>
        public static void FilterCSS(System.Web.UI.HtmlControls.HtmlHead _HtmlHead, string CssNames)
        {
            _hasFile = new StringBuilder();
            ArrayList _ArrayList = new ArrayList();
            for (int i = 0; i < _HtmlHead.Controls.Count; i++)
            {
                if (_HtmlHead.Controls[i].ToString().ToLower().IndexOf("htmllink") > -1)
                {
                    System.Web.UI.HtmlControls.HtmlLink _HtmlLink = (System.Web.UI.HtmlControls.HtmlLink)_HtmlHead.Controls[i];
                    string HtmlLinkHref = _HtmlLink.Href.ToString();
                    if (!dealCssNames(HtmlLinkHref, CssNames))
                    {
                        _ArrayList.Add(i.ToString());
                    }
                }
            }
            for (int u = _ArrayList.Count - 1; u >= 0; u--)
            {
                _HtmlHead.Controls.RemoveAt(int.Parse(_ArrayList[u].ToString()));
            }
            _hasFile = new StringBuilder();
        }
        /// <summary>
        /// ���CssPath�Ƿ������CssNames����
        /// ��������true
        /// </summary>
        /// <param name="CssPath">css�ļ�·��</param>
        /// <param name="CssNames">css�ļ�����</param>
        /// <returns>��������true</returns>
        private static bool dealCssNames(string CssPath, string CssNames)
        {
            bool S = false;
            string[] FileNames = CssNames.Split(new char[] { '|' });
            for (int i = 0; i < FileNames.Length; i++)
            {
                string _CssName = FileNames[i].ToString();
                string _CssName2 = FileNames[i].ToString() + ".css";
                string CssPath1 = CssPath.Substring(CssPath.LastIndexOf("/") + 1);
                string CssPath2 = CssPath.Substring(CssPath.LastIndexOf(@"\") + 1);
                if ((CssPath1.Equals(_CssName) || CssPath2.Equals(_CssName) || CssPath1.Equals(_CssName2) || CssPath2.Equals(_CssName2)) && _hasFile.ToString().IndexOf(_CssName) < 0)
                {
                    S = true;
                    _hasFile.Append(_CssName);
                    break;
                }
            }
            return S;
        }
    }
}
