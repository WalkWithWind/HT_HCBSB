
var listVm = new Vue({
    el: '.main',
    data: {
        listData: {
            total: 0,
            list: []
        },
        isLoading: true,
        //isLoadingLayer: -1,
        isLoadAll: false,
        searchKey: {
            cateid: 3,
            start_province: '',
            start_city: '',
            use_type: '',
            car_length: '',
            car_style: '',
            goods_type: '',
            page: 1,
            rows: 5
        },
        select: {
            startProvinceTab: true,
            stopProvinceTab: true
        },
        cityData: dsy,
        useTypeData: [],
        carLengthData: [],
        carStyleData: [],
        goodsTypeData: []
    },
    methods: {
        init: function () {
            this.bindScroll();
            this.loadData();
            this.loadCateData('use_type', 60);
            this.loadCateData('car_length', 101);
            this.loadCateData('car_style', 72);
            this.loadCateData('goods_type', 78);
        },
        loadData: function () {
            var _this = this;
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
                var _st = $('body').get(0).scrollTop;
                var _sh = $('body').get(0).scrollHeight;
                if ((_sh - _st - _wh < 10) && (!_this.isLoadAll)) {
                    _this.loadMore();
                }
            });
        },
        loadMore: function () {
            if (this.listData.list.length >= this.listData.total) return;
            this.searchKey.page++;
            this.loadData();
        },
        searchData: function () {
            var _this = this;
            _this.listData.total = 0;
            _this.listData.list = [];
            _this.searchKey.page = 1;
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
                        if (code == 'use_type') _this.useTypeData = resp.result;
                        if (code == 'car_length') _this.carLengthData = resp.result;
                        if (code == 'car_style') _this.carStyleData = resp.result;
                        if (code == 'goods_type') _this.goodsTypeData = resp.result;
                    }
                }
            });
        },
        showCity: function () {
            layer.open({
                type: 1,
                title: '工作地',
                content: $('.start_box'),
                offset: 'lb',
                area: ['100%', '500px'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        showUseType: function () {
            layer.open({
                type: 1,
                title: '驾照',
                content: $('.use_type_box'),
                offset: 'lb',
                area: ['100%', 'auto'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        showCarLength: function () {
            layer.open({
                type: 1,
                title: '工资待遇',
                content: $('.car_length_box'),
                offset: 'lb',
                area: ['100%', 'auto'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        showCarStyle: function () {
            layer.open({
                type: 1,
                title: '招聘筛选',
                content: $('.car_style_box'),
                offset: 'lb',
                area: ['100%', 'auto'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        selectProvince: function (code, item) {
            var _this = this;
            if (_this.searchKey.start_province != item) _this.searchKey.start_city = '';
            _this.searchKey.start_province = item;
            _this.select.startProvinceTab = false;
        },
        selectCity: function (code, item) {
            var _this = this;
            _this.searchKey.start_city = item;
        },
        selectTabProvince: function (code) {
            var _this = this;
            _this.select.startProvinceTab = true;
        },
        selectTabCity: function (code) {
            var _this = this;
            if (_this.searchKey.start_province == '') return;
            _this.select.startProvinceTab = false;
        },
        resetCity: function (code) {
            var _this = this;
            _this.select.startProvinceTab = true;
            _this.searchKey.start_province = '';
            _this.searchKey.start_city = '';
        },
        confirm: function (code) {
            var _this = this;
            layer.closeAll();
            _this.searchData();
        }
    }
});
listVm.init();