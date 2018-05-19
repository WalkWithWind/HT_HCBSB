
var detailVm = new Vue({
    el: '.mainDetails',
    data: {
        goodsData: {},
        keyWords: {
            id: 8
        },
        reviewData: [],
        searchKey: {
            page: 1,
            rows: 5,
            news_id: 8
        },
        reviewInfo: {
            content:'',
            newsId: 8,
            reviewType: ''//comment评论 reply回复
        }
    },
    methods: {
        init: function () {

            this.loadData();
            this.loadReview();
        },
        loadData: function () {

            var _this = this;

            _this.isLoading = true;

            $.ajax({
                type: 'post',
                url: '/Project/BaseNewsDetails',
                data: _this.keyWords,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        _this.goodsData = resp.result;
                        console.log('_this.goodsData', _this.goodsData);
                    }
                }
            });
        },
        loadReview: function () {

            var _this = this;

            _this.isLoading = true;

            $.ajax({
                type: 'post',
                url: '/Review/ReviewList',
                data: _this.searchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        _this.reviewData = resp.result;
                        console.log('_this.reviewData', _this.reviewData);
                    }
                }
            });



        },
        addReview: function (value) {
            var _this = this;
            if (!_this.reviewInfo.content) {
                alert('请输入内容');
                return;
            }

            if (!value) return;

            _this.reviewInfo.reviewType = value;

            $.ajax({
                type: 'post',
                url: '/Review/AddReview',
                data: _this.reviewInfo,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        alert(resp.msg);
                    } else {
                        alert(resp.msg);
                    }
                }
            });



        }

    }
});
detailVm.init();