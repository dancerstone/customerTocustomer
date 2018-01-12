<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Goods_XLY>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/jcweb/product.js" type="text/javascript"></script>
    <%
        SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
         %>
    <script type="text/javascript">
        $(function () {

            window.onload = function () {
                $(".d_2 img").each(function (i, v) {
                    var cc = $(v).attr("src");
                    $(v).attr("src", '<%=m_site.WebAddress %>' + cc)
                });
            }


        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            <%=Model.G_Title.JSubString(6*2,"") %>
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
        <%
            SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
            Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        %>
        <div class="p_i">
            <img src="<%=m_site.WebAddress+ Model.G_MainPic.JGetOnePic() %>" />
        </div>
        <div class="p_ii">
            <div class="p_t">
                <%=Model.G_Title %></div>
            <div class="p_p">
                ￥<%=Model.G_Price.ToJiaGe() %></div>
            <div class="p_car addCarBtn" login="<%=m_login.IsNull ? "false" : "true" %>">
                <span class="fl gwc">
                    <img src="../../App_Themes/images/p_21.png" /></span> <span class="fl">加入购物车</span>
                <span class="clear_f"></span>
            </div>
        </div>
        <input type="hidden" name="Goods_UID_By" value="<%=Model.G_UID %>" />
        <div class="p_iii">
            <div class="d_1">
                产品介绍</div>
            <div class="d_2">
                <%=Model.G_OnePic %>
                <%=Model.G_TwoPic %>
                <%=Model.G_ThreePic %>
            </div>
        </div>
    </div>
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
