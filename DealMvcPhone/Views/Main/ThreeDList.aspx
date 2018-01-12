<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../JS/jcweb/product.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            3D体验馆
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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        Pager<ExperienceHall> _Pager = (Pager<ExperienceHall>)ViewData["Pager"];
        SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
    %>
    <div class="PhoneWal">
        <div class="T_I">
            <ul>
                <li class="aNow"><a href="<%=Url.Action("ThreeDList","Main") %>">
                    <div class="p_t_i">
                        <img src="../../App_Themes/images/P_24.png" /></div>
                    <div class="p_t_t">
                        单品鉴赏
                    </div>
                    <div class="p_r_b">
                        &nbsp;</div>
                    <div class="clear_f">
                    </div>
                </a></li>
                <li><a href="<%=Url.Action("EffectDList","Main") %>">
                    <div class="p_t_i">
                        <img src="../../App_Themes/images/P_23.png" /></div>
                    <div class="p_t_t">
                        效果展示
                    </div>
                    <div class="clear_f">
                    </div>
                </a></li>
            </ul>
            <div class="clear_f">
            </div>
        </div>
        <div class="T_II">
            <ul>
                <%
                    foreach (var item in _Pager.DataList)
                    {
                        int? cv = 1;
                        string MRL = string.Empty;
                        switch (cv)
                        {
                            case 1:
                                MRL = m_site.WebAddress + item.SingleListPic;
                                break;
                            case 2:
                                MRL = m_site.WebAddress + item.EffectListPic;
                                break;
                            default:
                                break;
                        }
                %>
                <li><a>
                    <img src="<%=MRL %>" /></a> </li>
                <%} %>
            </ul>
            <span class="clear_f"></span>
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
                <a class="pageMore ThreeDMore" pid="<%=_Pager.PageIndex %>" pdd="<%=_Pager.PageCount %>"
                    cateid="1">more</a></div>
            <%
                }
            %>
        </div>
    </div>
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
