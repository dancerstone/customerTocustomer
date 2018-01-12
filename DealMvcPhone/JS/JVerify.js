////////////////////////////////////////////////
//////调用本页面前，请先引用jquery.js文件/////////
//////js表单验证集合/////////////////////////////
//////2010-4-12/////////////////////////////////
////////////////////////////////////////////////
//////////JVerify.js////////////////////////////

/*
Class 说明[可以任意组合] 
所有应用要生效,上级form表单必须Class需要应用:FormClassName[默认为var FormClassName = ".form"; //form表单样式]

.J -- 正常(什么都不验证), 目的让<input type="text" name="a1" class="J" min="2" max="8" />生效(不是必填,但又控制了min和max生效)
.req -- 必填  
.email -- 邮箱
.tel -- 座机
.ascii -- 字母 数字、“_”、“.”的字符串 字母开头
.cn -- 只能输入中文
.en -- 不能包含英文以外的字符
.trueacct -- 银行卡号格式
.phone -- 手机格式
.phoneortel -- 手机或座机
.phoneoremail -- 手机或邮箱
.idcardno -- 身份证
.postnum -- 邮编
.int -- 整数(不带小数点)
.float -- 数字

.Sreq -- 必选 <针对于select标签>
.Rreq -- 必选 <针对于Radio标签>
.Creq -- 必选 <针对于Checkbox标签>

.notips  -- 隐藏正常提示信息
.noBlur  -- 失去焦点时不验证
.noFocus -- 得到焦点时不验证

min 最小值  max 最大值
<input type="text" name="a1" class="req" min="2" max="8" />//必填长度在2-8
<input type="text" name="a1" class="req int" min="2" max="8" />//必填范围在2-8
<input type="text" name="a1" class="req int" min="2" max="8" msg="自定义错误提示信息" tips="自定义正常提示信息" />//必填范围在2-8

<input type="radio" name="rad" value="1" />1
<input type="radio" name="rad" value="2" />2
<input type="radio" name="rad" value="3" />3<span name="rad" class="Rreq"></span> //必选

<input type="checkbox" name="che" value="1" />1
<input type="checkbox" name="che" value="2" />2
<input type="checkbox" name="che" value="3" />3
<input type="checkbox" name="che" value="4" />4<span name="che" class="Creq" min="2" max="3"></span> //必选范围在2-3个

<select name="s1" class="Sreq"> //必选
<option value="">--</option>
<option value="1">1</option>
<option value="2">2</option>
<option value="3">3</option>
</select>

ajax验证
<input type="text" ajax_msg="此帐号已被注册" ajax="CheckUserID(this)" class=" req " name="UserName" />

function CheckUserID(obj)
{
var val = $(obj).val();
$.ajax({
url: '/R/CheckName.htm',
data: { name: val },
type: 'POST',
success: function (msg) { EndAjax(obj, msg == "1" ? true : false); }
});
}

再次输入密码验证
<input type="password" class="req" name="Pwd" id="mPwd1">
<input type="password" sameid="mPwd1" class="req same" msg="密码两次输入不一致" tips="请再次输入您设置的密码">

*/

var FormClassName = ".frmex"; //form表单样式

var NormalBorderColor = "#CCCCCC #999999 #999999 #CCCCC2"; //input边框
var NormalColor = "#666666"; //input字体颜色
var ErrorColor = "Red"; //错误提示颜色
var TipsColor = "#239BF7"; //正常提示颜色 
var RightColor = "Green"; //正常提示颜色

var reqTips = "";
var reqMsg = "*";

var form_VerifyList = new Array();
form_VerifyList.push(new Array("verify_J", "", ""));
form_VerifyList.push(new Array("verify_req", reqTips, reqMsg));
form_VerifyList.push(new Array("verify_email", "邮箱", "邮箱格式错误"));
form_VerifyList.push(new Array("verify_regemail", "", "邮箱格式错误"));
form_VerifyList.push(new Array("verify_loginname", "", "用户名格式错误"));
form_VerifyList.push(new Array("verify_ShopName", "", "店铺名称格式错误"));
form_VerifyList.push(new Array("verify_pwd", "", "密码格式错误"));
form_VerifyList.push(new Array("verify_tel", "座机", "座机格式错误"));
form_VerifyList.push(new Array("verify_ascii", "只能包含 字母 数字 _ . 字母开头", "只能包含 字母 数字 _ . 字母开头"));
form_VerifyList.push(new Array("verify_cn", "只能输入中文", "只能输入中文"));
form_VerifyList.push(new Array("verify_en", "不能包含英文以外的字符", "不能包含英文以外的字符"));
form_VerifyList.push(new Array("verify_trueacct", "", "银行卡号格式错误"));
form_VerifyList.push(new Array("verify_phone", "手机", "手机格式错误"));
form_VerifyList.push(new Array("verify_phoneortel", "手机或座机", "电话格式错误"));
form_VerifyList.push(new Array("verify_phoneoremail", "请输入手机或邮箱", "请输入手机或邮箱"));
form_VerifyList.push(new Array("verify_idcardno", "身份证", "身份证格式错误"));
form_VerifyList.push(new Array("verify_postnum", "邮编", "邮编格式错误"));
form_VerifyList.push(new Array("verify_int", "整数", "必须为整数"));
form_VerifyList.push(new Array("verify_float", "数字", "必须为数字"));

form_VerifyList.push(new Array("verify_same", "", "输入数据不一致")); //参数 sameid

form_VerifyList.push(new Array("verify_Sreq", "*", "*"));
form_VerifyList.push(new Array("verify_Rreq", "*", "*", false));
form_VerifyList.push(new Array("verify_Creq", "*", "*", false));


//校验注册邮箱
function verify_regemail(obj, tips, msg) {
    if ($(obj).val().replace(/(^\s*)|(\s*$)/g, "").length == 0) {
        set_flag_verify(obj, "邮箱必填！", tips, false);
        return false;
    }

    var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;

    if (!reg.test($(obj).val())) {
        set_flag_verify(obj, msg, "邮箱格式错误", false);return false;
     }

    var v_regEmail = $(obj).val();
    $.ajax({
        url: '/User/CheckEmail',
        async: false,
        dataType: 'json',
        type: "POST",
        data: { n: v_regEmail, r: new Date().getTime() },
        success: function (msg) {
            json = [];
            eval("json=" + msg);
            if (json.msg == 'err') {
                set_flag_verify(obj, "该邮箱已被注册", tips, false);
                return false;
            } else if (json.msg == 'success') {
                set_flag_verify(obj, "该邮箱可以注册", "该邮箱可以注册", true);
            }
        }
    });

    //else set_flag_verify(obj, msg, tips, true);
}

//校验登录账号
function verify_loginname(obj, tips, msg) {
    if ($(obj).val().replace(/(^\s*)|(\s*$)/g, "").length == 0) {
        set_flag_verify(obj, "用户名必填！", tips, false);
        return false;
    }
    var v_len = C_ASIIC($(obj).val().replace(/(^\s*)|(\s*$)/g, "")).length;
    var v_userName = $(obj).val();

    $.ajax({
        url: '/User/CheckUserName',
        async: false,
        dataType: 'json',
        type: "POST",
        data: { n: v_userName, r: new Date().getTime() },
        success: function (msg) {
            json = [];
            eval("json=" + msg);
            if (json.msg == 'err') {
                set_flag_verify(obj, "该用户名已被注册", tips, false);
                return false;
            } else if (json.msg == 'success') {
                set_flag_verify(obj, "该用户名可以注册", "该用户名可以注册", true);
            }
        }
    });
}

//校验登录账号
function verify_ShopName(obj, tips, msg) {
    if ($(obj).val().replace(/(^\s*)|(\s*$)/g, "").length == 0) {
        set_flag_verify(obj, "店铺名称必填！", tips, false);
        return false;
    }
    var v_len = C_ASIIC($(obj).val().replace(/(^\s*)|(\s*$)/g, "")).length;
    //2-20个字符，一个汉字为两个字符，推荐使用中文果粉。一旦注册成功果粉不能修改。

    if (v_len < 4 || v_len > 20) {
        set_flag_verify(obj, "店铺名称在4-20个字符内。", tips, false);
        return false
    }
    var v_userName = $(obj).val();
    if (/^\d*$/.test(v_userName)) {
        set_flag_verify(obj, "店铺名称不能全为数字。", tips, false);
        return false
    }
    var chines = /^[^!@#$%^&*()\-=+]+$/;
    if (!chines.test(v_userName)) {
        set_flag_verify(obj, "非法店铺名称。", tips, false);
        return false
    }
    $.ajax({
        url: '/User/CheckShopName',
        async: false,
        dataType: 'json',
        type: "POST",
        data: { n: v_userName, r: new Date().getTime() },
        success: function (msg) {
            json = [];
            eval("json=" + msg);
            if (json.msg == 'err') {
                set_flag_verify(obj, "该店铺名称已被注册", tips, false);
                return false;
            } else if (json.msg == 'success') {
                set_flag_verify(obj, "该店铺名称可以注册", "该店铺名称可以注册", true);
            }
        }
    });
}

function verify_pwd(obj, tips, msg) {
    var v_obj_val = $(obj).val();
    if (v_obj_val.length < 6 || v_obj_val.length > 18) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, "格式正确", true);
}

//正常,目的让<input type="text" name="a1" class="J" min="2" max="8" />生效(不是必填,但又控制了min和max生效)
function verify_J(obj, tips, msg) {
    set_flag_verify(obj, msg, tips, true);
}
//必填
function verify_req(obj, tips, msg) {
    if ($(obj).val().replace(/(^\s*)|(\s*$)/g, "").length == 0) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//邮件
function verify_email(obj, tips, msg) {
    var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//座机
function verify_tel(obj, tips, msg) {
    var reg = /^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//可带 字母 数字、“_”、“.”的字符串
function verify_ascii(obj, tips, msg) {
    var reg = /^[a-zA-Z]{1}([a-zA-Z0-9]|[._])+$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//只能为中文
function verify_cn(obj, tips, msg) {
    var reg = /^[\u4e00-\u9fa5]+$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//不能包含英文以外的字符
function verify_en(obj, tips, msg) {
    var reg = /^[a-zA-Z]+$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//验证银行卡：卡号必须为15位或16位或19位数字
function verify_trueacct(obj, tips, msg) {
    var reg = /^(\d{15}|\d{16}|\d{19})$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//验证手机号码：13、15、18
function verify_phone(obj, tips, msg) {
    var reg = /^1[3|5|8][0-9]{9}$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//验证手机或者座机
function verify_phoneortel(obj, tips, msg) {
    var reg = /^1[3|5|8][0-9]{9}$/; //手机
    var reg2 = /^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$/; //座机

    if (!reg.test($(obj).val()) && !reg2.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
function verify_phoneoremail(obj, tips, msg) {
    var reg = /^1[3|5|8][0-9]{9}$/; //手机
    var reg2 = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/; //邮箱

    if (!reg.test($(obj).val()) && !reg2.test($(obj).val()))
        set_flag_verify(obj, msg, tips, false);
    else
        set_flag_verify(obj, msg, tips, true);
}
//身份证验证
function verify_idcardno(obj, tips, msg) {
    var reg = /(^\d{15}$)|(^\d{17}([0-9]|X)$)/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//邮编
function verify_postnum(obj, tips, msg) {
    var reg = /^[1-9][0-9]{5}$/;

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//必须为整数
function verify_int(obj, tips, msg) {
    var reg = /^-?\d+$/; //^[0-9]+$

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//必须为数字
function verify_float(obj, tips, msg) {
    var reg = /^-?\d+(\.\d+)?$/; //^[0-9]+(\.)?([0-9])?([0-9])?$

    if (!reg.test($(obj).val())) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//验证两次输入密码是否相同
function verify_same(obj, tips, msg) {
    var value1 = $("#" + $(obj).attr("sameid")).val(); //原始值 

    if ($(obj).val() != value1) set_flag_verify(obj, msg, tips, false);
    else set_flag_verify(obj, msg, tips, true);
}
//radio必选
function verify_Rreq(obj, tips, msg) {
    try {
        var name = $(obj).attr("name");
        var P = $(obj).parents(FormClassName);
        //var AllCont = $(":radio[name='" + name + "']").length;
        if ($(P).find(":radio[name='" + name + "']:checked").length == 0) set_flag_verify(obj, msg, tips, false);
        else set_flag_verify(obj, msg, tips, true);
    } catch (e) { }
}
//checkbox必选
function verify_Creq(obj, tips, msg) {
    try {
        var name = $(obj).attr("name");
        var P = $(obj).parents(FormClassName);
        //var AllCont = $(":checkbox[name='" + name + "']").length;
        if ($(P).find(":checkbox[name='" + name + "']:checked").length == 0) set_flag_verify(obj, msg, tips, false);
        else set_flag_verify(obj, msg, tips, true);
    } catch (e) { }
}
//select必选
function verify_Sreq(obj, tips, msg) {
    try {
        if ($(obj).val() == null || $(obj).val().replace(/(^\s*)|(\s*$)/g, "") == "") set_flag_verify(obj, msg, tips, false);
        else set_flag_verify(obj, msg, tips, true);
    } catch (e) { }
}

//验证函数
function set_flag_verify(obj, msg, tips, value)
{ set_flag_verify(obj, msg, tips, value, false); }
//验证函数
function set_flag_verify(obj, msg, tips, value, first) {

    msg = getInputMsg(obj, msg);
    tips = getInputTips(obj, tips);

    var arr = checkMinMax(obj, value);
    if (!first) { value = arr[0]; }
    if (!HasAttr(obj, "msg"))
        msg = msg + arr[1];
    if (!HasAttr(obj, "tips"))
        tips = tips + arr[1];


    if ($(obj).val() != null && $(obj).val().replace(/(^\s*)|(\s*$)/g, "").length == 0 && !$(obj).hasClass("req") && !$(obj).hasClass("Sreq")) { value = true; }

    removeMsgTips(obj);

    if (value)
        AjaxCheck(obj, value);

    if (value == false) {
        addErrorSpan(obj, msg);
        $(obj).addClass("inputError");
    }
    else {
        addTipsSpan(obj, tips, first ? !first : true);
        $(obj).removeClass("inputError");
    }
}


//ajax验证
function AjaxCheck(obj) {
    try {
        if (HasAttr(obj, "ajax") && $.trim($(obj).val()) != "")
            eval($(obj).attr("ajax").replace("this", "$(obj)"));
    } catch (cse) { }

}
function EndAjax(obj, Result) {
    if (Result) {
        removeMsgTips(obj);
        addErrorSpan(obj, $(obj).attr("ajax_msg"));
        $(obj).addClass("inputError");
    }
}

//获取自定义错误提示信息
function getInputMsg(obj, msg) {
    if (HasAttr(obj, "msg"))
    //msg = msg + " " + $(obj).attr('msg');
        return $(obj).attr('msg');

    return msg;
}
//获取自定义正常提示信息
function getInputTips(obj, tips) {
    if (HasAttr(obj, "tips"))
    //tips = tips + " " + $(obj).attr('tips');
        return $(obj).attr('tips');

    return tips;
}
//获取自定义显示区
function getInputShowClass(obj) {
    if (HasAttr(obj, "showclass"))
    //tips = tips + " " + $(obj).attr('tips');
        return $(obj).attr('showclass');

    return "";
}

function GetArrValue(obj, ArrName) {
    if (HasAttr(obj, ArrName))
        return $(obj).attr(ArrName);
    return "";
}

//是否有此属性
function HasAttr(obj, attrName)
{ return $(obj).attr(attrName) != undefined && $(obj).attr(attrName) != null && $(obj).attr(attrName).length > 0; }

//获取焦点
function inputFocus(obj, tips) {
    tips = getInputTips(obj, tips);

    var arr = checkMinMax(obj, true);
    if (!HasAttr(obj, "tips"))
        tips = tips + arr[1];

    removeMsgTips(obj);
    $(obj).removeClass("inputError");
    addTipsSpan(obj, tips, false);

    //$(obj).css("border", "1px solid " + FocusColor);
    //$(obj).css("color", FocusColor);
}
//移除提示span
function removeMsgTips(obj) {
    var P = $(obj).parents(FormClassName);
    $(P).find("span[id='msg_" + $(obj).attr('name') + "']").remove();
    $(P).find("span[id='tips_" + $(obj).attr('name') + "']").remove();
}
//增加错误提示span
function addErrorSpan(obj, msg) {
    if ($(obj).hasClass("req")) {
        if (msg.indexOf(reqMsg) < 0)
        { msg = reqMsg + " " + msg; }
    }

    var sclass = getInputShowClass(obj);
    if (sclass.length <= 0)
        $(obj).after("<span class='spanError' style='color:" + ErrorColor + "' id='msg_" + $(obj).attr('name') + "'> " + msg + "</span>");
    else {
        $("." + sclass).html("<span class='spanError' style='color:" + ErrorColor + "' id='msg_" + $(obj).attr('name') + "'> " + msg + "</span>");
        $("." + sclass).css("color", ErrorColor);
    }

    //$(obj).css("border", "1px solid " + ErrorColor);
    $(obj).addClass("red");
}
//增加正常提示span
function addTipsSpan(obj, tips, S) {
    if ($(obj).hasClass("req")) {
        if (tips.indexOf(reqTips) < 0)
        { tips = reqTips + " " + tips; }
    }
    var hidden = $(obj).hasClass("notips") ? "display:none;" : "";

    var pic = "";
    var color = "";
    if (S) {
        pic = "spanRight";
        color = RightColor;
    }
    else {
        pic = "spanTips";
        color = TipsColor;
    }

    var sclass = getInputShowClass(obj);
    if (sclass.length <= 0)
        $(obj).after("<span  class='" + pic + "' style='" + hidden + "color:" + color + "' id='tips_" + $(obj).attr('name') + "'> " + tips + "</span>");
    else {
        $("." + sclass).html("<span  class='" + pic + "' style='" + hidden + "color:" + color + "' id='tips_" + $(obj).attr('name') + "'> " + tips + "</span>");
        $("." + sclass).css("color", color);
    }

    //$(obj).css("border-width", "1px");
    //$(obj).css("border-style", "solid");
    //$(obj).css("border-color", NormalBorderColor);
    $(obj).removeClass("red");
}
//验证字符串长度 或者 数值大小
function checkMinMax(obj, value) {
    var t = "";
    var s = value;

    var val = $(obj).val();
    if ($(obj).attr('min') != undefined && $(obj).attr('min') != null && $(obj).attr('min').length > 0) {
        var min = parseFloat($(obj).attr('min'));
        if ($(obj).hasClass("int") || $(obj).hasClass("float")) {
            val = parseFloat(val);
            if (min > val) { s = false; }
            t = t + " " + "值不小于" + $(obj).attr('min');
        }
        else if ($(obj).hasClass("Creq")) {
            var name = $(obj).attr("name");
            var P = $(obj).parents(FormClassName);
            var CheckCount = parseFloat($(P).find(":checkbox[name='" + name + "']:checked").length);
            if (min > CheckCount) { s = false; }
            t = t + " " + "个数不小于" + $(obj).attr('min');
        }
        else {
            if (min > C_ASIIC(val).length) { s = false; }
            t = t + " " + "长度不小于" + $(obj).attr('min');
        }
    }
    if ($(obj).attr('max') != undefined && $(obj).attr('max') != null && $(obj).attr('max').length > 0) {
        var max = parseFloat($(obj).attr('max'));
        if ($(obj).hasClass("int") || $(obj).hasClass("float")) {
            val = parseFloat(val);
            if (max < val) { s = false; }
            t = t + " " + "值不大于" + $(obj).attr('max');
        }
        else if ($(obj).hasClass("Creq")) {
            var name = $(obj).attr("name");
            var P = $(obj).parents(FormClassName);
            var CheckCount = parseFloat($(P).find(":checkbox[name='" + name + "']:checked").length);
            if (max < CheckCount) { s = false; }
            t = t + " " + "个数不大于" + $(obj).attr('max');
        }
        else {
            if (max < C_ASIIC(val).length) { s = false; }
            t = t + " " + "长度不大于" + $(obj).attr('max');
        }
    }
    return new Array(s, t);
}
//把一个双字节转换为ww
function C_ASIIC(val) {
    return val.replace(/[^\x00-\xff]/g, "ww");
}

//表单submit时验证
function verfy_submit(_obj) {

    //var obj = FormClassName;
    var obj = $(_obj);

    //if ($(obj).find(".inputError").length > 0) return false;
    if ($(obj).find(".inputError").not(".Rreq,.Creq").length > 0) return false;

    var output = "";
    for (var i = 0; i < form_VerifyList.length; i++) {
        var tips = form_VerifyList[i][1];
        var msg = form_VerifyList[i][2];
        var functionName = form_VerifyList[i][0];
        var className = "";
        try {
            className = functionName.split("_")[1];
        } catch (ce) { continue; }
        if (className == "") { continue; }

        output += "$(obj).find(\"." + className + "\").each(function(u, v){" + functionName + "($(v),'" + tips + "','" + msg + "');});";
    }
    eval(output);

    return $(obj).find(".inputError").length == 0 ? true : false;
}

//初始验证函数
$(function () {
    $(FormClassName).each(function (fi, fv) {
        for (var i = 0; i < form_VerifyList.length; i++) {
            var functionName = form_VerifyList[i][0];
            var tips = form_VerifyList[i][1]; //正常提示信息
            var msg = form_VerifyList[i][2];  //错误提示信息
            var className = "";
            try {
                className = functionName.split("_")[1];
            } catch (ce) { continue; }
            if (className == "") { continue; }

            $(fv).find("." + className).each(function (ui, vi) {
                $(vi).attr("tips", GetArrValue($(vi), "tips") + tips);
            });

            eval("$(fv).find(\"." + className + "\").blur(function(){if(!$(this).hasClass('noBlur')){" + functionName + "($(this),'" + tips + "','" + msg + "'); } });");
            eval("$(fv).find(\"." + className + "\").focus(function(){if(!$(this).hasClass('noFocus')){ inputFocus($(this),'" + tips + "'); } });");

            $(fv).find("." + className).each(function (ui, vi) {
                //初始显示提示
                eval("set_flag_verify($(this), '" + msg + "', '" + tips + "', true, true);");
            });

            var isRes = true;
            if (form_VerifyList[i][3] != undefined) { isRes = form_VerifyList[i][3]; }
            if (!isRes) { $(fv).find("." + className).hide(); $(fv).find("." + className).val("&nbsp;"); }

        }

        //增加submit方法
        $(fv).submit(function () { return verfy_submit($(this)); });
    });
});


