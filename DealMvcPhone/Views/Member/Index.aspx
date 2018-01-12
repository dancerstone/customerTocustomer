<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Member.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            会员中心</h1>
        <a class="btnL" href="<%=Url.Action("Index","Main") %>">
            <img src="/App_Themes/images/img_home.png" /></a> <a class="btnR">
                <img src="/App_Themes/images/img_car.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();//当前登录会员
        SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
    %>
    <div class="MemberCover">
        <ul>
            <li class="headpic"><a href="<%=Url.Action("MemberInfo","Member") %>">
                <img width="105" height="105" src="<%=m_login.M_IsPhoneHead ? m_login.M_Avatar.JEP() : (m_site.WebAddress + m_login.M_Avatar).JEP() %>" /></a></li>
            <li class="mName"><a href="<%=Url.Action("MemberInfo","Member") %>">
                <%=m_login.M_UserName %></a></li>
        </ul>
    </div>
    <div class="h7">
    </div>
    <div class="MemberContent">
        <ul>
            <li><a href="<%=Url.Action("MyOrders","Member") %>">
                <img class="left_img" src="/App_Themes/images/20150310_05.png" />我的订单</a></li>
            <li>
                <img class="left_img" src="/App_Themes/images/20150310_08.png" />商品评价</li>
            <li>
                <img class="left_img" src="/App_Themes/images/20150310_11.png" />我的收藏</li>
            <li>
                <img class="left_img" src="/App_Themes/images/20150310_15.png" />我的积分</li>
        </ul>
        <div class="h7">
        </div>
        <ul>
            <li>
                <img class="left_img" src="/App_Themes/images/20150310_19.png" />返修退换货</li>
            <li>
                <img class="left_img" src="/App_Themes/images/20150310_22.png" />联系售后客服</li>
        </ul>
        <div class="h7">
        </div>
        <ul>
            <li><a href="<%=Url.Action("DeliveryAddressList","Member") %>">
                <img class="left_img" src="/App_Themes/images/20150310_24.png" />收货地址管理</a></li>
            <li><a href="<%=Url.Action("UpdateLoginPassword","Member") %>">
                <img class="left_img" src="/App_Themes/images/20150310_26.png" />修改密码</a></li>
        </ul>
        <div class="h7">
        </div>
        <div class="div">
            <div class="safeOut">
                <a href="<%=Url.Action("LoginOut","User") %>">安全退出</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
