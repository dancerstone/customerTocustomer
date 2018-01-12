using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
/// <summary>
/// 网站API登录信息 - 业务层 SiteLoginAPI
/// </summary>
public class BLL_SiteLoginAPI
{
   /// <summary>
   /// SiteLoginAPI构造函数
   /// </summary>
   public BLL_SiteLoginAPI()
   {
   
   }

#region 原生态逻辑

   /// <summary>
   /// 分页列表
   /// </summary>
   /// <param name="Page">分页索引</param>
   /// <returns></returns>
   public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteLoginAPI> PagerList(int? Page)
   {
       DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteLoginAPI> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.SiteLoginAPI>(Page ?? 0, "");
       _Pager.OrderColumn = new string[] { "id" };
       _Pager.OrderType = new DealMvc.Orm.PagerEx.SQLOrderType[] {  Orm.PagerEx.SQLOrderType.DESC };
   
       _Pager.GetPageList();
       return _Pager;
   }

   /// <summary>
   /// 返回字段键值对
   /// </summary>
   /// <param name="id"></param>
   /// <returns></returns>
   public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.SiteLoginAPI m_slapi)
   {
       m_slapi = DealMvc.Model.SiteLoginAPI.GetModel(m_slapi.id ?? 0);
       if (m_slapi.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_slapi Is Null");
   
       NameValueCollectionEx _nvce = new NameValueCollectionEx();
        _nvce.Add("ApiType", m_slapi.ApiType);        //ApiType[Type=string] - API类型
        _nvce.Add("App_key", m_slapi.App_key);        //App_key[Type=string] - App_key
        _nvce.Add("App_secret", m_slapi.App_secret);        //App_secret[Type=string] - App_secret
        _nvce.Add("MetaProperty", m_slapi.MetaProperty);        //MetaProperty[Type=string] - MetaProperty
        _nvce.Add("UpTime", m_slapi.UpTime);        //UpTime[Type=DateTime?] - 更新时间

       return _nvce;
   }

   /// <summary>
   /// 添加/编辑
   /// </summary>
   /// <param name="isEdit"></param>
   /// <param name="p_SiteLoginAPI"></param>
   public void AESiteLoginAPI(DealMvc.ControllerBase _CB, bool isEdit, ref Model.SiteLoginAPI p_slapi)
   {
   Model.SiteLoginAPI m_slapi = null;
   
   if (isEdit)
   m_slapi = DealMvc.Model.SiteLoginAPI.GetModel(p_slapi.id ?? 0);
   else
   m_slapi = new Model.SiteLoginAPI();
   
   if (!isEdit)
   {
       if (Orm.EntityCore<Model.SiteLoginAPI>.Exists("=id", new object[] { p_slapi.id })) 
       {
           throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
       }
   }else{
       
   }
   
m_slapi.ApiType = p_slapi.ApiType;        //ApiType[Type=string] - API类型
m_slapi.App_key = p_slapi.App_key;        //App_key[Type=string] - App_key
m_slapi.App_secret = p_slapi.App_secret;        //App_secret[Type=string] - App_secret
m_slapi.MetaProperty = p_slapi.MetaProperty;        //MetaProperty[Type=string] - MetaProperty
m_slapi.UpTime = p_slapi.UpTime ?? DateTime.Now;        //UpTime[Type=DateTime?] - 更新时间

  
   p_slapi = m_slapi;
   if (isEdit)
   {
       m_slapi.Update();
       ExceptionEx.MyExceptionLog.AlertMessage(_CB, "编辑成功");
       _CB.IsSaveForm = true;
   }
   else
   {
       m_slapi.Add();
       ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
   }
   }

   /// <summary>
   /// 删除对象
   /// </summary>
   /// <param name="ids">1,2,3,4</param>
   /// <returns></returns>
   public bool DeleteSiteLoginAPI(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
   {
   bool result = false;
   string[] _ids = ids.Split(',');
   
   try
   {
   bool isDo = false;
   if (_SqlTranExtensions == null)
   {
   _SqlTranExtensions = new SqlTranEx.SqlTranExtensions();
   isDo = true;
   }
   for (int i = 0; i < _ids.Length; i++)
   {
   DealMvc.Orm.EntityCore<Model.SiteLoginAPI>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
   }
   if (isDo)
   _SqlTranExtensions.ExecuteSqlTran();
   result = true;
   }
   catch (Exception ce)
   {
   ExceptionEx.MyExceptionLog.AddLogError(ce.Message);
   result = false;
   }
   return result;
   }

#endregion

#region 业务逻辑

#endregion

#region 后台逻辑

#endregion

#region 前台逻辑

#endregion

#region 公用逻辑

#endregion

}

}
