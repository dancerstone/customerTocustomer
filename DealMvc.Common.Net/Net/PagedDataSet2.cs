using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 分页程序2
    /// </summary>
    public class PagedDataSet2
    {
        #region 属性

        /// <summary>
        /// 标识符
        /// </summary>
        private string _SessionString = null;

        /// <summary>
        /// 隐藏域
        /// </summary>
        private System.Web.UI.WebControls.HiddenField M_HiddenField = null;

        /// <summary>
        /// Ds数据源
        /// </summary>
        private System.Data.DataSet __DS = null;

        /// <summary>
        /// 数据绑定控件
        /// </summary>
        private System.Web.UI.WebControls.Repeater __Repeater = null;

        /// <summary>
        /// HtmlGenericControl(div控件)
        /// </summary>
        private System.Web.UI.HtmlControls.HtmlGenericControl __HtmlGenericControl = null;

        /// <summary>
        /// 读取或设置每页显示的条数(默认为10条)
        /// </summary>
        int _PageSize = 10;
        /// <summary>
        /// 读取或设置每页显示的条数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }

        /// <summary>
        /// 读取总页数
        /// </summary>
        private int _PageCount;
        /// <summary>
        /// 读取总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _PageCount;
            }
        }

        /// <summary>
        /// 读取当前页数
        /// </summary>
        private int _PageIndex = 0;
        /// <summary>
        /// 读取当前页数
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
        }
        /// <summary>
        /// 分页按钮颜色
        /// </summary>
        private string _ButtonColor = "#FFFFFF";

        /// <summary>
        /// 设置分页按钮颜色
        /// </summary>
        public string ButtonColor
        {
            set
            {
                _ButtonColor = value;
            }
        }
        /// <summary>
        /// 鼠标移动到按钮上按钮的颜色
        /// </summary>
        private string _ButtonNowColor = "#DFDFDF";

        /// <summary>
        /// 设置鼠标移动到按钮上按钮的颜色
        /// </summary>
        public string ButtonNowColor
        {
            set
            {
                _ButtonNowColor = value;
            }
        }
        /// <summary>
        /// 分页按钮边框颜色
        /// </summary>
        private string _ButtonBorderColor = "#666666";

        /// <summary>
        /// 设置分页按钮边框颜色
        /// </summary>
        public string ButtonBorderColor
        {
            set
            {
                _ButtonBorderColor = value;
            }
        }
        /// <summary>
        /// 分页按钮字体颜色
        /// </summary>
        private string _ButtonFontColor = "#626262";

        /// <summary>
        /// 设置分页按钮字体颜色
        /// </summary>
        public string ButtonFontColor
        {
            set
            {
                _ButtonFontColor = value;
            }
        }
        /// <summary>
        /// 分页按钮之间的距离
        /// </summary>
        private string _ButtonMargin = "3px";

        /// <summary>
        /// 设置分页按钮之间的距离
        /// </summary>
        public string ButtonMargin
        {
            set
            {
                _ButtonMargin = value;
            }
        }

        /// <summary>
        /// 设置按钮为图片
        /// </summary>
        private ArrayList _ButtonImageAttributes = new ArrayList();
        /// <summary>
        /// 设置按钮图片(ArrayList有四个值: 1为首页,2为上一页,3为下一页,4为末页)  例子 url|width|height, url|width|height, url|width|height, url|width|height
        /// </summary>
        public ArrayList ButtonImageAttributes
        {
            set
            {
                if (value.Count == 4)
                {
                    _ButtonImageAttributes = value;
                }
                else
                {
                    throw new Exception("ButtonImageAttributes 元素个数不正确");
                }
            }
        }

        /// <summary>
        /// 是否设置鼠标放上去背景图片移动到底部
        /// </summary>
        private bool _ButtonImageByMouseChange = false;
        /// <summary>
        /// 是否设置鼠标放上去背景图片移动到底部
        /// </summary>
        public bool ButtonImageByMouseChange
        {
            set
            {
                _ButtonImageByMouseChange = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedDataSet2() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_pagesize">每页显示的页数</param>
        public PagedDataSet2(int _pagesize)
        {
            _PageSize = _pagesize;
        }

        #endregion

        #region 分页方法

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="_Page">Page类</param>
        /// <param name="_DS">dataset数据源</param>
        /// <param name="_Repeater">数据绑定控件 Repeater 或者 DataList</param>
        /// <param name="_HtmlGenericControl">Div 加 runat='server'</param>
        /// <param name="_HiddenField">隐藏域</param>
        public void Paged(System.Web.UI.Page _Page, System.Data.DataSet _DS, System.Web.UI.WebControls.Repeater _Repeater, System.Web.UI.HtmlControls.HtmlGenericControl _HtmlGenericControl, System.Web.UI.WebControls.HiddenField _HiddenField)
        {
            //赋值
            __DS = _DS;
            __Repeater = _Repeater;
            __HtmlGenericControl = _HtmlGenericControl;
            _SessionString = _HiddenField.ID.ToString();
            M_HiddenField = _HiddenField;

            if (M_HiddenField.Value == String.Empty)
            {
                M_HiddenField.Value = "0";
            }

            //执行分页
            DoPage(_Page, __DS, __Repeater);
        }

        /// <summary>
        /// 用PagedDataSource实现分页
        /// </summary>
        /// <param name="_Page"></param>
        /// <param name="_DS"></param>
        /// <param name="_Repeater"></param>
        private void DoPage(System.Web.UI.Page _Page, System.Data.DataSet _DS, System.Web.UI.WebControls.Repeater _Repeater)
        {
            int S_index = int.Parse(M_HiddenField.Value.ToString());

            System.Web.UI.WebControls.PagedDataSource _PDS = new System.Web.UI.WebControls.PagedDataSource();
            _PDS.DataSource = _DS.Tables[0].DefaultView;
            _PDS.AllowPaging = true;

            _PDS.PageSize = _PageSize;
            _PageCount = _PDS.PageCount;

            if (S_index <= 0)
            {
                _PageIndex = 1;
            }
            else if (S_index >= _PageCount - 1)
            {
                _PageIndex = _PageCount;
            }
            else
            {
                _PageIndex = S_index + 1;
            }

            //清空
            __HtmlGenericControl.Controls.Clear();

            //创建四个按钮
            CreateFirstButton();
            CreatePrevButton();
            CreateNextButton();
            CreateLastButton();

            //创建分页数据
            string sumNum = _DS.Tables[0].Rows.Count.ToString();
            CreateShuju(_PageIndex.ToString(), _PageCount.ToString(), sumNum);

            _PDS.CurrentPageIndex = _PageIndex - 1;
            M_HiddenField.Value = Convert.ToString(_PageIndex - 1);
            //((System.Web.UI.WebControls.HiddenField)_Page.FindControl(_SessionString)).Value = M_HiddenField.Value;
            ((System.Web.UI.WebControls.HiddenField)_Repeater.Parent.FindControl(_SessionString)).Value = M_HiddenField.Value;

            _Repeater.DataSource = _PDS;
            _Repeater.DataBind();
        }

        #endregion

        #region 按钮
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Button"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        private System.Web.UI.WebControls.Button ImageButton(System.Web.UI.WebControls.Button _Button, int S)
        {
            if (_ButtonImageAttributes.Count != 0)
            {
                string outString = _ButtonImageAttributes[S - 1].ToString();
                string[] outStringArr = outString.Split(new char[] { '|' });
                _Button.Text = "";
                _Button.BorderWidth = new System.Web.UI.WebControls.Unit(0);
                _Button.Attributes.CssStyle.Add("border", "none");
                _Button.Attributes.CssStyle.Add("background", "url(" + outStringArr[0].ToString() + ")");
                _Button.Attributes.CssStyle.Add("background-repeat", "no-repeat");
                _Button.Attributes.CssStyle.Add("width", outStringArr[1].ToString());
                _Button.Attributes.CssStyle.Add("height", outStringArr[2].ToString());
                if (_ButtonImageByMouseChange)
                {
                    _Button.Attributes.Add("onmouseover", "this.style.backgroundPosition='bottom';");
                    _Button.Attributes.Add("onmouseout", "this.style.backgroundPosition='top';");
                }
            }
            else
            {
                _Button.Attributes.Add("onmouseover", "this.style.background='" + _ButtonNowColor + "';");
                _Button.Attributes.Add("onmouseout", "this.style.background='" + _ButtonColor + "';");
            }
            return _Button;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private System.Web.UI.WebControls.Button CreateButton()
        {
            System.Web.UI.WebControls.Button _Button = new System.Web.UI.WebControls.Button();
            if (!_ButtonColor.Equals(String.Empty))
            {
                try
                {
                    _Button.BackColor = System.Drawing.Color.FromName(_ButtonColor);
                }
                catch
                {
                    throw new Exception("颜色值不正确");
                }
            }
            _Button.BorderWidth = new System.Web.UI.WebControls.Unit(1);
            _Button.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            _Button.BorderColor = System.Drawing.Color.FromName(_ButtonBorderColor);
            _Button.Attributes.CssStyle.Add("margin-right", _ButtonMargin);
            _Button.Attributes.CssStyle.Add("color", _ButtonFontColor);
            _Button.Width = new System.Web.UI.WebControls.Unit(42);
            
            return _Button;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateFirstButton()
        {
            System.Web.UI.WebControls.Button _Button = CreateButton();
            _Button.ID = "FirstButton" + _SessionString;
            _Button.Text = "首页";
            _Button.ToolTip = "首页";
            _Button = ImageButton(_Button, 1);
            if (_PageIndex == 1)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "已是第一页";
            }
            _Button.Click += new EventHandler(_Button_Click1);
            __HtmlGenericControl.Controls.Add(_Button);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button_Click1(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button _Button = (System.Web.UI.WebControls.Button)sender;
            ((System.Web.UI.WebControls.HiddenField)_Button.Parent.FindControl(_SessionString)).Value = "0";
            DoPage(_Button.Page, __DS, __Repeater);
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreatePrevButton()
        {
            System.Web.UI.WebControls.Button _Button = CreateButton();
            _Button.ID = "PrevButton" + _SessionString;
            _Button.Text = "上一页";
            _Button.ToolTip = "上一页";
            _Button = ImageButton(_Button, 2);
            if (_PageIndex == 1)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "已是第一页";
            }
            _Button.Click += new EventHandler(_Button_Click2);
            __HtmlGenericControl.Controls.Add(_Button);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button_Click2(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button _Button = (System.Web.UI.WebControls.Button)sender;
            ((System.Web.UI.WebControls.HiddenField)_Button.Parent.FindControl(_SessionString)).Value = Convert.ToString(int.Parse(M_HiddenField.Value) - 1);
            DoPage(_Button.Page, __DS, __Repeater);
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateNextButton()
        {
            System.Web.UI.WebControls.Button _Button = CreateButton();
            _Button.ID = "NextButton" + _SessionString;
            _Button.Text = "下一页";
            _Button.ToolTip = "下一页";
            _Button = ImageButton(_Button, 3);
            if (_PageIndex == _PageCount)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "已是最后一页";
            }
            _Button.Click += new EventHandler(_Button_Click3);
            __HtmlGenericControl.Controls.Add(_Button);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button_Click3(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button _Button = (System.Web.UI.WebControls.Button)sender;
            ((System.Web.UI.WebControls.HiddenField)_Button.Parent.FindControl(_SessionString)).Value = Convert.ToString(int.Parse(M_HiddenField.Value) + 1);
            DoPage(_Button.Page, __DS, __Repeater);
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateLastButton()
        {
            System.Web.UI.WebControls.Button _Button = CreateButton();
            _Button.ID = "LastButton" + _SessionString;
            _Button.Text = "末页";
            _Button.ToolTip = "末页";
            _Button = ImageButton(_Button, 4);
            if (_PageIndex == _PageCount)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "已是最后一页";
            }
            _Button.Click += new EventHandler(_Button_Click4);
            __HtmlGenericControl.Controls.Add(_Button);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button_Click4(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button _Button = (System.Web.UI.WebControls.Button)sender;
            ((System.Web.UI.WebControls.HiddenField)_Button.Parent.FindControl(_SessionString)).Value = Convert.ToString(_PageCount - 1);
            DoPage(_Button.Page, __DS, __Repeater);
        }

        #endregion

        #region 创建显示数据

        /// <summary>
        ///   
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <param name="sumNum"></param>
        private void CreateShuju(string pageindex, string pagecount, string sumNum)
        {
            System.Web.UI.WebControls.Label _Label = new System.Web.UI.WebControls.Label();
            _Label.ID = "M_$NowPageNum" + _SessionString;
            _Label.Text = "当前第 " + pageindex + " 页/总共 " + pagecount + " 页" + "     共 " + sumNum + " 条数据";
            __HtmlGenericControl.Controls.Add(_Label);
        }

        #endregion
    }
}
