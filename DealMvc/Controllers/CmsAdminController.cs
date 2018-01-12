using System;
using System.Web.Mvc;
using DealMvc.Filter;

namespace DealMvc.Controllers
{
    [ControlInfo("帐号模块")]
    public class CmsAdminController : ControllerBase
    {
        [Role("帐号列表", Description = "帐号列表", IsAuthorize = true)]
        public ActionResult AdminList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<Model.Admin> _Pager = new Orm.PagerEx.Pager<Model.Admin>(Page ?? 0, "");
            _Pager.OrderColumn = new string[] { "OrderNum", "id" };
            _Pager.OrderType = new Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC, Orm.PagerEx.SQLOrderType.DESC };
            _Pager.GetPageList();
            ViewData["Pager"] = _Pager;

            return View();
        }

        [Role("添加/编辑帐号", Description = "添加/编辑帐号", IsAuthorize = true)]
        public ActionResult AEAdmin(int? id, string AdminID, string AdminPwd, string AdminPwd2, string AdminRealName, string AdminSex, int? AdminSortID, string[] AdminPowerValues, string AdminRemark, int? OrderNum)
        {
            Model.Admin m_Admin = null;

            if (Request["a_i_id"] != null) id = Request["a_i_id"].ToInt32();

            bool isEdit = true;
            if (id == null) { isEdit = false; }
            ViewData["isEdit"] = isEdit;

            if (IsGet)
            {
                if (isEdit)
                {
                    m_Admin = DealMvc.Orm.EntityCore<Model.Admin>.GetModel(id ?? 0);
                    if (m_Admin.IsNull) return RedirectToAction("AdminList");

                    NameValueCollectionEx _NameValueCollectionEx = new NameValueCollectionEx();
                    _NameValueCollectionEx.Add("AdminID", m_Admin.AdminID);
                    //_NameValueCollectionEx.Add("AdminPwd", m_Admin.AdminPwd);
                    _NameValueCollectionEx.Add("AdminRealName", m_Admin.AdminRealName);
                    _NameValueCollectionEx.Add("AdminSex", m_Admin.AdminSex);
                    _NameValueCollectionEx.Add("AdminSortID", m_Admin.AdminSortID);
                    _NameValueCollectionEx.Add("AdminPowerValues", m_Admin.AdminPowerValues.Split('|'));
                    _NameValueCollectionEx.Add("AdminRemark", m_Admin.AdminRemark);
                    _NameValueCollectionEx.Add("OrderNum", m_Admin.OrderNum);

                    SetSaveFormCollection = _NameValueCollectionEx;
                }
            }
            if (IsPost)
            {
                try
                {
                    if (isEdit)
                        m_Admin = DealMvc.Orm.EntityCore<Model.Admin>.GetModel(id ?? 0);
                    else
                    {
                        if (string.IsNullOrEmpty(AdminPwd) || AdminPwd != AdminPwd2)
                        {
                            DealMvc.Orm.EntityCore<Model.Admin>.Update(m_Admin);
                            ExceptionEx.MyExceptionLog.AlertMessage(this, "两次输入密码不一致");
                            IsSaveForm = true;
                            return View();
                        }
                        m_Admin = new Model.Admin();
                        m_Admin.AdminPwd = DealMvc.Common.Net.DealString.MD5(AdminPwd);
                    }

                    if (!isEdit)
                    {
                        //检查某些字段是否已经存在相同的值
                        if (Orm.EntityCore<Model.Admin>.Exists(new string[] { "AdminID" }, new System.Data.SqlDbType[] { System.Data.SqlDbType.NVarChar }, new object[] { AdminID })) throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                    }

                    m_Admin.AdminID = AdminID;

                    m_Admin.AdminRealName = AdminRealName;
                    m_Admin.AdminSex = AdminSex;
                    m_Admin.AdminRemark = AdminRemark;

                    if (Request["a_i_id"] == null)
                    {
                        m_Admin.AdminSortID = AdminSortID;
                        m_Admin.AdminPowerValues = AdminPowerValues == null ? "" : string.Join("|", AdminPowerValues);
                        m_Admin.OrderNum = OrderNum;
                    }
                    m_Admin.UpTime = DateTime.Now;
                    m_Admin.LastLoginTime = DateTime.Now;

                    if (isEdit)
                    {
                        DealMvc.Orm.EntityCore<Model.Admin>.Update(m_Admin);
                        ExceptionEx.MyExceptionLog.AlertMessage(this, "编辑成功");
                        IsSaveForm = true;
                    }
                    else
                    {
                        DealMvc.Orm.EntityCore<Model.Admin>.Add(m_Admin);
                        ExceptionEx.MyExceptionLog.AlertMessage(this, "添加成功", true);
                        return ViewAndEmpty();
                    }

                }
                catch (Exception ce)
                {
                    IsSaveForm = true;
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            }
            return View();
        }

        [Role("删除帐号", Description = "删除帐号", IsAuthorize = true)]
        public ContentResult DeleteAdmin(string ids)
        {
            string result = "";
            string[] _ids = ids.Split(',');

            try
            {
                DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions = new SqlTranEx.SqlTranExtensions();
                for (int i = 0; i < _ids.Length; i++)
                {
                    DealMvc.Orm.EntityCore<Model.Admin>.Delete(int.Parse(_ids[i].ToString()), _SqlTranExtensions);
                }
                _SqlTranExtensions.ExecuteSqlTran(); result = "true";
            }
            catch (Exception ce)
            {
                ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                result = "false";
            }
            return Content(result);
        }

        [Role("修改帐号密码", Description = "修改帐号密码", IsAuthorize = true)]
        public ActionResult EAdminPwd(int? a_i_id, string AdminOldPwd, string AdminPwd, string AdminPwd2)
        {
            DealMvc.Model.Admin m_Admin = null;
            m_Admin = DealMvc.Orm.EntityCore<DealMvc.Model.Admin>.GetModel(a_i_id ?? 0);

            if (!m_Admin.IsNull)
            {
                NameValueCollectionEx _NameValueCollectionEx = new NameValueCollectionEx();
                _NameValueCollectionEx.Add("AdminID", m_Admin.AdminID);

                SetSaveFormCollection = _NameValueCollectionEx;
            }

            if (IsPost)
            {
                try
                {
                    if (m_Admin.IsNull)
                        throw new ExceptionEx.MyExceptionMessageBox("修改密码失败,请稍候在试");
                    else
                    {
                        if (string.IsNullOrEmpty(AdminPwd) || AdminPwd != AdminPwd2)
                        {
                            throw new ExceptionEx.MyExceptionMessageBox("两次输入密码不一致");
                        }
                        else
                        {
                            if (m_Admin.AdminPwd == Common.Net.DealString.MD5(AdminOldPwd))
                            {
                                m_Admin.AdminPwd = Common.Net.DealString.MD5(AdminPwd);
                                DealMvc.Orm.EntityCore<DealMvc.Model.Admin>.Update(m_Admin);
                                throw new ExceptionEx.MyExceptionMessageBox("修改密码成功");
                            }
                            else
                            {
                                throw new ExceptionEx.MyExceptionMessageBox("旧密码不正确");
                            }
                        }
                    }
                }
                catch (Exception ce)
                {
                    ExceptionEx.MyExceptionLog.WriteLog(this, ce);
                }
            }
            return View();
        }

        /// <summary>
        /// 获取帐号类型的权限值
        /// </summary>
        /// <returns></returns>
        public ContentResult GetAdminPowerValues(int? sort_id)
        {
            string output = "";
            try
            {
                output = Orm.EntityCore<Model.AdminSort>.GetModel(sort_id ?? 0).AdminSortPowerValues;
            }
            catch { }
            return Content(output);
        }

        //[Role("恢复帐号权限", Description = "恢复帐号权限", IsAuthorize = true)]
        public ContentResult ReQX(string ids)
        {
            string output = "0";
            try
            {
                SqlTranEx.SqlTranExtensions _SqlTranExtensions = new SqlTranEx.SqlTranExtensions();
                if (IsPost)
                {
                    string[] _ids = ids.Split(',');
                    for (int i = 0; i < _ids.Length; i++)
                    {
                        int id = _ids[i].ToInt32();
                        Model.Admin m_Admin = Orm.EntityCore<Model.Admin>.GetModel(id);
                        if (m_Admin.IsNull) { output = "0"; break; }
                        m_Admin.AdminPowerValues = m_Admin.AdminSort.AdminSortPowerValues;
                        Orm.EntityCore<Model.Admin>.Update(m_Admin, _SqlTranExtensions);
                    }
                }
                _SqlTranExtensions.ExecuteSqlTran();
                output = "1";
            }
            catch { }
            return Content(output);
        }
    }
}