
var detailVm = new Vue({
    el: '.mainDetails',
    data: {
        goodsData: {},
        keyWords: {
            id: GetUrlParam('GoodsSourceDetails',0)
        },
        reviewData: {
            total: 0,
            list: []
        },
        searchKey: {
            page: 1,
            rows: 2,
            news_id: GetUrlParam('GoodsSourceDetails', 0),
            review_type:'comment'
        },
        reviewInfo: {
            review_content:'',
            news_id: GetUrlParam('GoodsSourceDetails', 0),
            review_type: '',//comment评论 reply回复
            review_id:0
        },
        likeData: [],//猜你喜欢
        likeSearchKey: {
            page: 1,
            rows: 5,
            min: 1,
            id: GetUrlParam('GoodsSourceDetails', 0)
        },
        layerIndex:0
    },
    methods: {
        init: function () {

            this.loadData();
            this.loadReview();
            this.loadLikeData();
        },
        //详情数据
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
        //加载留言
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
                        for (var i = 0; i < resp.result.list.length; i++) {
                            resp.result.list[i].classOpen = false;
                            resp.result.list[i].classText = '全文';
                        }
                        _this.reviewData.list = _this.reviewData.list.concat(resp.result.list);
                        _this.reviewData.total = resp.result.total;
                        console.log('_this.reviewData', _this.reviewData);
                    }
                }
            });
        },
        //更多留言
        loadMoreReview: function () {
            var _this = this;
            if (_this.reviewData.list.length >= _this.reviewData.total) {
                alert('没有更多了');
                return;
            }
            this.searchKey.page++;
            _this.loadReview();

        },

        //猜你喜欢
        loadLikeData: function () {
            var _this = this;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Project/BaseLikeNewsList',
                data: _this.likeSearchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        _this.likeData = resp.result; 
                        console.log('_this.likeData', _this.likeData);
                    }
                }
            });
        },
        //留言
        addReview: function () {
            var _this = this;
            if (!_this.reviewInfo.review_content) {
                alert('请输入内容');
                return;
            }
            $.ajax({
                type: 'post',
                url: '/Review/AddReview',
                data: _this.reviewInfo,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        layer.close(_this.layerIndex);
                        alert(resp.msg);
                        if (resp.result){
                            resp.result.classOpen = false;
                            resp.result.classText = '全文';
                            _this.reviewInfo.review_content = '';
                            if (_this.reviewInfo.review_type == 'comment') {
                                _this.reviewData.list.insert(0, resp.result);
                            } else if (_this.reviewInfo.review_type == 'reply') {
                                for (var i = 0; i < _this.reviewData.list.length; i++) {
                                    if (_this.reviewData.list[i].id == resp.result.review_id) {
                                        if (_this.reviewData.list[i].reply_list != null) {
                                            _this.reviewData.list[i].reply_list.insert(0, resp.result);
                                        } else {
                                            _this.reviewData.list[i].reply_list = [];
                                            _this.reviewData.list[i].reply_list.push(resp.result);
                                        }
                                    }
                                }
                            }
                        }
                    } else {
                        alert(resp.msg);
                    }
                }
            });
        },
        //显示留言对话框
        showReview: function (value,reviewId) {
            var _this = this;
            _this.reviewInfo.review_type = value;
            if (reviewId) _this.reviewInfo.review_id = reviewId;
            _this.layerIndex=layer.open({
                type: 1,
                title: '留言',
                content: $('.zhp_message_box'),
                offset: 'lb',
                area: ['100%', 'auto'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        //控制留言的全文 收起
        changeClass: function (item) {
            var _this = this;
            item.classOpen = !item.classOpen;
            item.classText = item.classOpen ? '收起' : '全文';
            console.log('item',item);
        }

    }
});
detailVm.init();