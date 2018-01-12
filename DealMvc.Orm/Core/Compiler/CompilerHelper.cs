using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class CompilerHelper
    {
        /// <summary>
        /// 动态编译
        /// </summary>
        /// <param name="reffs">要加引的DLL文件名外来DLL请注意路径.\r\n默认引用system.dll、system.data.dll、system.Xml.dll、Jessica.DbHelper.dll、Jessica.Core.dll</param>
        public static Assembly Compiler<ObjectType>(string[] reffs, ref  Type _Type, ref object _Compiled, bool IsDevelop)
        {
            return new Compiler().Complier(
                reffs,
                CompilerPathHelper.GetCompilerDataAccessPath<ObjectType>(),
                new CodeTemplate<ObjectType>().GetCodeSource(),
                ref _Type,
                ref _Compiled,
                IsDevelop
                );
        }

    }
}
