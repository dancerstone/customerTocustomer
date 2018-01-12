$(function () {

    $(".useNew").click(function () {
        $(this).hide();
        $(this).parent().hide();
        $(".NewAddress").show();
    });

    $(".EditAddress").live("click", function () {
        var this_v = $(this);
        $(".useNew").hide();
        $(".Loading").show();
        setTimeout(function () {
            $.get("/Member/EditAddress?da_id=" + this_v.attr("da_id"), function (data, status) {
                $(".Loading").hide();
                $(".NewAddress").html(data).show();
            });
        }, 800);
    });
    $(".DeleteId").live("click", function () {
        var this_v = $(this);
        var v_id = this_v.attr("v_id");
        $.get("/Member/DeleteDeliveryAddress?id=" + v_id, function (data, status) {
            $("body").showMessage(data);
            if (this_v.parent().parent().parent().attr("isdefault").toUpperCase() == "TRUE") {
                this_v.parent().parent().parent().next().attr("isdefault", true);
                $(".now_count").text(parseInt($(".now_count").text()) - 1);
                if (parseInt($(".now_count").text()) > 0) {
                    this_v.parent().parent().parent().next().find("dd").eq(0).before("<dd><a>已设置为默认</a></dd>");
                } else {
                    $(".useNew").hide();
                    $(".useNew").parent().hide();
                    $(".NewAddress").show();
                }
            }
            this_v.parent().parent().parent().remove();
        });
    });


    $(".pageBtn1").live("click", function () {
        var DA_ConsigneeName = $("input[name='DA_ConsigneeName']").val();
        var DA_Phone = $("input[name='DA_Phone']").val();
        var DA_LandLine = $("input[name='DA_LandLine']").val();
        var DA_Province = $("select[name='DA_Province']").val();
        var DA_City = $("select[name='DA_City']").val();
        var DA_Area = $("select[name='DA_Area']").val();
        var DA_DetailAddress = $("input[name='DA_DetailAddress']").val();
        if (DA_ConsigneeName == "" || DA_ConsigneeName == null) {
            $("body").showMessage("请输入收货人姓名");
            return false;
        }
        if (DA_Phone == "" || DA_Phone == null) {
            $("body").showMessage("请输入手机号码");
            return false;
        } else {
            var reg = /^1[3|5|8][0-9]{9}$/;
            if (!reg.test(DA_Phone)) {
                $("body").showMessage("手机格式不正确");
                return false;
            }
        }
        if (DA_LandLine != "" && DA_LandLine != null) {
            var reg = /^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$/;

            if (!reg.test(DA_LandLine)) {
                $("body").showMessage("座机格式不正确");
                return false;
            }
        }
        if (DA_Province == "" || DA_Province == null) {
            $("body").showMessage("请选择省份");
            return false;
        }
        if (DA_City == "" || DA_City == null) {
            $("body").showMessage("请选择城市");
            return false;
        }
        if (DA_Area == "" || DA_Area == null) {
            $("body").showMessage("请选择区县");
            return false;
        }
        if (DA_DetailAddress == "" || DA_DetailAddress == null) {
            $("body").showMessage("请填写详细地址");
            return false;
        }

        $("#memberInfo_Form").submit();
    });
});