<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        DeleteUrl = '<%=Url.Action("DeleteAdminSort")%>'; 
    </script>
    <script>
        myColumns.push(new Array("0", "id", "id"));
        myColumns.push(new Array("1", "AdminSortName", "账户类型名称"));
        myColumns.push(new Array("0", "AdminSortPowerValues", "账户类型权限"));
        myColumns.push(new Array("1", "OrderNum", "排序"));
        myColumns.push(new Array("1", "UpTime", "时间"));
    </script>
    <% Pager<AdminSort> _Pager = (Pager<AdminSort>)ViewData["Pager"]; %>
    <div class="peditHTML">
        <div class="editHTML">
        </div>
    </div>
    <table class="table1" style="display: none;">
        <tr>
            <th class="editHtmlbtn" title="自定义" style="width: 30px; cursor: pointer;">
            </th>
            <th style="width: 30px;">
                编号
            </th>
            <th class="C_olumn id">
                id
            </th>
            <th class="C_olumn AdminSortName">
                账户类型名称
            </th>
            <th class="C_olumn AdminSortPowerValues">
                账户类型权限
            </th>
            <th class="C_olumn OrderNum">
                排序
            </th>
            <th class="C_olumn UpTime">
                时间
            </th>
            <th style="width: 100px;">
                操作
            </th>
        </tr>
        <%int i = 0; foreach (AdminSort _AdminSort in _Pager.DataList)
          {
              i++;%><tr cid='<%=_AdminSort.id %>'>
                  <td>
                      <input type="checkbox" name="ids" value='<%=_AdminSort.id %>' />
                  </td>
                  <td>
                      <%=(_Pager.PageIndex-1)*_Pager.PageSize + i %>
                  </td>
                  <td class="C_olumn id" title="<%=_AdminSort.id%>">
                      <%=_AdminSort.id.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminSortName" title="<%=_AdminSort.AdminSortName%>">
                      <%=_AdminSort.AdminSortName.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminSortPowerValues" title="<%=_AdminSort.AdminSortPowerValues%>">
                      <%=_AdminSort.AdminSortPowerValues.JSubString(30)%>
                  </td>
                  <td class="C_olumn OrderNum" title="<%=_AdminSort.OrderNum%>">
                      <%=_AdminSort.OrderNum.JSubString(30)%>
                  </td>
                  <td class="C_olumn UpTime" title="<%=_AdminSort.UpTime%>">
                      <%=_AdminSort.UpTime.JSubString(30)%>
                  </td>
                  <td>
                      <a onclick='top.AddLabel("编辑账户类型","<%=Url.Action("AEAdminSort", new { id = _AdminSort.id })%>", location.href )'>编辑</a> <a onclick="DeleteId('<%=_AdminSort.id %>')">删除</a>
                  </td>
              </tr>
        <%} %>
        <%if (_Pager.DataList.Count == 0)
          { %><tr>
              <td colspan="25">
                  暂时没有内容... ...
              </td>
          </tr>
        <%} %><tr>
            <td colspan="25">
                <input type="hidden" name="Page" value='<%=_Pager.PageIndex %>' /><a id="SelectAllIds">全选</a>&nbsp;&nbsp;&nbsp;<a onclick="DeleteSelectAllIds(this)">删除选中</a>
            </td>
        </tr>
    </table>
    <div class="pagerhtml" style="padding-bottom: 10px;">
        <%=_Pager.PagerHTML(Page) %></div>
</asp:Content>
