


$(function () {
    //产品列表 加载更多
    $(".ProductMore").live("click", function () {
        var pid = parseInt($(this).attr("pid")); //当前页
        var pdd = parseInt($(this).attr("pdd")); //总页数
        var cateid = parseInt($(this).attr("cateid")); //分类ID
        var keyw = $(".webkeyword").val(); //关键字
        if (pid >= pdd) {
            $(this).hide();
            return;
        }
        $(this).remove();
        $.get("/Home/LoadingProduct", { Page: parseInt(pid + 1), cateid: cateid, keyword: keyw }, function (msg) {
            $(".productList ul li").last().after(msg);
        });
        $.get("/Home/LoadingProductPage", { Page: parseInt(pid + 1), cateid: cateid, keyword: keyw }, function (msg) {
            $(".MoreDivModel").html(msg);
        });
    });


    //产品列表 加载更多
    $(".ProductMoreThreeD").live("click", function () {
        var pid = parseInt($(this).attr("pid")); //当前页
        var pdd = parseInt($(this).attr("pdd")); //总页数
        var cateid = parseInt($(this).attr("cateid")); //分类ID
        if (pid >= pdd) {
            $(this).hide();
            return;
        }
        $(this).remove();
        $.get("/Home/LoadingThreeD", { Page: parseInt(pid + 1), cateid: cateid }, function (msg) {
            $(".threeDModelDiv ul li").last().after(msg);
        });
        $.get("/Home/LoadingThreeDPage", { Page: parseInt(pid + 1), cateid: cateid }, function (msg) {
            $(".MoreDivModel").html(msg);
        });
    });





    //只能输入数字
    $(".canint").live("keyup", function () {
        $(this).val($(this).val().replace(/\D/g, ''));
    });

    //加
    $(".numInput .jiaBtn").click(function () {
        var val = parseInt($(".buynumber").val());
        $(".buynumber").val(parseInt(val + 1));
    });
    //减
    $(".numInput .jianBtn").click(function () {
        var val = parseInt($(".buynumber").val());
        if (val > 1) {
            $(".buynumber").val(parseInt(val - 1));
        }
        else {
            $(".buynumber").val("1");
        }
    });
    //输入
    $(".numInput .buynumber").blur(function () {
        var val = parseInt($(this).val());
        if (val <= 0) {
            $(".buynumber").val("1");
        }
    });


    //提交订单
    $("#btn_submit_yuyue").click(function () {
        var _Count = parseInt($(".buynumber").val());
        var _P_UniqueID = $("input[name=Goods_UID_By]").val(); //商品唯一编号
        if (_Count <= 0) {
            $("body").showMessage("购买产品数量不能少于1");
            return;
        }
        var Param = new Array();
        Param.push("count=" + _Count); Param.push("goods_uid=" + _P_UniqueID);
        location.href = "/Order/Index" + "?" + Param.join("&");
    });

    //加入购物车
    $("#btn_addcar_yuyue").click(function () {

        var G_UID = $("input[name=Goods_UID_By]").val();
        var Count = $(".buynumber").val();
        if (Count <= 0) {
            $("body").showMessage("购买产品数量不能少于1");
            return;
        }
        $(".successcarmodel").show();
        var cart = $('.CarAllNumberModel');
        var imgtofly = $(".successcarmodel").find("img")
        if (imgtofly) {
            var imgclone = imgtofly.clone().offset({ top: imgtofly.offset().top, left: imgtofly.offset().left }).css({ 'opacity': '1', 'position': 'absolute', 'height': '35px', 'width': '35px', 'z-index': '1000' }).appendTo($('body')).animate({
                'top': cart.offset().top + 10,
                'left': cart.offset().left + 30,
                'width': 15,
                'height': 15
            }, 1000, '');
            imgclone.animate({ 'width': 0, 'height': 0 }, function () { $(this).detach() });
        }
        $(".successcarmodel").hide();

        $.ajax({
            url: '/ShoppingCart/AddShopCar',
            data: { G_UID: G_UID, Count: Count },
            type: 'POST',
            async: false,
            success: function (result) {
                if (result == "") {
                    $("body").showMessage("添加购物车操作失败，请稍候重试");
                }
                else {
                    $.get("/ShoppingCart/GetNowBuyNumber", { r: new Date().getTime() }, function (resutlt) {
                        $(".ShoppingCarModel").text(parseInt(resutlt));
                    });
                }
            }
        });
    });


    //购物车修改数量

    //加
    $(".numInput .jiaCar").click(function () {
        var obj = $(this).parent().find(".Carbuynumber");
        var val = parseInt(obj.val());
        UpdateNumber(obj, parseInt(val + 1));
    });
    //减
    $(".numInput .jianCar").click(function () {
        var obj = $(this).parent().find(".Carbuynumber");
        var val = parseInt(parseInt(obj.val()) - 1);

        if (val >= 1) {
            UpdateNumber(obj, val);
        }
        else {
            $("body").showMessage("购买产品数量不能少于1");
        }

    });
    //输入
    $(".numInput .Carbuynumber").blur(function () {
        var obj = $(this);
        var val = parseInt(obj.val());
        if (val >= 1) {
            UpdateNumber(obj, val);
        }
        else {
            $("body").showMessage("购买产品数量不能少于1");
        }
    });


    //更改
    function UpdateNumber(obj, val) {
        var SC_UniqueID = $(obj).attr("car_uid"); //标识
        $.ajax({
            url: "/ShoppingCart/AddShopCar2",
            data: { SC_UniqueID: SC_UniqueID, val: val },
            type: 'POST',
            success: function (result) {
                $(obj).val(val);
            }
        });
    }

    //删除购物车商品
    $(".DeleteModelBtn").click(function () {
        var SC_UniqueID = $(this).attr("car_uid");
        $(this).parent().parent().remove();
        $.ajax({
            url: "/ShoppingCart/Delete",
            data: { SC_UniqueID: SC_UniqueID },
            type: 'POST',
            success: function (result) {
                var c_i = parseInt($(".iii_model").length);
                if (c_i == 0) {
                    window.location.reload();
                }
            }
        });

    });


    //购物车收藏
    $(".AddSCModelBtn").live("click", function () {
        var this_c = $(this);
        var guid = $(this).attr("guid");
        $.get("/Home/GoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
            if (msg == "yes") {
                this_c.text("移出收藏");
                this_c.addClass("CutSCModelBtn");
                this_c.removeClass("AddSCModelBtn");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });
    //移除购物车收藏
    $(".CutSCModelBtn").live("click", function () {
        var this_c = $(this);
        var guid = $(this).attr("guid");
        $.get("/Home/CutGoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
            if (msg == "yes") {
                this_c.text("加入收藏");
                this_c.addClass("AddSCModelBtn");
                this_c.removeClass("CutSCModelBtn");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });


    //商品详情收藏
    $("#btn_addCollect_yuyue").live("click", function () {
        var this_c = $(this);
        var guid = $(this).next().attr("gid");
        var ccmm = $(this).next().attr("cc");
        if (ccmm == "0") {

            $(".successcollectmodel").show();
            var cart = $('.CollectGoodsModel');
            var imgtofly = $(".successcollectmodel").find("img")
            if (imgtofly) {
                var imgclone = imgtofly.clone().offset({ top: imgtofly.offset().top, left: imgtofly.offset().left }).css({ 'opacity': '1', 'position': 'absolute', 'height': '35px', 'width': '35px', 'z-index': '1000' }).appendTo($('body')).animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 30,
                    'width': 25,
                    'height': 25
                }, 1000, '');
                imgclone.animate({ 'width': 0, 'height': 0 }, function () { $(this).detach() });
            }
            $(".successcollectmodel").hide();
        }

        $.get("/Home/GoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
            if (msg == "yes") {
                this_c.next().text("已收藏");
                this_c.next().attr("cc", "1");
                this_c.next().addClass("CutGoodsDetailCollectM");
                this_c.next().removeClass("GoodsDetailCollectM");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });
    //商品移除购物车收藏
    //    $("#btn_cutCollect_yuyue").live("click", function () {
    //        var this_c = $(this);
    //        var guid = $(".CutGoodsDetailCollectM").attr("gid");
    //        $.get("/Home/CutGoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
    //            if (msg == "yes") {
    //                this_c.next().text("收藏");
    //                this_c.next().addClass("GoodsDetailCollectM");
    //                this_c.next().removeClass("CutGoodsDetailCollectM");
    //            }
    //            else {
    //                $("body").showMessage(msg);
    //            }
    //        });
    //    });

    //商品列表收藏
    $(".btn_collectGoods").live("click", function () {
        var this_c = $(this);
        var guid = $(this).next().attr("gid");
        var cc = $(this).next().attr("cc");
        if (cc == "0") {
            $(this).prev().show();
            var cart = $('.CollectGoodsModel');
            var imgtofly = $(this).prev().find("img")
            if (imgtofly) {
                var imgclone = imgtofly.clone().offset({ top: imgtofly.offset().top, left: imgtofly.offset().left }).css({ 'opacity': '1', 'position': 'absolute', 'height': '35px', 'width': '35px', 'z-index': '1000' }).appendTo($('body')).animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 30,
                    'width': 25,
                    'height': 25
                }, 1000, '');
                imgclone.animate({ 'width': 0, 'height': 0 }, function () { $(this).detach() });
            }
            $(this).prev().hide();
        }
        $.get("/Home/GoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
            if (msg == "yes") {
                this_c.next().html('<img src="/App_Themes/UI/image/okcollect.png" />');
                this_c.next().attr("cc", "1");
                this_c.next().removeClass("GoodsDetailCollectML");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });
    //商品列表移除购物车收藏
    $(".CutGoodsDetailCollectML").live("click", function () {
        var this_c = $(this);
        var guid = $(this).attr("gid");
        $.get("/Home/CutGoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
            if (msg == "yes") {
                this_c.html('<img src="/App_Themes/UI/image/btn3.png" />');
                this_c.addClass("GoodsDetailCollectML");
                // this_c.removeClass("CutGoodsDetailCollectML");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });


    //  $("#qx_btn,.qx_btn_text").toggle();
    //全选
    $("#qx_btn,.qx_btn_text").toggle(function () {
        $(":checkbox[name='all_qx_btn']").attr("checked", false);
        $("#qx_btn").removeClass("checkin");
        if ($(".all_qx_btn").is(':checked') == true) {
            $(":checkbox[name='goods_id_btn']").attr("checked", true);
            $(".goods_id_btn_M").addClass("checkin");
        }
        else {
            $(":checkbox[name='goods_id_btn']").attr("checked", false);
            $(".goods_id_btn_M").removeClass("checkin");
        }
    }, function () {
        $(":checkbox[name='all_qx_btn']").attr("checked", true);
        $("#qx_btn").addClass("checkin");
        if ($(".all_qx_btn").is(':checked') == true) {
            $(":checkbox[name='goods_id_btn']").attr("checked", true);
            $(".goods_id_btn_M").addClass("checkin");
        }
        else {
            $(":checkbox[name='goods_id_btn']").attr("checked", false);
            $(".goods_id_btn_M").removeClass("checkin");
        }
    });



    $(".goods_id_btn_M").click(function () {
        if ($(this).parent().find("input[name=goods_id_btn]").attr("checked")) {
            $(this).parent().find("input[name=goods_id_btn]").removeAttr("checked");
            $(this).removeClass("checkin");
            AllCheckF();
        }
        else {
            $(this).parent().find("input[name=goods_id_btn]").attr("checked", 'true');
            $(this).addClass("checkin");
            AllCheckF();
        }
    });

    function AllCheckF() {
        var cc_q = '';
        $("input[name=goods_id_btn]").each(function (i, v) {
            if ($(v).is(':checked') == false) {
                cc_q = '2';
            }
        });
        if (cc_q == "") {
            $(":checkbox[name='all_qx_btn']").attr("checked", true);
            $("#qx_btn").addClass("checkin");
        }
        else {
            $(":checkbox[name='all_qx_btn']").attr("checked", false);
            $("#qx_btn").removeClass("checkin");
        }
    }
    function AllCheckSubmit() {
        var cc_q = '';
        $("input[name=goods_id_btn]").each(function (i, v) {
            if ($(v).is(':checked') == true) {
                cc_q = '2';
            }
        });
        return cc_q;
    }


    //提交订单信息
    $(".SubmitOrderModel").click(function () {
        if ($(".iii_model").length <= 0) {
            $("body").showMessage("购物车没有任何商品");
        }
        else {
            if (AllCheckSubmit() != "") {
                $(".order_submit_form").submit();
            }
            else { alert("请选择需要提交的产品"); }
        }
    });




    //单品列表收藏
    $(".btn_collectSingleGoods").live("click", function () {
        var this_c = $(this);
        var guid = $(this).next().attr("sid");
        var cc = $(this).next().attr("cc");
        if (cc == "0") {
            $(this).prev().show();
            var cart = $('.CollectGoodsModel');
            var imgtofly = $(this).prev().find("img")
            if (imgtofly) {
                var imgclone = imgtofly.clone().offset({ top: imgtofly.offset().top, left: imgtofly.offset().left }).css({ 'opacity': '1', 'position': 'absolute', 'height': '35px', 'width': '35px', 'z-index': '1000' }).appendTo($('body')).animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 30,
                    'width': 25,
                    'height': 25
                }, 1000, '');
                imgclone.animate({ 'width': 0, 'height': 0 }, function () { $(this).detach() });
            }
            $(this).prev().hide();
        }

        $.get("/Home/GoodsCollectModel", { guid: guid, cate: 2 }, function (msg) {
            if (msg == "yes") {
                this_c.next().html('<img src="/App_Themes/UI/image/oksingle.png" />');
                this_c.next().attr("cc", "1");
                this_c.next().removeClass("SingleDetailCollectML");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });
    //单品列表移除购物车收藏
    $(".CutSingleDetailCollectML").live("click", function () {
        var this_c = $(this);
        var guid = $(this).attr("sid");
        $.get("/Home/CutGoodsCollectModel", { guid: guid, cate: 2 }, function (msg) {
            if (msg == "yes") {
                this_c.html('<img src="/App_Themes/UI/image/btn2.png" />');
                this_c.addClass("SingleDetailCollectML");
            }
            else {
                $("body").showMessage(msg);
            }
        });
    });



});


