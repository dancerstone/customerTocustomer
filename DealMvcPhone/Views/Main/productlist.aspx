<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/jcweb/product.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <%
        Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
        int? CateID = ViewData["CateID"].ToInt32();//产品分类ID
        GoodsCate_XLY m_gc_model = GoodsCate_XLY.GetModel(CateID ?? 0);
        
    %>
    <div class="PhoneTop">
        <div class="fl logo">
            <a href="<%=Url.Action("Index","Main") %>">
                <img src="../../App_Themes/images/img_home.png" /></a></div>
        <div class="title_M">
            <%=m_gc_model.GC_Name %>
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
        int? CateID = ViewData["CateID"].ToInt32();//产品分类ID
        GoodsCate_XLY m_gc_model = GoodsCate_XLY.GetModel(CateID ?? 0);
        SiteInfo m_site = SiteInfo.GetModel(t => t.id != 0);
        Pager<Goods_XLY> _Pager = (Pager<Goods_XLY>)ViewData["Pager"];
    %>
    <div class="PhoneWal">
        <div class="p_list">
            <ul>
                <%
                    foreach (Goods_XLY item in _Pager.DataList)
                    {
                        if (!item.G_IsAdded)
                            continue;
                        string URL_L = item.G_IsAdded ? Url.Action("productdetail", "Main", new { gid = item.id }) : "javascript:void()";
                %>
                <li>
                    <div class="p_img">
                        <a href="<%=URL_L %>">
                            <img src="<%=m_site.WebAddress+item.G_ListPic %>" /></a></div>
                    <div class="p_title">
                        <div class="p_n">
                            <%=item.G_Title.JSubString(6*2,"") %><br />
                            <span>作者：<%=item.G_ZuoZhe.JSubString(4*2,"") %></span>
                        </div>
                        <div class="p_c">
                            <%GoodsCollect_XLY m_gcc = GoodsCollect_XLY.GetModel(t => t.G_UID == item.G_UID && t.Member_UID == m_login.M_UID && t.A == "1");
                              if (m_gcc.IsNull)
                              {
                                  
                            %>
                            <a class="btn_collectGoods" gid="<%=item.G_UID %>" login="<%=m_login.IsNull ? "false" : "true" %>">
                                <img src="../../App_Themes/images/p_19.png" /></a>
                            <%    
}
                              else
                              { 
                            %>
                                 <img src="../../App_Themes/images/p_29.png" />
                            <%
}
                            %>
                        </div>
                        <div class="clear_f">
                        </div>
                    </div>
                </li>
                <%
                    }
                %>
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
                <a class="pageMore ProductMore" pid="<%=_Pager.PageIndex %>" pdd="<%=_Pager.PageCount %>"
                    cateid="<%=CateID %>">more</a></div>
            <%
                }
            %>
        </div>
    </div>
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
</asp:Content>
