Vue.filter('date', function (date) {
    if (date == undefined || !date) return;
    var dpl = date.substr(0, date.indexOf('.')).replace('T', '-').replace(':', '-').replace(':', '-').split('-');
    date = new Date(parseInt(dpl[0]), parseInt(dpl[1])-1, parseInt(dpl[2]), parseInt(dpl[3]), parseInt(dpl[4]), parseInt(dpl[5]));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var m = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var s = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    return date.getFullYear() + "-" + month + "-" + day + " " + hour + ":" + m + ":" + s;
})
Vue.filter('stringRemove', function (value, defvalue) {
    if (!value) return defvalue;
    var removes = ['null', 'undefined'];
    for (var i = 0; i < removes.length; i++) {
        if (removes[i]) {
            value = value.replace(removes[i], '');
        }
    }
    return value;
})
Vue.filter('cityFormart', function (city, district, province, defvalue) {
    if (!city || city == 'null' || city == 'undefined') city = '';
    if (!district || district == 'null' || district == 'undefined') district = '';
    if (!province || province == 'null' || province == 'undefined') province = '';
    if (!city && !district && !province) return defvalue;
    if ((city + district)) return city + district;
    return province;
})