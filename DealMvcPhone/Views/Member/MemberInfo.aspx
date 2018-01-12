<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/PCASClass.js" type="text/javascript"></script>
    <link href="/App_Themes/css/Member.css" rel="stylesheet" type="text/css" />
    <link href="/JS/systemtime/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="/JS/systemtime/jquery.datetimepicker.js" type="text/javascript"></script>
    <script src="/JS/jcweb/form.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            我的资料</h1>
        <a class="btnL" href="<%=Url.Action("Index","Member") %>">
            <img src="/App_Themes/images/img_goback.png" /></a> <a style="color:#fff;" class="btnR saveMemberInfo">保存
            </a>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".saveMemberInfo").click(function () {
                $(".MemberInfoForm").submit();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%Member m_login = new DealMvc.Core.Member.BLL_Member().GetLoginMember();
      SiteInfo m_site = SiteInfo.GetModel(t => t.id > 0);
        
    %>
    <div class="MemberInfoContent">
        <div class="h15">
        </div>
        <div class="MemberInfo">
            <form class="MemberInfoForm" method="post" enctype="multipart/form-data">
            <ul>
                <li>头像：<input name="M_Avatar" type="file" style="width: 70%;" />
                    <br />
                    &nbsp;&nbsp;&nbsp;<img src="<%=m_login.M_IsPhoneHead ? m_login.M_Avatar.JEP() : (m_site.WebAddress + m_login.M_Avatar).JEP() %>"
                        width="30%" />
                </li>
                <li>昵称：<input type="text" name="M_UserName" value="<%=m_login.M_UserName %>" /></li>
                <li>性别：
                    <select name="M_Sex" style="width: 20%;">
                        <option <%=m_login.M_Sex=="男"?"selected='selected'":"" %> value="男">男</option>
                        <option <%=m_login.M_Sex=="女"?"selected='selected'":"" %> value="女">女</option>
                    </select>
                </li>
                <li>生日：
                    <input type="text" name="M_BirthDay" class="input1 M_BirthDay" value="<%=m_login.M_BirthDay %>" />
                    <script type="text/javascript">
                        $('.M_BirthDay').datetimepicker({ lang: 'ch', timepicker: false, format: 'Y-m-d' });
                    </script>
                </li>
                <li>手机：<%=m_login.M_IsSetPhone ? m_login.M_Phone : "未绑定"%></li>
                <li>邮箱：<%=m_login.M_IsSetEmail ? m_login.M_Email : "未绑定"%></li>
                <li>地址：
                    <select name="M_Province" style="width: 18%;">
                    </select>
                    <select name="M_City" style="width: 18%;">
                    </select>
                    <select name="M_Area" style="width: 18%;">
                    </select>
                    <script type="text/javascript">
                        $(function () {
                            new PCAS("M_Province", "M_City", "M_Area", "<%=m_login.M_Province %>", "<%=m_login.M_City %>", "<%=m_login.M_Area %>");
                        });
                    </script>
                </li>
                <li>详细地址：<input type="text" name="M_AddressDetail" value="<%=m_login.M_AddressDetail %>" /></li>
            </ul>
            </form>
        </div>
        <div class="h15">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
