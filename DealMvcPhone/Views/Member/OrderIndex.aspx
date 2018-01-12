<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Member.css" rel="stylesheet" type="text/css" />
    <script src="../../JS/jcweb/product.js" type="text/javascript"></script>
    <script src="../../JS/jcweb/form.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            确认订单</h1>
        <a class="btnL" href="javascript:history.go(-1);">
            <img src="/App_Themes/images/img_goback.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        SiteInfo m_site = SiteInfo.GetModel(t => t.id != 0);
        //获取登录会员实体
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        //获取登录会员账户实体
        MemberAccount m_account = MemberAccount.GetModel(t => t.M_UID == m_login.M_UID);
        //获取当前登录会员收货地址
        List<DeliveryAddress> deliveryAddressList = DeliveryAddress.GetModelList(5, t => t.DA_MemberUID == m_login.M_UID, t => t.DA_Time.lb_Desc()).List;
        //获取默认的收货地址
        DeliveryAddress m_default = DeliveryAddress.GetModel(t => t.DA_MemberUID == m_login.M_UID && t.DA_IsDefault == true);
        //获取默认的支付方式
        OrderPayStyleAndInvoices m_opai = OrderPayStyleAndInvoices.GetModel(t => t.Member_UID == m_login.M_UID);
        //获取默认的发票信息
        OrderDefaultInvoices m_odi = OrderDefaultInvoices.GetModel(t => t.Member_UID == m_login.M_UID);
        //购物车列表
        List<ShoppingCar> shoppingcarList = (List<ShoppingCar>)ViewData["ShoppingCarList"];
        
    %>
    <form action="<%=Url.Action("SubmitOrder","Member") %>" method="post" class="OrderSubmitFrom">
    <input type="hidden" name="CarOrBuy" value="<%=ViewData["CarOrBuy".ToString2()] %>" />
    <input type="hidden" name="Count" value="<%=ViewData["Count"].ToInt32() %>" />
    <input type="hidden" name="goods_uid" value="<%=ViewData["goods_uid"] %>" />
    <input type="hidden" name="goods_id_btn" value="<%=ViewData["goods_id_btn"] %>" />
    <div class="sureOrder"> 
        <div class="h15">
        </div>
        <%
            foreach (var item in shoppingcarList)
            {
                double? SMALL_Money = (item.SC_Count * item.m_Goods.G_Price).ToDouble2();
        %>
        <div class="goodsInfo">
            <div class="img fl">
                <a href="<%=Url.Action("productdetail","Main",new{gid=item.m_Goods.id}) %>">
                    <img src="<%=m_site.WebAddress+item.m_Goods.G_MainPic.JGetOnePic() %>" /></a>
            </div>
            <div class="name fl">
                <a href="<%=Url.Action("productdetail","Main",new{gid=item.m_Goods.id}) %>">
                    <%=item.m_Goods.G_Title%></a>
            </div>
            <div class="price fl">
                ￥<%=SMALL_Money%>
                <span>X
                    <%=item.SC_Count %></span>
            </div>
            <span class="clear_f"></span>
        </div>
        <%} %>
        <div class="h7">
        </div>
        <div class="addressInfo " onclick="prompt_fun('.update_m_address')">
            <input type="hidden" name="Finally_Address_ID" value="<%=m_default.id %>" />
            <div class="ad_left fl m_address_div">
                <div class="top">
                    <div class="name fl">
                        <%=m_default.DA_ConsigneeName%></div>
                    <div class="phone fl">
                        <%=m_default.DA_Phone %></div>
                    <span class="clear_f"></span>
                </div>
                <div class="bottom">
                    <%=m_default.DA_Province+m_default.DA_City+m_default.DA_Area+m_default.DA_DetailAddress%>
                </div>
            </div>
            <div class="ad_right fr">
                <img src="../../App_Themes/images/20150310_28.png" />
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="RechargeLayer update_m_address">
            <div class="title">
                修改地址</div>
            <div class="form">
                <div class="address_list">
                    <ul>
                        <%
                            foreach (var item in deliveryAddressList)
                            {
                        %>
                        <li>
                            <div class="check_m" aid="<%=item.id %>">
                            </div>
                            <div class="a_list_m">
                                <%=item.DA_ConsigneeName %>&nbsp;&nbsp;&nbsp;<%=item.DA_Phone %><br />
                                <%=item.DA_Province + item.DA_City + item.DA_Area + item.DA_DetailAddress%>
                            </div>
                            <div class="clear_f">
                            </div>
                        </li>
                        <%
                            }
                        %>
                    </ul>
                </div>
                <div class="r_t_b">
                    <input type="button" name="name" value="确 认" class="sure_btn_c btn_order_a_sure" />
                </div>
            </div>
        </div>
        <div class="h7">
        </div>
        <div class="payStyle" onclick="prompt_fun('.update_pay_send_address')">
            <div class="ad_left fl m_pay_send_div">
                <input type="hidden" name="Finally_PayStyle_ID" value="<%=m_opai.id %>" />
                <div class="top">
                    <div class="name">
                        支付及配送</div>
                </div>
                <div class="bottom">
                    <%=m_opai.B %>/<%=m_opai.C == "2" ? "上门自提 运费：￥0.00" : "快递运输 运费：￥" + new DealMvc.Core.Goods.BLL_Goods_XLY().GetGoodsFeight(shoppingcarList)%>
                </div>
            </div>
            <div class="ad_right fr">
                <img src="../../App_Themes/images/20150310_28.png" />
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="RechargeLayer update_pay_send_address">
            <input type="hidden" name="checkpaystylevalue" value="" />
            <input type="hidden" name="checksendstylevalue" value="" />
            <input type="hidden" name="checkdjpaystylevalue" value="" />
            <div class="title">
                修改支付及配送</div>
            <div class="form">
                <div class="pay_send_list">
                    <div class="pay_m_model">
                        <div class="pay_i_m">
                            支付方式</div>
                        <div class="pay_ii_m">
                            <ul>
                                <li>
                                    <div class="check_m pay_click_model" psid="1">
                                    </div>
                                    <div class="a_list_m">
                                        支付宝支付
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </li>
                                <li>
                                    <div class="check_m pay_click_model" psid="2">
                                    </div>
                                    <div class="a_list_m">
                                        网银支付
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </li>
                                <li>
                                    <div class="check_m pay_click_model" psid="3">
                                    </div>
                                    <div class="a_list_m">
                                        货到付款
                                    </div>
                                    <div class="dj_list_m">
                                        <div class="dj_name_m">
                                            定金支付：</div>
                                        <div class="dj_style_m">
                                            <div class="dj_p_m">
                                                <span class="check_m" djid="1"></span><span class="check_t">支付宝</span> <span class="clear_f">
                                                </span>
                                            </div>
                                            <div class="dj_p_m">
                                                <span class="check_m" djid="2"></span><span class="check_t">网银</span> <span class="clear_f">
                                                </span>
                                            </div>
                                            <div class="clear_f">
                                            </div>
                                        </div>
                                        <div class="clear_f">
                                        </div>
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </li>
                                <li>
                                    <div class="check_m pay_click_model" psid="4">
                                    </div>
                                    <div class="a_list_m">
                                        余额支付
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="pay_i_m">
                            配送方式</div>
                        <div class="pay_ii_m send_click_model">
                            <ul>
                                <li>
                                    <div class="check_m" psid="1">
                                    </div>
                                    <div class="a_list_m">
                                        快递运输 运费：￥<%=new DealMvc.Core.Goods.BLL_Goods_XLY().GetGoodsFeight(shoppingcarList).ToJiaGe() %>
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </li>
                                <li>
                                    <div class="check_m" psid="2">
                                    </div>
                                    <div class="a_list_m">
                                        上门自提 运费：￥0.00
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="r_t_b">
                    <input type="button" name="name" value="确 认" class="sure_btn_c btn_order_pay_send_sure" />
                </div>
            </div>
        </div>
        <div class="h7">
        </div>
        <div class="invoiceInfo" onclick="prompt_fun('.update_m_invoies')">
            <div class="ad_left fl m_invocies_div">
                <input type="hidden" name="Finally_Invoices_ID" value="<%=m_odi.id %>" />
                <%
                    if (m_odi.IsNeedInvoices)
                    {
                %>
                发票类型：<%=m_odi.InvoicesCate%><br />
                发票抬头：<%=m_odi.Invoicesname%>
                <%
                    }
                    else
                    { 
                %>
                <span style="padding-top: 10px; float: left;">发票信息：不需要</span>
                <%
                    } %>
            </div>
            <div class="ad_right fr">
                <img src="../../App_Themes/images/20150310_28.png" />
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="RechargeLayer update_m_invoies">
            <div class="title">
                修改发票信息</div>
            <div class="form">
                <div class="invioices_list">
                    <div class="I_I_m_p">
                        <div class="p_m_t_I">
                            开具发票：</div>
                        <div class="p_m_tt_I">
                            <input type="hidden" name="xy_bxy_name" value="" />
                            <div class="child_m">
                                <span class="check_m" cc="需要"></span><span class="check_name">需要</span> <span class="clear_f">
                                </span>
                            </div>
                            <div class="child_m">
                                <span class="check_m" cc="不需要"></span><span class="check_name">不需要</span> <span class="clear_f">
                                </span>
                            </div>
                            <div class="clear_f">
                            </div>
                        </div>
                        <div class="clear_f">
                        </div>
                    </div>
                    <div class="II_I_m_p">
                        <div class="fp_info_model">
                            <span class="fp_t_m">发票类型：</span> <span class="fp_t_c">
                                <input type="text" name="f_cate" value="普通发票" readonly="readonly" class="InvoliceCateM" /></span>
                            <span class="clear_f"></span>
                        </div>
                        <div class="fp_info_model">
                            <span class="fp_t_m">发票抬头：</span> <span class="fp_t_c">
                                <input type="text" name="f_name" value="" class="InvoliceNameM" /></span> <span class="clear_f">
                                </span>
                        </div>
                    </div>
                </div>
                <div class="r_t_b">
                    <input type="button" name="name" value="确 认" class="sure_btn_c btn_order_invoices_sure" />
                </div>
            </div>
        </div>
        <div class="h7">
        </div>
        <div class="h60px">
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%
        //购物车列表
        List<ShoppingCar> shoppingcarList = (List<ShoppingCar>)ViewData["ShoppingCarList"];
        double? FeightAllMoney = new DealMvc.Core.Goods.BLL_Goods_XLY().GetGoodsFeight(shoppingcarList).ToDouble2();
        double? AllGoodsMoney = 0;//总商品金额
        foreach (var item in shoppingcarList)
        {
            double? SMALL_Money = (item.SC_Count * item.m_Goods.G_Price).ToDouble2();
            AllGoodsMoney += SMALL_Money;
        }
    %>
    <div class="CarResult BPBottom">
        <div class="allcount fl" style="margin-left: 3%;">
            总计：￥<span class="w_all_price"><%=(AllGoodsMoney+FeightAllMoney).ToDouble2().ToJiaGe() %></span></div>
        <div class="btn fr">
            <div class="btn_submit btn_submit_order" style="width: auto; margin-right: 7px;">
                提交订单</div>
        </div>
        <span class="clear_f"></span>
    </div>
</asp:Content>
