using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace DealMvc.DBUtility
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataStyle
    {
        SQL_Server = 0,
        Acess = 1
    }

    /// <summary>
    /// 数据库helper
    /// </summary>
    public class DataHelper
    {
        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public static DataStyle DataBaseName = DataStyle.SQL_Server;

        /// <summary>
        /// 否是为Access数据库
        /// </summary>
        public static bool IsAccess
        {
            get { return DataBaseName == DataStyle.Acess ? true : false; }
        }

        /// <summary>
        /// SqlParameter[]转OleDbParameter[]
        /// </summary>
        /// <param name="sqlparam">SqlParameter[]</param>
        /// <returns>OleDbParameter[]</returns>
        public static OleDbParameter[] GetOleDbParameterBySqlParameter(SqlParameter[] sqlparam)
        {
            if (sqlparam == null) return null;

            OleDbParameter[] oledbparam = new OleDbParameter[sqlparam.Length];
            for (int i = 0; i < sqlparam.Length; i++)
            {
                OleDbParameter _OleDbParameter = new OleDbParameter(sqlparam[i].ParameterName, sqlparam[i].Value);

                OleDbType _OleDbType = OleDbType.VarChar;
                bool s = true;
                try { _OleDbType = GetOleDbType(sqlparam[i].SqlDbType); }
                catch (Exception ce) { s = false; }

                if (s) _OleDbParameter.OleDbType = _OleDbType;

                oledbparam[i] = _OleDbParameter;
            }
            return oledbparam;
        }

        public static OleDbType GetOleDbType(SqlDbType _SqlDbType)
        {
            OleDbType _OleDbType = OleDbType.VarChar;

            switch (_SqlDbType)
            {
                case SqlDbType.BigInt:
                    _OleDbType = OleDbType.BigInt;
                    break;
                case SqlDbType.Binary:
                    _OleDbType = OleDbType.Binary;
                    break;
                case SqlDbType.Bit:
                    _OleDbType = OleDbType.Boolean;
                    break;
                case SqlDbType.Char:
                    _OleDbType = OleDbType.Char;
                    break;
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Time:
                case SqlDbType.Timestamp:
                    _OleDbType = OleDbType.Date;
                    break;
                case SqlDbType.Decimal:
                    _OleDbType = OleDbType.Decimal;
                    break;
                case SqlDbType.Image:
                    _OleDbType = OleDbType.LongVarBinary;
                    break;
                case SqlDbType.VarBinary:
                    _OleDbType = OleDbType.Binary;
                    break;
                case SqlDbType.Int:
                case SqlDbType.SmallInt:
                    _OleDbType = OleDbType.Integer;
                    break;
                case SqlDbType.Float:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    _OleDbType = OleDbType.Double;
                    break;
                case SqlDbType.NChar:
                    _OleDbType = OleDbType.WChar;
                    break;
                case SqlDbType.NText:
                    _OleDbType = OleDbType.LongVarWChar;
                    break;
                case SqlDbType.NVarChar:
                    _OleDbType = OleDbType.VarWChar;
                    break;
                case SqlDbType.Text:
                    _OleDbType = OleDbType.LongVarChar;
                    break;
                case SqlDbType.VarChar:
                    _OleDbType = OleDbType.VarChar;
                    break;
                default:
                    throw new Exception("类型转换失败");
                    break;
            }

            return _OleDbType;
        }

    }
}
