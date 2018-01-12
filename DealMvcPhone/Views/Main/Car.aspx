<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/jcweb/product.js" type="text/javascript"></script>
    <script src="../../JS/jcweb/form.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            购物车
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
    <div class="h15">
    </div>
    <div class="CarWal">
        <div class="CarList">
            <form action="<%=Url.Action("OrderIndex","Member") %>" method="post" class="order_submit_form">
            <ul>
                <%
                    SiteInfo m_site = SiteInfo.GetModel(t => t.id != 0);
                    Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
                    //获取购物车列表
                    List<ShoppingCar> ShoppingCarList = (List<ShoppingCar>)ViewData["XShop_ShoppingCarList"];
                    if (ShoppingCarList.Count > 0)
                    {
                        int? cc = 0;
                        foreach (var item in ShoppingCarList)
                        {
                            cc++;
                %>
                <li>
                    <input type="checkbox" name="goods_id_btn" p_p="<%=(item.m_Goods.G_Price*item.SC_Count).ToJiaGe()%>"
                        value="<%=item.SC_UniqueID %>" checked="checked" style="display: none;" />
                    <div class="li_left fl checkin">
                    </div>
                    <div class="li_right fr">
                        <div class="top">
                            <div class="img fl">
                                <a href="<%=Url.Action("productdetail","Main",new{gid=item.m_Goods.id}) %>">
                                    <img src="<%=m_site.WebAddress+item.m_Goods.G_MainPic.JGetOnePic() %>" /></a>
                            </div>
                            <div class="right fr">
                                <div class="title">
                                    <a href="<%=Url.Action("productdetail","Main",new{gid=item.m_Goods.id}) %>">
                                        <%=item.m_Goods.G_Title%></a>
                                </div>
                                <div class="other">
                                    <div class="color">
                                        颜色：<%=item.m_Goods.G_Color%></div>
                                    <div class="num">
                                        数量：<span class="now_num"><%=item.SC_Count %></span>
                                        <span onclick="prompt_fun('.updatebuynum<%=cc %>')">
                                            <img src="/App_Themes/images/img_car_edit.png" /></span>
                                    </div>
                                </div>
                            </div>
                            <span class="clear_f"></span>
                        </div>
                        <div class="bottom">
                            小计：￥<%=(item.m_Goods.G_Price*item.SC_Count).ToJiaGe()%>
                        </div>
                    </div>
                    <span class="clear_f"></span>
                    <div class="RechargeLayer updatebuynum<%=cc %>">
                        <div class="title">
                            修改数量</div>
                        <div class="form">
                            <div class="r_t_i">
                                <div class="ti_tt">
                                    数量：</div>
                                <div class="ti_tc">
                                    <span class="cut_b"></span><span class="input_b"><input type="text" name="name"
                                        value="<%=item.SC_Count %>" class="car_input canint buynumber" car_uid="<%=item.SC_UniqueID %>" d_p="<%=item.m_Goods.G_Price %>" /></span><span class="add_b"></span>
                                    <span class="clear_f"></span>
                                </div>
                                <div class="clear_f">
                                </div>
                            </div>
                            <div class="r_t_b">
                                <input type="button" name="name" value="确 认" class="sure_btn_c btn_car_sure" />
                            </div>
                        </div>
                    </div>
                </li>
                <%
}
                    }
                    else
                    { 
                %>
                <li class="KLI"><span class="K_I_V">
                    <img alt="" src="<%=m_site.WebAddress%>/App_Themes/UI/image/car_null.png"></span><br />
                    <span class="K_I_T">购物车什么产品也没有呀!</span> </li>
                <%
}
                %>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%
        //获取购物车列表
        List<ShoppingCar> ShoppingCarList = (List<ShoppingCar>)ViewData["XShop_ShoppingCarList"];
        if (ShoppingCarList.Count > 0)
        {
            double? p_all_price = 0;
            foreach (var item in ShoppingCarList)
            {
                p_all_price += item.m_Goods.G_Price * item.SC_Count;
            }
            
    %>
    <div class="CarResult BPBottom">
        <input type="checkbox" name="all_qx_btn" value="" class="all_qx_btn" checked="checked"
            style="display: none;" />
        <div class="img fl allin_check">
        </div>
        <div class="allcount fl">
            总计：￥<span class="w_all_price"><%=p_all_price %></span></div>
        <div class="btn fr">
            <div class="btn_submit btn_submit_car">
                结算<span class="h_aa_int">(<%=ShoppingCarList.Count%>)</span></div>
        </div>
        <span class="clear_f"></span>
    </div>
    <%} %>
    <%--<%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>--%>
</asp:Content>
