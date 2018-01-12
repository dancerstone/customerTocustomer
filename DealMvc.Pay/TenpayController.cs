using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections;
using DealMvc.Model;
using DealMvc.Pay.Tenpay;
using JumbotCms.API.Tenpay;
using DealMvc.Common.Config;
using DealMvc.SqlTranEx;

namespace DealMvc.Pay
{
    /// <summary>
    /// 财付通
    /// </summary>
    public class TenpayController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordernum"></param>
        /// <returns></returns>
        public ActionResult Checkout(string obj_uid, string cate, double? money, string brankcode)
        {
            UtilPay NowUtiPay = new UtilPay(System.Web.HttpContext.Current);
            NowUtiPay.Yinhang = brankcode;

            return Redirect(NowUtiPay.GetTenpayUrl(cate,
                obj_uid,
                money.ToString2(), "/Tenpay/Tenpay_Return"
                ));
        }
        /// <summary>
        /// 回调方法
        /// </summary>
        /// <returns></returns>
        public ActionResult Tenpay_Return()
        {
            bool isSuccess = false;
            string AcitonCate = string.Empty;
            try
            {
                /*
                 attach=&bargainor_id=1208389301&cmdno=1&date=20120111&fee_type=1&pay_info=OK&pay_result=0&pay_time=1326291710&sign=0925C26EA436BAC80010E7F3F6C9FAE3&sp_billno=2220091839&total_fee=1&transaction_id=1208389301201201112220091839&ver=1
                 */

                PayResponseHandler resHandler = new PayResponseHandler(System.Web.HttpContext.Current);
                SitePayAPI m_entity = SitePayAPI.GetModel(t => t.ApiType == "财付通");
                resHandler.setKey(m_entity.AppKey);

                //判断签名
                if (resHandler.isTenpaySign())
                {
                    string orderid = resHandler.getParameter("attach");
                    string cate = resHandler.getParameter("desc");
                    string total_fee = resHandler.getParameter("total_fee");
                    SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();
                    #region 付款成功之后的操作
                    switch (cate)
                    {
                        
                    }
                    #endregion

                    isSuccess = _STE.ExecuteSqlTran();
                }

            }
            catch (Exception ex)
            {
                return Content("error-4" + ex.Message.ToString());
            }
            if (isSuccess)
            {
                if (AcitonCate == "充值")
                    return RedirectToAction("MemberAccountInfo", "MSComm");
                else if (AcitonCate == "活动付款")
                    return RedirectToAction("SignUpSuccess", "Home");
                else if (AcitonCate == "会员卡")
                    return RedirectToAction("MemberCardLog", "MSComm");
                return RedirectToAction("MemberAccountInfo", "MSComm");
            }
            else
            {
                if (AcitonCate == "充值")
                    return RedirectToAction("MemberAccountInfo", "MSComm");
                else if (AcitonCate == "活动付款")
                    return RedirectToAction("SignUpFailed", "Home", new { msg = "支付失败，请重新核对支付信息后重新支付" });
                else if (AcitonCate == "会员卡")
                    return RedirectToAction("MemberCardLog", "MSComm");
                return RedirectToAction("SignUpFailed", "Home", new { msg = "支付失败，请重新核对支付信息后重新支付" });
            }
        }
    }


    /// <summary>
    /// 财付通直接支付订单接口
    /// </summary>
    public class TenpayForOrderController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordernum"></param>
        /// <returns></returns>
        public ActionResult Checkout(string Order_UID, string Retaion_UID, string brankcode)
        {
            //应付金额
            double? PayMoney = 0;
            //会员编号
            string Member_UID = string.Empty;
            //订单编号集合
            string OrderUIDLIST = string.Empty;
            //if (!string.IsNullOrEmpty(Retaion_UID))
            //{
            //    List<Orders> order_list = Orders.GetModelList(t => t.O_Relation_UID == Retaion_UID).List;
            //    foreach (var item in order_list)
            //    {
            //        PayMoney += item.O_PayMoney;
            //        Member_UID = item.Member_UID;
            //        OrderUIDLIST += item.O_UID + ",";
            //    }
            //}
            //else
            //{
            //    Orders m_order = Orders.GetModel(t => t.O_UID == Order_UID);
            //    PayMoney = m_order.O_PayMoney;
            //    Member_UID = m_order.Member_UID;
            //    OrderUIDLIST += m_order.O_UID + ",";
            //}

            UtilPay NowUtiPay = new UtilPay(System.Web.HttpContext.Current);
            NowUtiPay.Yinhang = brankcode;

            return Redirect(
                NowUtiPay.GetTenpayUrl(
                "会员[" + Member_UID + "] 支付订单",
                OrderUIDLIST,
                PayMoney.ToString2(), "/TenpayForOrder/Tenpay_Return"
                ));
        }

        /// <summary>
        /// 回调地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Tenpay_Return()
        {
            bool isSuccess = false;
            try
            {
                /*
                attach=&bargainor_id=1208389301&cmdno=1&date=20120111&fee_type=1&pay_info=OK&pay_result=0&pay_time=1326291710&sign=0925C26EA436BAC80010E7F3F6C9FAE3&sp_billno=2220091839&total_fee=1&transaction_id=1208389301201201112220091839&ver=1
                */

                PayResponseHandler resHandler = new PayResponseHandler(System.Web.HttpContext.Current);
                SitePayAPI m_entity = SitePayAPI.GetModel(t => t.ApiType == "财付通");
                resHandler.setKey(m_entity.AppKey);

                //判断签名
                if (resHandler.isTenpaySign())
                {
                    //交易单号---时间：2012-03-29 21:11 已更新
                    //string transaction_id = resHandler.getParameter("transaction_id");
                    // reqHandler.setParameter("attach", orderid);	//订单ID
                    string orderid = resHandler.getParameter("attach");

                    //金额金额,以分为单位
                    string total_fee = resHandler.getParameter("total_fee");

                    //支付结果
                    string pay_result = resHandler.getParameter("pay_result");

                    if (string.IsNullOrEmpty(orderid))
                        return Content("error-7");
                    if (!"0".Equals(pay_result))
                        return Content("error-8");

                    lock (this)
                    {
                        string[] OrderListUID = orderid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();
                        //for (int i = 0; i < OrderListUID.Length; i++)
                        //{
                        //    Orders m_now_orders = Orders.GetModel(t => t.O_UID == OrderListUID[i].ToString2());
                        //    if (!m_now_orders.IsNull && m_now_orders.O_IsPay == false)
                        //    {

                        //        #region 更改订单信息

                        //        m_now_orders.O_IsPay = true;
                        //        m_now_orders.O_Status = CommonEnumHelper.OrderStatus.已付款.ToString2();
                        //        m_now_orders.O_PayTime = DateTime.Now;
                        //        m_now_orders.Update(_STE);

                        //        #endregion

                        //        #region 更改商家帐户信息

                        //        MemberAccount m_store_account = MemberAccount.GetModel(t => t.M_UID == m_now_orders.Store_UID);
                        //        if (m_store_account.IsNull)
                        //            return Content("Error");
                        //        else
                        //        {
                        //            m_store_account.M_LockBalance += m_now_orders.O_StoreGetMoney;
                        //            m_store_account.Update(_STE);
                        //        }

                        //        #endregion

                        //        #region 更改会员经验值
                        //        MemberAccount m_member_account = MemberAccount.GetModel(t => t.M_UID == m_now_orders.Member_UID);
                        //        #endregion

                        //        #region 增加会员账户消费记录信息
                        //        MemberAccountLog m_mal = new MemberAccountLog(); //表名：MemberAccountLog 备注：会员账户消费记录
                        //        m_mal.M_UID = m_now_orders.Member_UID;        //M_UID[Type=string] - 会员唯一编号
                        //        m_mal.MA_UID = Common.Globals.CreateNewUniqueID();
                        //        m_mal.M_Cate = CommonEnumHelper.AccountCate.现金.ToString2();        //M_Cate[Type=string] - 分类(积分/现金)
                        //        m_mal.M_PayCate = CommonEnumHelper.AccountLogCate.支出.ToString2();        //M_PayCate[Type=string] - 收入/支出类型
                        //        m_mal.M_ObjectMoney = m_now_orders.O_PayMoney;        //M_ObjectMoney[Type=double?] - 操作金额
                        //        m_mal.M_BalanceMoney = m_member_account.M_AvailableBalance;        //M_BalanceMoney[Type=double?] - 账户余额
                        //        m_mal.M_Remark = "支付订单（订单编号：" + m_now_orders.O_UID + "）";        //M_Remark[Type=string] - 备注
                        //        m_mal.M_Status = "";        //M_Status[Type=string] - 当前状态
                        //        m_mal.M_Time = DateTime.Now;        //M_Time[Type=DateTime?] - 时间
                        //        m_mal.Add(_STE);
                        //        #endregion

                        //        #region 增加商家账户消费记录信息
                        //        MemberAccountLog m_mal_s = new MemberAccountLog(); //表名：MemberAccountLog 备注：会员账户消费记录
                        //        m_mal_s.M_UID = m_now_orders.Store_UID;        //M_UID[Type=string] - 会员唯一编号
                        //        m_mal_s.MA_UID = Common.Globals.CreateNewUniqueID();
                        //        m_mal_s.M_Cate = CommonEnumHelper.AccountCate.现金.ToString2();        //M_Cate[Type=string] - 分类(积分/现金)
                        //        m_mal_s.M_PayCate = CommonEnumHelper.AccountLogCate.收入.ToString2();        //M_PayCate[Type=string] - 收入/支出类型
                        //        m_mal_s.M_ObjectMoney = m_now_orders.O_StoreGetMoney;        //M_ObjectMoney[Type=double?] - 操作金额
                        //        m_mal_s.M_BalanceMoney = m_store_account.M_AvailableBalance;        //M_BalanceMoney[Type=double?] - 账户余额
                        //        m_mal_s.M_Remark = "订单收款（订单编号：" + m_now_orders.O_UID + "），所得金额现为锁定状态，需等买家确认收货/消费后自动解锁";        //M_Remark[Type=string] - 备注
                        //        m_mal_s.M_Status = "";        //M_Status[Type=string] - 当前状态
                        //        m_mal_s.M_Time = DateTime.Now;        //M_Time[Type=DateTime?] - 时间
                        //        m_mal_s.Add(_STE);
                        //        #endregion

                        //        isSuccess = _STE.ExecuteSqlTran();

                        //    }
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            if (isSuccess)
            {
                return RedirectToAction("OrderPaySuccess", "Order");
            }
            else
            {
                return RedirectToAction("OrderPayFailure", "Order", new { msg = "订单支付失败，请重新核对订单信息及支付信息后重新支付" });
            }
        }
    }
}
