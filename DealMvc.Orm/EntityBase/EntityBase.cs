using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using ExceptionEx;
using System.Linq.Expressions;

namespace DealMvc.Orm
{
    public class EntityBaseDataCache
    {
        public static Dictionary<string, List<CheckAttribute>> CheckAttributeListCache = null;
        public static Dictionary<string, PropertyInfo[]> propertyAttributeCache = null;
        static EntityBaseDataCache()
        {
            CheckAttributeListCache = new Dictionary<string, List<CheckAttribute>>();
            propertyAttributeCache = new Dictionary<string, PropertyInfo[]>();
        }

    }

    /// <summary>
    /// Model基类
    /// </summary>
    public class EntityBase<T> : LambdaToDBHelper<T> where T : EntityBase<T>, new()
    {
        [Column("id", System.Data.SqlDbType.Int)]
        [Check("id", "id", typeof(int))]
        public virtual int? id { set; get; }

        #region 属性Null

        private bool _isnull = true;
        /// <summary>
        /// 是否为null
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (_DataRow != null && !string.IsNullOrEmpty(_DataRow["id"].ToString2())) return false;
                return _isnull;
            }
        }

        #endregion

        #region 数据库->赋值

        /// <summary>
        /// 表结构
        /// </summary>
        public List<PropertyInfo2> _Prlist;
        /// <summary>
        /// 数据行
        /// </summary>
        public Dictionary<string, object> _DataRow;
        /// <summary>
        /// 赋值
        /// </summary>
        public void SetPValue(string ColumnName)
        {
            if (_Prlist != null && _DataRow != null)
            {
                //_Prlist.ForEach(p => { if (_DataRow[p.Name] != DBNull.Value) p.SetValue(this, _DataRow[p.Name], null); }); 
                PropertyInfo2 P = _Prlist.Find(p => p.Name == ColumnName);
                if (P != null && _DataRow[ColumnName] != DBNull.Value)
                {
                    PropertyInfo _PropertyInfo = this.GetType().GetProperty(ColumnName);
                    //限制小数点
                    ColumnAttribute[] cas = _PropertyInfo.GetCustomAttributes(typeof(ColumnAttribute), false) as ColumnAttribute[];
                    object v = _DataRow[ColumnName];
                    if (cas.Length > 0)
                    {
                        v = new Common.Net.CanShu().DealValue(v, cas[0].Type, v);
                    }
                    //限制小数点End
                    _PropertyInfo.SetValue(this, _DataRow[ColumnName], null);
                    SetDataEmpty(ColumnName);
                }
            }
        }
        /// <summary>
        /// 清空Data该格字段数据
        /// </summary>
        /// <param name="ColumnName"></param>
        private void SetDataEmpty(string ColumnName)
        {
            if (_DataRow != null && _DataRow[ColumnName] != DBNull.Value)
                _DataRow[ColumnName] = DBNull.Value;
            _isnull = false;
        }

        #endregion

        #region 赋值验证

        List<CheckAttribute> CheckAttributeList;
        private PropertyInfo[] propertyAttribute;
        /// <summary>
        /// 
        /// </summary>
        public EntityBase()
        {
            Type type = this.GetType();
            string Key = type.Name;

            //缓存propertyAttribute
            if (EntityBaseDataCache.propertyAttributeCache.ContainsKey(Key))
                propertyAttribute = EntityBaseDataCache.propertyAttributeCache[Key];
            else
            {
                propertyAttribute = type.GetProperties();
                EntityBaseDataCache.propertyAttributeCache[Key] = propertyAttribute;
            }

            //缓存CheckAttributeList
            if (EntityBaseDataCache.CheckAttributeListCache.ContainsKey(Key))
                CheckAttributeList = EntityBaseDataCache.CheckAttributeListCache[Key];
            else
            {
                CheckAttributeList = new List<CheckAttribute>();
                foreach (PropertyInfo pi in propertyAttribute)
                {
                    CheckAttribute[] cas = pi.GetCustomAttributes(typeof(CheckAttribute), false) as CheckAttribute[];
                    if (cas.Length > 0)
                        CheckAttributeList.Add(cas[0]);
                }
                EntityBaseDataCache.CheckAttributeListCache[Key] = CheckAttributeList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="PropertyName"></param>
        protected void Authentication(object val, string PropertyName)
        {
            for (int i = 0; i < CheckAttributeList.Count; i++)
            {
                CheckAttribute _ColumnAttribute = CheckAttributeList[i];
                if (_ColumnAttribute.Name != PropertyName) { continue; }

                //if (val == null) { throw new MyExceptionMessageBox(_ColumnAttribute.Name + "(" + _ColumnAttribute.Des + ")" + " 不允许为null"); }

                string output = DoCheck(val.ToString2(), _ColumnAttribute);
                if (!string.IsNullOrEmpty(output))
                {
                    //抛出异常 
                    throw new MyExceptionMessageBox(output);
                }
            }
            SetDataEmpty(PropertyName);
        }

        private Regex RegEmail = new Regex(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$");//邮箱
        private Regex RegPhone = new Regex(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");//电话号码正则表达式（支持手机号码，3-4位区号，7-8位直播号码，1－4位分机号）
        private Regex RegMoney = new Regex(@"^-?\d+(\.\d+)?$");//货币
        private Regex RegNumber = new Regex(@"^-?\d+$");//数字-可为负数
        private Regex RegChinese = new Regex("[\u4e00-\u9fa5]");//中文

        private string msg1 = "不能为空";
        private string msg2 = "长度不能小于";
        private string msg3 = "长度不能大于";
        private string msg4 = "不是一个合法的邮箱地址格式";
        private string msg5 = "不是一个合法的电话号码格式";
        private string msg6 = "必须为数字(支持小数点)";
        private string msg7 = "不能包含中文";
        private string msg8 = "必须为数字";
        private string msg9 = "不能小于";
        private string msg10 = "不能大于";

        private string DoCheck(string value, CheckAttribute _CheckAttribute)
        {
            string output = "";

            if (string.IsNullOrEmpty(value))//为空
            {
                if (_CheckAttribute.NotEmpty)
                { output = _CheckAttribute.Des + " " + msg1; }

                return output;
            }

            switch (_CheckAttribute.Type.Name.ToLower().Replace("?", ""))
            {
                case "string":
                    if (value.JDoubleByteToTwoByte().Length < _CheckAttribute.Min)//最小
                    {
                        output = _CheckAttribute.Des + " " + msg2 + _CheckAttribute.Min.ToString();
                    }
                    else if (value.JDoubleByteToTwoByte().Length > _CheckAttribute.Max)//最大
                    {
                        output = _CheckAttribute.Des + " " + msg3 + _CheckAttribute.Max.ToString();
                    }
                    else if (!RegEmail.IsMatch(value) && _CheckAttribute.IsEmail)//邮箱
                    {
                        output = _CheckAttribute.Des + " " + msg4;
                    }
                    else if (!RegPhone.IsMatch(value) && _CheckAttribute.IsPhone)//电话
                    {
                        output = _CheckAttribute.Des + " " + msg5;
                    }
                    else if (!RegMoney.IsMatch(value) && _CheckAttribute.IsMoney)//货币
                    {
                        output = _CheckAttribute.Des + " " + msg6;
                    }
                    else if (RegChinese.IsMatch(value) && _CheckAttribute.NotSupportChinese)//中文
                    {
                        output = _CheckAttribute.Des + " " + msg7;
                    }
                    else if (!RegNumber.IsMatch(value) && _CheckAttribute.IsNumber)//数字
                    {
                        output = _CheckAttribute.Des + " " + msg8;
                    }

                    break;
                case "int":
                case "int32":
                case "int16":
                    if (!RegNumber.IsMatch(value))
                    {
                        output = _CheckAttribute.Des + " " + msg8;
                    }
                    else if (int.Parse(value) < _CheckAttribute.Min)//最小值
                    {
                        output = _CheckAttribute.Des + " " + msg9 + _CheckAttribute.Min.ToString();
                    }
                    else if (int.Parse(value) > _CheckAttribute.Max)//最大值
                    {
                        output = _CheckAttribute.Des + " " + msg10 + _CheckAttribute.Max.ToString();
                    }
                    break;
                case "double":
                    if (!RegMoney.IsMatch(value) && _CheckAttribute.IsMoney)//货币
                    {
                        output = _CheckAttribute.Des + " " + msg6;
                    }
                    break;
                default:
                    break;
            }

            return output;
        }

        #endregion

        #region 获得所有字段的值

        /// <summary>
        /// 获得所有字段的值
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList getValues()
        {
            System.Collections.ArrayList _ArrayList = new System.Collections.ArrayList();

            #region MyRegion

            //foreach (PropertyInfo pi in propertyAttribute)
            //{
            //    try
            //    {
            //        CheckAttribute[] cas = pi.GetCustomAttributes(typeof(CheckAttribute), false) as CheckAttribute[];

            //        string des = pi.Name;
            //        if (cas.Length > 0)
            //        {
            //            des += "[" + cas[0].Des.ToString() + "]";
            //        }
            //        des += ": ";

            //        _ArrayList.Add(des + pi.GetValue(this, null).ToString());
            //    }
            //    catch { }
            //}

            #endregion

            return _ArrayList;
        }

        #endregion

        #region 外键查询

        /// <summary>
        /// 外键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public T GM<T>(int id, ref T m) where T : EntityBase<T>, new()
        {
            if (m == null || DealMvc.WebCache.WebCache.IsUseWebCache)
                m = Orm.EntityCore<T>.GetModel(id, false);
            return m;
        }

        /// <summary>
        ///  条件查询
        /// </summary>
        /// <returns></returns>
        public T GM<T>(string where, object[] obj, ref T m) where T : EntityBase<T>, new()
        {
            if (m == null || DealMvc.WebCache.WebCache.IsUseWebCache)
                m = Orm.EntityCore<T>.GetModel(where, obj, false);
            return m;
        }

        /// <summary>
        ///  条件查询
        /// </summary>
        /// <returns></returns>
        public T GM<T>(System.Linq.Expressions.Expression<Func<T, bool>> func, ref T m) where T : EntityBase<T>, new()
        {
            if (m == null || DealMvc.WebCache.WebCache.IsUseWebCache)
            {
                SQLAndParameter _SP = LambdaToSQL.GetWhere<T>(func).CheckSQLSort(SQLSort.SQLWhere);
                m = Orm.EntityCore<T>.GetModel(_SP.SQL, _SP.Parameter.Re_SqlParameter(), false);
            }
            return m;
        }

        #endregion

    }
}
