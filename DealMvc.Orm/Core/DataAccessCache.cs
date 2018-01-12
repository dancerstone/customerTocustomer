using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Configuration;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class DataAccessCache
    {
        private static int TryTimes = 0;//失败次数
        private static int MaxTimes = 5;//允许失败总次数

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, Type> DynamicTypeCache = null;
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, object> DynamicInstanceCache = null;

        static DataAccessCache()
        {
            Init();
        }

        /// <summary>
        /// 初始
        /// </summary>
        public static void Init()
        {
            DynamicTypeCache = new Dictionary<string, Type>();
            DynamicInstanceCache = new Dictionary<string, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="EntityObject"></typeparam>
        /// <returns></returns>
        public static Type GetType<EntityObject>()
        {
            Type TypeName = typeof(EntityObject);
            if (DynamicTypeCache.ContainsKey(TypeName.FullName))
                return DynamicTypeCache[TypeName.FullName];
            else
            {
                try
                {
                    //获取编译器实例的程序集 
                    Type _Type = null;
                    //实例
                    object _Compiled = null;

                    //获取当前网站模式是否是开发模式
                    bool IsDevelop = ConfigurationSettings.AppSettings["Mode"].ToString2().ToLower().Trim() == "Develop".ToLower().Trim();

                    //动态编译数据库问类
                    if (!System.IO.File.Exists(CompilerPathHelper.GetCompilerDataAccessPath<EntityObject>())
                        || IsDevelop)
                    {
                        CreateOneModelAccessBLL<EntityObject>(ref _Type, ref  _Compiled, IsDevelop);
                    }
                    else
                    {
                        //反射类型加载到Cache
                        _Type = Assembly.Load("Jessica.DataAccess." + TypeName.Name.Trim()).GetTypes()[0];
                        _Compiled = Activator.CreateInstance(_Type);
                    }

                    
                    DynamicTypeCache.Add(TypeName.FullName, _Type);


                    //反射实例加载到Cache
                    if (DynamicInstanceCache.ContainsKey(DynamicTypeCache[TypeName.FullName].GUID.ToString()))
                        DynamicInstanceCache.Remove(DynamicTypeCache[TypeName.FullName].GUID.ToString());

                    DynamicInstanceCache.Add(DynamicTypeCache[TypeName.FullName].GUID.ToString(), _Compiled);

                    TryTimes = 0;
                    return DynamicTypeCache[TypeName.FullName];
                }
                catch (Exception ce)
                {
                    //throw new Exception(ce.Message); 
                    Init();
                    TryTimes++;
                    if (TryTimes >= MaxTimes)
                        throw new Exception(ce.Message);
                    else
                        return GetType<EntityObject>();
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="EntityObject"></typeparam>
        public static void CreateOneModelAccessBLL<EntityObject>(ref Type _Type, ref object _Compiled, bool IsDevelop)
        {
            //单个编译
            CompilerHelper.Compiler<EntityObject>(new string[] { }, ref _Type, ref  _Compiled, IsDevelop);
            //单个编译
        }
    }
}
