$(function () {
    //--
    var fadeFlashTime = new Array();
    $('.fadeFlash').find('li:first').fadeIn(500);
    $('.fadeFlash').each(function (i) {
        fadeFlashTime[i] = setInterval("fadeFlashFun(" + i + ")", 5000);
        $(this).find('.btnDiv').find('span').each(function (ii) {
            $(this).hover(
			function () {
			    clearInterval(fadeFlashTime[i]);
			    $('.fadeFlash').eq(i).find('.btnDiv').find('span').removeClass('spanNow');
			    $(this).addClass('spanNow');
			    $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).hide();
			    fadeFlashNow[i] = ii;
			    $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).fadeIn(500);
			    fadeFlashTime[i] = setInterval("fadeFlashFun(" + i + ")", 5000);
			},
			function () { }
				)
        })
        $(this).find('.rightBtn').click(function () {
            clearInterval(fadeFlashTime[i]);
            $('.fadeFlash').eq(i).find('.btnDiv').find('span').removeClass('spanNow');
            $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).hide();
            if (fadeFlashNow[i] < $('.fadeFlash').eq(i).find('li').length - 1) {
                fadeFlashNow[i]++;
            } else {
                fadeFlashNow[i] = 0;
            }
            $('.fadeFlash').eq(i).find('.btnDiv').find('span').eq(fadeFlashNow[i]).addClass('spanNow');
            $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).fadeIn(500);
            fadeFlashTime[i] = setInterval("fadeFlashFun(" + i + ")", 5000);
        })
        $(this).find('.leftBtn').click(function () {
            clearInterval(fadeFlashTime[i]);
            $('.fadeFlash').eq(i).find('.btnDiv').find('span').removeClass('spanNow');
            $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).hide();
            if (fadeFlashNow[i] > 0) {
                fadeFlashNow[i]--;
            } else {
                fadeFlashNow[i] = $('.fadeFlash').eq(i).find('li').length - 1;
            }
            $('.fadeFlash').eq(i).find('.btnDiv').find('span').eq(fadeFlashNow[i]).addClass('spanNow');
            $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).fadeIn(500);
            fadeFlashTime[i] = setInterval("fadeFlashFun(" + i + ")", 5000);
        })
    })
    //--
    $('.pageDiv').each(function () {
        $('.pageBar').find('ul').html($('.pageBar').find('ul').html() + "<li></li>");
    })
    $(window).scroll(function () {
        $('.pageDiv').each(function (i) {
            if ($(window).scrollTop() >= $(this).offset().top - 330) {
                $('.pageBar').find('li').removeClass('liNow');
                $('.pageBar').find('li').eq(i).addClass('liNow');
            }
        })
    })
    $('.pageBar').find('li:first').addClass('liNow');
    $('.pageBar').find('li').each(function (i) {
        $(this).click(function () {
            $('.pageBar').find('li').removeClass('liNow');
            $(this).addClass('liNow');
            $('body,html').animate({ scrollTop: $('.pageDiv').eq(i).offset().top - 80 }, 200);
        })
    })
    //--
    $('.Experience_03').find('img').fadeTo(10, 0.9);
    $('.Experience_03').find('.aNow').find('img').fadeTo(10, 1);
    $('.Experience_03').find('a').hover(
	   function () {
	       $(this).find('img').fadeTo(10, 1);
	   },
	   function () {
	       if ($(this).attr('class') != "aNow") {
	           $(this).find('img').fadeTo(10,0.9);
	       }
	   }
	)
    //--
    //    $('.productList').find('li').hover(
    //	   function () {
    //	       $(this).addClass('liNow');
    //	   },
    //	   function () {
    //	       $(this).removeClass('liNow');
    //	   }
    //	)
    //产品列表页
    //    $(".productList li").live("mouseover", function () {
    //        $(this).addClass('liNow');
    //    });
    //    $(".productList li").live("mouseout", function () {
    //        $(this).removeClass('liNow');
    //    });



    //--
    $('.myAddress').find('li').hover(
	   function () {
	       $(this).find(".da_model").addClass('liNow');
	   },
	   function () {
	       $(this).find(".da_model").removeClass('liNow');
	   }
	)
    //--
    $('.life').find('li').hover(
	   function () {
	       $(this).find('.content').show();
	   },
	   function () {
	       $(this).find('.content').hide();
	   }
	)
    //--
    $('.tabContentDiv').find('.tabContent:first').show();
    $('.tab').each(function (i) {
        $(this).find('li').each(function (ii) {
            $(this).hover(
			function () {
			    $('.tab').eq(i).find('li').removeClass('liNow');
			    $(this).addClass('liNow');
			    $('.tabContentDiv').eq(i).find('.tabContent').hide();
			    $('.tabContentDiv').eq(i).find('.tabContent').eq(ii).show();
			},
			function () { }
				)
        })
    })
    //--
    $('.indexBar').find('li').hover(
	   function () {
	       $(this).find(".xsjDiv").show();
	       $('.indexBar').stop().animate({ height: 130 }, 200);
	   },
	   function () {
	       $(this).find(".xsjDiv").hide();
	       $('.indexBar').stop().animate({ height: 93 }, 200);
	   }
	)
    var indexBarTop = $('.indexBar').offset().top;
    $(window).scroll(function () {
        if ($(window).scrollTop() > indexBarTop) {
            $('.indexBar').addClass('indexBarNow');
        } else {
            $('.indexBar').removeClass('indexBarNow');
        }
    })
    //--




    //产品列表页
    //    $(".product li").live("mouseover", function () {
    //        $(this).addClass('liNow');
    //    });
    //    $(".product li").live("mouseout", function () {
    //        $(this).removeClass('liNow');
    //    });


    //3D体验馆
//    $(".threeDModelDiv li,.productList li").live("mouseover", function (e) {
//        $(this).find(".ico,.name,.btn,.info").fadeIn("slow");
//        stopBubble(e);
//    });
//    $(".threeDModelDiv li,.productList li").live("mouseout", function (e) {
//        $(this).find(".ico,.name,.btn,.info").fadeOut("slow");
//        stopBubble(e);

//    });


//    $(".productList li").live("mouseover", function (e) {
//        //$(this).find(".info").slideDown("slow");
//        $(this).find(".info").show();
//        stopBubble(e);
//    });
//    $(".productList li").live("mouseout", function (e) {
//        //$(this).find(".info").slideUp("slow");
//        $(this).find(".info").hide();
//        stopBubble(e);

//    });








    $(".prev,.next").hover(function () {
        $(this).css("opacity", "1");
    }, function () {
        $(this).css("opacity", "0.7");
    });

    function stopBubble(e) {
        //一般用在鼠标或键盘事件上
        if (e && e.stopPropagation) {
            //W3C取消冒泡事件
            e.stopPropagation();
        } else {
            //IE取消冒泡事件
            window.event.cancelBubble = true;
        }
    };


})