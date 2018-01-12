using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using DealMvc.Orm;
using System.Data.SqlClient;

namespace DealMvc
{
    /// <summary>
    /// SQL类型
    /// </summary>
    public enum SQLSort
    {
        /// <summary>
        /// SQL条件
        /// </summary>
        SQLWhere = 0,
        /// <summary>
        /// SQL排序
        /// </summary>
        SQLOrder = 1,
        /// <summary>
        /// SQL返回字段
        /// </summary>
        SQLFields = 2
    }

    /// <summary>
    /// SQL和参数类
    /// </summary>
    public class SQLAndParameter
    {
        /// <summary>
        /// 默认为SQL条件
        /// </summary>
        private SQLSort _SQLSort = SQLSort.SQLWhere;
        public SQLSort SQLSort
        {
            get { return _SQLSort; }
            set
            {
                if (value == DealMvc.SQLSort.SQLOrder)
                { sign *= 10; }
                _SQLSort = value;
            }
        }
        /// <summary>
        /// 标识
        /// </summary>
        public int sign = 7000;
        /// <summary>
        /// SQL部分语句
        /// </summary>
        public string SQL = String.Empty;
        /// <summary>
        /// SQL参数
        /// </summary>
        public DealMvc.Common.Net.CreateParameter Parameter = new Common.Net.CreateParameter();
        /// <summary>
        /// 检查SQL类型是否正确
        /// </summary>
        /// <returns></returns>
        public SQLAndParameter CheckSQLSort(SQLSort SS)
        {
            if (_SQLSort != SS && !string.IsNullOrEmpty(SQL))
                throw new Exception(string.Format("SQL类型本该为{0},但当前为{1}", _SQLSort.ToString(), SS.ToString()));
            if (SS == SQLSort.SQLOrder || SS == SQLSort.SQLFields)
                //替换
                this.SQL = this.SQL.Replace("AND", ",")
                                    .Replace("(", "").Replace(")", "")
                                    .Replace("（", "(").Replace("）", ")");
            if (SS == SQLSort.SQLFields && string.IsNullOrEmpty(SQL))
                SQL = " * ";
            return this;
        }
    }

    /// <summary>
    /// LambdaToSQL处理类
    /// </summary>
    public static class LambdaToSQL
    {

        #region Lambda扩展静态方法

        #region 字段

        /// <summary>
        /// 返回字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool lb_ColumeName<T>(this T obj)
        {
            return true;
        }
        /// <summary>
        /// 字段和， sum(money)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool lb_Sum<T>(this T obj)
        {
            return true;
        }
        /// <summary>
        /// 字段平局值， SELECT AVG(OrderPrice) AS OrderAverage FROM Orders
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool lb_Avg<T>(this T obj)
        {
            return true;
        }
        /// <summary>
        /// 取出某列重复字段， SELECT DISTINCT 列名称 FROM 表名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool lb_Distinct<T>(this T obj)
        {
            return true;
        }

        #endregion

        #region 排序

        /// <summary>
        /// Desc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool lb_Desc<T>(this T obj)
        {
            return true;
        }
        /// <summary>
        /// Asc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool lb_Asc<T>(this T obj)
        {
            return true;
        }
        /// <summary>
        /// Order By Arr 例子:  select * from table1 where id in (183,200) order by charindex(',' + ltrim(id) + ',' , ',183,200,')
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool lb_OrderByArr<T>(this T obj, int?[] array)
        {
            return true;
        }

        #endregion

        #region 条件

        /// <summary>
        /// In
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool lb_In(this int? obj, int?[] array)
        {
            return true;
        }

        /// <summary>
        ///(string) In
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool lb_In(this string obj, string[] array)
        {
            if (array.Length > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// NotIn
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool lb_NotIn(this int? obj, int?[] array)
        {
            return true;
        }
        /// <summary>
        /// Like - 不需传递%  生成 %likeStr%
        /// </summary>
        /// <param name="str"></param>
        /// <param name="likeStr"></param>
        /// <returns></returns>
        public static bool lb_Like(this string str, string likeStr)
        {
            return true;
        }
        /// <summary>
        /// LikeR - 不需传递%  生成 likeStr%
        /// </summary>
        /// <param name="str"></param>
        /// <param name="likeStr"></param>
        /// <returns></returns>
        public static bool lb_LikeR(this string str, string likeStr)
        {
            return true;
        }
        /// <summary>
        /// LikeL - 不需传递%  生成 %likeStr
        /// </summary>
        /// <param name="str"></param>
        /// <param name="likeStr"></param>
        /// <returns></returns>
        public static bool lb_LikeL(this string str, string likeStr)
        {
            return true;
        }
        /// <summary>
        /// NotLike
        /// </summary>
        /// <param name="str"></param>
        /// <param name="likeStr"></param>
        /// <returns></returns>
        public static bool lb_NotLike(this string str, string likeStr)
        {
            return true;
        }
        /// <summary>
        /// 字符串如果不为null,则执行等于比较
        /// </summary>
        /// <param name="obj"></param>o
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool lb_IsNotNullAndEqual<T>(this string obj, string Value)
        {
            return true;
        }
        /// <summary>
        /// object如果不为null,则执行等于FuHao(参数二)比较
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Value"></param>
        /// <param name="FuHao">比较符号</param>
        /// <returns></returns>
        public static bool lb_IsNotNullAndDo<T>(this T obj, object Value, string FuHao)
        {
            return true;
        }
        /// <summary>
        /// object如果不为null和Empty,则执行等于FuHao(参数二)比较
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Value"></param>
        /// <param name="FuHao">比较符号</param>
        /// <returns></returns>
        public static bool lb_IsNotNullAndEmptyAndDo<T>(this T obj, object Value, string FuHao)
        {
            return true;
        }
        /// <summary>
        /// 如果传入false则不执行比较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool lb_IsNotFalseAndEqual<T>(this T obj, bool Value)
        {
            return true;
        }

        #endregion

        #endregion

        /// <summary>
        /// 返回SQL和参数
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="func">Lambda表达式</param>
        /// <returns></returns>
        public static SQLAndParameter GetWhere<T>(Expression<Func<T, bool>> func) where T : EntityBase<T>, new()
        {
            SQLAndParameter SP = new SQLAndParameter();
            string SQL = string.Empty;
            if (func != null)
            {
                if (func.Body is BinaryExpression)
                {
                    BinaryExpression be = ((BinaryExpression)func.Body);
                    SP.SQL = BinarExpressionProvider(SP, be.Left, be.Right, be.NodeType);
                }
                else if (func.Body is MethodCallExpression)
                {
                    SP.SQL = ExpressionRouter(SP, ((MethodCallExpression)func.Body), true);
                }
            }
            else
                SP.SQL = "";
            return SP;
        }


        #region LambdaToSQL处理过程

        private static string BinarExpressionProvider(SQLAndParameter SP, Expression left, Expression right, ExpressionType type)
        {
            string sb = "(";
            //先处理左边
            sb += ExpressionRouter(SP, left, true);

            sb += ExpressionTypeCast(type);

            //再处理右边
            string tmpStr = ExpressionRouter(SP, right, false);
            if (tmpStr == null)
            {
                sb = sb.Trim();
                if (sb.EndsWith("="))
                    sb = sb.Substring(0, sb.Length - 1).Trim() + " is null";
                else if (sb.EndsWith("<>"))
                    sb = sb.Substring(0, sb.Length - 2).Trim() + " is not null";
            }
            else
                sb += tmpStr;
            return sb += ")";
        }

        private static string ExpressionRouter(SQLAndParameter SP, Expression exp, bool IsLeft)
        {//IsLeft是否是表达式左边

            string sb = string.Empty;
            if (exp is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)exp);
                return BinarExpressionProvider(SP, be.Left, be.Right, be.NodeType);
            }
            else if (exp is MemberExpression)
            {
                #region MyRegion

                if (!IsLeft)
                {
                    try
                    {
                        //获取值
                        object Result = Expression.Lambda(exp).Compile().DynamicInvoke();
                        return DealArrValue(SP, Result);
                    }
                    catch { throw new Exception("MemberExpression: Lambda表达式表 值必须放右边"); }
                }

                //获取字段
                return GetColumeName(SP, exp, true);

                #endregion

            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    if (ex == null || ex.ToString2() == "null") continue;
                    tmpstr.Append(ExpressionRouter(SP, ex, false));
                    tmpstr.Append(",");
                }
                if (ae.Expressions == null || ae.Expressions.Count == 0)
                {
                    tmpstr.Append(GetValue(SP, "0"));
                    tmpstr.Append(",");
                }
                string str = tmpstr.ToString();
                return str.Length >= 1 ? str.Substring(0, str.Length - 1) : str;
            }
            else if (exp is MethodCallExpression)
            {
                #region MyRegion

                MethodCallExpression mce = (MethodCallExpression)exp;

                #region 条件

                /*条件*/
                if (mce.Method.Name == "lb_Like")
                {
                    return string.Format("({0} like '%'+{1}+'%')", ExpressionRouter(SP, mce.Arguments[0], true), ExpressionRouter(SP, mce.Arguments[1], false));
                }
                if (mce.Method.Name == "lb_LikeR")
                { return string.Format("({0} like {1}+'%')", ExpressionRouter(SP, mce.Arguments[0], true), ExpressionRouter(SP, mce.Arguments[1], false)); }
                if (mce.Method.Name == "lb_LikeL")
                { return string.Format("({0} like '%'+{1})", ExpressionRouter(SP, mce.Arguments[0], true), ExpressionRouter(SP, mce.Arguments[1], false)); }
                else if (mce.Method.Name == "lb_NotLike")
                { return string.Format("({0} Not like '%'+{1}+'%')", ExpressionRouter(SP, mce.Arguments[0], true), ExpressionRouter(SP, mce.Arguments[1], false)); }
                else if (mce.Method.Name == "lb_In")
                { return string.Format("({0} In ({1}))", ExpressionRouter(SP, mce.Arguments[0], true), ExpressionRouter(SP, mce.Arguments[1], false)); }
                else if (mce.Method.Name == "lb_NotIn")
                { return string.Format("({0} Not In ({1}))", ExpressionRouter(SP, mce.Arguments[0], true), ExpressionRouter(SP, mce.Arguments[1], false)); }
                else if (mce.Method.Name == "lb_IsNotNullAndEqual")
                {
                    string ColumeName = ExpressionRouter(SP, mce.Arguments[0], true);
                    object Value = ExpressionRouter(SP, mce.Arguments[1], false);
                    if (Value == null)
                        return string.Format("( 1=1 )");
                    else
                        return string.Format("({0} = {1})", ColumeName, Value);
                }
                else if (mce.Method.Name == "lb_IsNotNullAndDo")
                {
                    string ColumeName = ExpressionRouter(SP, mce.Arguments[0], true);
                    object Value = ExpressionRouter(SP, mce.Arguments[1], false);
                    string FuHao = Expression.Lambda(mce.Arguments[2]).Compile().DynamicInvoke().ToString2().Replace("\"", "");// mce.Arguments[2].ToString2().Replace("\"", "");
                    if (Value == null)
                        return string.Format("( 1=1 )");
                    else
                        return string.Format("({0} {2} {1})", ColumeName, Value, FuHao);
                }
                else if (mce.Method.Name == "lb_IsNotNullAndEmptyAndDo")
                {
                    string ColumeName = ExpressionRouter(SP, mce.Arguments[0], true);
                    object Value = ExpressionRouter(SP, mce.Arguments[1], false);
                    string FuHao = Expression.Lambda(mce.Arguments[2]).Compile().DynamicInvoke().ToString2().Replace("\"", "");// mce.Arguments[2].ToString2().Replace("\"", "");
                    if (Value == null)
                    {
                        return string.Format("( 1=1 )");
                    }
                    else
                    {
                        int LastIndex = SP.Parameter.CanShu.ChanShuObjectArrSql.Count - 1;
                        if (LastIndex >= 0)
                        {
                            SqlParameter _SqlParameter = (SqlParameter)SP.Parameter.CanShu.ChanShuObjectArrSql[LastIndex];
                            string val = _SqlParameter.Value.ToString2();
                            if (string.IsNullOrEmpty(val))
                            {
                                SP.Parameter.CanShu.ChanShuObjectArrSql.RemoveAt(LastIndex);
                                return string.Format("( 1=1 )");
                            }
                        }
                        return string.Format("({0} {2} {1})", ColumeName, Value, FuHao);
                    }
                }
                else if (mce.Method.Name == "lb_IsNotFalseAndEqual")
                {
                    string ColumeName = ExpressionRouter(SP, mce.Arguments[0], true);
                    object Value = ExpressionRouter(SP, mce.Arguments[1], false);
                    if (Value == null)
                    {
                        return string.Format("( 1=1 )");
                    }
                    else
                    {
                        int LastIndex = SP.Parameter.CanShu.ChanShuObjectArrSql.Count - 1;
                        if (LastIndex >= 0)
                        {
                            SqlParameter _SqlParameter = (SqlParameter)SP.Parameter.CanShu.ChanShuObjectArrSql[LastIndex];
                            bool val = (bool)_SqlParameter.Value;
                            if (val == false)
                            {
                                SP.Parameter.CanShu.ChanShuObjectArrSql.RemoveAt(LastIndex);
                                return string.Format("( 1=1 )");
                            }
                        }
                        return string.Format("({0} = {1})", ColumeName, Value);
                    }
                }

                #endregion

                #region 排序

                /*排序*/
                else if (mce.Method.Name == "lb_Desc")
                {
                    SP.SQLSort = SQLSort.SQLOrder;
                    return string.Format(" {0} Desc ", GetColumeName(SP, mce.Arguments[0], false));
                }
                else if (mce.Method.Name == "lb_Asc")
                {
                    SP.SQLSort = SQLSort.SQLOrder;
                    return string.Format(" {0} Asc ", GetColumeName(SP, mce.Arguments[0], false));
                }
                else if (mce.Method.Name == "lb_OrderByArr")
                {
                    SP.SQLSort = SQLSort.SQLOrder;
                    string ColumeName = GetColumeName(SP, mce.Arguments[0], true);
                    object Value = ExpressionRouter(SP, mce.Arguments[1], false);
                    return string.Format(" charindex（',' + ltrim（{0}） + ',' , ','+{1}+',' ） ", ColumeName, Value.ToString2().Replace(",", "+','+"));
                }

                #endregion

                #region 字段

                /*字段*/
                else if (mce.Method.Name == "lb_ColumeName")
                {
                    SP.SQLSort = SQLSort.SQLFields;
                    return string.Format(" {0} ", ExpressionRouter(SP, mce.Arguments[0], true));
                }
                else if (mce.Method.Name == "lb_Sum")
                {
                    SP.SQLSort = SQLSort.SQLFields;
                    return string.Format(" Sum({0}) ", ExpressionRouter(SP, mce.Arguments[0], true));
                }
                else if (mce.Method.Name == "lb_Avg")
                {
                    SP.SQLSort = SQLSort.SQLFields;
                    return string.Format(" Avg({0}) ", ExpressionRouter(SP, mce.Arguments[0], true));
                }
                else if (mce.Method.Name == "lb_Distinct")
                {
                    SP.SQLSort = SQLSort.SQLFields;
                    return string.Format(" Distinct {0} ) ", ExpressionRouter(SP, mce.Arguments[0], true));
                }

                #endregion

                #endregion

                #region MyRegion

                try
                {
                    object Result = Expression.Lambda(exp).Compile().DynamicInvoke();
                    return DealArrValue(SP, Result);
                }
                catch { throw new Exception("MethodCallExpression: 获取值出错"); }

                #endregion
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression ce = ((ConstantExpression)exp);
                return GetValue(SP, ce.Value);
            }
            else if (exp is UnaryExpression)
            {
                //UnaryExpression ue = ((UnaryExpression)exp);
                //return ExpressionRouter(SP, ue.Operand);
                object Result = Expression.Lambda(exp).Compile().DynamicInvoke();
                return GetValue(SP, Result);
            }
            return null;
        }

        /// <summary>
        /// 获取和处理参数值
        /// </summary>
        /// <param name="SP"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static string GetValue(SQLAndParameter SP, object Value)
        {
            int LastIndex = SP.Parameter.CanShu.ChanShuObjectArrSql.Count - 1;
            if (Value == null)
            {
                SP.Parameter.CanShu.ChanShuObjectArrSql.RemoveAt(LastIndex);
                return null;
            }
            //else if (ce.Value is ValueType)
            //    return ce.Value.ToString();
            //else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
            //    return string.Format("'{0}'", ce.Value.ToString());
            SqlParameter _SqlParameter = (SqlParameter)SP.Parameter.CanShu.ChanShuObjectArrSql[LastIndex];
            if (_SqlParameter.TypeName == "-1")
            {
                _SqlParameter.TypeName = "";
                _SqlParameter.Value = Value;
                SP.Parameter.CanShu.ChanShuObjectArrSql[LastIndex] = _SqlParameter;
                SP.sign++;
                return _SqlParameter.ParameterName;
            }
            else
            {
                string ParameterName = _SqlParameter.ParameterName.Split('7')[0] + SP.sign;
                SP.Parameter.CanShu.Add(ParameterName, _SqlParameter.SqlDbType, Value);
                SP.sign++;
                return ParameterName;
            }
        }

        /// <summary>
        /// 处理数组
        /// </summary>
        /// <param name="SP"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        private static string DealArrValue(SQLAndParameter SP, object Result)
        {
            if (Result is Array)
            {
                StringBuilder tmpstr = new StringBuilder();
                Array Arr = (Array)Result;
                if (Arr == null || Arr.Length == 0) Arr = new int?[] { 0 };
                if (Arr.Length == 1 && Arr.GetValue(0) == null) Arr = new int?[] { 0 };
                foreach (object o in Arr)
                {
                    if (o == null || o.ToString2() == "null") continue;
                    tmpstr.Append(GetValue(SP, o));
                    tmpstr.Append(",");
                }
                string str = tmpstr.ToString();
                return str.Length >= 1 ? str.Substring(0, str.Length - 1) : str;
            }
            return GetValue(SP, Result);
        }

        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <returns></returns>
        private static string GetColumeName(SQLAndParameter SP, Expression exp, bool NeedParameter)
        {
            MemberExpression me = ((MemberExpression)exp);
            string ColumnName = me.Member.Name;
            ColumnAttribute[] cas = me.Member.ReflectedType.GetProperty(me.Member.Name).GetCustomAttributes(typeof(ColumnAttribute), false) as ColumnAttribute[];
            if (cas.Length > 0)
            {
                if (NeedParameter)
                {
                    SP.Parameter.CanShu.Add("@" + ColumnName + SP.sign, cas[0].Type, null);
                    ((SqlParameter)SP.Parameter.CanShu.ChanShuObjectArrSql[SP.Parameter.CanShu.ChanShuObjectArrSql.Count - 1]).TypeName = "-1";
                }
            }
            else
                throw new Exception("MemberExpression: LambdaToSQL " + ColumnName + " 数据库类型(SqlDbType)获取失败");
            return ColumnName;
        }

        /// <summary>
        /// 获取符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " = ";
                case ExpressionType.GreaterThan:
                    return " > ";
                case ExpressionType.GreaterThanOrEqual:
                    return " >= ";
                case ExpressionType.LessThan:
                    return " < ";
                case ExpressionType.LessThanOrEqual:
                    return " <= ";
                case ExpressionType.NotEqual:
                    return " <> ";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return " + ";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return " - ";
                case ExpressionType.Divide:
                    return " / ";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return " * ";
                default:
                    return null;
            }
        }

        #endregion
    }
}