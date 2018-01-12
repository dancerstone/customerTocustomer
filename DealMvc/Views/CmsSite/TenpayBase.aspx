<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" class="form">
    <table class="table1">
        <tr>
            <th align="right" style="width: 120px; overflow: hidden;"></th>
            <th></th>
        </tr>
        <tr>
            <td align="right">商户号 </td>
            <td>
                <input type="hidden" name="ApiType" value="财付通" />
                <input type="text" name="AppIdentity" class="req" />
            </td>
        </tr>
        <tr>
            <td align="right">密钥 </td>
            <td>
                <input type="text" name="AppKey" class="req" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <input type="submit" value="修改" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
