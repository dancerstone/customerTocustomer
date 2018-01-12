using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 实体类Admin (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("Admin", Info = "Admin")]
    public class Admin : EntityBase<Admin>
    {
        public static Admin getAdminByAdminID(string AdminID)
        {
            try
            {
                Common.Net.CreateParameter _C = new Common.Net.CreateParameter();
                _C.CanShu.Add("@AdminID", System.Data.SqlDbType.NVarChar, AdminID);
                List<Admin> m_AdminList = Orm.EntityCore<Admin>.GetModelList("AdminID=@AdminID", _C.Re_SqlParameter()).List;
                if (m_AdminList.Count == 0) return null;
                else
                    return m_AdminList[0];
            }
            catch
            {
               //Exception.MyExceptionLog.AddLogError("获取后体帐号model失败, Admin - getAdminByAdminID");
                return null;
            }
        }

        public Admin() { }


        #region Option

        public static string GetOptions()
        {
            StringBuilder output = new StringBuilder();
            try
            {
                List<Admin> m_AdminList = Orm.EntityCore<Admin>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (Admin _Admin in m_AdminList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _Admin.id, _Admin.id);
                }
            }
            catch { }
            return output.ToString();
        }

        #endregion


        #region GetConnectedModel

        private AdminSort _AdminSort;
        /// <summary>
        /// Get AdminSort Model By AdminSortID
        /// </summary>
        public AdminSort AdminSort
        {
            get
            {
                return GM<AdminSort>(AdminSortID ?? 0, ref _AdminSort);
            }
        }

        #endregion


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


        private string _adminid;
        /// <summary>
        /// 帐号ID
        /// </summary>
        [Column("AdminID", System.Data.SqlDbType.NVarChar)]
        [Check("AdminID", "帐号ID", typeof(string), NotEmpty = true, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminID
        {
            set { Authentication(value, "AdminID"); _adminid = value; }
            get { SetPValue("AdminID"); return _adminid == null ? "" : _adminid; }
        }


        private string _adminpwd;
        /// <summary>
        /// 帐号密码
        /// </summary>
        [Column("AdminPwd", System.Data.SqlDbType.NVarChar)]
        [Check("AdminPwd", "帐号密码", typeof(string), NotEmpty = true, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminPwd
        {
            set { Authentication(value, "AdminPwd"); _adminpwd = value; }
            get { SetPValue("AdminPwd"); return _adminpwd == null ? "" : _adminpwd; }
        }


        private string _adminrealname;
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("AdminRealName", System.Data.SqlDbType.NVarChar)]
        [Check("AdminRealName", "姓名", typeof(string), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminRealName
        {
            set { Authentication(value, "AdminRealName"); _adminrealname = value; }
            get { SetPValue("AdminRealName"); return _adminrealname == null ? "" : _adminrealname; }
        }


        private string _adminsex;
        /// <summary>
        /// 性别
        /// </summary>
        [Column("AdminSex", System.Data.SqlDbType.NVarChar)]
        [Check("AdminSex", "性别", typeof(string), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminSex
        {
            set { Authentication(value, "AdminSex"); _adminsex = value; }
            get { SetPValue("AdminSex"); return _adminsex == null ? "" : _adminsex; }
        }


        private int? _adminsortid;
        /// <summary>
        /// 帐号类型
        /// </summary>
        [Column("AdminSortID", System.Data.SqlDbType.Int)]
        [Check("AdminSortID", "帐号类型", typeof(int), NotEmpty = true, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = true)]
        public int? AdminSortID
        {
            set { Authentication(value, "AdminSortID"); _adminsortid = value; }
            get { SetPValue("AdminSortID"); return _adminsortid; }
        }

        public static string GetAdminSortForSelect()
        {
            StringBuilder output = new StringBuilder();

            DealMvc.Orm.PagerEx.Pager<Model.AdminSort> _Pager = new Orm.PagerEx.Pager<AdminSort>(1, "", null, int.MaxValue, "OrderNum", Orm.PagerEx.SQLOrderType.DESC);
            _Pager.GetPageList();
            foreach (AdminSort _AdminSort in _Pager.DataList)
            {
                output.AppendLine("<option value='" + _AdminSort.id + "'>" + _AdminSort.AdminSortName + "</option>");
            }
            return output.ToString();
        }

        public static string GetAdminSortName(int? id)
        {
            try { return DealMvc.Orm.EntityCore<Model.AdminSort>.GetModel(id ?? 0).AdminSortName; }
            catch { return ""; }
        }

        public string GetAdminSortName()
        {
            try { return DealMvc.Orm.EntityCore<Model.AdminSort>.GetModel(AdminSortID ?? 0).AdminSortName; }
            catch { return ""; }
        }

        private string _adminpowervalues;
        /// <summary>
        /// 权限
        /// </summary>
        [Column("AdminPowerValues", System.Data.SqlDbType.NText)]
        [Check("AdminPowerValues", "权限", typeof(string), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminPowerValues
        {
            set
            {
                SiteInfo m_SiteInfo = SiteInfo.GetModel(t => t.id != 0);
                if (m_SiteInfo.WebCompetence == true || string.IsNullOrEmpty(_adminpowervalues))
                { Authentication(value, "AdminPowerValues"); _adminpowervalues = value; }
            }
            get
            {
                SetPValue("AdminPowerValues");
                SiteInfo m_SiteInfo = SiteInfo.GetModel(t => t.id != 0);
                if (m_SiteInfo.WebCompetence == true)
                    return _adminpowervalues == null ? "" : _adminpowervalues;
                else
                    return "all";
            }
        }


        private string _adminremark;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("AdminRemark", System.Data.SqlDbType.NText)]
        [Check("AdminRemark", "备注", typeof(string), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public string AdminRemark
        {
            set { Authentication(value, "AdminRemark"); _adminremark = value; }
            get { SetPValue("AdminRemark"); return _adminremark == null ? "" : _adminremark; }
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
        /// 跟新时间
        /// </summary>
        [Column("UpTime", System.Data.SqlDbType.DateTime)]
        [Check("UpTime", "跟新时间", typeof(DateTime), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public DateTime? UpTime
        {
            set { Authentication(value, "UpTime"); _uptime = value; }
            get { SetPValue("UpTime"); return _uptime; }
        }


        private DateTime? _lastlogintime;
        /// <summary>
        /// LastLoginTime
        /// </summary>
        [Column("LastLoginTime", System.Data.SqlDbType.DateTime)]
        [Check("LastLoginTime", "LastLoginTime", typeof(DateTime), NotEmpty = false, IsEmail = false, IsPhone = false, IsMoney = false, NotSupportChinese = false, IsNumber = false)]
        public DateTime? LastLoginTime
        {
            set { Authentication(value, "LastLoginTime"); _lastlogintime = value; }
            get { SetPValue("LastLoginTime"); return _lastlogintime; }
        }

        #endregion Model 
    }

}



/*
public class AdminController : ControllerBase
{
 public ActionResult AdminList(int? Page)
 {
     DealMvc.Orm.PagerEx.Pager<Model.Admin> _Pager = new Orm.PagerEx.Pager<Model.Admin>(Page ?? 0, "");

     _Pager.GetPageList();
     ViewData["Pager"] = _Pager;

     return View();
 }
 public ActionResult AEAdmin(int? id,string AdminID,string AdminPwd,string AdminRealName,string AdminSex,int? AdminSortID,string AdminPowerValues,string AdminRemark,int? OrderNum,DateTime? UpTime,DateTime? LastLoginTime)
 {
 Model.Admin m_Admin = null;

 bool isEdit = true;
 if (id == null) { isEdit = false; }
 ViewData["isEdit"] = isEdit;

 if (IsGet)
 {
    if (isEdit)
    {
        m_Admin = DealMvc.Orm.EntityCore<Model.Admin>.GetModel(id ?? 0);
        if (m_Admin == null) return RedirectToAction("AdminList");

        NameValueCollectionEx _NameValueCollectionEx = new NameValueCollectionEx();
        _NameValueCollectionEx.Add("AdminID", m_Admin.AdminID);
        _NameValueCollectionEx.Add("AdminPwd", m_Admin.AdminPwd);
        _NameValueCollectionEx.Add("AdminRealName", m_Admin.AdminRealName);
        _NameValueCollectionEx.Add("AdminSex", m_Admin.AdminSex);
        _NameValueCollectionEx.Add("AdminSortID", m_Admin.AdminSortID);
        _NameValueCollectionEx.Add("AdminPowerValues", m_Admin.AdminPowerValues);
        _NameValueCollectionEx.Add("AdminRemark", m_Admin.AdminRemark);
        _NameValueCollectionEx.Add("OrderNum", m_Admin.OrderNum);
        _NameValueCollectionEx.Add("UpTime", m_Admin.UpTime);
        _NameValueCollectionEx.Add("LastLoginTime", m_Admin.LastLoginTime);

        SetSaveFormCollection = _NameValueCollectionEx;
    }
 }
 if (IsPost)
 {
    try{
 if (isEdit)
    m_Admin = DealMvc.Orm.EntityCore<Model.Admin>.GetModel(id ?? 0);
 else
    m_Admin = new Model.Admin();
 
m_Admin.AdminID = AdminID;
m_Admin.AdminPwd = AdminPwd;
m_Admin.AdminRealName = AdminRealName;
m_Admin.AdminSex = AdminSex;
m_Admin.AdminSortID = AdminSortID;
m_Admin.AdminPowerValues = AdminPowerValues;
m_Admin.AdminRemark = AdminRemark;
m_Admin.OrderNum = OrderNum;
m_Admin.UpTime = UpTime;
m_Admin.LastLoginTime = LastLoginTime;

 
 if (isEdit){
    DealMvc.Orm.EntityCore<Model.Admin>.Update(m_Admin);
    DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "编辑成功");
    IsSaveForm = true;
 }
 else{
    DealMvc.Orm.EntityCore<Model.Admin>.Add(m_Admin);
    DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "添加成功");
 }
 
 
 
        }catch (Exception ce){IsSaveForm = true;
DealMvc.Common.ExceptionEx.MyExceptionLog.WriteLog(this, ce);}
 }
 return View();
 
 }
 public ActionResult DeleteAdmin(int[] ids, int? Page)
 {
 if (ids == null || ids.Length == 0)
 DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "没有选择删除的内容", true);
 else{
 try{
 DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions = new SqlTranEx.SqlTranExtensions();
 for (int i = 0; i < ids.Length; i++){
 DealMvc.Orm.EntityCore<Model.Admin>.Delete(ids[i], _SqlTranExtensions);
 }
 _SqlTranExtensions.ExecuteSqlTran();
 DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "删除成功", true);
 }catch (Exception ce){DealMvc.Common.ExceptionEx.MyExceptionLog.WriteLog(this, ce);
DealMvc.Common.ExceptionEx.MyExceptionLog.AlertMessage(this, "删除失败", true);}
 }
 return RedirectToAction("AdminList", new { Page = Page });
 
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
<tr><td align="right">帐号ID</td><td><input type="text" name="AdminID" class="req" /></td></tr>
<tr><td align="right">帐号密码</td><td><input type="text" name="AdminPwd" class="req" /></td></tr>
<tr><td align="right">姓名</td><td><input type="text" name="AdminRealName" class="req" /></td></tr>
<tr><td align="right">性别</td><td><input type="text" name="AdminSex" class="req" /></td></tr>
<tr><td align="right">帐号类型</td><td><input type="text" name="AdminSortID" class="req" /></td></tr>
<tr><td align="right">权限</td><td><input type="text" name="AdminPowerValues" class="req" /></td></tr>
<tr><td align="right">备注</td><td><input type="text" name="AdminRemark" class="req" /></td></tr>
<tr><td align="right">排序</td><td><input type="text" name="OrderNum" class="req" /></td></tr>
<tr><td align="right">跟新时间</td><td><input type="text" name="UpTime" class="req" /></td></tr>
<tr><td align="right"></td><td><input type="text" name="LastLoginTime" class="req" /></td></tr>
<tr><td></td><td><%if ((bool)ViewData["isEdit"]){ %><input type="submit" value="编辑" /><%}else{ %><input type="submit" value="添加" /><%} %></td></tr>
</table></form>




<script>
 $(function (){
$("#SelectAllIds").toggle(function (){$(":checkbox[name='ids']").attr("checked", true);$(this).attr("checked", true);}, function (){$(":checkbox[name='ids']").attr("checked", false);$(this).attr("checked", false);});
});
function DeleteSelectAllIds(obj){if (IsConfirm()) $(".form1").submit();}
</script>
<% Pager<Admin> _Pager = (Pager<Admin>)ViewData["Pager"]; %>
<form class="form1" method="get" action='<%=Url.Action("DeleteAdmin")%>'>
<table class="table1" style="">
<tr><th style="width: 30px;"></th><th style="width: 30px;">编号</th><th>主题</th><th style="width: 100px;">操作</th></tr>

<%int i = 0;foreach (Admin _Admin in _Pager.DataList){i++;%><tr><td><input type="checkbox" name="ids" value='<%=_Admin.id %>' /></td><td><%=(_Pager.PageIndex-1)*_Pager.PageSize + i %></td><td><%=_Admin.???.JSubString(50)%></td><td><a onclick='top.AddLabel("编辑?????","<%=Url.Action("AEAdmin", new { id = _Admin.id })%>")'>编辑</a> <a onclick="return IsConfirm()" href='<%=Url.Action("DeleteAdmin", new { Page = _Pager.PageIndex, ids = _Admin.id })%>'>删除</a></td></tr><%} %>

<%if (_Pager.DataList.Count == 0){ %><tr><td colspan="25"> 暂时没有内容... ...</td></tr><%} %><tr><td colspan="25"><input type="hidden" name="Page" value='<%=_Pager.PageIndex %>' /><a id="SelectAllIds">全选</a>&nbsp;&nbsp;&nbsp;<a onclick="DeleteSelectAllIds(this)">删除选中</a></td></tr>
</table></form>

*/
