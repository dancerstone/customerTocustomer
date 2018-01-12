<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/css/password.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainTop" runat="server">
    <div class="PhoneTop2">
        <h1>
            找回密码</h1>
        <a class="btnL" href="<%=Url.Action("Login","User") %>">
            <img src="/App_Themes/images/img_goback.png" /></a>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {

            $(".div_phone").click(function () {
                $(this).addClass("select");
                $(this).parent().addClass("select");
                $(".div_email").removeClass("select");
                $(".div_email").parent().removeClass("select");
                $(".phone_div").show();
                $(".email_div").hide();
            });
            $(".div_email").click(function () {
                $(this).addClass("select");
                $(this).parent().addClass("select");
                $(".div_phone").removeClass("select");
                $(".div_phone").parent().removeClass("select");
                $(".phone_div").hide();
                $(".email_div").show();
            });

        });
    </script>
    <div class="fp_content">
        <div class="selectDiv">
            <div class="sd_left sd select">
                <div class="div_phone div_twoc select">
                </div>
            </div>
            <div class="sd_right sd">
                <div class="div_email div_twoc">
                </div>
            </div>
        </div>
        <div class="wal login">
            <!--wal-->
            <div class="LC_Content">
                <div class="phone_div">
                    <form method="post" class="submit_info_phone">
                    <ul>
                        <li>
                            <input type="text" class="input1 phone_value" name="PhoneNumber" placeholder="请输入手机号" /></li>
                        <li class="regli mb35">
                            <input type="text" maxlength="4" name="WebCode" placeholder="请输入验证码" />
                            <img src="/VerifyCodeImage.ashx" onclick="getNewCode(this)" title="单击刷新验证码" alt="单击刷新验证码"
                                class="reg_webcode" />
                            <script>
                                function getNewCode(obj) {
                                    obj.src = "/VerifyCodeImage.ashx?q=" + Math.random();
                                }
                            </script>
                            <span class="clear_f"></span></li>
                        <li>
                            <div class="btnDiv">
                                <input type="submit" class="btn_next_phone" value="下一步" /></div>
                        </li>
                    </ul>
                    </form>
                </div>
                <div class="email_div" style="display: none;">
                    <form method="post" class="submit_info_email">
                    <ul>
                        <li>
                            <input type="text" class="input1 email_value" name="EmailNumber" placeholder="请输入邮箱" /></li>
                        <li class="regli mb35">
                            <input type="text" maxlength="4" class="input1 input2 webcode_email" name="WebCode"
                                placeholder="请输入验证码" />
                            <img src="/VerifyCodeImage.ashx" onclick="getNewCode(this)" title="单击刷新验证码" alt="单击刷新验证码"
                                class="reg_webcode" />
                            <script>
                                function getNewCode(obj) {
                                    obj.src = "/VerifyCodeImage.ashx?q=" + Math.random();
                                }
                            </script>
                            <span class="clear_f"></span></li>
                        <li>
                            <div class="btnDiv">
                                <input type="submit" class="btn_next_email" value="下一步" /></div>
                </div>
                </li> </ul> </form>
            </div>
        </div>
        <!--walEnd-->
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainBottom" runat="server">
    <%Html.RenderPartial("~/Views/Shared/foot.ascx"); %>
</asp:Content>
