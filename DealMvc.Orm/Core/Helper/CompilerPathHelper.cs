using System;
using System.Collections.Generic;

using System.Text;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class CompilerPathHelper
    {
        /// <summary>
        /// 返回要编译DataAccess的全路径
        /// </summary>
        /// <typeparam name="EntityObject"></typeparam>
        /// <returns></returns>
        public static string GetCompilerDataAccessPath<EntityObject>()
        {
            return GetBinFolderPath() + @"\Jessica.DataAccess." + typeof(EntityObject).Name.Trim() + ".dll";
        }

        /// <summary>
        /// 返加Bin目录路径（Bin后边没有"\"）
        /// </summary>
        /// <returns></returns>
        public static string GetBinFolderPath()
        {
            return AppDomain.CurrentDomain.RelativeSearchPath;
        }


    }
}
