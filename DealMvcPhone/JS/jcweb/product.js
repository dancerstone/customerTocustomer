


$(function () {
    //产品列表 加载更多
    $(".ProductMore").live("click", function () {
        var pid = parseInt($(this).attr("pid")); //当前页
        var pdd = parseInt($(this).attr("pdd")); //总页数
        var cateid = parseInt($(this).attr("cateid")); //分类ID
        // var keyw = $(".webkeyword").val(); //关键字
        if (pid >= pdd) {
            $(this).hide();
            return;
        }
        $(this).remove();
        $.get("/Main/LoadingProduct", { Page: parseInt(pid + 1), cateid: cateid }, function (msg) {
            $(".p_list ul li").last().after(msg);
        });
        $.get("/Main/LoadingProductPage", { Page: parseInt(pid + 1), cateid: cateid }, function (msg) {
            $(".MoreDivModel").html(msg);
        });
    });

    //3D体验馆列表 加载更多
    $(".ThreeDMore").live("click", function () {
        var pid = parseInt($(this).attr("pid")); //当前页
        var pdd = parseInt($(this).attr("pdd")); //总页数
        var cateid = parseInt($(this).attr("cateid")); //分类ID
        // var keyw = $(".webkeyword").val(); //关键字
        if (pid >= pdd) {
            $(this).hide();
            return;
        }
        $(this).remove();
        $.get("/Main/LoadingThreeD", { Page: parseInt(pid + 1), cateid: cateid }, function (msg) {
            $(".T_II ul li").last().after(msg);
        });
        $.get("/Main/LoadingThreeDPage", { Page: parseInt(pid + 1), cateid: cateid }, function (msg) {
            $(".MoreDivModel").html(msg);
        });
    });



    //只能输入数字
    $(".canint").live("keyup", function () {
        $(this).val($(this).val().replace(/\D/g, ''));
    });

    //加
    $(".add_b").click(function () {
        var val = parseInt($(this).parent().find(".buynumber").val());
        $(this).parent().find(".buynumber").val(parseInt(val + 1));
    });
    //减
    $(".cut_b").click(function () {
        var val = parseInt($(this).parent().find(".buynumber").val());
        if (val > 1) {
            $(this).parent().find(".buynumber").val(parseInt(val - 1));
        }
        else {
            $(this).parent().find(".buynumber").val("1");
        }
    });
    //输入
    $(".buynumber").blur(function () {
        var val = parseInt($(this).val());
        if (val <= 0) {
            $(this).val("1");
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
    $(".addCarBtn").click(function () {
        var this_c = $(this);
        var login = $(this).attr("login");
        if (login == "true") {
            var G_UID = $("input[name=Goods_UID_By]").val();
            var Count = 1;
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
                        $("body").showMessage("商品已加入购物车");
                    }
                }
            });
        }
        else {
            window.location = "/User/Login";
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

    //确认更改数量
    $(".btn_car_sure").live("click", function () {
        var num = parseInt($(this).parent().parent().find(".buynumber").val());
        if (num <= 0 || num == undefined || num == null || isNaN(num)) {
            $(this).parent().parent().find(".buynumber").val("1");
            $("body").showMessage("购买产品数量不能少于1");
        }
        else {
            UpdateNumber($(this).parent().parent().find(".buynumber"), num);
            var n_p = parseFloat($(this).parent().parent().find(".buynumber").attr("d_p"));
            $(this).parent().parent().parent().parent().find("input[name=goods_id_btn]").attr("p_p", parseFloat(n_p * num))
            $(this).parent().parent().parent().parent().find(".now_num").text(num);
            AllCheckF();
        }
        close_prompt_fun($(this).parent().parent().parent());
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


    //商品列表收藏
    $(".btn_collectGoods").live("click", function () {
        var this_c = $(this);
        var login = $(this).attr("login");
        if (login == "true") {
            var guid = $(this).attr("gid");
            $.get("/Main/GoodsCollectModel", { guid: guid, cate: 1 }, function (msg) {
                if (msg == "yes") {
                    this_c.parent().html('<img src="../../App_Themes/images/p_29.png" />');
                }
                else {
                    $("body").showMessage(msg);
                }
            });
        }
        else {
            window.location = "/User/Login";
        }
    });



    //  $("#qx_btn,.qx_btn_text").toggle();
    //全选
    $(".CarResult .img").toggle(function () {
        $(":checkbox[name='all_qx_btn']").attr("checked", false);
        $(this).removeClass("allin_check");
        if ($(".all_qx_btn").is(':checked') == true) {
            $(":checkbox[name='goods_id_btn']").attr("checked", true);
            $(".li_left").addClass("checkin");
        }
        else {
            $(":checkbox[name='goods_id_btn']").attr("checked", false);
            $(".li_left").removeClass("checkin");
        }
        AllCheckF();
    }, function () {
        $(":checkbox[name='all_qx_btn']").attr("checked", true);
        $(this).addClass("allin_check");
        if ($(".all_qx_btn").is(':checked') == true) {
            $(":checkbox[name='goods_id_btn']").attr("checked", true);
            $(".li_left").addClass("checkin");
        }
        else {
            $(":checkbox[name='goods_id_btn']").attr("checked", false);
            $(".li_left").removeClass("checkin");
        }
        AllCheckF();
    });



    $(".li_left").click(function () {
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
        var pp = 0;
        var tt = 0;
        $("input[name=goods_id_btn]").each(function (i, v) {
            if ($(v).is(':checked') == false) {
                cc_q = '2';
            }
            if ($(v).is(':checked') == true) {
                pp += parseFloat($(v).attr("p_p"));
                tt++;
            }
        });
        $(".w_all_price").text(pp.toFixed(2));
        $(".h_aa_int").text("(" + parseInt(tt) + ")");
        if (cc_q == "") {
            $(":checkbox[name='all_qx_btn']").attr("checked", true);
            $(".CarResult .img").addClass("allin_check");
        }
        else {
            $(":checkbox[name='all_qx_btn']").attr("checked", false);
            $(".CarResult .img").removeClass("allin_check");
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
    $(".btn_submit_car").click(function () {

        if ($(".CarList li").length <= 0) {
            $("body").showMessage("购物车没有任何商品");
        }
        else {
            if (AllCheckSubmit() != "") {
                $(".order_submit_form").submit();
            }
            else { $("body").showMessage("请选择需要提交的产品"); }
        }
    });




    //单品列表收藏
    $(".btn_collectSingleGoods").live("click", function () {
        var this_c = $(this);
        var guid = $(this).next().attr("sid");
        var cc = $(this).next().attr("cc");
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



    /* 订单页 */
    $(".address_list .check_m").click(function () {
        $(".address_list .check_m").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=Finally_Address_ID]").val($(this).attr("aid"));
    });
    //保存地址
    $(".btn_order_a_sure").click(function () {
        var n_v = $("input[name=Finally_Address_ID]").val();
        if (n_v == "" || n_v == undefined || n_v == null) {
            alert("请选择收货地址");
        }
        else {
            $.get("/Member/ChangeAddressM", { log_id: parseInt(n_v) }, function (msg) {
                $(".m_address_div").html(msg.AllMsg);
                close_prompt_fun(".update_m_address");
            });
        }
    });

    //选择支付方式
    $(".pay_click_model").click(function () {
        $(".pay_click_model").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=checkpaystylevalue]").val($(this).attr("psid"));
    });

    //选择配送方式
    $(".send_click_model .check_m").click(function () {
        $(".send_click_model .check_m").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=checksendstylevalue]").val($(this).attr("psid"));
    });

    //选择定金支付方式
    $(".dj_list_m .check_m").click(function () {
        $(".dj_list_m .check_m").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=checkdjpaystylevalue]").val($(this).attr("djid"));
    });



    //确认修改支付方式和配送方式
    $(".btn_order_pay_send_sure").click(function () {
        var pay_v = $("input[name=checkpaystylevalue]").val();
        var send_v = $("input[name=checksendstylevalue]").val();
        var dj_pay_v = $("input[name=checkdjpaystylevalue]").val();
        var count = $("input[name=Count]").val();
        var goods_uid = $("input[name=goods_uid]").val();
        var goods_id_btn = $("input[name=goods_id_btn]").val();
        if (pay_v == "" || pay_v == undefined) {
            alert("请选择正确的支付方式");
            return;
        }
        if (pay_v == "3" && (dj_pay_v == "" || dj_pay_v == undefined)) {
            alert("请选择正确的定金支付方式");
            return;
        }
        if (send_v == "" || send_v == undefined) {
            alert("请选择正确的配送方式");
            return;
        }
        $.get("/Member/ChangePaySendStyle", { pay_v: pay_v, send_v: send_v, goods_uid: goods_uid, count: count, goods_id_btn: goods_id_btn, dj_pay_v: dj_pay_v }, function (msg) {
            $(".m_pay_send_div").html(msg.AllMsg).show();
            close_prompt_fun(".update_pay_send_address");
        });


    });


    //确认修改发票信息
    $(".btn_order_invoices_sure").click(function () {
        var pay_v = $(".InvoliceCateM").val();
        var send_v = $(".InvoliceNameM").val();
        var xy_v = $("input[name=xy_bxy_name]").val();
        if (xy_v == "") {
            alert("请选择发票是否需要");
            return;
        }
        if (xy_v == "需要" && (send_v == "" || send_v == undefined)) {
            alert("请填写发票抬头");
            return;
        }
        $.get("/Member/ChangePayInvoices", { pay_v: pay_v, send_v: send_v, xy_v: xy_v }, function (msg) {
            $(".m_invocies_div").html(msg.AllMsg).show();
            close_prompt_fun(".update_m_invoies");
        });

    });

    //选择需要/不需要发票
    $(".p_m_tt_I .check_m").click(function () {
        $(".p_m_tt_I .check_m").removeClass("checkin");
        $(this).addClass("checkin");
        var now_v = $(this).attr("cc");
        $("input[name=xy_bxy_name]").val(now_v);
        if (now_v == "需要") {
            $(".II_I_m_p").show();
        }
        else {
            $(".II_I_m_p").hide();
        }
    });


    //提交订单
    $(".btn_submit_order").click(function () {
        if ($("input[name=Finally_Address_ID]").val() == "" || $("input[name=Finally_Address_ID]").val() == null) {
            alert("请先保存收货人地址信息");
            return;
        }
        if ($("input[name=Finally_PayStyle_ID]").val() == "" || $("input[name=Finally_PayStyle_ID]").val() == null) {
            alert("请先保存支付及配送方式信息");
            return;
        }
        if ($("input[name=Finally_Invoices_ID]").val() == "" || $("input[name=Finally_Invoices_ID]").val() == null) {
            alert("请先保存发票信息");
            return;
        }
        $(".OrderSubmitFrom").submit();
    });

    //选择网银支付银行
    $(".c_b_smll").click(function () {
        var now_v = $(this).attr("dm");
        $(".c_b_smll").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=ChangeBankCode]").val(now_v);
    });

    //支付订单
    $(".NowPayOrderModel").click(function () {
        var cate = $(this).attr("p_style");
        if (cate == "1") {
            var o_uid = $(this).attr("o_uid");
            var r_uid = $(this).attr("r_uid");
            window.location = "/Member/AliPayOrder?Order_UID=" + o_uid + "&Retaion_UID=" + r_uid;
        }
        else if (cate == "2") {
            var o_uid = $(this).attr("o_uid");
            var r_uid = $(this).attr("r_uid");
            var bank = $("input[name=ChangeBankCode]").val();
            window.location = "/Member/WYZXPayOrder?Order_UID=" + o_uid + "&Retaion_UID=" + r_uid + "&bank=" + bank;
        }
        else if (cate == "4") {
            var o_uid = $(this).attr("o_uid");
            var r_uid = $(this).attr("r_uid");
            var p_w_d = $("input[name=paypwdModelIN]").val();
            if (p_w_d == "" || p_w_d == undefined) {
                alert("请输入支付密码");
                return;
            }

            $.ajax({
                url: '/Member/CheckMemberPayPassWord',
                async: false,
                dataType: 'json',
                type: "POST",
                data: { n: p_w_d, r: new Date().getTime() },
                success: function (msg) {
                    json = [];
                    eval("json=" + msg);
                    if (json.msg == 'err') {
                        $(".PDMAYPAYBTN").addClass("NowPayOrderModel").css("cursor", "pointer");
                        $(".ccmm").text("");
                    } else if (json.msg == 'success') {
                        $(".PDMAYPAYBTN").removeClass("NowPayOrderModel").css("cursor", "no-drop");
                        $(".ccmm").text("支付密码输入错误");
                        return false;
                    }
                }
            });


            $.get("/Member/BalancePayOrder", { Order_UID: o_uid, Retaion_UID: r_uid, m_password: p_w_d }, function (msg) {
                if (msg.msg == "yes") {
                    window.location = "/Member/OrderPaySuccess";
                }
                else {
                    window.location = "/Member/OrderPayFailure?msg=" + msg.msg;
                }
            });
        }
        else {
            window.location = "/Member/OrderSubmitFailure";
        }
    });



});


