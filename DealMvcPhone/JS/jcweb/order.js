
$(function () {

    //选中默认地址
    $(".defaultaddressM,.defaultaddressMT").live("click", function () {
        var now_v = $("input[name=SetDefaultModel]").val();
        if (now_v == "") {
            $(".defaultaddressM").addClass("checkin");
            $("input[name=SetDefaultModel]").val("yes");
        } else {
            $(".defaultaddressM").removeClass("checkin");
            $("input[name=SetDefaultModel]").val("");
        }
    });


    //点击使用新地址
    $(".UseNewAddressModel").live("click", function () {
        $(this).parent().next().show();
    });


    //验证收货人名称
    $("input[name=D_Name]").blur(function () {
        if ($(this).val() == "" || $(this).val() == null) {
            $(".D_Name_show").show().css("color", "red").text("收货人姓名不能为空");
        }
        else {
            $(".D_Name_show").show().css("color", "red").text("");
        }
    });
    //验证手机
    $("input[name=D_Phone]").blur(function () {
        var reg = /^1[3|5|8][0-9]{9}$/;
        if ($(this).val() == "" || $(this).val() == null) {
            $(".D_Phone_show").show().css("color", "red").text("手机号码不能为空");
        }
        else if (!reg.test($(this).val())) {
            $(".D_Phone_show").show().css("color", "red").text("请输入以1开头的11位手机号码");
            return;
        }
        else {
            $(".D_Phone_show").show().css("color", "red").text("");
        }
    });
    //验证详细地址
    $("input[name=D_DetailAddress]").blur(function () {
        if ($(this).val() == "" || $(this).val() == null) {
            $(".D_DetailAddress_show").show().css("color", "red").text("详细地址不能为空");
        }
        else {
            $(".D_DetailAddress_show").show().css("color", "red").text("");
        }
    });

    //添加/编辑新地址
    $(".SurePsInformation").live("click", function () {
        var log_id = $(this).attr("log_id");
        var name = $("input[name=D_Name]").val(); //收货人
        var phone = $("input[name=D_Phone]").val(); //手机
        var tel = $("input[name=D_Tel]").val(); //座机
        var P_D = $(".MM_Province").val(); //省份
        var C_D = $(".MM_City").val(); //城市
        var A_D = $(".MM_Area").val(); //区县
        var D_D = $("input[name=D_DetailAddress]").val(); //详细地址
        var S_D = $("input[name=SetDefaultModel]").val();
        if (name == "" || name == null) {
            $(".D_Name_show").show().css("color", "red").text("收货人姓名不能为空");
            return;
        }
        if (phone == "" || phone == null) {
            $(".D_Phone_show").show().css("color", "red").text("手机号码不能为空");
            return;
        }
        var reg = /^1[3|5|8][0-9]{9}$/;
        if (!reg.test(phone)) {
            $(".D_Phone_show").show().css("color", "red").text("请输入以1开头的11位手机号码");
            return;
        }

        if (P_D == "" || C_D == "" || A_D == "") {
            $(".D_DetailAddress_show").show().css("color", "red").text("请选择正确的省市区信息");
            return;
        }
        if (D_D == "") {
            $(".D_DetailAddress_show").show().css("color", "red").text("详细地址不能为空");
            return;
        }

        $.get("/Order/AddUpdateAddress", { log_id: log_id, name: name, phone: phone, tel: tel, P_D: P_D, C_D: C_D, A_D: A_D, D_D: D_D, S_D: S_D }, function (msg) {
            $(".II_M_A").hide();
            $(".addresslist_M").html(msg.AllMsg).show();
        });

    });


    //点击确认修改地址
    $(".SureSubmitAddress").live("click", function () {
        var now_v = $("input[name=nowchangeaddressvalue]").val();
        if (now_v == undefined || now_v == "0") {
            alert("请新增或者选择正确的收货地址");
            return;
        }
        $.get("/Order/ChangeAddressM", { log_id: parseInt(now_v) }, function (msg) {
            $(".II_M_A").hide();
            $(".NowChangeAddressList").hide();
            $(".NowCheckAddress").html(msg.AllMsg).show();
            $(".UpdateNewAddress").show();
        });

    });

    //选择修改收货地址
    $(".UpdateNewAddress").live("click", function () {
        $(this).hide();
        $(".NowCheckAddress").hide();
        $(".NowChangeAddressList").show();
    });

    //选择收货地址
    $(".I_List_M").live("click", function () {
        var now_v = $("input[name=nowchangeaddressvalue]").val();
        $(".I_List_M").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=nowchangeaddressvalue]").val($(this).attr("aid"));
    });


    //选择支付方式
    $(".I_Pay_M").live("click", function () {
        var now_v = $("input[name=checkpaystylevalue]").val();
        $(".I_Pay_M").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=checkpaystylevalue]").val($(this).attr("psid"));
    });

    //选择配送方式
    $(".I_Send_M").live("click", function () {
        var now_v = $("input[name=checksendstylevalue]").val();
        $(".I_Send_M").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=checksendstylevalue]").val($(this).attr("psid"));
    });

    //选择定金支付方式
    $(".DJ_Pay_II_I").live("click", function () {
        var now_v = $(this).attr("djid");
        $(".DJ_Pay_II_I").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=checkdjpaystylevalue]").val(now_v);
    });

    //选择修改支付方式
    $(".UpdateNewPaySend").live("click", function () {
        $(this).hide();
        $(".NowCheckPaySend").hide();
        $(".NowChangePaySendList").show();
    });

    //确认修改支付方式和配送方式
    $(".SureSubmitPaySend").live("click", function () {
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
        $.get("/Order/ChangePaySendStyle", { pay_v: pay_v, send_v: send_v, goods_uid: goods_uid, count: count, goods_id_btn: goods_id_btn, dj_pay_v: dj_pay_v }, function (msg) {
            $(".NowChangePaySendList").hide();
            $(".NowCheckPaySend").html(msg.AllMsg).show();
            $(".UpdateNewPaySend").show();
        });


    });


    //确认修改发票信息
    $(".SureSubmitInvoices").live("click", function () {
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
        $.get("/Order/ChangePayInvoices", { pay_v: pay_v, send_v: send_v, xy_v: xy_v }, function (msg) {
            $(".NowChangeInvoicesList").hide();
            $(".NowCheckInvoices").html(msg.AllMsg).show();
            $(".UpdateNewInvoices").show();
        });

    });

    //选择修改发票抬头
    $(".UpdateNewInvoices").live("click", function () {
        $(this).hide();
        $(".NowCheckInvoices").hide();
        $(".NowChangeInvoicesList").show();
    });

    //提交订单
    $(".SubmitOrderModel").click(function () {
        if ($(".NowChangeAddressList").css("display") == "block") {
            alert("请先保存收货人地址信息");
            return;
        }
        if ($(".NowChangePaySendList").css("display") == "block") {
            alert("请先保存支付及配送方式信息");
            return;
        }
        if ($(".NowChangeInvoicesList").css("display") == "block") {
            alert("请先保存发票信息");
            return;
        }
        $(".OrderSubmitFrom").submit();
    });


    //支付订单
    $(".NowPayOrderModel").live("click", function () {
        var cate = $(this).attr("p_style");
        if (cate == "1") {
            var o_uid = $(this).attr("o_uid");
            var r_uid = $(this).attr("r_uid");
            window.location = "/Order/AliPayOrder?Order_UID=" + o_uid + "&Retaion_UID=" + r_uid;
        }
        else if (cate == "2") {
            var o_uid = $(this).attr("o_uid");
            var r_uid = $(this).attr("r_uid");
            var bank = $("input[name=ChangeBankCode]").val();
            window.location = "/Order/WYZXPayOrder?Order_UID=" + o_uid + "&Retaion_UID=" + r_uid + "&bank=" + bank;
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
                url: '/Order/CheckMemberPayPassWord',
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


            $.get("/Order/BalancePayOrder", { Order_UID: o_uid, Retaion_UID: r_uid, m_password: p_w_d }, function (msg) {
                if (msg.msg == "yes") {
                    window.location = "/Order/OrderPaySuccess";
                }
                else {
                    window.location = "/Order/OrderPayFailure?msg=" + msg.msg;
                }
            });
        }
        else {
            window.location = "/Order/OrderSubmitFailure";
        }
    });





    //选择需要/不需要发票
    $(".KJFP_Model_M").live("click", function () {
        $(".KJFP_Model_M").removeClass("checkin");
        $(this).addClass("checkin");
        var now_v = $(this).attr("cc");
        $("input[name=xy_bxy_name]").val(now_v);
        if (now_v == "需要") {
            $(".fplx_model").show();
        }
        else {
            $(".fplx_model").hide();
        }
    });



    //判断输入支付密码是否正确
    $(".paypwdModelIN").live("blur", function () {
        var val_v = $(this).val();
        $.ajax({
            url: '/Order/CheckMemberPayPassWord',
            async: false,
            dataType: 'json',
            type: "POST",
            data: { n: val_v, r: new Date().getTime() },
            success: function (msg) {
                json = [];
                eval("json=" + msg);
                if (json.msg == 'err') {
                    $(".PDMAYPAYBTN").addClass("NowPayOrderModel").css("cursor", "pointer");
                    $(".ccmm").text("");
                } else if (json.msg == 'success') {
                    $(".PDMAYPAYBTN").removeClass("NowPayOrderModel").css("cursor", "no-drop");
                    $(".ccmm").text("支付密码输入错误");
                }
            }
        });

    });


    //选择网银支付银行
    $(".c_b_smll").live("click", function () {
        var now_v = $(this).attr("dm");
        $(".c_b_smll").removeClass("checkin");
        $(this).addClass("checkin");
        $("input[name=ChangeBankCode]").val(now_v);
    });





});




