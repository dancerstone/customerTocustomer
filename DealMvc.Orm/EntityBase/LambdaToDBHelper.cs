using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealMvc.Orm
{
    public class LambdaToDBHelper<T> where T : EntityBase<T>, new()
    {

        #region DBHelper

        #region Add

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model">当前实例</param>
        /// <param name="_SqlTranExtensions">事物对象</param>
        /// <returns></returns>
        public static int Add(T model, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { return Orm.EntityCore<T>.Add(model, _SqlTranExtensions); }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="_SqlTranExtensions">事物对象</param>
        /// <returns></returns>
        public int Add(SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { return Add(this as T, _SqlTranExtensions); }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model">当前实例</param>
        /// <returns></returns>
        public static int Add(T model)
        { return Orm.EntityCore<T>.Add(model); }

        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        public int Add()
        { return Add(this as T); }

        #endregion

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="primaryKeyValue">主键</param>
        /// <param name="_SqlTranExtensions">事物对象</param>
        public static void Delete(int? primaryKeyValue, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { Orm.EntityCore<T>.Delete(primaryKeyValue, _SqlTranExtensions); }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="_SqlTranExtensions">事物对象</param>
        public void Delete(SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { Delete(((T)this).id, _SqlTranExtensions); }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="primaryKeyValue">主键</param>
        public static void Delete(int? primaryKeyValue)
        { Orm.EntityCore<T>.Delete(primaryKeyValue); }

        /// <summary>
        /// Delete
        /// </summary>
        public void Delete()
        { Delete(((T)this).id); }
        /*
        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        public static void DeleteWhere(string where, System.Data.SqlClient.SqlParameter[] cmdParms)
        { Orm.EntityCore<Member>.DeleteWhere(where, cmdParms); }

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="_SqlTranExtensions">事物对象</param>
        public static void DeleteWhere(string where, System.Data.SqlClient.SqlParameter[] cmdParms, DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { Orm.EntityCore<Member>.DeleteWhere(where, cmdParms, _SqlTranExtensions); }

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        public static void DeleteWhere(string where, params object[] objects)
        { Orm.EntityCore<Member>.DeleteWhere(where, objects); }

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="_SqlTranExtensions">事物对象</param>
        public static void DeleteWhere(string where, object[] objects, DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { Orm.EntityCore<Member>.DeleteWhere(where, objects, _SqlTranExtensions); }
        */
        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model">当前实例</param>
        /// <param name="_SqlTranExtensions">事物对象</param>
        public static void Update(T model, DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { Orm.EntityCore<T>.Update(model, _SqlTranExtensions); }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="_SqlTranExtensions">事物对象</param>
        public void Update(DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        { Update(this as T, _SqlTranExtensions); }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model">当前实例</param>
        public static void Update(T model)
        { Orm.EntityCore<T>.Update(model); }

        /// <summary>
        /// Update
        /// </summary>
        public void Update()
        { Update(this as T); }

        #endregion

        #region Exists

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="primaryKeyValue">主键</param>
        /// <returns></returns>
        public static bool Exists(object primaryKeyValue)
        { return Orm.EntityCore<T>.Exists(primaryKeyValue); }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        //public static bool Exists(string strWhere, System.Data.SqlClient.SqlParameter[] cmdParms)
        //{ return Orm.EntityCore<Member>.Exists(strWhere, cmdParms); }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="KeyNames">字段名</param>
        /// <param name="_SqlDbTypes">字段类型</param>
        /// <param name="Values">值</param>
        /// <returns></returns>
        //public static bool Exists(string[] KeyNames, System.Data.SqlDbType[] _SqlDbTypes, object[] Values)
        //{ return Orm.EntityCore<Member>.Exists(KeyNames, _SqlDbTypes, Values); }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        //public static bool Exists(string strWhere, params object[] objects)
        //{ return Orm.EntityCore<Member>.Exists(strWhere, objects); }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="KeyNames">字段名</param>
        /// <param name="objects">字段类型</param>
        /// <returns></returns>
        //public static bool Exists(string[] KeyNames, object[] objects)
        //{ return Orm.EntityCore<Member>.Exists(KeyNames, objects); }

        #endregion

        #region GetList
        /*
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static System.Data.DataSet GetList(string strWhere, System.Data.SqlClient.SqlParameter[] cmdParms)
        { return Orm.EntityCore<Member>.GetList(strWhere, cmdParms); }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="topRecord">条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public static System.Data.DataSet GetList(int topRecord, string strWhere, System.Data.SqlClient.SqlParameter[] cmdParms, string filedOrder)
        { return Orm.EntityCore<Member>.GetList(topRecord, strWhere, cmdParms, filedOrder); }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        public static System.Data.DataSet GetList(string strWhere, params object[] objects)
        { return Orm.EntityCore<Member>.GetList(strWhere, objects); }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="topRecord">条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public static System.Data.DataSet GetList(int topRecord, string strWhere, object[] objects, string filedOrder)
        { return Orm.EntityCore<Member>.GetList(topRecord, strWhere, objects, filedOrder); }
        */
        #endregion

        #region GetModel

        /// <summary>
        /// GetModel - 不可能返回null
        /// </summary>
        /// <param name="primaryKeyValue">主键</param>
        /// <returns></returns>
        public static T GetModel(int? primaryKeyValue)
        { return Orm.EntityCore<T>.GetModel(primaryKeyValue); }

        /// <summary>
        /// GetModel
        /// </summary>
        /// <param name="primaryKeyValue">主键</param>
        /// <param name="canNull">是否能返回null</param>
        /// <returns></returns>
        public static T GetModel(int? primaryKeyValue, bool canNull)
        { return Orm.EntityCore<T>.GetModel(primaryKeyValue, canNull); }

        /// <summary>
        /// GetModel - 不可能返回null
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        //public static Member GetModel(string where, System.Data.SqlClient.SqlParameter[] cmdParms)
        //{ return Orm.EntityCore<Member>.GetModel(where, cmdParms); }

        /// <summary>
        /// GetModel
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="canNull">是否能返回null</param>
        /// <returns></returns>
        //public static Member GetModel(string where, System.Data.SqlClient.SqlParameter[] cmdParms, bool canNull)
        //{ return Orm.EntityCore<Member>.GetModel(where, cmdParms, canNull); }

        /// <summary>
        /// GetModel - 不可能返回null
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        //public static Member GetModel(string where, params object[] objects)
        //{ return Orm.EntityCore<Member>.GetModel(where, objects); }

        /// <summary>
        /// GetModel
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="canNull">是否能返回null</param>
        /// <returns></returns>
        //public static Member GetModel(string where, object[] objects, bool canNull)
        //{ return Orm.EntityCore<Member>.GetModel(where, objects, canNull); }

        #endregion

        #region GetModelList
        /*
        /// <summary>
        /// GetModelList
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static List2<Member> GetModelList(string where, System.Data.SqlClient.SqlParameter[] cmdParms)
        { return Orm.EntityCore<Member>.GetModelList(where, cmdParms); }

        /// <summary>
        /// GetModelList
        /// </summary>
        /// <param name="topRecord">条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public static List2<Member> GetModelList(int topRecord, string strWhere, System.Data.SqlClient.SqlParameter[] cmdParms, string filedOrder)
        { return Orm.EntityCore<Member>.GetModelList(topRecord, strWhere, cmdParms, filedOrder); }

        /// <summary>
        /// GetModelList
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="objects">参数</param>
        /// <returns></returns>
        public static List2<Member> GetModelList(string where, params object[] objects)
        { return Orm.EntityCore<Member>.GetModelList(where, objects); }

        /// <summary>
        /// GetModelList
        /// </summary>
        /// <param name="topRecord">条数</param>
        /// <param name="strWhere">条件</param>
        /// <param name="objects">参数</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public static List2<Member> GetModelList(int topRecord, string strWhere, object[] objects, string filedOrder)
        { return Orm.EntityCore<Member>.GetModelList(topRecord, strWhere, objects, filedOrder); }
        */
        #endregion

        #endregion DBHelper

        #region DBHelper-Lambda

        #region Add



        #endregion

        #region Delete

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        public static void DeleteWhere(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            Orm.EntityCore<T>.DeleteWhere(_SP.SQL, _SP.Parameter.Re_SqlParameter());
        }

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="_SqlTranExtensions">事务对象</param>
        public static void DeleteWhere(System.Linq.Expressions.Expression<Func<T, bool>> func, DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            Orm.EntityCore<T>.DeleteWhere(_SP.SQL, _SP.Parameter.Re_SqlParameter(), _SqlTranExtensions);
        }

        #endregion

        #region Exists

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <returns></returns>
        public static bool Exists(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            return Orm.EntityCore<T>.Exists(_SP.SQL, _SP.Parameter.Re_SqlParameter());
        }

        #endregion

        #region GetList

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <returns></returns>
        public static System.Data.DataSet GetList(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            return Orm.EntityCore<T>.GetList(_SP.SQL, _SP.Parameter.Re_SqlParameter());
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="Top">条数</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="funcOrder">Lambda形式排序</param>
        /// <returns></returns>
        public static System.Data.DataSet GetList(int Top, System.Linq.Expressions.Expression<Func<T, bool>> func, System.Linq.Expressions.Expression<Func<T, bool>> funcOrder)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPOrder = LambdaToSQL.GetWhere<T>(funcOrder).CheckSQLSort(SQLSort.SQLOrder);
            _SP.Parameter.CanShu.AddAsNvarchar(_SPOrder.Parameter.Re_SqlParameter());
            return Orm.EntityCore<T>.GetList(Top, _SP.SQL, _SP.Parameter.Re_SqlParameter(), _SPOrder.SQL);
        }

        #endregion

        #region GetModel

        /// <summary>
        /// GetModel - 不可能返回null
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <returns></returns>
        public static T GetModel(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            return Orm.EntityCore<T>.GetModel(_SP.SQL, _SP.Parameter.Re_SqlParameter());
        }

        /// <summary>
        /// GetModel
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="canNull">是否能返回null</param>
        /// <returns></returns>
        public static T GetModel(System.Linq.Expressions.Expression<Func<T, bool>> func, bool canNull)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            return Orm.EntityCore<T>.GetModel(_SP.SQL, _SP.Parameter.Re_SqlParameter(), canNull);
        }

        #endregion

        #region GetModelList

        /// <summary>
        /// GetModelList
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <returns></returns>
        public static List2<T> GetModelList(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            return Orm.EntityCore<T>.GetModelList(_SP.SQL, _SP.Parameter.Re_SqlParameter());
        }

        /// <summary>
        /// GetModelList
        /// </summary>
        /// <param name="Top">条数</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="funcOrder">Lambda形式排序</param>
        /// <returns></returns>
        public static List2<T> GetModelList(int Top, System.Linq.Expressions.Expression<Func<T, bool>> func, System.Linq.Expressions.Expression<Func<T, bool>> funcOrder)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPOrder = LambdaToSQL.GetWhere<T>(funcOrder).CheckSQLSort(SQLSort.SQLOrder);
            _SP.Parameter.CanShu.AddAsNvarchar(_SPOrder.Parameter.Re_SqlParameter());
            return Orm.EntityCore<T>.GetModelList(Top, _SP.SQL, _SP.Parameter.Re_SqlParameter(), _SPOrder.SQL);
        }

        #endregion

        #region GetListAndFileds

        /// <summary>
        /// GetListAndFileds
        /// </summary>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="funcFields">Lambda形式返回字段</param>
        /// <returns></returns>
        public static System.Data.DataSet GetListAndFileds(System.Linq.Expressions.Expression<Func<T, bool>> func, System.Linq.Expressions.Expression<Func<T, bool>> funcFields)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPFields = LambdaToSQL.GetWhere<T>(funcFields).CheckSQLSort(SQLSort.SQLFields);
            return Orm.EntityCore<T>.GetListAndFileds(_SP.SQL, _SP.Parameter.Re_SqlParameter(), _SPFields.SQL);
        }

        /// <summary>
        /// GetListAndFileds
        /// </summary>
        /// <param name="Top">条数</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="funcFields">Lambda形式返回字段</param>
        /// <returns></returns>
        public static System.Data.DataSet GetListAndFileds(int Top, System.Linq.Expressions.Expression<Func<T, bool>> func, System.Linq.Expressions.Expression<Func<T, bool>> funcFields)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPFields = LambdaToSQL.GetWhere<T>(funcFields).CheckSQLSort(SQLSort.SQLFields);
            return Orm.EntityCore<T>.GetListAndFileds(Top, _SP.SQL, _SP.Parameter.Re_SqlParameter(), _SPFields.SQL);
        }

        /// <summary>
        /// GetListAndFileds
        /// </summary>
        /// <param name="Top">条数</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="funcOrder">Lambda形式排序</param>
        /// <param name="funcFields">Lambda形式返回字段</param>
        /// <returns></returns>
        public static System.Data.DataSet GetListAndFileds(int Top, System.Linq.Expressions.Expression<Func<T, bool>> func, System.Linq.Expressions.Expression<Func<T, bool>> funcOrder, System.Linq.Expressions.Expression<Func<T, bool>> funcFields)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
            SQLAndParameter _SPOrder = LambdaToSQL.GetWhere<T>(funcOrder).CheckSQLSort(SQLSort.SQLOrder);
            SQLAndParameter _SPFields = LambdaToSQL.GetWhere<T>(funcFields).CheckSQLSort(SQLSort.SQLFields);
            _SP.Parameter.CanShu.AddAsNvarchar(_SPOrder.Parameter.Re_SqlParameter());
            return Orm.EntityCore<T>.GetListAndFileds(Top, _SP.SQL, _SP.Parameter.Re_SqlParameter(), _SPOrder.SQL, _SPFields.SQL);
        }

        #endregion

        #region GetPageList
        /*
        /// <summary>
        /// GetPageList
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页索引</param>
        /// <param name="func">Lambda形式条件和参数</param>
        /// <param name="funcOrder">Lambda形式排序</param>
        /// <param name="funcFields">Lambda形式返回字段</param>
        /// <returns></returns>
        public static System.Collections.ArrayList GetPageList(int PageSize, int PageIndex, System.Linq.Expressions.Expression<Func<Member, bool>> func, System.Linq.Expressions.Expression<Func<Member, bool>> funcOrder, System.Linq.Expressions.Expression<Func<Member, bool>> funcFields)
        {
            SQLAndParameter _SP = LambdaToSQL.GetWhere<Member>(func).CheckSQLSort( SQLSort.SQLWhere);
            SQLAndParameter _SPOrder = LambdaToSQL.GetWhere<Member>(funcOrder).CheckSQLSort(SQLSort.SQLOrder);
            SQLAndParameter _SPFields = LambdaToSQL.GetWhere<Member>(funcFields).CheckSQLSort(SQLSort.SQLFields);
            _SP.Parameter.CanShu.Add(_SPOrder.Parameter.Re_SqlParameter());
            return Orm.EntityCore<Member>.GetPageList(PageSize, PageIndex, _SP.SQL, _SP.Parameter.Re_SqlParameter(), _SPOrder.SQL, _SPFields.SQL);
        }
        */
        #endregion

        #endregion
    }
}
