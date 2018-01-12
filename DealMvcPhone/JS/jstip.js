
//******js文字提示txb20100119********/
/*
<p>
<a href="../" class="txbtip" title="点击我就会进到列表页哦" target="_blank">这是一个超链接</a>
</p><p>
<input type="button" value="我是按钮" class="txbtip" title="别点我，点我我也不会理你的。<br/>不信的话你试试!"/>
</p>
<p><div class="txbtip" title="<img src='../opendiv/singup-succese.png'/>">其它标签也可以做到哦</div></p>
*/
if (window.addEventListener) {
    window.addEventListener("load", ready, false);
} else if (window.attachEvent) {
    window.attachEvent("onload", ready);
}

function ready() {
    var txbtip = getElementsByClassName('txbtip', '*');
    var tipdiv = document.createElement("div");
    tipdiv.id = "txbtip";
    tipdiv.style.position = "absolute";
    tipdiv.style.padding = "3px";
    tipdiv.style.color = "#000";
    tipdiv.style.background = "#fff";
    tipdiv.style.zIndex = "999";
    tipdiv.style.border = "2px solid #000";
    tipdiv.style.fontsize = "14px";
    tipdiv.style.display = "none";
    var rootEle = document.body || document.documentElement;
    rootEle.appendChild(tipdiv);
    for (var tip in txbtip) {
        //alert(txbtip[tip].id);
        var temp = "";
        txbtip[tip].onmouseover = function (e) {
            tipdiv.style.display = "block";
            var title = this.title;
            temp = this.title;
            this.title = "";
            tipdiv.innerHTML = title;
            setTipPosition(e);
            //alert(title);
        }
        txbtip[tip].onmousemove = function (e) {
            setTipPosition(e);
        }
        txbtip[tip].onmouseout = function (e) {
            //alert("out");
            this.title = temp;
            temp = "";
            tipdiv.style.display = "none";
        }
    }


    function getElementsByClassName(n, tag) {
        tag = tag || "*";
        var classElements = [], allElements = document.getElementsByTagName(tag);
        for (var i = 0; i < allElements.length; i++) {
            n = "" + n + "";
            var cn = " " + allElements[i].className + " ";
            if (cn.indexOf(n) != -1) {
                classElements[classElements.length] = allElements[i];
            }
        }
        return classElements;
    }
    function setTipPosition(e) {
        e = e || event;
        tipdiv.style.left = e.clientX + 10 + 'px';
        var top = document.body.scrollTop ? document.body.scrollTop : document.documentElement.scrollTop;
        tipdiv.style.top = e.clientY + 10 + top + 'px';
    }
}