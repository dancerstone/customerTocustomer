using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 广告信息 - 业务层 AdvertisingInfo
    /// </summary>
    public class BLL_AdvertisingInfo
    {
        /// <summary>
        /// AdvertisingInfo构造函数
        /// </summary>
        public BLL_AdvertisingInfo()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.AdvertisingInfo> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.AdvertisingInfo> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.AdvertisingInfo>(Page ?? 0, "");
            _Pager.OrderColumn = new string[] { "id" };
            _Pager.OrderType = new DealMvc.Orm.PagerEx.SQLOrderType[] { Orm.PagerEx.SQLOrderType.DESC };

            _Pager.GetPageList();
            return _Pager;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.AdvertisingInfo> PagerList(int? Page, string Title)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.AdvertisingInfo> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.AdvertisingInfo>(Page ?? 0, t => t.AI_AdLocation.lb_Like(Title));
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
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.AdvertisingInfo m_ai)
        {
            m_ai = DealMvc.Model.AdvertisingInfo.GetModel(m_ai.id ?? 0);
            if (m_ai.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_ai Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("AI_AdLocation", m_ai.AI_AdLocation);        //AI_AdLocation[Type=string] - 广告位置
            _nvce.Add("AI_AdPicWidth", m_ai.AI_AdPicWidth);        //AI_AdPicWidth[Type= int?] - 像素宽
            _nvce.Add("AI_AdPicHeight", m_ai.AI_AdPicHeight);        //AI_AdPicHeight[Type= int?] - 像素高
            _nvce.Add("AI_AdTitle", m_ai.AI_AdTitle);        //AI_AdTitle[Type=string] - 广告语
            _nvce.Add("AI_AdPic", m_ai.AI_AdPic);        //AI_AdPic[Type=string] - 广告图片
            _nvce.Add("AI_LinkUrl", m_ai.AI_LinkUrl);        //AI_LinkUrl[Type=string] - 链接地址
            _nvce.Add("AI_Remarks", m_ai.AI_Remarks);        //AI_Remarks[Type=string] - 广告备注
            _nvce.Add("AI_IsTarget", m_ai.AI_IsTarget);        //AI_IsTarget[Type=bool] - 是否新窗口打开
            _nvce.Add("AI_Time", m_ai.AI_Time);        //AI_Time[Type=DateTime?] - 操作时间
            _nvce.Add("A", m_ai.A);        //A[Type=string] - 扩展A字段
            _nvce.Add("B", m_ai.B);        //B[Type=string] - 扩展B字段
            _nvce.Add("C", m_ai.C);        //C[Type=string] - 扩展C字段
            _nvce.Add("D", m_ai.D);        //D[Type=string] - 扩展D字段
            _nvce.Add("E", m_ai.E);        //E[Type=string] - 扩展E字段

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_AdvertisingInfo"></param>
        public void AEAdvertisingInfo(DealMvc.ControllerBase _CB, bool isEdit, ref Model.AdvertisingInfo p_ai)
        {
            Model.AdvertisingInfo m_ai = null;

            if (isEdit)
                m_ai = DealMvc.Model.AdvertisingInfo.GetModel(p_ai.id ?? 0);
            else
                m_ai = new Model.AdvertisingInfo();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.AdvertisingInfo>.Exists("=id", new object[] { p_ai.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
                m_ai.AI_AdLocation = p_ai.AI_AdLocation;        //AI_AdLocation[Type=string] - 广告位置
                m_ai.AI_AdPicWidth = p_ai.AI_AdPicWidth ?? 0;        //AI_AdPicWidth[Type= int?] - 像素宽
                m_ai.AI_AdPicHeight = p_ai.AI_AdPicHeight ?? 0;        //AI_AdPicHeight[Type= int?] - 像素高
            }
            else
            {

            }

            m_ai.AI_AdTitle = p_ai.AI_AdTitle;        //AI_AdTitle[Type=string] - 广告语
            m_ai.AI_AdPic = string.IsNullOrEmpty(p_ai.AI_AdPic) ? m_ai.AI_AdPic : p_ai.AI_AdPic;        //AI_AdPic[Type=string] - 广告图片
            m_ai.AI_LinkUrl = p_ai.AI_LinkUrl;        //AI_LinkUrl[Type=string] - 链接地址
            m_ai.AI_Remarks = p_ai.AI_Remarks;        //AI_Remarks[Type=string] - 广告备注
            m_ai.AI_IsTarget = p_ai.AI_IsTarget;        //AI_IsTarget[Type=bool] - 是否新窗口打开
            m_ai.AI_Time = p_ai.AI_Time ?? DateTime.Now;        //AI_Time[Type=DateTime?] - 操作时间
            m_ai.A = p_ai.A;        //A[Type=string] - 扩展A字段
            m_ai.B = p_ai.B;        //B[Type=string] - 扩展B字段
            m_ai.C = p_ai.C;        //C[Type=string] - 扩展C字段
            m_ai.D = p_ai.D;        //D[Type=string] - 扩展D字段
            m_ai.E = p_ai.E;        //E[Type=string] - 扩展E字段


            p_ai = m_ai;
            if (isEdit)
            {
                m_ai.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "编辑成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_ai.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteAdvertisingInfo(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.AdvertisingInfo>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
        public StringBuilder GetAd(string location)
        {
            StringBuilder ads = new StringBuilder();
            Model.AdvertisingInfo ad = Model.AdvertisingInfo.GetModel(t => t.AI_AdLocation == location);
            if (ad != null)
            {
                ads.Append("<a  href=\"" + ad.AI_LinkUrl + "\"" + (ad.AI_IsTarget ? " target=\"_blank\"" : "") + ">");
                //ads.Append("<a>");
                //ads.Append("<img src=\"" + ad.AI_AdPic + "\" height=\"" + ad.AI_AdPicHeight + "\" /></a>");
                ads.Append("<img src=\"" + ad.AI_AdPic + "\" /></a>");
            }

            return ads;
        }
        /// <summary>
        /// 手机版获取
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StringBuilder GetAdPhone(string location)
        {
            StringBuilder ads = new StringBuilder();
            Model.SiteInfo m_site = Model.SiteInfo.GetModel(t => t.id > 0);

            Model.AdvertisingInfo ad = Model.AdvertisingInfo.GetModel(t => t.AI_AdLocation == location);
            if (ad != null)
            {
                ads.Append("<a  href=\"" + ad.AI_LinkUrl + "\"" + (ad.AI_IsTarget ? " target=\"_blank\"" : "") + ">");
                //ads.Append("<a>");
                //ads.Append("<img src=\"" + ad.AI_AdPic + "\" height=\"" + ad.AI_AdPicHeight + "\" /></a>");
                ads.Append("<img src=\"" +m_site.WebAddress+ad.AI_AdPic + "\" /></a>");
            }

            return ads;
        }


        public StringBuilder GetAd2(string location)
        {
            StringBuilder ads = new StringBuilder();
            Model.AdvertisingInfo ad = Model.AdvertisingInfo.GetModel(t => t.AI_AdLocation == location);
            if (ad != null)
            {
                ads.Append("<a href=\"" + ad.AI_LinkUrl + "\"" + (ad.AI_IsTarget ? " target=\"_blank\"" : "") + " style=\"background: url(" + ad.AI_AdPic + ") center top no-repeat;\"></a>");
            }

            return ads;
        }
        #endregion

    }

}
