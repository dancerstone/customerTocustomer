<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage<BusinessCircle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--功能说明-->
    <table class="table1">
        <tr>
            <td>
                <div class="PageTitle">
                    <%=(bool)ViewData["isEdit"]?"编辑":"新增"%>商圈</div>
                <div class="PageDetail">
                    商圈说明信息</div>
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
                名称
            </td>
            <td>
                <input type="text" name="C_Title" class="req noJ" />
                <!--<span class="InputDetail">{名称}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                所属地区
            </td>
            <td>
                <%=new DealMvc.Core.Base.BLL_SiteCity().GetCityInt("SiteCityID1", "SiteCityID2", "SiteCityID3", Model.SiteCityID1, Model.SiteCityID2, Model.SiteCityID3)%>
                <script type="text/javascript">
                    $(function () {
                        GetSiteCityList.GetCityListInt('SiteCityID1', 'SiteCityID2', 'SiteCityID3', 1, '');
                    });
                </script>
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
</asp:Content>
