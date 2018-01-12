using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 网站配置信息 - 业务层 SiteInfo
    /// </summary>
    public class BLL_SiteInfo
    {
        /// <summary>
        /// SiteInfo构造函数
        /// </summary>
        public BLL_SiteInfo()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteInfo> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteInfo> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.SiteInfo>(Page ?? 0, "");
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
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.SiteInfo m_si)
        {
            m_si = DealMvc.Model.SiteInfo.GetModel(m_si.id ?? 0);
            if (m_si.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_si Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("WebName", m_si.WebName);        //WebName[Type=string] - 网站名称
            _nvce.Add("WebAddress", m_si.WebAddress);        //WebAddress[Type=string] - 网站域名
            _nvce.Add("WebTitle", m_si.WebTitle);        //WebTitle[Type=string] - 网站标题
            _nvce.Add("WebKeyword", m_si.WebKeyword);        //WebKeyword[Type=string] - 网站关键词
            _nvce.Add("WebDelimiter", m_si.WebDelimiter);        //WebDelimiter[Type=string] - 网站标题分隔符
            _nvce.Add("WebCopyright", m_si.WebCopyright);        //WebCopyright[Type=string] - 版权
            _nvce.Add("WebStatus", m_si.WebStatus);        //WebStatus[Type=bool] - 网站状态(开启网站、关闭网站)
            _nvce.Add("WebCloseRemark", m_si.WebCloseRemark);        //WebCloseRemark[Type=string] - 关闭网站原因
            _nvce.Add("WebCompetence", m_si.WebCompetence);        //WebCompetence[Type=bool] - 网站后台权限
            _nvce.Add("IsOpenDataCache", m_si.IsOpenDataCache);        //IsOpenDataCache[Type=bool] - 数据缓存  是否启用
            _nvce.Add("DataCacheTime", m_si.DataCacheTime);        //DataCacheTime[Type= int?] - 缓存时间(秒)
            _nvce.Add("ProportionMention", m_si.ProportionMention);        //ProportionMention[Type=double?] - 平台提拥比例
            _nvce.Add("WebBottomInfo", m_si.WebBottomInfo);        //WebBottomInfo[Type=string] - 网站底部信息
            _nvce.Add("WebFriendLinks", m_si.WebFriendLinks);        //WebFriendLinks[Type=string] - 友情链接
            _nvce.Add("WebBottomContact", m_si.WebBottomContact);        //WebBottomContact[Type=string] - 底部联系方式
            _nvce.Add("WebBottomHours", m_si.WebBottomHours);        //WebBottomHours[Type=string] - 营业时间
            _nvce.Add("WebRegService", m_si.WebRegService);        //WebRegService[Type=string] - 注册条款
            _nvce.Add("WebWeiXinImage", m_si.WebWeiXinImage);        //WebWeiXinImage[Type=string] - 微信二维码图片
            _nvce.Add("WebWeiBoImage", m_si.WebWeiBoImage);        //WebWeiBoImage[Type=string] - 微博二维码图片
            _nvce.Add("WebContactPhone", m_si.WebContactPhone);        //WebContactPhone[Type=string] - 联系电话
            _nvce.Add("WebContactEmail", m_si.WebContactEmail);        //WebContactEmail[Type=string] - 联系邮箱
            _nvce.Add("KeFuQQ", m_si.KeFuQQ);        //KeFuQQ[Type=string] - 客服QQ
            _nvce.Add("BeforeSalePhone", m_si.BeforeSalePhone);        //BeforeSalePhone[Type=string] - 售前电话
            _nvce.Add("AfterSalePhone", m_si.AfterSalePhone);        //AfterSalePhone[Type=string] - 售后电话
            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_SiteInfo"></param>
        public void AESiteInfo(DealMvc.ControllerBase _CB, bool isEdit, ref Model.SiteInfo p_si)
        {
            Model.SiteInfo m_si = null;

            if (isEdit)
                m_si = DealMvc.Model.SiteInfo.GetModel(p_si.id ?? 0);
            else
                m_si = new Model.SiteInfo();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.SiteInfo>.Exists("=id", new object[] { p_si.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }
            m_si.WebName = p_si.WebName;        //WebName[Type=string] - 网站名称
            m_si.WebAddress = p_si.WebAddress;        //WebAddress[Type=string] - 网站域名
            m_si.WebTitle = p_si.WebTitle;        //WebTitle[Type=string] - 网站标题
            m_si.WebKeyword = p_si.WebKeyword;        //WebKeyword[Type=string] - 网站关键词
            m_si.WebDelimiter = p_si.WebDelimiter;        //WebDelimiter[Type=string] - 网站标题分隔符
            m_si.WebCopyright = p_si.WebCopyright;        //WebCopyright[Type=string] - 版权
            m_si.WebStatus = p_si.WebStatus;        //WebStatus[Type=bool] - 网站状态(开启网站、关闭网站)
            m_si.WebCloseRemark = p_si.WebCloseRemark;        //WebCloseRemark[Type=string] - 关闭网站原因
            m_si.WebCompetence = p_si.WebCompetence;        //WebCompetence[Type=bool] - 网站后台权限
            m_si.IsOpenDataCache = p_si.IsOpenDataCache;        //IsOpenDataCache[Type=bool] - 数据缓存  是否启用
            m_si.DataCacheTime = p_si.DataCacheTime ?? 0;        //DataCacheTime[Type= int?] - 缓存时间(秒)
            m_si.ProportionMention = p_si.ProportionMention ?? 0;        //ProportionMention[Type=double?] - 平台提拥比例
            m_si.WebBottomInfo = p_si.WebBottomInfo;        //WebBottomInfo[Type=string] - 网站底部信息

            //WebFriendLinks[Type=string] - 友情链接
            Common.Globals.UpFileResult _UpFileResult = Common.Globals.Upload("WebFriendLinks");
            if (_UpFileResult.returnerror.Count == 0)
            {
                if (_UpFileResult.returnfilename.Count > 0)
                {
                    m_si.WebFriendLinks = _UpFileResult.returnfilename[0].ToString();
                }
            }
            else
            {
                throw new ExceptionEx.MyExceptionMessageBox(string.Join("<br/>", (string[])_UpFileResult.returnerror.ToArray(typeof(string))));
            }


            m_si.WebBottomContact = p_si.WebBottomContact;        //WebBottomContact[Type=string] - 底部联系方式
            m_si.WebBottomHours = p_si.WebBottomHours;        //WebBottomHours[Type=string] - 营业时间
            m_si.WebRegService = p_si.WebRegService;        //WebRegService[Type=string] - 注册条款
            //m_si.WebWeiXinImage = p_si.WebWeiXinImage;        //WebWeiXinImage[Type=string] - 微信二维码图片
            Common.Globals.UpFileResult _UpFileResultWebWeiXinImage = Common.Globals.Upload("WebWeiXinImage");
            if (_UpFileResultWebWeiXinImage.returnerror.Count == 0)
            {
                if (_UpFileResultWebWeiXinImage.returnfilename.Count > 0)
                {
                    m_si.WebWeiXinImage = _UpFileResultWebWeiXinImage.returnfilename[0].ToString();
                }
            }
            else
            {
                throw new ExceptionEx.MyExceptionMessageBox(string.Join("<br/>", (string[])_UpFileResultWebWeiXinImage.returnerror.ToArray(typeof(string))));
            }

            //m_si.WebWeiBoImage = p_si.WebWeiBoImage;        //WebWeiBoImage[Type=string] - 微博二维码图片
            Common.Globals.UpFileResult _UpFileResultWebWeiBoImage = Common.Globals.Upload("WebWeiBoImage");
            if (_UpFileResultWebWeiBoImage.returnerror.Count == 0)
            {
                if (_UpFileResultWebWeiBoImage.returnfilename.Count > 0)
                {
                    m_si.WebWeiBoImage = _UpFileResultWebWeiBoImage.returnfilename[0].ToString();
                }
            }
            else
            {
                throw new ExceptionEx.MyExceptionMessageBox(string.Join("<br/>", (string[])_UpFileResultWebWeiBoImage.returnerror.ToArray(typeof(string))));
            }
            m_si.WebContactPhone = p_si.WebContactPhone;        //WebContactPhone[Type=string] - 联系电话
            m_si.WebContactEmail = p_si.WebContactEmail;        //WebContactEmail[Type=string] - 联系邮箱
            m_si.KeFuQQ = p_si.KeFuQQ;        //KeFuQQ[Type=string] - 客服QQ
            m_si.BeforeSalePhone = p_si.BeforeSalePhone;        //BeforeSalePhone[Type=string] - 售前电话
            m_si.AfterSalePhone = p_si.AfterSalePhone;        //AfterSalePhone[Type=string] - 售后电话

            p_si = m_si;
            if (isEdit)
            {
                m_si.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "修改网站基本信息成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_si.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteSiteInfo(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.SiteInfo>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
