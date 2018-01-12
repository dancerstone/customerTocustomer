<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
    SiteInfo si_model = SiteInfo.GetModel(t => t.id != null);
    string qq_link = "http://wpa.qq.com/msgrd?v=3&uin=" + si_model.KeFuQQ + "&site=qq&menu=yes";
    HelpCenterCate hcc_gsjj = HelpCenterCate.GetModel(t => t.HCC_Level == 2 && t.HCC_Name == "公司简介");
    HelpCenterCate hcc_jrwm = HelpCenterCate.GetModel(t => t.HCC_Level == 2 && t.HCC_Name == "加入我们");
%>
<div class="footBg">
    <div class="wal">
        <!--footBg-->
        <div class="foot_01">
            <ul>
                <li>
                    <div class="ico">
                        <img src="/App_Themes/UI/image/nimg36_1.png" /></div>
                    <h1>
                        专业</h1>
                    <div class="content">
                        拥有资深艺术顾问和先进交易平台，安全物流，快捷支付。</div>
                </li>
                <li>
                    <div class="ico">
                        <img src="/App_Themes/UI/image/nimg36_2.png" /></div>
                    <h1>
                        保真</h1>
                    <div class="content">
                        有阵容强大的艺术评鉴团队，确保平台上的商品货真价实。</div>
                </li>
                <li>
                    <div class="ico">
                        <img src="/App_Themes/UI/image/nimg36_3.png" /></div>
                    <h1>
                        保值</h1>
                    <div class="content">
                        每一款商品都是具备艺术价值的作品，助您的资产保值、增值。</div>
                </li>
            </ul>
        </div>
        <div class="foot_02">
            <ul>
                <li>
                    <div class="ewm">
                        <img width="70" height="70" src="<%=si_model.WebWeiXinImage.JEP() %>" /></div>
                    <div class="imgDiv">
                        <img src="/App_Themes/UI/image/nimg92_1.png" width="66" height="66" /></div>
                    <div class="content">
                        请扫描左侧二维码<br />
                        添加物禅官方微信</div>
                </li>
                <li>
                    <div class="ewm">
                        <img width="70" height="70" src="<%=si_model.WebWeiBoImage.JEP() %>" /></div>
                    <div class="imgDiv">
                        <img src="/App_Themes/UI/image/nimg92_2.png" width="66" height="66" /></div>
                    <div class="content">
                        请扫描左侧二维码<br />
                        添加物禅官方微博</div>
                </li>
                <li>
                    <dl>
                        <dd>
                            <img src="/App_Themes/UI/image/nimg40_1.png" /><em><%=si_model.WebContactPhone %></em></dd>
                        <dd>
                            <img src="/App_Themes/UI/image/nimg40_2.png" /><%=si_model.WebContactEmail %></dd>
                    </dl>
                </li>
            </ul>
        </div>
        <div class="foot_03">
            <%--<h2>
                <a href="<%=Url.Action("Faq","Home",new{HC_ID=hcc_gsjj.id}) %>">公司简介</a>|<a href="<%=Url.Action("Faq","Home",new{HC_ID=hcc_jrwm.id}) %>">加入我们</a></h2>--%>
            <%=si_model.WebBottomInfo%>
        </div>
        <!--footBgEnd-->
    </div>
</div>
<style>
    .sideBar .list li { height: 84px; }
    .sideBar .list a { height: auto; }
</style>
<div class="sideBar">
    <div class="list">
        <ul>
            <li>
                <div>
                    <a href="<%=qq_link %>">
                        <img src="/App_Themes/UI/image/nimg40_3.png" /></a>
                </div>
                <a href="<%=qq_link %>">联系客服</a></li>
            <li>
                <div class="CCM_Model">
                    <a href="<%=Url.Action("MyCollectGoodsList","Member") %>"><img src="/App_Themes/UI/image/nimg40_4.png" /></a>
                    <div class="CollectGoodsModel">
                    </div>
                </div>
                <a href="<%=Url.Action("MyCollectGoodsList","Member") %>">收藏夹</a></li>
            <%
                //获取购物车列表
                List<ShoppingCar> ShoppingCarList = new DealMvc.Core.ShoppingCar.BLL_ShoppingCar().ShoppingCarList(m_login.M_UID);
            %>
            <li id="collectBox" style="position:relative;"><em class="ShoppingCarModel"><a href="<%=Url.Action("ShoppingCarIndex","Member") %>">
                <%=ShoppingCarList.Count%></a></em><div class="CAM_Model">
                    <a href="<%=Url.Action("ShoppingCarIndex","Member") %>"><img src="/App_Themes/UI/image/nimg40_5.png" /></a>
                    <div class="CarAllNumberModel">
                    </div>
                </div>
                <a href="<%=Url.Action("ShoppingCarIndex","Member") %>">购物车</a></li>
        </ul>
    </div>
    <div class="tel">
        <div class="imgDiv">
            <img src="/App_Themes/UI/image/nimg40_6.png" /></div>
        <div class="content">
            客服<br />
            <%=si_model.BeforeSalePhone %></div>
    </div>
    <div class="btnDiv">
        <a href="#">
            <img src="/App_Themes/UI/image/nimg40_7.png" /></a></div>
</div>
<div class="pageBar">
    <ul>
    </ul>
</div>
