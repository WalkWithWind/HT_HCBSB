var listVm = new Vue({
    el: '.main',
    data: {
        order_no:data.order_no,
        total:data.total,
        money:data.money,
        pay:'余额'
    },
    created:function() {
        this.init();
    },
    methods: {
        init: function(){
            if (this.money < this.total) this.pay = '微信';
        },
        pay: function () {
            if (this.pay == '余额') {
                this.moneyPay();
                return;
            }
            if (this.pay == '微信') {
                window.location.href="/WX/Pay/"+data.order_no;
                return;
            }
        },
        moneyPay: function () {
            var _this = this;
            if (_this.isLoading) return;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/User/PostMoneyPay',
                data: {
                    order_no: _this.order_no
                },
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    layer.msg(resp.msg, {}, function () {
                        if (resp.status) location.href = '/User/PayResult/' + _this.order_no;
                    })
                }
            });
        }
    }
});