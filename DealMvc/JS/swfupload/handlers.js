
var PhotoIndex = 1;
function fileQueueError(file, errorCode, message) {
    try {
        var imageName = "error.gif";
        var errorName = "";
        if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
            errorName = "You have attempted to queue too many files.";
        }

        if (errorName !== "") {
            alert(errorName);
            return;
        }

        switch (errorCode) {
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                imageName = "zerobyte.gif";
                break;
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                imageName = "toobig.gif";
                break;
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
            default:
                alert(message);
                break;
        }

        addImage("images/" + imageName);

    } catch (ex) {
        this.debug(ex);
    }

}
function fileQueued(file, errorCode, message) {
    try {
    } catch (ex) {
        this.debug(ex);
    }

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
    try {
        if (numFilesQueued > 0) {
            this.startUpload();
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadProgress(file, bytesLoaded) {
    try {
        $(".SWUploadState").show();
        var percent = Math.ceil((bytesLoaded / file.size) * 100);

        var progress = new FileProgress(file, this.customSettings.upload_target);
        progress.setProgress(percent);
        if (percent === 100) {
            progress.setStatus("Creating thumbnail...");
            progress.toggleCancel(false, this);
        } else {
            progress.setStatus("Uploading...");
            progress.toggleCancel(true, this);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadSuccess(file, serverData) {
    try {
        var jsonDate = eval('(' + serverData + ')');
        if (jsonDate.msg == 'err') {
            CommObj.Msg(jsonDate.alt);
        } else if (jsonDate.msg == 'success') {
            $(".Imges").html($(".Imges").html() + "<div value2='PhotoName" + PhotoIndex + "' class='allImg' style=' position: relative;width:200px;float:left; margin-top:10px; margin-bottom:10px; text-align:center;'><div style=' height: 22px;left: 0;overflow: hidden;position: absolute;top: 0;width: 100%;'><div style='background-color: #FFFFFF;height: 100%;opacity: 0.7;width: 100%;'></div><a value1='PhotoName" + PhotoIndex + "' style='top: 3px;left: 88px;color: #0082AA;font-family: 宋体;font-size: 12px; position: absolute;text-decoration: none;' class='DeleteBtn CP'>删除</a></div><img src='" + jsonDate.b + "' width='100px' height='109px;' /><br /><input name='PhotoName" + PhotoIndex + "' type='text'  style=' width:100px; margin-top:10px;' value='" + jsonDate.a + "' /><br /><input type='text' name='PhotoImagePath" + PhotoIndex + "'  style='display:none; width:100px; margin-top:10px;' value='" + jsonDate.b + "' /></div>");
            PhotoIndex++;
        }
        addImage("thumbnail.aspx?id=" + serverData);

        var progress = new FileProgress(file, this.customSettings.upload_target);

        progress.setStatus("Thumbnail Created.");
        progress.toggleCancel(false);


    } catch (ex) {
        this.debug(ex);
    }
}

function uploadComplete(file) {
    try {
        /*  I want the next upload to continue automatically so I'll call startUpload here */
        if (this.getStats().files_queued > 0) {
            this.startUpload();
        } else {
            var progress = new FileProgress(file, this.customSettings.upload_target);
            progress.setComplete();
            progress.setStatus("All images received.");
            progress.toggleCancel(false);
            $(".SWUploadState").hide();
        }
    } catch (ex) {
        $(".SWUploadState").hide();
        this.debug(ex);
    }
}

function uploadError(file, errorCode, message) {
    var imageName = "error.gif";
    var progress;
    try {
        switch (errorCode) {
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                try {
                    progress = new FileProgress(file, this.customSettings.upload_target);
                    progress.setCancelled();
                    progress.setStatus("Cancelled");
                    progress.toggleCancel(false);
                }
                catch (ex1) {
                    this.debug(ex1);
                }
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                try {
                    progress = new FileProgress(file, this.customSettings.upload_target);
                    progress.setCancelled();
                    progress.setStatus("Stopped");
                    progress.toggleCancel(true);
                }
                catch (ex2) {
                    this.debug(ex2);
                }
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                imageName = "uploadlimit.gif";
                break;
            default:
                alert(message);
                break;
        }

        addImage("images/" + imageName);

    } catch (ex3) {
        this.debug(ex3);
    }

}


function addImage(src) {
    var newImg = document.createElement("img");
    newImg.style.margin = "5px";

    document.getElementById("thumbnails").appendChild(newImg);
    if (newImg.filters) {
        try {
            newImg.filters.item("DXImageTransform.Microsoft.Alpha").opacity = 0;
        } catch (e) {
            // If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
            newImg.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + 0 + ')';
        }
    } else {
        newImg.style.opacity = 0;
    }

    newImg.onload = function () {
        fadeIn(newImg, 0);
    };
    newImg.src = src;
}

function fadeIn(element, opacity) {
    var reduceOpacityBy = 5;
    var rate = 30; // 15 fps

    if (opacity < 100) {
        opacity += reduceOpacityBy;
        if (opacity > 100) {
            opacity = 100;
        }

        if (element.filters) {
            try {
                element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
            } catch (e) {
                // If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
                element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
            }
        } else {
            element.style.opacity = opacity / 100;
        }
    }

    if (opacity < 100) {
        setTimeout(function () {
            fadeIn(element, opacity);
        }, rate);
    }
}



/* ******************************************
*	FileProgress Object
*	Control object for displaying file info
* ****************************************** */

function FileProgress(file, targetID) {
    this.fileProgressID = "divFileProgress";
    this.fileProgressWrapper = document.getElementById(this.fileProgressID);
    if (!this.fileProgressWrapper) {
        this.fileProgressWrapper = document.createElement("div");
        this.fileProgressWrapper.className = "progressWrapper";
        this.fileProgressWrapper.id = this.fileProgressID;

        this.fileProgressElement = document.createElement("div");
        this.fileProgressElement.className = "progressContainer";

        var progressCancel = document.createElement("a");
        progressCancel.className = "progressCancel";
        progressCancel.href = "#";
        progressCancel.style.visibility = "hidden";
        progressCancel.appendChild(document.createTextNode(" "));

        var progressText = document.createElement("div");
        progressText.className = "progressName";
        progressText.appendChild(document.createTextNode(file.name));

        var progressBar = document.createElement("div");
        progressBar.className = "progressBarInProgress";

        var progressStatus = document.createElement("div");
        progressStatus.className = "progressBarStatus";
        progressStatus.innerHTML = "&nbsp;";

        this.fileProgressElement.appendChild(progressCancel);
        this.fileProgressElement.appendChild(progressText);
        this.fileProgressElement.appendChild(progressStatus);
        this.fileProgressElement.appendChild(progressBar);

        this.fileProgressWrapper.appendChild(this.fileProgressElement);

        document.getElementById(targetID).appendChild(this.fileProgressWrapper);
        fadeIn(this.fileProgressWrapper, 0);

    } else {
        this.fileProgressElement = this.fileProgressWrapper.firstChild;
        this.fileProgressElement.childNodes[1].firstChild.nodeValue = file.name;
    }

    this.height = this.fileProgressWrapper.offsetHeight;

}
FileProgress.prototype.setProgress = function (percentage) {
    this.fileProgressElement.className = "progressContainer green";
    this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
    this.fileProgressElement.childNodes[3].style.width = percentage + "%";
};
FileProgress.prototype.setComplete = function () {
    this.fileProgressElement.className = "progressContainer blue";
    this.fileProgressElement.childNodes[3].className = "progressBarComplete";
    this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setError = function () {
    this.fileProgressElement.className = "progressContainer red";
    this.fileProgressElement.childNodes[3].className = "progressBarError";
    this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setCancelled = function () {
    this.fileProgressElement.className = "progressContainer";
    this.fileProgressElement.childNodes[3].className = "progressBarError";
    this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setStatus = function (status) {
    this.fileProgressElement.childNodes[2].innerHTML = status;
};

FileProgress.prototype.toggleCancel = function (show, swfuploadInstance) {
    this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
    if (swfuploadInstance) {
        var fileID = this.fileProgressID;
        this.fileProgressElement.childNodes[0].onclick = function () {
            swfuploadInstance.cancelUpload(fileID);
            return false;
        };
    }
};


function uploadSuccessBBSPhoto(file, serverData) {
    try {
        //        $(".PhotoTable").append(serverData);
        $("#NTemImgUrl").val(serverData);
        $("#NMBImage").attr("src", serverData);
    } catch (ex) {
        this.debug(ex);
    }
}

function qford_getimagesize(filepath) {
    alert("你");
    var imgsize = {
        width: 0,
        height: 0
    };
    var image = new Image();
    image.src = filepath;
    imgsize.width = image.width;
    imgsize.height = image.height;
    alert(image.width + "----" + image.height);
    return imgsize;
}

function checkimg(img) {
    var message = "";
    var maxwidth = 1; //设置图片宽度界限
    var maxheight = 1; //设置图片高度界限

    if (img.readystate != "complete") {

        return false; //确保图片完全加载

    }
    if (img.offsetheight > maxheight) message += "r高度超额：" + img.offsetheight;
    if (img.offsetwidth > maxwidth) message += "r宽度超额：" + img.offsetwidth;
    if (message != "") alert(message);
}

function uploadSuccessUserPhoto(file, serverData) {
    try {
        //$(".PhotoTable").append(serverData);
        $("object").hide();
        $("#CJImage").val(serverData);
     
        $("#PICImg").attr("src", serverData);
        $("#PhotoImgCon").show();

        var api = $.Jcrop('#PICImg', {
            setSelect: [250, 250, 10, 10],
            onSelect: updateCoords, aspectRatio: 1 / 1
        });
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadSuccessUserPhoto_M(file, serverData) {
    try {
        //$(".PhotoTable").append(serverData);
        $("object").hide();
        $("#CJImageL").val(serverData);
        $("#PICImgG").attr("src", serverData);
        $("#PhotoImgConN").show();
        var api = $.Jcrop('#PICImgG', {
            setSelect: [250, 250, 10, 10],
            onSelect: updateCoords, aspectRatio: 1 / 1
        });

    } catch (ex) {
        this.debug(ex);
    }
}


function updateCoords(c) {
    $('#IX').val(c.x);
    $('#IY').val(c.y);
    $('#IW').val(c.w);
    $('#IH').val(c.h);
}

function updateCoordpics(c) {
    $('#IXX').val(c.x);
    $('#IYY').val(c.y);
    $('#IWW').val(c.w);
    $('#IHH').val(c.h);
}

function uploadSuccessBBSFile(file, serverData) {
    try {
        $(".FileTable").append(serverData);

    } catch (ex) {
        this.debug(ex);
    }
}

//后台论坛上传图片成功回调方法
function CmsUpPhotoSuccess(file, serverData) {
    $(".ModelPhotos").append(serverData);
}

//后台论坛上传附件成功回调方法
function CmsUpFileSuccess(file, serverData) {
    $(".CmsPostFilesCon").append(serverData);
}
