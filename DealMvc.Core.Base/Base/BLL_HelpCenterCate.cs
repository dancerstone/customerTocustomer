using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 帮助中心分类 - 业务层 HelpCenterCate
    /// </summary>
    public class BLL_HelpCenterCate
    {
        /// <summary>
        /// HelpCenterCate构造函数
        /// </summary>
        public BLL_HelpCenterCate()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.HelpCenterCate> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.HelpCenterCate> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.HelpCenterCate>(Page ?? 0, "");
            _Pager.OrderColumn = new string[] { "HCC_Sort" };
            _Pager.OrderType = new DealMvc.Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC };

            _Pager.GetPageList();
            return _Pager;
        }

        /// <summary>
        /// 返回字段键值对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.HelpCenterCate m_hcc)
        {
            m_hcc = DealMvc.Model.HelpCenterCate.GetModel(m_hcc.id ?? 0);
            if (m_hcc.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_hcc Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("HCC_Name", m_hcc.HCC_Name);        //HCC_Name[Type=string] - 分类名称
            _nvce.Add("HCC_Sort", m_hcc.HCC_Sort);        //HCC_Sort[Type= int?] - 排序
            _nvce.Add("HCC_Time", m_hcc.HCC_Time);        //HCC_Time[Type=DateTime?] - 操作时间
            _nvce.Add("A", m_hcc.A);        //A[Type=string] - 扩展A字段
            _nvce.Add("B", m_hcc.B);        //B[Type=string] - 扩展B字段
            _nvce.Add("C", m_hcc.C);        //C[Type=string] - 扩展C字段
            _nvce.Add("D", m_hcc.D);        //D[Type=string] - 扩展D字段
            _nvce.Add("E", m_hcc.E);        //E[Type=string] - 扩展E字段

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_HelpCenterCate"></param>
        public void AEHelpCenterCate(DealMvc.ControllerBase _CB, bool isEdit, ref Model.HelpCenterCate p_hcc)
        {
            Model.HelpCenterCate m_hcc = null;

            if (isEdit)
                m_hcc = DealMvc.Model.HelpCenterCate.GetModel(p_hcc.id ?? 0);
            else
                m_hcc = new Model.HelpCenterCate();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.HelpCenterCate>.Exists("=id", new object[] { p_hcc.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
                if (Orm.EntityCore<Model.HelpCenterCate>.Exists("=HCC_Name", new object[] { p_hcc.HCC_Name }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
                m_hcc.HCC_ParentID = p_hcc.HCC_ParentID ?? 0;        //IndustryCate_TopID[Type=int?] - 上级分类
                #region 判断级别

                int NewParentID = p_hcc.HCC_ParentID.ToInt32();
                if (NewParentID <= 0)
                {
                    m_hcc.HCC_Level = 1;//IC_Level[Type=int?] - 等级
                }
                else
                {
                    Model.HelpCenterCate ParentMenu = Model.HelpCenterCate.GetModel(NewParentID);
                    if (ParentMenu == null || ParentMenu.IsNull)
                    {
                        throw new ExceptionEx.MyExceptionMessageBox("您选择的上级分类不存在！");
                    }

                    m_hcc.HCC_Level = ParentMenu.HCC_Level + 1;//IC_Level[Type=int?] - 等级
                    if (m_hcc.HCC_Level > 4)
                        throw new ExceptionEx.MyExceptionMessageBox("您只能添加【两级】菜单！");
                }
                #endregion
            }
            else
            {

            }

            m_hcc.HCC_Name = p_hcc.HCC_Name;        //HCC_Name[Type=string] - 分类名称
            m_hcc.HCC_Sort = p_hcc.HCC_Sort ?? 0;        //HCC_Sort[Type= int?] - 排序
            m_hcc.HCC_Time = p_hcc.HCC_Time ?? DateTime.Now;        //HCC_Time[Type=DateTime?] - 操作时间
            m_hcc.A = p_hcc.A;        //A[Type=string] - 扩展A字段
            m_hcc.B = p_hcc.B;        //B[Type=string] - 扩展B字段
            m_hcc.C = p_hcc.C;        //C[Type=string] - 扩展C字段
            m_hcc.D = p_hcc.D;        //D[Type=string] - 扩展D字段
            m_hcc.E = p_hcc.E;        //E[Type=string] - 扩展E字段


            p_hcc = m_hcc;
            if (isEdit)
            {
                m_hcc.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "编辑成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_hcc.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteHelpCenterCate(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.HelpCenterCate>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
        public Dictionary<Model.HelpCenterCate, string> GetTreeList(int SelectID, int StopID)
        {
            Dictionary<Model.HelpCenterCate, string> list = new Dictionary<Model.HelpCenterCate, string>();

            GetTreeList_Sub(list, 0, "", SelectID, StopID);

            return list;
        }
        private void GetTreeList_Sub(Dictionary<Model.HelpCenterCate, string> list, int NowID, string Index, int SelectID, int StopID)
        {
            DealMvc.WebCache.WebCache.Close();
            List<Model.HelpCenterCate> ActivityCateList = Model.HelpCenterCate.GetModelList(int.MaxValue, t => t.HCC_ParentID == NowID, t => t.HCC_Sort.lb_Desc()).List;
            DealMvc.WebCache.WebCache.Reset();
            foreach (Model.HelpCenterCate HelpCenterCate in ActivityCateList)
            {
                if (StopID == (HelpCenterCate.id ?? 0)) continue;
                string select = SelectID == (HelpCenterCate.id ?? 0) ? " selected='selected' " : "";

                HelpCenterCate.HCC_Name = Index + HelpCenterCate.HCC_Name;
                list.Add(HelpCenterCate, string.Format("<option {2} value='{0}'>{1}</option>", HelpCenterCate.id, HelpCenterCate.HCC_Name, select));

                GetTreeList_Sub(list, HelpCenterCate.id ?? 0, Index + "&nbsp;&nbsp;&nbsp;&nbsp;", SelectID, StopID);
            }
        }
        #endregion

        #region 后台逻辑

        #endregion

        #region 前台逻辑

        #endregion

        #region 公用逻辑

        #endregion

    }

}
