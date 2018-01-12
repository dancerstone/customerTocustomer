<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/Member.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            我的订单</h1>
        <a class="btnL" href="javascript:history.go(-1);">
            <img src="/App_Themes/images/img_goback.png" /></a> <a class="btnR" href="<%=Url.Action("Car","Main") %>">
                <img src="/App_Themes/images/img_car.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Pager<Orders> _Pager = (Pager<Orders>)ViewData["Pager"];
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        string c_status = ViewData["c_status"].ToString2();
        string c_time = ViewData["c_time"].ToString2();
    %>
    <div class="orderContent">
        <form method="post" class="search_btn_submit">
        <div class="searchDiv">
            <div class="fl statusDiv">
                <div>
                    <%=HtmlEnum.EnumToSDCn("c_status", typeof(CommonEnumHelper.PhoneOrderStatus), c_status, "", "全部状态", "class='c_status_model'")%>
                </div>
            </div>
            <div class="fl speatorDiv">
                <img src="/App_Themes/images/img_speator2.png" /></div>
            <div class="fl timeDiv">
                <div>
                    <%=HtmlEnum.EnumToSDCn("c_time", typeof(CommonEnumHelper.PhoneOrderTime), c_time, "", "全部周期", "class='c_time_model'")%></div>
            </div>
            <span class="clear_f"></span>
        </div>
        </form>
        <div class="h15">
        </div>
        <div class="orderList">
            <ul>
                <%
                    int? cb = 0;
                    foreach (var item in _Pager.DataList)
                    {
                        cb++;
                %>
                <li>
                    <div class="title">
                        <span class="status fl">
                            <%=item.O_Status %>
                        </span><span class="fr orderNum">订单号：<%=item.O_UID %><span class="deleteorder_model"
                            o_id="<%=item.O_UID %>"><img src="/App_Themes/images/img_order_del.png" /></span></span>
                        <span class="clear_f"></span>
                    </div>
                    <div class="logistics">
                        <%
                            if (item.O_Status == "未付款")
                            {
                        %>
                        <div class="btn_order_model">
                            <a href="<%=Url.Action("OrderSubmitSuccess","Member",new{Order_UID=item.O_UID}) %>">
                                立即支付</a></div>
                        <div class="btn_order_model">
                            <a class="CancelThisOrder" o_id="<%=item.O_UID %>">取消订单</a></div>
                        <%}
                            else if (item.O_Status == "已发货")
                            {
                                if (item.O_PayStyle != 3 && item.A == "1")
                                {
                        %><a class="color1 ConfirmReceiptOrder" o_id="<%=item.O_UID %>">确认收货</a><br />
                        <%-- <a class="color1" href="<%=Url.Action("OrderApplyForReturn","Member",new{Order_UID=item.O_UID}) %>"
                            target="_blank">返修/退货</a>--%><br />
                        <%
                            }
                            }
                        %>
                        <div class="clear_f">
                        </div>
                    </div>
                    <div class="content">
                        <div class="top">
                            <div class="big_top fl">
                                <%
List<OrderGoodsList> OG_LIST = OrderGoodsList.GetModelList(t => t.Order_UID == item.O_UID).List;
int? cc = 0;
foreach (var og in OG_LIST)
{
    cc++;
                                %>
                                <div class="good_model_m">
                                    <div class="img fl">
                                        <a href="<%=Url.Action("productdetail","main",new{gid=og.Goods.id}) %>">
                                            <img src="/App_Themes/images/img_car_img1.png" /></a>
                                    </div>
                                    <div class="right fl">
                                        <div class="title">
                                            <a href="#">
                                                <%=og.Goods.G_Title%></a>
                                        </div>
                                        <div class="other">
                                            <div class="color">
                                                颜色：<%=og.Goods.G_Color%></div>
                                            <div class="num">
                                                下单时间：<%=og.OG_Time%></div>
                                        </div>
                                    </div>
                                    <div class="clear_f">
                                    </div>
                                </div>
                                <%} %>
                            </div>
                            <div class="fr bottomImg">
                                <a href="<%=Url.Action("OrderDetail","Member",new{Order_UID=item.O_UID}) %>">
                                    <img src="/App_Themes/images/20150310_28.png" /></a></div>
                            <span class="clear_f"></span>
                        </div>
                    </div>
                    <div class="result">
                        实付款：￥<%=item.O_OrderAllMoney.ToJiaGe()%>
                    </div>
                </li>
                <%
                    }
                    if (_Pager.DataList.Count == 0)
                    {
                %>
                <div class="m_no_model">
                    暂无相关的订单信息</div>
                <%
                    }
                %>
            </ul>
        </div>
        <div class="MoreDivModel">
            <%
                if (_Pager.PageIndex >= _Pager.PageCount)
                {
            %>
            <%
                }
                else
                { 
            %>
            <div class="p_more">
                <a class="pageMore ProductMore" pid="<%=_Pager.PageIndex %>" pdd="<%=_Pager.PageCount %>">
                    more</a></div>
            <%
                }
            %>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            //产品列表 加载更多
            $(".ProductMore").live("click", function () {
                var pid = parseInt($(this).attr("pid")); //当前页
                var pdd = parseInt($(this).attr("pdd")); //总页数
                var c_status = $(".c_status_model").val();
                var c_time = $(".c_time_model").val();
                if (pid >= pdd) {
                    $(this).hide();
                    return;
                }
                $(this).remove();
                $.get("/Member/LoadingOrders", { Page: parseInt(pid + 1), c_status: c_status, c_time: c_time }, function (msg) {
                    $(".orderList ul li").last().after(msg);
                });
                $.get("/Member/LoadingOrdersPage", { Page: parseInt(pid + 1), c_status: c_status, c_time: c_time }, function (msg) {
                    $(".MoreDivModel").html(msg);
                });
            });


            //订单查询
            $(".c_status_model").change(function () {
                var vs_v = $(this).val();
                $(".search_btn_submit").submit();
            });
            //订单查询
            $(".c_time_model").change(function () {
                var vs_v = $(this).val();
                $(".search_btn_submit").submit();
            });

            //取消订单
            $(".CancelThisOrder").click(function () {
                var O_UID = $(this).attr("O_ID");
                if (confirm("你确定取消订单编号：" + O_UID + " 的订单吗?")) {
                    $.get("/Comm/CancelOrder", { Order_UID: O_UID }, function (msg) {
                        if (msg == "true") {
                            $("body").showMessage("取消订单操作成功");
                            window.location.reload();
                        }
                    });
                }
            });
            //删除订单
            $(".deleteorder_model").click(function () {
                var O_UID = $(this).attr("O_ID");
                if (confirm("你确定删除该订单吗?")) {
                    $.get("/Comm/DeleteThisOrder", { Order_UID: O_UID }, function (msg) {
                        if (msg == "true") {
                            $("body").showMessage("删除订单操作成功");
                            window.location.reload();
                        }
                    });
                }
            });
            //确认收货
            $(".ConfirmReceiptOrder").click(function () {
                var O_UID = $(this).attr("O_ID");
                if (confirm("订单编号：" + O_UID + " 确认收货后将无法申请退款退货，确定收货操作吗？")) {
                    $.get("/Comm/ConfirmReceiptOrder", { Order_UID: O_UID }, function (msg) {
                        if (msg == "true") {
                            $("body").showMessage("确认收货操作成功");
                            window.location.reload();
                        }
                    });
                }
            });




        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
