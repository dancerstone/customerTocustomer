/*
Dialog.js ### 2011-7-20 ###
*/

function Dialog(contentObj, title, callBack, showBtn)
{
    var modelDiv;
    var dialogDiv;

    $(document.body).find("#____ModelDiv").remove();
    $(document.body).find("#____DialogDiv").remove();

    $(document.body).append("<div id='____ModelDiv' style='position:absolute;top:0px;left:0px;z-index:9998;filter:alpha(opacity=10);opacity:0.1;position: fixed;'>&nbsp;</div>");
    modelDiv = $("#____ModelDiv");

    $(document.body).append("<div id='____DialogDiv' class='Dialog'><div class='DTitle'><font>" + title + "</font><a class='Dclo' href='javascript:DialogHide();'>&nbsp;Close</a></div><div class='DCnt'></div><div class='DTitle'><input type='button' class='Dbtn' value='确&nbsp;认' /></div></div>");
    dialogDiv = $("#____DialogDiv");

    //给button事件
    dialogDiv.find(".Dbtn").click(function ()
    {
        var canClose = true;
        if (callBack != undefined && callBack != null)
            canClose = callBack();

        if (canClose)
            DialogHide();
    });

    if (showBtn == false)
        dialogDiv.find(".DTitle:eq(1)").hide();

    $(contentObj).show().appendTo(dialogDiv.find(".DCnt"));
    //$("<br class='CLS'/>").appendTo(dialogDiv.find(".DCnt"));

    dialogDiv = $("#____DialogDiv");
    dialogDiv.width(dialogDiv.find(".DCnt *:first").width() + 10);


    /////////////////////////////////
    var W_height = $(top.window).height();
    var S_top = $(top.window).scrollTop();
    //alert(S_top +"|"+ W_height);
    var Ch = parseInt((W_height - dialogDiv.height()) / 2) + S_top;
    /////////////////////////////////
    //var Wh = $(document).height();
    //var Jh = 50;
    //var Ch = parseInt((Wh - dialogDiv.height()) / 2);
    //if (Ch <= 0) { Ch = Jh; $(document.body).height(dialogDiv.height() + Jh * 2); }
    /////////////////////////////////

    /////////////////////////////////
    var f = [$.browser.opera && $.browser.version > "9.5" && $.fn.jquery < "1.3" || $.browser.opera && $.browser.version < "9.5" && $.fn.jquery > "1.2.6" ? $(window)[0].innerHeight : $(window).height(), $(window).width()];
    modelDiv.css({ width: f[1] + "px", height: f[0] + "px", display: "block", "background-color": "#000" });

    dialogDiv.css({ position: "absolute", top: Ch + "px", left: ($(document).width() - dialogDiv.width()) / 2, "z-index": "9999", display: "block" });

    $("#____DialogDiv").easydrag();
    $("#____DialogDiv").setHandler(".DTitle");

    $(window).bind("resize.dialog",
            function ()
            {
                var _f = [$.browser.opera && $.browser.version > "9.5" && $.fn.jquery < "1.3" || $.browser.opera && $.browser.version < "9.5" && $.fn.jquery > "1.2.6" ? $(window)[0].innerHeight : $(window).height(), $(window).width()];
                modelDiv.css({ width: _f[1] + "px", height: _f[0] + "px" });
            });
}

function DialogHide()
{
    $("#____ModelDiv").remove();
    var Obj = $("#____DialogDiv");
    Obj.find(".DCnt *:first").hide().find("br").remove();
    //再次打开[input值/事件]会丢失
    //$(document.body).append(Obj.find(".DCnt").html());
    //[input值/事件] 保留
    Obj.find(".DCnt *:first").appendTo($(document.body));
    Obj.remove();
}

/**
* EasyDrag 1.5 - Drag & Drop jQuery Plug-in
*
* Thanks for the community that is helping the improvement
* of this little piece of code.拖动
*/

(function ($)
{

    // to track if the mouse button is pressed
    var isMouseDown = false;

    // to track the current element being dragged
    var currentElement = null;

    // callback holders
    var dropCallbacks = {};
    var dragCallbacks = {};

    // bubbling status
    var bubblings = {};

    // global position records
    var lastMouseX;
    var lastMouseY;
    var lastElemTop;
    var lastElemLeft;

    // track element dragStatus
    var dragStatus = {};

    // if user is holding any handle or not
    var holdingHandler = false;

    // returns the mouse (cursor) current position
    $.getMousePosition = function (e)
    {
        var posx = 0;
        var posy = 0;

        if (!e) var e = window.event;

        if (e.pageX || e.pageY)
        {
            posx = e.pageX;
            posy = e.pageY;
        }
        else if (e.clientX || e.clientY)
        {
            posx = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            posy = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
        }

        return { 'x': posx, 'y': posy };
    };

    // updates the position of the current element being dragged
    $.updatePosition = function (e)
    {
        var pos = $.getMousePosition(e);

        var spanX = (pos.x - lastMouseX);
        var spanY = (pos.y - lastMouseY);

        $(currentElement).css("top", (lastElemTop + spanY));
        $(currentElement).css("left", (lastElemLeft + spanX));
    };

    // when the mouse is moved while the mouse button is pressed
    $(document).mousemove(function (e)
    {
        if (isMouseDown && dragStatus[currentElement.id] != 'false')
        {
            // update the position and call the registered function
            $.updatePosition(e);
            if (dragCallbacks[currentElement.id] != undefined)
            {
                dragCallbacks[currentElement.id](e, currentElement);
            }

            return false;
        }
    });

    // when the mouse button is released
    $(document).mouseup(function (e)
    {
        if (isMouseDown && dragStatus[currentElement.id] != 'false')
        {
            isMouseDown = false;
            if (dropCallbacks[currentElement.id] != undefined)
            {
                dropCallbacks[currentElement.id](e, currentElement);
            }

            return false;
        }
    });

    // register the function to be called while an element is being dragged
    $.fn.ondrag = function (callback)
    {
        return this.each(function ()
        {
            dragCallbacks[this.id] = callback;
        });
    };

    // register the function to be called when an element is dropped
    $.fn.ondrop = function (callback)
    {
        return this.each(function ()
        {
            dropCallbacks[this.id] = callback;
        });
    };

    // disable the dragging feature for the element
    $.fn.dragOff = function ()
    {
        return this.each(function ()
        {
            dragStatus[this.id] = 'off';
        });
    };

    // enable the dragging feature for the element
    $.fn.dragOn = function ()
    {
        return this.each(function ()
        {
            dragStatus[this.id] = 'on';
        });
    };

    // set a child element as a handler
    $.fn.setHandler = function (handlerId)
    {
        return this.each(function ()
        {
            var draggable = this;

            // enable event bubbling so the user can reach the handle
            bubblings[this.id] = true;

            // reset cursor style
            $(draggable).css("cursor", "");

            // set current drag status
            dragStatus[draggable.id] = "handler";

            // change handle cursor type
            $(handlerId).css("cursor", "pointer");

            // bind event handler
            $(handlerId).mousedown(function (e)
            {
                holdingHandler = true;
                $(draggable).trigger('mousedown', e);
            });

            // bind event handler
            $(handlerId).mouseup(function (e)
            {
                holdingHandler = false;
            });
        });
    }

    // set an element as draggable - allowBubbling enables/disables event bubbling
    $.fn.easydrag = function (allowBubbling)
    {

        return this.each(function ()
        {

            // if no id is defined assign a unique one
            if (undefined == this.id || !this.id.length) this.id = "easydrag" + (new Date().getTime());

            // save event bubbling status
            bubblings[this.id] = allowBubbling ? true : false;

            // set dragStatus 
            dragStatus[this.id] = "on";

            // change the mouse pointer
            $(this).css("cursor", "move");

            // when an element receives a mouse press
            $(this).mousedown(function (e)
            {

                // just when "on" or "handler"
                if ((dragStatus[this.id] == "off") || (dragStatus[this.id] == "handler" && !holdingHandler))
                    return bubblings[this.id];

                // set it as absolute positioned
                $(this).css("position", "absolute");

                // set z-index
                //$(this).css("z-index", parseInt(new Date().getTime() / 1000));

                // update track variables
                isMouseDown = true;
                currentElement = this;

                // retrieve positioning properties
                var pos = $.getMousePosition(e);
                lastMouseX = pos.x;
                lastMouseY = pos.y;

                lastElemTop = this.offsetTop;
                lastElemLeft = this.offsetLeft;

                $.updatePosition(e);

                return bubblings[this.id];
            });
        });
    };

})(jQuery);