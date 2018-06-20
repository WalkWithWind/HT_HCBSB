
$(function () {
    if ($('.banner .swiper-container').get(0) != null) {
        var mySwiper = new Swiper('.banner .swiper-container', {
            autoplay: {
                delay: 5000,
                stopOnLastSlide: false,
                disableOnInteraction: true
            },
            pagination: {
                el: '.banner .swiper-pagination'
            },
            loop: true,
        })
    }
    if ($('.zhx_div .btn').get(0) != null && $('.zhx_code').get(0) != null) {
        $('.zhx_div .btn').click(function () {
            layer.open({
                type: 1,
                title: false,
                skin: 'layui-layer-zhx',
                content: $('.zhx_code'),
                area: ['260px', 'auto'],
                closeBtn: 1,
                shade: 0.3,
                shadeClose: false,
                scrollbar: false,
                anim: 2
            });
        })
    }
});



Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

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
function IsPhone(mobile) {
    var myreg = /^[1][3,4,5,7,8][0-9]{9}$/;
    if (!myreg.test(mobile)) {
        return false;
    } else {
        return true;
    }
}  

