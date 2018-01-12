using System;
using System.Web.Mvc;
using DealMvc.Filter;

namespace DealMvc.Controllers
{
    [ControlInfo("帐号类型模块")]
    public class CmsAdminSortController : ControllerBase
    {
        [Role("帐号类型列表", Description = "帐号类型列表", IsAuthorize = true)]
        public ActionResult AdminSortList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<Model.AdminSort> _Pager = new Orm.PagerEx.Pager<Model.AdminSort>(Page ?? 0, "");
            _Pager.OrderColumn = new string[] { "OrderNum" };
            _Pager.OrderType = new Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC };
            _Pager.GetPageList();
            ViewData["Pager"] = _Pager;

            return View();
        }

        [Role("添加/编辑帐号类型", Description = "添加/编辑帐号类型", IsAuthorize = true)]
        public ActionResult AEAdminSort(int? id, string AdminSortName, string[] AdminSortPowerValues, int? OrderNum)
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
                    if (m_AdminSort.IsNull) return RedirectToAction("AdminSortList");

                    NameValueCollectionEx _NameValueCollectionEx = new NameValueCollectionEx();
                    _NameValueCollectionEx.Add("AdminSortName", m_AdminSort.AdminSortName);
                    _NameValueCollectionEx.Add("AdminSortPowerValues", m_AdminSort.AdminSortPowerValues.Split('|'));
                    _NameValueCollectionEx.Add("OrderNum", m_AdminSort.OrderNum);
                    _NameValueCollectionEx.Add("UpTime", m_AdminSort.UpTime);

                    SetSaveFormCollection = _NameValueCollectionEx;
                }
            }
            if (IsPost)
            {
                try
                {
                    if (isEdit)
                        m_AdminSort = DealMvc.Orm.EntityCore<Model.AdminSort>.GetModel(id ?? 0);
                    else
                        m_AdminSort = new Model.AdminSort();

                    if (!isEdit)
                    {
                        //检查某些字段是否已经存在相同的值
                        if (Orm.EntityCore<Model.AdminSort>.Exists(new string[] { "AdminSortName" }, new System.Data.SqlDbType[] { System.Data.SqlDbType.NVarChar }, new object[] { AdminSortName })) throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                    }

                    m_AdminSort.AdminSortName = AdminSortName;
                    m_AdminSort.AdminSortPowerValues = AdminSortPowerValues != null ? string.Join("|", AdminSortPowerValues) : "";
                    m_AdminSort.OrderNum = OrderNum;
                    m_AdminSort.UpTime = DateTime.Now;

                    if (isEdit)
                    {
                        DealMvc.Orm.EntityCore<Model.AdminSort>.Update(m_AdminSort);
                        ExceptionEx.MyExceptionLog.AlertMessage(this, "编辑成功");
                        IsSaveForm = true;
                    }
                    else
                    {
                        DealMvc.Orm.EntityCore<Model.AdminSort>.Add(m_AdminSort);
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

        [Role("删除帐号类型", Description = "删除帐号类型", IsAuthorize = true)]
        public ContentResult DeleteAdminSort(string ids)
        {
            string result = "";
            string[] _ids = ids.Split(',');

            try
            {
                DealMvc.SqlTranEx.SqlTranExtensions _SqlTranExtensions = new SqlTranEx.SqlTranExtensions();
                for (int i = 0; i < _ids.Length; i++)
                {
                    DealMvc.Orm.EntityCore<Model.AdminSort>.Delete(int.Parse(_ids[i].ToString()), _SqlTranExtensions);
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
    }
}