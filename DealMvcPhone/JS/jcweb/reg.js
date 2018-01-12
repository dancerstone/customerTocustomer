
$(function () {
    $("input").keypress(function () {
        $(this).next().fadeOut("slow");
        $(this).next().next().fadeOut("slow");
    });

    //用户名
    $(".loginname").blur(function () {
        var obj = $(this);
        if (obj.val().replace(/(^\s*)|(\s*$)/g, "").length == 0) {
            obj.next().text("手机号/邮箱不能为空").fadeIn(200);
            obj.next().next().fadeOut("slow");
            obj.attr("data-verifyed", "false");
            return;
        }

        var reg_email = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        var reg_phone = /^1[3|5|8][0-9]{9}$/;
        if (!reg_email.test(obj.val()) && !reg_phone.test(obj.val())) {
            obj.next().text("手机号/邮箱格式不正确").fadeIn(200);
            obj.next().next().fadeOut("slow");
            obj.attr("data-verifyed", "false");
            return;
        }
        if (reg_email.test(obj.val())) {
            $("input[name='NT']").val("e");
        } else if (reg_phone.test(obj.val())) {
            $("input[name='NT']").val("p");
        }

        $.ajax({
            url: '/User/CheckUserName',
            async: false,
            dataType: 'json',
            type: "POST",
            data: { n: obj.val(), r: new Date().getTime() },
            success: function (msg) {
                json = [];
                eval("json=" + msg);
                if (json.msg == 'err') {
                    obj.next().text("该手机号/邮箱已被注册").fadeIn(200);
                    obj.next().next().fadeOut("slow");
                    obj.attr("data-verifyed", "false");
                    return false;
                } else if (json.msg == 'success') {
                    obj.next().next().fadeIn(200);
                    obj.attr("data-verifyed", "true");
                }
            }
        });

    });
    //密码
    $(".pwd").blur(function () {
        var obj = $(this);
        if (obj.val().length < 6 || obj.val().length > 18) {
            obj.next().text("密码为6-20位字符").fadeIn(200);
            obj.next().next().fadeOut("slow");
            obj.attr("data-verifyed", "false");
            return;
        } else {
            obj.next().next().fadeIn(200);
            obj.attr("data-verifyed", "true");
        }
    });
    //重复密码
    $(".pwd_again").blur(function () {
        var obj = $(this);
        if (obj.val().length < 6 || obj.val().length > 18) {
            obj.next().text("密码为6-20位字符").fadeIn(200);
            obj.next().next().fadeOut("slow");
            obj.attr("data-verifyed", "false");
            return;
        } else if (obj.val() != $(".pwd").val()) {
            obj.next().text("两次密码不一致").fadeIn(200);
            obj.next().next().fadeOut("slow");
            obj.attr("data-verifyed", "false");
            return;
        } else {
            obj.next().next().fadeIn(200);
            obj.attr("data-verifyed", "true");
        }
    });
    //验证码
    $(".WebCode").blur(function () {
        var obj = $(this);
        if (obj.val().length != 4) {
            obj.next().text("验证码为4位字符").fadeIn(200);
            obj.next().next().fadeOut("slow");
            obj.attr("data-verifyed", "false");
            return;
        } else {
            obj.next().next().fadeIn(200);
            obj.attr("data-verifyed", "true");
        }

    });

    $(".btn_regsubmit").click(function () {
        $(".loginname").blur();
        $(".pwd").blur();
        $(".pwd_again").blur();
        $(".WebCode").blur();

        var ischeck = "";
        $("input").each(function (i, v) {
            if ($(v).attr("data-verifyed") == "false") {
                ischeck = "false";
                return false;
            } else {
                ischeck = "true";
            }
        });
        if (ischeck == "" || ischeck == "false") {
            return;
        } else if (ischeck == "true") {
            $("#reg_form").submit();
        }

    });

});


$(function () {
    var keystring = ""; //记录按键的字符串
    function keydown(e) {
        var e = e || event;
        var currKey = e.keyCode || e.which || e.charCode;
        if ((currKey > 7 && currKey < 14) || (currKey > 31 && currKey < 47)) {
            switch (currKey) {
                case 8: keyName = "[退格]"; break;
                case 9: keyName = "[制表]"; break;
                case 13:
                    keyName = "[回车]";
                    $(".btn_regsubmit").click();
                    break;
                case 32: keyName = "[空格]";
                    break;
                case 33: keyName = "[PageUp]"; break;
                case 34: keyName = "[PageDown]"; break;
                case 35: keyName = "[End]"; break;
                case 36: keyName = "[Home]"; break;
                case 37: keyName = "[方向键左]"; break;
                case 38: keyName = "[方向键上]"; break;
                case 39: keyName = "[方向键右]"; break;
                case 40: keyName = "[方向键下]"; break;
                case 46: keyName = "[删除]"; break;
                default: keyName = ""; break;
            }
            keystring += keyName;
        }
        $("content").innerHTML = keystring;
    }
    function keyup(e) {
        $("content").innerHTML = keystring;
    }
    document.onkeydown = keydown;
    document.onkeyup = keyup;
});
