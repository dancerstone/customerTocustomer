using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 网站配置信息 - 实体类SiteInfo(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("SiteInfo", Info = "网站配置信息")]
    public class SiteInfo : EntityBase<SiteInfo>
    {
        public SiteInfo() { }

        #region BLLService-业务逻辑层
        /// <summary>
        /// 网站配置信息-SiteInfo-业务逻辑层
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
                List<SiteInfo> m_SiteInfoList = Orm.EntityCore<SiteInfo>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (SiteInfo _SiteInfo in m_SiteInfoList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _SiteInfo.id, _SiteInfo.id);
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
        /// id
        /// </summary>
        [Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
        [Check("id", "id", typeof(int))]
        public override int? id
        {
            set { Authentication(value, "id"); _id = value; }
            get { SetPValue("id"); return _id; }
        }

        private string _webname;
        /// <summary>
        /// 网站名称
        /// </summary>
        [Column("WebName", System.Data.SqlDbType.NVarChar)]
        [Check("WebName", "网站名称", typeof(string))]
        public string WebName
        {
            set { Authentication(value, "WebName"); _webname = value; }
            get { SetPValue("WebName"); return _webname == null ? "" : _webname; }
        }

        private string _webaddress;
        /// <summary>
        /// 网站域名
        /// </summary>
        [Column("WebAddress", System.Data.SqlDbType.NVarChar)]
        [Check("WebAddress", "网站域名", typeof(string))]
        public string WebAddress
        {
            set { Authentication(value, "WebAddress"); _webaddress = value; }
            get { SetPValue("WebAddress"); return _webaddress == null ? "" : _webaddress; }
        }

        private string _webtitle;
        /// <summary>
        /// 网站标题
        /// </summary>
        [Column("WebTitle", System.Data.SqlDbType.NVarChar)]
        [Check("WebTitle", "网站标题", typeof(string))]
        public string WebTitle
        {
            set { Authentication(value, "WebTitle"); _webtitle = value; }
            get { SetPValue("WebTitle"); return _webtitle == null ? "" : _webtitle; }
        }

        private string _webkeyword;
        /// <summary>
        /// 网站关键词
        /// </summary>
        [Column("WebKeyword", System.Data.SqlDbType.NText)]
        [Check("WebKeyword", "网站关键词", typeof(string))]
        public string WebKeyword
        {
            set { Authentication(value, "WebKeyword"); _webkeyword = value; }
            get { SetPValue("WebKeyword"); return _webkeyword == null ? "" : _webkeyword; }
        }

        private string _webdelimiter;
        /// <summary>
        /// 网站标题分隔符
        /// </summary>
        [Column("WebDelimiter", System.Data.SqlDbType.NVarChar)]
        [Check("WebDelimiter", "网站标题分隔符", typeof(string))]
        public string WebDelimiter
        {
            set { Authentication(value, "WebDelimiter"); _webdelimiter = value; }
            get { SetPValue("WebDelimiter"); return _webdelimiter == null ? "" : _webdelimiter; }
        }

        private string _webcopyright;
        /// <summary>
        /// 版权
        /// </summary>
        [Column("WebCopyright", System.Data.SqlDbType.NVarChar)]
        [Check("WebCopyright", "版权", typeof(string))]
        public string WebCopyright
        {
            set { Authentication(value, "WebCopyright"); _webcopyright = value; }
            get { SetPValue("WebCopyright"); return _webcopyright == null ? "" : _webcopyright; }
        }

        private bool _webstatus = false;
        /// <summary>
        /// 网站状态(开启网站、关闭网站)
        /// </summary>
        [Column("WebStatus", System.Data.SqlDbType.Bit)]
        [Check("WebStatus", "网站状态(开启网站、关闭网站)", typeof(bool))]
        public bool WebStatus
        {
            set { Authentication(value, "WebStatus"); _webstatus = value; }
            get { SetPValue("WebStatus"); return _webstatus; }
        }

        private string _webcloseremark;
        /// <summary>
        /// 关闭网站原因
        /// </summary>
        [Column("WebCloseRemark", System.Data.SqlDbType.NText)]
        [Check("WebCloseRemark", "关闭网站原因", typeof(string))]
        public string WebCloseRemark
        {
            set { Authentication(value, "WebCloseRemark"); _webcloseremark = value; }
            get { SetPValue("WebCloseRemark"); return _webcloseremark == null ? "" : _webcloseremark; }
        }

        private bool _webcompetence = false;
        /// <summary>
        /// 网站后台权限
        /// </summary>
        [Column("WebCompetence", System.Data.SqlDbType.Bit)]
        [Check("WebCompetence", "网站后台权限", typeof(bool))]
        public bool WebCompetence
        {
            set { Authentication(value, "WebCompetence"); _webcompetence = value; }
            get { SetPValue("WebCompetence"); return _webcompetence; }
        }

        private bool _isopendatacache = false;
        /// <summary>
        /// 数据缓存  是否启用
        /// </summary>
        [Column("IsOpenDataCache", System.Data.SqlDbType.Bit)]
        [Check("IsOpenDataCache", "数据缓存  是否启用", typeof(bool))]
        public bool IsOpenDataCache
        {
            set { Authentication(value, "IsOpenDataCache"); _isopendatacache = value; }
            get { SetPValue("IsOpenDataCache"); return _isopendatacache; }
        }

        private int? _datacachetime = 0;
        /// <summary>
        /// 缓存时间(秒)
        /// </summary>
        [Column("DataCacheTime", System.Data.SqlDbType.Int)]
        [Check("DataCacheTime", "缓存时间(秒)", typeof(int))]
        public int? DataCacheTime
        {
            set { Authentication(value, "DataCacheTime"); _datacachetime = value; }
            get { SetPValue("DataCacheTime"); return _datacachetime; }
        }
        private double? _proportionmention = 0;
        /// <summary>
        /// 平台提拥比例
        /// </summary>
        [Column("ProportionMention", System.Data.SqlDbType.Float)]
        [Check("ProportionMention", "平台提拥比例", typeof(double))]
        public double? ProportionMention
        {
            set { Authentication(value, "ProportionMention"); _proportionmention = value; }
            get { SetPValue("ProportionMention"); return _proportionmention; }
        }

        private string _webbottominfo;
        /// <summary>
        /// 网站底部信息
        /// </summary>
        [Column("WebBottomInfo", System.Data.SqlDbType.NText)]
        [Check("WebBottomInfo", "网站底部信息", typeof(string))]
        public string WebBottomInfo
        {
            set { Authentication(value, "WebBottomInfo"); _webbottominfo = value; }
            get { SetPValue("WebBottomInfo"); return _webbottominfo == null ? "" : _webbottominfo; }
        }
        private string _webfriendlinks;
        /// <summary>
        /// 友情链接
        /// </summary>
        [Column("WebFriendLinks", System.Data.SqlDbType.NText)]
        [Check("WebFriendLinks", "友情链接", typeof(string))]
        public string WebFriendLinks
        {
            set { Authentication(value, "WebFriendLinks"); _webfriendlinks = value; }
            get { SetPValue("WebFriendLinks"); return _webfriendlinks == null ? "" : _webfriendlinks; }
        }
        private string _webbottomcontact;
        /// <summary>
        /// 底部联系方式
        /// </summary>
        [Column("WebBottomContact", System.Data.SqlDbType.NVarChar)]
        [Check("WebBottomContact", "底部联系方式", typeof(string))]
        public string WebBottomContact
        {
            set { Authentication(value, "WebBottomContact"); _webbottomcontact = value; }
            get { SetPValue("WebBottomContact"); return _webbottomcontact == null ? "" : _webbottomcontact; }
        }

        private string _webbottomhours;
        /// <summary>
        /// 营业时间
        /// </summary>
        [Column("WebBottomHours", System.Data.SqlDbType.NVarChar)]
        [Check("WebBottomHours", "营业时间", typeof(string))]
        public string WebBottomHours
        {
            set { Authentication(value, "WebBottomHours"); _webbottomhours = value; }
            get { SetPValue("WebBottomHours"); return _webbottomhours == null ? "" : _webbottomhours; }
        }
        private string _webregservice;
        /// <summary>
        /// 注册条款
        /// </summary>
        [Column("WebRegService", System.Data.SqlDbType.NText)]
        [Check("WebRegService", "注册条款", typeof(string))]
        public string WebRegService
        {
            set { Authentication(value, "WebRegService"); _webregservice = value; }
            get { SetPValue("WebRegService"); return _webregservice == null ? "" : _webregservice; }
        }
        private string _webweixinimage;
        /// <summary>
        /// 微信二维码图片
        /// </summary>
        [Column("WebWeiXinImage", System.Data.SqlDbType.NVarChar)]
        [Check("WebWeiXinImage", "微信二维码图片", typeof(string))]
        public string WebWeiXinImage
        {
            set { Authentication(value, "WebWeiXinImage"); _webweixinimage = value; }
            get { SetPValue("WebWeiXinImage"); return _webweixinimage == null ? "" : _webweixinimage; }
        }

        private string _webweiboimage;
        /// <summary>
        /// 微博二维码图片
        /// </summary>
        [Column("WebWeiBoImage", System.Data.SqlDbType.NVarChar)]
        [Check("WebWeiBoImage", "微博二维码图片", typeof(string))]
        public string WebWeiBoImage
        {
            set { Authentication(value, "WebWeiBoImage"); _webweiboimage = value; }
            get { SetPValue("WebWeiBoImage"); return _webweiboimage == null ? "" : _webweiboimage; }
        }

        private string _webcontactphone;
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("WebContactPhone", System.Data.SqlDbType.NVarChar)]
        [Check("WebContactPhone", "联系电话", typeof(string))]
        public string WebContactPhone
        {
            set { Authentication(value, "WebContactPhone"); _webcontactphone = value; }
            get { SetPValue("WebContactPhone"); return _webcontactphone == null ? "" : _webcontactphone; }
        }

        private string _webcontactemail;
        /// <summary>
        /// 联系邮箱
        /// </summary>
        [Column("WebContactEmail", System.Data.SqlDbType.NVarChar)]
        [Check("WebContactEmail", "联系邮箱", typeof(string))]
        public string WebContactEmail
        {
            set { Authentication(value, "WebContactEmail"); _webcontactemail = value; }
            get { SetPValue("WebContactEmail"); return _webcontactemail == null ? "" : _webcontactemail; }
        }

        private string _kefuqq;
        /// <summary>
        /// 客服QQ
        /// </summary>
        [Column("KeFuQQ", System.Data.SqlDbType.NVarChar)]
        [Check("KeFuQQ", "客服QQ", typeof(string))]
        public string KeFuQQ
        {
            set { Authentication(value, "KeFuQQ"); _kefuqq = value; }
            get { SetPValue("KeFuQQ"); return _kefuqq == null ? "" : _kefuqq; }
        }
        private string _beforesalephone;
        /// <summary>
        /// 售前电话
        /// </summary>
        [Column("BeforeSalePhone", System.Data.SqlDbType.NVarChar)]
        [Check("BeforeSalePhone", "售前电话", typeof(string))]
        public string BeforeSalePhone
        {
            set { Authentication(value, "BeforeSalePhone"); _beforesalephone = value; }
            get { SetPValue("BeforeSalePhone"); return _beforesalephone == null ? "" : _beforesalephone; }
        }

        private string _aftersalephone;
        /// <summary>
        /// 售后电话
        /// </summary>
        [Column("AfterSalePhone", System.Data.SqlDbType.NVarChar)]
        [Check("AfterSalePhone", "售后电话", typeof(string))]
        public string AfterSalePhone
        {
            set { Authentication(value, "AfterSalePhone"); _aftersalephone = value; }
            get { SetPValue("AfterSalePhone"); return _aftersalephone == null ? "" : _aftersalephone; }
        }
        #endregion Model
    }

}
