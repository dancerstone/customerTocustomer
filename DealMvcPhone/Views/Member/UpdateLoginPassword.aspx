<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Member.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            修改登录密码</h1>
        <a class="btnL" href="<%=Url.Action("Index","Member") %>">
            <img src="/App_Themes/images/img_goback.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="UpdateLoginPwdContent">
        <form method="post" class="UpdatePwdSubmit">
        <ul>
            <li>
                <input class="input1 input_pwd OldPwd" type="password" maxlength="20" name="OldPwd"
                    placeholder="请输入旧密码" />
            </li>
            <li>
                <input class="input1 input_pwd NewPwd" type="password" maxlength="20" name="NewPwd"
                    placeholder="请输入新密码" />
            </li>
            <li>
                <input class="input1 input_pwd NewPwdAgin" type="password" maxlength="20" name="NewPwdAgin"
                    placeholder="重复新密码" />
            </li>
        </ul>
        <div class="btnDiv" style="">
            <input type="submit" class="pageBtn1 btn_submit" value="提　　交" /></div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
