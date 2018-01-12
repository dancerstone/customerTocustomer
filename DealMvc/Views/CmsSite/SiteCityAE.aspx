<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage<SiteCity>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--功能说明-->
    <table class="table1">
        <tr>
            <td>
                <div class="PageTitle">
                    <%=(bool)ViewData["isEdit"]?"编辑":"新增"%>城市</div>
                <div class="PageDetail">
                    城市说明信息</div>
            </td>
        </tr>
    </table>
    <!--表单信息-->
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
                级别
            </td>
            <td>
                <select name="C_Level" class="Sreq">
                    <option value="">请选择</option>
                    <option value="1">省</option>
                    <option value="2">市</option>
                    <option value="3">区</option>
                </select>
                <!--<span class="InputDetail">{级别}</span>-->
            </td>
        </tr>
        <tr class="hide topCate">
            <td align="right">
                上级分类
            </td>
            <td>
                <%
                    List<SiteCity> sitecity_sheng = SiteCity.GetModelList(t => t.C_Level == 1).List;
                %>
                <select name="sheng" class="hide" id="sheng" level="2">
                    <option>请选择</option>
                    <%
                        foreach (SiteCity item in sitecity_sheng)
                        {
                    %>
                    <option value="<%=item.id %>">
                        <%=item.C_Title %></option>
                    <%
                        }
                    %>
                </select>
                <select name="shi" class="hide" id="shi">
                    <option>请选择</option>
                </select>
            </td>
        </tr>
        <tr>
            <td align="right">
                名称
            </td>
            <td>
                <input type="text" name="C_Title" class="req noJ" />
                <!--<span class="InputDetail">{名称}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                排序字段
            </td>
            <td>
                <input type="text" name="Sort" class="req  int noJ w80px" value="0" />
                <!--<span class="InputDetail">{排序字段}</span>-->
            </td>
        </tr>
        <%if ((bool)ViewData["isEdit"])
          { %>
        <tr>
            <td align="right">
                添加时间
            </td>
            <td>
                <%=Model.Time %>
            </td>
        </tr>
        <%} %>
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
    <script type="text/javascript">
        $(function () {
            $("select[name='C_Level']").change(function () {
                var level = $(this).val();
                if (level == 1) {
                    $(".topCate").hide();
                    $("select[name='sheng']").hide();
                    $("select[name='shi']").hide();
                }
                else if (level == 2) {
                    $(".topCate").show();
                    $("select[name='sheng']").show();
                    $("select[name='shi']").hide();
                } else if (level == 3) {
                    $(".topCate").show();
                    $("select[name='sheng']").show();
                    $("select[name='shi']").show();
                }
            });
            //省发生变化时，市发生变化
            $("select[name='sheng']").change(function () {
                $.ajax({
                    url: '/Comm/GetNextCitys',
                    data: { topid: $(this).val(), level: $(this).attr("level"), cate: 1, r: new Date().getTime() },
                    type: 'POST',
                    async: false,
                    success: function (msg) {
                        $("select[name='shi']").html(msg);
                    }
                });
            });
        });
        window.onload = function () {
            var isEdit = '<%=(bool)ViewData["isEdit"] %>';
            if (isEdit == true || isEdit == 'True') {
                var levle_c = '<%=Model.C_Level %>';
                var sheng_2 = '<%=Model.CitysTopID %>';
                var sheng_3 = '<%=Model.SiteCityParent.CitysTopID %>';
                var shi_3 = '<%=Model.CitysTopID %>';
                if (levle_c == 1) {
                    $(".topCate").hide();
                    $("#sheng").hide();
                    $("#shi").hide();
                }
                else if (levle_c == 2) {
                    $(".topCate").show();
                    $("#sheng").val(parseInt(sheng)).show();
                    $("#shi").hide();
                } else if (levle_c == 3) {
                    $(".topCate").show();
                    $("#sheng").val(parseInt(sheng_3)).show().change();
                    $("#shi").val(parseInt(shi_3)).show();
                }
            }
        }
    </script>
</asp:Content>
