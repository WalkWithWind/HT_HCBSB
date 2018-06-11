
var mainDetails = new Vue({
    el: '.details',
    data: {
        newsData: {},
        id: GetUrlParam('CarSellDetails', 0),
        imgs:[],
        newsSearchKey: {
            id: 0
        },
        relationSearchKey: {
            main_id: ''
        },
        layerIndex: 0
    },
    methods: {
        init: function () {
            this.loadData();
        },
        //详情数据
        loadData: function () {
            var _this = this;
            _this.newsSearchKey.id = _this.id;
            _this.isLoading = true;
            $.ajax({
                type: 'post',
                url: '/Project/BaseNewsDetails',
                data: _this.newsSearchKey,
                dataType: 'json',
                success: function (resp) {
                    _this.isLoading = false;
                    if (resp.status) {
                        _this.newsData = resp.result;
                        if (_this.newsData.imgs) {
                            _this.imgs = _this.newsData.imgs.split(',');
                            _this.showSwiper();
                        }
                        //console.log('_this.newsData', _this.newsData);
                    }
                }
            });
        },
        showSwiper: function () {
            var _this = this;
            //if (_this.imgs && _this.imgs.length > 1) {
                setTimeout(function () {
                    var mySwiper = new Swiper('.zhc_container.swiper-container', {
                        autoplay: 5000,
                        navigation: {
                            nextEl: '.zhc_container .swiper-button-next',
                            prevEl: '.zhc_container .swiper-button-prev',
                        },
                        pagination: {
                            el: '.zhc_container .swiper-pagination',
                            type: 'fraction'
                        },
                        spaceBetween: 6,
                        preventClicks: false,
                        loop: true,
                    });
                    //$('.zhc_container.swiper-container .swiper-slide img').click(function () {
                    //    var ob = $(this).clone();
                    //    $(ob).css('max-width', 'none');
                    //    $(ob).css('display', 'block');
                    //    var html = $(ob).prop("outerHTML")
                    //    layer.open({
                    //        type: 1,
                    //        title: '图片',
                    //        content: html,
                    //        offset: 'lb',
                    //        area: ['100%', '100%'],
                    //        shade: 0.5,
                    //        scrollbar: true,
                    //        anim: 2
                    //    });
                    //});
                }, 10);
            //}
        },
        //点赞
        clickPraise: function (news) {
            var _this = this;
            _this.relationSearchKey.main_id = news.id;
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
            console.log('news', news);
            var _this = this;
            _this.relationSearchKey.main_id = news.id;
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
mainDetails.init();