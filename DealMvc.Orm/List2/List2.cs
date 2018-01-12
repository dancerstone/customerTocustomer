using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace DealMvc
{
    /// <summary>
    /// ModelList缓冲对象
    /// </summary>
    public class List2<EntityObject> where EntityObject : DealMvc.Orm.EntityBase<EntityObject>, new()
    {
        private int? topRecord;
        private string strWhere;
        private SqlParameter[] cmdParms;
        private string filedOrder;
        private bool canEmpty;

        public List2(string strWhere, SqlParameter[] cmdParms, bool canEmpty)
        {
            this.strWhere = strWhere;
            this.cmdParms = cmdParms;
            this.canEmpty = canEmpty;
        }
        public List2(int topRecord, string strWhere, SqlParameter[] cmdParms, string filedOrder)
        {
            this.topRecord = topRecord;
            this.strWhere = strWhere;
            this.cmdParms = cmdParms;
            this.filedOrder = filedOrder;
        }

        private List<EntityObject> _list;
        /// <summary>
        /// 获得数据库List集合
        /// </summary>
        public List<EntityObject> List
        {
            get
            {
                if (_list != null && !DealMvc.WebCache.WebCache.IsUseWebCache)
                    return _list;
                if (topRecord == null)
                {
                    ArrayList Arr = new ArrayList();
                    Arr.Add(typeof(EntityObject).Name.ToString2());
                    Arr.Add("List");
                    Arr.Add(strWhere);
                    Arr.Add(Orm.EntityCore<EntityObject>.GetSqlParametersString(cmdParms));

                    object obj = Orm.EntityCore<EntityObject>.AddModelWebCache(Arr.JArrayListToString(Orm.EntityCore<EntityObject>.Sign, true), null);
                    if (obj == null)
                    {
                        List<EntityObject> _List = (List<EntityObject>)Orm.EntityCore<EntityObject>.InvokeMethod("GetModels", new object[] { Orm.SQL.DealSQL<EntityObject>(strWhere), cmdParms }, new Type[] { typeof(string), typeof(SqlParameter[]) });
                        //if (_List.Count < 1 && !canEmpty) _List.Add(new EntityObject());
                        if (_List == null) _List = new List<EntityObject>();
                        _list = (List<EntityObject>)Orm.EntityCore<EntityObject>.AddModelWebCache(Arr.JArrayListToString(Orm.EntityCore<EntityObject>.Sign, true), _List);
                    }
                    else { _list = (List<EntityObject>)obj; }
                }
                else
                {
                    ArrayList Arr = new ArrayList();
                    Arr.Add(typeof(EntityObject).Name.ToString2());
                    Arr.Add("List");
                    Arr.Add(topRecord ?? 0);
                    Arr.Add(strWhere);
                    Arr.Add(Orm.EntityCore<EntityObject>.GetSqlParametersString(cmdParms));
                    Arr.Add(filedOrder);

                    object obj = Orm.EntityCore<EntityObject>.AddModelWebCache(Arr.JArrayListToString(Orm.EntityCore<EntityObject>.Sign, true), null);
                    if (obj == null)
                    {
                        object obj2 = Orm.EntityCore<EntityObject>.InvokeMethod("GetModels", new object[] { topRecord ?? 0, Orm.SQL.DealSQL<EntityObject>(strWhere), cmdParms, filedOrder }, new Type[] { typeof(int), typeof(string), typeof(SqlParameter[]), typeof(string) });
                        if (obj2 == null) obj2 = new List<EntityObject>();
                        _list = (List<EntityObject>)Orm.EntityCore<EntityObject>.AddModelWebCache(Arr.JArrayListToString(Orm.EntityCore<EntityObject>.Sign, true), obj2);
                    }
                    else { _list = (List<EntityObject>)obj; }
                }
                return _list;
            }
        }

        private int? _count;
        /// <summary>
        /// 获得数据库List个数
        /// </summary>
        public int Count
        {
            get
            {
                if (_count != null && !DealMvc.WebCache.WebCache.IsUseWebCache)
                    return _count ?? 0;
                DataSet ds = null;
                if (topRecord == null)
                {
                    ds = Orm.EntityCore<EntityObject>.GetListAndFileds(strWhere, cmdParms, " count(*) ");
                }
                else
                {
                    //count(*)时,不需要排序
                    ds = Orm.EntityCore<EntityObject>.GetListAndFileds(topRecord ?? 0, strWhere, cmdParms, "", " count(*) ");
                }
                if (ds == null) return 0;
                if (ds.Tables.Count < 1) return 0;
                return ds.Tables[0].Rows[0][0].ToInt32();
            }
        }
    }

}


