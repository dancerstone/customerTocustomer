<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        DeleteUrl = '<%=Url.Action("DeleteAdmin")%>'; 
    </script>
    <script>
        myColumns.push(new Array("0", "id", "id"));
        myColumns.push(new Array("1", "AdminID", "帐号ID"));
        myColumns.push(new Array("0", "AdminPwd", "帐号密码"));
        myColumns.push(new Array("1", "AdminRealName", "姓名"));
        myColumns.push(new Array("1", "AdminSex", "性别"));
        myColumns.push(new Array("1", "AdminSortID", "帐号类型"));
        myColumns.push(new Array("0", "AdminPowerValues", "权限"));
        myColumns.push(new Array("0", "AdminRemark", "备注"));
        myColumns.push(new Array("1", "OrderNum", "排序"));
        myColumns.push(new Array("0", "UpTime", "跟新时间"));
        myColumns.push(new Array("1", "LastLoginTime", "最近登录时间"));

        $(function ()
        {
            //reQX
            $(".reQX").click(function ()
            {
                if (IsConfirm())
                {
                    var ids = new Array();
                    $(":checkbox[name='ids']:checked").each(function (i, v)
                    { ids[i] = $(v).val(); });
                    var can = ids.join(",");
                    $.ajax({
                        url: '<%=Url.Action("ReQX","CmsAdmin") %>',
                        data: 'ids=' + can,
                        type: 'POST',
                        success: function (msg)
                        {
                            if (msg == "1")
                                $("body").showMessage("执行成功");
                            else
                                $("body").showMessage("执行失败");
                        }
                    });
                }
            });
        });

    </script>
    <% Pager<Admin> _Pager = (Pager<Admin>)ViewData["Pager"]; %>
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
            <th class="C_olumn AdminID">
                帐号ID
            </th>
            <th class="C_olumn AdminPwd">
                帐号密码
            </th>
            <th class="C_olumn AdminRealName">
                姓名
            </th>
            <th class="C_olumn AdminSex">
                性别
            </th>
            <th class="C_olumn AdminSortID">
                帐号类型
            </th>
            <th class="C_olumn AdminPowerValues">
                权限
            </th>
            <th class="C_olumn AdminRemark">
                备注
            </th>
            <th class="C_olumn OrderNum">
                排序
            </th>
            <th class="C_olumn UpTime">
                跟新时间
            </th>
            <th class="C_olumn LastLoginTime">
                最近登录时间
            </th>
            <th style="width: 140px;">
                操作
            </th>
        </tr>
        <%int i = 0; foreach (Admin _Admin in _Pager.DataList)
          {
              i++;%><tr cid='<%=_Admin.id %>'>
                  <td>
                      <input type="checkbox" name="ids" value='<%=_Admin.id %>' />
                  </td>
                  <td>
                      <%=(_Pager.PageIndex-1)*_Pager.PageSize + i %>
                  </td>
                  <td class="C_olumn id" title="<%=_Admin.id%>">
                      <%=_Admin.id.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminID" title="<%=_Admin.AdminID%>">
                      <%=_Admin.AdminID.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminPwd" title="<%=_Admin.AdminPwd%>">
                      <%=_Admin.AdminPwd.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminRealName" title="<%=_Admin.AdminRealName%>">
                      <%=_Admin.AdminRealName.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminSex" title="<%=_Admin.AdminSex%>">
                      <%=_Admin.AdminSex.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminSortID" title="<%=_Admin.GetAdminSortName()%>">
                      <%=_Admin.GetAdminSortName().JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminPowerValues" title="<%=_Admin.AdminPowerValues%>">
                      <%=_Admin.AdminPowerValues.JSubString(30)%>
                  </td>
                  <td class="C_olumn AdminRemark" title="<%=_Admin.AdminRemark%>">
                      <%=_Admin.AdminRemark.JSubString(30)%>
                  </td>
                  <td class="C_olumn OrderNum" title="<%=_Admin.OrderNum%>">
                      <%=_Admin.OrderNum.JSubString(30)%>
                  </td>
                  <td class="C_olumn UpTime" title="<%=_Admin.UpTime%>">
                      <%=_Admin.UpTime.JSubString(30)%>
                  </td>
                  <td class="C_olumn LastLoginTime" title="<%=_Admin.LastLoginTime.JHtmlEncode()%>">
                      <%=_Admin.LastLoginTime.JSubString(30)%>
                  </td>
                  <td>
                      <a onclick='top.AddLabel("修改密码","<%=Url.Action("EAdminPwd", new { a_i_id = _Admin.id })%>")'>修改密码</a> <a onclick='top.AddLabel("编辑帐号","<%=Url.Action("AEAdmin", new { id = _Admin.id })%>", location.href )'>编辑</a> <a onclick="DeleteId('<%=_Admin.id %>')">删除</a>
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
                <input type="hidden" name="Page" value='<%=_Pager.PageIndex %>' /><a id="SelectAllIds">全选</a>&nbsp;&nbsp;&nbsp;<a onclick="DeleteSelectAllIds(this)">删除选中</a>&nbsp;&nbsp;&nbsp;<a class="reQX">恢复默认权限</a>
            </td>
        </tr>
    </table>
    <div class="pagerhtml" style="padding-bottom: 10px;">
        <%=_Pager.PagerHTML(Page) %></div>
</asp:Content>
