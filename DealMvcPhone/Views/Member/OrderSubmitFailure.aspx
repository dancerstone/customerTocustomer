<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            下单失败
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
    <div class="PhoneWalOrder">
        <div class="success_order">
            <div class="s_i_m">
                <img src="../../App_Themes/images/nimg128_no.png" />
            </div>
            <div class="s_iii_m">
                <div class="">
                    订单提交时出现错误：</div>
                <div class="">
                    1.选择的商品不存在/已下架停止销售<br />
                    2.非法改变订单参数以及非法攻击<br />
                </div>
                <div class="clear_f">
                </div>
            </div>
            <div class="h60px"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
