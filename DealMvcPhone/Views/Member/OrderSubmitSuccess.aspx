<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/jcweb/product.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            下单成功
        </div>
        <div class="fr other">
            <ul>
                <li><a href="<%=Url.Action("Index","Member") %>">
                    <img src="../../App_Themes/images/p_2.png" /></a></li>
                <li><a href="<%=Url.Action("Car","Main") %>">
                    <img src="../../App_Themes/images/p_3.png" /></a></li>
            </ul>
            <span class="clear_f"></span>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        //获取当前登录会员实体
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        //获取当前登录会员的账户实体
        MemberAccount m_account = MemberAccount.GetModel(t => t.M_UID == m_login.M_UID);
        //返回的订单联合编号
        string AssociationNumber = ViewData["AssociationNumber"].ToString2();
        //返回的订单号
        string Order_UID = ViewData["Order_UID"].ToString2();

        //实例化订单信息
        Orders m_now_order = Orders.GetModel(t => t.O_UID == Order_UID);
        //应付金额
        double? MONEYALL = 0;
        if (m_now_order.IsNull)
        {
            //根据返回的联合订单编号获取所有的订单列表
            List<Orders> Orders_List = Orders.GetModelList(t => t.O_Relation_UID == AssociationNumber && t.O_IsPay == false).List;
            if (Orders_List.Count > 0)
                m_now_order = Orders_List[0];
            foreach (var item in Orders_List)
            {
                MONEYALL += item.O_PayMoney;
            }
        }
        else
        {
            MONEYALL = m_now_order.O_PayMoney;
        }

        if (m_now_order.O_PayStyle == 3)
        {
            MONEYALL = MONEYALL * 0.3;
        }
        
    %>
    <div class="PhoneWalOrder">
        <div class="success_order">
            <div class="s_i_m">
                <img src="../../App_Themes/images/nimg128.png" />
            </div>
            <div class="s_ii_m">
                <%=m_now_order.O_PayStyle == 3 ? "恭喜您！订单提交成功~请您尽快支付定金" : "恭喜您！订单提交成功~请您尽快支付！"%>
            </div>
            <div class="s_iii_m">
                <div class="I_O_M_I">
                    需支付<%=m_now_order.O_PayStyle==3?"定金":"" %>：<span>￥<%=MONEYALL.ToJiaGe() %></span></div>
                <div class="II_O_M_I">
                    支付方式：<span><%=m_now_order.B %><%=m_now_order.O_PayStyle==4?"（余额：￥"+m_account.M_AvailableBalance.ToJiaGe()+"）":"" %></span></div>
                <div class="clear_f">
                </div>
            </div>
            <%
               
                if (m_now_order.O_PayStyle == 2 || (m_now_order.O_PayStyle == 3 && m_now_order.E == "2"))
                {
            %>
            <div class="BankPayModel">
                <input type="hidden" name="ChangeBankCode" value="CCB" />
                <div class="small_b">
                    <div class="c_b_smll checkboxM checkin" dm="CCB">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -1050px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="BOCB2C">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -292px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="CMB">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -1337px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="ICBC-DEBIT">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -3103px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="COMM-DEBIT">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -1410px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="CIB">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -1266px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="ABC">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -5px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="CEB-DEBIT">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -1193px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="SPDB">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -5084px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="CMBC">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -1374px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="GDB">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -2165px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="small_b">
                    <div class="c_b_smll checkboxM" dm="SPABANK">
                    </div>
                    <div class="c_b_ico" style="background-position: 0px -5047px;">
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <div class="clear_f">
                </div>
            </div>
            <%
                }
            %>
            <div class="btnDiv" style="margin-bottom: 20px;">
                <%
                    if (m_now_order.O_PayStyle == 4)
                    {
                %>
                <div class="InputPassWordM">
                    支付密码：<input type="password" name="paypwdModelIN" value="" class="paypwdModelIN" /><span
                        class="ccmm"><% if (!m_login.M_IsSetPayPwd)
                                        { %>您还未设置支付密码,<a href="<%=Url.Action("SafetyCertificationIndex","Member") %>" target="_blank">设置支付密码</a><%} %></span>
                </div>
                <%} %>
                <div class="NowPayOrderModel PDMAYPAYBTN" p_style="<%=m_now_order.O_PayStyle==3?m_now_order.E.ToInt32():m_now_order.O_PayStyle %>"
                    o_uid="<%=m_now_order.O_UID %>" r_uid="<%=AssociationNumber %>">
                    立即支付</div>
            </div>
             <div class="h15">
        </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
