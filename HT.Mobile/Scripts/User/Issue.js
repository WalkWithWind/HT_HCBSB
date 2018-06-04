var listVm = new Vue({
    el: '.main',
    data: {
        listData: {
            total: 0,
            list: []
        },
        isLoading: false,
        //isLoadingLayer: -1,
        tabList: [
            { pay_status: '', status: '', expire:'', text: '全部' },
            { pay_status: '1', status: '1', expire: '0', text: '显示中' },
            { pay_status: '0', status: '0', expire: '0', text: '待付款' },
            { pay_status: '1', status: '0', expire: '0', text: '待审核' },
            { pay_status: '', status: '', expire: '1', text: '已过期' }
        ],
        searchKey: {
            pay_status: '',
            status: '',
            expire: '',
            isme:true,
            page: 0,
            rows: 5
        }
    },
    created() {
        this.init();
    },
    methods:{
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
                            for (var i = 0; i < resp.result.list.length; i++) {
                                resp.result.list[i].status_text = _this.statusCheck(resp.result.list[i]);
                            }
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
        selectTab: function (tabLi) {
            var _this = this;
            _this.searchKey.pay_status = tabLi.pay_status;
            _this.searchKey.status = tabLi.status;
            _this.searchKey.expire = tabLi.expire;
            _this.listData.total = 0;
            _this.listData.list = [];
            _this.searchKey.page = 0;
            _this.loadData();
        },
        statusCheck: function (item) {
            var value = parseInt(item.add_time.replace(/\/Date\((\d+)\)\//gi, "$1"));
            value = new Date(value);
            if (item.validity_unit == '月') {
                value.setMonth(value.getMonth() + item.validity_num);
            }
            else if (item.validity_unit == '天') {
                value.setDate(value.getDate() + item.validity_num);
            }

            var curDate = new Date();
            if (value.getTime() < curDate.getTime()) return '已过期';

            if (item.pay_status == 0) return '待付款';
            if (item.status == 0) return '待审核';
            if (item.status == 2) return '审核不通过';

            var year = value.getFullYear();
            var month = value.getMonth() + 1 < 10 ? "0" + (value.getMonth() + 1) : value.getMonth() + 1;
            var day = value.getDate() < 10 ? "0" + value.getDate() : value.getDate();
            if (year == curDate.getFullYear()) return "显示中 " + month + "月" + day + "日 " + "过期";
            return "显示中 " + year + "年" + month + "月" + day + "日 " + "过期";
        },
        toEdit: function (item) {
            if (item.cateid == 1) {
                location.href = '/Project/PostGoods/' + item.id;
            } else if (item.cateid == 2) {
                location.href = '/Project/PostCars/' + item.id;
            } else if (item.cateid == 3) {
                location.href = '/Project/PostRecruit/' + item.id;
            } else if (item.cateid == 4) {
                location.href = '/Project/PostJob/' + item.id;
            } else if (item.cateid == 5) {
                location.href = '/Project/PostCarSell/' + item.id;
            } else if (item.cateid == 6) {
                location.href = '/Project/PostCarBuy/' + item.id;
            } else if (item.cateid == 7) {
                location.href = '/Project/PostTemplate/' + item.id;
            }
        },
        del: function (item,index) {
            var _this = this;
            if (_this.isLoading) return;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Project/PostDel',
                data: {id:item.id},
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        _this.listData.list.splice(index, 1);
                        layer.msg(resp.msg);
                    }
                }
            });
        }
    }
});