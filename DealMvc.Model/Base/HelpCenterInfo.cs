using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 帮助中心信息 - 实体类HelpCenterInfo(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("HelpCenterInfo", Info = "帮助中心信息")]
    public class HelpCenterInfo : EntityBase<HelpCenterInfo>
    {
        public HelpCenterInfo() { }

        #region BLLService-业务逻辑层
        /// <summary>
        /// 帮助中心信息-HelpCenterInfo-业务逻辑层
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
                List<HelpCenterInfo> m_HelpCenterInfoList = Orm.EntityCore<HelpCenterInfo>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (HelpCenterInfo _HelpCenterInfo in m_HelpCenterInfoList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _HelpCenterInfo.id, _HelpCenterInfo.id);
                }
            }
            catch { }
            return output.ToString();
        }
        #endregion



        #region GetConnectedModel
        public HelpCenterCate GetHelpCate1()
        {
            HelpCenterCate hc = HelpCenterCate.GetModel(HelpCateID1 ?? 0);
            return hc;
        }
        public HelpCenterCate GetHelpCate2()
        {
            HelpCenterCate hc2 = HelpCenterCate.GetModel(HelpCateID2 ?? 0);
            return hc2;
        }
        #endregion



        #region Model

        private int? _id;
        /// <summary>
        /// 主键--标识ID
        /// </summary>
        [Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
        [Check("id", "主键--标识ID", typeof(int))]
        public override int? id
        {
            set { Authentication(value, "id"); _id = value; }
            get { SetPValue("id"); return _id; }
        }

        private int? _helpcateid1 = 0;
        /// <summary>
        /// 帮助中心一级分类
        /// </summary>
        [Column("HelpCateID1", System.Data.SqlDbType.Int)]
        [Check("HelpCateID1", "帮助中心一级分类", typeof(int))]
        public int? HelpCateID1
        {
            set { Authentication(value, "HelpCateID1"); _helpcateid1 = value; }
            get { SetPValue("HelpCateID1"); return _helpcateid1; }
        }

        private int? _helpcateid2 = 0;
        /// <summary>
        /// 帮助中心二级分类
        /// </summary>
        [Column("HelpCateID2", System.Data.SqlDbType.Int)]
        [Check("HelpCateID2", "帮助中心二级分类", typeof(int))]
        public int? HelpCateID2
        {
            set { Authentication(value, "HelpCateID2"); _helpcateid2 = value; }
            get { SetPValue("HelpCateID2"); return _helpcateid2; }
        }

        private string _hci_littletitle;
        /// <summary>
        /// 小标题
        /// </summary>
        [Column("HCI_LittleTitle", System.Data.SqlDbType.NVarChar)]
        [Check("HCI_LittleTitle", "小标题", typeof(string))]
        public string HCI_LittleTitle
        {
            set { Authentication(value, "HCI_LittleTitle"); _hci_littletitle = value; }
            get { SetPValue("HCI_LittleTitle"); return _hci_littletitle == null ? "" : _hci_littletitle; }
        }

        private string _hci_title;
        /// <summary>
        /// 详细标题
        /// </summary>
        [Column("HCI_Title", System.Data.SqlDbType.NVarChar)]
        [Check("HCI_Title", "详细标题", typeof(string))]
        public string HCI_Title
        {
            set { Authentication(value, "HCI_Title"); _hci_title = value; }
            get { SetPValue("HCI_Title"); return _hci_title == null ? "" : _hci_title; }
        }

        private string _hci_content;
        /// <summary>
        /// 信息内容
        /// </summary>
        [Column("HCI_Content", System.Data.SqlDbType.NText)]
        [Check("HCI_Content", "信息内容", typeof(string))]
        public string HCI_Content
        {
            set { Authentication(value, "HCI_Content"); _hci_content = value; }
            get { SetPValue("HCI_Content"); return _hci_content == null ? "" : _hci_content; }
        }

        private bool _hci_isdefault = false;
        /// <summary>
        /// 是否默认展示
        /// </summary>
        [Column("HCI_IsDefault", System.Data.SqlDbType.Bit)]
        [Check("HCI_IsDefault", "是否默认展示", typeof(bool))]
        public bool HCI_IsDefault
        {
            set { Authentication(value, "HCI_IsDefault"); _hci_isdefault = value; }
            get { SetPValue("HCI_IsDefault"); return _hci_isdefault; }
        }

        private int? _hci_sort = 0;
        /// <summary>
        /// 排序
        /// </summary>
        [Column("HCI_Sort", System.Data.SqlDbType.Int)]
        [Check("HCI_Sort", "排序", typeof(int))]
        public int? HCI_Sort
        {
            set { Authentication(value, "HCI_Sort"); _hci_sort = value; }
            get { SetPValue("HCI_Sort"); return _hci_sort; }
        }

        private DateTime? _hci_time = DateTime.Now;
        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("HCI_Time", System.Data.SqlDbType.DateTime)]
        [Check("HCI_Time", "操作时间", typeof(DateTime))]
        public DateTime? HCI_Time
        {
            set { Authentication(value, "HCI_Time"); _hci_time = value; }
            get { SetPValue("HCI_Time"); return _hci_time; }
        }

        private string _a;
        /// <summary>
        /// 扩展A字段
        /// </summary>
        [Column("A", System.Data.SqlDbType.NVarChar)]
        [Check("A", "扩展A字段", typeof(string))]
        public string A
        {
            set { Authentication(value, "A"); _a = value; }
            get { SetPValue("A"); return _a == null ? "" : _a; }
        }

        private string _b;
        /// <summary>
        /// 扩展B字段
        /// </summary>
        [Column("B", System.Data.SqlDbType.NVarChar)]
        [Check("B", "扩展B字段", typeof(string))]
        public string B
        {
            set { Authentication(value, "B"); _b = value; }
            get { SetPValue("B"); return _b == null ? "" : _b; }
        }

        private string _c;
        /// <summary>
        /// 扩展C字段
        /// </summary>
        [Column("C", System.Data.SqlDbType.NVarChar)]
        [Check("C", "扩展C字段", typeof(string))]
        public string C
        {
            set { Authentication(value, "C"); _c = value; }
            get { SetPValue("C"); return _c == null ? "" : _c; }
        }

        private string _d;
        /// <summary>
        /// 扩展D字段
        /// </summary>
        [Column("D", System.Data.SqlDbType.NVarChar)]
        [Check("D", "扩展D字段", typeof(string))]
        public string D
        {
            set { Authentication(value, "D"); _d = value; }
            get { SetPValue("D"); return _d == null ? "" : _d; }
        }

        private string _e;
        /// <summary>
        /// 扩展E字段
        /// </summary>
        [Column("E", System.Data.SqlDbType.NVarChar)]
        [Check("E", "扩展E字段", typeof(string))]
        public string E
        {
            set { Authentication(value, "E"); _e = value; }
            get { SetPValue("E"); return _e == null ? "" : _e; }
        }
        #endregion Model
    }

}
