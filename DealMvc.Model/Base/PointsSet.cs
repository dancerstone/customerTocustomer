using System;
using System.Collections.Generic;
using System.Text;
using DealMvc.Orm;

namespace DealMvc.Model
{
    /// <summary>
    /// 积分设置 - 实体类PointsSet(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [Table("PointsSet", Info = "积分设置")]
    public class PointsSet : EntityBase<PointsSet>
    {
        public PointsSet() { }

        #region BLLService-业务逻辑层
        /// <summary>
        /// 积分设置-PointsSet-业务逻辑层
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
                List<PointsSet> m_PointsSetList = Orm.EntityCore<PointsSet>.GetModelList(int.MaxValue, "", null, "OrderNum Desc").List;
                foreach (PointsSet _PointsSet in m_PointsSetList)
                {
                    output.AppendFormat("<option value='{0}'>{1}</option>", _PointsSet.id, _PointsSet.id);
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
        /// 主键--标识ID
        /// </summary>
        [Column("id", System.Data.SqlDbType.Int, PrimaryKey = true, AutoIncrement = true)]
        [Check("id", "主键--标识ID", typeof(int))]
        public override int? id
        {
            set { Authentication(value, "id"); _id = value; }
            get { SetPValue("id"); return _id; }
        }

        private double? _ps_consumerpoints = 0;
        /// <summary>
        /// 完成消费送积分
        /// </summary>
        [Column("PS_ConsumerPoints", System.Data.SqlDbType.Float)]
        [Check("PS_ConsumerPoints", "完成消费送积分", typeof(double))]
        public double? PS_ConsumerPoints
        {
            set { Authentication(value, "PS_ConsumerPoints"); _ps_consumerpoints = value; }
            get { SetPValue("PS_ConsumerPoints"); return _ps_consumerpoints; }
        }

        private int? _ps_regpoints = 0;
        /// <summary>
        /// 完成注册送积分
        /// </summary>
        [Column("PS_RegPoints", System.Data.SqlDbType.Int)]
        [Check("PS_RegPoints", "完成注册送积分", typeof(int))]
        public int? PS_RegPoints
        {
            set { Authentication(value, "PS_RegPoints"); _ps_regpoints = value; }
            get { SetPValue("PS_RegPoints"); return _ps_regpoints; }
        }

        private int? _ps_phoneverifypoints = 0;
        /// <summary>
        /// 验证手机送积分
        /// </summary>
        [Column("PS_PhoneVerifyPoints", System.Data.SqlDbType.Int)]
        [Check("PS_PhoneVerifyPoints", "验证手机送积分", typeof(int))]
        public int? PS_PhoneVerifyPoints
        {
            set { Authentication(value, "PS_PhoneVerifyPoints"); _ps_phoneverifypoints = value; }
            get { SetPValue("PS_PhoneVerifyPoints"); return _ps_phoneverifypoints; }
        }

        private int? _ps_emailverifypoints = 0;
        /// <summary>
        /// 验证邮箱送积分
        /// </summary>
        [Column("PS_EmailVerifyPoints", System.Data.SqlDbType.Int)]
        [Check("PS_EmailVerifyPoints", "验证邮箱送积分", typeof(int))]
        public int? PS_EmailVerifyPoints
        {
            set { Authentication(value, "PS_EmailVerifyPoints"); _ps_emailverifypoints = value; }
            get { SetPValue("PS_EmailVerifyPoints"); return _ps_emailverifypoints; }
        }

        private DateTime? _ps_time = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("PS_Time", System.Data.SqlDbType.DateTime)]
        [Check("PS_Time", "更新时间", typeof(DateTime))]
        public DateTime? PS_Time
        {
            set { Authentication(value, "PS_Time"); _ps_time = value; }
            get { SetPValue("PS_Time"); return _ps_time; }
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
