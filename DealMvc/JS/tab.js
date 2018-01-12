
function SetWinHeight(obj)
{
    //    var AHeight = 50;
    //    try
    //    {
    //        var win = obj;
    //        if (document.getElementById)
    //        {
    //            if (win && !window.opera)
    //            {
    //                if (win.contentDocument && win.contentDocument.body.offsetHeight)
    //                {
    //                    win.height = parseInt(win.contentDocument.body.offsetHeight) + AHeight + "px";
    //                }
    //                else if (win.Document && win.Document.body.scrollHeight)
    //                {
    //                    win.height = parseInt(win.Document.body.scrollHeight) + AHeight + "px";
    //                }
    //            }
    //        }
    //    } catch (e) { }

    var cwin = obj;
    if (document.getElementById)
    {
        var Jheight = parseInt($(top.window).height()) - 155;
        var Cheight = 0;
        if (cwin && !window.opera)
        {
            if (cwin.contentDocument && cwin.contentDocument.body.offsetHeight)
                Cheight = cwin.contentDocument.body.offsetHeight; //FF NS
            else if (cwin.Document && cwin.Document.body.scrollHeight)
                Cheight = cwin.Document.body.scrollHeight; //IE
        }
        else
        {
            if (cwin.contentWindow.document && cwin.contentWindow.document.body.scrollHeight)
                Cheight = cwin.contentWindow.document.body.scrollHeight; //Opera
        }
        //alert(Jheight + "|" + Cheight);

        //cwin.height = Math.max(Jheight, Cheight) + "px";
        cwin.height = Jheight + "px";
        $(".leftMenu").height((Jheight - 100) + "px");
    }

}
var Mindex = 1;
var MMenuColor = new Array();
$(function ()
{
    $(".menu > .sub").hide().find("div p:not('.title')").hide().css("margin-left", "18px");
    $(".menu > .sub > div").find("div").hide().css("margin-left", "18px");
    $(".menu > a").css("display", "block").addClass("menua").each(
                function (i, v)
                {
                    $(v).click(function ()
                    {
                        $(".menua2").removeClass("menua2").addClass("menua");
                        $(v).removeClass("menua").addClass("menua2");
                        $(".leftMenu").html($(".menu > .sub").eq(i).html());
                        $(".cltop").html($(v).html());
                        var Color = $(".cltop").find("span").attr("color") == undefined ? "transparent" : $(".cltop").find("span").attr("color");
                        $(".MenuC").css("background", Color);
                        $(".leftMenu p").css("line-height", "26px");
                        $(".leftMenu a").css("color", "#006699").css("text-decoration", "none").each(function (u, ele)
                        {
                            if ($(ele).parent().hasClass("title"))
                            { $(ele).before("<span class='ico2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>"); }
                            else
                            {
                                var Q_url = $(ele).attr("href").split("?")[0].toLowerCase();

                                if (!CheckColorMenuHas(Q_url))
                                { MMenuColor.push(new Array(Q_url, Color)); }

                                $(ele).before("<span class='ico3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>");
                                $(ele).click(function ()
                                {

                                    var title = $(this).text();
                                    var url = $(this).attr("href");
                                    //alert(url);

                                    return AddLabel(title, url, "");

                                });
                            }
                        });
                        $(".leftMenu div .title a").toggle(
                            function ()
                            {
                                $(this).parent().parent().children("*:not('.title')").show();
                                $(this).parent().find(".ico2").removeClass("ico2").addClass("ico1");
                            },
                            function ()
                            {
                                $(this).parent().parent().children("*:not('.title')").hide();
                                $(this).parent().find(".ico1").removeClass("ico1").addClass("ico2");
                            }
                        ).click();
                    });
                }
            );

    $(".Ifr_Title .reload").click(function ()
    {
        try
        {
            $(".Ifr_Content iframe:visible").attr("src", $(".Ifr_Content iframe:visible").attr("src"));
        } catch (e) { }
    });

    $(".menu").find("br").remove();
    $(".menu").show();
    $(".menu > a").eq(0).css("margin-left", "20px").click();

});

function AddLabel(title, url, url2)
{
    //判断是否已经打开,是就直接选中
    var IsHas = false;
    $(".Ifr_Content > iframe").each(function (i, v)
    {
        if ($(v).attr("src").toLowerCase() != url.toLowerCase()) return true;
        else
        {
            var inf = parseInt($(v).attr("id").replace("Ifr", ""));
            $("#Tit" + inf + " > div:not('.reload')").eq(0).click();
            IsHas = true;
            return false;
        }
    });
    if (IsHas) return false;

    var Colorr = "#FFFFFF";
    if (url2 != undefined && url2 != "")
    {
        //alert(url2);
        Colorr = GetColorMenu(url2.split("?")[0].toLowerCase());

        if (!CheckColorMenuHas(url.split("?")[0].toLowerCase()))
        { MMenuColor.push(new Array(url.split("?")[0].toLowerCase(), Colorr)); }
        //alert(Colorr_o);
    }
    else
    {
        Colorr = GetColorMenu(url.split("?")[0].toLowerCase());
    }


    //class --  ifr_title ifr_title_now
    var a_title = "<div id='Tit" + Mindex + "' class='fl ifr_title_now'><div class='left_bj fl'>&nbsp;&nbsp;&nbsp;</div><div class='content_bj fl'>" + title + "</div><div class='close_bj fl'></div><div class='right_bj fl'>&nbsp;&nbsp;&nbsp;</div><div class='cb' style='/%display:none;%/'></div></div>";
    var a_iframe = "<iframe id='Ifr" + Mindex + "' name='Ifr" + Mindex + "' frameborder='0' onload='Javascript:SetWinHeight(this)'   height='300' style='overflow-y:scroll;overflow-x:auto; width:100%;border:0px;'  src='" + url + "'/>"; //scrolling='no'

    $(".Ifr_Title > div").removeClass("ifr_title_now").addClass("ifr_title");
    $(".Ifr_Content > iframe").hide();

    $(".Ifr_Title").append(a_title);
    $(".Ifr_Content").append(a_iframe);

    //
    var Colorr = GetColorMenu(url.split("?")[0].toLowerCase());
    //alert(Colorr);
    $(".Ifr_Title .ifr_title_now .content_bj").css("backgroundColor", Colorr);
    if (Colorr.toLowerCase() == "#ffffff" || Colorr.toLowerCase() == "#fff")
    {
        $(".Ifr_Title .ifr_title_now").css("color", "#666");
    }
    //
    CfadeTo();

    $(".Ifr_Title > div:not('.reload')").each(
                                        function (ii, vv)
                                        {

                                            $(vv).click(function ()
                                            {
                                                $(".Ifr_Title > div").removeClass("ifr_title_now").addClass("ifr_title");
                                                $(vv).removeClass("ifr_title").addClass("ifr_title_now");
                                                var ind = $(vv).attr("id").replace("Tit", "");
                                                $(".Ifr_Content > iframe").hide();
                                                $("#Ifr" + ind).show();
                                                SetWinHeight($("#Ifr" + ind)[0]);

                                                //
                                                CfadeTo();
                                            });

                                            /*
                                            $(vv).hover
                                            (
                                            function ()
                                            {
                                            if ($(this).hasClass("ifr_title"))
                                            {
                                            $(this).removeClass("ifr_title");
                                            $(this).addClass("ifr_title_now2");
                                            }
                                            }
                                            ,
                                            function ()
                                            {
                                            if ($(this).hasClass("ifr_title_now2"))
                                            {

                                            $(this).addClass("ifr_title");
                                            $(this).removeClass("ifr_title_now2");
                                            }
                                            }
                                            );
                                            */

                                            $(vv).find(".close_bj").click(function ()
                                            {
                                                var ind = $(this).parent().attr("id").replace("Tit", "");

                                                var nextLength = $(this).parent().nextAll().length;
                                                var prevLength = $(this).parent().prevAll().length;

                                                if (nextLength > 0)
                                                {
                                                    $(this).parent().next().removeClass("ifr_title").addClass("ifr_title_now");
                                                    var ind_n = $(this).parent().next().attr("id").replace("Tit", "");
                                                    $("#Ifr" + ind_n).show();
                                                    SetWinHeight($("#Ifr" + ind_n)[0]);
                                                }
                                                else if (prevLength > 0)
                                                {
                                                    $(this).parent().prev().removeClass("ifr_title").addClass("ifr_title_now");
                                                    var ind_n = $(this).parent().prev().attr("id").replace("Tit", "");
                                                    $("#Ifr" + ind_n).show();
                                                    SetWinHeight($("#Ifr" + ind_n)[0]);
                                                }
                                                else
                                                {
                                                    return;
                                                }
                                                $(this).parent().remove();
                                                $("#Ifr" + ind).remove();
                                                CfadeTo();
                                            });
                                        }
                                    );

    Mindex++;
    return false;
}
function CheckColorMenuHas(Q_url)
{
    var IsHas = false;
    for (var intt = 0; intt < MMenuColor.length; intt++)
    {
        if (MMenuColor[intt][0] == Q_url) { IsHas = true; break; }
    }
    return IsHas;
}
function GetColorMenu(Q_url)
{
    var NowColor = "transparent";
    for (var intt = 0; intt < MMenuColor.length; intt++)
    {
        if (Q_url.indexOf(MMenuColor[intt][0]) >= 0) { NowColor = MMenuColor[intt][1]; break; }
    }
    if (NowColor == "transparent") NowColor = "#FFFFFF";
    return NowColor;
}
function CfadeTo()
{
    $(".Ifr_Title > div:not('.reload')").each(
                                        function (ii, vv)
                                        {
                                            if ($(vv).hasClass("ifr_title_now"))
                                                $(vv).find(".content_bj").fadeTo(20, 1);
                                            else
                                                $(vv).find(".content_bj").fadeTo(20, 0.6);
                                        });
}
function closeHiddLables()
{
    $(".ifr_title:not('.reload')").remove();
    $(".Ifr_Content").find("iframe:hidden").remove();
}