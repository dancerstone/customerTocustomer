<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        DeleteUrl = '<%=Url.Action("DeleteBusinessCircle")%>'; 
    </script>
    <script>
        myColumns.push(new Array("0", "id", "主键--标识ID"));
        myColumns.push(new Array("1", "C_Title", "名称"));
        myColumns.push(new Array("1", "SiteCityID1", "所属省ID"));
        myColumns.push(new Array("1", "SiteCityID2", "所属市ID"));
        myColumns.push(new Array("1", "SiteCityID3", "所属区ID"));
        myColumns.push(new Array("1", "C_Initials", "首字母"));
        myColumns.push(new Array("1", "Sort", "排序字段"));
        myColumns.push(new Array("1", "Time", "添加时间"));
        //myColumns.push(new Array("0","A", "扩展A字段"));
        //myColumns.push(new Array("0","B", "扩展B字段"));
        //myColumns.push(new Array("0","C", "扩展C字段"));
        //myColumns.push(new Array("0","D", "扩展D字段"));
        //myColumns.push(new Array("0","E", "扩展E字段"));
    </script>
    <!--功能说明-->
    <table class="table1">
        <tr>
            <td>
                <div class="PageTitle">
                    商圈列表</div>
                <div class="PageDetail">
                    商圈说明信息</div>
            </td>
        </tr>
    </table>
    <!--搜索信息-->
    <% Pager<BusinessCircle> _Pager = (Pager<BusinessCircle>)ViewData["Pager"]; %>
    <%--<form>
    <table class="table1" style="">
        <tr>
            <td>
                Title&nbsp;
                <input type="text" name="Title" value='' class='noJ w180px Ber' maxlength='50' />&nbsp;
                <input type="submit" value="搜索" />
            </td>
        </tr>
    </table>
    </form>--%>
    <div class="peditHTML">
        <div class="editHTML">
        </div>
    </div>
    <!--数据信息-->
    <table class="table1" style="display: none;">
        <tr>
            <td colspan="16">
                <a class="XshopCheckAll">全部选定</a>&nbsp;&nbsp;&nbsp;<a class="XshopReverseCheck">反向选择</a>&nbsp;&nbsp;&nbsp;<a
                    class="XshopDelCheck" onclick="DeleteSelectAllIds(this)">删除选中</a>
            </td>
        </tr>
        <tr>
            <th class="editHtmlbtn" title="自定义" style="width: 30px; cursor: pointer;">
            </th>
            <th style="width: 30px;">
                编号
            </th>
            <th class="C_olumn id">
                主键--标识ID
            </th>
            <th class="C_olumn C_Title">
                名称
            </th>
            <th class="C_olumn SiteCityID1">
                所属地区
            </th>
            <th class="C_olumn C_Initials">
                首字母
            </th>
            <th class="C_olumn Sort">
                排序字段
            </th>
            <th class="C_olumn Time">
                添加时间
            </th>
            <%--<th class="C_olumn A">扩展A字段</th>--%>
            <%--<th class="C_olumn B">扩展B字段</th>--%>
            <%--<th class="C_olumn C">扩展C字段</th>--%>
            <%--<th class="C_olumn D">扩展D字段</th>--%>
            <%--<th class="C_olumn E">扩展E字段</th>--%>
            <th style="width: 120px;">
                操作
            </th>
        </tr>
        <%int i = 0; foreach (BusinessCircle m_bc in _Pager.DataList)
          {
              i++;%>
        <tr cid='<%=m_bc.id %>'>
            <td>
                <input type="checkbox" name="ids" value='<%=m_bc.id %>' />
            </td>
            <td>
                <%=(_Pager.PageIndex-1)*_Pager.PageSize + i %>
            </td>
            <td class="C_olumn id">
                <%=m_bc.id.JSubString(30)%>
            </td>
            <td class="C_olumn C_Title">
                <%=m_bc.C_Title.JSubString(30)%>
            </td>
            <td class="C_olumn SiteCityID1">
                <%=new DealMvc.Core.Base.BLL_SiteCity().GetSiteCityString(m_bc.SiteCityID1, m_bc.SiteCityID2, m_bc.SiteCityID3)%>
            </td>
            <td class="C_olumn C_Initials">
                <%=m_bc.C_Initials.JSubString(30)%>
            </td>
            <td class="C_olumn Sort">
                <%=m_bc.Sort.JSubString(30)%>
            </td>
            <td class="C_olumn Time">
                <%=m_bc.Time.JSubString(30)%>
            </td>
            <%--<td class="C_olumn A"><%=m_bc.A.JSubString(30)%></td>--%>
            <%--<td class="C_olumn B"><%=m_bc.B.JSubString(30)%></td>--%>
            <%--<td class="C_olumn C"><%=m_bc.C.JSubString(30)%></td>--%>
            <%--<td class="C_olumn D"><%=m_bc.D.JSubString(30)%></td>--%>
            <%--<td class="C_olumn E"><%=m_bc.E.JSubString(30)%></td>--%>
            <td>
                <a class='SmallButton' onclick='top.AddLabel("编辑信息","<%=Url.Action("BusinessCircleAE", new { id = m_bc.id })%>", location.href )'>
                    编辑/查看</a> <a class='SmallButton' onclick="DeleteId('<%=m_bc.id %>')">删除信息</a>
            </td>
        </tr>
        <%} %>
        <%if (_Pager.DataList.Count == 0)
          { %><tr>
            <td colspan="16">
                暂时没有内容... ...
            </td>
        </tr>
        <%} %><tr>
            <td colspan="16">
                <input type="hidden" name="Page" value='<%=_Pager.PageIndex %>' /><a class="XshopCheckAll">全部选定</a>&nbsp;&nbsp;&nbsp;<a
                    class="XshopReverseCheck">反向选择</a>&nbsp;&nbsp;&nbsp;<a class="XshopDelCheck" onclick="DeleteSelectAllIds(this)">删除选中</a>
            </td>
        </tr>
    </table>
    <!--分页信息-->
    <div class="pagerhtml" style="padding-bottom: 10px;">
        <%=_Pager.PagerHTML(Page) %></div>
</asp:Content>
