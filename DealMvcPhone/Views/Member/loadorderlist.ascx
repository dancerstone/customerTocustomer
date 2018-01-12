<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Pager<Orders> _Pager = (Pager<Orders>)ViewData["Pager"];
    Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
    int? cb = 0;
    foreach (var item in _Pager.DataList)
    {
        cb++;
%>
<li>
    <div class="title">
        <span class="status fl">
            <%=item.O_Status %>
        </span><span class="fr orderNum">订单号：<%=item.O_UID %><span class="deleteorder_model"><img
            src="/App_Themes/images/img_order_del.png" /></span></span> <span class="clear_f">
        </span>
    </div>
    <div class="logistics">
        <%
            if (item.O_Status == "未付款")
            {
        %>
        <div class="btn_order_model">
            <a href="<%=Url.Action("OrderSubmitSuccess","Member",new{Order_UID=item.O_UID}) %>">
                立即支付</a></div>
        <div class="btn_order_model">
            <a href="#">取消订单</a></div>
        <%} %>
        <div class="clear_f">
        </div>
    </div>
    <div class="content">
        <div class="top">
            <div class="big_top fl">
                <%
List<OrderGoodsList> OG_LIST = OrderGoodsList.GetModelList(t => t.Order_UID == item.O_UID).List;
int? cc = 0;
foreach (var og in OG_LIST)
{
    cc++;
                %>
                <div class="good_model_m">
                    <div class="img fl">
                        <a href="<%=Url.Action("productdetail","main",new{gid=og.Goods.id}) %>">
                            <img src="/App_Themes/images/img_car_img1.png" /></a>
                    </div>
                    <div class="right fl">
                        <div class="title">
                            <a href="#">
                                <%=og.Goods.G_Title %></a>
                        </div>
                        <div class="other">
                            <div class="color">
                                颜色：<%=og.Goods.G_Color %></div>
                            <div class="num">
                                下单时间：<%=og.OG_Time %></div>
                        </div>
                    </div>
                    <div class="clear_f">
                    </div>
                </div>
                <%} %>
            </div>
            <div class="fr bottomImg">
                <img src="/App_Themes/images/20150310_28.png" /></div>
            <span class="clear_f"></span>
        </div>
    </div>
    <div class="result">
        实付款：￥<%=item.O_OrderAllMoney.ToJiaGe()%>
    </div>
</li>
<%
    }
    if (_Pager.DataList.Count == 0)
    {
%>
<div class="m_no_model">
    暂无相关的订单信息</div>
<%
    }
%>