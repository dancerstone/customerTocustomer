<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/password.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            找回密码</h1>
        <a class="btnL" href="<%=Url.Action("FindPassword","User") %>">
            <img src="/App_Themes/images/img_goback.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        string Type = ViewData["Type"].ToString2();
    %>
    <div class="fp_content">
        <div class="wal login LC_Content">
            <!--wal-->
            <div class="success_tips">
                <div>
                    <img src="/App_Themes/receptionPic/true.png" /></div>
                <div class="fl">
                    密码更改成功&nbsp;<a href="<%=Url.Action("Login","User") %>">返回登录</a></div>
            </div>
            <!--walEnd-->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
