
var detailVm = new Vue({
    el: '.mainDetails',
    data: {
        goodsData: {},
        newId: GetUrlParam('GoodsSourceDetails', 0),
        keyWords: {
            id: GetUrlParam('GoodsSourceDetails',0)
        },
        relationSearchKey: {
            main_id:''
        },
        layerIndex:0
    },
    created: function () {
        this.init();
    },
    methods: {
        init: function () {
            this.loadData();
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
        //点赞
        clickPraise:function(news) {
            var _this = this;
            _this.relationSearchKey.main_id = _this.newId;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Relation/AddRelation',
                data: _this.relationSearchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        console.log('resp', resp);
                        news.is_praise = true;
                        news.praise_num++;

                    } else {
                        if (resp.code == 10035) {
                            window.location.href = "/User/Mobile?url=" + encodeURI(window.location.href);
                        } else {
                            alert(resp.msg);
                        }
                    }
                }
            });
        },
        //取消点赞
        cancelClickPraise: function (news) {
            console.log('news',news);
            var _this = this;
            _this.relationSearchKey.main_id = _this.newId;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Relation/DeleteRelation',
                data: _this.relationSearchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        console.log('resp', resp);
                        news.is_praise = false;
                        news.praise_num--;

                    } else {
                        if (resp.code == 10035) {
                            window.location.href = "/User/Mobile?url=" + encodeURI(window.location.href);
                        } else {
                            alert(resp.msg);
                        }
                    }
                }
            });

        }

    }
});