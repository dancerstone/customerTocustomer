<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<!--提示信息-->
<script type="text/javascript">
    var msg = '<%= ViewData["AlertMessage"]==null ? "" : ViewData["AlertMessage"] %>';
    if (msg == "") { msg += '<%= TempData["AlertMessage"]==null ? "" : TempData["AlertMessage"] %>'; }
    if (msg != "") { top.$("body").showMessage(msg); }
</script>
<!--防止重复提交-->
<script type="text/javascript">
    $(function () {
        var R_hidd = '<%=ViewData["___R_hidd"]==null?"":ViewData["___R_hidd"] %>';
        if (R_hidd != "") {
            $("form").each(function (i, v) { if ($(v).attr("target") != "_blank" && $(v).attr("target") != "subForm") { var hidd = "<input type='hidden' notsavevalue='true'  name='___R_hidd' value='" + R_hidd + "' />"; $(v).append(hidd); } });
        }
    });
</script>
