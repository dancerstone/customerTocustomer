/*
//%%%%%%%%%%%%%%%%%%
//【商品图片】
//%%%%%%%%%%%%%%%%%%
*/

KindEditor.ready(function (K) {
    var GoodsPicEditor = K.editor({
        allowFileManager: true,
        uploadJson: '/Comm/UpGPic',
        imageSizeLimit: "50MB",
        imageUploadLimit: 20,
        imageFileTypes: "*.jpg;*.gif;*.png"
    });
    var GoodsPicTemp = "";
    $('#GoodsPicUp').click(function () {
        GoodsPicEditor.loadPlugin('multiimage', function () {
            GoodsPicEditor.plugin.multiImageDialog({
                clickFn: function (urlList) {
                    if (urlList.length == 0) {
                        if (!confirm("您还未上传图片，是否继续上传图片？")) {
                            GoodsPicEditor.hideDialog();
                        }
                    } else {
                        if (confirm("插入图片？会将以前图片清空哦！确认操作吗？")) {
                            $("#GoodsPicDiv").html("");

                            var Gids = "", GImageFile = "", J_imageView_Html = "", Ghover = "";
                            var Gfirst = true;
                            //each
                            $.each(urlList, function (i, gpicentity) {
                                //
                                if (!Gfirst) {
                                    Gids += ",";
                                    GImageFile += ",";
                                    Ghover = "";
                                } else {
                                    $("[name=ThumbnailPic]").val(gpicentity.tsrc);
                                    $("[name=SmallPic]").val(gpicentity.ssrc);
                                    $("[name=BigPic]").val(gpicentity.url);
                                    $("[name=GoodsDefaultPicID]").val(gpicentity.id);
                                    Ghover = " hover";
                                }
                                Gids += gpicentity.id;
                                GImageFile += gpicentity.url;

                                J_imageView_Html += "<li class=\"J_imageViewLi\" goodspicid=\"" + gpicentity.id + "\" tsrc=\"" + gpicentity.tsrc + "\" ssrc=\"" + gpicentity.ssrc + "\" bsrc=\"" + gpicentity.url + "\" tit=\"" + gpicentity.dtime + "\"><div class=\"asc\"> </div><div class=\"desc\"> </div>";
                                J_imageView_Html += "    <div class=\"imgDiv" + Ghover + "\"><img src=\"" + gpicentity.tsrc + "\" alt=\"" + gpicentity.dtime + "\"></div>"; //title=\"" + gpicentity.dtime + "\"
                                J_imageView_Html += "    <div class=\"gpicdelete\">删除</div>";
                                J_imageView_Html += "</li>";

                                Gfirst = false;

                            });
                            $("#GoodsPicDiv").html(J_imageView_Html);


                            $("[name=GoodsPicIDItem]").val(Gids);
                            $("[name=ImageFile]").val(GImageFile);
                            //$(".J_imageViewLi").preview();
                            GoodsPicEditor.hideDialog();
                        }

                    }

                }
            });
        });
    });

});
$(function () {
    //设置默认图片
    $(".J_imageViewLi .imgDiv").live("click", function () {

        var Gpicli = $(this).parent(); var gdefaultpicid = $("[name=GoodsDefaultPicID]");
        var vgoodspicid = Gpicli.attr("goodspicid");
        if (gdefaultpicid.val() != vgoodspicid) {
            gdefaultpicid.val(vgoodspicid);
            $("[name=ThumbnailPic]").val(Gpicli.attr("tsrc"));
            $("[name=SmallPic]").val(Gpicli.attr("ssrc"));
            $("[name=BigPic]").val(Gpicli.attr("bsrc"));
            $(".J_imageViewLi .imgDiv").removeClass("hover");
            $(this).addClass("hover");

        }

    });
    //删除图片操作
    $(".J_imageViewLi .gpicdelete").live("click", function () {
        var Gpicli = $(this).parent();
        var vgoodspicid = Gpicli.attr("goodspicid");

        Gpicli.remove();
        $(".J_imageViewLi[goodspicid=" + vgoodspicid + "]").remove();
        saveGoodsPicOrder();
    });
    //执行排序-按钮--隐藏、显示
    $(".J_imageViewLi").live({
        mouseenter: function () {
            $(this).find(".asc,.desc").show();
        },
        mouseleave: function () {
            $(this).find(".asc,.desc").hide();
        }
    })
    //向上
    $(".J_imageViewLi .asc").live("click", function () {
        var this_sv = $(this).parents(".J_imageViewLi");
        if (this_sv.parent().find(".J_imageViewLi").length <= 1)
            return false;
        //把他的上一个往下排把他排上去如果是第一个就不让他往上排
        //【insertAfter】
        var checkedLi = this_sv.prev();
        checkedLi.insertAfter(this_sv);

        //处理排序后的事件
        saveGoodsPicOrder();
    });
    //执行排序
    //向下
    $(".J_imageViewLi .desc").live("click", function () {
        var this_sv = $(this).parents(".J_imageViewLi");
        if (this_sv.parent().find(".J_imageViewLi").length <= 1)
            return false;
        //把他的下一个往上排把他排下去如果是最后一个就不让他往下排
        //【insertBefore】        
        var checkedLi = this_sv.next();
        checkedLi.insertBefore(this_sv);

        //处理排序后的事件
        saveGoodsPicOrder();
    });
});
//处理上传图片后的事务
function saveGoodsPicOrder() {
    var ulobj = $("#GoodsPicDiv");
    var idids = GpicIdListIds(ulobj);
    var imgids = GpicImgListIds(ulobj);
    // reset
    if ($("[name=GoodsPicIDItem]").val() != idids) {

    }
    if (idids.length == "") {
        $("[name=ThumbnailPic]").val("");
        $("[name=SmallPic]").val("");
        $("[name=BigPic]").val("");
        $("[name=ImageFile]").val("");
    }
    $("[name=GoodsPicIDItem]").val(idids)
    $("[name=ImageFile]").val(imgids)
};
function GpicIdListIds(obj) {
    var ids = "";
    var first = true;
    $(obj).children("li").each(function () {
        if (!first) {
            ids += ",";
        }
        first = false;
        ids += $(this).attr("goodspicid");

    });
    return ids;
}
function GpicImgListIds(obj) {
    var ids = "";
    var first = true; var isde = false;
    if ($(".J_imageViewLi").find(".hover").length == 0) {
        isde = true;
    }
    $(obj).children("li").each(function () {
        if (!first) {
            ids += ",";
        } else {
            if (isde) {
                $("[name=ThumbnailPic]").val($(this).attr("tsrc"));
                $("[name=SmallPic]").val($(this).attr("ssrc"));
                $("[name=BigPic]").val($(this).attr("bsrc"));
                $(this).find(".imgDiv").addClass("hover");
                //执行ajax

            }
        }

        first = false;
        ids += $(this).attr("bsrc");

    });
    return ids;
}
