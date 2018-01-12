using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 分页程序1
    /// </summary>
    public class PagedDataSet
    {

        #region 解析函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedDataSet() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_pagesize">每页显示的页数</param>
        public PagedDataSet(int _pagesize)
        {
            _PageSize = _pagesize;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 读取或设置每页显示的条数(默认为10条)
        /// </summary>
        private int _PageSize = 10;
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


        #endregion

        #region 分页方法

        /// <summary>
        /// 用PagedDataSource实现分页
        /// </summary>
        /// <param name="_DS">DataSet对象</param>
        /// <param name="S_index">当前页的索引</param>
        /// <returns>PagedDataSource对象</returns>
        public System.Web.UI.WebControls.PagedDataSource Paged(System.Data.DataSet _DS, int S_index)
        {
            System.Web.UI.WebControls.PagedDataSource _PDS = new System.Web.UI.WebControls.PagedDataSource();
            _PDS.DataSource = _DS.Tables[0].DefaultView;
            _PDS.AllowPaging = true;
            _PDS.PageSize = _PageSize;
            _PageCount = _PDS.PageCount;
            if (S_index <= 0)
            {
                _PageIndex = 0;
            }
            else if (S_index >= _PageCount - 1)
            {
                _PageIndex = _PageCount - 1;
            }
            else
            {
                _PageIndex = S_index;
            }
            _PDS.CurrentPageIndex = _PageIndex;
            return _PDS;
        }

        #endregion

        #region 分页跳转

        /// <summary>
        /// 调至首页
        /// </summary>
        /// <returns>返回当前页数的索引</returns>
        public int FirstPage()
        {
            return 0;
        }

        /// <summary>
        /// 调至上一页
        /// </summary>
        /// <param name="S_index">当前页的索引</param>
        /// <returns>返回当前页数的索引</returns>
        public int PrePage(int S_index)
        {
            if (S_index <= 0)
            {
                S_index = 0;
            }
            else
            {
                S_index--;
            }
            return S_index;
        }

        /// <summary>
        /// 调至下一页
        /// </summary>
        /// <param name="S_index">当前页的索引</param>
        /// <param name="S_count">总页数</param>
        /// <returns>返回当前页数的索引</returns>
        public int NextPage(int S_index, int S_count)
        {
            if (S_index >= S_count - 1)
            {
                S_index = S_count - 1;
            }
            else
            {
                S_index++;
            }
            return S_index;
        }

        /// <summary>
        /// 调至末页
        /// </summary>
        /// <param name="S_index">当前页的索引</param>
        /// <param name="S_count">总页数</param>
        /// <returns>返回当前页数的索引</returns>
        public int LastPage(int S_index, int S_count)
        {
            S_index = S_count - 1;
            return S_index;
        }

        #endregion

    }
}
