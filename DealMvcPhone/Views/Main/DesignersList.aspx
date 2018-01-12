<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            设计师专区
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
    <div class="PhoneWal">
        <div class="d_w_t">
            <ul>
                <%
                    SiteInfo m_site = SiteInfo.GetModel(t => t.id != null);
                    List<Designers_XLY> d_list = (List<Designers_XLY>)ViewData["d_list"];
                    foreach (Designers_XLY item in d_list)
                    {
                        string AddressImg = m_site.WebAddress + item.M_BigPic;
                %>
                <li>
                    <p>
                        <%=item.M_DesignerName %>
                        <br />
                        <%=item.M_SimpleRemarks %>
                    </p>
                    <img src="<%=AddressImg %>" />
                </li>
                <%
}
                %>
            </ul>
        </div>
    </div>
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
