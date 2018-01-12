using System;
using System.Reflection;
using System.Globalization;
using Microsoft.CSharp;
using Microsoft;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Text;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class Compiler
    {

        /// <summary>
        /// 动态编译代码到dll
        /// </summary>
        /// <param name="reference">要引用的程序集(文件形式，如：System.dll)</param>
        /// <param name="outputAssembly">程序集的输出目录</param>
        /// <param name="codeSource">要编译的代码</param>
        /// <returns></returns>
        public Assembly Complier(string[] reference, string outputAssembly, string codeSource
            ,
            ref   Type _Type, ref object _Compiled
            ,
            bool IsDevelop
            )
        {
            // 创建代码编译引擎参数
            System.Collections.Generic.IDictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string>();

            dic.Add("CompilerVersion", "v3.5");

            // 创建代码编译引擎
            CSharpCodeProvider cSCP = new CSharpCodeProvider(dic);

            // 代码编译参数
            CompilerParameters cPS = new CompilerParameters();


            cPS.ReferencedAssemblies.Add("system.dll");
            cPS.ReferencedAssemblies.Add("system.data.dll");
            cPS.ReferencedAssemblies.Add("system.Xml.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.DBUtility.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.Orm.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.SqlTranEx.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.Model.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.Common.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.ExceptionEx.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.Common.Net.dll");
            cPS.ReferencedAssemblies.Add(AppDomain.CurrentDomain.RelativeSearchPath + @"\DealMvc.WebCache.dll");

            if (reference != null)
                foreach (string s in reference)
                {
                    cPS.ReferencedAssemblies.Add(s);
                }

            cPS.GenerateExecutable = false;
            cPS.GenerateInMemory = false;
            if (!IsDevelop)
                cPS.OutputAssembly = outputAssembly;
            cPS.CompilerOptions = "/target:library /optimize";
            cPS.IncludeDebugInformation = false;

            // 代码编译结果
            CompilerResults cr = cSCP.CompileAssemblyFromSource(cPS, codeSource);

            if (cr.Errors.HasErrors)
            {
                throw (new Exception("动态编译代码到dll 出现Error"));
            }
            else
            {
                Assembly a = cr.CompiledAssembly;
                _Type = a.GetTypes()[0];
                _Compiled = a.CreateInstance(string.Format("DealMvc.DataAccess.{0}", _Type.Name));

                return cr.CompiledAssembly;
            }

        }

    }
}
