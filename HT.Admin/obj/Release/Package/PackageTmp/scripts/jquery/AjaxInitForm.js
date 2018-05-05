function AjaxInitForm(formId, btnId, msgId) {
    var formObj = $('#' + formId);
    var btnObj = $("#" + btnId);
    var msgObj = $("#" + msgId);
    formObj.Validform({
        tiptype: function (msg, o, cssctl) {
            var objtip = $("#" + msgId);
            cssctl(objtip, o.type);
            objtip.text(msg);
        },
        showAllError:false,
        ajaxurl:{
            success: function (data, obj) {
                if (data.status == "y") {
                    msgObj.css("color", "green");
                } else {
                    msgObj.css("color", "red");
                }
            }
        },
        callback: function (form) {
            //AJAX提交表单
            $(form).ajaxSubmit({
                success: formResponse,
                url: formObj.attr("url"),
                type: "post",
                dataType: "json",
                timeout: 60000
            });
            return false;
        }
    });


    //表单提交后
    function formResponse(data, textStatus) {

        if (data.status == 1) {
            btnObj.val("提交成功");
            if (typeof (data.url) != "undefined") {
                layer.open({
                    content: data.msg,
                    time: 2
                });
                setTimeout(function () {
                    location.href = data.url;
                }, 1000);
               
            } else {
                layer.open({
                    title: '提示',
                    content: data.msg,
                    btn: ['OK']
                });
            }
        } else if (data.status == 2) {
            location.href = data.url;
        } else {

            //layer.alert(data.msg);
            btnObj.prop("disabled", false);
            btnObj.val("再次提交");

            if (typeof (data.url) != "undefined") {
                layer.open({
                    content: data.msg,
                    time: 2
                });
                setTimeout(function () {
                    location.href = data.url;
                }, 1000);

            } else {
                layer.open({
                    title: '提示',
                    content: data.msg,
                    btn: ['OK']
                });
            }

        }
    }

}



/*切换验证码*/
function ToggleCode(obj, codeurl) {
    $(obj).children("img").eq(0).attr("src", codeurl + "?time=" + Math.random());
    return false;
}