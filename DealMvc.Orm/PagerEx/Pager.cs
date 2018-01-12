/*风格1*/
/*
.PagerStyle1 { }
.PagerStyle1 .normalFontColor { color: #666666; }
.PagerStyle1 .allPageNumberFontColor { color: red; font-weight: 700; }
.PagerStyle1 .indexPageNumberFontColor { color: red; font-weight: 700; }
.PagerStyle1 a { padding: 1px 6px; border: solid 1px #ddd; background: #fff; text-decoration: none; margin-right: 2px; color: #31A9D0; }
.PagerStyle1 a:hover { color: #fff; background: #ffa501; border-color: #ffa501; text-decoration: none; }
.PagerStyle1 .nowNumber { color: red; font-weight: 700; padding: 1px 4px; }

*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace DealMvc.Orm.PagerEx
{

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum SQLOrderType
    {
        /// <summary>
        /// 升序
        /// </summary>
        ASC = 0,
        /// <summary>
        /// 降序
        /// </summary>
        DESC = 1
    }
    /// <summary>
    /// 分页描述位置
    /// </summary>
    public enum PagerDesAlign
    {
        /// <summary>
        /// 左边
        /// </summary>
        Left = 0,
        /// <summary>
        /// 右边
        /// </summary>
        Right = 1
    }
    /// <summary>
    /// 分页类
    /// </summary>
    /// <typeparam name="table"></typeparam>
    public class Pager<table> where table : EntityBase<table>, new()
    {
        #region 构造函数 - 传统传参方式

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        public Pager(int _pageindex, string _sqlwhere)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        /// <param name="_sqlparms">条件参数</param>
        public Pager(int _pageindex, string _sqlwhere, SqlParameter[] _sqlparms)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
            this._sqlparms = _sqlparms;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        /// <param name="_sqlparms">条件参数</param>
        /// <param name="_pagesize">每页记录数</param>
        public Pager(int _pageindex, string _sqlwhere, SqlParameter[] _sqlparms, int _pagesize)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
            this._sqlparms = _sqlparms;
            this._pagesize = _pagesize;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        /// <param name="_sqlparms">条件参数</param>
        /// <param name="_pagesize">每页记录数</param>
        /// <param name="_ordercolumn">排序字段,默认为id</param>
        /// <param name="_ordertype">排序方式</param>
        public Pager(int _pageindex, string _sqlwhere, SqlParameter[] _sqlparms, int _pagesize, string _ordercolumn, SQLOrderType _ordertype)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
            this._sqlparms = _sqlparms;
            this._pagesize = _pagesize;
            this._ordercolumn = new string[] { _ordercolumn };
            this._ordertype = new SQLOrderType[] { _ordertype };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        /// <param name="_sqlparms">条件参数</param>
        /// <param name="_pagesize">每页记录数</param>
        /// <param name="_ordercolumn">排序字段,默认为id</param>
        /// <param name="_ordertype">排序方式</param>
        public Pager(int _pageindex, string _sqlwhere, SqlParameter[] _sqlparms, int _pagesize, string[] _ordercolumn, SQLOrderType[] _ordertype)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
            this._sqlparms = _sqlparms;
            this._pagesize = _pagesize;
            this._ordercolumn = _ordercolumn;
            this._ordertype = _ordertype;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        /// <param name="_sqlparms">条件参数</param>
        /// <param name="_pagesize">每页记录数</param>
        /// <param name="_ordercolumn">排序字段,默认为id</param>
        /// <param name="_ordertype">排序方式</param>
        /// <param name="_columns">查询字段,默认为*</param>
        public Pager(int _pageindex, string _sqlwhere, SqlParameter[] _sqlparms, int _pagesize, string[] _ordercolumn, SQLOrderType[] _ordertype, string _columns)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
            this._sqlparms = _sqlparms;
            this._pagesize = _pagesize;
            this._ordercolumn = _ordercolumn;
            this._ordertype = _ordertype;
            this._columns = _columns;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="_sqlwhere">条件</param>
        /// <param name="objects">条件参数</param>
        public Pager(int _pageindex, string _sqlwhere, params object[] objects)
        {
            this._pageindex = _pageindex;
            this._sqlwhere = _sqlwhere;
            this._sqlparms = SQL.GetSQLParameter<table>(_sqlwhere, objects, typeof(table)).Re_SqlParameter();
        }

        #endregion

        #region 构造函数 - Lambda传参方式

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="func">Lambda形式条件和参数</param>
        public Pager(int _pageindex, System.Linq.Expressions.Expression<Func<table, bool>> func)
        {
            this._pageindex = _pageindex;
            SQLAndParameter _SP = LambdaToSQL.GetWhere<table>(func).CheckSQLSort(SQLSort.SQLWhere);
            this._sqlwhere = _SP.SQL;
            this._sqlparms = _SP.Parameter.Re_SqlParameter();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="_pagesize">每页记录数</param>
        public Pager(int _pageindex, System.Linq.Expressions.Expression<Func<table, bool>> func, int _pagesize)
        {
            this._pageindex = _pageindex;
            SQLAndParameter _SP = LambdaToSQL.GetWhere<table>(func).CheckSQLSort(SQLSort.SQLWhere);
            this._sqlwhere = _SP.SQL;
            this._sqlparms = _SP.Parameter.Re_SqlParameter();
            this._pagesize = _pagesize;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="_pagesize">每页记录数</param>
        ///<param name="funcOrder">Lambda形式排序</param>
        public Pager(int _pageindex, System.Linq.Expressions.Expression<Func<table, bool>> func, int _pagesize, System.Linq.Expressions.Expression<Func<table, bool>> funcOrder)
        {
            this._pageindex = _pageindex;
            SQLAndParameter _SP = LambdaToSQL.GetWhere<table>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPOrder = LambdaToSQL.GetWhere<table>(funcOrder).CheckSQLSort(SQLSort.SQLOrder);
            this._sqlwhere = _SP.SQL;
            this._sqlparms = _SP.Parameter.Re_SqlParameter();
            this._pagesize = _pagesize;
            this._orderstring = _SPOrder.SQL;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_pageindex">当前页数, 1为第一页</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="_pagesize">每页记录数</param>
        ///<param name="funcOrder">Lambda形式排序</param>
        ///<param name="funcFields">Lambda形式字段</param>
        public Pager(int _pageindex, System.Linq.Expressions.Expression<Func<table, bool>> func, int _pagesize, System.Linq.Expressions.Expression<Func<table, bool>> funcOrder, System.Linq.Expressions.Expression<Func<table, bool>> funcFields)
        {
            this._pageindex = _pageindex;
            SQLAndParameter _SP = LambdaToSQL.GetWhere<table>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPOrder = LambdaToSQL.GetWhere<table>(funcOrder).CheckSQLSort(SQLSort.SQLOrder);
            SQLAndParameter _SPFields = LambdaToSQL.GetWhere<table>(funcFields).CheckSQLSort(SQLSort.SQLFields);
            this._sqlwhere = _SP.SQL;
            this._sqlparms = _SP.Parameter.Re_SqlParameter();
            this._pagesize = _pagesize;
            this._orderstring = _SPOrder.SQL;
            this._columns = _SPFields.SQL;
        }

        #endregion

        #region 属性

        private int _pagesize = 20;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            set { _pagesize = value; }
            get { return _pagesize; }
        }

        private int _pagecount = 0;
        /// <summary>
        /// 总共页数,只读
        /// </summary>
        public int PageCount
        {
            //set { _pagecount = value; }
            get { return _pagecount; }
        }

        private int _pageindex = 1;
        /// <summary>
        /// 当前页数, 1为第一页
        /// </summary>
        public int PageIndex
        {
            set { _pageindex = value; }
            get { return _pageindex; }
        }

        private string _sqlwhere = "";
        /// <summary>
        /// SQL查询条件
        /// </summary>
        public string SQLWhere
        {
            set { _sqlwhere = value; }
            get { return _sqlwhere; }
        }

        private SqlParameter[] _sqlparms = new SqlParameter[] { };
        /// <summary>
        /// SQL参数,可为null
        /// </summary>
        public SqlParameter[] SQLParms
        {
            set { _sqlparms = value; }
            get { return _sqlparms; }
        }

        private string[] _ordercolumn = new string[] { "id" };
        /// <summary>
        /// 排序字段,默认为id
        /// </summary>
        public string[] OrderColumn
        {
            set { _ordercolumn = value; }
            get { return _ordercolumn; }
        }

        private SQLOrderType[] _ordertype = { SQLOrderType.DESC };
        /// <summary>
        /// 排序方式
        /// </summary>
        public SQLOrderType[] OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }

        private string _orderstring = string.Empty;
        /// <summary>
        /// 排序字符串
        /// </summary>
        public string OrderString
        {
            set { _orderstring = value; }
            get { return _orderstring; }
        }

        private int _recordcount = 0;
        /// <summary>
        /// 总记录数,只读
        /// </summary>
        public int RecordCount
        {
            //set { _recordcount = value; }
            get { return _recordcount; }
        }

        private string _columns = "*";
        /// <summary>
        /// 查询字段,默认为*
        /// </summary>
        public string Columns
        {
            set { _columns = value; }
            get { return _columns; }
        }

        private List<table> _datalist = new List<table>();
        /// <summary>
        /// 查询结果List集合
        /// </summary>
        public List<table> DataList
        {
            set { _datalist = value; }
            get { return _datalist; }
        }

        private DataSet _dataset = new DataSet();
        /// <summary>
        /// DataSet 数据
        /// </summary>
        public DataSet DataSet
        {
            set { _dataset = value; }
            get { return _dataset; }
        }

        #endregion

        #region 执行查询

        /// <summary>
        /// 执行查询分页
        /// </summary>
        public void GetPageList()
        {
            if (_pageindex < 1) { _pageindex = 1; }

            #region 排序

            if (string.IsNullOrEmpty(_orderstring.Trim()))
            {
                if (_ordercolumn.Length != _ordertype.Length || _ordercolumn.Length < 1)
                { throw new Exception("分页排序出错"); }

                ArrayList OrderOutput = new ArrayList();
                for (int u = 0; u < _ordercolumn.Length; u++)
                {
                    OrderOutput.Add(_ordercolumn[u].ToString() + " " + _ordertype[u].ToString());
                }
                _orderstring = String.Join(",", (string[])OrderOutput.ToArray(typeof(string)));
            }

            #endregion

            ArrayList _ArrayList = EntityCore<table>.GetPageList(_pagesize, _pageindex, _sqlwhere, _sqlparms, _orderstring, _columns);

            if (_ArrayList.Count == 5)
            {
                _datalist = (List<table>)_ArrayList[0];
                _dataset = (DataSet)_ArrayList[1];
                _recordcount = int.Parse(_ArrayList[2].ToString());
                _pagecount = int.Parse(_ArrayList[3].ToString());
                _pageindex = int.Parse(_ArrayList[4].ToString());
            }
            else
            {
                throw new Exception("分页出错");
            }
        }

        #endregion

        #region 分页参数

        private string _urlsign = "Page";
        /// <summary>
        /// 地址栏参数名称,默认为Page
        /// </summary>
        public string UrlSign
        {
            set { _urlsign = value; }
            get { return _urlsign; }
        }

        private int _C_aNums = 3;
        /// <summary>
        /// 分页单边点击a标签个数, 默认3个
        /// </summary>
        public int C_aNums
        {
            get { return _C_aNums; }
            set { _C_aNums = value; }
        }

        private bool _C_DesIsShow = true;
        /// <summary>
        /// 分页描述是否显示
        /// </summary>
        public bool C_DesIsShow
        {
            get { return _C_DesIsShow; }
            set { _C_DesIsShow = value; }
        }

        private string _C_CssClass = "PagerStyle1";
        /// <summary>
        /// 分页样式Class, 默认样式为 PagerStyle1
        /// </summary>
        public string C_CssClass
        {
            get { return _C_CssClass; }
            set { _C_CssClass = value; }
        }

        private PagerDesAlign _C_PagerDesAlign = PagerDesAlign.Left;
        /// <summary>
        /// 分页描述位置
        /// </summary>
        public PagerDesAlign C_PagerDesAlign
        {
            get { return _C_PagerDesAlign; }
            set { _C_PagerDesAlign = value; }
        }

        #endregion

        #region 生成分页代码

        /// <summary>
        /// 生成分页按钮
        /// </summary>
        /// <returns></returns>
        public string PagerStaticHTML(System.Web.UI.Page _Page)
        {
            string PageQuery = _Page.Request.Url.AbsolutePath + "?";

            foreach (string s in _Page.Request.QueryString.Keys)
            {
                if (!string.IsNullOrEmpty(s) && s.ToString().Trim().ToUpper() == _urlsign.ToUpper())
                    continue;

                PageQuery += (s + "=" + _Page.Request.QueryString[s] + "&");
            }

            StringBuilder output = new StringBuilder();
            output.Append("<span class='" + _C_CssClass + "'>");

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Left)
            {
                output.Append("<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页&nbsp;&nbsp;</span>");
            }

            if (_pageindex <= 1)
            {
                output.Append("<a style='cursor:default;'>首页</a>");
                output.Append("<a style='cursor:default;'>上一页</a>");
            }
            else
            {
                output.Append("<a p_id='1'>首页</a> ");
                output.Append("<a p_id='" + (_pageindex - 1).ToString() + "'>上一页</a>");
            }

            for (int i = _pageindex - _C_aNums; i < _pageindex; i++)
            {
                if (i < 1) { continue; }
                output.Append("<a p_id='" + i.ToString2() + "'>" + i.ToString() + "</a>");
            }
            output.Append("<span class='nowNumber'>" + _pageindex.ToString() + "</span>");
            for (int i = _pageindex + 1; i < _pageindex + _C_aNums + 1; i++)
            {
                if (i > _pagecount) { continue; }
                output.Append("<a  p_id='" + i.ToString2() + "'>" + i.ToString() + "</a>");
            }

            if (_pageindex >= _pagecount)
            {
                output.Append("<a style='cursor:default;'>下一页</a>");
                output.Append("<a style='cursor:default;'>末页</a>");
            }
            else
            {
                output.Append("<a p_id='" + (_pageindex + 1).ToString() + "'>下一页</a>");
                output.Append("<a p_id='" + _pagecount.ToString() + "'>末页</a>");
            }

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Right)
            {
                output.Append("&nbsp;&nbsp;<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页</span>");
            }

            output.Append("</span>");
            return output.ToString();
        }

        /// <summary>
        /// 生成分页按钮
        /// </summary>
        /// <returns></returns>
        public string PagerHTML(System.Web.UI.Page _Page)
        {
            string PageQuery = _Page.Request.Url.AbsolutePath + "?";

            foreach (string s in _Page.Request.QueryString.Keys)
            {
                if (!string.IsNullOrEmpty(s) && s.ToString().Trim().ToUpper() == _urlsign.ToUpper())
                    continue;

                PageQuery += (s + "=" + _Page.Request.QueryString[s] + "&");
            }

            StringBuilder output = new StringBuilder();
            output.Append("<span class='" + _C_CssClass + "'>");

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Left)
            {
                output.Append("<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页&nbsp;&nbsp;</span>");
            }

            if (_pageindex <= 1)
            {
                output.Append("<a style='cursor:default;'>首页</a>");
                output.Append("<a style='cursor:default;'>上一页</a>");
            }
            else
            {
                output.Append("<a href='" + PageQuery + _urlsign + "=1" + "'>首页</a> ");
                output.Append("<a href='" + PageQuery + _urlsign + "=" + (_pageindex - 1).ToString() + "'>上一页</a>");
            }

            for (int i = _pageindex - _C_aNums; i < _pageindex; i++)
            {
                if (i < 1) { continue; }
                output.Append("<a href='" + PageQuery + _urlsign + "=" + i.ToString() + "'>" + i.ToString() + "</a>");
            }
            output.Append("<span class='nowNumber'>" + _pageindex.ToString() + "</span>");
            for (int i = _pageindex + 1; i < _pageindex + _C_aNums + 1; i++)
            {
                if (i > _pagecount) { continue; }
                output.Append("<a href='" + PageQuery + _urlsign + "=" + i.ToString() + "'>" + i.ToString() + "</a>");
            }

            if (_pageindex >= _pagecount)
            {
                output.Append("<a style='cursor:default;'>下一页</a>");
                output.Append("<a style='cursor:default;'>末页</a>");
            }
            else
            {
                output.Append("<a href='" + PageQuery + "Page=" + (_pageindex + 1).ToString() + "'>下一页</a>");
                output.Append("<a href='" + PageQuery + "Page=" + _pagecount.ToString() + "'>末页</a>");
            }

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Right)
            {
                output.Append("&nbsp;&nbsp;<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页</span>");
            }

            output.Append("</span>");
            return output.ToString();
        }

        /// <summary>
        /// 生成分页按钮
        /// </summary>
        /// <returns></returns>
        public string PagerHTML_XLY(System.Web.UI.Page _Page)
        {
            string PageQuery = _Page.Request.Url.AbsolutePath + "?";

            foreach (string s in _Page.Request.QueryString.Keys)
            {
                if (!string.IsNullOrEmpty(s) && s.ToString().Trim().ToUpper() == _urlsign.ToUpper())
                    continue;

                PageQuery += (s + "=" + _Page.Request.QueryString[s] + "&");
            }

            StringBuilder output = new StringBuilder();
            //output.Append("<span class='" + _C_CssClass + "'>");

            if (_pageindex <= 1)
            {
                output.Append("<a style='cursor:default;'>首页</a>");
                output.Append("<a style='cursor:default;'>上一页</a>");
            }
            else
            {
                output.Append("<a href='" + PageQuery + _urlsign + "=1" + "'>首页</a> ");
                output.Append("<a href='" + PageQuery + _urlsign + "=" + (_pageindex - 1).ToString() + "'>上一页</a>");
            }

            for (int i = _pageindex - _C_aNums; i < _pageindex; i++)
            {
                if (i < 1) { continue; }
                output.Append("<a href='" + PageQuery + _urlsign + "=" + i.ToString() + "'>" + i.ToString() + "</a>");
            }
            output.Append("<a class='aNow'>" + _pageindex.ToString() + "</a>");
            for (int i = _pageindex + 1; i < _pageindex + _C_aNums + 1; i++)
            {
                if (i > _pagecount) { continue; }
                output.Append("<a href='" + PageQuery + _urlsign + "=" + i.ToString() + "'>" + i.ToString() + "</a>");
            }

            if (_pageindex >= _pagecount)
            {
                output.Append("<a style='cursor:default;'>下一页</a>");
                output.Append("<a style='cursor:default;'>末页</a>");
            }
            else
            {
                output.Append("<a href='" + PageQuery + "Page=" + (_pageindex + 1).ToString() + "'>下一页</a>");
                output.Append("<a href='" + PageQuery + "Page=" + _pagecount.ToString() + "'>末页</a>");
            }


            output.Append("</span>");
            return output.ToString();
        }

        /// <summary>
        /// 生成分页按钮
        /// </summary>
        /// <returns></returns>
        public string PagerHTML_FL(System.Web.UI.Page _Page)
        {
            string PageQuery = _Page.Request.Url.AbsolutePath + "?";

            foreach (string s in _Page.Request.QueryString.Keys)
            {
                if (!string.IsNullOrEmpty(s) && s.ToString().Trim().ToUpper() == _urlsign.ToUpper())
                    continue;

                PageQuery += (s + "=" + _Page.Request.QueryString[s] + "&");
            }

            StringBuilder output = new StringBuilder();
            output.Append("<span class='" + _C_CssClass + "'>");

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Left)
            {
                output.Append("<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页&nbsp;&nbsp;</span>");
            }

            if (_pageindex <= 1)
            {
                output.Append("<a style='cursor:default;'>首页</a>");
                output.Append("<a style='cursor:default;'>上一页</a>");
            }
            else
            {
                output.Append("<a href='" + PageQuery + "Page_Old=1" + "'>首页</a> ");
                output.Append("<a href='" + PageQuery + "Page_Old=" + (_pageindex - 1).ToString() + "'>上一页</a>");
            }

            for (int i = _pageindex - _C_aNums; i < _pageindex; i++)
            {
                if (i < 1) { continue; }
                output.Append("<a href='" + PageQuery + "Page_Old=" + i.ToString() + "'>" + i.ToString() + "</a>");
            }
            output.Append("<span class='nowNumber'>" + _pageindex.ToString() + "</span>");
            for (int i = _pageindex + 1; i < _pageindex + _C_aNums + 1; i++)
            {
                if (i > _pagecount) { continue; }
                output.Append("<a href='" + PageQuery + "Page_Old=" + i.ToString() + "'>" + i.ToString() + "</a>");
            }

            if (_pageindex >= _pagecount)
            {
                output.Append("<a style='cursor:default;'>下一页</a>");
                output.Append("<a style='cursor:default;'>末页</a>");
            }
            else
            {
                output.Append("<a href='" + PageQuery + "Page_Old=" + (_pageindex + 1).ToString() + "'>下一页</a>");
                output.Append("<a href='" + PageQuery + "Page_Old=" + _pagecount.ToString() + "'>末页</a>");
            }

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Right)
            {
                output.Append("&nbsp;&nbsp;<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页</span>");
            }

            output.Append("</span>");
            return output.ToString();
        }


        /// <summary>
        /// 生成无刷新分页按钮
        /// </summary>
        /// <returns></returns>
        public string PagerHTMLNoRefresh(System.Web.UI.Page _Page)
        {
            string PageQuery = _Page.Request.Url.AbsolutePath + "?";

            foreach (string s in _Page.Request.QueryString.Keys)
            {
                if (!string.IsNullOrEmpty(s) && s.ToString().Trim().ToUpper() == _urlsign.ToUpper())
                    continue;

                PageQuery += (s + "=" + _Page.Request.QueryString[s] + "&");
            }

            StringBuilder output = new StringBuilder();
            output.Append("<span class='" + _C_CssClass + "'>");

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Left)
            {
                output.Append("<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页&nbsp;&nbsp;</span>");
            }

            if (_pageindex <= 1)
            {
                output.Append("<a style='cursor:default;'>首页</a>");
                output.Append("<a style='cursor:default;'>上一页</a>");
            }
            else
            {
                output.Append("<a PID='1'>首页</a> ");
                output.Append("<a PID='" + (_pageindex - 1).ToString() + "'>上一页</a>");
            }

            for (int i = _pageindex - _C_aNums; i < _pageindex; i++)
            {
                if (i < 1) { continue; }
                output.Append("<a PID='" + i.ToString() + "'>" + i.ToString() + "</a>");
            }
            output.Append("<span class='nowNumber'>" + _pageindex.ToString() + "</span>");
            for (int i = _pageindex + 1; i < _pageindex + _C_aNums + 1; i++)
            {
                if (i > _pagecount) { continue; }
                output.Append("<a PID='" + i.ToString() + "'>" + i.ToString() + "</a>");
            }

            if (_pageindex >= _pagecount)
            {
                output.Append("<a style='cursor:default;'>下一页</a>");
                output.Append("<a style='cursor:default;'>末页</a>");
            }
            else
            {
                output.Append("<a PID='" + (_pageindex + 1).ToString() + "'>下一页</a>");
                output.Append("<a PID='" + _pagecount.ToString() + "'>末页</a>");
            }

            if (_C_DesIsShow && C_PagerDesAlign == PagerDesAlign.Right)
            {
                output.Append("&nbsp;&nbsp;<span class='normalFontColor'>总共<span class='allPageNumberFontColor'>" + _pagecount.ToString() + "</span>页&nbsp;&nbsp;当前第<span class='indexPageNumberFontColor'>" + _pageindex + "</span>页</span>");
            }

            output.Append("</span>");
            return output.ToString();
        }
        #endregion
    }

}
