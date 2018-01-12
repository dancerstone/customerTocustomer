using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections;
using DealMvc.Model;
using DealMvc.SqlTranEx;

namespace DealMvc.Pay
{

    /// <summary>
    /// 充值版块
    /// </summary>
    public class CBPayMenForChongZhiController : Controller
    {
        //必要的交易信息
        protected string v_amount;       // 订单金额
        protected string v_moneytype;    // 币种
        protected string v_md5info;      // 对拼凑串MD5私钥加密后的值
        protected string v_mid;		 // 商户号
        protected string v_url;		 // 返回页地址
        protected string v_oid;		 // 推荐订单号构成格式为 年月日-商户号-小时分钟秒

        //收货信息
        //protected string v_rcvname;      // 收货人
        //protected string v_rcvaddr;      // 收货地址
        //protected string v_rcvtel;       // 收货人电话
        //protected string v_rcvpost;      // 收货人邮编
        //protected string v_rcvemail;     // 收货人邮件
        //protected string v_rcvmobile;    // 收货人手机号

        ////订货人信息
        //protected string v_ordername;    // 订货人姓名
        //protected string v_orderaddr;    // 订货人地址
        //protected string v_ordertel;     // 订货人电话
        //protected string v_orderpost;    // 订货人邮编
        //protected string v_orderemail;   // 订货人邮件
        //protected string v_ordermobile;  // 订货人手机号

        //两个备注
        protected string remark1;
        protected string remark2;

        /// <summary>
        /// 跳转支付函数
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult Checkout(string obj_uid, string M_UID, string cate, double? money)
        {
            string HomeUrl = SiteInfo.GetModel(t => t.id != 0).WebAddress;

            v_mid = "22483778";				 // 商户号，这里为测试商户号20000400，替换为自己的商户号即可
            v_url = HomeUrl + "/CBPayMenForChongZhi/CBPayMen_Return"; ; // 商户自定义返回接收支付结果的页面
            // MD5密钥要跟订单提交页相同，如Send.asp里的 key = "test" ,修改""号内 test 为您的密钥
            string key = "klfd235d6s8922fd2s3afsa";	 // 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
            // 登陆后在上面的导航栏里可能找到“资料管理”，在资料管理的二级导航栏里有“MD5密钥设置”
            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了

            #region 充值 操作
            if (!string.IsNullOrEmpty(obj_uid) && money > 0)
            {

                //网站这边订单号
                v_oid = obj_uid; //Request["v_oid"];//订单编号
                v_amount = money.ToString2();//订单金额
                if (v_oid == null || v_oid.Equals(""))
                {
                    DateTime dt = DateTime.Now;
                    string v_ymd = dt.ToString("yyyyMMdd"); // yyyyMMdd
                    string timeStr = dt.ToString("HHmmss"); // HHmmss
                    v_oid = v_ymd + v_mid + timeStr;
                }
                
                v_moneytype = "CNY";

                string text = v_amount + v_moneytype + v_oid + v_mid + v_url + key; // 拼凑加密串

                v_md5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToUpper();

                //收货信息
                //v_rcvname = "";// Request["v_rcvname"];
                //v_rcvaddr = "";// Request["v_rcvaddr"];
                //v_rcvtel = "";// Request["v_rcvtel"];
                //v_rcvpost = "";// Request["v_rcvpost"];
                //v_rcvemail = "";// Request["v_rcvemail"];
                //v_rcvmobile = "";// Request["v_rcvmobile"];

                ////订货人信息
                //v_ordername = Request["v_ordername"];
                //v_orderaddr = Request["v_orderaddr"];
                //v_ordertel = Request["v_ordertel"];
                //v_orderpost = Request["v_orderpost"];
                //v_orderemail = Request["v_orderemail"];
                //v_ordermobile = Request["v_ordermobile"];

                remark1 = cate;// Request["remark1"];
                remark2 = M_UID;//Request["remark2"];

                StringBuilder sHtmlText = new StringBuilder();

                sHtmlText.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sHtmlText.AppendLine("  <html xmlns=\"http://www.w3.org/1999/xhtml\" >");
                sHtmlText.AppendLine("  <head runat=\"server\">");
                sHtmlText.AppendLine("      <title>订单提交</title>");
                sHtmlText.AppendLine("  </head>");
                sHtmlText.AppendLine("  <body >");
                sHtmlText.AppendLine("  <body onLoad=\"javascript:document.E_FORM.submit()\" >");
                sHtmlText.AppendLine("       <form    action=\"https://pay3.chinabank.com.cn/PayGate?encoding=UTF-8\" accept-charset=\"utf-8\"  method=\"post\" name=\"E_FORM\">");

                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_md5info\"    value=\"" + v_md5info + "\" size=\"100\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_mid\"        value=\"" + v_mid + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_oid\"        value=\"" + v_oid + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_amount\"     value=\"" + v_amount + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_moneytype\"  value=\"" + v_moneytype + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_url\"        value=\"" + v_url + "\" />");


                sHtmlText.AppendLine("  <!--以下几项项为网上支付完成后，随支付反馈信息一同传给信息接收页-->");

                sHtmlText.AppendLine("   <input type=\"hidden\"  name=\"remark1\" value=\"" + remark1 + "\" />");
                sHtmlText.AppendLine("    <input type=\"hidden\"  name=\"remark2\" value=\"" + remark2 + "\" />");

                //sHtmlText.AppendLine("    <!--以下几项只是用来记录客户信息，可以不用，不影响支付 -->");

                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvname\"      value=\"<%=v_rcvname%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvaddr\"      value=\"<%=v_rcvaddr%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvtel\"       value=\"<%=v_rcvtel%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvpost\"      value=\"<%=v_rcvpost%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvemail\"     value=\"<%=v_rcvemail%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvmobile\"    value=\"<%=v_rcvmobile%>\" />");

                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_ordername\"    value=\"<%=v_ordername%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_orderaddr\"    value=\"<%=v_orderaddr%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_ordertel\"     value=\"<%=v_ordertel%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_orderpost\"    value=\"<%=v_orderpost%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_orderemail\"   value=\"<%=v_orderemail%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_ordermobile\"  value=\"<%=v_ordermobile%>\" />");
                sHtmlText.AppendLine("      </form>");
                sHtmlText.AppendLine("  </body>");
                sHtmlText.AppendLine("  </html>");

                return Content(sHtmlText.ToString());
            }
            #endregion

            return Content("该交易流水号不存在!");
        }


        //protected string v_oid;		// 订单号
        protected string v_pstatus;	// 支付状态码
        //20（支付成功，对使用实时银行卡进行扣款的订单）；
        //30（支付失败，对使用实时银行卡进行扣款的订单）；

        protected string v_pstring;	//支付状态描述
        protected string v_pmode;	//支付银行
        //protected string v_md5info;	//MD5校验码
        //protected string v_amount;	//支付金额
        //protected string v_moneytype;	//币种		
        //protected string remark1;	// 备注1
        //protected string remark2;	// 备注1

        protected string v_md5str;

        protected string status_msg;

        /// <summary>
        /// 即刻回调函数
        /// </summary>
        /// <returns></returns>
        public ActionResult CBPayMen_Return()
        {
            bool isSuccess = false;
            string AcitonCate = string.Empty;
            // MD5密钥要跟订单提交页相同，如Send.asp里的 key = "test" ,修改""号内 test 为您的密钥
            string key = "klfd235d6s8922fd2s3afsa";	// 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
            // 登陆后在上面的导航栏里可能找到“资料管理”，在资料管理的二级导航栏里有“MD5密钥设置”
            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了

            v_oid = Request["v_oid"];
            v_pstatus = Request["v_pstatus"];
            v_pstring = Request["v_pstring"];
            v_pmode = Request["v_pmode"];
            v_md5str = Request["v_md5str"];
            v_amount = Request["v_amount"];
            v_moneytype = Request["v_moneytype"];
            remark1 = Request["remark1"];
            remark2 = Request["remark2"];

            string str = v_oid + v_pstatus + v_amount + v_moneytype + key;

            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();

            if (str == v_md5str)
            {

                if (v_pstatus.Equals("20"))
                {
                    //支付成功
                    //在这里商户可以写上自己的业务逻辑
                    //#################################################
                    try
                    {
                        SqlTranEx.SqlTranExtensions _STE = new SqlTranEx.SqlTranExtensions();
                        #region 付款成功之后的操作
                        switch (remark2)
                        {
                            
                        }
                        #endregion

                        isSuccess = _STE.ExecuteSqlTran();
                    }
                    catch { return Content("Error"); }

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
            else
            {
                return RedirectToAction("Order_Status3", "Order", new { msg = "校验失败,数据可疑" });
            }

            return RedirectToAction("Order_Status3", "Order", new { msg = "error" });
        }
    }

    /// <summary>
    /// 支付订单版块
    /// </summary>
    public class CBPayMenForOrderController : Controller
    {
        //必要的交易信息
        protected string v_amount;       // 订单金额
        protected string v_moneytype;    // 币种
        protected string v_md5info;      // 对拼凑串MD5私钥加密后的值
        protected string v_mid;		 // 商户号
        protected string v_url;		 // 返回页地址
        protected string v_oid;		 // 推荐订单号构成格式为 年月日-商户号-小时分钟秒

        //收货信息
        protected string v_rcvname;      // 收货人
        protected string v_rcvaddr;      // 收货地址
        protected string v_rcvtel;       // 收货人电话
        protected string v_rcvpost;      // 收货人邮编
        protected string v_rcvemail;     // 收货人邮件
        protected string v_rcvmobile;    // 收货人手机号

        ////订货人信息
        //protected string v_ordername;    // 订货人姓名
        //protected string v_orderaddr;    // 订货人地址
        //protected string v_ordertel;     // 订货人电话
        //protected string v_orderpost;    // 订货人邮编
        //protected string v_orderemail;   // 订货人邮件
        //protected string v_ordermobile;  // 订货人手机号

        //两个备注
        protected string remark1;
        protected string remark2;

        /// <summary>
        /// 跳转支付函数
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult Checkout(string Order_UID, string Retaion_UID)
        {
            string HomeUrl = SiteInfo.GetModel(t => t.id != 0).WebAddress;
            SitePayAPI m_entity = SitePayAPI.GetModel(t => t.ApiType == "财付通");


            v_mid = m_entity.AppIdentity.ToString2();				 // 商户号，这里为测试商户号20000400，替换为自己的商户号即可
            v_url = HomeUrl + "/CBPayMenForOrder/CBPayMen_Return"; ; // 商户自定义返回接收支付结果的页面
            // MD5密钥要跟订单提交页相同，如Send.asp里的 key = "test" ,修改""号内 test 为您的密钥
            string key = m_entity.AppKey.ToString2();	 // 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
            // 登陆后在上面的导航栏里可能找到“资料管理”，在资料管理的二级导航栏里有“MD5密钥设置”
            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了

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

            //网站这边订单号
            v_oid = OrderUIDLIST; //Request["v_oid"];
            v_amount = PayMoney.ToString2();
            if (v_oid == null || v_oid.Equals(""))
            {
                DateTime dt = DateTime.Now;
                string v_ymd = dt.ToString("yyyyMMdd"); // yyyyMMdd
                string timeStr = dt.ToString("HHmmss"); // HHmmss
                v_oid = v_ymd + v_mid + timeStr;
            }

            #region 支付

            v_moneytype = "CNY";
            if (PayMoney > 0 && !string.IsNullOrEmpty(OrderUIDLIST))
            {
                string text = v_amount + v_moneytype + v_oid + v_mid + v_url + key; // 拼凑加密串
                v_md5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToUpper();
                remark1 = "支付订单：" + v_oid;// Request["remark1"];
                remark2 = "";//Request["remark2"];

                StringBuilder sHtmlText = new StringBuilder();

                sHtmlText.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sHtmlText.AppendLine("  <html xmlns=\"http://www.w3.org/1999/xhtml\" >");
                sHtmlText.AppendLine("  <head runat=\"server\">");
                sHtmlText.AppendLine("      <title>订单提交</title>");
                sHtmlText.AppendLine("  </head>");
                sHtmlText.AppendLine("  <body >");
                sHtmlText.AppendLine("  <body  onLoad=\"javascript:document.E_FORM.submit()\">");
                sHtmlText.AppendLine("       <form    action=\"https://pay3.chinabank.com.cn/PayGate?encoding=UTF-8\" accept-charset=\"utf-8\"  method=\"post\" name=\"E_FORM\">");

                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_md5info\"    value=\"" + v_md5info + "\" size=\"100\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_mid\"        value=\"" + v_mid + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_oid\"        value=\"" + v_oid + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_amount\"     value=\"" + v_amount + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_moneytype\"  value=\"" + v_moneytype + "\" />");
                sHtmlText.AppendLine("        <input type=\"hidden\" name=\"v_url\"        value=\"" + v_url + "\" />");


                sHtmlText.AppendLine("  <!--以下几项项为网上支付完成后，随支付反馈信息一同传给信息接收页-->");
                sHtmlText.AppendLine("   <input type=\"hidden\"  name=\"remark1\" value=\"" + remark1 + "\" />");
                sHtmlText.AppendLine("    <input type=\"hidden\"  name=\"remark2\" value=\"" + remark2 + "\" />");

                //sHtmlText.AppendLine("    <!--以下几项只是用来记录客户信息，可以不用，不影响支付 -->");

                sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvname\"      value=\"" + v_rcvname + "\" />");
                sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvaddr\"      value=\"" + v_rcvaddr + "\" />");
                sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvtel\"       value=\"" + v_rcvtel + "\" />");
                sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvpost\"      value=\"" + v_rcvpost + "\" />");
                sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvemail\"     value=\"" + v_rcvemail + "\" />");
                sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_rcvmobile\"    value=\"" + v_rcvmobile + "\" />");

                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_ordername\"    value=\"<%=v_ordername%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_orderaddr\"    value=\"<%=v_orderaddr%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_ordertel\"     value=\"<%=v_ordertel%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_orderpost\"    value=\"<%=v_orderpost%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_orderemail\"   value=\"<%=v_orderemail%>\" />");
                //sHtmlText.AppendLine("      <input type=\"hidden\"  name=\"v_ordermobile\"  value=\"<%=v_ordermobile%>\" />");
                sHtmlText.AppendLine("      </form>");
                sHtmlText.AppendLine("  </body>");
                sHtmlText.AppendLine("  </html>");

                return Content(sHtmlText.ToString());
            } 
            #endregion

            return Content("该订单流水号不存在!");

        }


        //protected string v_oid;		// 订单号
        protected string v_pstatus;	// 支付状态码
        //20（支付成功，对使用实时银行卡进行扣款的订单）；
        //30（支付失败，对使用实时银行卡进行扣款的订单）；

        protected string v_pstring;	//支付状态描述
        protected string v_pmode;	//支付银行
        //protected string v_md5info;	//MD5校验码
        //protected string v_amount;	//支付金额
        //protected string v_moneytype;	//币种		
        //protected string remark1;	// 备注1
        //protected string remark2;	// 备注1

        protected string v_md5str;

        protected string status_msg;

        /// <summary>
        /// 即刻回调函数
        /// </summary>
        /// <returns></returns>
        public ActionResult CBPayMen_Return()
        {
            bool isSuccess = false;
            // MD5密钥要跟订单提交页相同，如Send.asp里的 key = "test" ,修改""号内 test 为您的密钥
            string key = "klfd235d6s8922fd2s3afsa";	// 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
            // 登陆后在上面的导航栏里可能找到“资料管理”，在资料管理的二级导航栏里有“MD5密钥设置”
            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了

            v_oid = Request["v_oid"];
            v_pstatus = Request["v_pstatus"];
            v_pstring = Request["v_pstring"];
            v_pmode = Request["v_pmode"];
            v_md5str = Request["v_md5str"];
            v_amount = Request["v_amount"];
            v_moneytype = Request["v_moneytype"];
            remark1 = Request["remark1"];
            remark2 = Request["remark2"];

            string str = v_oid + v_pstatus + v_amount + v_moneytype + key;

            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();

            if (str == v_md5str)
            {

                if (v_pstatus.Equals("20"))
                {
                    #region 支付成功
                    try
                    {
                        string[] OrderListUID = v_oid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
                    catch
                    {
                         return Content("err-003失败：该流水号不存在"); 
                    }
                    #endregion

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
            else
            {
                return Content("校验失败,数据可疑");
            }

            return Content("error");
        }
    } 
}
