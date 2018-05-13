Vue.filter('dateFormart', function (value, end) {
    if (value == '') return '';
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
