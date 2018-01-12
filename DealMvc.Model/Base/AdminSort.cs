using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 实体类AdminSort (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("AdminSort", Info = "AdminSort")]
    public class AdminSort : EntityBase<AdminSort>
    {
        public static AdminSort GetAdminSort(int? id)
        {
            return DealMvc.Orm.EntityCore<AdminSort>.GetModel(id ?? 0);
        }

        public AdminSort() { }


        #region Model

        private int? _id;
        /// <summary>
        /// id
        /// </summary>
        [Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
        [Check("id", "id", typeof(int), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public override int? id
        {
            set { Authentication(value, "id"); _id = value; }
            get { SetPValue("id"); return _id; }
        }


        private string _adminsortname;
        /// <summary>
        /// 账户类型名称
        /// </summary>
        [Column("AdminSortName", System.Data.SqlDbType.NVarChar)]
        [Check("AdminSortName", "账户类型名称", typeof(string), NotEmpty = true, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminSortName
        {
            set { Authentication(value, "AdminSortName"); _adminsortname = value; }
            get { SetPValue("AdminSortName"); return _adminsortname == null ? "" : _adminsortname; }
        }


        private string _adminsortpowervalues;
        /// <summary>
        /// 账户类型权限
        /// </summary>
        [Column("AdminSortPowerValues", System.Data.SqlDbType.NText)]
        [Check("AdminSortPowerValues", "账户类型权限", typeof(string), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminSortPowerValues
        {
            set
            {
                SiteInfo m_SiteInfo = SiteInfo.GetModel(t => t.id != 0);
                if (m_SiteInfo.WebCompetence ==true || string.IsNullOrEmpty(_adminsortpowervalues))
                { Authentication(value, "AdminSortPowerValues"); _adminsortpowervalues = value; }
            }
            get
            {
                SetPValue("AdminSortPowerValues");
                SiteInfo m_SiteInfo = SiteInfo.GetModel(t => t.id != 0);
                if (m_SiteInfo.WebCompetence == true)
                    return _adminsortpowervalues == null ? "" : _adminsortpowervalues;
                else
                    return "all";
            }
        }


        private int? _ordernum;
        /// <summary>
        /// 排序
        /// </summary>
        [Column("OrderNum", System.Data.SqlDbType.Int)]
        [Check("OrderNum", "排序", typeof(int), NotEmpty = true, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = true)]
        public int? OrderNum
        {
            set { Authentication(value, "OrderNum"); _ordernum = value; }
            get { SetPValue("OrderNum"); return _ordernum; }
        }


        private DateTime? _uptime;
        /// <summary>
        /// 时间
        /// </summary>
        [Column("UpTime", System.Data.SqlDbType.DateTime)]
        [Check("UpTime", "时间", typeof(DateTime), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public DateTime? UpTime
        {
            set { Authentication(value, "UpTime"); _uptime = value; }
            get { SetPValue("UpTime"); return _uptime; }
        }

        #endregion Model 
    }

}



/*
public class AdminSortController : ControllerBase
{
 public ActionResult AdminSortList(int? Page)
 {
     DealMvc.Orm.PagerEx.Pager<Model.AdminSort> _Pager = new Orm.PagerEx.Pager<Model.AdminSort>(Page ?? 0, "");

     _Pager.GetPageList();
     ViewData["Pager"] = _Pager;

     return View();
 }
 public ActionResult AEAdminSort(int? id,string AdminSortName,string AdminSortPowerValues,int? OrderNum,DateTime? UpTime)
 {
 Model.AdminSort m_AdminSort = null;

 bool isEdit = true;
 if (id == null) { isEdit = false; }
 ViewData["isEdit"] = isEdit;

 if (IsGet)
 {
    if (isEdit)
    {
        m_AdminSort = DealMvc.Orm.EntityCore<Model.AdminSort>.GetModel(id ?? 0);
        if (m_AdminSort == null) return RedirectToAction("AdminSortList");

        NameValueCollectionEx _NameValueCollectionEx = new NameValueCollectionEx();
        _NameValueCollectionEx.Add("AdminSortName", m_AdminSort.AdminSortName);
        _NameValueCollectionEx.Add("AdminSortPowerValues", m_AdminSort.AdminSortPowerValues);
        _NameValueCollectionEx.Add("OrderNum", m_AdminSort.OrderNum);
        _NameValueCollectionEx.Add("UpTime", m_AdminSort.UpTime);

        SetSaveFormCollection = _NameValueCollectionEx;
    }
 }
 if (IsPost)
 {
    try{
 if (isEdit)
    m_AdminSort = DealMvc.Orm.EntityCore<Model.AdminSort>.GetModel(id ?? 0);
 else
    m_AdminSort = new Model.AdminSort();
 
m_AdminSort.AdminSortName = AdminSortName;
m_AdminSort.AdminSortPowerValues = AdminSortPowerValues;
m_AdminSort.OrderNum = OrderNum;
m_AdminSort.UpTime = UpTime;

 
 if (isEdit){
    DealMvc.Orm.EntityCore<Model.AdminSort>.Update(m_AdminSort);
    DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "编辑成功");
    IsSaveForm = true;
 }
 else{
    DealMvc.Orm.EntityCore<Model.AdminSort>.Add(m_AdminSort);
    DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "添加成功");
 }
 
 
 
        }catch (Exception ce){IsSaveForm = true;
DealMvc.Common.ExceptionEx.MyExceptionLog.WriteLog(this, ce);}
 }
 return View();
 
 }
 public ActionResult DeleteAdminSort(int[] ids, int? Page)
 {
 if (ids == null || ids.Length == 0)
 DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "没有选择删除的内容", true);
 else{
 try{
 DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions = new SqlTranEx.SqlTranExtensions();
 for (int i = 0; i < ids.Length; i++){
 DealMvc.Orm.EntityCore<Model.AdminSort>.Delete(ids[i], _SqlTranExtensions);
 }
 _SqlTranExtensions.ExecuteSqlTran();
 DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "删除成功", true);
 }catch (Exception ce){DealMvc.Common.ExceptionEx.MyExceptionLog.WriteLog(this, ce);
DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "删除失败", true);}
 }
 return RedirectToAction("AdminSortList", new { Page = Page });
 
 }
}





<form method="post" class="form">
<table class="table1">
<tr>
                                <th align='right' style='width: 120px; overflow: hidden;'>
                                </th>
                                <th>
                                </th>
                              </tr>
<tr><td align="right"></td><td><input type="text" name="id" class="req" /></td></tr>
<tr><td align="right">账户类型名称</td><td><input type="text" name="AdminSortName" class="req" /></td></tr>
<tr><td align="right">账户类型权限</td><td><input type="text" name="AdminSortPowerValues" class="req" /></td></tr>
<tr><td align="right">排序</td><td><input type="text" name="OrderNum" class="req" /></td></tr>
<tr><td align="right">时间</td><td><input type="text" name="UpTime" class="req" /></td></tr>
<tr><td></td><td><%if ((bool)ViewData["isEdit"]){ %><input type="submit" value="编辑" /><%}else{ %><input type="submit" value="添加" /><%} %></td></tr>
</table></form>




<script>
 $(function (){
$("#SelectAllIds").toggle(function (){$(":checkbox[name='ids']").attr("checked", true);$(this).attr("checked", true);}, function (){$(":checkbox[name='ids']").attr("checked", false);$(this).attr("checked", false);});
});
function DeleteSelectAllIds(obj){if (IsConfirm()) $(".form1").submit();}
</script>
<% Pager<AdminSort> _Pager = (Pager<AdminSort>)ViewData["Pager"]; %>
<form class="form1" method="get" action='<%=Url.Action("DeleteAdminSort")%>'>
<table class="table1" style="">
<tr><th style="width: 30px;"></th><th style="width: 30px;">编号</th><th>主题</th><th style="width: 100px;">操作</th></tr>

<%int i = 0;foreach (AdminSort _AdminSort in _Pager.DataList){i++;%><tr><td><input type="checkbox" name="ids" value='<%=_AdminSort.id %>' /></td><td><%=(_Pager.PageIndex-1)*_Pager.PageSize + i %></td><td><%=_AdminSort.???.JSubString(50)%></td><td><a onclick='top.AddLabel("编辑?????","<%=Url.Action("AEAdminSort", new { id = _AdminSort.id })%>")'>编辑</a> <a onclick="return IsConfirm()" href='<%=Url.Action("DeleteAdminSort", new { Page = _Pager.PageIndex, ids = _AdminSort.id })%>'>删除</a></td></tr><%} %>

<%if (_Pager.DataList.Count == 0){ %><tr><td colspan="25"> 暂时没有内容... ...</td></tr><%} %><tr><td colspan="25"><input type="hidden" name="Page" value='<%=_Pager.PageIndex %>' /><a id="SelectAllIds">全选</a>&nbsp;&nbsp;&nbsp;<a onclick="DeleteSelectAllIds(this)">删除选中</a></td></tr>
</table></form>

*/
