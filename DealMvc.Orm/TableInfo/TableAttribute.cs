using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Configuration;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class TableAttribute : System.Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string name = string.Empty;
        /// <summary>
        /// 表名
        /// </summary>
        public string Name
        {
            get
            {
                string TablePrefix = ConfigurationSettings.AppSettings["TablePrefix"];//数据库表前缀

                if (string.IsNullOrEmpty(TablePrefix)) return name;

                return TablePrefix + "_" + name.Replace(TablePrefix, "");
            }
            set { name = value; }
        }
        /// <summary>
        /// 信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public TableAttribute(string Name)
        {
            this.Name = Name;
            this.Info = string.Empty;
        }
    }
}
