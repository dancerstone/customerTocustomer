<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Address.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            管理收货地址</h1>
        <a class="btnL" href="<%=Url.Action("Index","Member") %>">
            <img src="/App_Themes/images/img_goback.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        List<DeliveryAddress> da_list = DeliveryAddress.GetModelList(5, t => t.DA_MemberUID == m_login.M_UID, t => t.DA_Time.lb_Desc()).List;
        DeliveryAddress da_model_default = DeliveryAddress.GetModel(t => t.DA_MemberUID == m_login.M_UID && t.DA_IsDefault == true);
    %>
    <div class="adWal">
        <div class="btn">
            <div class="addNew">
                <a href="<%=Url.Action("AEDeliveryAddress","Member") %>">
                    <img src="/App_Themes/images/img_ad_bg.png" />新建收货地址</a></div>
        </div>
        <div class="adList">
            <ul>
                <li>
                    <div class="ad_left fl">
                        <div class="top">
                            <div class="name fl">
                                <%=da_model_default.DA_ConsigneeName%></div>
                            <div class="phone fl">
                                <%=da_model_default.DA_Phone%></div>
                            <span class="clear_f"></span>
                        </div>
                        <div class="bottom">
                            <%=da_model_default.DA_DetailAddress%>
                        </div>
                    </div>
                    <div class="ad_right fr">
                        <div class="fl">
                            <a href="<%=Url.Action("DefaultOperation","Member",new{ da_id=da_model_default.id }) %>">
                                <img src="/App_Themes/images/img_location.png" /></a>
                        </div>
                        <div class="fr">
                            <a href="<%=Url.Action("AEDeliveryAddress","Member",new{id=da_model_default.id}) %>">
                                <img src="/App_Themes/images/img_ad_edit.png" /></a>
                        </div>
                        <span class="clear_f"></span>
                    </div>
                    <span class="clear_f"></span></li>
                <%
                    foreach (DeliveryAddress item in da_list)
                    {
                        if (item.id == da_model_default.id) continue;
                %>
                <li>
                    <div class="ad_left fl">
                        <div class="top">
                            <div class="name fl">
                                <%=item.DA_ConsigneeName %></div>
                            <div class="phone fl">
                                <%=item.DA_Phone %></div>
                            <span class="clear_f"></span>
                        </div>
                        <div class="bottom">
                            <%=item.DA_DetailAddress %>
                        </div>
                    </div>
                    <div class="ad_right fr">
                        <div class="fl">
                            <a href="<%=Url.Action("DefaultOperation","Member",new{ da_id=item.id }) %>">
                                <img src="/App_Themes/images/img_location2.png" /></a>
                        </div>
                        <div class="fr">
                            <a href="<%=Url.Action("AEDeliveryAddress","Member",new{ id=item.id }) %>">
                                <img src="/App_Themes/images/img_ad_edit.png" />
                            </a>
                        </div>
                        <span class="clear_f"></span>
                    </div>
                    <span class="clear_f"></span></li>
                <%
                    } %>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
