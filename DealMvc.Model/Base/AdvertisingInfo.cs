using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
/// <summary>
/// 广告信息 - 实体类AdvertisingInfo(属性说明自动提取数据库字段的描述信息)
/// </summary>
[Serializable]
[Table("AdvertisingInfo", Info = "广告信息")]
public class AdvertisingInfo : EntityBase<AdvertisingInfo>
{
public AdvertisingInfo(){}

#region BLLService-业务逻辑层
/// <summary>
/// 广告信息-AdvertisingInfo-业务逻辑层
/// </summary>
public static class BLLService
{
     
}
#endregion


#region Option
public static string GetOptions(){
StringBuilder output = new StringBuilder();
try{
 List<AdvertisingInfo> m_AdvertisingInfoList = Orm.EntityCore<AdvertisingInfo>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
foreach (AdvertisingInfo _AdvertisingInfo in m_AdvertisingInfoList){
output.AppendFormat("<option value='{0}'>{1}</option>", _AdvertisingInfo.id, _AdvertisingInfo.id);}}catch { }
return output.ToString();
}
#endregion


#region GetConnectedModel
#endregion


#region Model

private  int? _id;
/// <summary>
/// 主键--标识ID
/// </summary>
[Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
[Check("id", "主键--标识ID", typeof( int) )]
public override int? id
{
set { Authentication(value, "id");  _id = value; }
get { SetPValue("id"); return _id; }
}

private string _ai_adlocation;
/// <summary>
/// 广告位置
/// </summary>
[Column("AI_AdLocation", System.Data.SqlDbType.NVarChar)]
[Check("AI_AdLocation", "广告位置", typeof(string) )]
public string AI_AdLocation
{
set { Authentication(value, "AI_AdLocation");  _ai_adlocation = value; }
get { SetPValue("AI_AdLocation"); return _ai_adlocation == null ? "" : _ai_adlocation; }
}

private  int? _ai_adpicwidth=0;
/// <summary>
/// 像素宽
/// </summary>
[Column("AI_AdPicWidth", System.Data.SqlDbType.Int)]
[Check("AI_AdPicWidth", "像素宽", typeof( int) )]
public  int? AI_AdPicWidth
{
set { Authentication(value, "AI_AdPicWidth");  _ai_adpicwidth = value; }
get { SetPValue("AI_AdPicWidth"); return _ai_adpicwidth; }
}

private  int? _ai_adpicheight=0;
/// <summary>
/// 像素高
/// </summary>
[Column("AI_AdPicHeight", System.Data.SqlDbType.Int)]
[Check("AI_AdPicHeight", "像素高", typeof( int) )]
public  int? AI_AdPicHeight
{
set { Authentication(value, "AI_AdPicHeight");  _ai_adpicheight = value; }
get { SetPValue("AI_AdPicHeight"); return _ai_adpicheight; }
}

private string _ai_adtitle;
/// <summary>
/// 广告语
/// </summary>
[Column("AI_AdTitle", System.Data.SqlDbType.NVarChar)]
[Check("AI_AdTitle", "广告语", typeof(string) )]
public string AI_AdTitle
{
set { Authentication(value, "AI_AdTitle");  _ai_adtitle = value; }
get { SetPValue("AI_AdTitle"); return _ai_adtitle == null ? "" : _ai_adtitle; }
}

private string _ai_adpic;
/// <summary>
/// 广告图片
/// </summary>
[Column("AI_AdPic", System.Data.SqlDbType.NVarChar)]
[Check("AI_AdPic", "广告图片", typeof(string) )]
public string AI_AdPic
{
set { Authentication(value, "AI_AdPic");  _ai_adpic = value; }
get { SetPValue("AI_AdPic"); return _ai_adpic == null ? "" : _ai_adpic; }
}

private string _ai_linkurl;
/// <summary>
/// 链接地址
/// </summary>
[Column("AI_LinkUrl", System.Data.SqlDbType.NVarChar)]
[Check("AI_LinkUrl", "链接地址", typeof(string) )]
public string AI_LinkUrl
{
set { Authentication(value, "AI_LinkUrl");  _ai_linkurl = value; }
get { SetPValue("AI_LinkUrl"); return _ai_linkurl == null ? "" : _ai_linkurl; }
}

private string _ai_remarks;
/// <summary>
/// 广告备注
/// </summary>
[Column("AI_Remarks", System.Data.SqlDbType.NVarChar)]
[Check("AI_Remarks", "广告备注", typeof(string) )]
public string AI_Remarks
{
set { Authentication(value, "AI_Remarks");  _ai_remarks = value; }
get { SetPValue("AI_Remarks"); return _ai_remarks == null ? "" : _ai_remarks; }
}

private bool _ai_istarget=false;
/// <summary>
/// 是否新窗口打开
/// </summary>
[Column("AI_IsTarget", System.Data.SqlDbType.Bit)]
[Check("AI_IsTarget", "是否新窗口打开", typeof(bool) )]
public bool AI_IsTarget
{
set { Authentication(value, "AI_IsTarget");  _ai_istarget = value; }
get { SetPValue("AI_IsTarget"); return _ai_istarget; }
}

private DateTime? _ai_time=DateTime.Now;
/// <summary>
/// 操作时间
/// </summary>
[Column("AI_Time", System.Data.SqlDbType.DateTime)]
[Check("AI_Time", "操作时间", typeof(DateTime) )]
public DateTime? AI_Time
{
set { Authentication(value, "AI_Time");  _ai_time = value; }
get { SetPValue("AI_Time"); return _ai_time; }
}

private string _a;
/// <summary>
/// 扩展A字段
/// </summary>
[Column("A", System.Data.SqlDbType.NVarChar)]
[Check("A", "扩展A字段", typeof(string) )]
public string A
{
set { Authentication(value, "A");  _a = value; }
get { SetPValue("A"); return _a == null ? "" : _a; }
}

private string _b;
/// <summary>
/// 扩展B字段
/// </summary>
[Column("B", System.Data.SqlDbType.NVarChar)]
[Check("B", "扩展B字段", typeof(string) )]
public string B
{
set { Authentication(value, "B");  _b = value; }
get { SetPValue("B"); return _b == null ? "" : _b; }
}

private string _c;
/// <summary>
/// 扩展C字段
/// </summary>
[Column("C", System.Data.SqlDbType.NVarChar)]
[Check("C", "扩展C字段", typeof(string) )]
public string C
{
set { Authentication(value, "C");  _c = value; }
get { SetPValue("C"); return _c == null ? "" : _c; }
}

private string _d;
/// <summary>
/// 扩展D字段
/// </summary>
[Column("D", System.Data.SqlDbType.NVarChar)]
[Check("D", "扩展D字段", typeof(string) )]
public string D
{
set { Authentication(value, "D");  _d = value; }
get { SetPValue("D"); return _d == null ? "" : _d; }
}

private string _e;
/// <summary>
/// 扩展E字段
/// </summary>
[Column("E", System.Data.SqlDbType.NVarChar)]
[Check("E", "扩展E字段", typeof(string) )]
public string E
{
set { Authentication(value, "E");  _e = value; }
get { SetPValue("E"); return _e == null ? "" : _e; }
}
#endregion Model
}

}
