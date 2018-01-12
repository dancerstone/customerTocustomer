﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class AttributeHelper
    {
        #region 实体辅助方法
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ObjectType"></typeparam>
        /// <returns></returns>
        public static TableAttribute GetTableInfo<ObjectType>()
        {
            return GetInfo<ObjectType>().Table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ObjectType"></typeparam>
        /// <returns></returns>
        public static ColumnAttribute[] GetColumnInfo<ObjectType>()
        {
            return GetInfo<ObjectType>().Columns;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ObjectType"></typeparam>
        /// <returns></returns>
        public static ColumnAttribute GetPrimaryKey<ObjectType>()
        {
            foreach (ColumnAttribute cla in GetColumnInfo<ObjectType>())
            {
                if (cla.PrimaryKey)
                    return cla;
            }
            throw new Exception("没有定义主键");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ObjectType"></typeparam>
        /// <returns></returns>
        public static TableInfo GetInfo<ObjectType>()
        {
            Type type = typeof(ObjectType);
            return GetInfo(type);
        }

        private static TableInfo GetInfo(Type type)
        {
            TableInfo info = new TableInfo();

            TableAttribute[] tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false) as TableAttribute[];

            List<ColumnAttribute> columnAttributeList = new List<ColumnAttribute>();
            PropertyInfo[] propertyAttribute = type.GetProperties();

            foreach (PropertyInfo pi in propertyAttribute)
            {
                ColumnAttribute[] cas = pi.GetCustomAttributes(typeof(ColumnAttribute), false) as ColumnAttribute[];

                if (cas.Length > 0)
                {
                    columnAttributeList.Add(cas[0]);
                }
            }

            info.Table = tableAttribute[0];
            info.Columns = columnAttributeList.ToArray();
            info.TypeFullName = type.FullName;
            return info;
        }


        /// <summary>
        /// C#数据类型到Database
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string PrimaryKeyTypeToCs(ColumnAttribute col)
        {
            switch (col.Type)
            {
                case SqlDbType.Int:
                case SqlDbType.SmallInt:
                    return "int";
                default:
                    return "string";

            }
        }

        #endregion

        #region 列辅助方法

        /// <summary>
        /// 获取所有列如：id,name,age
        /// </summary>
        /// <param name="cols">列数组</param>
        /// <returns></returns>
        public static string GetColunsString(ColumnAttribute[] cols)
        {
            return GetColunsString(cols, "", false);
        }

        /// <summary>
        /// 获取所有列如：id,name,age
        /// </summary>
        /// <param name="cols">列数组</param>
        /// <param name="removeIdentity">是否称除自动列</param>
        /// <returns></returns>
        public static string GetColunsString(ColumnAttribute[] cols, bool removeIdentity)
        {
            return GetColunsString(cols, "", removeIdentity);
        }

        /// <summary>
        /// 获取所有列如：@id,@name,@age
        /// </summary>
        /// <param name="cols">列数组</param>
        /// <returns></returns>
        public static string GetValuesString(ColumnAttribute[] cols)
        {
            return GetColunsString(cols, "@", false);
        }

        /// <summary>
        /// 获取所有列如：@id,@name,@age
        /// </summary>
        /// <param name="cols">列数组</param>
        /// <param name="removeIdentity">是否称除自动列</param>
        /// <returns></returns>
        public static string GetValuesString(ColumnAttribute[] cols, bool removeIdentity)
        {
            return GetColunsString(cols, "@", removeIdentity);
        }

        /// <summary>
        /// 获取所有列如：id,name,age
        /// </summary>
        /// <param name="cols">列数组</param>
        /// <param name="tagFix">列名的前缀</param>
        /// <param name="removeIdentity">是否称除自动列</param>
        /// <returns></returns>
        private static string GetColunsString(ColumnAttribute[] cols, string tagFix, bool removeIdentity)
        {
            string source = "";
            foreach (ColumnAttribute c in cols)
            {
                if (c.AutoIncrement && removeIdentity)
                    continue;
                source += tagFix + c.Name + ",";
            }
            return source.Remove(source.Length - 1);
        }

        #endregion

        #region SqlParameter辅助方法

        /// <summary>
        /// 生成列的SqlParameter声明如：new SqlParameter("@Title", SqlDbType.NVarChar,64),
        /// </summary>
        /// <param name="cols">列列表</param>
        /// <returns></returns>
        public static string GetSqlParameterDeclare(ColumnAttribute[] cols)
        {
            StringBuilder source = new StringBuilder();

            foreach (ColumnAttribute c in cols)
            {
                source.AppendLine(string.Format("new SqlParameter(\"@{0}\", SqlDbType.{1}{2}),", c.Name, c.Type.ToString(), c.Length > 0 ? "," + c.Length.ToString() : ""));
            }

            return source.Remove(source.Length - 1, 1).ToString();
        }

        /// <summary>
        /// 生成列的SqlParameter声明如：new SqlParameter("@Title", SqlDbType.NVarChar,64),
        /// </summary>
        /// <param name="cols">列列表</param>
        /// <param name="removeIdentity">是否删除自动列</param>
        /// <returns></returns>
        public static string GetSqlParameterDeclare(ColumnAttribute[] cols, bool removeIdentity)
        {
            StringBuilder source = new StringBuilder();

            foreach (ColumnAttribute c in cols)
            {
                if (c.AutoIncrement && removeIdentity)
                    continue;
                source.AppendLine(string.Format("new SqlParameter(\"@{0}\", SqlDbType.{1}{2}),", c.Name, c.Type.ToString(), c.Length > 0 ? "," + c.Length.ToString() : ""));
            }

            return source.Remove(source.Length - 1, 1).ToString();
        }

        /// <summary>
        /// 生成列的SqlParameter的赋值:如parameters[0].Value = model.Title;
        /// </summary>
        /// <param name="cols">列列表</param>
        /// <returns></returns>
        public static string GetSqlParameterValue(ColumnAttribute[] cols)
        {
            //StringBuilder source = new StringBuilder();

            //for (int idx = 0; idx < cols.Length; idx++)
            //{
            //    //source.AppendLine(string.Format("parameters[{0}].Value = model.{1};", idx, cols[idx].Name.Trim()));
            //    source.AppendLine(SubSiteSetValue(idx, cols[idx].Name.Trim()));
            //}
            //return source.ToString();
            return GetSqlParameterValue(cols, false);
        }

        /// <summary>
        /// 生成列的SqlParameter的赋值:如parameters[0].Value = model.Title;
        /// </summary>
        /// <param name="cols">列列表</param>
        /// <param name="removeIdentity"></param>
        /// <returns></returns>
        public static string GetSqlParameterValue(ColumnAttribute[] cols, bool removeIdentity)
        {
            StringBuilder source = new StringBuilder();
            source.AppendLine("string __SubSite = Common.Globals.getCookie(\"SubSite\");");
            source.AppendLine("int __ss = 0 ;");
            source.AppendLine(" if ( !string.IsNullOrEmpty(__SubSite) && __SubSite.ToInt32() != 0){__ss = __SubSite.ToInt32() ;}");
            source.AppendLine();

            int colIdx = 0;

            for (int idx = 0; idx < cols.Length; idx++)
            {
                if (cols[idx].AutoIncrement && removeIdentity)
                    continue;
                //source.AppendLine(string.Format("parameters[{0}].Value = model.{1};", colIdx, cols[idx].Name.Trim()));
                source.AppendLine(SubSiteSetValue(colIdx, cols[idx].Name.Trim()));
                colIdx++;
            }
            return source.ToString();
        }

        /// <summary>
        /// 分站动态赋值
        /// </summary>
        private static string SubSiteSetValue(int colIdx, string columnName)
        {
            StringBuilder Output = new StringBuilder();

            if (columnName.ToLower() == "CityId".ToLower())
                Output.AppendFormat("parameters[" + colIdx + "].Value = SQL.IsOpenSubSite ? __ss : model." + columnName + " ;");
            else
                Output.AppendFormat("parameters[" + colIdx + "].Value =  model." + columnName + " ;");

            return Output.ToString();
        }

        /// <summary>
        /// 生成Update时所需要字段
        /// </summary>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static string GetUpdateColumns(ColumnAttribute[] cols)
        {
            StringBuilder source = new StringBuilder();
            string where = "";

            foreach (ColumnAttribute c in cols)
            {
                if (c.PrimaryKey)
                {
                    where = GetPrimaryKeyWhere(c);
                    continue;
                }

                source.AppendLine(string.Format("strSql.Append(\"{0}=@{0},\");", c.Name));
            }

            source.AppendLine(where);

            string temp = source.ToString().ToString();

            return temp.Remove(temp.LastIndexOf(","), 1).ToString();
        }

        /// <summary>
        /// 生成以主键为条件的语句
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string GetPrimaryKeyWhere(ColumnAttribute col)
        {
            return string.Format("strSql.Append(\" where {0}=@{0} \");", col.Name);
        }


        #endregion

    }
}
