<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/life.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            生活美学家
        </div>
        <div class="fr other">
            <ul>
                <li><a href="<%=Url.Action("Index","Member") %>">
                    <img src="../../App_Themes/images/p_2.png" /></a></li>
                <li><a href="<%=Url.Action("Car","Main") %>">
                    <img src="../../App_Themes/images/p_3.png" /></a></li>
            </ul>
            <span class="clear_f"></span>
        </div>
    </div>
    <span class="clear_f"></span>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        Pager<LifeEstheticianNew_XLY> _Pager = (Pager<LifeEstheticianNew_XLY>)ViewData["Pager"];
        string SortType = ViewData["SortType"].ToString();
    %>
    <div class="lifeContent">
        <div class="h3">
        </div>
        <div class="title">
            <div class="fl <%=SortType=="time"?"divNow":"" %>">
                <a href="<%=Url.Action("Life","Main",new{ sorttype="time" }) %>">最新上传</a>
            </div>
            <div class="fr <%=SortType=="praise"?"divNow":"" %>">
                <a href="<%=Url.Action("Life","Main",new{ sorttype="praise" }) %>">更多点赞</a>
            </div>
        </div>
        <div class="h15">
        </div>
        <div class="content">
            <ul id="masonry2">
                <%
                    SiteInfo m_site = SiteInfo.GetModel(t => t.id != null);
                    foreach (LifeEstheticianNew_XLY item in _Pager.DataList)
                    {
                        string beside = item.M_IsBeside ? "BesideDiv" : "";
                        LifePraise_XLY lp_model = LifePraise_XLY.GetModel(t => t.M_MemberUID == m_login.M_UID && t.M_LifeID == item.id);
                        string Class_isFullOrNull = lp_model.IsNull ? "btn_null" : "";
                        string Img_isFullOrNull = lp_model.IsNull ? "/App_Themes/images/null_img.png" : "/App_Themes/images/full_img.png";
                %>
                <li>
                    <div class="img <%=beside %>">
                        <img src="<%=Url.ImageAutoSize(202,166,m_site.WebAddress+item.M_FrontCover,ImageSizeMode.Size) %>" />
                    </div>
                    <div class="author">
                        <div class="headpic fl">
                            <img class="img_head2" src="<%=item.Member.GetAvatr() %>" /><img class="img_head1"
                                src="/App_Themes/images/img_life_headpic.png" />
                        </div>
                        <div class="name fl">
                            <div style="color: #fefefe;">
                                <%=item.Member.M_UserName %></div>
                            <%=item.M_Time %>
                        </div>
                        <div class="praise fr">
                            <div class="icon">
                                <img lifeid="<%=item.id %>" class="<%=Class_isFullOrNull %>" src="<%=Img_isFullOrNull %>" />
                            </div>
                            <span>
                                <%=item.M_PraiseNum%></span>
                        </div>
                        <span class="clear_f"></span>
                    </div>
                    <span class="clear_f"></span></li>
                <%
} %>
            </ul>
        </div>
        <span class="clear_f"></span>
        <%if (_Pager.PageCount > 1)
          { %>
        <div class="p_more">
            <a class="LifeMore" pid="<%=_Pager.PageIndex %>" pdd="<%=_Pager.PageCount %>">more</a></div>
        <%} %>
        <span class="clear_f"></span>
        <div class="h3">
        </div>
    </div>
    <script type="text/javascript">
        $(function () {

            $(".btn_null").click(function () {
                var this_v = $(this);
                $.ajax({
                    url: '/Main/OperationPraise',
                    async: false,
                    type: "POST",
                    data: { lifeID: $(this).attr("lifeID"), r: new Date().getTime() },
                    success: function (msg) {
                        var json = $.parseJSON(msg);
                        if (json.error == 0) {//
                            var count = 0;
                            if (this_v.parent().next().text().trim() == undefined || this_v.parent().next().text().trim() == null || this_v.parent().next().text().trim() == "") {
                                count = 0;
                            } else {
                                count = this_v.parent().next().text();
                            }
                            this_v.parent().next().text(parseInt(count) + parseInt(1));
                            this_v.attr("src", "/App_Themes/images/full_img.png");
                            this_v.parent().next().removeClass("btn_null");
                        } else {
                            $("body").showMessage(json.message);
                        }
                    }
                });
            });


            //美学杂志 加载更多
            $(".LifeMore").live("click", function () {
                var n_pid = parseInt($(this).attr("pid")); //当前页
                var m_pdd = parseInt($(this).attr("pdd")); //总页数
                var this_v = $(this);
                if (n_pid >= m_pdd) {
                    $(this).parent().hide();
                    return;
                }
                $.get("/Main/LoadingLife", { Page: (parseInt(n_pid) + parseInt(1)), sorttype: '<%=SortType %>' }, function (msg) {

                    $(".BesideDiv").each(function (i, v) {
                        $(v).removeClass("BesideDiv");
                    });
                    $("#masonry2 li").last().after(msg);
                    this_v.attr("pid", parseInt(n_pid) + parseInt(1));
                    if (parseInt(n_pid) + parseInt(1) >= m_pdd) {
                        this_v.parent().remove();
                    }
                    setTimeout(function () {
                        Img_Head();
                    }, 500);
                });
            });


        });

        window.onload = function () {
            Img_Head();
        }

        window.onresize = function () {
            $(".img_head2").height($(".img_head1").height());
        }

        function Img_Head() {
            $(".img_head2").height($(".img_head1").height());
            $(".BesideDiv").each(function (i, v) {
                var imgH = $(v).find("img").height();
                $(v).height(imgH);
                $(v).css("overflow", "hidden");
                $(v).find("img").height(parseInt(imgH) + parseInt(imgH * 0.1));
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
