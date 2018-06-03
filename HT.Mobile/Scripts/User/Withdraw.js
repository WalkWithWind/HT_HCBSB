var withdrawVm = new Vue({
    el: '.withdraw',
    data: {
        isLoading: false,
        money:''
    },
    methods: {

        withdraw: function () {
            var _this = this;

            if (!_this.money) {
                alert('请输入要提现的金额');
                return;
            }
            if (_this.isLoading) return;
            _this.isLoading = true;
            var reqData = {
                money: _this.money
            };
            $.ajax({
                type: 'post',
                url: '/User/AddUserMoneyLog',
                data: reqData,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        window.location.href = "/User/WithdrawSuccess";
                    } else {
                        alert(resp.msg);
                    }
                }
            });
        },

       

    }
});