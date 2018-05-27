



var teamVm = new Vue({
    el: '.main',
    data: {
        isLoadAll: false,
        teamData: {
            list: [],
            total: 0,
            total_money: 0,
            total_people_num: 0,
        },
        searchKey: {
            page: 0,
            rows: 10
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
            this.searchKey.page++;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/User/TeamList',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        if (resp.result.list.length > 0) {
                            _this.teamData.list = _this.teamData.list.concat(resp.result.list);
                        }
                        _this.teamData.total = resp.result.total;
                        _this.teamData.total_money = resp.result.total_money;
                        _this.teamData.total_people_num = resp.result.total_people_num;
                        console.log('_this.teamData', _this.teamData);
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
            var _this = this;
            if (_this.teamData.list.length >= _this.teamData.total) return;
            this.loadData();
        },
       
    }
});
teamVm.init();