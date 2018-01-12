using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections;
using DealMvc.Model;

namespace DealMvc.Pay
{
    public class AlipayController : Controller, IPayMent
    {
        public ActionResult Checkout(string param1, string param2)
        {
            return Content("");
        }

        /// <summary>
        /// 跳转支付函数
        /// </summary>
        /// <param name="m_uid">会员编号</param>
        /// <param name="money">充值金额</param>
        /// <param name="cate">支付类型【"充值"/"活动付款"】</param>
        /// <returns></returns>
        public ActionResult Checkout(string obj_uid, string M_UID, string cate, double? money)
        {
            if (!string.IsNullOrEmpty(obj_uid) && money > 0)
            {
                #region 充值/活动支付
                ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的///////////////////////////

                AlipayConfig con = new AlipayConfig();
                string partner = con.Partner;
                string key = con.Key;
                string seller_email = con.Seller_email;
                string input_charset = con.Input_charset;
                string notify_url = con.Notify_url;
                string return_url = con.Return_url;
                string show_url = con.Show_url;    //#
                string sign_type = con.Sign_type;

                //##########################################
                ///////////////////////以下参数是需要通过下单时的订单数据传入进来获得////////////////////////////////
                string m_username = "";
                string actioncate = string.Empty;
                switch (cate)
                {
                    
                }
                //必填参数
                //请与贵网站订单系统中的唯一订单号匹配
                string out_trade_no = obj_uid;
                //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
                string tips = "会员[" + m_username + "]" + actioncate;
                string subject = tips;
                //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
                string body = cate;
                //订单总金额，显示在支付宝收银台里的“应付总额”里    
                string total_fee = money.ToDouble2().ToString("0.00");
                //必填参数 - End
                //##########################################


                //扩展功能参数——默认支付方式
                string paymethod = "bankPay"; //默认支付方式，四个值可选：bankPay(网银); cartoon(卡通); directPay(余额); CASH(网点支付)，初始值
                string defaultbank = "";  //默认网银代号，代号列表见http://club.alipay.com/read.php?tid=8681379 初始值
                //string pay_mode = Request["pay_bank"];
                //if (pay_mode == "directPay")
                //{
                //    paymethod = "directPay";
                //}
                //else
                //{
                //    paymethod = "bankPay";
                //    defaultbank = pay_mode;
                //}
                //扩展功能参数——防钓鱼
                //请慎重选择是否开启防钓鱼功能
                //exter_invoke_ip、anti_phishing_key一旦被设置过，那么它们就会成为必填参数
                //建议使用POST方式请求数据
                string anti_phishing_key = "";                                  //防钓鱼时间戳
                string exter_invoke_ip = "";                                    //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
                //如：
                //exter_invoke_ip = "";
                //anti_phishing_key = AlipayFunction.Query_timestamp(partner);  //获取防钓鱼时间戳函数
                //扩展功能参数——其他
                string extra_common_param = M_UID;                                 //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
                string buyer_email = "";			                            //默认买家支付宝账号
                //扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)
                string royalty_type = "";                                   //提成类型，该值为固定值：10，不需要修改
                string royalty_parameters = "";
                //提成信息集，与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
                //各分润金额的总和须小于等于total_fee
                //提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
                //如：
                //royalty_type = "10";
                //royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二";

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                //构造请求函数，无需修改
                AlipayService aliService = new AlipayService(partner, seller_email, return_url, notify_url, show_url, out_trade_no, subject, body, total_fee, paymethod, defaultbank, anti_phishing_key, exter_invoke_ip, extra_common_param, buyer_email, royalty_type, royalty_parameters, key, input_charset, sign_type);
                string sHtmlText = aliService.Build_Form();

                //打印页面
                return Content(sHtmlText);
                #endregion
            }
            return Content("该充值流水号不存在!");
        }

        /// <summary>
        /// 即刻回调函数
        /// </summary>
        /// <returns></returns>
        public ActionResult Alipay_Return()
        {
            bool isSuccess = false;
            string AcitonCate = string.Empty;
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
                //////////////////////////////////////////////////////////////////////////////////////////////

                if (sArrary.Count > 0)//判断是否有带返回参数
                {
                    AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.QueryString["notify_id"], partner, key, input_charset, sign_type, transport);
                    string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                    string sign = Request.QueryString["sign"];  //获取支付宝反馈回来的sign结果
                    string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的签名结果

                    //写日志记录（若要调试，请取消下面两行注释）
                    //string sWord = "responseTxt=" + responseTxt + "\n return_url_log:sign=" + Request.QueryString["sign"] + "&mysign=" + mysign + "\n return回来的参数：" + aliNotify.PreSignStr;
                    //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt",sWord);

                    //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                    //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                    //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关

                    if (responseTxt == "true" && sign == mysign)//验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                        string trade_no = Request.QueryString["trade_no"];      //支付宝交易号
                        string order_no = Request.QueryString["out_trade_no"];	//获取订单号
                        string total_fee = Request.QueryString["total_fee"];	//获取总金额
                        string subject = Request.QueryString["subject"];        //商品名称、订单名称
                        string body = Request.QueryString["body"];              //商品描述、订单备注、描述
                        string buyer_email = Request.QueryString["buyer_email"];//买家支付宝账号
                        string trade_status = Request.QueryString["trade_status"];//交易状态
                        string extra_common_param = Request.QueryString["trade_status"];//会员唯一编号
                        ////打印页面
                        if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                        {
                            //#################################################
                            try
                            {
                                SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();
                                #region 付款成功之后的操作
                                switch (body)
                                {
                                   
                                }
                                #endregion

                                isSuccess = _STE.ExecuteSqlTran();

                                //return Content("失败：该流水号不存在");
                            }
                            catch { return Content("Error"); }
                            //#################################################
                        }
                        else
                        {
                            Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                        }
                        ////——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                    }
                    else
                    { //验证失败
                    }
                }
                else
                {
                    return Content("无返回参数");
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 定时回调函数
        /// </summary>
        /// <returns></returns>
        public ActionResult Alipay_Notify()
        {
            SortedDictionary<string, string> sArrary = GetRequestPost();
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
            AlipayConfig con = new AlipayConfig();
            string partner = con.Partner;
            string key = con.Key;
            string input_charset = con.Input_charset;
            string sign_type = con.Sign_type;
            string transport = con.Transport;
            //////////////////////////////////////////////////////////////////////////////////////////////

            if (sArrary.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.Form["notify_id"], partner, key, input_charset, sign_type, transport);
                string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                string sign = Request.Form["sign"];         //获取支付宝反馈回来的sign结果
                string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的签名结果

                //写日志记录（若要调试，请取消下面两行注释）
                //string sWord = "responseTxt=" + responseTxt + "\n notify_url_log:sign=" + Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + aliNotify.PreSignStr;
                //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);

                //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关

                if (responseTxt == "true" && sign == mysign)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码
                    string trade_no = Request.QueryString["trade_no"];      //支付宝交易号
                    string order_no = Request.QueryString["out_trade_no"];//订单号
                    string total_fee = Request.QueryString["total_fee"];//金额
                    string strTradeStatus = Request.QueryString["trade_status"];//订单状态
                    string body = Request.QueryString["body"];              //商品描述、订单备注、描述

                    //}
                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //#################################################
                        try
                        {

                            SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();

                            #region 付款成功之后的操作
                            switch (body)
                            {
                                
                            }
                            #endregion

                            _STE.ExecuteSqlTran();
                        }
                        catch { return Content("Error"); }
                        //#################################################
                    }
                    else
                    {
                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }

                    return Content("success");
                }
                else
                {//验证失败
                    return Content("fail");
                }
            }
            else
            {
                return Content("无通知参数");
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

    public class AlipayForOrderController : Controller, IPayMent
    {
        /// <summary>
        /// 跳转支付函数
        /// </summary>
        /// <param name="Order_UID"></param>
        /// <param name="Retaion_UID"></param>
        /// <returns></returns>
        public ActionResult Checkout(string Order_UID, string Retaion_UID)
        {

            Model.SiteInfo siteinfo = Model.SiteInfo.GetModel(t => t.id != 0);
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的///////////////////////////
            AlipayConfig con = new AlipayConfig();
            string partner = con.Partner;
            string key = con.Key;
            string seller_email = con.Seller_email;
            string input_charset = con.Input_charset;
            string notify_url = siteinfo.WebAddress + "/AlipayForOrder/Alipay_Notify";
            string return_url = siteinfo.WebAddress + "/AlipayForOrder/Alipay_Return";
            string show_url = con.Show_url;
            string sign_type = con.Sign_type;

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
                #region 支付
                //必填参数
                //请与贵网站订单系统中的唯一订单号匹配
                string out_trade_no = OrderUIDLIST;
                //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
                string subject = "会员[" + Member_UID + "] 支付订单";
                //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
                string body = "";
                //订单总金额，显示在支付宝收银台里的“应付总额”里    
                string total_fee = PayMoney.ToDouble2().ToString("0.00");
                //必填参数 - End
                //##########################################


                //扩展功能参数——默认支付方式
                string paymethod = "bankPay"; //默认支付方式，四个值可选：bankPay(网银); cartoon(卡通); directPay(余额); CASH(网点支付)，初始值
                string defaultbank = "";  //默认网银代号，代号列表见http://club.alipay.com/read.php?tid=8681379 初始值
                //string pay_mode = Request["pay_bank"];
                //if (pay_mode == "directPay")
                //{
                //    paymethod = "directPay";
                //}
                //else
                //{
                //    paymethod = "bankPay";
                //    defaultbank = pay_mode;
                //}
                //扩展功能参数——防钓鱼
                //请慎重选择是否开启防钓鱼功能
                //exter_invoke_ip、anti_phishing_key一旦被设置过，那么它们就会成为必填参数
                //建议使用POST方式请求数据
                string anti_phishing_key = "";                                  //防钓鱼时间戳
                string exter_invoke_ip = "";                                    //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
                //如：
                //exter_invoke_ip = "";
                //anti_phishing_key = AlipayFunction.Query_timestamp(partner);  //获取防钓鱼时间戳函数
                //扩展功能参数——其他
                string extra_common_param = "";                                 //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
                string buyer_email = "";			                            //默认买家支付宝账号
                //扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)
                string royalty_type = "";                                   //提成类型，该值为固定值：10，不需要修改
                string royalty_parameters = "";
                //提成信息集，与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
                //各分润金额的总和须小于等于total_fee
                //提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
                //如：
                //royalty_type = "10";
                //royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二";

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                //构造请求函数，无需修改
                AlipayService aliService = new AlipayService(partner, seller_email, return_url, notify_url, show_url, out_trade_no, subject, body, total_fee, paymethod, defaultbank, anti_phishing_key, exter_invoke_ip, extra_common_param, buyer_email, royalty_type, royalty_parameters, key, input_charset, sign_type);
                string sHtmlText = aliService.Build_Form();

                //打印页面
                return Content(sHtmlText);
                #endregion
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

                //////////////////////////////////////////////////////////////////////////////////////////////

                if (sArrary.Count > 0)//判断是否有带返回参数
                {
                    AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.QueryString["notify_id"], partner, key, input_charset, sign_type, transport);
                    string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                    string sign = Request.QueryString["sign"];  //获取支付宝反馈回来的sign结果
                    string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的签名结果

                    //写日志记录（若要调试，请取消下面两行注释）
                    //string sWord = "responseTxt=" + responseTxt + "\n return_url_log:sign=" + Request.QueryString["sign"] + "&mysign=" + mysign + "\n return回来的参数：" + aliNotify.PreSignStr;
                    //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt",sWord);

                    //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                    //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                    //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关

                    if (responseTxt == "true" && sign == mysign)//验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                        string trade_no = Request.QueryString["trade_no"];      //支付宝交易号
                        string order_no = Request.QueryString["out_trade_no"];	//获取订单号
                        string total_fee = Request.QueryString["total_fee"];	//获取总金额
                        string subject = Request.QueryString["subject"];        //商品名称、订单名称
                        string body = Request.QueryString["body"];              //商品描述、订单备注、描述
                        string buyer_email = Request.QueryString["buyer_email"];//买家支付宝账号
                        string trade_status = Request.QueryString["trade_status"];//交易状态

                        ////打印页面
                        if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                        {
                            //#################################################
                            try
                            {
                                string[] OrderListUID = order_no.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
                            catch { return Content("Error"); }
                            //#################################################
                        }
                        else
                        {
                            Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                        }
                        ////——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                    }
                    else
                    { //验证失败
                    }
                }
                else
                {
                    return Content("无返回参数");
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
            SortedDictionary<string, string> sArrary = GetRequestPost();
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
            AlipayConfig con = new AlipayConfig();
            string partner = con.Partner;
            string key = con.Key;
            string input_charset = con.Input_charset;
            string sign_type = con.Sign_type;
            string transport = con.Transport;
            //////////////////////////////////////////////////////////////////////////////////////////////

            if (sArrary.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.Form["notify_id"], partner, key, input_charset, sign_type, transport);
                string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                string sign = Request.Form["sign"];         //获取支付宝反馈回来的sign结果
                string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的签名结果

                //写日志记录（若要调试，请取消下面两行注释）
                //string sWord = "responseTxt=" + responseTxt + "\n notify_url_log:sign=" + Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + aliNotify.PreSignStr;
                //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);

                //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关

                if (responseTxt == "true" && sign == mysign)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码
                    string trade_no = Request.Form["trade_no"];      //支付宝交易号
                    string order_no = Request.Form["out_trade_no"];//订单号
                    string total_fee = Request.Form["total_fee"];//金额
                    string strTradeStatus = Request.Form["trade_status"];//订单状态

                    //}
                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //#################################################
                        try
                        {
                            string[] OrderListUID = order_no.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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

                                //        _STE.ExecuteSqlTran();

                                //    }
                                //}
                            }
                        }
                        catch { return Content("Error"); }
                        //#################################################
                    }
                    else
                    {
                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }

                    return Content("success");
                }
                else
                {//验证失败
                    return Content("fail");
                }
            }
            else
            {
                return Content("无通知参数");
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
