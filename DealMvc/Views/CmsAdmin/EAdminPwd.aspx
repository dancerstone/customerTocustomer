<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" class="form">
    <table class="table1">
        <tr>
            <th align='right' style='width: 120px; overflow: hidden;'>
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td align="right">
                帐号ID
            </td>
            <td>
                <input type="text" name="AdminID" readonly="readonly" />
            </td>
        </tr>
        <tr>
            <td align="right">
                旧帐号密码
            </td>
            <td>
                <input type="password" name="AdminOldPwd" class="req" />
            </td>
        </tr>
        <tr>
            <td align="right">
                新帐号密码
            </td>
            <td>
                <input type="password" name="AdminPwd" class="req" min="5" max="16" />
            </td>
        </tr>
        <tr>
            <td align="right">
                新帐号密码
            </td>
            <td>
                <input type="password" name="AdminPwd2" class="req" min="5" max="16" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value="编辑" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
