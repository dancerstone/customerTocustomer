using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace DealMvc.Timer
{
    /// <summary>
    /// 订单类
    /// </summary>
    public class AboutOrderThings : ITimer
    {

        string ITimer.Name
        {
            get;
            set;
        }

        DateTime? ITimer.StartTime
        {
            get;
            set;
        }

        DateTime? ITimer.EndTime
        {
            get;
            set;
        }

        int ITimer.GSeconds
        {
            get;
            set;
        }

        void ITimer.Execute()
        {
            //DealMvc.Core.Orders.BLL_Orders b_BLL_Orders = new Core.Orders.BLL_Orders();
            //b_BLL_Orders.AutomaticallyCancelOrder();//24小时后未付款时自动取消订单
            //b_BLL_Orders.SendTenDaysAfterRefundsReturns();//退货商品寄出后10天内未确认收货 自动确认
            //b_BLL_Orders.TimeOutConfirmReceiptOrder();//发货后10天内未确认收货系统将自动确认
        }
    }
}
