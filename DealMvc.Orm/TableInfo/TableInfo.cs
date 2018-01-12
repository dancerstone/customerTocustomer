using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 类对应的表的相关信息
        /// </summary>
        public TableAttribute Table { get; set; }

        /// <summary>
        /// 类对应的列的相关信息
        /// </summary>
        public ColumnAttribute[] Columns { get; set; }

        /// <summary>
        /// 获取模型的命名空间全称
        /// </summary>
        public string TypeFullName { get; set; }

    }
}
