using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// ��ҳ����3
    /// </summary>
    public class PagedDataSet3
    {
        #region ����

        /// <summary>
        /// ��ʶ��
        /// </summary>
        private string _SessionString = null;

        /// <summary>
        /// ������
        /// </summary>
        private System.Web.UI.WebControls.HiddenField M_HiddenField = null;

        /// <summary>
        /// Ds����Դ
        /// </summary>
        private System.Data.DataSet __DS = null;

        /// <summary>
        /// ���ݰ󶨿ؼ�
        /// </summary>
        private System.Web.UI.WebControls.Repeater __Repeater = null;

        /// <summary>
        /// HtmlGenericControl(div�ؼ�)
        /// </summary>
        private System.Web.UI.HtmlControls.HtmlGenericControl __HtmlGenericControl = null;

        /// <summary>
        /// ��ȡ������ÿҳ��ʾ������(Ĭ��Ϊ10��)
        /// </summary>
        int _PageSize = 10;
        /// <summary>
        /// ��ȡ������ÿҳ��ʾ������
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
        /// ��ȡ��ҳ��
        /// </summary>
        private int _PageCount;
        /// <summary>
        /// ��ȡ��ҳ��
        /// </summary>
        public int PageCount
        {
            get
            {
                return _PageCount;
            }
        }

        /// <summary>
        /// ��ȡ��ǰҳ��
        /// </summary>
        private int _PageIndex = 0;
        /// <summary>
        /// ��ȡ��ǰҳ��
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
        }
        /// <summary>
        /// ��ҳ��ť��ɫ
        /// </summary>
        private string _ButtonColor = "#FFFFFF";

        /// <summary>
        /// ���÷�ҳ��ť��ɫ
        /// </summary>
        public string ButtonColor
        {
            set
            {
                _ButtonColor = value;
            }
        }
        /// <summary>
        /// ��ҳ��ť�߿���ɫ
        /// </summary>
        private string _ButtonBorderColor = "#666666";

        /// <summary>
        /// ���÷�ҳ��ť�߿���ɫ
        /// </summary>
        public string ButtonBorderColor
        {
            set
            {
                _ButtonBorderColor = value;
            }
        }

        /// <summary>
        /// ��ҳ��ť������ɫ
        /// </summary>
        private string _ButtonFontColor = "#626262";

        /// <summary>
        /// ���÷�ҳ��ť������ɫ
        /// </summary>
        public string ButtonFontColor
        {
            set
            {
                _ButtonFontColor = value;
            }
        }

        /// <summary>
        /// �滻��ҳģ��ġ�
        /// </summary>
        private string _ButtonIco = "��";

        /// <summary>
        /// �滻��ҳģ��ġ�
        /// </summary>
        public string ButtonIco
        {
            set
            {
                _ButtonIco = value;
            }
        }

        /// <summary>
        /// ����ƶ�����ť�ϰ�ť����ɫ
        /// </summary>
        private string _ButtonNowColor = "#DFDFDF";

        /// <summary>
        /// ����ƶ�����ť�ϰ�ť����ɫ
        /// </summary>
        public string ButtonNowColor
        {
            set
            {
                _ButtonNowColor = value;
            }
        }

        /// <summary>
        /// ��ҳ��ť֮��ľ���
        /// </summary>
        private string _ButtonMargin = "3px";

        /// <summary>
        /// ���÷�ҳ��ť֮��ľ���
        /// </summary>
        public string ButtonMargin
        {
            set
            {
                _ButtonMargin = value;
            }
        }

        /// <summary>
        /// ���ð�ťΪͼƬ
        /// </summary>
        private ArrayList _ButtonImageAttributes = new ArrayList();
        /// <summary>
        /// ���ð�ťͼƬ(ArrayList�����ֵ: 1Ϊ��ҳ,2Ϊ��һҳ,3Ϊ��һҳ,4Ϊĩҳ,5Ϊ����ҳ)  ���� url|width|height, url|width|height, url|width|height, url|width|height, url|width|height
        /// </summary>
        public ArrayList ButtonImageAttributes
        {
            set
            {
                if (value.Count == 5)
                {
                    _ButtonImageAttributes = value;
                }
                else
                {
                    throw new Exception("ButtonImageAttributes Ԫ�ظ�������ȷ");
                }
            }
        }

        /// <summary>
        /// �Ƿ�����������ȥ����ͼƬ�ƶ����ײ�
        /// </summary>
        private bool _ButtonImageByMouseChange = false;
        /// <summary>
        /// �Ƿ�����������ȥ����ͼƬ�ƶ����ײ�
        /// </summary>
        public bool ButtonImageByMouseChange
        {
            set
            {
                _ButtonImageByMouseChange = value;
            }
        }

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public PagedDataSet3() { }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="_pagesize">ÿҳ��ʾ��ҳ��</param>
        public PagedDataSet3(int _pagesize)
        {
            _PageSize = _pagesize;
        }

        #endregion

        #region ��ҳ����

        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="_Page">Page��</param>
        /// <param name="_DS">dataset����Դ</param>
        /// <param name="_Repeater">���ݰ󶨿ؼ� Repeater ���� DataList</param>
        /// <param name="_HtmlGenericControl">Div �� runat='server'</param>
        /// <param name="_HiddenField">������</param>
        public void Paged(System.Web.UI.Page _Page, System.Data.DataSet _DS, System.Web.UI.WebControls.Repeater _Repeater, System.Web.UI.HtmlControls.HtmlGenericControl _HtmlGenericControl, System.Web.UI.WebControls.HiddenField _HiddenField)
        {
            //��ֵ
            __DS = _DS;
            __Repeater = _Repeater;
            __HtmlGenericControl = _HtmlGenericControl;
            _SessionString = _HiddenField.ID.ToString();
            M_HiddenField = _HiddenField;

            if (M_HiddenField.Value == String.Empty)
            {
                M_HiddenField.Value = "0";
            }

            //ִ�з�ҳ
            DoPage(_Page, __DS, __Repeater);
        }

        /// <summary>
        /// ��PagedDataSourceʵ�ַ�ҳ
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

            //���
            __HtmlGenericControl.Controls.Clear();

            //�����ĸ���ťǰ����
            CreateFirstButton();
            CreatePrevButton();

            //������ҳ����
            string sumNum = _DS.Tables[0].Rows.Count.ToString();
            CreateShuju(_PageIndex.ToString(), _PageCount.ToString(), sumNum);

            //�����ĸ���ť������
            CreateNextButton();
            CreateLastButton();

            _PDS.CurrentPageIndex = _PageIndex - 1;
            M_HiddenField.Value = Convert.ToString(_PageIndex - 1);
            //((System.Web.UI.WebControls.HiddenField)_Page.FindControl(_SessionString)).Value = M_HiddenField.Value;
            ((System.Web.UI.WebControls.HiddenField)_Repeater.Parent.FindControl(_SessionString)).Value = M_HiddenField.Value;

            _Repeater.DataSource = _PDS;
            _Repeater.DataBind();
        }

        #endregion

        #region ��ť
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
                if (S != 5)
                {
                    _Button.Text = "";
                }
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
                    throw new Exception("��ɫֵ����ȷ");
                }
            }

            _Button.BorderWidth = new System.Web.UI.WebControls.Unit(1);
            _Button.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            _Button.BorderColor = System.Drawing.Color.FromName(_ButtonBorderColor);
            _Button.Attributes.CssStyle.Add("margin-right", _ButtonMargin);
            _Button.Attributes.CssStyle.Add("color", _ButtonFontColor);
            _Button.Width = new System.Web.UI.WebControls.Unit(22);

            return _Button;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateFirstButton()
        {
            System.Web.UI.WebControls.Button _Button = CreateButton();
            _Button.ID = "FirstButton" + _SessionString;
            _Button.Text = "|<<";
            _Button.ToolTip = "��ҳ";
            _Button = ImageButton(_Button, 1);
            if (_PageIndex == 1)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "���ǵ�һҳ";
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
            _Button.Text = "<";
            _Button.ToolTip = "��һҳ";
            _Button = ImageButton(_Button, 2);
            if (_PageIndex == 1)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "���ǵ�һҳ";
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
            _Button.Text = ">";
            _Button.ToolTip = "��һҳ";
            _Button = ImageButton(_Button, 3);
            if (_PageIndex == _PageCount)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "�������һҳ";
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
            _Button.Text = ">>|";
            _Button.ToolTip = "ĩҳ";
            _Button = ImageButton(_Button, 4);
            if (_PageIndex == _PageCount)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "�������һҳ";
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

        #region ������ʾ����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <param name="sumNum"></param>
        private void CreateShuju(string pageindex, string pagecount, string sumNum)
        {
            if (int.Parse(pagecount) > 10)
            {

                if ((_PageIndex > 5) && (_PageIndex < (_PageCount - 5)))
                {
                    //1,2ҳ
                    for (int s = 0; s < 2; s++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + s.ToString();
                        _Button.Text = Convert.ToString(s + 1);
                        _Button.ToolTip = "��" + Convert.ToString(s + 1) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, s);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }

                    System.Web.UI.WebControls.Label _Label1 = new System.Web.UI.WebControls.Label();
                    _Label1.ID = "M_SSS" + _SessionString;
                    _Label1.Text = "��";
                    __HtmlGenericControl.Controls.Add(_Label1);

                    for (int s = 1; s <= 3; s++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + Convert.ToString(_PageIndex - 4 + s - 1);
                        _Button.Text = Convert.ToString(_PageIndex - 4 + s);
                        _Button.ToolTip = "��" + Convert.ToString(_PageIndex - 4 + s) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, _PageIndex - 4 + s - 1);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }
                    System.Web.UI.WebControls.Button _ButtonNow = CreateButton();
                    _ButtonNow.ID = "M_Num" + _SessionString + Convert.ToString(_PageIndex - 1);
                    _ButtonNow.Text = Convert.ToString(_PageIndex);
                    _ButtonNow.ToolTip = "��" + Convert.ToString(_PageIndex) + "ҳ";
                    _ButtonNow = ImageButton(_ButtonNow, 5);
                    _ButtonNow.Click += new EventHandler(Num_Button_Click);
                    _ButtonNow = NowNumIndex(_ButtonNow, _PageIndex - 1);
                    __HtmlGenericControl.Controls.Add(_ButtonNow);
                    for (int s = 1; s <= 3; s++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + Convert.ToString(_PageIndex + s - 1);
                        _Button.Text = Convert.ToString(_PageIndex + s);
                        _Button.ToolTip = "��" + Convert.ToString(_PageIndex + s) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, _PageIndex + s - 1);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }
                    System.Web.UI.WebControls.Label _Label2 = new System.Web.UI.WebControls.Label();
                    _Label2.ID = "M_ZZZ" + _SessionString;
                    _Label2.Text = _ButtonIco;
                    __HtmlGenericControl.Controls.Add(_Label2);

                    //����ҳ
                    for (int u = int.Parse(pagecount) - 2; u < int.Parse(pagecount); u++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + u.ToString();
                        _Button.Text = Convert.ToString(u + 1);
                        _Button.ToolTip = "��" + Convert.ToString(u + 1) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, u);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }
                }
                else if ((_PageIndex <= 5) && (_PageIndex < (_PageCount - 5)))
                {
                    for (int s = 1; s <= 7; s++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + Convert.ToString(s - 1);
                        _Button.Text = s.ToString();
                        _Button.ToolTip = "��" + s.ToString() + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, s - 1);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }
                    System.Web.UI.WebControls.Label _Label2 = new System.Web.UI.WebControls.Label();
                    _Label2.ID = "M_ZZZ" + _SessionString;
                    _Label2.Text = _ButtonIco;
                    __HtmlGenericControl.Controls.Add(_Label2);
                    //����ҳ
                    for (int u = int.Parse(pagecount) - 2; u < int.Parse(pagecount); u++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + u.ToString();
                        _Button.Text = Convert.ToString(u + 1);
                        _Button.ToolTip = "��" + Convert.ToString(u + 1) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, u);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }
                }
                else if ((_PageIndex > 5) && (_PageIndex >= (_PageCount - 5)))
                {
                    //1,2ҳ
                    for (int s = 0; s < 2; s++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + s.ToString();
                        _Button.Text = Convert.ToString(s + 1);
                        _Button.ToolTip = "��" + Convert.ToString(s + 1) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, s);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }

                    System.Web.UI.WebControls.Label _Label1 = new System.Web.UI.WebControls.Label();
                    _Label1.ID = "M_SSS" + _SessionString;
                    _Label1.Text = _ButtonIco;
                    __HtmlGenericControl.Controls.Add(_Label1);

                    for (int s = 1; s <= 7; s++)
                    {
                        System.Web.UI.WebControls.Button _Button = CreateButton();
                        _Button.ID = "M_Num" + _SessionString + Convert.ToString(_PageCount - 7 + s - 1);
                        _Button.Text = Convert.ToString(_PageCount - 7 + s);
                        _Button.ToolTip = "��" + Convert.ToString(_PageCount - 7 + s) + "ҳ";
                        _Button = ImageButton(_Button, 5);
                        _Button.Click += new EventHandler(Num_Button_Click);
                        _Button = NowNumIndex(_Button, _PageCount - 7 + s - 1);
                        __HtmlGenericControl.Controls.Add(_Button);
                    }
                }


            }
            else
            {
                for (int i = 0; i < int.Parse(pagecount); i++)
                {
                    System.Web.UI.WebControls.Button _Button = CreateButton();
                    _Button.ID = "M_Num" + _SessionString + i.ToString();
                    _Button.Text = Convert.ToString(i + 1);
                    _Button.ToolTip = "��" + Convert.ToString(i + 1) + "ҳ";
                    _Button = ImageButton(_Button, 5);
                    _Button.Click += new EventHandler(Num_Button_Click);
                    _Button = NowNumIndex(_Button, i);
                    __HtmlGenericControl.Controls.Add(_Button);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Num_Button_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button _Button = (System.Web.UI.WebControls.Button)sender;

            int Num = int.Parse(_Button.ID.ToString().Replace("M_Num" + _SessionString, ""));

            ((System.Web.UI.WebControls.HiddenField)_Button.Parent.FindControl(_SessionString)).Value = Convert.ToString(Num);

            DoPage(_Button.Page, __DS, __Repeater);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Button"></param>
        /// <param name="Num"></param>
        private System.Web.UI.WebControls.Button NowNumIndex(System.Web.UI.WebControls.Button _Button, int Num)
        {
            if ((_PageIndex - 1) == Num)
            {
                _Button.Enabled = false;
                _Button.ToolTip = "��ǰҳ";
                _Button.BackColor = System.Drawing.Color.FromName(_ButtonNowColor);
                _Button.Attributes.CssStyle.Add("font-weight", "700");
            }
            return _Button;
        }

        #endregion
    }
}