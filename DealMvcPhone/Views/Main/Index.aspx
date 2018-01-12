<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            window.onload = function () {
                picM();
            }
            $(window).resize(function () {
                picM();
            });
            function picM() {
                var ob = $(".waltype .right_b");
                var a = ob.height() / 2;
                ob.prev().find(".top_a").css('height', a);
                ob.prev().find(".top_b").css('height', a - 3);
                ob.prev().find(".top_b img").css('height', a - 3);

                var obc = $(".waltypenew .right_b");
                var ac = ob.height() / 2;
                obc.next().find(".top_a").css('height', a);
                obc.next().find(".top_b").css('height', a - 3);
                obc.next().find(".top_b img").css('height', a - 3);
            }

            var indexBarTop = $('.classification').offset().top;
            $(window).scroll(function () {
                if ($(window).scrollTop() > indexBarTop) {
                    $('.classification').addClass('classificationnow');
                } else {
                    $('.classification').removeClass('classificationnow');
                }
            })


        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="topA">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/p_1.png" /></a></div>
        <div class="fr other">
            <ul>
                <li><a href="<%=Url.Action("Index","Member") %>">
                    <img src="../../App_Themes/images/p_2.png" /></a></li>
                <li><a href="<%=Url.Action("Car","Main") %>">
                    <img src="../../App_Themes/images/p_3.png" /></a></li>
            </ul>
            <span class="clear_f"></span>
        </div>
        <div class="clear_f">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        DealMvc.Core.Base.BLL_AdvertisingInfo b_BLL_AdvertisingInfo = new DealMvc.Core.Base.BLL_AdvertisingInfo();
        SiteInfo m_site = SiteInfo.GetModel(t => t.id != 0);
    %>
    <div class="blanner mBan2">
        <div id="slideBox" class="slideBox">
            <div class="hd">
                <ul>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div>
            <div class="bd">
                <ul>
                    <li>
                        <%=b_BLL_AdvertisingInfo.GetAdPhone(CommonEnumHelper.AdsLocation.首页幻灯片1.ToString2())%>
                    </li>
                    <li>
                        <%=b_BLL_AdvertisingInfo.GetAdPhone(CommonEnumHelper.AdsLocation.首页幻灯片2.ToString2())%>
                    </li>
                    <li>
                        <%=b_BLL_AdvertisingInfo.GetAdPhone(CommonEnumHelper.AdsLocation.首页幻灯片3.ToString2())%>
                    </li>
                    <li>
                        <%=b_BLL_AdvertisingInfo.GetAdPhone(CommonEnumHelper.AdsLocation.首页幻灯片4.ToString2())%>
                    </li>
                    <li>
                        <%=b_BLL_AdvertisingInfo.GetAdPhone(CommonEnumHelper.AdsLocation.首页幻灯片5.ToString2())%>
                    </li>
                </ul>
            </div>
        </div>
        <span class="clear_f"></span>
        <script type="text/javascript">
            jQuery(".slideBox").slide({ mainCell: ".bd ul", effect: "fold", autoPlay: true, trigger: "click" });
        </script>
    </div>
    <div class="navA">
        <ul>
            <li>
                <div class="ico_a">
                    <a href="<%=Url.Action("Magazine","Main") %>">
                        <img src="../../App_Themes/images/p_4.png" /></a></div>
                <div class="title_a">
                    <a href="<%=Url.Action("Magazine","Main") %>">电子杂志</a></div>
            </li>
            <li>
                <div class="ico_a">
                    <a href="<%=Url.Action("ThreeDList","Main") %>">
                        <img src="../../App_Themes/images/p_6.png" /></a></div>
                <div class="title_a">
                    <a href="<%=Url.Action("ThreeDList","Main") %>">3D体验馆</a></div>
            </li>
            <li>
                <div class="ico_a">
                    <a href="<%=Url.Action("Life","Main") %>">
                        <img src="../../App_Themes/images/p_7.png" /></a></div>
                <div class="title_a">
                    <a href="<%=Url.Action("Life","Main") %>">生活美学家</a></div>
            </li>
            <li>
                <div class="ico_a">
                    <a href="<%=Url.Action("DesignersList","Main") %>">
                        <img src="../../App_Themes/images/p_5.png" /></a></div>
                <div class="title_a">
                    <a href="<%=Url.Action("DesignersList","Main") %>">设计师专区</a></div>
            </li>
        </ul>
        <span class="clear_f"></span>
    </div>
    <div class="classification">
        <ul>
            <%
                List<GoodsCate_XLY> GC_XLY_LIST = GoodsCate_XLY.GetModelList(t => t.id > 0).List;
                foreach (var item in GC_XLY_LIST)
                {
            %>
            <li><a href="<%=Url.Action("productlist", "Main", new { cateid=item.id })%>">
                <img src="<%=m_site.WebAddress+item.GC_SmallPic %>" /></a> </li>
            <%} %>
        </ul>
        <span class="clear_f"></span>
    </div>
    <%
        List<GoodsCate_XLY> GC_Cate_List = GoodsCate_XLY.GetModelList(t => t.id > 0).List;
        int? cb = 0;
        foreach (GoodsCate_XLY item in GC_Cate_List)
        {
            cb++;
            //产品列表
            List<Goods_XLY> Goods_List = Goods_XLY.GetModelList(2, t => t.G_Cate_ID == item.id.ToString2() && t.G_TuiJianIndex == true, t => t.G_IndexSort.lb_Desc()).List;
            if (cb % 2 != 0)
            {
    %>
    <div class="waltype">
        <div class="left_a">
            <div class="top_a">
                <div class="img_a">
                    <a href="<%=Url.Action("productlist", "Main", new { cateid=item.id })%>">
                        <img src="<%=m_site.WebAddress+item.GC_SmallPicNow %>" /></a></div>
                <div class="tittle">
                    <a href="<%=Url.Action("productlist", "Main", new { cateid=item.id })%>">
                        <%=item.GC_EnglinshName %><br />
                        <%=item.GC_Name %></a></div>
            </div>
            <div class="top_b">
                <%
                    if (Goods_List.Count >= 1)
                    {
                        Goods_XLY m_1 = Goods_List[0];
                %>
                <a href="<%=Url.Action("productdetail", "Main", new { gid=m_1.id })%>">
                    <img src="<%=m_site.WebAddress+m_1.G_IndexPic %>" /></a>
                <%
                    }
                    else
                    { 
                %><a href=""><img src="/App_Themes/images/p_12.png" /></a><% } %>
            </div>
        </div>
        <div class="right_b">
            <%
                if (Goods_List.Count >= 2)
                {
                    Goods_XLY m_1 = Goods_List[1];
            %>
            <a href="<%=Url.Action("productdetail", "Main", new { gid=m_1.id })%>">
                <img src="<%=m_site.WebAddress+m_1.G_IndexPic %>" /></a>
            <%
                }
                else
                { 
            %>
            <a href="">
                <img src="/App_Themes/images/p_9.png" /></a><% } %>
        </div>
        <div class="clear_f">
        </div>
    </div>
    <%}
            else
            {
    %>
    <div class="waltypenew">
        <div class="right_b">
            <%
                if (Goods_List.Count >= 2)
                {
                    Goods_XLY m_1 = Goods_List[1];
            %>
            <a href="<%=Url.Action("productdetail", "Main", new { gid=m_1.id })%>">
                <img src="<%=m_site.WebAddress+m_1.G_IndexPic %>" /></a>
            <%
                }
                else
                { 
            %>
            <a href="">
                <img src="/App_Themes/images/p_9.png" /></a><% } %>
        </div>
        <div class="left_a">
            <div class="top_a">
                <div class="img_a">
                    <a href="<%=Url.Action("productlist", "Main", new { cateid=item.id })%>">
                        <img src="<%=m_site.WebAddress+item.GC_SmallPicNow %>" /></a></div>
                <div class="tittle">
                    <a href="<%=Url.Action("productlist", "Main", new { cateid=item.id })%>">
                        <%=item.GC_EnglinshName %><br />
                        <%=item.GC_Name %></a></div>
            </div>
            <div class="top_b">
                <%
                    if (Goods_List.Count >= 1)
                    {
                        Goods_XLY m_1 = Goods_List[0];
                %>
                <a href="<%=Url.Action("productdetail", "Main", new { gid=m_1.id })%>">
                    <img src="<%=m_site.WebAddress+m_1.G_IndexPic %>" /></a>
                <%
                    }
                    else
                    { 
                %><a href=""><img src="/App_Themes/images/p_12.png" /></a><% } %>
            </div>
        </div>
        <div class="clear_f">
        </div>
    </div>
    <%
}
        }%>
    <div class="changePC">
        <a href="<%=m_site.WebAddress %>">切换到电脑版>></a>
    </div>
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
