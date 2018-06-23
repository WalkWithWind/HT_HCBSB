
var listVm = new Vue({
    el: '.main',
    data: {
        listData: {
            total: 0,
            list: []
        },
        searchKey: {
            subscribe_id: GetUrlParam('SourceSubscribe', 0),
            page: 0,
            rows: 5
        },
        isLoading: false
    },
    created:function() {
        this.init();
    },
    methods: {
        init: function () {
            this.bindScroll();
            this.loadData();
        },
        loadData: function () {
            var _this = this;
            if (_this.isLoading) return;
            this.searchKey.page++;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Project/SubscribeNewsList',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        if (resp.result.list.length>0) {
                            _this.listData.list = _this.listData.list.concat(resp.result.list);
                        }
                        _this.listData.total = resp.result.total;
                    }
                }
            });
        },
        bindScroll: function () {
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
        }
    }
});