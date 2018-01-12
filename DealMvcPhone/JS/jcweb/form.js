//-------弹出对话框	
function prompt_fun(a) {
    $('.Layer1').remove();
    $(a).after("<div class='Layer1'></div>");
    var aWidth = $(a).width();
    var aHeight = $(a).height();
    var bodyWidth = $("body").width();
    var bodyHeight = $("body").height();
    var windowHeight = $(window).height();
    if (bodyHeight > windowHeight) {
        $(".Layer1").height(bodyHeight+"px");
    } else {
        $(".Layer1").height(windowHeight + "px");
    }
    $(".Layer1").width(bodyWidth+"px");
    $(a).css({ left: (bodyWidth - aWidth) / 2, top: $(window).scrollTop() + (windowHeight - aHeight) / 2 });
    $(".Layer1").fadeTo("fast", 0.5);
    $(a).show();
    $(".Layer1").click(function () {
        close_prompt_fun(a);
    })
}
function close_prompt_fun(a) {
    $(a).hide();
    $(".Layer1").fadeOut("slow", function () {
        $(".Layer1").remove();
    });
}