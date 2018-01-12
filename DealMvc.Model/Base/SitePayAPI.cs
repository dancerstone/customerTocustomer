using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 网站支付信息 - 实体类SitePayAPI(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("SitePayAPI", Info = "网站支付信息")]
    public class SitePayAPI : EntityBase<SitePayAPI>
    {
        public SitePayAPI() { }

        #region BLLService-业务逻辑层
        /// <summary>
        /// 网站支付信息-SitePayAPI-业务逻辑层
        /// </summary>
        public static class BLLService
        {

        }
        #endregion


        #region Option
        public static string GetOptions()
        {
            StringBuilder output = new StringBuilder();
            try
            {
                List<SitePayAPI> m_SitePayAPIList = Orm.EntityCore<SitePayAPI>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (SitePayAPI _SitePayAPI in m_SitePayAPIList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _SitePayAPI.id, _SitePayAPI.id);
                }
            }
            catch { }
            return output.ToString();
        }
        #endregion


        #region GetConnectedModel
        #endregion


        #region Model

        private int? _id;
        /// <summary>
        /// 编号
        /// </summary>
        [Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
        [Check("id", "编号", typeof(int))]
        public override int? id
        {
            set { Authentication(value, "id"); _id = value; }
            get { SetPValue("id"); return _id; }
        }

        private string _apitype;
        /// <summary>
        /// API类型
        /// </summary>
        [Column("ApiType", System.Data.SqlDbType.NVarChar)]
        [Check("ApiType", "API类型", typeof(string))]
        public string ApiType
        {
            set { Authentication(value, "ApiType"); _apitype = value; }
            get { SetPValue("ApiType"); return _apitype == null ? "" : _apitype; }
        }

        private string _account;
        /// <summary>
        /// 签约账号
        /// </summary>
        [Column("Account", System.Data.SqlDbType.NVarChar)]
        [Check("Account", "签约账号", typeof(string))]
        public string Account
        {
            set { Authentication(value, "Account"); _account = value; }
            get { SetPValue("Account"); return _account == null ? "" : _account; }
        }

        private string _appidentity;
        /// <summary>
        /// 合作者身份
        /// </summary>
        [Column("AppIdentity", System.Data.SqlDbType.NVarChar)]
        [Check("AppIdentity", "合作者身份", typeof(string))]
        public string AppIdentity
        {
            set { Authentication(value, "AppIdentity"); _appidentity = value; }
            get { SetPValue("AppIdentity"); return _appidentity == null ? "" : _appidentity; }
        }

        private string _appkey;
        /// <summary>
        /// 密钥
        /// </summary>
        [Column("AppKey", System.Data.SqlDbType.NVarChar)]
        [Check("AppKey", "密钥", typeof(string))]
        public string AppKey
        {
            set { Authentication(value, "AppKey"); _appkey = value; }
            get { SetPValue("AppKey"); return _appkey == null ? "" : _appkey; }
        }

        private DateTime? _uptime = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpTime", System.Data.SqlDbType.DateTime)]
        [Check("UpTime", "更新时间", typeof(DateTime))]
        public DateTime? UpTime
        {
            set { Authentication(value, "UpTime"); _uptime = value; }
            get { SetPValue("UpTime"); return _uptime; }
        }
        #endregion Model
    }

}
