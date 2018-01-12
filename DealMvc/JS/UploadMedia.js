/*
//%%%%%%%%%%%%%%%%%%
//【商品图片】
//%%%%%%%%%%%%%%%%%%
*/

KindEditor.ready(function (K) {
    var self = this;
    var GoodsMediaEditor = K.editor({
        allowFileManager: true,
        uploadJson: '/Comm/UploadImage',
        imageSizeLimit: "200MB",
        imageUploadLimit: 200,
        imageFileTypes: "*.swf"
    });
    var GoodsPicTemp = "";
    $('#GoodsMediaUp').click(function () {
    $("#SSSWWWSSSWWW").attr("once", "1");
        GoodsMediaEditor.loadPlugin('media', function () {
            GoodsMediaEditor.plugin.media.edit();
        });});

});

