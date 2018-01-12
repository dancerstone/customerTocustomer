function pageFun() {
    if ($('.indexFlash').length > 0) {
        indexFlashFun();
    }
}
//--
function indexFlashFun() {
    $('.indexFlash').height($('.indexFlash').find('img').height());
}
//--
var fadeFlashNow = new Array();
for (i = 0; i < 50; i++) {
    fadeFlashNow[i] = 0;
}
function fadeFlashFun(i) {
//    $('.fadeFlash').eq(i).find('.btnDiv').find('span').removeClass('spanNow');
//    $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).hide();
//    if (fadeFlashNow[i] < $('.fadeFlash').eq(i).find('li').length - 1) {
//        fadeFlashNow[i]++;
//    } else {
//        fadeFlashNow[i] = 0;
//    }
//    $('.fadeFlash').eq(i).find('li').eq(fadeFlashNow[i]).fadeIn(500);
//    $('.fadeFlash').eq(i).find('.btnDiv').find('span').eq(fadeFlashNow[i]).addClass('spanNow');
}
//--	
function pblFun() {
    $('#masonry2').masonry({
        itemSelector: 'li',
        columnWidth: 1
    });
}		