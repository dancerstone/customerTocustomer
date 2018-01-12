<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" class="form">
    <table class="table1">
        <tr>
            <th align="right" style="width: 120px; overflow: hidden;"></th>
            <th></th>
        </tr>
        <tr>
            <td align="right">支付宝帐户 </td>
            <td>
                <input type="hidden" name="ApiType" value="支付宝" />
                <input type="text" name="Account" class="req" />
            </td>
        </tr>
        <tr>
            <td align="right">合作者身份ID </td>
            <td>
                <input type="text" name="AppIdentity" class="req" />
                {合作者身份(Partner ID)} </td>
        </tr>
        <tr>
            <td align="right">交易安全校验码 </td>
            <td>
                <input type="text" name="AppKey" class="req" />
                {安全校验码(Key)} </td>
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
