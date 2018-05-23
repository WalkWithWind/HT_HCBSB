var listVm = new Vue({
    el: '.main',
    data: {
        username: '',
        password:'',
        isLoading: false
    },
    methods: {
        login: function () {
            var _this = this;
            if (_this.isLoading) return;
            if (!_this.username) {
                alert('请输入账号', null, null);
                return;
            }
            if (!_this.password) {
                alert('请输入密码', null, null);
                return;
            }
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/User/TestLogin',
                data: {
                    username: _this.username,
                    password: _this.password
                },
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        layer.msg(resp.msg, {}, function () {
                            location.href = '/User/Index';
                        })
                    }
                }
            });
        }
    }
});