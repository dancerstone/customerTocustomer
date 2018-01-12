using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 检查DataSet是否为空
    /// </summary>
    public class CheckDataSet
    {
        /// <summary>
        /// 判断DataSet是否有数据,没有数据新加一条提示"暂无数据"
        /// </summary>
        /// <param name="_DataSet">DataSet源</param>
        /// <returns>重组后的DataSet</returns>
        public static System.Data.DataSet DataSet_ToEmpty(System.Data.DataSet _DataSet)
        {
            System.Data.DataSet ds = _DataSet;
            if (ds.Tables[0].Rows.Count < 1)
            {
                ds.Tables[0].Clear();
                for (int u = 0; u < ds.Tables[0].Columns.Count; u++)
                {
                    ds.Tables[0].Columns[u].DataType = typeof(String);
                    ds.Tables[0].Columns[u].AllowDBNull = true;
                }
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        ds.Tables[0].Rows[0][i] = Msg.NotData;
                    }
                    else
                    {
                        ds.Tables[0].Rows[0][i] = "..";
                    }
                }
            }
            return ds;
        }
    }
}
