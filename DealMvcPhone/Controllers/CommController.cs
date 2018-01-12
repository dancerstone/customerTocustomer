using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealMvc;
using DealMvc.Common;

namespace DealMvcPhone.Controllers
{
    public class CommController : DealMvc.ControllerBase
    {
        #region 订单类 事物处理
        ///// <summary>
        ///// 订单业务逻辑
        ///// </summary>
        //DealMvc.Core.Orders.BLL_Orders _BLL_Orders = new DealMvc.Core.Orders.BLL_Orders();

        ///// <summary>
        ///// 取消订单
        ///// </summary>
        ///// <param name="Order_UID"></param>
        ///// <returns></returns>
        //public ContentResult CancelOrder(string Order_UID)
        //{
        //    return Content(_BLL_Orders.CancelOrder(Order_UID).ToString2().ToLower());
        //}

        ///// <summary>
        /////确认收货 
        ///// </summary>
        ///// <param name="Order_UID"></param>
        ///// <returns></returns>
        //public ContentResult ConfirmReceiptOrder(string Order_UID)
        //{
        //    return Content(_BLL_Orders.ConfirmReceiptOrder(Order_UID).ToString2().ToLower());
        //}

        ///// <summary>
        ///// 判断会员卡输入的抵扣金额
        ///// </summary>
        ///// <param name="m_id"></param>
        ///// <param name="input_money"></param>
        ///// <param name="order_money"></param>
        ///// <returns></returns>
        //public ContentResult JudgeDeductibleMoney(int? m_id, double? input_money, float? order_money)
        //{
        //    string msg = string.Empty;
        //    DealMvc.Model.MemberGetUseCard m_Card_model = DealMvc.Model.MemberGetUseCard.GetModel(m_id ?? 0);
        //    if (m_Card_model.IsNull)
        //    {
        //        msg = "0";//会员卡编号不存在，请重新核对后再试
        //    }
        //    else
        //    {
        //        if (input_money <= 0)
        //        {
        //            msg = "1";//输入的抵扣金额必须大于等于1元
        //        }
        //        else
        //        {
        //            if (input_money > m_Card_model.MC_CardBanlance)
        //            {
        //                msg = "2";//输入的抵扣金额已超出卡上可用余额
        //            }
        //            else
        //            {
        //                if (input_money > order_money)
        //                {
        //                    msg = "3";//输入的抵扣金额已超出本次购物的交易金额
        //                }
        //                else
        //                {
        //                    msg = "4";//ok
        //                }
        //            }
        //        }
        //    }
        //    return Content(msg);
        //}
        ///// <summary>
        ///// 确认收货并退款
        ///// </summary>
        ///// <param name="R_LogID"></param>
        ///// <returns></returns>
        //public ContentResult ConfirmReceiptAndRefund(int? R_LogID)
        //{
        //    return Content(_BLL_Orders.ConfirmReceiptAndRefund(R_LogID).ToString2().ToLower());
        //}

        ///// <summary>
        ///// 网站介入退货处理
        ///// </summary>
        ///// <param name="R_LogID"></param>
        ///// <returns></returns>
        //public ContentResult ApplicationSiteIntervene(int? R_LogID)
        //{
        //    return Content(_BLL_Orders.ApplicationSiteIntervene(R_LogID).ToString2().ToLower());
        //}
        ///// <summary>
        ///// 取消此次退货处理
        ///// </summary>
        ///// <param name="R_LogID"></param>
        ///// <returns></returns>
        //public ContentResult CancerRetutnIntervene(int? R_LogID)
        //{
        //    return Content(_BLL_Orders.CancerRetutnIntervene(R_LogID).ToString2().ToLower());
        //}

        ///// <summary>
        ///// 确认 返修退换货
        ///// </summary>
        ///// <param name="cid"></param>
        ///// <returns></returns>
        //public ContentResult SureReturnOrderModel(int? cid)
        //{
        //    return Content(_BLL_Orders.SureReturnOrderModel(cid).ToString2().ToLower());
        //}

       
        ///// <summary>
        ///// 取消订单
        ///// </summary>
        ///// <param name="Order_UID"></param>
        ///// <returns></returns>
        //public ContentResult DeleteThisOrder(string Order_UID)
        //{
        //    return Content(_BLL_Orders.deleteTOrder(Order_UID).ToString2().ToLower());
        //}


        #endregion

    }
}
