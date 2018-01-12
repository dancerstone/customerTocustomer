using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 网站邮箱配置 - 实体类SiteEmail(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("SiteEmail", Info = "网站邮箱配置")]
    public class SiteEmail : EntityBase<SiteEmail>
    {
        public SiteEmail() { }

        #region BLLService-业务逻辑层
        /// <summary>
        /// 网站邮箱配置-SiteEmail-业务逻辑层
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
                List<SiteEmail> m_SiteEmailList = Orm.EntityCore<SiteEmail>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (SiteEmail _SiteEmail in m_SiteEmailList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _SiteEmail.id, _SiteEmail.id);
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

        private string _smtp;
        /// <summary>
        /// 邮箱SMTP
        /// </summary>
        [Column("Smtp", System.Data.SqlDbType.NVarChar)]
        [Check("Smtp", "邮箱SMTP", typeof(string))]
        public string Smtp
        {
            set { Authentication(value, "Smtp"); _smtp = value; }
            get { SetPValue("Smtp"); return _smtp == null ? "" : _smtp; }
        }

        private string _emailname;
        /// <summary>
        /// 用户帐号
        /// </summary>
        [Column("Emailname", System.Data.SqlDbType.NVarChar)]
        [Check("Emailname", "用户帐号", typeof(string))]
        public string Emailname
        {
            set { Authentication(value, "Emailname"); _emailname = value; }
            get { SetPValue("Emailname"); return _emailname == null ? "" : _emailname; }
        }

        private string _email;
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Column("Email", System.Data.SqlDbType.NVarChar)]
        [Check("Email", "邮箱地址", typeof(string))]
        public string Email
        {
            set { Authentication(value, "Email"); _email = value; }
            get { SetPValue("Email"); return _email == null ? "" : _email; }
        }

        private string _emailpwd;
        /// <summary>
        /// 邮箱密码
        /// </summary>
        [Column("Emailpwd", System.Data.SqlDbType.NVarChar)]
        [Check("Emailpwd", "邮箱密码", typeof(string))]
        public string Emailpwd
        {
            set { Authentication(value, "Emailpwd"); _emailpwd = value; }
            get { SetPValue("Emailpwd"); return _emailpwd == null ? "" : _emailpwd; }
        }

        private int? _port = 0;
        /// <summary>
        /// 邮箱发送端口
        /// </summary>
        [Column("Port", System.Data.SqlDbType.Int)]
        [Check("Port", "邮箱发送端口", typeof(int))]
        public int? Port
        {
            set { Authentication(value, "Port"); _port = value; }
            get { SetPValue("Port"); return _port; }
        }

        private bool _isregsendemail = false;
        /// <summary>
        /// 是否开启
        /// </summary>
        [Column("IsRegSendEmail", System.Data.SqlDbType.Bit)]
        [Check("IsRegSendEmail", "是否开启", typeof(bool))]
        public bool IsRegSendEmail
        {
            set { Authentication(value, "IsRegSendEmail"); _isregsendemail = value; }
            get { SetPValue("IsRegSendEmail"); return _isregsendemail; }
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
