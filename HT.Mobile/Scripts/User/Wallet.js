var walletVm = new Vue({
    el: '.wallet',
    data: {
        totalAmount: 0,
        walletChildData: {
            list: [],
            total: 0
        },
        searchKey: {
            page: 0,
            rows: 6
        }
    },
    methods: {
        init: function () {
            this.loadData();
            //this.bindScroll();
        },
        loadData: function () {
            var _this = this;
            if (_this.isLoading) return;
            this.searchKey.page++;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/User/UserMoneyLogData',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        if (resp.result.list.length > 0) {
                            _this.walletChildData.list = _this.walletChildData.list.concat(resp.result.list);
                        }
                        _this.totalAmount = resp.result.total_amount;
                        _this.walletChildData.total = resp.result.total;
                        //console.log('_this.walletChildData', _this.walletChildData);
                    }
                }
            });
        },

        //bindScroll: function () {
        //    var _this = this;
        //    $(window).bind('scroll', function (e) {
        //        var _wh = $(window).height();
        //        var _st = $(document).scrollTop();
        //        var _sh = $(document).height();
        //        if (_sh - _st - _wh < 10) {
        //            _this.loadMore();
        //        }
        //    });
        //},

        loadMore: function () {
            var _this = this;
            if (_this.walletChildData.list.length >= _this.walletChildData.total) {
                alert('没有更多了');
                return;
            }
            this.loadData();
        },

    }
});
walletVm.init();