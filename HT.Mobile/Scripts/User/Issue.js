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
        tabList: [
            { pay_status: '', status: '', expire:'', text: '全部' },
            { pay_status: '1', status: '2', expire: '0', text: '显示中' },
            { pay_status: '0', status: '0', expire: '0', text: '待付款' },
            { pay_status: '1', status: '1', expire: '0', text: '待审核' },
            { pay_status: '', status: '', expire: '1', text: '已过期' }
        ],
        searchKey: {
            pay_status: '',
            status: '',
            expire: '',
            page: 1,
            rows: 5
        }
    },
    methods:{
        init: function () {
            this.bindScroll();
            this.loadData();
        },
        loadData: function () {
            var _this = this;
            _this.isLoading = true;
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
        selectTab: function (tabLi) {
            var _this = this;
            _this.searchKey.pay_status = tabLi.pay_status;
            _this.searchKey.status = tabLi.status;
            _this.searchKey.expire = tabLi.expire;
            _this.listData.total = 0;
            _this.listData.list = [];
            _this.searchKey.page = 1;
            _this.loadData();
        }
    }
});