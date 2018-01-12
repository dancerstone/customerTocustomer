
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


    var reg = /^1[3|5|8][0-9]{9}$/;
    var reg_e = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;

    $("input").keypress(function () {
        $(this).next().fadeOut("slow");
    });

    $(".phone_value").blur(function () {
        if (!reg.test($(this).val())) {
            $(this).next().text("手机号格式不正确").fadeIn(200);
            $(this).attr("data-verifyed", "false");
            return;
        } else {
            $(this).attr("data-verifyed", "true");
        }
    });

    $(".webcode_phone").blur(function () {
        if ($(this).val().length != 4) {
            $(this).next().text("验证码为4位字符").fadeIn(200);
            $(this).attr("data-verifyed", "false");
            return;
        } else {
            $(this).attr("data-verifyed", "true");
        }
    });

    //下一步操作-手机
    $(".btn_next_phone").click(function () {
        var phone = $(".phone_value").val();
        var webcode_phone = $(".webcode_phone").val();

        $(".phone_value").blur();
        $(".webcode_phone").blur();

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
            $(".submit_info_phone").submit();
        }
    });

    $(".email_value").blur(function () {
        if (!reg_e.test($(this).val())) {
            $(this).next().text("邮箱格式不正确").fadeIn(200);
            $(this).attr("data-verifyed", "false");
            return;
        } else {
            $(this).attr("data-verifyed", "true");
        }
    });

    $(".webcode_email").blur(function () {
        if ($(this).val().length != 4) {
            $(this).next().text("验证码为4位字符").fadeIn(200);
            $(this).attr("data-verifyed", "false");
            return;
        } else {
            $(this).attr("data-verifyed", "true");
        }
    });


    //下一步操作-邮箱
    $(".btn_next_email").click(function () {
        var phone = $(".email_value").val();
        var webcode_email = $(".webcode_email").val();

        $(".email_value").blur();
        $(".webcode_email").blur();

        var ischeck = "";
        $(" input").each(function (i, v) {
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
            $(".submit_info_email").submit();
        }
    });
});

