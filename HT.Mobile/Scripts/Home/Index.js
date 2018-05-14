var listVm = new Vue({
    el: '.main',
    data: {
        listData: {
            total: 0,
            list: []
        },
        isLoading: true,
        isLoadAll: false,
        searchKey: {
            recommend: true,
            page: 1,
            rows: 5
        }
    },
    methods: {
        init: function () {
            this.bindScroll();
            this.loadData();
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
        }
    }
});
listVm.init();