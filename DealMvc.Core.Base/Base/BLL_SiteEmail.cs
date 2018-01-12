using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 网站邮箱配置 - 业务层 SiteEmail
    /// </summary>
    public class BLL_SiteEmail
    {
        /// <summary>
        /// SiteEmail构造函数
        /// </summary>
        public BLL_SiteEmail()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteEmail> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteEmail> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.SiteEmail>(Page ?? 0, "");
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
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.SiteEmail m_se)
        {
            m_se = DealMvc.Model.SiteEmail.GetModel(m_se.id ?? 0);
            if (m_se.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_se Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("Smtp", m_se.Smtp);        //Smtp[Type=string] - 邮箱SMTP
            _nvce.Add("Emailname", m_se.Emailname);        //Emailname[Type=string] - 用户帐号
            _nvce.Add("Email", m_se.Email);        //Email[Type=string] - 邮箱地址
            _nvce.Add("Emailpwd", m_se.Emailpwd);        //Emailpwd[Type=string] - 邮箱密码
            _nvce.Add("Port", m_se.Port);        //Port[Type= int?] - 邮箱发送端口
            _nvce.Add("IsRegSendEmail", m_se.IsRegSendEmail);        //IsRegSendEmail[Type=bool] - 是否开启
            _nvce.Add("UpTime", m_se.UpTime);        //UpTime[Type=DateTime?] - 更新时间

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_SiteEmail"></param>
        public void AESiteEmail(DealMvc.ControllerBase _CB, bool isEdit, ref Model.SiteEmail p_se)
        {
            Model.SiteEmail m_se = null;

            if (isEdit)
                m_se = DealMvc.Model.SiteEmail.GetModel(p_se.id ?? 0);
            else
                m_se = new Model.SiteEmail();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.SiteEmail>.Exists("=id", new object[] { p_se.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }
            m_se.Smtp = p_se.Smtp;        //Smtp[Type=string] - 邮箱SMTP
            m_se.Emailname = p_se.Emailname;        //Emailname[Type=string] - 用户帐号
            m_se.Email = p_se.Email;        //Email[Type=string] - 邮箱地址
            m_se.Emailpwd = p_se.Emailpwd;        //Emailpwd[Type=string] - 邮箱密码
            m_se.Port = p_se.Port ?? 0;        //Port[Type= int?] - 邮箱发送端口
            m_se.IsRegSendEmail = p_se.IsRegSendEmail;        //IsRegSendEmail[Type=bool] - 是否开启
            m_se.UpTime = p_se.UpTime ?? DateTime.Now;        //UpTime[Type=DateTime?] - 更新时间


            p_se = m_se;
            if (isEdit)
            {
                m_se.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "修改网站邮箱信息成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_se.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteSiteEmail(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.SiteEmail>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
