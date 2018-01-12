using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DealMvc.Pay
{
    public interface IPayMent
    {
        /// <summary>
        /// 开始支付
        /// </summary>
        ActionResult Checkout(string Order_UID, string Retaion_UID);
    }
}
