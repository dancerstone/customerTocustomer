var uppicobj = {};
uppicobj.temp = function (iptname) {
    return '<div class="ClaimUpDiv">' +
                                                '    <input type="button" class="ClaimPicUp" />' +
                                                '</div>' +
                                                '<div class="UpPicModel">' +
                                                '    <input type="hidden"  name="' + iptname + '" />' +
                                                '    <div class="UpPicDiv"></div>' +
                                                '</div>';
};
uppicobj.init = function () {
    var v_obj = $(".ClaimUpPicIpt");
    if (v_obj.length > 0) {
        v_obj.each(function (i, v) {
            $(v).html(uppicobj.temp($(v).attr("vnane")));
        });
    }
};

KindEditor.ready(function (K) {
    uppicobj.init();
    var GoodsPicEditor = K.editor({
        allowFileManager: true,
        uploadJson: '/Comm/UpGPic',
        imageSizeLimit: "2MB",
        imageUploadLimit: 1,
        imageFileTypes: "*.jpg;*.gif;*.png"
    });
    $('.ClaimPicUp').live("click", function () {
        var this_obj = $(this);
        GoodsPicEditor.loadPlugin('image', function () {
            GoodsPicEditor.plugin.imageDialog({
                showRemote: false,
                clickFn: function (url, title, width, height, border, align) {
                    this_obj.parent().parent().find(".UpPicModel input").val(url);
                    this_obj.parent().parent().find(".UpPicModel .UpPicDiv").show().html("<p><img style='width:300px;' src='" + url + "'  /></p>");
                    GoodsPicEditor.hideDialog();
                }
            });
        });
    });

});