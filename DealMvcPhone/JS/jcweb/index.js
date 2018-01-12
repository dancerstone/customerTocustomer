$(function () {

    var gd_height = 0;
    $(window).bind("scroll", function () {
        $(".indexPart1").each(function (i, v) {
            var imgD = $(v).find(".IndexCateBigPic").offset();
            if (parseInt(parseFloat($(window).scrollTop()) + 950) >= parseInt(imgD.top) || (parseInt($(window).scrollTop()) >= parseInt(imgD.top) && parseInt($(window).scrollTop()) <= parseInt(parseInt(imgD.top) + parseInt($(v).find(".IndexCateBigPic").height())))) {
                if (parseInt($(window).scrollTop()) >= parseInt(parseInt(imgD.top) + parseInt($(v).find(".IndexCateBigPic").height())))
                    return true;
                if ((navigator.userAgent.indexOf('MSIE') >= 0)
    && (navigator.userAgent.indexOf('Opera') < 0)) {
                    $(v).find(".IndexCateBigPic").css("backgroundPositionX", "center ").css("backgroundPositionY", parseInt(parseInt($(window).scrollTop() / 2) - parseInt((i + 1) * 550)) + "px");
                } else if (navigator.userAgent.indexOf('Firefox') >= 0) {
                    $(v).find(".IndexCateBigPic").css("background-position", "center " + parseInt(parseInt($(window).scrollTop() / 2) - parseInt((i + 1) * 550)) + "px");
                } else {
                    //$(v).find(".IndexCateBigPic").css("background-position", "center -" + parseInt(parseInt($(window).scrollTop() / 2) - parseInt((i + 1) * 550)) + "px");
                    $(v).find(".IndexCateBigPic").css("background-position", "center -" + parseInt(parseInt($(window).scrollTop() / 2) - parseInt((i + 1) * 550) + 400) + "px");
                    //$(v).find(".IndexCateBigPic").css("backgroundPositionX", "center ").css("backgroundPositionY", parseInt(parseInt($(window).scrollTop() / 2) - parseInt((i + 1) * 550)) + "px");
                }
            }
        });
    });

});