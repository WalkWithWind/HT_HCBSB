
var listVm = new Vue({
    el: '.main',
    data: {
        listData: {
            total: 0,
            list: []
        },
        isLoading: false,
        noneData: false,
        //isLoadingLayer: -1,
        //isLoadAll: false,
        searchKey: {
            cateid: 2,
            status: 1,
            expire: 0,
            start_province: '',
            start_city: '',
            start_district: '',
            stop_province: '',
            stop_city: '',
            stop_district: '',
            car_length: '',
            car_style: '',
            page: 0,
            rows: 5
        },
        select: {
            showCityStart: false,
            showCityStop: false
        },
        carLengthData: [],
        carStyleData: []
    },
    methods: {
        init: function () {
            this.bindScroll();
            this.loadData();
            this.loadCateData('car_length', 4);
            this.loadCateData('car_style', 16);
        },
        loadData: function () {
            var _this = this;
            if (_this.isLoading) return;
            _this.searchKey.page++;
            _this.isLoading = true;
            //_this.isLoadingLayer = layer.load(0);
            $.ajax({
                type: 'post',
                url: '/Project/BaseNewsList',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    //layer.close(_this.isLoadingLayer);
                    if (resp.status) {
                        if (resp.result.list.length == 0) {
                            _this.isLoadAll = true;
                        } else {
                            _this.listData.list = _this.listData.list.concat(resp.result.list);
                        }
                        _this.listData.total = resp.result.total;
                        if (_this.listData.total == 0) {
                            _this.noneData = true;
                        }
                        //console.log(_this.listData.list);
                    }
                }
            });
        },
        bindScroll: function () {
            //console.log($(window).scrollTop());
            var _this = this;
            $(window).bind('scroll', function (e) {
                var _wh = $(window).height();
                var _st = $(document).scrollTop();
                var _sh = $(document).height();
                if (_sh - _st - _wh < 10) {
                    _this.loadMore();
                }
            });
        },
        loadMore: function () {
            if (this.listData.list.length >= this.listData.total) return;
            this.loadData();
        },
        searchData: function () {
            var _this = this;
            _this.listData.total = 0;
            _this.listData.list = [];
            _this.searchKey.page = 0;
            _this.loadData();
        },
        loadCateData: function (code, cid) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: '/Home/CateList',
                data: { cid: cid },
                dataType: 'json',
                success: function (resp) {
                    if (resp.status) {
                        if (code == 'car_length') _this.carLengthData = resp.result;
                        if (code == 'car_style') _this.carStyleData = resp.result;
                    }
                }
            });
        },
        showCarLength: function () {
            layer.open({
                type: 1,
                title: '车源筛选',
                content: $('.car_length_box'),
                offset: 'lb',
                area: ['100%', 'auto'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        confirm: function (code) {
            var _this = this;
            layer.closeAll();
            _this.searchData();
        }
    }
});
listVm.init();