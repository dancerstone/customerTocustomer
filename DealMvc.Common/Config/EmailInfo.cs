using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DealMvc.Common.Config
{
    /// <summary>
    /// 邮箱信息配置类
    /// </summary>
    public class EmailInfo : IConfigInfo
    {

        public string smtp { get; set; }

        public string emailname { get; set; }

        public string email { get; set; }

        public string emailpwd { get; set; }

        public string port { get; set; }

    }
}