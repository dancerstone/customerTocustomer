using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DealMvc.SqlTranEx
{
    /// <summary>
    /// 实现SQL事务机制
    /// </summary>
    public class SqlTranExtensions
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlTranExtensions() { }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _Hashtable = new Dictionary<string, object>();
        /// <summary>
        /// 事物集合
        /// </summary>
        public Dictionary<string, object> C_Hashtable
        {
            get { return _Hashtable; }
            set { _Hashtable = value; }
        }

        /// <summary>
        /// 增加一个数据库事物
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(object key, object value)
        {
            string KongGe = "";
            for (int i = 0; i < C_Hashtable.Count; i++)
            {
                KongGe += " ";
            }
            C_Hashtable.Add(key + KongGe, value);
        }

        /// <summary>
        /// 执行事物集合
        /// </summary>
        public bool ExecuteSqlTran()
        {
            return DBUtility.DbHelperSQL.ExecuteSqlTran(C_Hashtable);
        }

        /// <summary>
        /// 清空事物集合
        /// </summary>
        public void Clear()
        {
            _Hashtable.Clear();
        }
    }
}
