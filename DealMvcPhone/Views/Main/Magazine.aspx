<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            电子杂志
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
    <div class="Magazine">
        <div class="m_title">
            欢迎订阅物禅美学微信公众号
        </div>
        <div class="m_content">
            <img src="/App_Themes/images/img_twocode.png" />
        </div>
        <div class="m_foot">
            ↑长按识别二维码关注↑
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
