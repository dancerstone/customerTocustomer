using System;
using System.Collections.Generic;
using System.Text;


namespace DealMvc.Core.Base
{
    /// <summary>
    /// 网站上传信息 - 业务层 SiteUpLoad
    /// </summary>
    public class BLL_SiteUpLoad
    {
        /// <summary>
        /// SiteUpLoad构造函数
        /// </summary>
        public BLL_SiteUpLoad()
        {

        }

        #region 原生态逻辑

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="Page">分页索引</param>
        /// <returns></returns>
        public DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteUpLoad> PagerList(int? Page)
        {
            DealMvc.Orm.PagerEx.Pager<DealMvc.Model.SiteUpLoad> _Pager = new DealMvc.Orm.PagerEx.Pager<Model.SiteUpLoad>(Page ?? 0, "");
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
        public NameValueCollectionEx NameValueCollectionEx(ref DealMvc.Model.SiteUpLoad m_sul)
        {
            m_sul = DealMvc.Model.SiteUpLoad.GetModel(m_sul.id ?? 0);
            if (m_sul.IsNull) throw new ExceptionEx.MyExceptionMessageBox("m_sul Is Null");

            NameValueCollectionEx _nvce = new NameValueCollectionEx();
            _nvce.Add("UploadFolder", m_sul.UploadFolder);        //UploadFolder[Type=string] - 上传文件夹
            _nvce.Add("UploadExtension", m_sul.UploadExtension);        //UploadExtension[Type=string] - 文件后缀名
            _nvce.Add("UploadSize", m_sul.UploadSize);        //UploadSize[Type=double?] - 文件大小
            _nvce.Add("DefaultImg", m_sul.DefaultImg);        //DefaultImg[Type=string] - 默认图片
            _nvce.Add("UpTime", m_sul.UpTime);        //UpTime[Type=DateTime?] - 更新时间

            return _nvce;
        }

        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <param name="isEdit"></param>
        /// <param name="p_SiteUpLoad"></param>
        public void AESiteUpLoad(DealMvc.ControllerBase _CB, bool isEdit, ref Model.SiteUpLoad p_sul)
        {
            Model.SiteUpLoad m_sul = null;

            if (isEdit)
                m_sul = DealMvc.Model.SiteUpLoad.GetModel(p_sul.id ?? 0);
            else
                m_sul = new Model.SiteUpLoad();

            if (!isEdit)
            {
                if (Orm.EntityCore<Model.SiteUpLoad>.Exists("=id", new object[] { p_sul.id }))
                {
                    throw new ExceptionEx.MyExceptionMessageBox("已经存在此记录");
                }
            }
            else
            {

            }

            //m_sul.UploadFolder = p_sul.UploadFolder;        //UploadFolder[Type=string] - 上传文件夹
            m_sul.UploadExtension = p_sul.UploadExtension;        //UploadExtension[Type=string] - 文件后缀名
            m_sul.UploadSize = p_sul.UploadSize ?? 0;        //UploadSize[Type=double?] - 文件大小
            m_sul.DefaultImg = p_sul.DefaultImg;        //DefaultImg[Type=string] - 默认图片
            m_sul.UpTime = p_sul.UpTime ?? DateTime.Now;        //UpTime[Type=DateTime?] - 更新时间


            p_sul = m_sul;
            if (isEdit)
            {
                m_sul.Update();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "修改网站上传信息成功");
                _CB.IsSaveForm = true;
            }
            else
            {
                m_sul.Add();
                ExceptionEx.MyExceptionLog.AlertMessage(_CB, "添加成功", true);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">1,2,3,4</param>
        /// <returns></returns>
        public bool DeleteSiteUpLoad(string ids, SqlTranEx.SqlTranExtensions _SqlTranExtensions)
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
                    DealMvc.Orm.EntityCore<Model.SiteUpLoad>.Delete(_ids[i].ToInt32(), _SqlTranExtensions);
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
