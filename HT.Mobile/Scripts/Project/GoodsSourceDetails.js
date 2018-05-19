
var detailVm = new Vue({
    el: '.main',
    data: {
        goodsData: {},
        searchKey: {
            id:8
        },
    },
    methods: {
        init: function () {

            this.loadData();
        },
        loadData: function () {

            var _this = this;

            _this.isLoading = true;

            $.ajax({
                type: 'post',
                url: '/Project/BaseNewsDetails',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        _this.goodsData = resp.result;
                        console.log('_this.goodsData', _this.goodsData);
                    }
                }
            });
        }

    }
});
detailVm.init();