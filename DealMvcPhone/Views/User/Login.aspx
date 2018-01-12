<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            登录</h1>
        <a class="btnL" href="<%=Url.Action("Index","Main") %>">
            <img src="/App_Themes/images/img_home.png" /></a> <a class="btnR" href="<%=Url.Action("Car","Main") %>">
                <img src="/App_Themes/images/img_car.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post">
    <div class="Login_Content">
        <div class="LC_Head">
            <h1>
                物禅美学会员登录</h1>
            <span>登录物禅美学网，体验美学产品、美学理念、美学文化</span>
        </div>
        <div class="LC_Content">
            <ul>
                <li>
                    <input type="text" name="M_UserName" placeholder="请输入邮箱地址或手机号码" /></li>
                <li class="mb35">
                    <input type="password" name="M_Pwd" placeholder="请输入密码" /></li>
                <li>
                    <input type="submit" class="btn_submit" name="" value="立即登录" /></li>
                <li class="otherli"><a>其他方式登录</a>|<a href="<%=Url.Action("FindPassword","User") %>">忘记密码？</a></li>
                <li><a href="<%=Url.Action("Reg","User") %>" class="regbuttom">还不是物禅美学用户，立即注册</a>
                </li>
            </ul>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
