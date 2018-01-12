
var font_count;
function WordLength(obj) {
    var oVal = obj.val();
    var oValLength = 0;
    if (oVal.replace(/\n*\s*/, '') == '') {
        oValLength = 0;
    } else {
        if (oVal.match(/[^ -~]/g) == null) {
            oValLength = oVal.length;
        }
        else {
            oValLength = oVal.length + oVal.match(/[^ -~]/g).length;
        }
    }
    return oValLength;
}
function Limit(obj, iNow, tit) {
    var oValLength = WordLength(obj);
    font_count = Math.floor((iNow - oValLength) / 2);
    if (font_count >= 0) {
        tit.html("<div class='tips inputwordcount' style='float: none;'>还可以输入<b class='wordcount'>" + font_count + "</b>个汉字</div>");
        return true;
    } else {
        tit.html("<div class='tips inputwordcount' style='float: none;'>已超出<b class='wordcount' style='color:red;'>" + Math.abs(font_count) + "</b>个汉字</div>");
        return false;
    }
    return font_count;
}

$(function () {
    $(".pageBtn1").click(function () {
        //检查封面必填
        var m_title = $("input[name='M_Title_FM']").val();
        var m_frontcover = $("input[name='M_FrontCover_FM']").val();
        var m_simpleRemarks = $("textarea[name='M_SimpleRemarks_FM']").val();
        var length_remarks = WordLength($("textarea[name='M_SimpleRemarks_FM']"));
        if (m_title == "" || m_title == null) {
            $("body").showMessage("请填写封面标题");
            return;
        }
        if (m_frontcover == "" || m_frontcover == null) {
            $("body").showMessage("请上传封面");
            return;
        }
        if (m_simpleRemarks == "" || m_simpleRemarks == null) {
            $("body").showMessage("请填写封面说明");
            return;
        }
        if (length_remarks > 500) {
            $("body").showMessage("说明字数超出，请适当调整");
            return;
        }
        //检查图片及说明
        $("[name='v_ids']").each(function (i, v) {
            var v_id = $(v).val();
            if (v_id == "FM") {
                return true;
            }
            var m_frontcover_ = $("input[name='M_FrontCover_" + v_id + "']").val();
            var m_simpleRemarks_ = $("textarea[name='M_SimpleRemarks_" + v_id + "']").val();
            var length_remarks_ = WordLength($("textarea[name='M_SimpleRemarks_" + v_id + "']"));

            if (m_frontcover_ == "" || m_frontcover_ == null) {
                $("body").showMessage("请上传图片");
                return;
            }
            if (m_simpleRemarks_ == "" || m_simpleRemarks_ == null) {
                $("body").showMessage("请填写图片说明");
                return;
            }
            if (length_remarks_ > 500) {
                $("body").showMessage("说明字数超出，请适当调整");
                return;
            }
        });

        $("#LE_Form").submit();
    });



    var oH2 = $(".inputwordcount"); //提示文字
    var oTextarea = $("#M_SimpleRemarks_FM"); //输入框
    var oButton = $("#submit_btn_live"); //按钮
    var wordcount = parseInt($(".wordcount").text()) * 2;

    //weibotext-keyup
    $("#M_SimpleRemarks_FM").keyup(function () {
        Limit(oTextarea, wordcount, oH2);
    });


});
window.onload = function () {
    //    var topWin = document.getElementById("keHDHDHDHD").contentWindow;
    //    topWin.document.body.setAttribute("style", "color:#fff;");
}