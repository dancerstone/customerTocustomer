using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ObjectType"></typeparam>
    public class CodeTemplate<ObjectType>
    {
        #region 字段

        private string classSource = @" using System;
                                        using System.Data;
                                        using System.Text;
                                        using System.Data.SqlClient;
                                        using System.Data.OleDb;
                                        using System.Collections.Generic;
                                        using DealMvc.DBUtility;
                                        using DealMvc.Orm;
                                        using DealMvc.SqlTranEx;
                                        using DealMvc.Model;
                                        namespace DealMvc.DataAccess
                                        {
	                                        /// <summary>
	                                        /// {ModelName}数据访问类，动态生成
	                                        /// </summary>
	                                        public class {ModelName}:EntityBase<{ModelName}>
	                                        {
		                                        public {ModelName}(){}
                                                {Exists}
                                                {Add}
                                                {Update}
                                                {Delete} 
                                                {GetList}
                                                {GetList1}
                                                {GetList2}
                                                {DeleteWhere}
                                                {GetModel}
                                                {GetModel1} 
                                                {GetModel2}                                                                                                  {GetPageList}
	                                        }
                                        }
                                        ";


        private TableInfo tableInfo = null;
        private ColumnAttribute primaryKey = AttributeHelper.GetPrimaryKey<ObjectType>();

        #endregion

        #region 对外方法

        /// <summary>
        /// 
        /// </summary>
        public CodeTemplate()
        {
            tableInfo = AttributeHelper.GetInfo<ObjectType>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCodeSource()
        {
            classSource = classSource.Replace("{ModelName}", tableInfo.Table.Name)
                          .Replace("{Exists}", CreateExists())
                          .Replace("{Add}", CreateAdd())
                          .Replace("{Update}", CreateUpdate())
                          .Replace("{Delete}", CreateDelete())
                          .Replace("{DeleteWhere}", CreateDeleteWhere())
                          .Replace("{GetList}", CreateGetList())
                          .Replace("{GetList1}", CreateGetList1())
                          .Replace("{GetList2}", CreateGetListFieds())
                          .Replace("{GetModel}", CreateGetModel())
                          .Replace("{GetModel1}", CreateGetModel1())
                          .Replace("{GetModel2}", CreateGetModel2())
                          .Replace("{GetPageList}", CreateGetPageList());
            return classSource;
        }

        #endregion

        #region 调试代码

        public string tryStart()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("StringBuilder strSql = new StringBuilder();");
            output.AppendLine("try{");
            return output.ToString();
        }
        public string tryEnd()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("}catch (Exception ce) { ExceptionEx.MyExceptionLog.AddLogError(ce.Message); throw new Exception(strSql.ToString() +\" ; \" +ce.Message); }");
            return output.ToString();
        }

        #endregion

        #region 生成类方法代码

        private string CreateExists()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine(" public bool _Exists" + string.Format("({0} {1})", AttributeHelper.PrimaryKeyTypeToCs(primaryKey), primaryKey.Name) + "{");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine(string.Format("strSql.Append(\"select count(1) from {0}\");", tableInfo.Table.Name));

            //创建参数列表
            source.AppendLine(string.Format("strSql.Append(\" where {0}=@{0} \");", primaryKey.Name));

            //创建参数赋值
            source.AppendLine("SqlParameter[] parameters = {new SqlParameter(\"@" + primaryKey.Name + "\", SqlDbType." + primaryKey.Type.ToString() + ")};parameters[0].Value = " + primaryKey.Name + ";");

            //创建执行命令
            source.AppendLine("return DbHelperSQL.Exists(strSql.ToString(), parameters);");

            source.AppendLine(tryEnd());
            source.AppendLine("}");

            return source.ToString();

        }

        private string CreateAdd()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine("public int _Add(" + tableInfo.TypeFullName + " model, SqlTranExtensions  _SqlTranExtensions){");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine("strSql.Append(\"insert into " + tableInfo.Table.Name + "(\");");

            //创建列名列表
            source.AppendLine("strSql.Append(\"" + AttributeHelper.GetColunsString(tableInfo.Columns, true) + ")\"" + ");");
            source.AppendLine("strSql.Append(\" values (\");");

            //创建列名值列表
            source.AppendLine(string.Format("strSql.Append(\"{0})\");", AttributeHelper.GetValuesString(tableInfo.Columns, true)));
            source.AppendLine("strSql.Append(\";select @@IDENTITY\");");

            //创建参数列表
            source.AppendLine("SqlParameter[] parameters = {" + AttributeHelper.GetSqlParameterDeclare(tableInfo.Columns, true) + "};");

            //创建参数赋值
            source.AppendLine(AttributeHelper.GetSqlParameterValue(tableInfo.Columns, true));

            //创建执行命令
            source.AppendLine("if(_SqlTranExtensions != null){_SqlTranExtensions.Add(strSql.ToString(),parameters);return 0;}");


            //判断数据库
            source.AppendLine("object obj = null;");
            source.AppendLine(@"if(DataHelper.IsAccess){
                                obj = DbHelperOleDb.GetSingle(strSql.ToString(), DataHelper.GetOleDbParameterBySqlParameter(parameters));
                            }else{
                                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                            }");

            source.AppendLine("return obj == null ? -1 : Convert.ToInt32(obj);");


            source.AppendLine(tryEnd());
            source.AppendLine("}");

            return source.ToString();
        }

        private string CreateUpdate()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine("public void _Update(" + tableInfo.TypeFullName + "  model,  SqlTranExtensions  _SqlTranExtensions){");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine(string.Format("strSql.Append(\"update {0} set \");", tableInfo.Table.Name));

            //创建更新字段列表
            source.AppendLine(AttributeHelper.GetUpdateColumns(tableInfo.Columns));

            //创建参数列表
            source.AppendLine("SqlParameter[] parameters = {" + AttributeHelper.GetSqlParameterDeclare(tableInfo.Columns) + "};");

            //创建参数赋值
            source.AppendLine(AttributeHelper.GetSqlParameterValue(tableInfo.Columns));

            //创建执行命令
            source.AppendLine("if(_SqlTranExtensions != null){_SqlTranExtensions.Add(strSql.ToString(),parameters);return;}");

            //判断数据库
            source.AppendLine(@"if(DataHelper.IsAccess){ 
                                DbHelperOleDb.ExecuteSql(strSql.ToString(), DataHelper.GetOleDbParameterBySqlParameter(parameters)); 
                            }else{ 
                                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
                            }");


            source.AppendLine(tryEnd());
            source.AppendLine("}");


            return source.ToString();
        }

        private string CreateDelete()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine(" public void _Delete(" + AttributeHelper.PrimaryKeyTypeToCs(primaryKey) + " " + primaryKey.Name + ", SqlTranExtensions  _SqlTranExtensions){");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine(string.Format("strSql.Append(\"delete from {0} \");", tableInfo.Table.Name));

            //创建以主键为条件语句
            source.AppendLine(AttributeHelper.GetPrimaryKeyWhere(primaryKey));

            //创建参数列表
            source.AppendLine("SqlParameter[] parameters = {" + AttributeHelper.GetSqlParameterDeclare(new ColumnAttribute[] { primaryKey }) + "};");

            //创建参数赋值
            source.AppendLine(AttributeHelper.GetSqlParameterValue(new ColumnAttribute[] { primaryKey }).Replace("model.", ""));

            //创建执行命令
            source.AppendLine("if(_SqlTranExtensions != null){_SqlTranExtensions.Add(strSql.ToString(),parameters);return;}");

            //判断数据库
            source.AppendLine(@"if(DataHelper.IsAccess){
                                DbHelperOleDb.ExecuteSql(strSql.ToString(), DataHelper.GetOleDbParameterBySqlParameter(parameters));
                              }");
            source.AppendLine(@"else{
                                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                              }");

            source.AppendLine(tryEnd());
            source.AppendLine("}");

            return source.ToString();
        }

        private string CreateDeleteWhere()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine(" public void _DeleteWhere(string deleteWhere, SqlParameter[] cmdParms, SqlTranExtensions  _SqlTranExtensions){");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine(string.Format("strSql.Append(\"delete from {0} \");", tableInfo.Table.Name));

            //创建删除条件语句
            source.AppendLine("strSql.Append(\" where \"+ deleteWhere);");

            //创建执行命令
            source.AppendLine("if(_SqlTranExtensions != null){_SqlTranExtensions.Add(strSql.ToString(),cmdParms);return;}");

            //判断数据库
            source.AppendLine(@"if(DataHelper.IsAccess){
                                DbHelperOleDb.ExecuteSql(strSql.ToString(), DataHelper.GetOleDbParameterBySqlParameter(cmdParms));
                              }");
            source.AppendLine(@"else{
                                DbHelperSQL.ExecuteSql(strSql.ToString(), cmdParms);
                              }");

            source.AppendLine(tryEnd());
            source.AppendLine("}");

            return source.ToString();
        }

        private string CreateGetList()
        {
            StringBuilder source = new StringBuilder();

            source.AppendLine("public DataSet _GetList(string strWhere, SqlParameter[] cmdParms){");
            source.AppendLine(tryStart());
            source.AppendLine("return _GetList(int.MaxValue,strWhere,cmdParms,\"\");");
            source.AppendLine(tryEnd());
            source.AppendLine("}");


            return source.ToString();
        }

        private string CreateGetList1()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine("public DataSet _GetList(int Top, string strWhere, SqlParameter[] cmdParms, string filedOrder ){");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine("strSql.Append(\"select \");");

            //创建Top
            source.AppendLine("if (Top > 0) strSql.Append(\" top \" + Top.ToString());");

            //创建字段列表
            source.AppendLine(string.Format("strSql.Append(\" {0} \");", AttributeHelper.GetColunsString(tableInfo.Columns)));

            //创建From
            source.AppendLine(string.Format("strSql.Append(\" FROM {0} \");", tableInfo.Table.Name));

            //创建Where
            source.AppendLine("if (strWhere.Trim() != \"\") strSql.Append(\" where \" + strWhere);");

            //创建Order
            source.AppendLine("if(filedOrder.Trim()!=\"\")strSql.Append(\" order by \" + filedOrder);");

            //创建执行命令

            //判断数据库
            source.AppendLine(@"if(DataHelper.IsAccess){
                                return DbHelperOleDb.Query(strSql.ToString(), DataHelper.GetOleDbParameterBySqlParameter(cmdParms));
                              }");
            source.AppendLine(@"else{
                                return DbHelperSQL.Query(strSql.ToString(), cmdParms);
                              }");

            source.AppendLine(tryEnd());
            source.AppendLine("}");


            return source.ToString();
        }

        private string CreateGetModel()
        {

            StringBuilder sources = new StringBuilder();

            sources.AppendLine("public " + tableInfo.TypeFullName + " _GetModel(" + AttributeHelper.PrimaryKeyTypeToCs(primaryKey) + " " + primaryKey.Name + "){");

            sources.AppendLine(tryStart());

            sources.AppendLine("DataSet dst = new DataSet();");
            //创建参数赋值
            sources.AppendLine("SqlParameter[] parameters = {new SqlParameter(\"@" + primaryKey.Name + "\", SqlDbType." + primaryKey.Type.ToString() + ")};parameters[0].Value = " + primaryKey.Name + ";");

            sources.AppendLine(string.Format("dst=_GetList(1,\" {0}=@{0} \",parameters,\"\");", primaryKey.Name));
            sources.AppendLine("if (dst.Tables[0].Rows.Count < 1){");
            sources.AppendLine("return null;");
            sources.AppendLine("}");
            sources.AppendLine("return dst.Tables[0].ToList<" + tableInfo.TypeFullName + ">()[0];");

            sources.AppendLine(tryEnd());
            sources.AppendLine("}");

            return sources.ToString();
        }

        private string CreateGetModel1()
        {
            StringBuilder sources = new StringBuilder();

            sources.AppendLine("public List<" + tableInfo.TypeFullName + " > _GetModels(string where, SqlParameter[] cmdParms){");

            sources.AppendLine(tryStart());

            sources.AppendLine("DataSet dst = new DataSet();");
            sources.AppendLine("dst=_GetList(int.MaxValue,where,cmdParms,\"\");");
            sources.AppendLine("if (dst.Tables[0].Rows.Count < 1){");
            sources.AppendLine("return new List<" + tableInfo.TypeFullName + ">();");
            sources.AppendLine("}");
            sources.AppendLine("return dst.Tables[0].ToList<" + tableInfo.TypeFullName + ">();");

            sources.AppendLine(tryEnd());
            sources.AppendLine("}");

            return sources.ToString();
        }

        private string CreateGetModel2()
        {
            StringBuilder sources = new StringBuilder();

            sources.AppendLine("public List<" + tableInfo.TypeFullName + " > _GetModels(int Top, string strWhere,SqlParameter[] cmdParms, string filedOrder){");

            sources.AppendLine(tryStart());

            sources.AppendLine("DataSet dst = new DataSet();");
            sources.AppendLine("dst=_GetList(Top,strWhere,cmdParms,filedOrder);");
            sources.AppendLine("if (dst.Tables[0].Rows.Count < 1){");
            sources.AppendLine("return new List<" + tableInfo.TypeFullName + ">();");
            sources.AppendLine("}");
            sources.AppendLine("return dst.Tables[0].ToList<" + tableInfo.TypeFullName + ">();");

            sources.AppendLine(tryEnd());
            sources.AppendLine("}");

            return sources.ToString();
        }

        private string CreateGetListFieds()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            source.AppendLine("public DataSet _GetListFieds(int Top, string strWhere,SqlParameter[] cmdParms, string filedOrder, string returnFields){");

            source.AppendLine(tryStart());

            //创建SQL语句头
            source.AppendLine("strSql.Append(\"select \");");

            //创建Top
            source.AppendLine("if (Top > 0) strSql.Append(\" top \" + Top.ToString());");

            //创建字段列表
            source.AppendLine("strSql.Append(\" \"+" + "returnFields" + "+\" \");");

            //创建From
            source.AppendLine(string.Format("strSql.Append(\" FROM {0} \");", tableInfo.Table.Name));

            //创建Where
            source.AppendLine("if (strWhere.Trim() != \"\") strSql.Append(\" where \" + strWhere);");

            //创建Order
            source.AppendLine("if(filedOrder.Trim()!=\"\")strSql.Append(\" order by \" + filedOrder);");

            //创建执行命令

            //判断数据库
            source.AppendLine(@"if(DataHelper.IsAccess){
                                return DbHelperOleDb.Query(strSql.ToString(), DataHelper.GetOleDbParameterBySqlParameter(cmdParms));
                              }");
            source.AppendLine(@"else{
                                return DbHelperSQL.Query(strSql.ToString(),cmdParms);
                              }");

            source.AppendLine(tryEnd());
            source.AppendLine("}");


            return source.ToString();

        }

        private string CreateGetPageList()
        {
            StringBuilder source = new StringBuilder();

            //创建方法头
            //source.AppendLine("public System.Collections.ArrayList GetPageList(int pagesize, int pageindex, string strWhere, SqlParameter[] cmdParms, string[] orderCol, int[] strOrderType, string columns){");
            source.AppendLine("public System.Collections.ArrayList _GetPageList(int pagesize, int pageindex, string strWhere, SqlParameter[] cmdParms, string orderColType, string columns){");

            source.AppendLine(tryStart());

            //判断数据库
            source.AppendLine(@"if(DataHelper.IsAccess){
                                    return _GetPageList_access(pagesize, pageindex, strWhere, cmdParms, orderColType, columns);
                                }");

            source.AppendLine("System.Collections.ArrayList C_ArrayList = new System.Collections.ArrayList();");
            source.AppendLine("int recordCount = 0;");
            source.AppendLine("string TableName = \"" + tableInfo.Table.Name + "\";");
            //source.AppendLine("if (string.IsNullOrEmpty(orderCol)){orderCol = \" id \";}");
            //source.AppendLine("string orderType = \" DESC\";");
            //source.AppendLine("if (strOrderType == 0){orderType = \" ASC\";}");
            source.AppendLine("if (pageindex < 1){pageindex = 1;}");
            source.AppendLine("StringBuilder strCount = new StringBuilder();");
            source.AppendLine("if (string.IsNullOrEmpty(strWhere)){strWhere = \" 1=1 \";}");
            source.AppendLine("strCount.AppendFormat(\"select count(*) from {0} where {1}\", TableName, strWhere);");
            source.AppendLine("SqlDataReader dr = DbHelperSQL.ExecuteReader(strCount.ToString(),cmdParms);");
            source.AppendLine("if (dr.Read()){recordCount = int.Parse(dr[0].ToString());}else{recordCount = 0;}");
            source.AppendLine("dr.Close(); dr.Dispose();");
            source.AppendLine("int pageCount = 0;");
            source.AppendLine("if ((recordCount % pagesize) > 0){pageCount = recordCount / pagesize + 1;}else{pageCount = recordCount / pagesize;}");
            source.AppendLine("if (pageindex > pageCount){pageindex = pageCount;}");

            source.AppendLine("if (pageindex <= 1){strSql.AppendFormat(\"select top {0} {3} from {1} where {2}\", pagesize.ToString(), TableName, strWhere, columns);}");
            source.AppendLine("else");
            source.AppendLine("{");
            source.AppendLine("strSql.AppendFormat(\"select top {0} {2} from {1} \", pagesize, TableName, columns);");
            source.AppendLine("strSql.AppendFormat(\" where id not in (select top {0} id from {1} \", pagesize * (pageindex - 1), TableName);");
            source.AppendLine("if (strWhere.Trim() != \"\"){strSql.AppendFormat(\" where {0} order by {1} ) and {0}\", strWhere, orderColType);}else{strSql.AppendFormat(\" order by {0}) \", orderColType);}");
            source.AppendLine("}");
            source.AppendLine("strSql.AppendFormat(\" order by {0} \", orderColType);");
            source.AppendLine("C_ArrayList.Add( DbHelperSQL.Query(strSql.ToString(),cmdParms) );");
            source.AppendLine("C_ArrayList.Add(recordCount);");
            source.AppendLine("C_ArrayList.Add(pageCount);");
            source.AppendLine("C_ArrayList.Add(pageindex);");
            source.AppendLine("return C_ArrayList;");

            source.AppendLine(tryEnd());
            source.AppendLine("}");


            source.AppendLine(""); source.AppendLine(""); source.AppendLine("");
            //access
            source.AppendLine("public System.Collections.ArrayList _GetPageList_access(int pagesize, int pageindex, string strWhere, SqlParameter[] cmdParms, string orderColType, string columns){");

            source.AppendLine(tryStart());

            source.AppendLine("System.Collections.ArrayList C_ArrayList = new System.Collections.ArrayList();");
            source.AppendLine("int recordCount = 0;");
            source.AppendLine("string TableName = \"" + tableInfo.Table.Name + "\";");
            source.AppendLine("if (pageindex < 1){pageindex = 1;}");
            source.AppendLine("StringBuilder strCount = new StringBuilder();");
            source.AppendLine("if (string.IsNullOrEmpty(strWhere)){strWhere = \" 1=1 \";}");
            source.AppendLine("strCount.AppendFormat(\"select count(*) from {0} where {1}\", TableName, strWhere);");
            source.AppendLine("OleDbDataReader dr = DbHelperOleDb.ExecuteReader(strCount.ToString(),DataHelper.GetOleDbParameterBySqlParameter(cmdParms));");
            source.AppendLine("if (dr.Read()){recordCount = int.Parse(dr[0].ToString());}else{recordCount = 0;}");
            source.AppendLine("dr.Close(); dr.Dispose();");
            source.AppendLine("int pageCount = 0;");
            source.AppendLine("if ((recordCount % pagesize) > 0){pageCount = recordCount / pagesize + 1;}else{pageCount = recordCount / pagesize;}");
            source.AppendLine("if (pageindex > pageCount){pageindex = pageCount;}");

            source.AppendLine("if (pageindex <= 1){strSql.AppendFormat(\"select top {0} {3} from {1} where {2}\", pagesize.ToString(), TableName, strWhere, columns);}");
            source.AppendLine("else");
            source.AppendLine("{");
            source.AppendLine("strSql.AppendFormat(\"select top {0} {2} from {1} \", pagesize, TableName, columns);");
            source.AppendLine("strSql.AppendFormat(\" where id not in (select top {0} id from {1} \", pagesize * (pageindex - 1), TableName);");
            source.AppendLine("if (strWhere.Trim() != \"\"){strSql.AppendFormat(\" where {0} order by {1} ) and {0}\", strWhere, orderColType);}else{strSql.AppendFormat(\" order by {0}) \", orderColType);}");
            source.AppendLine("}");
            source.AppendLine("strSql.AppendFormat(\" order by {0} \", orderColType);");
            source.AppendLine("C_ArrayList.Add( DbHelperOleDb.Query(strSql.ToString(),DataHelper.GetOleDbParameterBySqlParameter(cmdParms)) );");
            source.AppendLine("C_ArrayList.Add(recordCount);");
            source.AppendLine("C_ArrayList.Add(pageCount);");
            source.AppendLine("C_ArrayList.Add(pageindex);");
            source.AppendLine("return C_ArrayList;");

            source.AppendLine(tryEnd());
            source.AppendLine("}");

            return source.ToString();

        }

        #endregion
    }

}