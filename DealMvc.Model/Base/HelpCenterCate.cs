using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 帮助中心分类 - 实体类HelpCenterCate(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("HelpCenterCate", Info = "帮助中心分类")]
    public class HelpCenterCate : EntityBase<HelpCenterCate>
    {
        public HelpCenterCate() { }

        #region BLLService-业务逻辑层
        /// <summary>
        /// 帮助中心分类-HelpCenterCate-业务逻辑层
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
                List<HelpCenterCate> m_HelpCenterCateList = Orm.EntityCore<HelpCenterCate>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (HelpCenterCate _HelpCenterCate in m_HelpCenterCateList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _HelpCenterCate.id, _HelpCenterCate.id);
                }
            }
            catch { }
            return output.ToString();
        }
        #endregion


        #region GetConnectedModel
        private HelpCenterCate _ActivityCate;
        /// <summary>
        /// [上级分类]Get HelpCenterCate Model By HCC_ParentID
        /// </summary>
        public HelpCenterCate Parent_HelpCenterCate()
        {
            return GM<HelpCenterCate>(HCC_ParentID ?? 0, ref _ActivityCate);
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

        private int? _hcc_parentid = 0;
        /// <summary>
        /// 父级ID
        /// </summary>
        [Column("HCC_ParentID", System.Data.SqlDbType.Int)]
        [Check("HCC_ParentID", "父级ID", typeof(int))]
        public int? HCC_ParentID
        {
            set { Authentication(value, "HCC_ParentID"); _hcc_parentid = value; }
            get { SetPValue("HCC_ParentID"); return _hcc_parentid; }
        }

        private string _hcc_name;
        /// <summary>
        /// 分类名称
        /// </summary>
        [Column("HCC_Name", System.Data.SqlDbType.NVarChar)]
        [Check("HCC_Name", "分类名称", typeof(string))]
        public string HCC_Name
        {
            set { Authentication(value, "HCC_Name"); _hcc_name = value; }
            get { SetPValue("HCC_Name"); return _hcc_name == null ? "" : _hcc_name; }
        }

        private int? _hcc_level = 0;
        /// <summary>
        /// 级别
        /// </summary>
        [Column("HCC_Level", System.Data.SqlDbType.Int)]
        [Check("HCC_Level", "级别", typeof(int))]
        public int? HCC_Level
        {
            set { Authentication(value, "HCC_Level"); _hcc_level = value; }
            get { SetPValue("HCC_Level"); return _hcc_level; }
        }

        private int? _hcc_sort = 0;
        /// <summary>
        /// 排序
        /// </summary>
        [Column("HCC_Sort", System.Data.SqlDbType.Int)]
        [Check("HCC_Sort", "排序", typeof(int))]
        public int? HCC_Sort
        {
            set { Authentication(value, "HCC_Sort"); _hcc_sort = value; }
            get { SetPValue("HCC_Sort"); return _hcc_sort; }
        }

        private DateTime? _hcc_time = DateTime.Now;
        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("HCC_Time", System.Data.SqlDbType.DateTime)]
        [Check("HCC_Time", "操作时间", typeof(DateTime))]
        public DateTime? HCC_Time
        {
            set { Authentication(value, "HCC_Time"); _hcc_time = value; }
            get { SetPValue("HCC_Time"); return _hcc_time; }
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
