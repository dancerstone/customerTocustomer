using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class ColumnAttribute : System.Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public SqlDbType Type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 是否支持空
        /// </summary>
        public bool CanNull { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public object Default { get; set; }
        /// <summary>
        /// 是否是主键,但不表示自动增长
        /// </summary>
        public bool PrimaryKey { get; set; }
        /// <summary>
        /// 是否是自动增长列
        /// </summary>
        public bool AutoIncrement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ColumnAttribute(string name, SqlDbType type)
        {
            Name = name;
            Type = type;
            Length = -1;
            Intro = string.Empty;
            CanNull = true;
            Default = null;
            PrimaryKey = false;
            AutoIncrement = false;
        }
    }

}
