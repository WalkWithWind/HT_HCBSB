Vue.filter('Date', function (date) {
    if (date == undefined || !date) return;
    date = new Date(date.replace('T', ' '));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var m = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var s = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    return date.getFullYear() + "-" + month + "-" + day + " " + hour + ":" + m + ":" + s;
})
Vue.filter('stringRemove', function (value, defvalue) {
    if (!value) return defvalue;
    var removes = ['null', 'undefined', '省', '省', '市', '直辖市', '自治区', '区', '县', '镇'];
    for (var i = 0; i < removes.length; i++) {
        if (removes[i]) {
            value = value.replace(removes[i], '');
        }
    }
    return value;
})