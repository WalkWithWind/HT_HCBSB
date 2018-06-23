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
            { pay_status: '', status: '3', expire: '1', text: '已过期' }
        ],
        searchKey: {
            pay_status: '',
            status: '',
            expire: '',
            isme:true,
            page: 0,
            rows: 5
        },
        setTop: {
            idx:-1,
            id: 0,
            set_top: 0,
            money: 0,
            isPay0: false,
            userMoney: 0,
            pay:'微信'
        },
        top_cate_money: 0,
        top_all_money: 0
    },
    created: function() {
        this.init();
    },
    methods:{
        init: function () {
            this.bindScroll();
            this.loadData();
            this.loadConfigData('top_cate_money');//分类置顶金额
            this.loadConfigData('top_all_money');//全站置顶金额
        },
        loadConfigData: function (configName) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: '/Config/Get',
                data: { configName: configName },
                dataType: 'json',
                success: function (resp) {
                    if (resp.status) {
                        if (configName == 'top_cate_money') { _this.top_cate_money = parseFloat(resp.result) };
                        if (configName == 'top_all_money') { _this.top_all_money = parseFloat(resp.result) };
                    }
                }
            });
        },//配置
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
                location.href = '/Project/EditPostGoods/' + item.id;
            } else if (item.cateid == 2) {
                location.href = '/Project/EditPostCars/' + item.id;
            } else if (item.cateid == 3) {
                location.href = '/Project/EditPostRecruit/' + item.id;
            } else if (item.cateid == 4) {
                location.href = '/Project/EditPostJob/' + item.id;
            } else if (item.cateid == 5) {
                location.href = '/Project/EditPostCarSell/' + item.id;
            } else if (item.cateid == 6) {
                location.href = '/Project/EditPostCarBuy/' + item.id;
            } else if (item.cateid == 7) {
                location.href = '/Project/EditPostTemplate/' + item.id;
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
        },
        showSetTop: function (id, idx, item) {
            var _this = this;
            _this.setTop.idx = idx;
            _this.setTop.id = id;
            _this.setTop.set_top = 1;
            _this.setTop.money = _this.top_cate_money;
            _this.setTop.isPay0 = item.pay_status == 0;
            _this.setTop.pay = '余额';
            if (!_this.setTop.isPay0) {
                $.ajax({
                    type: 'post',
                    url: '/User/GetUserMoney',
                    dataType: 'json',
                    success: function (resp) {
                        _this.isLoading = false;
                        if (resp.status) {
                            _this.setTop.userMoney = resp.result;
                            if (_this.setTop.userMoney < _this.setTop.money) _this.setTop.pay = '微信';
                            _this.showSetTopDialog();
                        } else {
                            _this.setTop.userMoney = 0;
                            _this.setTop.pay = '微信';
                            _this.showSetTopDialog();
                        }
                    },
                    error: function () {
                        _this.setTop.userMoney = 0;
                        _this.setTop.pay = '微信';
                        _this.showSetTopDialog();
                    }
                });
            } else {
                _this.showSetTopDialog();
            }
        },
        showSetTopDialog: function () {
            setTimeout(function () {
                layer.open({
                    type: 1,
                    title: '请选择置顶',
                    content: $('.set_top_box'),
                    offset: 'lb',
                    area: ['100%', 'auto'],
                    shade: 0.5,
                    scrollbar: false,
                    anim: 2,
                    end: function () {
                    }
                });
            }, 50);
        },
        selectSetTop: function (num) {
            this.setTop.set_top = num;
            if (num == 1) {
                this.setTop.money = this.top_cate_money;
            } else if (num == 2) {
                this.setTop.money = this.top_all_money;
            }
        },
        confirmSetTop: function () {
            layer.closeAll();
            if (this.listData.list[this.setTop.idx].pay_status == 0) {
                this.postUpdateSetTop();
            } else {
                this.payUpdateSetTop();
            }
        },
        payUpdateSetTop: function () {
            var _this = this;
            if (_this.setTop.pay == '余额') {
                var reqData = {
                    id: _this.setTop.id,
                    set_top: _this.setTop.set_top,
                    money: _this.setTop.money,
                    pay: _this.setTop.pay
                };
                _this.isLoading = true;
                $.ajax({
                    type: 'post',
                    url: '/Project/UpdteSetTop',
                    data: reqData,
                    dataType: 'json',
                    success: function (resp) {
                        _this.isLoading = false;
                        if (resp.status) {
                            _this.listData.list[_this.setTop.idx].set_top = _this.setTop.set_top;
                            layer.closeAll();
                        }
                        alert(resp.msg);
                    }
                });
            } else {
                if (!window.isOnBridgeReady) {
                    alert('请在微信中打开');
                    return;
                }
                $.ajax({
                    type: 'post',
                    url: "/WX/SetTopPay",
                    data: {
                        id: _this.setTop.id,
                        set_top: _this.setTop.set_top,
                        money: _this.setTop.money
                    },
                    dataType: "json",
                    success: function (resp) {
                        if (resp.code == 0) {
                            // 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
                            WeixinJSBridge.invoke('getBrandWCPayRequest', resp.result, function (res) {
                                // 返回res.err_msg,取值 
                                // get_brand_wcpay_request:cancel 用户取消 
                                // get_brand_wcpay_request:fail 发送失败 
                                // get_brand_wcpay_request:ok 发送成功 
                                if (res.err_msg == "get_brand_wcpay_request:ok") {
                                    _this.listData.list[_this.setTop.idx].set_top = _this.setTop.set_top;
                                    layer.closeAll();
                                    alert('支付成功');
                                   // window.location.href = "/WX/PaySuccess/" + "@ViewBag.OrderNo";

                                }
                                else {
                                    alert('支付失败');
                                }
                            });
                        }
                        else {
                            alert(resp.msg);
                        }
                    }
                });
            }
        },
        postUpdateSetTop: function () {
            //console.log('未支付 跳入支付页  /User/Pay/B20180610162542918471765854');
            var _this = this;
            var reqData = {
                id: _this.setTop.id,
                set_top: _this.setTop.set_top,
                money: _this.setTop.money
            };
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Project/UpdteSetTop',
                data: reqData,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        window.location.href = '/User/Pay/' + resp.result;
                    } else {
                        alert(resp.msg);
                    }
                }
            });
        }
    }
});