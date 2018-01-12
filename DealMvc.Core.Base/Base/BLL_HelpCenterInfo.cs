using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 帮助中心信息 - 业务层 HelpCenterInfo
    /// </summary>
    public class BLL_HelpCenterInfo
    {
        /// <summary>
        /// HelpCenterInfo构造函数
        /// </summary>
        public BLL_HelpCenterInfo()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.HelpCenterInfo> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.HelpCenterInfo> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.HelpCenterInfo>(Page ?? 0, "");
            _Pager.OrderColumn = new string[] { "HCI_Sort" };
            _Pager.OrderType = new DealMvc.Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC };

            _Pager.GetPageList();
            return _Pager;
        }

        /// <summary>
        /// 返回字段键值对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.HelpCenterInfo m_hci)
        {
            m_hci = DealMvc.Model.HelpCenterInfo.GetModel(m_hci.id ?? 0);
            if (m_hci.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_hci Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("HelpCateID1", m_hci.HelpCateID1);        //HelpCateID1[Type= int?] - 帮助中心一级分类
            _nvce.Add("HelpCateID2", m_hci.HelpCateID2);        //HelpCateID2[Type= int?] - 帮助中心二级分类
            _nvce.Add("HCI_LittleTitle", m_hci.HCI_LittleTitle);        //HCI_LittleTitle[Type=string] - 小标题
            _nvce.Add("HCI_Title", m_hci.HCI_Title);        //HCI_Title[Type=string] - 详细标题
            _nvce.Add("HCI_Content", m_hci.HCI_Content);        //HCI_Content[Type=string] - 信息内容
            _nvce.Add("HCI_IsDefault", m_hci.HCI_IsDefault);        //HCI_IsDefault[Type=bool] - 是否默认展示
            _nvce.Add("HCI_Sort", m_hci.HCI_Sort);        //HCI_Sort[Type= int?] - 排序
            _nvce.Add("HCI_Time", m_hci.HCI_Time);        //HCI_Time[Type=DateTime?] - 操作时间
            _nvce.Add("A", m_hci.A);        //A[Type=string] - 扩展A字段
            _nvce.Add("B", m_hci.B);        //B[Type=string] - 扩展B字段
            _nvce.Add("C", m_hci.C);        //C[Type=string] - 扩展C字段
            _nvce.Add("D", m_hci.D);        //D[Type=string] - 扩展D字段
            _nvce.Add("E", m_hci.E);        //E[Type=string] - 扩展E字段

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_HelpCenterInfo"></param>
        public void AEHelpCenterInfo(DealMvc.ControllerBase _CB, bool isEdit, ref Model.HelpCenterInfo p_hci)
        {
            Model.HelpCenterInfo m_hci = null;

            if (isEdit)
                m_hci = DealMvc.Model.HelpCenterInfo.GetModel(p_hci.id ?? 0);
            else
                m_hci = new Model.HelpCenterInfo();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.HelpCenterInfo>.Exists("=id", new object[] { p_hci.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
                //if (Orm.EntityCore<Model.HelpCenterInfo>.Exists("=HCI_LittleTitle", new object[] { p_hci.HCI_LittleTitle }))
                //{
                //    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                //}
                if (Orm.EntityCore<Model.HelpCenterInfo>.Exists("=HCI_Title", new object[] { p_hci.HCI_Title }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }


            m_hci.HelpCateID1 = p_hci.HelpCateID1 ?? 0;        //HelpCateID1[Type= int?] - 帮助中心一级分类
            m_hci.HelpCateID2 = p_hci.HelpCateID2 ?? 0;        //HelpCateID2[Type= int?] - 帮助中心二级分类
            m_hci.HCI_LittleTitle = p_hci.HCI_LittleTitle;        //HCI_LittleTitle[Type=string] - 小标题
            m_hci.HCI_Title = p_hci.HCI_Title;        //HCI_Title[Type=string] - 详细标题
            m_hci.HCI_Content = p_hci.HCI_Content;        //HCI_Content[Type=string] - 信息内容
            m_hci.HCI_IsDefault = p_hci.HCI_IsDefault;        //HCI_IsDefault[Type=bool] - 是否默认展示
            if (p_hci.HCI_IsDefault == true)
            {
                List<Model.HelpCenterInfo> collection = Model.HelpCenterInfo.GetModelList(t => t.HCI_IsDefault == true).List;
                foreach (Model.HelpCenterInfo item in collection)
                {
                    item.HCI_IsDefault = false;
                    item.Update();
                }
            }
            m_hci.HCI_Sort = p_hci.HCI_Sort ?? 0;        //HCI_Sort[Type= int?] - 排序
            m_hci.HCI_Time = p_hci.HCI_Time ?? DateTime.Now;        //HCI_Time[Type=DateTime?] - 操作时间
            m_hci.A = p_hci.A;        //A[Type=string] - 扩展A字段
            m_hci.B = p_hci.B;        //B[Type=string] - 扩展B字段
            m_hci.C = p_hci.C;        //C[Type=string] - 扩展C字段
            m_hci.D = p_hci.D;        //D[Type=string] - 扩展D字段
            m_hci.E = p_hci.E;        //E[Type=string] - 扩展E字段


            p_hci = m_hci;
            if (isEdit)
            {
                m_hci.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "编辑成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_hci.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteHelpCenterInfo(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.HelpCenterInfo>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.HelpCenterInfo> PagerList(int? Page, int? hcc_id)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.HelpCenterInfo> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.HelpCenterInfo>(Page ?? 0, t => t.id != null);
            _Pager.OrderColumn = new string[] { "HCI_Sort" };
            _Pager.OrderType = new DealMvc.Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC };

            _Pager.GetPageList();
            return _Pager;
        }
        #endregion

        #region 前台逻辑

        #endregion

        #region 公用逻辑

        #endregion

    }

}
