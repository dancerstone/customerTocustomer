<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage<PointsSet>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--表单信息-->
    <form method="post" class="form">
    <table class="table1">
        <tr>
            <th align='right' style='width: 220px; overflow: hidden;'>
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td align="right">
                完成订单每消费1元钱即可获得
            </td>
            <td>
                <%=Html.Hidden("id",Model.id) %>
                <input type="text" name="PS_ConsumerPoints" class="req  float noJ w80px" value="<%=Model.PS_ConsumerPoints %>" />积分 <span class="InputDetail">
                    { 如：设置0则代表用户不能获取积分 }</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                注册成为会员即可获得
            </td>
            <td>
                <input type="text" name="PS_RegPoints" class="req  int noJ w80px" value="<%=Model.PS_RegPoints %>" />积分
                <!--<span class="InputDetail">{完成注册送积分}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                验证手机赠送积分
            </td>
            <td>
                <input type="text" name="PS_PhoneVerifyPoints" class="req  int noJ w80px" value="<%=Model.PS_PhoneVerifyPoints %>"  />积分
                <!--<span class="InputDetail">{验证手机送积分}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                验证邮箱赠送积分
            </td>
            <td>
                <input type="text" name="PS_EmailVerifyPoints" class="req  int noJ w80px" value="<%=Model.PS_EmailVerifyPoints %>"  />积分
                <!--<span class="InputDetail">{验证邮箱送积分}</span>-->
            </td>
            <tr>
                <td>
                </td>
                <td>
                    <input type="submit" value="修 改" />
                </td>
            </tr>
    </table>
    </form>
</asp:Content>
