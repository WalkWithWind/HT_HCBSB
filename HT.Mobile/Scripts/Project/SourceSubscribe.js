
var listVm = new Vue({
    el: '.main',
    data: {
        model: {
            start_province: "",//出发地省份
            start_city: '', //出发地城市
            start_district: '', //出发地区域
            stop_province: '',//目的地省份
            stop_city: '',//目的地城市
            stop_district: ''//目的地区域
        },
        select: {
            showCityStart: false,
            showCityStop: false
        },
        listData: {
            total: 0,
            list: []
        },
        isedit: false,
        isLoading: false
    },
    created() {
        this.init();
    },
    methods: {
        init: function () {
            this.loadData();
        },
        loadData: function () {
            var _this = this;
            if (_this.isLoading) return;
            _this.isLoading = true;
            //_this.isLoadingLayer = layer.load(0);
            $.ajax({
                type: 'post',
                url: '/Project/SubscribeList',
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    //layer.close(_this.isLoadingLayer);
                    if (resp.status) {
                        if (resp.result.list.length == 0) {
                            _this.isLoadAll = true;
                        } else {
                            for (var i = 0; i < resp.result.list.length; i++) {
                                resp.result.list[i].del = false;
                            }
                            _this.listData.list = _this.listData.list.concat(resp.result.list);
                        }
                        _this.listData.total = resp.result.total;
                    }
                }
            });
        },
        clickItem: function (item) {
            if (!this.isedit) return;
            if (item.del) {
                item.del = false;
            } else {
                item.del = true;
            }
        },
        add: function () {
            layer.open({
                type: 1,
                title: '货源路线',
                content: $('.add_box'),
                offset: 'lb',
                area: ['100%', 'auto'],
                shade: 0.5,
                scrollbar: false,
                anim: 2,
                end: function () {
                }
            });
        },
        del: function () {
            var _this = this;
            var ids = $.Enumerable.From(_this.listData.list)
                .Where(function (x) { return x.del})
                .Select(function (x) { return x.id })
                .ToArray();
            if (ids.length == 0) {
                _this.isedit = false;
                return;
            }
            $.ajax({
                type: 'post',
                url: '/Project/DelSubscribe',
                data: { ids: ids.join(',') },
                dataType: 'json',
                success: function (resp) {
                    alert(resp.msg);
                    if (resp.status) {
                        for (var i = _this.listData.list.length - 1; i >= 0; i--) {
                            if (ids.indexOf(_this.listData.list[i].id) >= 0) {
                                _this.listData.list.splice(i, 1);
                            }
                        }
                    }
                }
            });
        },
        confirm: function (code) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: '/Project/PostSubscribe',
                data: _this.model,
                dataType: 'json',
                success: function (resp) {
                    alert(resp.msg);
                    if (resp.status) {
                        resp.result.del = false;
                        _this.listData.list = _this.listData.list.concat(resp.result);
                        layer.closeAll();
                    }
                }
            });
        }
    }
});