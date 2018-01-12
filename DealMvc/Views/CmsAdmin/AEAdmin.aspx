<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function ()
        {
            $(".AdminSort").change(function ()
            {
                var val = $(this).val();
                if (val == "") return;
                $.ajax({
                    url: '<%=Url.Action("GetAdminPowerValues") %>',
                    data: 'sort_id=' + val,
                    type: 'POST',
                    success: function (msg)
                    {
                        $(".qx").each(function (i, v)
                        {
                            var isCheck = false;
                            if (msg.indexOf($(v).val()) >= 0)
                            { isCheck = true; }
                            $(v).attr("checked", isCheck);
                        });
                    }
                });
            });
        });
    </script>
    <form method="post" class="form">
    <table class="table1">
        <tr>
            <th align='right' style='width: 120px; overflow: hidden;'>
            </th>
            <th>
            </th>
        </tr>
        <%
            string adminid_readonly = "";
            if ((bool)ViewData["isEdit"])
            {
                adminid_readonly = "readonly='readonly'";
            }%>
        <tr>
            <td align="right">
                帐号ID
            </td>
            <td>
                <input type="text" name="AdminID" <%=adminid_readonly %> class="req" min="4" max="12" />
            </td>
        </tr>
        <%if (!(bool)ViewData["isEdit"])
          { %>
        <tr>
            <td align="right">
                帐号密码
            </td>
            <td>
                <input type="password" name="AdminPwd" class="req" min="5" max="16" />
            </td>
        </tr>
        <tr>
            <td align="right">
                帐号密码
            </td>
            <td>
                <input type="password" name="AdminPwd2" class="req" min="5" max="16" />
            </td>
        </tr>
        <%} %>
        <tr>
            <td align="right">
                真实姓名
            </td>
            <td>
                <input type="text" name="AdminRealName" max="20" class="J" />
            </td>
        </tr>
        <tr>
            <td align="right">
                性别
            </td>
            <td>
                <%--<input type="text" name="AdminSex" class="req" />--%>
                <input type="radio" name="AdminSex" value="男" checked="checked" />男
                <input type="radio" name="AdminSex" value="女" />女
            </td>
        </tr>
        <%--<tr>
            <td align="right">
                权限
            </td>
            <td>
                <input type="text" name="AdminPowerValues" class="req" />
            </td>
        </tr>--%>
        <tr>
            <td align="right">
                备注
            </td>
            <td>
                <%--<input type="text" name="AdminRemark" class="req" />--%>
                <%--<textarea name="AdminRemark" cols="60" rows="4" style="border-width: 1px;"></textarea>--%>
                <%=Html.TextArea("AdminRemark", new {cols="60", rows="4", style = "border-width: 1px;" })%>
            </td>
        </tr>
        <%if (Request["a_i_id"] == null)
          { %>
        <tr>
            <td align="right">
                排序
            </td>
            <td>
                <input type="text" name="OrderNum" class="req int" value="0" />
            </td>
        </tr>
        <tr>
            <td align="right">
                帐号类型
            </td>
            <td>
                <%--<input type="text" name="AdminSortID" class="req" />--%>
                <select name="AdminSortID" class="Sreq AdminSort">
                    <option value="">-选择帐号类型-</option>
                    <%=DealMvc.Model.Admin.GetAdminSortForSelect()%>
                </select>
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
                <% 
                foreach (KeyValuePair<string, List<DealMvc.Filter.RoleAttribute>> Key in DealMvc.Filter.RoleFactory.GetAllAction())
                   { %>
                <ul style="border: 1px solid #CCCCC2; margin: 2px 0; overflow: hidden; padding:3px 10px;">
                    <li style="height: 26px; line-height: 26px; font-weight: 700;">
                        <%= Key.Key%>：</li>
                    <li style=" line-height: 26px;">
                        <% foreach (DealMvc.Filter.RoleAttribute RA in Key.Value)
                           { %>
                        <div style="width: 225px;" class="fl">
                            <input type="checkbox" class="qx" name="AdminPowerValues" value="<%= RA.ActionUrl %>#<%= RA.Name%>" />
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
        <%}
          } %>
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
