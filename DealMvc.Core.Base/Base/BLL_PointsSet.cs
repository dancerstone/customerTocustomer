using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 积分设置 - 业务层 PointsSet
    /// </summary>
    public class BLL_PointsSet
    {
        /// <summary>
        /// PointsSet构造函数
        /// </summary>
        public BLL_PointsSet()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.PointsSet> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.PointsSet> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.PointsSet>(Page ?? 0, "");
            _Pager.OrderColumn = new string[] { "id" };
            _Pager.OrderType = new DealMvc.Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC };

            _Pager.GetPageList();
            return _Pager;
        }

        /// <summary>
        /// 返回字段键值对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.PointsSet m_ps)
        {
            m_ps = DealMvc.Model.PointsSet.GetModel(m_ps.id ?? 0);
            if (m_ps.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_ps Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("PS_ConsumerPoints", m_ps.PS_ConsumerPoints);        //PS_Cate[Type= int?] - 完成消费送积分
            _nvce.Add("PS_RegPoints", m_ps.PS_RegPoints);        //PS_RegPoints[Type= int?] - 完成注册送积分
            _nvce.Add("PS_PhoneVerifyPoints", m_ps.PS_PhoneVerifyPoints);        //PS_PhoneVerifyPoints[Type= int?] - 验证手机送积分
            _nvce.Add("PS_EmailVerifyPoints", m_ps.PS_EmailVerifyPoints);        //PS_EmailVerifyPoints[Type= int?] - 验证邮箱送积分
            _nvce.Add("PS_Time", m_ps.PS_Time);        //PS_Time[Type=DateTime?] - 更新时间
            _nvce.Add("A", m_ps.A);        //A[Type=string] - 扩展A字段
            _nvce.Add("B", m_ps.B);        //B[Type=string] - 扩展B字段
            _nvce.Add("C", m_ps.C);        //C[Type=string] - 扩展C字段
            _nvce.Add("D", m_ps.D);        //D[Type=string] - 扩展D字段
            _nvce.Add("E", m_ps.E);        //E[Type=string] - 扩展E字段

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_PointsSet"></param>
        public void AEPointsSet(DealMvc.ControllerBase _CB, bool isEdit, ref Model.PointsSet p_ps)
        {
            Model.PointsSet m_ps = null;

            if (isEdit)
                m_ps = DealMvc.Model.PointsSet.GetModel(p_ps.id ?? 0);
            else
                m_ps = new Model.PointsSet();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.PointsSet>.Exists("=id", new object[] { p_ps.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }

            m_ps.PS_ConsumerPoints = p_ps.PS_ConsumerPoints ?? 0;        //PS_Cate[Type= int?] - 完成消费送积分
            m_ps.PS_RegPoints = p_ps.PS_RegPoints ?? 0;        //PS_RegPoints[Type= int?] - 完成注册送积分
            m_ps.PS_PhoneVerifyPoints = p_ps.PS_PhoneVerifyPoints ?? 0;        //PS_PhoneVerifyPoints[Type= int?] - 验证手机送积分
            m_ps.PS_EmailVerifyPoints = p_ps.PS_EmailVerifyPoints ?? 0;        //PS_EmailVerifyPoints[Type= int?] - 验证邮箱送积分
            m_ps.PS_Time = p_ps.PS_Time ?? DateTime.Now;        //PS_Time[Type=DateTime?] - 更新时间
            m_ps.A = p_ps.A;        //A[Type=string] - 扩展A字段
            m_ps.B = p_ps.B;        //B[Type=string] - 扩展B字段
            m_ps.C = p_ps.C;        //C[Type=string] - 扩展C字段
            m_ps.D = p_ps.D;        //D[Type=string] - 扩展D字段
            m_ps.E = p_ps.E;        //E[Type=string] - 扩展E字段


            p_ps = m_ps;
            if (isEdit)
            {
                m_ps.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "编辑成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_ps.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeletePointsSet(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.PointsSet>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
