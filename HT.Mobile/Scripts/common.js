
$(function() {
    //tab切换
    //var $product_tab = $(".tab-box")
    //$product_tab.each(function() {
    //    var _this = $(this);
    //    var $title = _this.find(".tab-title");
    //    var $content = _this.find(".tab-content");
    //    $title.find("li").click(function () {
    //        if ($(this).hasClass('click-off')) return false;
    //        var index = $(this).index();
    //        $title.find("li").removeClass("active");
    //        $content.hide();
    //        $(this).addClass("active");
    //        $content.eq(index).fadeIn(500);
    //    })
    //})

});






Array.prototype.insert = function (index, item) {
    this.splice(index, 0, item);
};  

//layer提示
window.alert = function (msg, icon, time, fn) {
    if (!time) time = 2;
    return layer.msg(msg, {
        icon: icon,
        shadeClose: true,
        time: time * 1000 //2秒关闭（如果不配置，默认是3秒）
    }, function () {
        if (!!fn) fn();
    });
};
//layer 等待提示
window.progress = function (type, time) {
    var option = {
        //shadeClose :true,
        shade: 0.1
    }
    if (!!time) option.time = time * 1000;
    return layer.load(type, option);
};
//layer 确认对话框
window.confirm = function (title, msg, yesText, cancelText, yesFn, cancelFn) {
    return layer.confirm(msg, {
        title: title,
        closeBtn: 0,
        //shadeClose: true,
        btn: [yesText, cancelText] //按钮
    }, function (index, layerDom) {
        if (!!yesFn) yesFn(index, layerDom);
    }, function (index) {
        if (!!cancelFn) cancelFn(index);
    });
};



function GetUrlParam(_k, _i) {
    var _h = window.location.href;
    _h = _h.substr(_h.lastIndexOf(_k) + _k.length+1);
    return _h.split('/')[_i];
}

