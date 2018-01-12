using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// �����ҳUrl��ַ�Ƿ��зǷ��ַ�
    /// </summary>
    public class CheckUrl
    {
        /// <summary>
        /// �����ҳUrl��ַ�Ƿ��зǷ��ַ�(��ע��),�зǷ��ַ�������ת����ҳ
        /// </summary>
        /// <param name="_Page">Page����</param>
        /// <returns></returns>
        public static void HasSQL(System.Web.UI.Page _Page)
        {
            //��ҳ��ַ·��
            string _URL = _Page.Request.Url.AbsoluteUri.ToString();
            if (checkSQL(_URL.ToLower()))
            {
                _Page.Response.Redirect("~/");
            }
        }

        /// <summary>
        /// ����ַ����Ƿ��зǷ��ַ�,true��ʾ�зǷ��ַ�
        /// </summary>
        /// <param name="_String"></param>
        /// <returns></returns>
        public static bool checkSQL(string _String)
        {
            bool HaveSQL = false;

            string SQL = Z_String.SQL_String3;

            string[] SQLArr = SQL.Split(new char[] { '|' });

            for (int I = 0; I < SQLArr.Length; I++)
            {
                if (_String.IndexOf(SQLArr[I].ToString()) > -1)
                {
                    HaveSQL = true;
                    break;
                }
            }
            return HaveSQL;
        }
    }
}
