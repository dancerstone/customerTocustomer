<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
    Pager<LifeEstheticianNew_XLY> _Pager = (Pager<LifeEstheticianNew_XLY>)ViewData["Pager"];
    string SortType = ViewData["SortType"].ToString();
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
    });
</script>
