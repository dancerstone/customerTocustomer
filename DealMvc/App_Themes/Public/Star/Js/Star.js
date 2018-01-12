$(function () {
    window.onload = function () {
        $(".starpic_model").each(function (i, v) {
            var oStar = $(v);
            var aLi = oStar.find("li");
            var oUl = oStar.find("ul")[0];
            var oSpan = oStar.find("span")[0];
            var oInput = oStar.find("input")[0];
            var oP = oStar.find("p")[0];
            var i = iScore = iStar = 0;
            var aMsg = [
				"很不满意|差得太离谱，与卖家描述的严重不符，非常不满",
				"不满意|部分有破损，与卖家描述的不符，不满意",
				"一般|质量一般，没有卖家描述的那么好",
				"满意|质量不错，与卖家描述的基本一致，还是挺满意的",
				"非常满意|质量非常好，与卖家描述的完全一致，非常满意"
				]

            for (i = 1; i <= aLi.length; i++) {
                aLi[i - 1].index = i;

                //鼠标移过显示分数
                aLi[i - 1].onmouseover = function () {
                    fnPoint(this.index);
                    //浮动层显示
                    //oP.style.display = "block";
                    //计算浮动层位置
                    //oP.style.left = oUl.offsetLeft + this.index * this.offsetWidth - 104 + "px";
                    //匹配浮动层文字内容
                    //oP.innerHTML = "<em><b>" + this.index + "</b> 分 " + aMsg[this.index - 1].match(/(.+)\|/)[1] + "</em>" + aMsg[this.index - 1].match(/\|(.+)/)[1]
                };

                //鼠标离开后恢复上次评分
                aLi[i - 1].onmouseout = function () {
                    fnPoint();
                    //关闭浮动层
                    oP.style.display = "none"
                };

                //点击后进行评分处理
                aLi[i - 1].onclick = function () {
                    iStar = this.index;
                    oP.style.display = "none";
                    oSpan.innerHTML = "<strong>" + (this.index) + " 分</strong> ";
                    oInput.value = this.index;
                }
            }

            //评分处理
            function fnPoint(iArg) {
                //分数赋值
                iScore = iArg || iStar;
                for (i = 0; i < aLi.length; i++) aLi[i].className = i < iScore ? "on" : "";
            }
        });
    };

    //提交评价信息
    $(".review_submit_model_btn").click(function () {
        var g_score = $(this).parent().parent().find("input[name=g_score_star]");
        var g_experience = $(this).parent().parent().find(".Goods_Experience_model");
        var store_score = $(this).parent().parent().find("input[name=store_score_star]");
        var service_score = $(this).parent().parent().find("input[name=service_score_star]");
        var logistics_score = $(this).parent().parent().find("input[name=Logistics_score_star]");
        if (parseInt(g_score.val()) <= 0) {
            g_score.parent().parent().next().html("请为商品打分");
            return;
        } else {
            g_score.parent().parent().next().html("");
        }
        if (g_experience.val() == "" || g_experience.val() == null) {
            g_experience.parent().next().html("请填写商品心得");
            return;
        } else {
            g_experience.parent().next().html("");
        }
        if (parseInt(store_score.val()) <= 0) {
            store_score.parent().parent().next().html("请为商家打分");
            return;
        } else {
            store_score.parent().parent().next().html("");
        }
        if (parseInt(service_score.val()) <= 0) {
            service_score.parent().parent().next().html("请为商家服务打分");
            return;
        } else {
            service_score.parent().parent().next().html("");
        }
        if (parseInt(logistics_score.val()) <= 0) {
            logistics_score.parent().parent().next().html("请为商家物流打分");
            return;
        } else {
            logistics_score.parent().parent().next().html("");
        }

        $(this).parent().parent().submit();
    });

    //申请退款退货提交
    $(".Return_submit_model_btn").click(function () {
        var resons = $(this).parent().parent().find(".Goods_reson_model");
        var money_c = $(this).parent().parent().find("input[name=ClaimReturnMoney]");
        var goods_money = $(this).parent().parent().find("input[name=ClaimReturnMoney]").attr("goods_money");

        if (resons.val() == "" || resons.val() == null) {
            resons.parent().next().html("请填写退货原因");
            return;
        } else {
            resons.parent().next().html("");
        }
        if (parseFloat(money_c.val()) <= 0 || money_c.val() == "") {
            money_c.parent().next().html("请填写要求退款金额");
            return;
        } else {
            money_c.parent().next().html("");
        }
        if (parseFloat(money_c.val()) > parseFloat(goods_money)) {
            money_c.parent().next().html("请填写正确的退款金额，退款金额不得大于购买商品金额");
            return;
        } else {
            money_c.parent().next().html("");
        }
        $(this).parent().parent().submit();
    });
});
//只能输入数字
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