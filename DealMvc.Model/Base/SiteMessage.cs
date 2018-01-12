using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
/// <summary>
/// 网站短信配置 - 实体类SiteMessage(属性说明自动提取数据库字段的描述信息)
/// </summary>
[Serializable]
[Table("SiteMessage", Info = "网站短信配置")]
public class SiteMessage : EntityBase<SiteMessage>
{
public SiteMessage(){}

#region BLLService-业务逻辑层
/// <summary>
/// 网站短信配置-SiteMessage-业务逻辑层
/// </summary>
public static class BLLService
{
     
}
#endregion


#region Option
public static string GetOptions(){
StringBuilder output = new StringBuilder();
try{
 List<SiteMessage> m_SiteMessageList = Orm.EntityCore<SiteMessage>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
foreach (SiteMessage _SiteMessage in m_SiteMessageList){
output.AppendFormat("<option value='{0}'>{1}</option>", _SiteMessage.id, _SiteMessage.id);}}catch { }
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

private string _username;
/// <summary>
/// 账号
/// </summary>
[Column("UserName", System.Data.SqlDbType.NVarChar)]
[Check("UserName", "账号", typeof(string) )]
public string UserName
{
set { Authentication(value, "UserName");  _username = value; }
get { SetPValue("UserName"); return _username == null ? "" : _username; }
}

private string _userpwd;
/// <summary>
/// 密码
/// </summary>
[Column("UserPwd", System.Data.SqlDbType.NVarChar)]
[Check("UserPwd", "密码", typeof(string) )]
public string UserPwd
{
set { Authentication(value, "UserPwd");  _userpwd = value; }
get { SetPValue("UserPwd"); return _userpwd == null ? "" : _userpwd; }
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
