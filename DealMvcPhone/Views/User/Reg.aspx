<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            注册</h1>
        <a class="btnL">
            <img src="/App_Themes/images/img_home.png" /></a> <a class="btnR">
                <img src="/App_Themes/images/img_car.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Login_Content">
        <div class="LC_Head">
            <h1>
                物禅美学会员注册</h1>
            <span>注册物禅美学网，体验美学产品、美学理念、美学文化</span>
        </div>
        <div class="LC_Content">
            <form method="post">
            <ul>
                <li>
                    <input type="text" name="M_UserName" placeholder="请输入邮箱地址或手机号码" /></li>
                <li>
                    <input type="password" name="M_Pwd" placeholder="请输入密码" /></li>
                <li>
                    <input type="password" name="M_PwdAgain" placeholder="请重新输入密码" /></li>
                <li class="regli mb35">
                    <input type="text" name="WebCode" placeholder="请输入验证码" />
                    <img src="/VerifyCodeImage.ashx" onclick="getNewCode(this)" title="单击刷新验证码" alt="单击刷新验证码"
                        class="reg_webcode" />
                    <script>
                        function getNewCode(obj) {
                            obj.src = "/VerifyCodeImage.ashx?q=" + Math.random();
                        }
                    </script>
                    <span class="clear_f"></span></li>
                <li>
                    <%=Html.Hidden("NT") %>
                    <input type="submit" class="btn_submit" value="立即注册" /></li>
                <li class="otherli_reg">我已经注册，现在就&nbsp;<a href="<%=Url.Action("Login","User") %>">登录</a></li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
