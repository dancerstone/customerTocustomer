<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>项目网站后台管理系统（Website background management system）</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <script src="/JS/jquery-1.5.1.min.js" type="text/javascript"></script>
    <link href="/App_Themes/Cms/PT/login.css" rel="stylesheet" type="text/css" />
    <script src="/JS/sM.js" type="text/javascript"></script>
    <link href="/App_Themes/Public/sM.css" rel="stylesheet" type="text/css" />
    <script src="/JS/JVerify.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#Login").height($(window).height());
            $(".Box").css("padding-top", ($(window).height() - 350) / 2);
            if (top.location.href != this.location.href)
                top.location.href = this.location.href;
            $(".inputuserClass").focus();

        });
    </script>
</head>
<body>
    <div id="Info" style="width: 380px; height: 190px; display: none; overflow: hidden;">
    </div>
    <div id="Login">
        <div class="Box">
            <div class="Form">
                <form method="post" class="form" id="frmlogin">
                <ul>
                    <li><font>用户名</font>
                        <%=Html.TextBox("username", "", new { @class = "req notips inputuserClass", autocomplete = "off", maxlength = "18", showclass = "msg_username" })%>
                        <span class="msg_username msg_span"></span></li>
                    <li><font>密 码</font>
                        <%=Html.Password("userpwd", "", new { @class = "req notips", maxlength = "18", showclass = "msg_userpwd" })%>
                        <span class="msg_userpwd msg_span"></span></li>
                    <li><font>验证码</font>
                        <%=Html.TextBox("usercode", "", new { @class = "notips", style = "width:60px;", autocomplete = "off", maxlength = "4", showclass = "msg_usercode" })%>
                        <img width="80" height="27" src="/VerifyCodeImage.ashx" onclick="getNewCode(this)" title="单击刷新验证码" alt="单击刷新验证码" style="cursor: pointer;" />
                        <script>
                            function getNewCode(obj) {
                                obj.src = "/VerifyCodeImage.ashx?q=" + Math.random();
                            }
                        </script>
                        <span class="msg_usercode msg_span"></span></li>
                    <li>
                        <input type="submit" value=" 登 录 " class="submit" style="cursor: pointer;" />
                        <input type="reset" value="  " class="exit" style="cursor: pointer;" />
                    </li>
                </ul>
                </form>
            </div>
        </div>
    </div>
</body>
<Jessica:SFrom ID="SFrom1" runat="server" close="false" />
</html>
<%Html.RenderPartial("JSBottom"); %>
