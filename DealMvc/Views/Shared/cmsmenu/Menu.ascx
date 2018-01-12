<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<span class="sub">
    <p>
        <a class="Icon IconE default_open" href='<%=Url.Action("SiteBase", "CmsSite")%>'>网站基本信息</a>
    </p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("EmailBase", "CmsSite")%>'>网站邮箱设置</a>
    </p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("MessageBase", "CmsSite")%>'>短信接口配置</a>
    </p>
    <%-- <p>
        <a class="Icon IconE" href='<%=Url.Action("UpLoadFileBase", "CmsSite")%>'>网站上传文件设置</a>
    </p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("PointsSetAE", "CmsSite")%>'>网站积分设置</a>--%>
    </p>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">支付信息设置</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("AlipayBase", "CmsSite")%>'>支付宝信息设置</a></p>
       <%-- <p>
            <a class="Icon IconE" href='<%=Url.Action("TenpayBase", "CmsSite")%>'>网银在线设置</a></p>--%>
    </div>
    <%-- <div>
        <p class="title">
            <a class="Icon IconE" href="#">城市管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("SiteCityList", "CmsSite")%>'>城市列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("SiteCityAE", "CmsSite")%>'>增加城市</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">商圈管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("BusinessCircleList", "CmsSite")%>'>商圈列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("BusinessCircleAE", "CmsSite")%>'>增加商圈</a></p>
    </div>--%>
</span><a><span class="Icon IconE">系统设置</span></a>
<br />
<span class="sub">
    <%--<div>
        <p class="title">
            <a class="Icon IconE" href="#">问答分类</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("QuestionsAnswersCateList", "CmsInformation")%>'>
                问答分类列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("QuestionsAnswersCateAE", "CmsInformation")%>'>
                增加问答分类</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">问答管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("QuestionsList", "CmsInformation")%>'>问题列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("QuestionsAE", "CmsInformation")%>'>添加问题</a></p>
    </div>--%>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">帮助中心</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("HelpCenterCateList", "CmsInformation")%>'>
                帮助分类列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("HelpCenterCateAE", "CmsInformation")%>'>增加帮助分类</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("HelpCenterInfoList", "CmsInformation")%>'>
                帮助信息列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("HelpCenterInfoAE", "CmsInformation")%>'>增加帮助信息</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">广告管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("AdvertisingInfoList", "CmsInformation")%>'>
                广告列表</a></p>
        <%--<p>
            <a class="Icon IconA" href='<%=Url.Action("AdvertisingInfoAE", "CmsInformation")%>'>
                增加广告</a></p>--%>
    </div>
</span><a><span class="Icon IconE">信息中心</span></a>
<br />
<span class="sub">
    <p>
        <a class="Icon IconE" href='<%=Url.Action("AdminList", "CmsAdmin")%>'>帐号列表</a></p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("AEAdmin", "CmsAdmin")%>'>增加帐号</a></p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("AdminSortList", "CmsAdminSort")%>'>帐号类型列表</a></p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("AEAdminSort", "CmsAdminSort")%>'>增加帐号类型</a></p>
</span><a><span class="Icon IconE">帐号中心</span></a>
<br />
<span class="sub">
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">会员信息</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberList", "CmsMember")%>'>会员列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberAccountList", "CmsMember")%>'>会员账户列表</a></p>
        <%--<p>
            <a class="Icon IconE" href='<%=Url.Action("MemberProfileList", "CmsMember")%>'>会员资料列表</a></p>--%>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberAPIList", "CmsMember")%>'>会员API登录列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("ZFBBandInfoList","CmsMember")%>'>会员支付宝列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("ApplyWithdrawList", "CmsMember")%>'>提现申请列表</a></p>
        <%--<p>
            <a class="Icon IconE" href='<%=Url.Action("WithdrawLog", "CmsMember")%>'>商家提现记录</a></p>--%>
    </div>
    <%--<div>
        <p class="title">
            <a class="Icon IconE" href="#">会员等级</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberLevelList", "CmsMember")%>'>会员等级列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberLevelAE", "CmsMember")%>'>新增会员等级</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">会员认证</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("SafetyCertificationList", "CmsMember")%>'>
                会员认证列表</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">会员汽车</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MyCarList", "CmsMember")%>'>会员汽车列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MyCarAE", "CmsMember")%>'>新增会员汽车</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">密保管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("SecurityQuestionList", "CmsMember")%>'>密保问题列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("SecurityQuestionAE", "CmsMember")%>'>添加密保问题</a></p>
    </div>--%>
</span><a><span class="Icon IconE">会员管理</span></a>
<br />
<%--<span class="sub">
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">商家信息</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("StoreShopList", "CmsStore")%>'>商家列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("CommissionAndServiceMoney", "CmsStore")%>'>商家提拥列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("WaitAuditStoreShopList", "CmsStore")%>'>待审核商家</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("ReportInfoList", "CmsStore")%>'>商家举报管理</a>
        </p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">会员卡管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberCardList", "CmsStore")%>'>商家会员卡列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("MemberGetUseCardList", "CmsStore")%>'>绑定的会员卡记录</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">商家提现管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("ApplyWithdrawList", "CmsStore")%>'>提现申请列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("WithdrawLog", "CmsStore")%>'>商家提现记录</a></p>
    </div>
</span><a><span class="Icon IconE">商家管理</span></a>
<br />--%>
<span class="sub">
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">美学杂志管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("Magazine_XLYList", "CmsMagazine")%>'>杂志列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("Magazine_XLYAE", "CmsMagazine")%>'>添加杂志</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">定制攻略管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("CustomRaidersCate_XLYList", "CmsMagazine")%>'>
                攻略分类列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("CustomRaidersCate_XLYAE", "CmsMagazine")%>'>
                添加攻略分类</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("CustomRaiders_XLYList", "CmsMagazine")%>'>
                定制攻略列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("CustomRaiders_XLYAE", "CmsMagazine")%>'>添加定制攻略</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">设计师管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("Designers_XLYList", "CmsMagazine")%>'>设计师列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("Designers_XLYAE", "CmsMagazine")%>'>添加设计师</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">美学家管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("LifeEstheticianNew_XLYList", "CmsMagazine")%>'>
                美学家信息列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("LifeEstheticianNew_XLYAdd", "CmsMagazine")%>'>
                添加美学家信息</a></p>
    </div>
</span><a><span class="Icon IconE">杂志与攻略</span></a>
<br />
<span class="sub">
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">产品分类</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("GoodsCate_XLYList", "CmsGoods")%>'>分类列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("GoodsCate_XLYAE", "CmsGoods")%>'>添加分类</a></p>
    </div>
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">产品管理</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("Goods_XLYList", "CmsGoods")%>'>产品列表</a></p>
        <p>
            <a class="Icon IconA" href='<%=Url.Action("Goods_XLYAE", "CmsGoods")%>'>添加产品</a></p>
    </div>
</span><a><span class="Icon IconE">产品管理</span></a>
<br />
<span class="sub">
    <div>
        <p class="title">
            <a class="Icon IconE" href="#">订单信息</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("OrdersList", "CmsOrder")%>'>订单列表</a></p>
        <p>
            <a class="Icon IconE" href='<%=Url.Action("WebRefundsReturns", "CmsOrder")%>'>返修退换货列表</a></p>
    </div>
</span><a><span class="Icon IconE">订单管理</span></a>
<br />
<span class="sub">
    <p>
        <a class="Icon IconE" href='<%=Url.Action("ExperienceHallList", "CmsGoods")%>'>信息列表</a></p>
    <p>
        <a class="Icon IconE" href='<%=Url.Action("ExperienceHallAE", "CmsGoods")%>'>添加信息</a></p>
</span><a><span class="Icon IconE">3D体验馆</span></a>
<br />
