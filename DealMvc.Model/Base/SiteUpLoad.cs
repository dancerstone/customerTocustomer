using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
/// <summary>
/// 网站上传信息 - 实体类SiteUpLoad(属性说明自动提取数据库字段的描述信息)
/// </summary>
[Serializable]
[Table("SiteUpLoad", Info = "网站上传信息")]
public class SiteUpLoad : EntityBase<SiteUpLoad>
{
public SiteUpLoad(){}

#region BLLService-业务逻辑层
/// <summary>
/// 网站上传信息-SiteUpLoad-业务逻辑层
/// </summary>
public static class BLLService
{
     
}
#endregion


#region Option
public static string GetOptions(){
StringBuilder output = new StringBuilder();
try{
 List<SiteUpLoad> m_SiteUpLoadList = Orm.EntityCore<SiteUpLoad>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
foreach (SiteUpLoad _SiteUpLoad in m_SiteUpLoadList){
output.AppendFormat("<option value='{0}'>{1}</option>", _SiteUpLoad.id, _SiteUpLoad.id);}}catch { }
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

private string _uploadfolder;
/// <summary>
/// 上传文件夹
/// </summary>
[Column("UploadFolder", System.Data.SqlDbType.NVarChar)]
[Check("UploadFolder", "上传文件夹", typeof(string) )]
public string UploadFolder
{
set { Authentication(value, "UploadFolder");  _uploadfolder = value; }
get { SetPValue("UploadFolder"); return _uploadfolder == null ? "" : _uploadfolder; }
}

private string _uploadextension;
/// <summary>
/// 文件后缀名
/// </summary>
[Column("UploadExtension", System.Data.SqlDbType.NVarChar)]
[Check("UploadExtension", "文件后缀名", typeof(string) )]
public string UploadExtension
{
set { Authentication(value, "UploadExtension");  _uploadextension = value; }
get { SetPValue("UploadExtension"); return _uploadextension == null ? "" : _uploadextension; }
}

private double? _uploadsize=0;
/// <summary>
/// 文件大小
/// </summary>
[Column("UploadSize", System.Data.SqlDbType.Float)]
[Check("UploadSize", "文件大小", typeof(double) )]
public double? UploadSize
{
set { Authentication(value, "UploadSize");  _uploadsize = value; }
get { SetPValue("UploadSize"); return _uploadsize; }
}

private string _defaultimg;
/// <summary>
/// 默认图片
/// </summary>
[Column("DefaultImg", System.Data.SqlDbType.NVarChar)]
[Check("DefaultImg", "默认图片", typeof(string) )]
public string DefaultImg
{
set { Authentication(value, "DefaultImg");  _defaultimg = value; }
get { SetPValue("DefaultImg"); return _defaultimg == null ? "" : _defaultimg; }
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
