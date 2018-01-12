<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<style>
    .indexBar a { height: auto; }
</style>
<div class="indexBar">
    <div class="wal">
        <ul>
            <%
                List<GoodsCate_XLY> GC_XLY_LIST = GoodsCate_XLY.GetModelList(t => t.id > 0).List;
                foreach (var item in GC_XLY_LIST)
                {
            %>
            <li class="liNow">
                <div class="imgDiv">
                    <a href="<%=Url.Action("productlist", "Home", new { cateid=item.id })%>">
                        <img src="<%=item.GC_SmallPic.JEP() %>" /><img src="<%=item.GC_SmallPicNow.JEP() %>"
                            class="img2" /></a></div>
                <div class="content">
                    <%-- <div class="xsjDiv">
                        <img src="/App_Themes/UI/image/nimg69_bg.png" /></div>--%>
                    <div>
                        <a href="<%=Url.Action("productlist", "Home", new { cateid=item.id })%>">
                            <%=item.GC_EnglinshName %></a></div>
                    <a href="<%=Url.Action("productlist", "Home", new { cateid=item.id })%>">
                        <%=item.GC_Name %></a></div>
            </li>
            <%
}
            %>
        </ul>
        <div class="clear_f">
        </div>
    </div>
</div>
