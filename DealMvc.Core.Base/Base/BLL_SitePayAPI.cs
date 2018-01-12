using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 网站支付信息 - 业务层 SitePayAPI
    /// </summary>
    public class BLL_SitePayAPI
    {
        /// <summary>
        /// SitePayAPI构造函数
        /// </summary>
        public BLL_SitePayAPI()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SitePayAPI> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SitePayAPI> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.SitePayAPI>(Page ?? 0, "");
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
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.SitePayAPI m_spapi)
        {
            m_spapi = DealMvc.Model.SitePayAPI.GetModel(m_spapi.id ?? 0);
            if (m_spapi.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_spapi Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("ApiType", m_spapi.ApiType);        //ApiType[Type=string] - API类型
            _nvce.Add("Account", m_spapi.Account);        //Account[Type=string] - 签约账号
            _nvce.Add("AppIdentity", m_spapi.AppIdentity);        //AppIdentity[Type=string] - 合作者身份
            _nvce.Add("AppKey", m_spapi.AppKey);        //AppKey[Type=string] - 密钥
            _nvce.Add("UpTime", m_spapi.UpTime);        //UpTime[Type=DateTime?] - 更新时间

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_SitePayAPI"></param>
        public void AESitePayAPI(DealMvc.ControllerBase _CB, bool isEdit, ref Model.SitePayAPI p_spapi)
        {
            Model.SitePayAPI m_spapi = null;

            if (isEdit)
                m_spapi = DealMvc.Model.SitePayAPI.GetModel(p_spapi.id ?? 0);
            else
                m_spapi = new Model.SitePayAPI();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.SitePayAPI>.Exists("=id", new object[] { p_spapi.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }

            m_spapi.ApiType = p_spapi.ApiType;        //ApiType[Type=string] - API类型
            m_spapi.Account = p_spapi.Account;        //Account[Type=string] - 签约账号
            m_spapi.AppIdentity = p_spapi.AppIdentity;        //AppIdentity[Type=string] - 合作者身份
            m_spapi.AppKey = p_spapi.AppKey;        //AppKey[Type=string] - 密钥
            m_spapi.UpTime = p_spapi.UpTime ?? DateTime.Now;        //UpTime[Type=DateTime?] - 更新时间


            p_spapi = m_spapi;
            if (isEdit)
            {
                m_spapi.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "修改网站支付信息成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_spapi.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteSitePayAPI(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.SitePayAPI>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
