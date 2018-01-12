<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" class="form">
    <table class="table1">
        <tr>
            <th align="right" style="width: 120px; overflow: hidden;"></th>
            <th></th>
        </tr>
        <tr>
            <td align="right">账号 </td>
            <td>
                <input type="text" name="UserName" class="req" />
            </td>
        </tr>
        <tr>
            <td align="right">密码 </td>
            <td>
                <input type="password" name="UserPwd" class="req" style="width: 280px;" />
            </td>
        </tr>
      <%--  <tr>
            <td align="right">剩余短信条数 </td>
            <td>
                <%=DealMvc..Message.GetBalance(); %>
                条 </td>
        </tr>--%>
        <tr>
            <td></td>
            <td>
                <input type="submit" value="修改" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
