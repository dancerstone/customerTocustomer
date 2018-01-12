using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections;
using DealMvc.Model;
using Com.Alipay;

namespace DealMvc.Pay
{

    /// <summary>
    /// 支付宝  网银支付
    /// </summary>
    public class AlipaydirectController : Controller
    {
        /// <summary>
        /// 跳转支付函数
        /// </summary>
        /// <param name="Order_UID"></param>
        /// <param name="Retaion_UID"></param>
        /// <returns></returns>
        public ActionResult Checkout_new(string Order_UID, string Retaion_UID, string bank)
        {
            SitePayAPI m_entity = SitePayAPI.GetModel(t => t.ApiType == "支付宝");
            Model.SiteInfo siteinfo = Model.SiteInfo.GetModel(t => t.id != 0);
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的///////////////////////////
            AlipayConfig con = new AlipayConfig();

            //##########################################
            ///////////////////////以下参数是需要通过下单时的订单数据传入进来获得////////////////////////////////

            //应付金额
            double? PayMoney = 0;
            //会员编号
            string Member_UID = string.Empty;
            //订单编号集合
            string OrderUIDLIST = string.Empty;
            //if (!string.IsNullOrEmpty(Retaion_UID))
            //{
            //    List<Orders> order_list = Orders.GetModelList(t => t.O_Relation_UID == Retaion_UID).List;
            //    string _dj_pay = string.Empty;
            //    foreach (var item in order_list)
            //    {
            //        if (item.O_PayStyle == 3)
            //            _dj_pay = "3";
            //        PayMoney += item.O_PayMoney;
            //        Member_UID = item.Member_UID;
            //        OrderUIDLIST += item.O_UID + ",";
            //    }
            //    PayMoney = string.IsNullOrEmpty(_dj_pay) ? PayMoney : PayMoney * 0.3;
            //}
            //else
            //{
            //    Orders m_order = Orders.GetModel(t => t.O_UID == Order_UID);
            //    PayMoney = m_order.O_PayMoney;
            //    PayMoney = m_order.O_PayStyle != 3 ? PayMoney : PayMoney * 0.3;
            //    Member_UID = m_order.Member_UID;
            //    OrderUIDLIST += m_order.O_UID + ",";
            //}

            if (PayMoney > 0 && !string.IsNullOrEmpty(OrderUIDLIST))
            {
                ////////////////////////////////////////////请求参数////////////////////////////////////////////

                //支付类型
                string payment_type = "1";
                //必填，不能修改
                //服务器异步通知页面路径
                string notify_url = siteinfo.WebAddress + "/AlipayForOrder/Alipay_Notify";
                //需http://格式的完整路径，不能加?id=123这类自定义参数

                //页面跳转同步通知页面路径
                string return_url = siteinfo.WebAddress + "/AlipayForOrder/Alipay_Return";
                //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

                //卖家支付宝帐户
                string seller_email = m_entity.Account;
                //必填

                //商户订单号
                string out_trade_no = OrderUIDLIST.Trim();
                //商户网站订单系统中唯一订单号，必填

                //订单名称
                string subject = "会员[" + Member_UID + "] 支付订单";
                //必填

                //付款金额
                string total_fee = PayMoney.ToDouble2().ToString("0.00");
                //必填

                //订单描述

                string body = "";
                //默认支付方式
                string paymethod = "bankPay";
                //必填
                //默认网银
                string defaultbank = string.IsNullOrEmpty(bank) ? "CCB" : bank;
                //必填，银行简码请参考接口技术文档

                //商品展示地址
                string show_url = con.Show_url;
                //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

                //防钓鱼时间戳
                string anti_phishing_key = "";
                //若要使用请调用类文件submit中的query_timestamp函数

                //客户端的IP地址
                string exter_invoke_ip = "";
                //非局域网的外网IP地址，如：221.0.0.1


                ////////////////////////////////////////////////////////////////////////////////////////////////

                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                sParaTemp.Add("partner", Config.Partner);
                sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTemp.Add("service", "create_direct_pay_by_user");
                sParaTemp.Add("payment_type", payment_type);
                sParaTemp.Add("notify_url", notify_url);
                sParaTemp.Add("return_url", return_url);
                sParaTemp.Add("seller_email", seller_email);
                sParaTemp.Add("out_trade_no", out_trade_no);
                sParaTemp.Add("subject", subject);
                sParaTemp.Add("total_fee", total_fee);
                sParaTemp.Add("body", body);
                sParaTemp.Add("paymethod", paymethod);
                sParaTemp.Add("defaultbank", defaultbank);
                sParaTemp.Add("show_url", show_url);
                sParaTemp.Add("anti_phishing_key", anti_phishing_key);
                sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

                //建立请求
                string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
                return Content(sHtmlText);
            }
            return Content("该订单流水号不存在!");



        }

        /// <summary>
        /// 即刻回调函数
        /// </summary>
        /// <returns></returns>
        public ActionResult Alipay_Return()
        {

            bool isSuccess = false;
            string o = "";
            try
            {
                SortedDictionary<string, string> sArrary = GetRequestGet();
                ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
                AlipayConfig con = new AlipayConfig();
                string partner = con.Partner;
                string key = con.Key;
                string input_charset = con.Input_charset;
                string sign_type = con.Sign_type;
                string transport = con.Transport;

                SortedDictionary<string, string> sPara = GetRequestPost();

                if (sPara.Count > 0)//判断是否有带返回参数
                {
                    Notify aliNotify = new Notify();
                    bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                    if (verifyResult)//验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //请在这里加上商户的业务逻辑程序代码
                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                        //商户订单号
                        string out_trade_no = Request.Form["out_trade_no"];

                        //支付宝交易号

                        string trade_no = Request.Form["trade_no"];

                        //交易状态
                        string trade_status = Request.Form["trade_status"];


                        if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
                        {
                            string[] OrderListUID = trade_no.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();
                            lock (this)
                            {
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

                                //        #region 增加会员账户消费记录信息
                                //        MemberAccountLog m_mal = new MemberAccountLog(); //表名：MemberAccountLog 备注：会员账户消费记录
                                //        m_mal.M_UID = m_now_orders.Member_UID;        //M_UID[Type=string] - 会员唯一编号
                                //        m_mal.MA_UID = Common.Globals.CreateNewUniqueID();
                                //        m_mal.M_Cate = CommonEnumHelper.AccountCate.现金.ToString2();        //M_Cate[Type=string] - 分类(积分/现金)
                                //        m_mal.M_PayCate = CommonEnumHelper.AccountLogCate.支出.ToString2();        //M_PayCate[Type=string] - 收入/支出类型
                                //        m_mal.M_ObjectMoney = m_now_orders.O_PayMoney;        //M_ObjectMoney[Type=double?] - 操作金额
                                //        //m_mal.M_BalanceMoney = m_member_account.M_AvailableBalance;        //M_BalanceMoney[Type=double?] - 账户余额
                                //        m_mal.M_Remark = "支付订单（订单编号：" + m_now_orders.O_UID + "）";        //M_Remark[Type=string] - 备注
                                //        m_mal.M_Status = "";        //M_Status[Type=string] - 当前状态
                                //        m_mal.M_Time = DateTime.Now;        //M_Time[Type=DateTime?] - 时间
                                //        m_mal.Add(_STE);
                                //        #endregion

                                //        isSuccess = _STE.ExecuteSqlTran();

                                //    }
                                //}
                            }
                        }
                        else
                        {
                        }

                        Response.Write("success");  //请不要修改或删除

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    else//验证失败
                    {
                        Response.Write("fail");
                    }
                }
                else
                {
                    Response.Write("无通知参数");
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 定时回调函数
        /// </summary>
        /// <returns></returns>
        public ActionResult Alipay_Notify()
        {
            bool isSuccess = false;
            string o = "";
            try
            {
                SortedDictionary<string, string> sArrary = GetRequestGet();
                ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
                AlipayConfig con = new AlipayConfig();
                string partner = con.Partner;
                string key = con.Key;
                string input_charset = con.Input_charset;
                string sign_type = con.Sign_type;
                string transport = con.Transport;

                SortedDictionary<string, string> sPara = GetRequestPost();

                if (sPara.Count > 0)//判断是否有带返回参数
                {
                    Notify aliNotify = new Notify();
                    bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                    if (verifyResult)//验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //请在这里加上商户的业务逻辑程序代码
                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                        //商户订单号
                        string out_trade_no = Request.Form["out_trade_no"];

                        //支付宝交易号

                        string trade_no = Request.Form["trade_no"];

                        //交易状态
                        string trade_status = Request.Form["trade_status"];


                        if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
                        {
                            string[] OrderListUID = trade_no.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();
                            lock (this)
                            {
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

                                //        #region 增加会员账户消费记录信息
                                //        MemberAccountLog m_mal = new MemberAccountLog(); //表名：MemberAccountLog 备注：会员账户消费记录
                                //        m_mal.M_UID = m_now_orders.Member_UID;        //M_UID[Type=string] - 会员唯一编号
                                //        m_mal.MA_UID = Common.Globals.CreateNewUniqueID();
                                //        m_mal.M_Cate = CommonEnumHelper.AccountCate.现金.ToString2();        //M_Cate[Type=string] - 分类(积分/现金)
                                //        m_mal.M_PayCate = CommonEnumHelper.AccountLogCate.支出.ToString2();        //M_PayCate[Type=string] - 收入/支出类型
                                //        m_mal.M_ObjectMoney = m_now_orders.O_PayMoney;        //M_ObjectMoney[Type=double?] - 操作金额
                                //        //m_mal.M_BalanceMoney = m_member_account.M_AvailableBalance;        //M_BalanceMoney[Type=double?] - 账户余额
                                //        m_mal.M_Remark = "支付订单（订单编号：" + m_now_orders.O_UID + "）";        //M_Remark[Type=string] - 备注
                                //        m_mal.M_Status = "";        //M_Status[Type=string] - 当前状态
                                //        m_mal.M_Time = DateTime.Now;        //M_Time[Type=DateTime?] - 时间
                                //        m_mal.Add(_STE);
                                //        #endregion

                                //        isSuccess = _STE.ExecuteSqlTran();

                                //    }
                                //}
                            }
                        }
                        else
                        {
                        }

                        Response.Write("success");  //请不要修改或删除

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    else//验证失败
                    {
                        Response.Write("fail");
                    }
                }
                else
                {
                    Response.Write("无通知参数");
                }
            }
            catch (Exception ex)
            {
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

        #region IPayMent 成员

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        #endregion

    }

}
