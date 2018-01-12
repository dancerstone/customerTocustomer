using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Reflection.Emit;
using System.Linq;
using System.Collections;


namespace DealMvc.Orm
{
    public class PropertyInfo2
    {
        public PropertyInfo2(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class DataTableExtensions
    {

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        //public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        //{
        //#region 第一种方法

        ////第一种方法
        ////创建一个属性的列表
        //List<PropertyInfo> prlist = new List<PropertyInfo>();

        ////获取TResult的类型实例  反射的入口
        //Type t = typeof(TResult);

        ////获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表
        //Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });


        ////创建返回的集合
        //List<TResult> oblist = new List<TResult>();

        //foreach (DataRow row in dt.Rows)
        //{
        //    //创建TResult的实例
        //    TResult ob = new TResult();

        //    //找到对应的数据  并赋值
        //    prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });

        //    //放入到返回的集合中.
        //    oblist.Add(ob);
        //}
        //return oblist;
        ////*/

        //#endregion

        ///////////////////////////////////////////////////////////

        //#region 第二种方法

        //第二种方法
        // 定义集合
        //List<TResult> ts = new List<TResult>();

        //// 获得此模型的类型
        //Type type = typeof(TResult);

        //List<PropertyInfo> propertys = null;

        //foreach (DataRow dr in dt.Rows)
        //{
        //    TResult _t = new TResult();

        //    // 获得此模型的公共属性
        //    if (propertys == null)
        //    {
        //        propertys = new List<PropertyInfo>();
        //        PropertyInfo[] propertysA = _t.GetType().GetProperties();
        //        foreach (PropertyInfo pi in propertysA)
        //        {
        //            if (pi.CanWrite && dt.Columns.Contains(pi.Name))
        //                propertys.Add(pi);
        //        }
        //    }
        //    foreach (PropertyInfo _PropertyInfo in propertys)
        //    {
        //        object value = dr[_PropertyInfo.Name];
        //        if (value != DBNull.Value)
        //            _PropertyInfo.SetValue(_t, value, null);
        //    }

        //    ts.Add(_t);
        //}

        //return ts;


        //#endregion

        ///////////////////////////////////////////////////////////

        //#region 第三种方法

        //    List<TResult> list = new List<TResult>();
        //    if (dt == null || dt.Rows.Count < 1) return list;
        //    DataTableEntityBuilder<TResult> eblist = DataTableEntityBuilder<TResult>.CreateBuilder(dt.Rows[0]);
        //    foreach (DataRow info in dt.Rows) list.Add(eblist.Build(info));
        //    dt.Dispose(); dt = null;
        //    return list;

        //    //#endregion
        //}

        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : EntityBase<TResult>, new()
        {
            //第一种方法


            //获取TResult的类型实例  反射的入口
            Type t = typeof(TResult);
            if (t.BaseType != typeof(EntityBase<TResult>))
            {
                List<TResult> list = new List<TResult>();
                if (dt == null || dt.Rows.Count < 1) return list;
                DataTableEntityBuilder<TResult> eblist = DataTableEntityBuilder<TResult>.CreateBuilder(dt.Rows[0]);
                foreach (DataRow info in dt.Rows) list.Add(eblist.Build(info));
                dt.Dispose(); dt = null;
                return list;
            }
            else
            {
                //创建一个属性的列表
                List<PropertyInfo2> prlist = new List<PropertyInfo2>();
                Hashtable Ht = new Hashtable();

                //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表
                Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(new PropertyInfo2(p.Name)); });

                List<TResult> oblist = new List<TResult>();
                foreach (DataRow row in dt.Rows)
                {
                    EntityBase<TResult> ob = new TResult() as EntityBase<TResult>;
                    ob._Prlist = prlist;
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                        result.Add(dc.ColumnName, row[dc]);
                    ob._DataRow = result;
                    oblist.Add(ob as TResult);
                }
                return oblist;
            }
        }

        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {

            //创建属性的集合

            List<PropertyInfo> pList = new List<PropertyInfo>();

            //获得反射的入口

            Type type = typeof(TResult);

            DataTable dt = new DataTable();

            //把所有的public属性加入到集合 并添加DataTable的列

            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });

            foreach (var item in value)
            {

                //创建一个DataRow实例

                DataRow row = dt.NewRow();

                //给row 赋值

                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));

                //加入到DataTable

                dt.Rows.Add(row);

            }

            return dt;

        }

        public class DataTableEntityBuilder<Entity>
        {
            private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });
            private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });
            private delegate Entity Load(DataRow dataRecord);

            private Load handler;
            private DataTableEntityBuilder() { }

            public Entity Build(DataRow dataRecord)
            {
                return handler(dataRecord);
            }
            public static DataTableEntityBuilder<Entity> CreateBuilder(DataRow dataRecord)
            {
                DataTableEntityBuilder<Entity> dynamicBuilder = new DataTableEntityBuilder<Entity>();
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(Entity), new Type[] { typeof(DataRow) }, typeof(Entity), true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(typeof(Entity));
                generator.Emit(OpCodes.Newobj, typeof(Entity).GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);

                for (int i = 0; i < dataRecord.ItemArray.Length; i++)
                {
                    PropertyInfo propertyInfo = typeof(Entity).GetProperty(dataRecord.Table.Columns[i].ColumnName);
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);
                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }
        }
    }
}