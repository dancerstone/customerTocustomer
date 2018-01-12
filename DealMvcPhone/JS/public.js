//加入收藏
function AddFavorite(sURL, sTitle) {
    try {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}
//设为首页
function SetHome(obj, vrl) {
    try {
        obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl);
    }
    catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }
            catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
        }
    }
}

function QueryString(url) {
    var Arr = new Array();
    var name, value, i;
    var str = url;
    var num = str.indexOf("?")
    str = str.substr(num + 1); //截取“?”后面的参数串
    var arrtmp = str.split("&"); //将各参数分离形成参数数组
    for (i = 0; i < arrtmp.length; i++) {
        num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num); //取得参数名称
            value = arrtmp[i].substr(num + 1); //取得参数值
            //this[name] = value; //定义对象属性并初始化
            Arr.push(new Array(name, value));
        }
    }
    return Arr;
}
function IsConfirm() {
    if (window.confirm("你确定要执行操作吗?"))
        return true;
    else
        return false;
}

//*******************************
function DeleteId(id) {//删除一条数据
    if (IsConfirm()) DeleteIdsForAjax(new Array(id));
}
function DeleteSelectAllIds(obj) {//删除选中的数据
    if (IsConfirm()) {
        DeleteIdsForAjax(GetCheckedIds());
    }
}
var DeleteUrl = ""; //删除数据地址默认为空
function DeleteIdsForAjax(ids) {//ajax执行删除数据功能
    var can = ids.join(",");
    if (can == "")
    { $("body").showMessage("未选择任何内容"); return; }
    if (DeleteUrl == "")
    { $("body").showMessage("删除失败, 路径为空"); return; }

    $.ajax({
        url: DeleteUrl,
        type: "POST",
        data: "ids=" + can,
        success: function (msg) {
            if (msg == "true") {
                for (var i = 0; i < ids.length; i++) {
                    $(":checkbox[name='ids'][value='" + ids[i] + "']").parent().parent().remove();
                }
                $("body").showMessage("删除成功");
            }
            else
                $("body").showMessage("删除失败");
        }
    });
}

function BeforDeleteId(id) {//删除一条数据
    if (IsConfirm()) BeforDeleteIdsForAjax(new Array(id));
}
var DeleteUrl = ""; //删除数据地址默认为空
function BeforDeleteIdsForAjax(ids) {//ajax执行删除数据功能
    var can = ids.join(",");
    if (can == "")
    { $("body").showMessage("未选择任何内容"); return; }
    if (DeleteUrl == "")
    { $("body").showMessage("删除失败, 路径为空"); return; }

    $.ajax({
        url: DeleteUrl,
        type: "POST",
        data: "ids=" + can,
        success: function (msg) {
            if (msg == "true") {
                for (var i = 0; i < ids.length; i++) {
                    $(":input[name='ids'][value='" + ids[i] + "']").parent().parent().remove();
                }
                $("body").showMessage("删除成功");
            }
            else
                $("body").showMessage("删除失败");
        }
    });
}

//*******************************

//*******************************
//获取选中的复选框的值,返回一个数组
function GetCheckedIds() {
    var ids = new Array();
    $(":checkbox[name='ids']:checked").each(function (i, v)
    { ids[i] = $(v).val(); });
    return ids;
}
//*******************************

//input='text'默认提示文字
function TextDefaultValue() {
    $("input[type='text']").each(function (i, v) {
        if ($(v).attr("Jtips") != undefined && $(v).attr("Jtips") != null) {
            var Jtips = $(v).attr("Jtips");
            $(v).focus(function () {
                var val = $.trim($(this).val());
                if (val == Jtips)
                    $(this).val("");
            });
            $(v).blur(function () {
                var val = $.trim($(this).val());
                if (val == "")
                    $(this).val(Jtips);
            });
            $(v).blur();
        }
    });
}

$(function () {
    //全部Checkbox功能
    $(".XshopCheckAll").toggle(function () {
        $(":checkbox[name='ids']").attr("checked", true);
    }, function () {
        $(":checkbox[name='ids']").attr("checked", false);
    });
    //反选Checkbox功能
    //    $(".XshopReverseCheck").click(function () {
    //        $(":checkbox[name='ids']").attr("checked", !$(":checkbox[name='ids']").attr("checked"));
    //    });


    //反向选择Checkbox功能
    $(".XshopReverseCheck").click(function () {
        $(":checkbox[name='ids']").each(function (i, v) {
            if ($(v).attr("checked"))
                $(v).attr("checked", false);
            else
                $(v).attr("checked", true);
        });
    });

    $(".table1").attr("border", "0");
    $(".table1").attr("cellpadding", "0");
    $(".table1").attr("cellspacing", "0");

    TextDefaultValue();
});

$(function () {
    $(".selectsub").live("mouseover", function () {
        $(".topLayerDIV").show();
    });
    $(".topLayerDIV").mouseover(function () {
        $(this).show();
    });
    $(".topLayerDIV").mouseout(function () {
        $(this).hide();
    });

    $('.login').find('.a1').toggle(
    function () {
        $('.loginLayer').show();
    },
   function () {
       $('.loginLayer').hide();
   }
);
});



