using System;
using System.Collections.Generic;

using System.Text;


namespace DealMvc.Orm
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckAttribute : System.Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Type为数字(int)时,表示最小值(可等于),Type为字符串(string)时,表示最小长度(可等于)
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Type为数字(int)时,表示最大值(可等于),Type为字符串(string)时,表示最大长度(可等于)
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool NotEmpty { get; set; }

        /// <summary>
        /// 是否限制为邮箱格式
        /// </summary>
        public bool IsEmail { get; set; }

        /// <summary>
        /// 是否限制为电话号码格式
        /// </summary>
        public bool IsPhone { get; set; }

        /// <summary>
        /// 是否限制为货币格式
        /// </summary>
        public bool IsMoney { get; set; }

        /// <summary>
        /// 是否允许中文
        /// </summary>
        public bool NotSupportChinese { get; set; }

        /// <summary>
        /// 是否限制为数字
        /// </summary>
        public bool IsNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="des"></param>
        /// <param name="type"></param>
        public CheckAttribute(string name, string des, Type type)
        {
            this.Name = name;
            this.Des = des;
            this.Type = type;
            this.Min = int.MinValue;
            this.Max = int.MaxValue;
            this.NotEmpty = false;
            this.IsEmail = false;
            this.IsPhone = false;
            this.IsMoney = false;
            this.NotSupportChinese = false;
            this.IsNumber = false;
        }
    }
}
