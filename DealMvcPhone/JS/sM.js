//
//使用说明:
//参数
//sMClass.sM_CssText
//sMClass.sM_Time
//
//调用
//$("body").showMessage("弹出消息");
//
////////////////////////////////////////////////

var sMClass = new Object();

sMClass = {
    sM_Text: "<div class='Dialog_Facebox_Alert_Popup'><table><tbody><tr><td class='Dialog_Facebox_Alert_TopLeft'></td><td class='Dialog_Facebox_Alert_Bg'></td><td class='Dialog_Facebox_Alert_TopReight'></td></tr><tr><td class='Dialog_Facebox_Alert_Bg'></td><td class='Dialog_Facebox_Alert_Body'>@MSG@</td><td class='Dialog_Facebox_Alert_Bg'></td></tr><tr><td class='Dialog_Facebox_Alert_BottomLeft'></td><td class='Dialog_Facebox_Alert_Bg'></td><td class='Dialog_Facebox_Alert_BottomReight'></td></tr></tbody></table></div>",
    sM_Time: 2400
};

$.fn.extend({
    showMessage: function (Message)
    {
        $(".Dialog_Facebox_Alert").remove();
        var sM_Text = sMClass.sM_Text;
        var sM_Time = sMClass.sM_Time;
        var msgObj = document.createElement("div");
        $(msgObj).html(sM_Text.replace("@MSG@", Message)).addClass("Dialog_Facebox_Alert");
        var aI = 0;
        var aInt = setInterval(function ()
        {
            try
            {
                aI++;
                if (aI > 2)
                    clearInterval(aInt);
                document.body.appendChild(msgObj);

                $(msgObj).css({ top: ($(window).height() / 2 + $(document).scrollTop() - $(msgObj).height() / 2), left: ($(window).width() / 2 - $(msgObj).width() / 2) }).show();

                clearInterval(aInt);

                setTimeout(function ()
                {
                    $(msgObj).animate({ top: parseInt($(msgObj).css("top")) - 60, opacity: 'toggle' }, { duration: 600 });
                }, sM_Time);
            }
            catch (e) { }
        }, 20
        );
    }

});

 