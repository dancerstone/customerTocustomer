using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DealMvc.Common.Config
{
    /// <summary>
    /// 支付宝信息配置类
    /// </summary>
    public class Alipay : IConfigInfo
    {
        public string partner { get; set; }
        public string key { get; set; }
        public string seller_email { get; set; }
    }
}