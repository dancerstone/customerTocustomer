using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 创建Parameter数组
    /// </summary>
    public class CreateParameter
    {
        /// <summary>
        /// Parameter数组..
        /// </summary>
        public CanShu CanShu = new CanShu();

        /// <summary>
        /// 创建并返回一个OleDbParameter数组
        /// </summary>
        /// <returns>OleDbParameter数组</returns>
        public OleDbParameter[] Re_OleDbParameter()
        {
            OleDbParameter[] _OleDbParameter = new OleDbParameter[CanShu.OleCount];
            for (int i = 0; i < CanShu.OleCount; i++)
            {
                _OleDbParameter[i] = (OleDbParameter)CanShu.ChanShuObjectArrOle[i];
            }
            return _OleDbParameter;
        }

        /// <summary>
        /// 创建并返回一个SqlParameter数组
        /// </summary>
        /// <returns>SqlParameter数组</returns>
        public SqlParameter[] Re_SqlParameter()
        {
            SqlParameter[] _SqlParameter = new SqlParameter[CanShu.SqlCount];
            for (int i = 0; i < CanShu.SqlCount; i++)
            {
                _SqlParameter[i] = (SqlParameter)CanShu.ChanShuObjectArrSql[i];
            }
            return _SqlParameter;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~CreateParameter()
        {
            //CanShu.Clear();
            CanShu = null;
        }
    }
    /// <summary>
    /// 参数对象集合
    /// </summary>
    public class CanShu
    {
        /// <summary>
        /// 允许数据库float字段的小数点位数
        /// </summary>
        private int XiaoShuWeiShu = 2;

        /// <summary>
        /// SqlParameter数组
        /// </summary>
        public ArrayList ChanShuObjectArrSql = new ArrayList();
        /// <summary>
        /// OleDbParameter数组
        /// </summary>
        public ArrayList ChanShuObjectArrOle = new ArrayList();
        /// <summary>
        /// SqlParameter数组个数
        /// </summary>
        public int SqlCount
        {
            get { return ChanShuObjectArrSql.Count; }
        }
        /// <summary>
        /// OleDbParameter数组个数
        /// </summary>
        public int OleCount
        {
            get { return ChanShuObjectArrOle.Count; }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 添加SqlParameter / OleDbParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, object Value)
        {
            ChanShuObjectArrSql.Add(new SqlParameter(Key, Value));
            ChanShuObjectArrOle.Add(new OleDbParameter(Key, Value));
        }
        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 添加SqlParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Type">SqlDbType</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, SqlDbType Type, object Value)
        {
            SqlParameter _S = new SqlParameter(Key, Type);
            _S.Value = DealValue(Value, Type, 0);
            ChanShuObjectArrSql.Add(_S);
        }
        /// <summary>
        /// 添加SqlParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Type">SqlDbType</param>
        /// <param name="Size">大小</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, SqlDbType Type, int Size, object Value)
        {
            SqlParameter _S = new SqlParameter(Key, Type, Size);
            _S.Value = DealValue(Value, Type, 0);
            ChanShuObjectArrSql.Add(_S);
        }
        /// <summary>
        /// 添加SqlParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Type">SqlDbType</param>
        /// <param name="Size">大小</param>
        /// <param name="ColumnName">列名</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, SqlDbType Type, int Size, string ColumnName, object Value)
        {
            SqlParameter _S = new SqlParameter(Key, Type, Size, ColumnName);
            _S.Value = DealValue(Value, Type, 0);
            ChanShuObjectArrSql.Add(_S);
        }
        /// <summary>
        /// 添加 []_SqlParameterArr, 把 []_SqlParameterArr 自动转换成Nvarchar类型
        /// </summary>
        /// <param name="_SqlParameter">SqlParameter</param>
        public void AddAsNvarchar(SqlParameter[] _SqlParameterArr)
        {
            foreach (SqlParameter _SqlParameter in _SqlParameterArr)
            {
                _SqlParameter.SqlDbType = SqlDbType.NVarChar;
                ChanShuObjectArrSql.Add(_SqlParameter);
            }
        }
        public object DealValue(object v, SqlDbType Type, object obj)
        {
            switch (Type)
            {

                case SqlDbType.Decimal:
                case SqlDbType.Float:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                case SqlDbType.Real:
                    if (string.IsNullOrEmpty(v.ToString2())) return obj;
                    return v.ToString2().ToDouble2().JRound(XiaoShuWeiShu);//小数点位数
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt:
                    if (string.IsNullOrEmpty(v.ToString2())) return obj;
                    break;
            }
            return v;
        }
        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 添加OleDbParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Type">SqlDbType</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, OleDbType Type, object Value)
        {
            OleDbParameter _O = new OleDbParameter(Key, Type);
            _O.Value = Value;
            ChanShuObjectArrOle.Add(_O);
        }
        /// <summary>
        /// 添加OleDbParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Type">SqlDbType</param>
        /// <param name="Size">大小</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, OleDbType Type, int Size, object Value)
        {
            OleDbParameter _O = new OleDbParameter(Key, Type, Size);
            _O.Value = Value;
            ChanShuObjectArrOle.Add(_O);
        }
        /// <summary>
        /// 添加OleDbParameter
        /// </summary>
        /// <param name="Key">@name</param>
        /// <param name="Type">SqlDbType</param>
        /// <param name="Size">大小</param>
        /// <param name="ColumnName">列名</param>
        /// <param name="Value">Value</param>
        public void Add(string Key, OleDbType Type, int Size, string ColumnName, object Value)
        {
            OleDbParameter _O = new OleDbParameter(Key, Type, Size, ColumnName);
            _O.Value = Value;
            ChanShuObjectArrOle.Add(_O);
        }
    }

}
