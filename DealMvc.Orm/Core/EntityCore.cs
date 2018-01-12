using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using DealMvc.SqlTranEx;
using System.Linq.Expressions;

namespace DealMvc.Orm
{

    /// <summary>
    /// 访问数据库的核心操作类
    /// </summary>
    public static class EntityCore<EntityObject> where EntityObject : EntityBase<EntityObject>, new()
    {

        #region 内部方法

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="method">要调用的方法</param>
        /// <param name="parameter">调用方法的参数</param>
        /// <returns></returns>
        public static object InvokeMethod(string method, object[] parameter)
        {
            Type t = DataAccessCache.GetType<EntityObject>();

            object tInstance = DataAccessCache.DynamicInstanceCache[t.GUID.ToString()];

            return t.GetMethod("_" + method).Invoke(tInstance, parameter);
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="method">要调用的方法</param>
        /// <param name="parameter">调用方法的参数</param>
        /// <param name="parameterTypes">调用方法的参数类型</param>
        /// <returns></returns>
        public static object InvokeMethod(string method, object[] parameter, Type[] parameterTypes)
        {
            Type t = DataAccessCache.GetType<EntityObject>();

            object tInstance = DataAccessCache.DynamicInstanceCache[t.GUID.ToString()];

            return t.GetMethod("_" + method, parameterTypes).Invoke(tInstance, parameter);
        }

        /// <summary>
        /// 通过SQL语句获取对象集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static List<EntityObject> GetModelListBySql(string sql)
        {
            List<EntityObject> list = new List<EntityObject>();

            list = DealMvc.DBUtility.DbHelperSQL.Query(sql).Tables[0].ToList<EntityObject>();

            return list;
        }
        #endregion

        #region 对外方法

        #region Add

        /// <summary>
        /// 添加一条记录,正常返回新增主键, 异常返回-1
        /// </summary>
        /// <param name="model">model实例</param>
        /// <returns>返回新增int 自动增加的主键的记录号</returns>
        public static int Add(object model)
        {
            return Add(model, null);
        }
        /// <summary>
        /// 添加一条记录,正常返回新增主键, 异常返回-1, 事务返回0
        /// </summary>
        /// <param name="model">model实例</param>
        /// <param name="_SqlTranExtensions">事务机制对象</param>
        /// <returns>返回新增int 自动增加的主键的记录号</returns>
        public static int Add(object model, SqlTranExtensions _SqlTranExtensions)
        {
            ClearWebCache();
            return (int)InvokeMethod("Add", new object[] { model, _SqlTranExtensions });
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        public static void Delete(int? primaryKeyValue)
        {
            Delete(primaryKeyValue, null);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="_SqlTranExtensions">事务机制对象</param>
        public static void Delete(int? primaryKeyValue, SqlTranExtensions _SqlTranExtensions)
        {
            ClearWebCache();
            InvokeMethod("Delete", new object[] { primaryKeyValue, _SqlTranExtensions });
        }

        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <param name="cmdParms">删除参数</param>
        public static void DeleteWhere(string where, SqlParameter[] cmdParms)
        {
            DeleteWhere(where, cmdParms, null);
        }

        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <param name="objects">删除参数</param>
        public static void DeleteWhere(string where, params object[] objects)
        {
            DeleteWhere(where, SQL.GetSQLParameter<EntityObject>(where, objects, typeof(EntityObject)).Re_SqlParameter());
        }

        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <param name="cmdParms">删除参数</param>
        /// <param name="_SqlTranExtensions">事务机制对象</param>
        public static void DeleteWhere(string where, SqlParameter[] cmdParms, SqlTranExtensions _SqlTranExtensions)
        {
            ClearWebCache();
            InvokeMethod("DeleteWhere", new object[] { SQL.DealSQL<EntityObject>(where), cmdParms, _SqlTranExtensions });
        }

        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <param name="objects">删除参数</param>
        /// <param name="_SqlTranExtensions">事务机制对象</param>
        public static void DeleteWhere(string where, object[] objects, SqlTranExtensions _SqlTranExtensions)
        {
            DeleteWhere(where, SQL.GetSQLParameter<EntityObject>(where, objects, typeof(EntityObject)).Re_SqlParameter(), _SqlTranExtensions);
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">实体</param>
        public static void Update(EntityObject model)
        {
            Update(model, null);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="_SqlTranExtensions">事务机制对象</param>
        public static void Update(EntityObject model, SqlTranExtensions _SqlTranExtensions)
        {
            ClearWebCache();
            InvokeMethod("Update", new object[] { model, _SqlTranExtensions });
        }

        #endregion

        #region Exists

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <returns>返回布尔值</returns>
        public static bool Exists(object primaryKeyValue)
        {
            return (bool)InvokeMethod("Exists", new object[] { primaryKeyValue });
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        ///<param name="strWhere">条件</param>
        ///<param name="cmdParms">参数</param>
        /// <returns>返回布尔值</returns>
        public static bool Exists(string strWhere, SqlParameter[] cmdParms)
        {
            List<EntityObject> _List = GetModelList(1, strWhere, cmdParms, "").List;
            try { if (_List.Count > 0) return true; }
            catch { }
            return false;
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        ///<param name="strWhere">条件</param>
        ///<param name="objects">参数</param>
        /// <returns>返回布尔值</returns>
        public static bool Exists(string strWhere, params object[] objects)
        {
            return Exists(strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter());
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="KeyNames">字段名数组</param>
        /// <param name="_SqlDbTypes">字段类型数组</param>
        /// <param name="Values">字段值数组</param>
        /// <returns>bool</returns>
        public static bool Exists(string[] KeyNames, System.Data.SqlDbType[] _SqlDbTypes, object[] Values)
        {
            System.Collections.ArrayList SqlWhere = new System.Collections.ArrayList();
            Common.Net.CreateParameter _C = new Common.Net.CreateParameter();
            for (int i = 0; i < KeyNames.Length; i++)
            {
                _C.CanShu.Add("@" + KeyNames[i], _SqlDbTypes[i], Values[i]);
                SqlWhere.Add(string.Format(" {0}=@{0} ", KeyNames[i]));
            }
            return Exists(SqlWhere.JArrayListToString(" and ", true), _C.Re_SqlParameter());
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="KeyNames">字段名数组</param>
        /// <param name="objects">字段值数组</param>
        /// <returns>bool</returns>
        public static bool Exists(string[] KeyNames, params object[] objects)
        {
            System.Collections.ArrayList SqlWhere = new System.Collections.ArrayList();
            for (int i = 0; i < KeyNames.Length; i++)
            {
                SqlWhere.Add(string.Format(" {0}=@{0} ", KeyNames[i]));
            }
            string where = SqlWhere.JArrayListToString(" and ", true);
            return Exists(where, SQL.GetSQLParameter<EntityObject>(where, objects, typeof(EntityObject)).Re_SqlParameter());
        }

        #endregion

        #region GetList

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere, SqlParameter[] cmdParms)
        {
            return (DataSet)InvokeMethod("GetList", new object[] { SQL.DealSQL<EntityObject>(strWhere), cmdParms }, new Type[] { typeof(string), typeof(SqlParameter[]) });
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere, params object[] objects)
        {
            return GetList(strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter());
        }


        /// <summary>
        /// 查询记录并返回指定条数
        /// </summary>
        /// <param name="topRecord">返回指定条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="filedOrder">排序参数</param>
        /// <returns></returns>
        public static DataSet GetList(int topRecord, string strWhere, SqlParameter[] cmdParms, string filedOrder)
        {
            return (DataSet)InvokeMethod("GetList", new object[] { topRecord, SQL.DealSQL<EntityObject>(strWhere), cmdParms, filedOrder }, new Type[] { typeof(int), typeof(string), typeof(SqlParameter[]), typeof(string) });
        }

        /// <summary>
        /// 查询记录并返回指定条数
        /// </summary>
        /// <param name="topRecord">返回指定条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="filedOrder">排序参数</param>
        /// <returns></returns>
        public static DataSet GetList(int topRecord, string strWhere, object[] objects, string filedOrder)
        {
            return GetList(topRecord, strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter(), filedOrder);
        }

        #endregion

        #region GetModel

        /// <summary>
        /// 返回一个实体,不可能为null
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <returns></returns>
        public static EntityObject GetModel(int? primaryKeyValue)
        {
            //return (EntityObject)InvokeMethod("GetModel", new object[] { primaryKeyValue });
            return GetModel(primaryKeyValue, false);
        }

        /// <summary>
        /// 返回一个实体,canNull==false(不能返回null,为null返回一个新实体)
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="canNull">是否可以返回null</param>
        /// <returns></returns>
        public static EntityObject GetModel(int? primaryKeyValue, bool canNull)
        {
            //EntityObject _EntityObject = (EntityObject)InvokeMethod("GetModel", new object[] { primaryKeyValue });

            //if (!canNull && _EntityObject == null) _EntityObject = new EntityObject();

            //ArrayList Arr = new ArrayList();
            //Arr.Add(typeof(EntityObject).Name.ToString2());
            //Arr.Add(primaryKeyValue);

            //return (EntityObject)AddModelWebCache(Arr.ArrayListToString(Sign, true), _EntityObject);


            ArrayList Arr = new ArrayList();
            Arr.Add(typeof(EntityObject).Name.ToString2());
            Arr.Add(primaryKeyValue);

            object obj = AddModelWebCache(Arr.JArrayListToString(Sign, true), null);
            if (obj == null)
            {
                EntityObject _EntityObject = (EntityObject)InvokeMethod("GetModel", new object[] { primaryKeyValue });

                if (!canNull && _EntityObject == null) _EntityObject = new EntityObject();

                return (EntityObject)AddModelWebCache(Arr.JArrayListToString(Sign, true), _EntityObject);
            }
            else
                return (EntityObject)obj;
        }

        /// <summary>
        /// 返回一个实体,不可能为null
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static EntityObject GetModel(string where, SqlParameter[] cmdParms)
        {
            return GetModel(where, cmdParms, false);
        }

        /// <summary>
        /// 返回一个实体,不可能为null
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        public static EntityObject GetModel(string where, params object[] objects)
        {
            return GetModel(where, SQL.GetSQLParameter<EntityObject>(where, objects, typeof(EntityObject)).Re_SqlParameter());
        }

        /// <summary>
        /// 返回一个实体,canNull==false(不能返回null,为null返回一个新实体)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="canNull">是否可以返回null</param>
        /// <returns></returns>
        public static EntityObject GetModel(string where, SqlParameter[] cmdParms, bool canNull)
        {
            List<EntityObject> _EntityObjectList = GetModelList(1, where, cmdParms, "").List;
            if (_EntityObjectList.Count == 1)
                return _EntityObjectList[0];
            else if (!canNull)
                return new EntityObject();
            else
                return null;
        }

        /// <summary>
        /// 返回一个实体,canNull==false(不能返回null,为null返回一个新实体)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="canNull">是否可以返回null</param>
        /// <returns></returns>
        public static EntityObject GetModel(string where, object[] objects, bool canNull)
        {
            return GetModel(where, SQL.GetSQLParameter<EntityObject>(where, objects, typeof(EntityObject)).Re_SqlParameter(), canNull);
        }

        #endregion

        #region GetModelList

        /// <summary>
        /// 返回实体列表,无则返回一个空列表Count==0, 注意没有返回null的情况
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static List2<EntityObject> GetModelList(string where, SqlParameter[] cmdParms)
        {
            return GetModelList(where, cmdParms, true);
        }

        /// <summary>
        /// 返回实体列表,无则返回一个空列表Count==0, 注意没有返回null的情况
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        public static List2<EntityObject> GetModelList(string where, params object[] objects)
        {
            return GetModelList(where, SQL.GetSQLParameter<EntityObject>(where, objects, typeof(EntityObject)).Re_SqlParameter());
        }

        /// <summary>
        /// 返回实体列表,无是否返回一个空由canEmpty决定
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="canEmpty">无效 - 是否可以返回null</param>
        /// <returns></returns>
        private static List2<EntityObject> GetModelList(string strWhere, SqlParameter[] cmdParms, bool canEmpty)
        {
            List2<EntityObject> m_Models = new List2<EntityObject>(strWhere, cmdParms, canEmpty);
            return m_Models;
        }

        /// <summary>
        /// 返回实体列表,无则返回一个空列表
        /// </summary>
        /// <param name="topRecord">返回指定条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="filedOrder">排序参数</param>
        /// <returns></returns>
        public static List2<EntityObject> GetModelList(int topRecord, string strWhere, SqlParameter[] cmdParms, string filedOrder)
        {
            List2<EntityObject> m_Models = new List2<EntityObject>(topRecord, strWhere, cmdParms, filedOrder);
            return m_Models;
        }

        /// <summary>
        /// 返回实体列表,无则返回一个空列表
        /// </summary>
        /// <param name="topRecord">返回指定条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="filedOrder">排序参数</param>
        /// <returns></returns>
        public static List2<EntityObject> GetModelList(int topRecord, string strWhere, object[] objects, string filedOrder)
        {
            return GetModelList(topRecord, strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter(), filedOrder);
        }

        #endregion

        #region GetListAndFileds

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="returnFields">返回的字段</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>符合条件的记录</returns>
        public static DataSet GetListAndFileds(string strWhere, SqlParameter[] cmdParms, string returnFields)
        {
            return GetListAndFileds(0, strWhere, cmdParms, "", returnFields);
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="returnFields">返回的字段</param>
        /// <param name="objects">参数</param>
        /// <returns>符合条件的记录</returns>
        public static DataSet GetListAndFileds(string strWhere, object[] objects, string returnFields)
        {
            return GetListAndFileds(strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter(), returnFields);
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="topRecord">返回多少条</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="returnFields">返回的字段</param>
        /// <returns>符合条件的记录</returns>
        public static DataSet GetListAndFileds(int topRecord, string strWhere, SqlParameter[] cmdParms, string returnFields)
        {
            return GetListAndFileds(topRecord, strWhere, cmdParms, "", returnFields);
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="topRecord">返回多少条</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="objects">参数</param>
        /// <param name="returnFields">返回的字段</param>
        /// <returns>符合条件的记录</returns>
        public static DataSet GetListAndFileds(int topRecord, string strWhere, object[] objects, string returnFields)
        {
            return GetListAndFileds(topRecord, strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter(), returnFields);
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="topRecord">返回多少条</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="cmdParms">条件参数</param>
        /// <param name="filedOrder">排序字段</param>
        /// <param name="returnFields">返回的字段</param>
        /// <returns>符合条件的记录</returns>
        public static DataSet GetListAndFileds(int topRecord, string strWhere, SqlParameter[] cmdParms, string filedOrder, string returnFields)
        {
            return (DataSet)InvokeMethod("GetListFieds", new object[] { topRecord, SQL.DealSQL<EntityObject>(strWhere), cmdParms, filedOrder, string.IsNullOrEmpty(returnFields) ? "*" : returnFields });
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="topRecord">返回多少条</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="objects">条件参数</param>
        /// <param name="filedOrder">排序字段</param>
        /// <param name="returnFields">返回的字段</param>
        /// <returns>符合条件的记录</returns>
        public static DataSet GetListAndFileds(int topRecord, string strWhere, object[] objects, string filedOrder, string returnFields)
        {
            return GetListAndFileds(topRecord, strWhere, SQL.GetSQLParameter<EntityObject>(strWhere, objects, typeof(EntityObject)).Re_SqlParameter(), filedOrder, returnFields);
        }

        #endregion

        #region GetPageList

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="PageSize">分页个数</param>
        /// <param name="PageIndex">当前分页号</param>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">条件参数</param>
        /// <param name="orderColType">排序字符串</param>
        /// <param name="Columns">赛选字段</param>
        /// <returns></returns>
        public static ArrayList GetPageList(int PageSize, int PageIndex, string strWhere, SqlParameter[] cmdParms, string orderColType, string Columns)
        {
            ArrayList Arr = new ArrayList();
            Arr.Add(typeof(EntityObject).Name.ToString2());
            Arr.Add("PageList");
            Arr.Add(PageSize);
            Arr.Add(PageIndex);
            Arr.Add(strWhere);
            Arr.Add(GetSqlParametersString(cmdParms));
            Arr.Add(orderColType);
            Arr.Add(Columns);

            object obj = AddModelWebCache(Arr.JArrayListToString(Sign, true), null);
            if (obj == null)
            {
                object obj2 = InvokeMethod("GetPageList", new object[] { PageSize, PageIndex, SQL.DealSQL<EntityObject>(strWhere), cmdParms, orderColType, Columns });

                System.Collections.ArrayList _ArrayList = (System.Collections.ArrayList)obj2;
                _ArrayList.Insert(0, ((DataSet)_ArrayList[0]).Tables[0].ToList<EntityObject>());

                return (System.Collections.ArrayList)AddModelWebCache(Arr.JArrayListToString(Sign, true), _ArrayList);
            }
            else
                return (System.Collections.ArrayList)obj;
        }

        #endregion

        #endregion

        #region 缓存方法

        /// <summary>
        /// 标识
        /// </summary>
        public static string Sign = "#";

        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="Key">键</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static object AddModelWebCache(string Key, object obj)
        {
            //DealMvc.Common.Config.SiteInfo  = DealMvc.Common.Config.ConfigInfo<DealMvc.Common.Config.SiteInfo>.Instance();
            return AddModelWebCache(Key, obj, WebCache.WebCache.WebCacheTime.ToInt32());
        }

        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="Key">键</param>
        /// <param name="obj">对象</param>
        /// <param name="_SecondsBase">缓存时间(秒)</param>
        /// <returns></returns>
        public static object AddModelWebCache(string Key, object obj, int _SecondsBase)
        {
            //是否使用缓存缓存obj对象
            if (!DealMvc.WebCache.WebCache.IsUseWebCache) return obj;
            //是否使用缓存缓存obj对象

            return DealMvc.WebCache.WebCache.JGet(Key, obj, _SecondsBase);
        }

        /// <summary>
        /// 清除 当前和 EntityObject 所有有关的缓存
        /// </summary>
        public static void ClearWebCache()
        {
            try
            {
                string TitleKey = typeof(EntityObject).Name.ToString2();
                DealMvc.WebCache.WebCache.Clear(TitleKey, Sign);
            }
            catch { }
        }

        #region 缓存辅助方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static string GetSqlParametersString(SqlParameter[] cmdParms)
        {
            if (cmdParms == null || cmdParms.Length == 0) return "";
            ArrayList arr = new ArrayList();
            foreach (SqlParameter _SqlParameter in cmdParms)
            {
                arr.Add(_SqlParameter.Value.ToString2());
            }
            return arr.JArrayListToString();
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// SQLParameter
    /// </summary>
    public class SQL
    {
        /// <summary>
        /// 处理SQLParameter
        /// </summary>
        /// <param name="s">SQL字符串</param>
        /// <param name="o">参数对象</param>
        /// <param name="t">表Type</param>
        /// <returns>Common.Net.CreateParameter</returns>
        public static Common.Net.CreateParameter GetSQLParameter<EntityObject>(string s, object[] o, Type t)
        {
            try
            {
                Common.Net.CreateParameter _C = new Common.Net.CreateParameter();
                string propertyKey = t.Name;
                PropertyInfo[] propertyAttribute = null;
                //缓存propertyAttribute
                if (EntityBaseDataCache.propertyAttributeCache.ContainsKey(propertyKey))
                    propertyAttribute = EntityBaseDataCache.propertyAttributeCache[propertyKey];
                else
                {
                    propertyAttribute = t.GetProperties();
                    EntityBaseDataCache.propertyAttributeCache[propertyKey] = propertyAttribute;
                }

                Regex _R = new Regex(@"@[_a-zA-Z][_a-zA-Z\d]*");
                MatchCollection _mc = _R.Matches(DealSQL<EntityObject>(s));
                if (_mc.Count > o.Length) throw new Exception("Parameter参数不正确");

                int i = 0;
                foreach (Match m in _mc)
                {
                    string c = m.Value.ToString2();

                    bool isBP = false;
                    foreach (PropertyInfo pi in propertyAttribute)
                    {
                        string K = c.Replace("@", "").ToLower().Replace(pi.Name.ToLower(), "");
                        if (K == "" || K.isInt())
                        {
                            ColumnAttribute[] cas = pi.GetCustomAttributes(typeof(ColumnAttribute), false) as ColumnAttribute[];
                            if (cas.Length > 0)
                            {
                                ColumnAttribute ca = cas[0];
                                _C.CanShu.Add(c, ca.Type, o[i].ToString2());
                                isBP = true;
                            }
                            break;
                        }
                    }
                    if (!isBP)//匹配不成功的都默认为 NVarChar
                        _C.CanShu.Add(c, SqlDbType.NVarChar, o[i].ToString2());
                    i++;
                }
                return _C;
            }
            catch { throw new Exception("处理Parameter参数错误"); }
        }

        /// <summary>
        /// 处理SQL语句, 关于简写
        /// </summary>
        /// <param name="s">SQL语句</param>
        /// <returns></returns>
        public static string DealSQL<ObjectType>(string s)
        {
            //s = " =Adminname and =Pwd and <>SortId1 or %Sex or -Sex% or %Sex% ";
            s = " " + s + " ";
            Regex _R = new Regex(@"\s(=|<>|%|-)([_a-zA-Z][_a-zA-Z0-9]*)%?\s");
            MatchCollection _mc = _R.Matches(s);
            foreach (Match m in _mc)
            {
                string c = m.Value;
                string a1 = m.Result("$1");
                string a2 = m.Result("$2");
                switch (a1)
                {
                    case "=":
                        s = s.Replace(c, " " + a2 + a1 + "@" + a2 + " ");
                        break;
                    case "<>":
                        s = s.Replace(c, " " + a2 + a1 + "@" + a2 + " ");
                        break;
                    case "%":
                        if (c.LastIndexOf("%") > 1) s = s.Replace(c, " " + a2 + " like " + "'%' + @" + a2 + " + '%' ");
                        else
                            s = s.Replace(c, " " + a2 + " like " + "'%' + @" + a2 + " ");
                        break;
                    case "-":
                        if (c.LastIndexOf("%") > 1) s = s.Replace(c, " " + a2 + " like " + "@" + a2 + " + '%' ");
                        break;
                }
            }

            //加入分站条件
            s = s.Trim();
            string SubSite = Common.Base.CookieClass.getCookie("SubSite");
            if (!string.IsNullOrEmpty(SubSite) && SubSite.ToInt32() != 0 && IsOpenSubSite)
            {
                TableInfo tableInfo = AttributeHelper.GetInfo<ObjectType>();
                foreach (ColumnAttribute ca in tableInfo.Columns)
                {
                    if (ca.Name.ToLower() == "SiteCityID".ToLower())
                    {
                        if (string.IsNullOrEmpty(s)) s = " 1=1 ";
                        s += " and SiteCityID=" + SubSite.ToInt32().ToString() + " ";
                        break;
                    }
                }
            }
            return s.Trim();
        }

        /// <summary>
        /// 分站功能开关
        /// </summary>
        public static bool IsOpenSubSite = false;
        /// <summary>
        /// 是否开启分站功能
        /// </summary>
        public static bool SetSubSiteStatus
        {
            set
            {
                IsOpenSubSite = value;
            }
        }
    }

}

