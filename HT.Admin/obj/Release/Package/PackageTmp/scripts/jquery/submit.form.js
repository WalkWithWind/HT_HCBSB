/**
 * 自定义表单提交
 * @param {表单id} form 
 * @param {回调函数} callback 
 * @param {表单提交之前事件处理} beforeSubmit
 */
function ValidSubmit(form, callback,beforeSubmit) {
    $.Tipmsg.r = null;
    $(form).Validform({
        tiptype: function(msg) {
            layer.msg(msg);
        },
        beforeSubmit: beforeSubmit,
        tipSweep: true,
        ajaxPost: true,
        callback: callback != null ? callback : function(e) {
            if (e.status == 1) {
                layer.msg(e.msg);
                if (e.url != null) {
                    setTimeout(function() {
                        location.href = e.url;
                    }, 2000);
                }
            } else {
                layer.msg(e.msg);
            }
        },
        error: function(e) {
            layer.msg(e.statusText);
        }
    });
}