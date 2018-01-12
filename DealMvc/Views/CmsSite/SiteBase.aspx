<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CmsSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" class="form" enctype="multipart/form-data">
    <table class="table1">
        <tr>
            <th align="right" style="width: 120px; overflow: hidden;">
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td align="right">
                网站名称
            </td>
            <td>
                <input type="text" name="WebName" class="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                网站域名
            </td>
            <td>
                <input type="text" name="WebAddress" class="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                网站标题
            </td>
            <td>
                <input type="text" name="WebTitle" class="" />
                <!--<span class="InputDetail">{网站标题}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                网站关键词
            </td>
            <td>
                <%=Html.TextArea("WebKeyword", new { @class = "J notips TextArea", style = "width:290px; height:50px;" })%>
                <!--<span class="InputDetail">{网站关键词}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                标题分隔符
            </td>
            <td>
                <input type="text" name="WebDelimiter" class="noJ" style="width: 20px;" />
            </td>
        </tr>
        <tr>
            <td align="right">
                网站版权
            </td>
            <td>
                <input type="text" name="WebCopyright" class="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                网站后台权限
            </td>
            <td>
                <input type="radio" name="WebCompetence" id="PermissionsStatus1" value="True" /><label
                    class="green" for="PermissionsStatus1">启用权限模块</label><input type="radio" id="PermissionsStatus0"
                        name="WebCompetence" value="False" /><label class="red" for="PermissionsStatus0">关闭权限模块</label>
            </td>
        </tr>
        <tr>
            <td align="right">
                数据缓存设置
            </td>
            <td>
                <input type="checkbox" value="True" name="IsOpenDataCache" />开启缓存&nbsp;&nbsp;&nbsp;&nbsp;缓存时间(秒)
                <input type="text" name="DataCacheTime" class="int noJ" style="width: 100px;" />
            </td>
        </tr>
        <%--<tr>
            <td align="right">
                商家订单提拥比例
            </td>
            <td>
                <input type="text" name="ProportionMention" class="noJ float" style="width: 50px;" />%
            </td>
        </tr>
        <tr>
            <td align="right">
                底部联系方式
            </td>
            <td>
                <input type="text" name="WebBottomContact" class="req noJ " />
                <!--<span class="InputDetail">{底部联系方式}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                底部营业时间
            </td>
            <td>
                <input type="text" name="WebBottomHours" class="req noJ " />
                <!--<span class="InputDetail">{营业时间}</span>-->
            </td>
        </tr>--%>
        <tr>
            <td align="right" valign="top">
                网站状态
            </td>
            <td>
                <input type="radio" name="WebStatus" id="SiteStatus1" value="True" /><label class="green"
                    for="SiteStatus1">开启网站</label>
                <input type="radio" id="SiteStatus0" name="WebStatus" value="False" /><label class="red"
                    for="SiteStatus0">关闭网站</label>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                关闭网站原因
            </td>
            <td>
                <%=Html.TextArea("WebCloseRemark", new { @class = "J notips TextArea", style = "width:680px; height:200px;" })%>
                <script type="text/javascript">
                    KindEditor.ready(function (K) {
                        window.EditorObject = K.create('textarea[name="WebCloseRemark"]', {
                            resizeType: 1,
                            uploadJson: '/HtmlEditor/UploadImage',
                            fileManagerJson: '/HtmlEditor/ProcessRequest',
                            allowFileManager: true,
                            themeType: 'simple'
                        });
                    });
                </script>
            </td>
        </tr>
        <tr>
            <td align="right">
                网站底部信息
            </td>
            <td>
                <%=Html.TextArea("WebBottomInfo", new { @class = "J notips TextArea", style = "width:680px; height:350px;" })%>
                <%=DealMvc.Common.HtmlStrHelper.KindEditorReady("WebBottomInfo")%>
                <!--<span class="InputDetail">{网站底部信息}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                首页底部图片
            </td>
            <td>
                <input type="file" name="WebFriendLinks" class="" /><br />
                <img width="600" height="250" src='<%=ViewData["_WebFriendLinks"].JEP2() %>' />
                <!--<span class="InputDetail">{友情链接}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                首页底部介绍
            </td>
            <td>
                <input type="text" name="WebRegService" class="noJ w450px" />
                <!--<span class="InputDetail">{友情链接}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                微信二维码图片
            </td>
            <td>
                <input type="file" name="WebWeiXinImage" class="" /><br />
                <img width="105" height="105" src='<%=ViewData["_WebWeiXinImage"].JEP2() %>' />
                <!--<span class="InputDetail">{微信二维码图片}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                微博二维码图片
            </td>
            <td>
                <input type="file" name="WebWeiBoImage" class="" /><br />
                <img width="105" height="105" src='<%=ViewData["_WebWeiBoImage"].JEP2() %>' />
                <!--<span class="InputDetail">{微博二维码图片}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                联系电话
            </td>
            <td>
                <input type="text" name="WebContactPhone" class="req noJ w450px" />
                <!--<span class="InputDetail">{联系电话}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                联系邮箱
            </td>
            <td>
                <input type="text" name="WebContactEmail" class="req noJ w450px" />
                <!--<span class="InputDetail">{联系邮箱}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                客服QQ
            </td>
            <td>
                <input type="text" name="KeFuQQ" class="req noJ w450px" />
                <!--<span class="InputDetail">{客服QQ}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                售前电话
            </td>
            <td>
                <input type="text" name="BeforeSalePhone" class="req noJ w450px" />
                <!--<span class="InputDetail">{售前电话}</span>-->
            </td>
        </tr>
        <tr>
            <td align="right">
                售后电话
            </td>
            <td>
                <input type="text" name="AfterSalePhone" class="req noJ w450px" />
                <!--<span class="InputDetail">{售后电话}</span>-->
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value="修改" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
