using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
/// <summary>
/// 网站API登录信息 - 实体类SiteLoginAPI(属性说明自动提取数据库字段的描述信息)
/// </summary>
[Serializable]
[Table("SiteLoginAPI", Info = "网站API登录信息")]
public class SiteLoginAPI : EntityBase<SiteLoginAPI>
{
public SiteLoginAPI(){}

#region BLLService-业务逻辑层
/// <summary>
/// 网站API登录信息-SiteLoginAPI-业务逻辑层
/// </summary>
public static class BLLService
{
     
}
#endregion


#region Option
public static string GetOptions(){
StringBuilder output = new StringBuilder();
try{
 List<SiteLoginAPI> m_SiteLoginAPIList = Orm.EntityCore<SiteLoginAPI>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
foreach (SiteLoginAPI _SiteLoginAPI in m_SiteLoginAPIList){
output.AppendFormat("<option value='{0}'>{1}</option>", _SiteLoginAPI.id, _SiteLoginAPI.id);}}catch { }
return output.ToString();
}
#endregion


#region GetConnectedModel
#endregion


#region Model

private  int? _id;
/// <summary>
/// 编号
/// </summary>
[Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
[Check("id", "编号", typeof( int) )]
public override int? id
{
set { Authentication(value, "id");  _id = value; }
get { SetPValue("id"); return _id; }
}

private string _apitype;
/// <summary>
/// API类型
/// </summary>
[Column("ApiType", System.Data.SqlDbType.NVarChar)]
[Check("ApiType", "API类型", typeof(string) )]
public string ApiType
{
set { Authentication(value, "ApiType");  _apitype = value; }
get { SetPValue("ApiType"); return _apitype == null ? "" : _apitype; }
}

private string _app_key;
/// <summary>
/// App_key
/// </summary>
[Column("App_key", System.Data.SqlDbType.NVarChar)]
[Check("App_key", "App_key", typeof(string) )]
public string App_key
{
set { Authentication(value, "App_key");  _app_key = value; }
get { SetPValue("App_key"); return _app_key == null ? "" : _app_key; }
}

private string _app_secret;
/// <summary>
/// App_secret
/// </summary>
[Column("App_secret", System.Data.SqlDbType.NVarChar)]
[Check("App_secret", "App_secret", typeof(string) )]
public string App_secret
{
set { Authentication(value, "App_secret");  _app_secret = value; }
get { SetPValue("App_secret"); return _app_secret == null ? "" : _app_secret; }
}

private string _metaproperty;
/// <summary>
/// MetaProperty
/// </summary>
[Column("MetaProperty", System.Data.SqlDbType.NVarChar)]
[Check("MetaProperty", "MetaProperty", typeof(string) )]
public string MetaProperty
{
set { Authentication(value, "MetaProperty");  _metaproperty = value; }
get { SetPValue("MetaProperty"); return _metaproperty == null ? "" : _metaproperty; }
}

private DateTime? _uptime=DateTime.Now;
/// <summary>
/// 更新时间
/// </summary>
[Column("UpTime", System.Data.SqlDbType.DateTime)]
[Check("UpTime", "更新时间", typeof(DateTime) )]
public DateTime? UpTime
{
set { Authentication(value, "UpTime");  _uptime = value; }
get { SetPValue("UpTime"); return _uptime; }
}
#endregion Model
}

}
