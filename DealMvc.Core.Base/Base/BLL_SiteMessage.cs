using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 网站短信配置 - 业务层 SiteMessage
    /// </summary>
    public class BLL_SiteMessage
    {
        /// <summary>
        /// SiteMessage构造函数
        /// </summary>
        public BLL_SiteMessage()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteMessage> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteMessage> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.SiteMessage>(Page ?? 0, "");
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
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.SiteMessage m_sm)
        {
            m_sm = DealMvc.Model.SiteMessage.GetModel(m_sm.id ?? 0);
            if (m_sm.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_sm Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("UserName", m_sm.UserName);        //UserName[Type=string] - 账号
            _nvce.Add("UserPwd", m_sm.UserPwd);        //UserPwd[Type=string] - 密码
            _nvce.Add("UpTime", m_sm.UpTime);        //UpTime[Type=DateTime?] - 更新时间

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_SiteMessage"></param>
        public void AESiteMessage(DealMvc.ControllerBase _CB, bool isEdit, ref Model.SiteMessage p_sm)
        {
            Model.SiteMessage m_sm = null;

            if (isEdit)
                m_sm = DealMvc.Model.SiteMessage.GetModel(p_sm.id ?? 0);
            else
                m_sm = new Model.SiteMessage();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.SiteMessage>.Exists("=id", new object[] { p_sm.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }

            m_sm.UserName = p_sm.UserName;        //UserName[Type=string] - 账号
            m_sm.UserPwd = p_sm.UserPwd;        //UserPwd[Type=string] - 密码
            m_sm.UpTime = p_sm.UpTime ?? DateTime.Now;        //UpTime[Type=DateTime?] - 更新时间


            p_sm = m_sm;
            if (isEdit)
            {
                m_sm.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "修改网站短信信息成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_sm.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteSiteMessage(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.SiteMessage>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
