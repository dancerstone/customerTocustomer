using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 防止重复提交数据
    /// </summary>
    public class StopReback
    {
        /// <summary>
        /// 初始化防止重复提交在Page_Load
        /// 初始化之前要在前台加入一个隐藏域web控件&lt;asp:HiddenField ID="HidSign" runat="server" /&gt;
        /// <param name="_Page">Page对象</param>
        /// <param name="_HiddenField">隐藏域对象ID</param>
        /// </summary>
        public static void Start_HidSign(System.Web.UI.Page _Page, System.Web.UI.WebControls.HiddenField _HiddenField)
        {
            string S_Sign = MathRandom.RandomDateTime(false, true).ToString() + MathRandom.RandomNumber(6);
            _HiddenField.Value = S_Sign;
            _Page.Session["WHidSign"] = S_Sign;
        }

        /// <summary>
        /// 判断不是重复提交
        /// </summary>
        /// <param name="_Page">Page对象</param>
        /// <param name="_HiddenField">隐藏域对象ID</param>
        /// <returns>不是重复提交返回true,重复提交返回false</returns>
        public static bool IsNotReback(System.Web.UI.Page _Page, System.Web.UI.WebControls.HiddenField _HiddenField)
        {
            bool M_S = false;

            if (_Page.Session["WHidSign"] == null)
            {
                StopReback.Start_HidSign(_Page, _HiddenField);
                //正常提交
                M_S = true;
            }
            else
            {
                string M_A = _HiddenField.Value.ToString();
                string M_B = _Page.Session["WHidSign"].ToString();
                
                if (M_A.Equals(M_B))
                {
                    //正常提交
                    M_S = true;
                }
                else
                {
                    //重复提交
                    M_S = false;
                }
            }

            StopReback.Start_HidSign(_Page, _HiddenField);

            return M_S;
        }

    }
}
