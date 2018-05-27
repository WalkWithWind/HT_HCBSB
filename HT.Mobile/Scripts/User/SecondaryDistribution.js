var teamChildVm = new Vue({
    el: '.teamChild',
    data: {
        isLoadAll: false,
        teamChildData: {
            list: [],
            total: 0
        },
        searchKey: {
            page: 1,
            rows: 12,
            parentid: GetUrlParam('SecondaryDistribution',0)
        }
    },
    methods: {
        init: function () {
            this.bindScroll();
            this.loadData();
        },
        loadData: function () {
            var _this = this;
            if (_this.isLoading) return;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/User/TeamChildList',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        if (resp.result.list.length == 0) {
                            _this.isLoadAll = true;
                        } else {
                            _this.teamChildData.list = _this.teamChildData.list.concat(resp.result.list);
                        }
                        _this.teamChildData.total = resp.result.total;
                        console.log('_this.teamChildData', _this.teamChildData);
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
                if ((_sh - _st - _wh < 10) && (!_this.isLoadAll)) {
                    _this.loadMore();
                }
            });
        },

        loadMore: function () {
            var _this = this;
            if (_this.teamChildData.list.length >= _this.teamChildData.total) return;
            this.searchKey.page++;
            this.loadData();
        },

    }
});
teamChildVm.init();