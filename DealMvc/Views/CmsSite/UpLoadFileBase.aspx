<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" class="form" enctype="multipart/form-data">
    <table class="table1">
        <tr>
            <th align="right" style="width: 120px; overflow: hidden;"></th>
            <th></th>
        </tr>
        <tr>
            <td align="right">上传文件格式 </td>
            <td>
                <input type="text" name="UploadExtension" class="req" tips="以 | 分割" />
            </td>
        </tr>
        <tr>
            <td align="right">上传文件大小 </td>
            <td>
                <input type="text" name="UploadSize" class="req float" tips="单位兆(M)" />
            </td>
        </tr>
      <%--  <tr>
            <td align="right">默认图片</td>
            <td>
                <input type="file" name="DefaultImg" class="" />
            </td>
        </tr>
        <%if (!string.IsNullOrEmpty(ViewData["DefaultImg"].ToString2()))
          { %>
        <tr>
            <td align="right"></td>
            <td><img src="<%=ViewData["DefaultImg"] %>" width="250px" /> </td>
        </tr>
        <%} %>--%>
        <tr>
            <td></td>
            <td>
                <input type="submit" value="修改" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
