using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DealMvc
{
    #region 处理页面
    /// <summary>
    /// 作者：郭先军
    /// 日期：2013-01-24
    /// </summary>
    public static class HtmlItem
    {
        /// <summary>
        /// Radio TrueAndFalse 数据库字段是：Bit 页面编辑
        /// 范例如：<%=DealMvc.Common.HtmlString.HtmlInputRadioTrueAndFalse("MR_IsDefault", true, "是", "否")%>
        /// </summary>
        /// <param name="inputname">控件名称</param>
        /// <param name="checkedval">选中</param>
        /// <returns></returns>
        public static string Radio(string inputname, bool? checkedval)
        {
            return Radio(inputname, checkedval, "是", "否");
        }
        /// <summary>
        /// Radio TrueAndFalse 数据库字段是：Bit 页面编辑
        /// 范例如：<%=DealMvc.Common.HtmlString.Radio("MR_IsDefault", true, "是", "否")%>
        /// </summary>
        /// <param name="inputname">控件名称</param>
        /// <param name="checkedval">选中</param>
        /// <param name="TrueString">True文字</param>
        /// <param name="FalseString">False文字</param>
        /// <returns></returns>
        public static string Radio(string inputname, bool? checkedval, string TrueString, string FalseString)
        {
            StringBuilder sb_new = new StringBuilder();
            sb_new.AppendFormat("<input type=\"radio\" {1} value=\"True\" name=\"{0}\" id=\"{0}_1\">", inputname, checkedval ?? true ? "checked=\"checked\"" : "");
            sb_new.AppendFormat("<label for=\"{0}_1\" style='cursor: pointer;'  title='{1}'>{1}</label>&nbsp;&nbsp;", inputname, TrueString);
            sb_new.AppendFormat("<input type=\"radio\" {1} value=\"False\" name=\"{0}\" id=\"{0}_0\">", inputname, checkedval ?? false ? "" : "checked=\"checked\"");
            sb_new.AppendFormat("<label for=\"{0}_0\" style='cursor: pointer;' title='{1}'>{1}</label>", inputname, FalseString);
            return sb_new.ToString();
        }
    }
    #endregion

    #region 枚举转List帮助类
    /// <summary>
    /// 枚举转List帮助类
    /// </summary>
    public class GetEnumListHelper
    {
        /// <summary>
        /// 枚举转List帮助类
        /// </summary>
        public class EnumModel
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string Title { get; set; }
        }

        /// <summary>
        /// 获取枚举里面的值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public List<EnumModel> GetAll(Type type)
        {
            List<EnumModel> EnumModelList = new List<EnumModel>();
            foreach (var a in Enum.GetValues(type))
            {
                EnumModelList.Add(new EnumModel()
                {
                    id = (int)a,
                    Title = a.ToString()
                });
            }
            return EnumModelList;
        }
        //如：List<GetEnumListHelper.EnumModel> GoodsPay= new GetEnumListHelper().GetCkList(typeof(GoodsEnum.G_Pay_Mode_Accepted_Option),M_Goods.G_Pay_Mode_Accepted_Option);
        /// <summary>
        /// 获取枚举里 选择的值 List
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="CkVal">选择的值</param>
        /// <returns></returns>
        public List<EnumModel> GetCkList(Type type, string CkVal)
        {
            List<EnumModel> CkList = new List<EnumModel>();
            if (!string.IsNullOrEmpty(CkVal))
            {
                List<EnumModel> GetAllList = GetAll(type);
                string[] str_ck_val = CkVal.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in str_ck_val)
                {
                    EnumModel _model = GetAllList.Find(t => t.id == item.ToInt32());
                    if (_model == null)
                        continue;
                    if (CkList.Find(t => t.id == _model.id) != null)
                        continue;
                    CkList.Add(_model);
                }
            }
            return CkList;
        }

    }
    #endregion

    #region 控件
    /// <summary>
    /// 
    /// </summary>
    public class HtmlEnum
    {


        private static HtmlEnum _instance = null;
        public static HtmlEnum Instance()
        {
            if (_instance == null)
            {
                _instance = new HtmlEnum();
            }
            return _instance;
        }

        private HtmlEnum()
        { }
        #region 方法 Select

        /// <summary>
        /// 加载状态枚举到控件
        /// </summary>
        /// <param name="userControlName">控件name</param>
        /// <param name="t">枚举</param>
        public static string EnumToSelect(string userControlName, Type t)
        {

            string html = "<select name='{0}' id='{0}' >{1}</select>";
            string options = "<option value='{0}'>{1}</option>";
            string temps = "";

            foreach (string s in Enum.GetNames(t))
            {
                temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, temps);

        }
        /// <summary>
        /// 加载状态枚举到控件 扩展Select 无
        /// WeiKeHtmlHelper.EnumToSelect("A_Top", typeof(BssArticle.ETop), v_models.A_Top.ToString()
        /// </summary>
        /// <param name="userControlName">ControlName</param>
        /// <param name="t">枚举</param>
        /// <param name="selectValue">默认选中值</param>
        /// <returns></returns>
        public static string EnumToSelect(string userControlName, Type t, string selectValue)
        {

            string html = "<select name='{0}'  id='{0}'>{1}</select>";
            string options = "<option value='{0}'>{1}</option>";
            string options1 = "<option selected='selected' value='{0}'>{1}</option>";
            string temps = "";

            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, temps);

        }

        /// <summary>
        /// 加载状态枚举到控件 扩展Select 无
        /// WeiKeHtmlHelper.EnumToSelect("A_Top", typeof(BssArticle.ETop), v_models.A_Top.ToString()
        /// </summary>
        /// <param name="userControlName">ControlName</param>
        /// <param name="t">枚举</param>
        /// <param name="selectValue">默认选中值</param>
        /// <returns></returns>
        public static string EnumToSelectGetValue(string userControlName, Type t, string selectValue)
        {

            string html = "<select name='{0}'  id='{0}'>{1}</select>";
            string options = "<option value='{1}'>{1}</option>";
            string options1 = "<option selected='selected' value='{1}'>{1}</option>";
            string temps = "";

            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, temps);

        }

        /// <summary>
        /// 加载状态枚举到控件 扩展Select 第一默认值自定义
        /// 
        /// <%=WeiKeHtmlHelper.EnumToSelectDefault("EIncome", typeof(Member.EIncome), "0", "0", "请选择", "")%>
        /// </summary>
        /// <param name="userControlName">控件NAME和ID</param>
        /// <param name="t">有的枚举</param>
        /// <param name="selectValue">选中值</param>
        /// <param name="DefaultValue">第一默认值</param>
        /// <param name="DefaultText">第一默认文字</param>
        /// <returns></returns>
        public static string EnumToSelectDefault(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText, string ClassName)
        {

            string html = "<select name='{0}'  id='{0}' " + ClassName + ">{1}</select>";
            string DefaultOption = string.Format("<option value='{0}' selected='selected' >{1}</option>", DefaultValue, DefaultText);
            string options = "<option value='{0}'>{1}</option>";
            string options1 = "<option selected='selected' value='{0}'>{1}</option>";
            string temps = "";

            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, DefaultOption + temps);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControlName"></param>
        /// <param name="t"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        public static string EnumToSelectValueByString(string userControlName, Type t, string selectValue)
        {

            string html = "<select name='{0}'  id='{0}'>{1}</select>";
            string options = "<option value='{0}'>{0}</option>";
            string options1 = "<option selected='selected' value='{0}'>{0}</option>";
            string temps = "";

            foreach (string s in Enum.GetNames(t))
            {
                if (s == selectValue)
                    temps += string.Format(options1, s);
                else
                    temps += string.Format(options, s);
            }

            return string.Format(html, userControlName, temps);

        }



        #endregion

        #region 显示枚举里面值
        /// <summary>
        /// 显示枚举里面值 Checkbox值
        /// </summary>
        /// <param name="_enumtype">枚举typeof(DealMvc.Enumerate.Member.EBestPart)</param>
        /// <param name="ErrorString">错误显示</param>
        /// <param name="selectedval">值</param>
        /// <param name="SplitSign">页面</param>
        /// <param name="GSign">数据库</param>
        /// <returns>DealMvc.HtmlEnum.ShowEnumMethodCheckbox(typeof(DealMvc.Enumerate.Member.EBestPart), "", "1/2/17", "，", '/')</returns>
        public static string ShowEnumMethodCheckbox(Type _enumtype, string ErrorString, string selectedval, string SplitSign, char GSign)
        {
            string output = ErrorString;
            int icount = 0;
            string[] strzu = null;
            if (selectedval.Contains(GSign))
                strzu = selectedval.Split(GSign);
            foreach (string s in Enum.GetNames(_enumtype))
            {
                if (!selectedval.Contains(GSign))
                {
                    if (selectedval == ((int)Enum.Parse(_enumtype, s)).ToString())
                        output = s;
                }
                else
                {
                    if (strzu[icount] == ((int)Enum.Parse(_enumtype, s)).ToString())
                    {
                        output += s + SplitSign;
                        if (icount < strzu.Length - 1)
                            icount++;
                    }

                }
            }
            if (output.Contains(SplitSign))
                output = output.Substring(0, output.Length - 1);
            return output;
        }
        /// <summary>
        /// 显示枚举里面值 Checkbox值 "，", '/'
        /// </summary>
        /// <param name="_enumtype">枚举typeof(DealMvc.Enumerate.Member.EBestPart)</param>
        /// <param name="ErrorString">错误显示</param>
        /// <param name="selectedval">值</param>
        /// <returns>DealMvc.HtmlEnum.ShowEnumMethodCheckbox(typeof(DealMvc.Enumerate.Member.EBestPart), "","1/2/17")</returns>
        public static string ShowEnumMethodCheckbox(Type _enumtype, string ErrorString, string selectedval)
        {
            return ShowEnumMethodCheckbox(_enumtype, ErrorString, selectedval, "，", '/');
        }
        /// <summary>
        ///  显示枚举里面值 Option/Radio值
        /// </summary>
        /// <param name="_enumtype">枚举</param>
        /// <param name="ErrorString">需要显示字</param>
        /// <param name="selectedval">选中值</param>
        /// <returns>DealMvc.HtmlEnum.ShowEnumMethodB(typeof(DealMvc.Enumerate.Member.EBestPart), "CWU", "0");</returns>
        public static string ShowEnumMethodB(Type _enumtype, string ErrorString, string selectedval)
        {
            string output = ErrorString;
            foreach (string s in Enum.GetNames(_enumtype))
                if (selectedval == ((int)Enum.Parse(_enumtype, s)).ToString()) output = s;
            return output;
        }
        #endregion

        #region Radio
        public static string EnumToRadio(string userControlName, Type _Type, int? selectValue, string ClassName, bool IfReq)
        {
            return EnumToRadio(userControlName, _Type, selectValue.ToString2(), ClassName, IfReq);
        }
        public static string EnumToRadio(string userControlName, Type _Type, string selectValue, string ClassName, bool IfReq)
        {
            Type t = _Type;
            string ckstr = "checked='checked'";
            string reqstr = string.Format("<span name='{0}' class='Rreq'></span>", userControlName);
            StringBuilder Output = new StringBuilder();
            foreach (string s in Enum.GetNames(t))
            {
                //<input type="checkbox" value="western" id="check-6" name="genre"><label for="check-6" class="">Western</label>
                //
                Output.Append(string.Format("<input type='radio' value='{1}' id='{0}-{1}'  {3} {4} style='margin: 0px;padding: 0px;' name='{0}'><label for='{0}-{1}'>{2}&nbsp;</label>",
                     userControlName,
                    (int)Enum.Parse(t, s),
                    s,
                    ((int)Enum.Parse(t, s)).ToString() == selectValue ? ckstr : "",
                    ClassName.Length > 0 ? ClassName : ""
                    ));
            }
            return Output.ToString() + (IfReq ? reqstr : "");
        }
        public static string EnumToRadioCN(string userControlName, Type _Type, string selectValue, string ClassName, bool IfReq)
        {
            Type t = _Type;
            string ckstr = "checked='checked'";
            string reqstr = string.Format("<span name='{0}' class='Rreq'></span>", userControlName);
            StringBuilder Output = new StringBuilder();
            foreach (string s in Enum.GetNames(t))
            {
                //<input type="checkbox" value="western" id="check-6" name="genre"><label for="check-6" class="">Western</label>
                //
                Output.Append(string.Format("<input type='radio' value='{1}' id='{0}-{1}'  {3} {4} style='margin: 0px;padding: 0px;' name='{0}'><label for='{0}-{1}'>{2}&nbsp;</label>",
                     userControlName,
                    Enum.Parse(t, s),
                    s,
                    (Enum.Parse(t, s)).ToString() == selectValue ? ckstr : "",
                    ClassName.Length > 0 ? ClassName : ""
                    ));
            }
            return Output.ToString() + (IfReq ? reqstr : "");
        }
        public static string EnumToRadio(string userControlName, Type _Type, string selectValue)
        {
            return EnumToRadio(userControlName, _Type, selectValue, "", true);
        }
        #endregion

        #region Checkbox


        public static string EnumToCheckbox(string userControlName, Type _Type, string selectValue, string ClassName, char GSign)
        {
            Type t = _Type;
            string ckstr = "checked='checked'";
            string temp_html = "<input type='checkbox' value='{1}' id='{0}-{1}' name='{0}'  {3} {4}><label for='{0}-{1}' >{2}&nbsp;</label>";
            StringBuilder Output = new StringBuilder();

            int icount = 0;
            string[] strzu = null;
            if (selectValue.Contains(GSign))
                strzu = selectValue.Split(GSign);

            foreach (string s in Enum.GetNames(_Type))
            {
                if (!selectValue.Contains(GSign))
                {

                    Output.Append(string.Format(temp_html,
                    userControlName,
                    (int)Enum.Parse(t, s),
                    s,
                    ((int)Enum.Parse(t, s)).ToString() == selectValue ? ckstr : "",
                    ClassName.Length > 0 ? ClassName : ""));
                }
                else
                {

                    if (strzu[icount] == ((int)Enum.Parse(_Type, s)).ToString())
                    {
                        Output.Append(string.Format(temp_html,
                            userControlName,
                            (int)Enum.Parse(t, s),
                            s,
                            ckstr,
                            ClassName.Length > 0 ? ClassName : ""));

                        if (icount < strzu.Length - 1)
                            icount++;
                    }
                    else
                    {
                        Output.Append(string.Format(temp_html,
                            userControlName,
                            (int)Enum.Parse(t, s),
                            s,
                            "",
                            ClassName.Length > 0 ? ClassName : ""));
                    }

                }
            }
            return Output.ToString();
        }
        public static string EnumToCheckbox(string userControlName, Type _Type, string selectValue)
        {
            return EnumToCheckbox(userControlName, _Type, selectValue, "", ',');
        }
        #endregion

        #region Select 页面通用 --自定义
        public static string EnumToSD(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText, string ClassName)
        {

            string html = "<select name='{0}'  id='{0}' " + ClassName + ">{1}</select>";
            string DefaultOptionA = string.Format("<option value='{0}' selected='selected' >{1}</option>", DefaultValue, DefaultText);
            string DefaultOptionB = string.Format("<option value='{0}' >{1}</option>", DefaultValue, DefaultText);
            string options = "<option value='{0}'>{1}</option>";
            string options1 = "<option selected='selected' value='{0}'>{1}</option>";
            string temps = "";
            bool Ifrr = false;
            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                {
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                    Ifrr = true;
                }
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, (DefaultValue == "" && DefaultText != "" ? (Ifrr ? DefaultOptionB : DefaultOptionA) : "") + temps);

        }
        public static string EnumToSD(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText, string ClassName, string ClassValue)
        {

            string html = "<select name='{0}'  id='{0}' " + ClassName + "=\'" + ClassValue + "\'>{1}</select>";
            string DefaultOptionA = string.Format("<option value='{0}' selected='selected' >{1}</option>", DefaultValue, DefaultText);
            string DefaultOptionB = string.Format("<option value='{0}' >{1}</option>", DefaultValue, DefaultText);
            string options = "<option value='{0}'>{1}</option>";
            string options1 = "<option selected='selected' value='{0}'>{1}</option>";
            string temps = "";
            bool Ifrr = false;
            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                {
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                    Ifrr = true;
                }
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, (DefaultValue == "" && DefaultText != "" ? (Ifrr ? DefaultOptionB : DefaultOptionA) : "") + temps);

        }
        public static string EnumToSD(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText)
        {
            return EnumToSD(userControlName, t, selectValue, DefaultValue, DefaultText, "");
        }
        public static string EnumToSD(string userControlName, Type t, string selectValue)
        {
            return EnumToSD(userControlName, t, selectValue, "", "", "");
        }
        public static string EnumToSD(string userControlName, Type t, string selectValue, string ClassName)
        {
            return EnumToSD(userControlName, t, selectValue, "", "", ClassName);
        }

        public static string EnumToNameSD(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText, string ClassName)
        {

            string html = "<select name='{0}' " + ClassName + ">{1}</select>";
            string DefaultOptionA = string.Format("<option value='{0}' selected='selected' >{1}</option>", DefaultValue, DefaultText);
            string DefaultOptionB = string.Format("<option value='{0}' >{1}</option>", DefaultValue, DefaultText);
            string options = "<option value='{0}'>{1}</option>";
            string options1 = "<option selected='selected' value='{0}'>{1}</option>";
            string temps = "";
            bool Ifrr = false;
            foreach (string s in Enum.GetNames(t))
            {
                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                {
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                    Ifrr = true;
                }
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, (DefaultValue == "" && DefaultText != "" ? (Ifrr ? DefaultOptionB : DefaultOptionA) : "") + temps);

        }
        public static string EnumToNameSD(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText)
        {
            return EnumToNameSD(userControlName, t, selectValue, DefaultValue, DefaultText, "");
        }
        public static string EnumToNameSD(string userControlName, Type t, string selectValue)
        {
            return EnumToNameSD(userControlName, t, selectValue, "", "", "");
        }
        public static string EnumToNameSD(string userControlName, Type t, string selectValue, string ClassName)
        {
            return EnumToNameSD(userControlName, t, selectValue, "", "", ClassName);
        }
        #endregion

        #region New_Selected
        /// <summary>
        /// 中文 下拉框
        /// </summary>
        /// <param name="userControlName"></param>
        /// <param name="t"></param>
        /// <param name="selectValue"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="DefaultText"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static string EnumToSDCn(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText, string ClassName)
        {

            string html = "<select name='{0}'  id='{0}' " + ClassName + ">{1}</select>";
            string DefaultOptionA = string.Format("<option value='{0}' selected='selected' >{1}</option>", DefaultValue, DefaultText);
            string DefaultOptionB = string.Format("<option value='{0}' >{1}</option>", DefaultValue, DefaultText);
            string options = "<option value='{0}' >{0}</option>";
            string options1 = "<option selected='selected' value='{0}' >{0}</option>";
            string temps = "";
            bool Ifrr = false;
            string news = string.Empty;
            foreach (string s in Enum.GetNames(t))
            {
                news = s.EnumReplace().ToString2();
                if (news == selectValue.ToString2())
                {
                    temps += string.Format(options1, news);
                    Ifrr = true;
                }
                else
                    temps += string.Format(options, news);
            }

            return string.Format(html, userControlName, (DefaultValue == "" && DefaultText != "" ? (Ifrr ? DefaultOptionB : DefaultOptionA) : "") + temps);

        }
        /// <summary>
        /// 自定义下拉框属性值
        /// </summary>
        /// <param name="userControlName"></param>
        /// <param name="t"></param>
        /// <param name="selectValue"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="DefaultText"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static string EnumToSDCnZd(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText, string ClassName)
        {

            string html = "<select name='{0}'  id='{0}' " + ClassName + ">{1}</select>";
            string DefaultOptionA = string.Format("<option value='{0}' selected='selected' num='0' >{1}</option>", DefaultValue, DefaultText);
            string DefaultOptionB = string.Format("<option value='{0}' num='0' >{1}</option>", DefaultValue, DefaultText);
            string options = "<option value='{1}'  num='{0}'>{1}</option>";
            string options1 = "<option selected='selected' value='{1}'  num='{0}'>{1}</option>";
            string temps = "";
            bool Ifrr = false;

            foreach (string s in Enum.GetNames(t))
            {

                if (((int)Enum.Parse(t, s)).ToString() == selectValue)
                {
                    temps += string.Format(options1, ((int)Enum.Parse(t, s)).ToString(), s);
                    Ifrr = true;
                }
                else
                    temps += string.Format(options, ((int)Enum.Parse(t, s)).ToString(), s);
            }

            return string.Format(html, userControlName, (DefaultValue == "" && DefaultText != "" ? (Ifrr ? DefaultOptionB : DefaultOptionA) : "") + temps);

        }
        public static string EnumToSDCn(string userControlName, Type t, string selectValue, string DefaultValue, string DefaultText)
        {
            return EnumToSDCn(userControlName, t, selectValue, DefaultValue, DefaultText, "");
        }
        public static string EnumToSDCn(string userControlName, Type t, string selectValue)
        {
            return EnumToSDCn(userControlName, t, selectValue, "", "", "");
        }
        #endregion

        #region 多选框
        /// <summary>
        /// 多选框 中文
        /// </summary>
        /// <param name="userControlName"></param>
        /// <param name="_Type"></param>
        /// <param name="selectValue"></param>
        /// <param name="ClassName"></param>
        /// <param name="GSign"></param>
        /// <returns></returns>
        public static string EnumToCheckboxCN(string userControlName, Type _Type, string selectValue, string ClassName, char GSign)
        {
            Type t = _Type;
            string ckstr = "checked='checked'";
            string temp_html = "<input type='checkbox' value='{2}' id='{0}-{1}' name='{0}'  {3} {4}><label for='{0}-{1}' style='cursor:pointer;'>{2}&nbsp;</label>";
            StringBuilder Output = new StringBuilder();

            int icount = 0;
            string[] strzu = null;
            if (selectValue.Contains(GSign))
                strzu = selectValue.Split(GSign);

            foreach (string s in Enum.GetNames(_Type))
            {
                if (!selectValue.Contains(GSign))
                {

                    Output.Append(string.Format(temp_html,
                    userControlName,
                    (int)Enum.Parse(t, s),
                    s,
                    s == selectValue ? ckstr : "",
                    ClassName.Length > 0 ? ClassName : ""));
                }
                else
                {

                    if (strzu[icount] == s)
                    {
                        Output.Append(string.Format(temp_html,
                            userControlName,
                            (int)Enum.Parse(t, s),
                            s,
                            ckstr,
                            ClassName.Length > 0 ? ClassName : ""));

                        if (icount < strzu.Length - 1)
                            icount++;
                    }
                    else
                    {
                        Output.Append(string.Format(temp_html,
                            userControlName,
                            (int)Enum.Parse(t, s),
                            s,
                            "",
                            ClassName.Length > 0 ? ClassName : ""));
                    }

                }
            }
            return Output.ToString();
        }

        /// <summary>
        /// 多选框 中文  参与方式  附加参与人数
        /// </summary>
        /// <param name="userControlName"></param>
        /// <param name="_Type"></param>
        /// <param name="selectValue"></param>
        /// <param name="ClassName"></param>
        /// <param name="GSign"></param>
        /// <returns></returns>
        public static string EnumToCheckboxCN_ActivityJoinStyle(string userControlName, Type _Type, string selectValue, string ClassName, char GSign)
        {
            Type t = _Type;
            string ckstr = "checked='checked'";
            string temp_html = "<input type='checkbox' value='{2}' id='{0}-{1}' name='{0}'  {3} {4}><label for='{0}-{1}' style='cursor:pointer;'>{2}&nbsp;</label> <input type='text' name='ATJS_JoinStyleValue' value='' class='i_input_js canint' placeholder='参与数量' />";
            StringBuilder Output = new StringBuilder();

            int icount = 0;
            string[] strzu = null;
            if (selectValue.Contains(GSign))
                strzu = selectValue.Split(GSign);

            foreach (string s in Enum.GetNames(_Type))
            {
                if (!selectValue.Contains(GSign))
                {

                    Output.Append(string.Format(temp_html,
                    userControlName,
                    (int)Enum.Parse(t, s),
                    s,
                    s == selectValue ? ckstr : "",
                    ClassName.Length > 0 ? ClassName : ""));
                }
                else
                {

                    if (strzu[icount] == s)
                    {
                        Output.Append(string.Format(temp_html,
                            userControlName,
                            (int)Enum.Parse(t, s),
                            s,
                            ckstr,
                            ClassName.Length > 0 ? ClassName : ""));

                        if (icount < strzu.Length - 1)
                            icount++;
                    }
                    else
                    {
                        Output.Append(string.Format(temp_html,
                            userControlName,
                            (int)Enum.Parse(t, s),
                            s,
                            "",
                            ClassName.Length > 0 ? ClassName : ""));
                    }

                }
            }
            return Output.ToString();
        }


        public static string EnumToCheckboxCN(string userControlName, Type _Type, string selectValue)
        {
            return EnumToCheckboxCN(userControlName, _Type, selectValue, "", ',');
        }
        #endregion

    }
    #endregion

    #region 通用枚举类
    /// <summary>
    /// 通用枚举类
    /// </summary>
    public static class CommonEnumHelper
    {
        /// <summary>
        /// 商品分类-分类(产品/服务)
        /// </summary>
        [Flags]
        public enum GoodsCate_Cate { 产品 = 1, 服务 }

        /// <summary>
        /// 账户消费 现金/积分 分类
        /// </summary>
        [Flags]
        public enum AccountCate { 现金 = 1, 积分 }

        /// <summary>
        /// 账户 收入/支出 分类
        /// </summary>
        [Flags]
        public enum AccountLogCate { 收入 = 1, 支出 }


        /// <summary>
        /// 提现审核状态
        /// </summary>
        [Flags]
        public enum TiXianStatus { 申请中 = 1, 提现成功, 提现失败 }

        /// <summary>
        /// 收藏类型
        /// </summary>
        [Flags]
        public enum CollectionCate { 店铺 = 1, 商品, 团购, 服务 }


        /// <summary>
        /// 车友问答排序方式
        /// </summary>
        [Flags]
        public enum QA_SortType { 按回答时间 = 1, 按提问时间, 待解答 }

        /// <summary>
        /// 订单状态
        /// </summary>
        [Flags]
        public enum OrderStatus { 未付款, 已付款, 已发货, 已完成, 已取消, 售后服务中 }

        /// <summary>
        /// 返修退换货
        /// </summary>
        [Flags]
        public enum ApplyReturnGoods_XLY { 申请返修退换货中, 同意返修退换货, 已寄出返修退换货, 返修退换货完成 }

        /// <summary>
        /// 退货退款状态
        /// </summary>
        [Flags]
        public enum ApplyReturnGoods { 申请中, 商家同意等待寄回, 商家不同意退货, 已寄出等待确认, 退货完成, 网站方介入处理, 取消退款退货, 网站介入同意等待寄回, 网站介入不同意退货 }

        /// <summary>
        /// 举报状态
        /// </summary>
        [Flags]
        public enum ReportStatus { 等待处理 = 1, 处理成功 }


        /// <summary>
        /// 订单状态
        /// </summary>
        [Flags]
        public enum PhoneOrderStatus { 未付款, 已付款, 已发货, 已完成, 已取消 }
        /// <summary>
        /// 订单状态
        /// </summary>
        [Flags]
        public enum PhoneOrderTime { 一周内, 一月内, 半年内, 一年内 }

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum 支付宝状态
        {
            申请中 = 1,
            绑定成功,
            绑定失败
        }

        /// <summary>
        /// 广告位置
        /// </summary>
        [Flags]
        public enum AdsLocation
        {
            首页幻灯片1 = 1,
            首页幻灯片2,
            首页幻灯片3,
            首页幻灯片4,
            首页幻灯片5,
            活动专区幻灯片1,
            活动专区幻灯片2,
            活动专区幻灯片3,
            活动专区幻灯片4,
            活动专区幻灯片5,
            定制攻略幻灯片1,
            定制攻略幻灯片2,
            定制攻略幻灯片3,
            定制攻略幻灯片4,
            定制攻略幻灯片5

        }
        /// <summary>
        /// 活动报名支付状态
        /// </summary>
        [Flags]
        public enum ActivitySignUpStatus { 未付款, 已付款, 已取消 }
    }
    #endregion




}
