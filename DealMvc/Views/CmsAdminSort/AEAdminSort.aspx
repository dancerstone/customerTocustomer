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
                账户类型名称
            </td>
            <td>
                <input type="text" name="AdminSortName" class="req" />
            </td>
        </tr>
        <%
            SiteInfo m_SiteInfo = SiteInfo.GetModel(t => t.id != 0);
            if (m_SiteInfo.WebCompetence)
            {
        %>
        <tr>
            <td align="right" valign="top">
                账户类型权限
            </td>
            <td>
                <script>
                    $(function ()
                    {
                        $(".all").click(function ()
                        {
                            $(".qx").attr("checked", true);
                        });
                        $(".allF").click(function ()
                        {
                            $(".qx").each(function (i, v)
                            {
                                if ($(v).attr("checked"))
                                    $(v).attr("checked", false);
                                else
                                    $(v).attr("checked", true);
                            });
                        });
                        $(".allEsc").click(function ()
                        {
                            $(".qx").attr("checked", false);
                        });
                    });
                </script>
                <a href="#" class="all">全选</a>&nbsp;&nbsp;<a href="#" class="allF">反选</a>&nbsp;&nbsp;<a href="#" class="allEsc">全部取消</a>
                <%--<input type="text" name="AdminSortPowerValues" class="req" />--%>
                <% foreach (KeyValuePair<string, List<DealMvc.Filter.RoleAttribute>> Key in DealMvc.Filter.RoleFactory.GetAllAction())
                   { %>
                <ul style="border: 1px solid #CCCCC2; margin: 2px 0; overflow: hidden; padding:3px 10px;">
                    <li style="height: 26px; line-height: 26px; font-weight: 700;">
                        <%= Key.Key%>：</li>
                    <li style="line-height: 26px;">
                        <% foreach (DealMvc.Filter.RoleAttribute RA in Key.Value)
                           { %>
                        <div style="width: 225px;" class="fl">
                            <input type="checkbox" class="qx" name="AdminSortPowerValues" value="<%= RA.ActionUrl %>#<%= RA.Name%>" />
                            <span>
                                <%= RA.Name%></span>
                        </div>
                        <% } %>
                        <div class="cb">
                        </div>
                    </li>
                </ul>
                <%} %>
                <a href="#" class="all">全选</a>&nbsp;&nbsp;<a href="#" class="allF">反选</a>&nbsp;&nbsp;<a href="#" class="allEsc">全部取消</a>
            </td>
        </tr>
        <%} %>
        <tr>
            <td align="right">
                排序
            </td>
            <td>
                <input type="text" name="OrderNum" class="req int" value="0" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <%if ((bool)ViewData["isEdit"])
                  { %><input type="submit" value="编辑" /><%}
                  else
                  { %><input type="submit" value="添加" /><%} %>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
