$(function () {

    //选择返修类型
    $(".M_I").click(function () {
        $(".M_I").removeClass("checkin");
        $(this).addClass("checkin");
        var c_cate = $(this).attr("cate");
        if (c_cate == "退货") {
            $(".THMONEYMODEL").show();
        }
        else {
            $(".THMONEYMODEL").hide();
        }
        $("input[name=r_cate_m]").val(c_cate);
    });


    $(".G_M_M").toggle(function () {
        $(this).addClass("checkin");
        addgoods();
    }, function () {
        $(this).removeClass("checkin");
        addgoods();
    });


    function addgoods() {
        var goods_p = 0;
        var goods_u = "";
        $(".goodsTable .checkin").each(function (i, v) {
            goods_u += $(v).attr("guid") + ",";
            goods_p += parseFloat($(v).attr("gprice"));
        });
        $("input[name=r_goods_uid]").val(goods_u);
        $(".cc_mm").text("￥" + parseFloat(goods_p).toFixed(2));
        $(".cc_mm").attr("cc_m", parseFloat(goods_p));
    }



    //提交退货申请
    $(".btnmrodel").click(function () {
        var c_cate = $("input[name=r_cate_m]").val();
        var goods_u = $("input[name=r_goods_uid]").val();
        var v_reason = $(".r_reason_model").val();
        var v_money = $("input[name=r_r_money]").val();
        var cc_mm = $(".cc_mm").attr("cc_m");
        if (c_cate == "") {
            alert("请选择返修退换货类别");
            return;
        }
        if (goods_u == "") {
            alert("请选择返修退换货产品");
            return;
        }
        if (v_reason == "") {
            alert("请填写返修退换货原因");
            return;
        }
        if (v_money == "" && c_cate == "退货") {
            alert("请填写退货退款金额");
            return;
        }
        if (parseFloat(v_money) > parseFloat(cc_mm) && c_cate == "退货") {
            alert("退款金额不能大于购买产品金额");
            return;
        }
        $(".returnbyorder").submit();
    });



    //提交退货申请
    $(".btnmrodell").click(function () {
        var com = $("input[name=r_r_logiscompany]").val();
        var num = $("input[name=r_r_logisNumber]").val();
        var v_reason = $(".r_reason_model").val();
        var v_money = $("input[name=r_r_money]").val();
        var cc_mm = $(".cc_mm").attr("cc_m");
        var cid = $("input[name=OrderUIDModel]").val();
        if (com == "") {
            alert("请填写正确的物流公司名称");
            return;
        }
        if (num == "") {
            alert("请填写正确的物流编号");
            return;
        }

        $.get("/Member/UpdateApplyOrder", { com: com, num: num, cid: cid }, function (msg) {
            if (msg == "yes") {
                $("body").showMessage("操作成功，等待网站方确认");
                window.location.reload();
            }
            else {
                $("body").showMessage(msg);
            }
        });


    });





});

function clearNoNum(obj) {
    //先把非数字的都替换掉，除了数字和.
    obj.value = obj.value.replace(/[^\d.]/g, "");
    //必须保证第一个为数字而不是.
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个.
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
}