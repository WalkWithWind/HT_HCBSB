Vue.filter('dateFormart', function (value, end) {
    if (value == '' || value == undefined) return '';
    value = parseInt(value.replace(/\/Date\((\d+)\)\//gi, "$1"));
    value = new Date(value)
    var date = new Date().getTime() - value.getTime();
    var minutes = Math.floor(date / (60 * 1000));
    if (minutes == 0) {
        return "刚刚" + end;
    }
    else if (minutes >= 1 && minutes < 60) {
        return minutes + "分钟前" + end;
    }
    else if (minutes >= 60 && minutes < 60 * 24) {
        return Math.floor(minutes / 60)+ "小时前" + end;
    }
    else if (minutes >= 60 * 24 && minutes < 60 * 24 * 30) {
        return Math.floor(minutes / 60 / 24) + "天前" + end;
    }
    else if (minutes > 60 * 24 * 30 && minutes < 60 * 24 * 30 * 12) {
        return Math.floor(minutes / 60 / 24 / 30) + "月前" + end;
    }
    else
        return value.getFullYear() + "年" + end;
});
Vue.filter('moneyFormart', function (value) {
    if (value == '') return '';
    var result = Math.floor(value /10000);
    if (result > 10) {
        return result + "万";
    }
    else
        return value + "元";
});

Vue.filter('jsonFormart', function (jsonDate) {
    if (jsonDate == undefined || !jsonDate) return;
    var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + day;
})