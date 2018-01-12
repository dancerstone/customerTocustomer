var Sponsor = null;

//吴辉
//前台登录框
function Islogin(isRefresh, sponsor) {
    if (sponsor) {
        Sponsor = sponsor;
    }
    var Common = {
        getEvent: function () {//ie/ff
            if (document.all) {
                return window.event;
            }
            func = getEvent.caller;
            while (func != null) {
                var arg0 = func.arguments[0];
                if (arg0) {
                    if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                        return arg0;
                    }
                }
                func = func.caller;
            }
            return null;
        },
        getMousePos: function (ev) {
            if (!ev) {
                ev = this.getEvent();
            }
            if (ev.pageX || ev.pageY) {
                return {
                    x: ev.pageX,
                    y: ev.pageY
                };
            }

            if (document.documentElement && document.documentElement.scrollTop) {
                return {
                    x: ev.clientX + document.documentElement.scrollLeft - document.documentElement.clientLeft,
                    y: ev.clientY + document.documentElement.scrollTop - document.documentElement.clientTop
                };
            }
            else if (document.body) {
                return {
                    x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
                    y: ev.clientY + document.body.scrollTop - document.body.clientTop
                };
            }
        },
        getItself: function (id) {
            return "string" == typeof id ? document.getElementById(id) : id;
        },
        getViewportSize: { w: (window.innerWidth) ? window.innerWidth : (document.documentElement && document.documentElement.clientWidth) ? document.documentElement.clientWidth : document.body.offsetWidth, h: (window.innerHeight) ? window.innerHeight : (document.documentElement && document.documentElement.clientHeight) ? document.documentElement.clientHeight : document.body.offsetHeight },
        isIE: document.all ? true : false,
        setOuterHtml: function (obj, html) {
            var Objrange = document.createRange();
            obj.innerHTML = html;
            Objrange.selectNodeContents(obj);
            var frag = Objrange.extractContents();
            obj.parentNode.insertBefore(frag, obj);
            obj.parentNode.removeChild(obj);
        }
    }

    ///------------------------------------------------------------------------------------------------------
    var Class = {
        create: function () {
            return function () { this.init.apply(this, arguments); }
        }
    }
    var Drag = Class.create();
    Drag.prototype = {
        init: function (titleBar, dragDiv, Options) {
            //设置点击是否透明，默认不透明
            titleBar = Common.getItself(titleBar);
            dragDiv = Common.getItself(dragDiv);
            this.dragArea = { maxLeft: 0, maxRight: Common.getViewportSize.w - dragDiv.offsetWidth - 2, maxTop: 0, maxBottom: Common.getViewportSize.h - dragDiv.offsetHeight - 2 };
            if (Options) {
                this.opacity = Options.opacity ? (isNaN(parseInt(Options.opacity)) ? 100 : parseInt(Options.opacity)) : 100;
                this.keepOrigin = Options.keepOrigin ? ((Options.keepOrigin == true || Options.keepOrigin == false) ? Options.keepOrigin : false) : false;
                if (this.keepOrigin) { this.opacity = 50; }
                if (Options.area) {
                    if (Options.area.left && !isNaN(parseInt(Options.area.left))) { this.dragArea.maxLeft = Options.area.left };
                    if (Options.area.right && !isNaN(parseInt(Options.area.right))) { this.dragArea.maxRight = Options.area.right };
                    if (Options.area.top && !isNaN(parseInt(Options.area.top))) { this.dragArea.maxTop = Options.area.top };
                    if (Options.area.bottom && !isNaN(parseInt(Options.area.bottom))) { this.dragArea.maxBottom = Options.area.bottom };
                }
            }
            else {
                this.opacity = 100, this.keepOrigin = false;
            }
            this.originDragDiv = null;
            this.tmpX = 0;
            this.tmpY = 0;
            this.moveable = false;

            var dragObj = this;

            titleBar.onmousedown = function (e) {
                var ev = e || window.event || Common.getEvent();
                //只允许通过鼠标左键进行拖拽,IE鼠标左键为1 FireFox为0
                if (Common.isIE && ev.button == 1 || !Common.isIE && ev.button == 0) {
                }
                else {
                    return false;
                }

                if (dragObj.keepOrigin) {
                    dragObj.originDragDiv = document.createElement("div");
                    dragObj.originDragDiv.style.cssText = dragDiv.style.cssText;
                    dragObj.originDragDiv.style.display = "none";
                    dragObj.originDragDiv.style.width = dragDiv.offsetWidth;
                    dragObj.originDragDiv.style.height = dragDiv.offsetHeight;
                    dragObj.originDragDiv.innerHTML = dragDiv.innerHTML;
                    dragDiv.parentNode.appendChild(dragObj.originDragDiv);
                }

                dragObj.moveable = true;
                dragDiv.style.zIndex = dragObj.GetZindex() + 1111;
                var downPos = Common.getMousePos(ev);
                dragObj.tmpX = downPos.x - dragDiv.offsetLeft;
                dragObj.tmpY = downPos.y - dragDiv.offsetTop;

                titleBar.style.cursor = "move";
                if (Common.isIE) {
                    dragDiv.setCapture();
                } else {
                    window.captureEvents(Event.MOUSEMOVE);
                }

                dragObj.SetOpacity(dragDiv, dragObj.opacity);

                //FireFox 去除容器内拖拽图片问题
                if (ev.preventDefault) {
                    ev.preventDefault();
                    ev.stopPropagation();
                }

                document.onmousemove = function (e) {
                    if (dragObj.moveable) {
                        var ev = e || window.event || Common.getEvent();
                        //IE 去除容器内拖拽图片问题
                        if (document.all) //IE
                        {
                            ev.returnValue = false;
                        }

                        var movePos = Common.getMousePos(ev);
                        dragDiv.style.left = Math.max(Math.min(movePos.x - dragObj.tmpX, dragObj.dragArea.maxRight), dragObj.dragArea.maxLeft) + "px";
                        dragDiv.style.top = Math.max(Math.min(movePos.y - dragObj.tmpY, dragObj.dragArea.maxBottom), dragObj.dragArea.maxTop) + "px";
                    }
                };

                document.onmouseup = function () {
                    if (dragObj.keepOrigin) {
                        if (Common.isIE) {
                            dragObj.originDragDiv.outerHTML = "";
                        }
                        else {
                            Common.setOuterHtml(dragObj.originDragDiv, "");
                        }
                    }
                    if (dragObj.moveable) {
                        if (Common.isIE) {
                            dragDiv.releaseCapture();
                        }
                        else {
                            window.releaseEvents(Event.MOUSEMOVE);
                        }
                        dragObj.SetOpacity(dragDiv, 100);
                        titleBar.style.cursor = "move";
                        dragObj.moveable = false;
                        dragObj.tmpX = 0;
                        dragObj.tmpY = 0;
                    }
                };
            }
        },
        SetOpacity: function (dragDiv, n) {
            if (Common.isIE) {
                dragDiv.filters.alpha.opacity = n;
            }
            else {
                dragDiv.style.opacity = n / 100;
            }
        },
        GetZindex: function () {
            var maxZindex = 0;
            var divs = document.getElementsByTagName("div");
            for (z = 0; z < divs.length; z++) {
                maxZindex = Math.max(maxZindex, divs[z].style.zIndex);
            }
            return maxZindex;
        }
    }
    $.ajax({
        url: "/User/IsLogin",
        type: "POST",
        dataType: "html",
        data: { data: (new Date()).valueOf() },
        global: false,
        cache: false,
        async: false,
        success: function (data) {
            if (data == "1") {
                //                document.getElementById("btn_submit").click();
                if (Sponsor != null) {
                    $(Sponsor).click();
                }
                else {
                    document.getElementById("btn_submit").click();
                }
            } else {
                //btn_login_desk
                var loginHtml = '<div id=\"AllDesk\"></div><div id=\"dragDiv\"><div class=\"formDiv \">                    <div class=\"form\">                        <ul>                       <li class=\"li_01\" id=\"li_01\">    <strong>登录</strong><a id="CloseDesk" style=\"float: right; font-size: 22px; cursor: pointer;font-weight: bold;\">×</a></li>       <li>                                <span class=\"spanLogin\">登录帐号：</span><input name=\"M_UserName\" id=\"M_UserName\" type=\"text\" class=\"input1 input_hover req loginname\" maxlength=\"20\" autocomplete=\"off\" placeholder=\"请输入邮箱地址或手机号码\"/></li>                            <li>                                <span class=\"spanLogin\">登录密码：</span><input name=\"M_LoginPassword\"  id=\"M_LoginPassword\" type=\"password\" class=\"input1 input_hover req pwd\" maxlength=\"18\" placeholder=\"请输入密码\"/></li>                             <li class=\"li_02\">                                                                <div class=\"fl\" style=\" padding-left:62px;\">                                    <a href=\"/User/FindPassword\" class=\"blue2\">忘记密码？</a></div>                            </li>                            <li>                                 <div class=\"fl\" style=\" padding-left:58px;\"><input name=\"\" id=\"btn_login_desk\" type=\"button\" class=\"fl btn1\" value=\"登录\" /></div>                                <div  class=\"fl\" style=\" padding-left:28px;\"><a href=\"/User/Reg\"><input name=\"\" type=\"button\" class=\"fr btn1 btn2\" value=\"注册\"onclick=\"\" /></a></div>                            </li>                        </ul>                    </div>                </div></div> ';

                $("body").append(loginHtml);

                ReLogin();

                new Drag("li_01", "dragDiv", { opacity: 100, keepOrigin: false });

                window.onresize = function () {
                    try {
                        document.getElementById("AllDesk").width = document.body.clientWidth;
                        $("#AllDesk").height($("body").height());
                        $("#dragDiv").css({ top: ($(window).height() / 2 + $(document).scrollTop() - $("#dragDiv").height() / 2), left: ($(window).width() / 2 - $("#dragDiv").width() / 2) }).show();
                    }
                    catch (e) {
                    }
                }

                $("#CloseDesk").click(function () {
                    $("#AllDesk").remove();
                    $("#dragDiv").remove();
                });

                $("#btn_login_desk").click(function () {
                    var M_UserName = $.trim($("#M_UserName").val());
                    var M_LoginPassword = $("#M_LoginPassword").val();
                    var WebCode = $("#WebCode").val();
                    //var WebCode = "0000";
                    if (M_UserName == "") {
                        $("body").showMessage("登录帐号不能为空！");
                    } else if (M_LoginPassword == "") {
                        $("body").showMessage("登录密码不能为空！");
                    } else if (WebCode == "") {
                        $("body").showMessage("验证码不能为空！");
                    } else {
                        $.ajax({
                            url: "/User/LoginWindow",
                            type: "POST",
                            dataType: "html",
                            data: { lg_datetype: "loginwindow", M_UserName: M_UserName, M_LoginPassword: M_LoginPassword, WebCode: WebCode },
                            global: false,
                            cache: false,
                            success: function (data) {
                                if (data == "success") {
                                    if (isRefresh == "yes") {
                                        $("body").showMessage("登录成功！");
                                        window.location = location;
                                    } else {
                                        $("body").showMessage("登录成功！");
                                        //$("#MainTop").load("/User/Top", { r: new Date().getTime() });
                                        $("#AllDesk").remove();
                                        $("#dragDiv").remove();
                                    }
                                } else {
                                    $("body").showMessage(data);
                                }
                            }
                        });
                    }
                });
            }
        }
    });
}


function Islogin2(isRefresh, sponsor) {
    if (sponsor) {
        Sponsor = sponsor;
    }
    var Common = {
        getEvent: function () {//ie/ff
            if (document.all) {
                return window.event;
            }
            func = getEvent.caller;
            while (func != null) {
                var arg0 = func.arguments[0];
                if (arg0) {
                    if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                        return arg0;
                    }
                }
                func = func.caller;
            }
            return null;
        },
        getMousePos: function (ev) {
            if (!ev) {
                ev = this.getEvent();
            }
            if (ev.pageX || ev.pageY) {
                return {
                    x: ev.pageX,
                    y: ev.pageY
                };
            }

            if (document.documentElement && document.documentElement.scrollTop) {
                return {
                    x: ev.clientX + document.documentElement.scrollLeft - document.documentElement.clientLeft,
                    y: ev.clientY + document.documentElement.scrollTop - document.documentElement.clientTop
                };
            }
            else if (document.body) {
                return {
                    x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
                    y: ev.clientY + document.body.scrollTop - document.body.clientTop
                };
            }
        },
        getItself: function (id) {
            return "string" == typeof id ? document.getElementById(id) : id;
        },
        getViewportSize: { w: (window.innerWidth) ? window.innerWidth : (document.documentElement && document.documentElement.clientWidth) ? document.documentElement.clientWidth : document.body.offsetWidth, h: (window.innerHeight) ? window.innerHeight : (document.documentElement && document.documentElement.clientHeight) ? document.documentElement.clientHeight : document.body.offsetHeight },
        isIE: document.all ? true : false,
        setOuterHtml: function (obj, html) {
            var Objrange = document.createRange();
            obj.innerHTML = html;
            Objrange.selectNodeContents(obj);
            var frag = Objrange.extractContents();
            obj.parentNode.insertBefore(frag, obj);
            obj.parentNode.removeChild(obj);
        }
    }

    ///------------------------------------------------------------------------------------------------------
    var Class = {
        create: function () {
            return function () { this.init.apply(this, arguments); }
        }
    }
    var Drag = Class.create();
    Drag.prototype = {
        init: function (titleBar, dragDiv, Options) {
            //设置点击是否透明，默认不透明
            titleBar = Common.getItself(titleBar);
            dragDiv = Common.getItself(dragDiv);
            this.dragArea = { maxLeft: 0, maxRight: Common.getViewportSize.w - dragDiv.offsetWidth - 2, maxTop: 0, maxBottom: Common.getViewportSize.h - dragDiv.offsetHeight - 2 };
            if (Options) {
                this.opacity = Options.opacity ? (isNaN(parseInt(Options.opacity)) ? 100 : parseInt(Options.opacity)) : 100;
                this.keepOrigin = Options.keepOrigin ? ((Options.keepOrigin == true || Options.keepOrigin == false) ? Options.keepOrigin : false) : false;
                if (this.keepOrigin) { this.opacity = 50; }
                if (Options.area) {
                    if (Options.area.left && !isNaN(parseInt(Options.area.left))) { this.dragArea.maxLeft = Options.area.left };
                    if (Options.area.right && !isNaN(parseInt(Options.area.right))) { this.dragArea.maxRight = Options.area.right };
                    if (Options.area.top && !isNaN(parseInt(Options.area.top))) { this.dragArea.maxTop = Options.area.top };
                    if (Options.area.bottom && !isNaN(parseInt(Options.area.bottom))) { this.dragArea.maxBottom = Options.area.bottom };
                }
            }
            else {
                this.opacity = 100, this.keepOrigin = false;
            }
            this.originDragDiv = null;
            this.tmpX = 0;
            this.tmpY = 0;
            this.moveable = false;

            var dragObj = this;

            titleBar.onmousedown = function (e) {
                var ev = e || window.event || Common.getEvent();
                //只允许通过鼠标左键进行拖拽,IE鼠标左键为1 FireFox为0
                if (Common.isIE && ev.button == 1 || !Common.isIE && ev.button == 0) {
                }
                else {
                    return false;
                }

                if (dragObj.keepOrigin) {
                    dragObj.originDragDiv = document.createElement("div");
                    dragObj.originDragDiv.style.cssText = dragDiv.style.cssText;
                    dragObj.originDragDiv.style.display = "none";
                    dragObj.originDragDiv.style.width = dragDiv.offsetWidth;
                    dragObj.originDragDiv.style.height = dragDiv.offsetHeight;
                    dragObj.originDragDiv.innerHTML = dragDiv.innerHTML;
                    dragDiv.parentNode.appendChild(dragObj.originDragDiv);
                }

                dragObj.moveable = true;
                dragDiv.style.zIndex = dragObj.GetZindex() + 1111;
                var downPos = Common.getMousePos(ev);
                dragObj.tmpX = downPos.x - dragDiv.offsetLeft;
                dragObj.tmpY = downPos.y - dragDiv.offsetTop;

                titleBar.style.cursor = "move";
                if (Common.isIE) {
                    dragDiv.setCapture();
                } else {
                    window.captureEvents(Event.MOUSEMOVE);
                }

                dragObj.SetOpacity(dragDiv, dragObj.opacity);

                //FireFox 去除容器内拖拽图片问题
                if (ev.preventDefault) {
                    ev.preventDefault();
                    ev.stopPropagation();
                }

                document.onmousemove = function (e) {
                    if (dragObj.moveable) {
                        var ev = e || window.event || Common.getEvent();
                        //IE 去除容器内拖拽图片问题
                        if (document.all) //IE
                        {
                            ev.returnValue = false;
                        }

                        var movePos = Common.getMousePos(ev);
                        dragDiv.style.left = Math.max(Math.min(movePos.x - dragObj.tmpX, dragObj.dragArea.maxRight), dragObj.dragArea.maxLeft) + "px";
                        dragDiv.style.top = Math.max(Math.min(movePos.y - dragObj.tmpY, dragObj.dragArea.maxBottom), dragObj.dragArea.maxTop) + "px";
                    }
                };

                document.onmouseup = function () {
                    if (dragObj.keepOrigin) {
                        if (Common.isIE) {
                            dragObj.originDragDiv.outerHTML = "";
                        }
                        else {
                            Common.setOuterHtml(dragObj.originDragDiv, "");
                        }
                    }
                    if (dragObj.moveable) {
                        if (Common.isIE) {
                            dragDiv.releaseCapture();
                        }
                        else {
                            window.releaseEvents(Event.MOUSEMOVE);
                        }
                        dragObj.SetOpacity(dragDiv, 100);
                        titleBar.style.cursor = "move";
                        dragObj.moveable = false;
                        dragObj.tmpX = 0;
                        dragObj.tmpY = 0;
                    }
                };
            }
        },
        SetOpacity: function (dragDiv, n) {
            if (Common.isIE) {
                dragDiv.filters.alpha.opacity = n;
            }
            else {
                dragDiv.style.opacity = n / 100;
            }
        },
        GetZindex: function () {
            var maxZindex = 0;
            var divs = document.getElementsByTagName("div");
            for (z = 0; z < divs.length; z++) {
                maxZindex = Math.max(maxZindex, divs[z].style.zIndex);
            }
            return maxZindex;
        }
    }
    $.ajax({
        url: "/User/IsLogin",
        type: "POST",
        dataType: "html",
        data: { data: (new Date()).valueOf() },
        global: false,
        cache: false,
        async: false,
        success: function (data) {
            if (data == "1") {
                //                document.getElementById("btn_submit").click();
                if (Sponsor != null) {
                    Sponsor.prev().click();
                }
                else {
                    document.getElementById("btn_submit").click();
                }
            } else {
                //btn_login_desk
                var loginHtml = '<div id=\"AllDesk\"></div><div id=\"dragDiv\"><div class=\"formDiv \">                    <div class=\"form\">                        <ul>                       <li class=\"li_01\" id=\"li_01\">    <strong>登录</strong><a id="CloseDesk" style=\"float: right; font-size: 22px; cursor: pointer;font-weight: bold;\">×</a></li>       <li>                                <span class=\"spanLogin\">登录帐号：</span><input name=\"M_UserName\" id=\"M_UserName\" type=\"text\" class=\"input1 input_hover req loginname\" maxlength=\"20\" autocomplete=\"off\" placeholder=\"请输入邮箱地址或手机号码\"/></li>                            <li>                                <span class=\"spanLogin\">登录密码：</span><input name=\"M_LoginPassword\"  id=\"M_LoginPassword\" type=\"password\" class=\"input1 input_hover req pwd\" maxlength=\"18\" placeholder=\"请输入密码\"/></li>                             <li class=\"li_02\">                                                                <div class=\"fl\" style=\" padding-left:62px;\">                                    <a href=\"/User/FindPassword\" class=\"blue2\">忘记密码？</a></div>                            </li>                            <li>                                 <div class=\"fl\" style=\" padding-left:58px;\"><input name=\"\" id=\"btn_login_desk\" type=\"button\" class=\"fl btn1\" value=\"登录\" /></div>                                <div  class=\"fl\" style=\" padding-left:28px;\"><a href=\"/User/Reg\"><input name=\"\" type=\"button\" class=\"fr btn1 btn2\" value=\"注册\"onclick=\"\" /></a></div>                            </li>                        </ul>                    </div>                </div></div> ';

                $("body").append(loginHtml);

                ReLogin();

                new Drag("li_01", "dragDiv", { opacity: 100, keepOrigin: false });

                window.onresize = function () {
                    try {
                        document.getElementById("AllDesk").width = document.body.clientWidth;
                        $("#AllDesk").height($("body").height());
                        $("#dragDiv").css({ top: ($(window).height() / 2 + $(document).scrollTop() - $("#dragDiv").height() / 2), left: ($(window).width() / 2 - $("#dragDiv").width() / 2) }).show();
                    }
                    catch (e) {
                    }
                }

                $("#CloseDesk").click(function () {
                    $("#AllDesk").remove();
                    $("#dragDiv").remove();
                });

                $("#btn_login_desk").click(function () {
                    var M_UserName = $.trim($("#M_UserName").val());
                    var M_LoginPassword = $("#M_LoginPassword").val();
                    var WebCode = $("#WebCode").val();
                    //var WebCode = "0000";
                    if (M_UserName == "") {
                        $("body").showMessage("登录帐号不能为空！");
                    } else if (M_LoginPassword == "") {
                        $("body").showMessage("登录密码不能为空！");
                    } else if (WebCode == "") {
                        $("body").showMessage("验证码不能为空！");
                    } else {
                        $.ajax({
                            url: "/User/LoginWindow",
                            type: "POST",
                            dataType: "html",
                            data: { lg_datetype: "loginwindow", M_UserName: M_UserName, M_LoginPassword: M_LoginPassword, WebCode: WebCode },
                            global: false,
                            cache: false,
                            success: function (data) {
                                if (data == "success") {
                                    if (isRefresh == "yes") {
                                        $("body").showMessage("登录成功！");
                                        window.location = location;
                                    } else {
                                        $("body").showMessage("登录成功！");
                                        //$("#MainTop").load("/User/Top", { r: new Date().getTime() });
                                        $("#AllDesk").remove();
                                        $("#dragDiv").remove();
                                    }
                                } else {
                                    $("body").showMessage(data);
                                }
                            }
                        });
                    }
                });
            }
        }
    });
}

function ReLogin() {
    document.getElementById("AllDesk").width = document.body.clientWidth;
    $("#AllDesk").height($("body").height());
    $("#dragDiv").css({ top: ($(window).height() / 2 + $(document).scrollTop() - $("#dragDiv").height() / 2), left: ($(window).width() / 2 - $("#dragDiv").width() / 2) }).show();
    return false;
}