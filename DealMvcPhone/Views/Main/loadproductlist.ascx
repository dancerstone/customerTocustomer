<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Pager<Goods_XLY> _Pager = (Pager<Goods_XLY>)ViewData["Pager"];
    Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
    SiteInfo m_site = SiteInfo.GetModel(t => t.id != 0);
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