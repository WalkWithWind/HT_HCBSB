﻿
var mainDetails = new Vue({
    el: '.template',
    data: {
        newsData: {},
        id: GetUrlParam('TemplateDetails', 0),
        imgs: [],
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
                        _this.imgs = _this.newsData.imgs.split(',');
                        _this.showSwiper();
                        console.log('_this.newsData', _this.newsData);
                    }
                }
            });
        },
        showSwiper: function () {
            setTimeout(function () {
                var mySwiper = new Swiper('.zhc_container.swiper-container', {
                    autoplay: 5000,
                    height: 300,//你的slide高度
                    prevButton: '.swiper-button-next',
                    nextButton: '.swiper-button-prev',
                    pagination: '.zhc_container .swiper-pagination',
                    paginationType: 'fraction',
                    spaceBetween: 6,
                    loop: true,
                })
            }, 10);
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