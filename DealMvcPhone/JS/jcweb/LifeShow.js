$(function () {

    $(".praise_div").click(function () {
        var this_v = $(this);
        $.ajax({
            url: '/Home/OperationPraise',
            async: false,
            type: "POST",
            data: { lifeID: $(this).attr("lifeID"), r: new Date().getTime() },
            success: function (msg) {
                var json = $.parseJSON(msg);
                if (json.error == 0) {
                    var count = 0;
                    if (this_v.next().text().trim() == undefined || this_v.next().text().trim() == null || this_v.next().text().trim() == "") {
                        count = 0;
                    } else {
                        count = this_v.next().text().trim();
                    }
                    $(".no_data_praise").remove();
                    this_v.next().text(parseInt(count) + parseInt(1))
                    $(".list_count_praise").text(parseInt(count) + parseInt(1));
                    $(".ul_praise").prepend(json.message);
                    $(".tab li").removeClass("liNow").eq(1).addClass("liNow");
                    $(".tabContent").hide();
                    $(".tabContent2").show();
                    this_v.next().removeClass("msg_null").addClass("msg_full");
                } else {
                    $("body").showMessage(json.message);
                }
            }
        });
    });

    $(".btn_submit_comment").click(function () {
        var c_ontent = $(this).prev().val();
        if (c_ontent == "" || c_ontent == null) {
            $("body").showMessage("请输入评价内容");
            return false;
        }
        var this_v = $(this);
        $.ajax({
            url: '/Home/LifeComment',
            async: false,
            type: "POST",
            data: { lifeID: $(this).attr("lifeID"), content: c_ontent, r: new Date().getTime() },
            success: function (msg) {
                var json = $.parseJSON(msg);
                if (json.error == 0) {
                    var count = 0;
                    if ($(".list_count_comment").text().trim() == undefined || $(".list_count_comment").text().trim() == null || $(".list_count_comment").text().trim() == "") {
                        count = 0;
                    } else {
                        count = $(".list_count_comment").text().trim();
                    }
                    $(".no_data_comment").remove();
                    this_v.next().text(parseInt(count) + parseInt(1));
                    this_v.prev().val("");
                    $(".list_count_comment").text(parseInt(count) + parseInt(1));
                    $(".ul_comment").prepend(json.message);
                    $(".tab li").removeClass("liNow").eq(0).addClass("liNow");
                    $(".tabContent").show();
                    $(".tabContent2").hide();
                } else {
                    $("body").showMessage(json.message);
                }
            }
        });
    });
});