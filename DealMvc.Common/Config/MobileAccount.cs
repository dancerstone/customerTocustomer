using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DealMvc.Common.Config
{
    /// <summary>
    /// 短信接口帐号
    /// </summary>
    public class MobileAccount : IConfigInfo
    {
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
}