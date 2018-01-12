<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DeliveryAddress>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/PCASClass.js" type="text/javascript"></script>
    <link href="/App_Themes/css/Address.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <%
        bool isEdit = (bool)ViewData["isEdit"];
    %>
    <div class="PhoneTop2">
        <h1>
            <%=isEdit?"编辑":"添加" %>收货地址</h1>
        <a class="btnL" href="<%=Url.Action("DeliveryAddressList","Member") %>">
            <img src="/App_Themes/images/img_goback.png" /></a> <a style="color: #fff;" class="btnR saveDeliveryAddress">
                保存 </a>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".saveDeliveryAddress").click(function () {
                $(".DeliveryAddressForm").submit();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        bool isEdit = (bool)ViewData["isEdit"];
    %>
    <div class="da_content">
        <div class="h15">
        </div>
        <div class="baseInfo">
            <form method="post" class="DeliveryAddressForm">
            <ul>
                <li>
                    <%=Html.Hidden("DA_IsDefault",Model.IsNull?"true":Model.DA_IsDefault.ToString())%>
                    <div class="div">
                        收货人：<input type="text" name="DA_ConsigneeName" value="<%=Model.DA_ConsigneeName %>" /></div>
                </li>
                <li>
                    <div class="div">
                        手机号码：<input type="text" name="DA_Phone" value="<%=Model.DA_Phone %>" /></div>
                </li>
                <li>
                    <div class="div">
                        联系电话：<input type="text" name="DA_LandLine" value="<%=Model.DA_LandLine %>" /></div>
                </li>
                <li class="areali">
                    <div class="div">
                        区域：<select name="DA_Province">
                        </select>
                        <select name="DA_City">
                        </select>
                        <select name="DA_Area">
                        </select>
                        <script type="text/javascript">
                            $(function () {
                                new PCAS("DA_Province", "DA_City", "DA_Area", "<%=Model.DA_Province %>", "<%=Model.DA_City %>", "<%=Model.DA_Area %>");
                            });
                        </script>
                    </div>
                </li>
                <li>
                    <div class="div" style="border-bottom: none;">
                        街道：<input type="text" name="DA_DetailAddress" value="<%=Model.DA_DetailAddress %>" /></div>
                </li>
            </ul>
            </form>
        </div>
        <div class="h7">
        </div>
        <div class="defaultInfo">
            <ul>
                <li>
                    <div class="div" style="border-bottom: none;">
                        <div class="fl">
                            设为默认：<span class="defaultText"><%=Model.DA_IsDefault?"开":"关" %></span></div>
                        <div class="fr checkDefault" ckval="<%=Model.DA_IsDefault %>">
                            <img src="/App_Themes/images/<%=Model.DA_IsDefault?"img_check.png":"img_uncheck.png" %>" /></div>
                    </div>
                </li>
            </ul>
        </div>
        <script type="text/javascript">
            $(function () {
                $(".checkDefault").click(function () {
                    var ckVal = $(this).attr("ckval");
                    if (ckVal == "False") {
                        $(".defaultText").text("开");
                        $(this).attr("ckval", "True");
                        $("input[name='DA_IsDefault']").val("true");
                        $(this).find("img").attr("src", "/App_Themes/images/img_check.png");
                    } else if (ckVal == "True") {
                        $(".defaultText").text("关");
                        $(this).attr("ckval", "False");
                        $("input[name='DA_IsDefault']").val("false");
                        $(this).find("img").attr("src", "/App_Themes/images/img_uncheck.png");
                    }
                });
            });
        </script>
        <%if (isEdit && !Model.DA_IsDefault)
          { %>
        <a href="<%=Url.Action("DeleteDeliveryAddress","Member",new{ids=Model.id}) %>">
            <div class="deleteAddress">
                <img src="/App_Themes/images/img_delete.png" />删除收货地址</div>
        </a>
        <%} %>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
