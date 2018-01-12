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
        string K = ViewData["K"].ToString2();
        string Z = ViewData["Z"].ToString2();
    %>
    <div class="fp_content">
        <div class="wal login LC_Content">
            <!--wal-->
            <div class="form">
                <form method="post" class="submit_info">
                <%=Html.Hidden("K", K)%>
                <%=Html.Hidden("Z", Z)%>
                <ul>
                    <li>
                        <input class="in1 NewPwd input1" name="NewPwd" type="password" maxlength="20" placeholder="请输入新密码" /></li>
                    <li>
                        <input class="in1 NewPwdAgin input1" name="NewPwdAgin" type="password" placeholder="再次输入新密码"
                            maxlength="20" /></li>
                    <li>
                        <div class="btnDiv">
                            <input type="submit" class="btn_next_phone buttn_in" value="下一步" /></div>
                    </li>
                </ul>
                </form>
            </div>
            <!--walEnd-->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
