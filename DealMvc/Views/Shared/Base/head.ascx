<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
%>
<script type="text/javascript" language="javascript">
    function AddFavorite(sURL, sTitle) {
        try {
            window.external.addFavorite(sURL, sTitle);
        }
        catch (e) {
            try {
                window.sidebar.addPanel(sTitle, sURL, "");
            }
            catch (e) {
                alert("加入收藏失败，请使用Ctrl+D进行添加");
            }
        }
    }
    function SetHome(obj, vrl) {
        try {
            obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl);
        }
        catch (e) {
            if (window.netscape) {
                try {
                    netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                }
                catch (e) {
                    alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将[signed.applets.codebase_principal_support]设置为'true'");
                }
                var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
                prefs.setCharPref('browser.startup.homepage', vrl);
            }
        }
    }  
</script>
<div class="headDiv">
    <div class="wal">
        <a href="<%=Url.Action("Index","Home") %>" class="logo">
            <img src="/App_Themes/UI/image/logo.png" /></a>
    </div>
</div>
<div class="nav">
    <div class="wal">
        <ul>
            <li><a href="<%=Url.Action("Index","Home") %>">首页</a></li>
            <li><a href="<%=Url.Action("Activities","Home") %>">活动专区</a></li>
            <li><a href="<%=Url.Action("AppreciationSingle","Home") %>">3D体验馆</a></li>
            <li><a href="<%=Url.Action("Life","Home") %>">生活美学家</a></li>
            <li><a href="<%=Url.Action("CustomRaiders","Home") %>">定制攻略</a></li>
            <li><a href="<%=Url.Action("Faq","Home") %>">问题咨询</a></li>
            <li><a href="<%=Url.Action("Designers","Home") %>">设计师专区</a></li>
        </ul>
        <form action="<%=Url.Action("productlist","Home") %>" method="post">
        <%
            string keywordS = ViewData["keyword"].ToString2();
        %>
        <div class="form">
            <input type="text" class="input1 input_hover webkeyword" value="<%=string.IsNullOrEmpty(keywordS)?"搜索产品":keywordS %>" title="搜索产品" placeholder="搜索产品"
                name="keyword" />
            <input type="submit" class="btn1" value="">
            <span class="clear_f"></span>
        </div>
        </form>
        <div class="btn">
            <%if (m_login.IsNull)
              { %><a href="<%=Url.Action("Login","User") %>"><img src="/App_Themes/UI/image/nimg30.png" /></a>
            <%}
              else
              { %>
            <div class="m_info">
                <div class="img fl" style="width: 0px;">
                    <a href="<%=Url.Action("Index","Member") %>">
                        <img  src="<%=m_login.M_Avatar.JEP() %>" /></a></div>
                <div class="fl" style="width: 12px; margin-left: 76px; overflow: hidden;">
                    <span class="selectsub">▼</span></div>
                <div class="clear_f">
                </div>
            </div>
            <div class="topLayerDIV">
                <ul>
                    <li><a href="<%=Url.Action("LoginOut","User") %>">退出</a></li>
                </ul>
            </div>
            <%} %>
        </div>
    </div>
</div>
