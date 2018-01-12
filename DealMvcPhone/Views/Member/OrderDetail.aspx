<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Orders>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Member.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            订单详情</h1>
        <a class="btnL" href="javascript:history.go(-1);">
            <img src="/App_Themes/images/img_goback.png" /></a> <a class="btnR" href="<%=Url.Action("Car","Main") %>">
                <img src="/App_Themes/images/img_car.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sureOrder">
        <div class="h15">
        </div>
        <%
            SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
            List<OrderGoodsList> OG_LIST = OrderGoodsList.GetModelList(t => t.Order_UID == Model.O_UID).List;
            foreach (var item in OG_LIST)
            {
                double? SMALL_Money = (item.OG_GoodsCount * item.Goods.G_Price).ToDouble2();
        %>
        <div class="goodsInfo">
            <div class="img fl">
                <a href="<%=Url.Action("productdetail","Main",new{gid=item.Goods.id}) %>">
                    <img src="<%=m_site.WebAddress+item.Goods.G_MainPic.JGetOnePic() %>" /></a>
            </div>
            <div class="name fl">
                <a href="<%=Url.Action("productdetail","Main",new{gid=item.Goods.id}) %>">
                    <%=item.Goods.G_Title%></a>
            </div>
            <div class="price fl">
                ￥<%=SMALL_Money%>
                <span>X
                    <%=item.OG_GoodsCount%></span>
            </div>
            <span class="clear_f"></span>
        </div>
        <%} %>
        <div class="h7">
        </div>
        <div class="addressInfo ">
            <div class="ad_left fl m_address_div">
                <div class="top">
                    <div class="name fl">
                        <%=Model.O_ReceiptName%></div>
                    <div class="phone fl">
                        <%=Model.O_ReceiptPhone%></div>
                    <span class="clear_f"></span>
                </div>
                <div class="bottom">
                    <%=Model.O_ReceiptProvince+" "+Model.O_ReceiptCity+" "+Model.O_ReceiptArea+" "+Model.O_ReceiptDetail %>
                </div>
            </div>
            <div class="ad_right fr">
                <%-- <img src="../../App_Themes/images/20150310_28.png" />--%>
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="h7">
        </div>
        <div class="payStyle">
            <div class="ad_left fl m_pay_send_div">
                <div class="top">
                    <div class="name">
                        支付及配送</div>
                </div>
                <div class="bottom">
                    <%=Model.B %>/
                    <%=Model.A=="2"?"上门自提 运费：￥0.00" : "快递运输 运费：￥"+Model.O_GoodsFreight.ToJiaGe() %>
                </div>
            </div>
            <div class="ad_right fr">
                <%-- <img src="../../App_Themes/images/20150310_28.png" />--%>
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="h7">
        </div>
        <div class="invoiceInfo">
            <div class="ad_left fl m_invocies_div">
                <%
                    if (string.IsNullOrEmpty(Model.O_InvoicesName) || string.IsNullOrEmpty(Model.O_InvoicesCate))
                    { 
                %>
                <span style="float: left;">发票信息：不需要</span>
                <%
                    }
                    else
                    { 
                %>
                发票类型：<%=Model.O_InvoicesCate %><br />
                发票抬头：<%=Model.O_InvoicesName %>
                <%
                    }
                %>
            </div>
            <div class="ad_right fr">
                <%--<img src="../../App_Themes/images/20150310_28.png" />--%>
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="h7">
        </div>
        <%   if (Model.O_IsDelivery == true && !string.IsNullOrEmpty(Model.O_LogisticsCompany) && !string.IsNullOrEmpty(Model.O_LogisticsNumber))
             {  %>
        <div class="invoiceInfo">
            <div class="ad_left fl m_invocies_div">
                物流公司：<%=Model.O_LogisticsCompany %><br />
                物流编号：<%=Model.O_LogisticsNumber %>
            </div>
            <div class="ad_right fr">
                <%--<img src="../../App_Themes/images/20150310_28.png" />--%>
            </div>
            <span class="clear_f"></span>
        </div>
        <div class="h7">
        </div>
        <%} %>
        <div class="h60px">
        </div>
    </div>
    <div class="CarResult BPBottom" style="height: auto; position: inherit; background-color: #2b2b2b;">
        <div class="btn fr" style="width: 100%; margin-top: 0px; margin-right: 5%; text-align: right;">
            <span>
                <%=OG_LIST.Count %></span>件商品，总商品金额：</em>￥<%=Model.O_GoodsAllMoney.ToJiaGe() %><br />
            <em>运费：</em>￥<%=Model.O_GoodsFreight.ToJiaGe() %><br />
            总计：￥<span class="w_all_price"><%=Model.O_OrderAllMoney.ToJiaGe() %></span>
            <%
                if (Model.O_PayStyle == 3 && Model.O_IsPay == true)
                {
            %>
            <em>已支付定金：</em>￥<%=(Model.O_PayMoney * 0.3).ToJiaGe()%>
            <%
                }
            %>
        </div>
        <span class="clear_f"></span>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%
        List<OrderGoodsList> OG_LIST = OrderGoodsList.GetModelList(t => t.Order_UID == Model.O_UID).List;
    %>
</asp:Content>
