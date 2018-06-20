
var mainDetails = new Vue({
    el: '.details',
    data: {
        newsData: {},
        id: GetUrlParam('CarSellDetails', 0),
        imgs: [],
        newsSearchKey: {
            id: 0
        },
        relationSearchKey: {
            main_id: ''
        },
        layerIndex:0,
        items: [],
        imgobs: [],
        gallery: 0,
        loadItr: -1,
        wWidth:0,
        PhotoSwipeUI_Default: PhotoSwipeUI_Default
    },
    //呈现前构造数据
    mounted: function () {
        this.wWidth = $(window).width();
        this.init();
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
                            _this.initPhotoData();
                            _this.showSwiper();
                        }
                    }
                }
            });
        },
        checkImgLoad: function () {
            var _this = this;
            var num = 0;
            for (var i = 0; i < _this.imgobs.length; i++) {
                if (_this.imgobs[i].width > 0 || _this.imgobs[i].height > 0) {
                    _this.items[i].w = _this.imgobs[i].width;
                    _this.items[i].h = _this.imgobs[i].height;
                    num++;
                }
            }
            if (num >= _this.imgobs.length) clearInterval(_this.loadItr);
        },
        initPhotoData: function () {
            var _this = this;
            if (_this.imgs.length > 0) {
                for (var i = 0; i < _this.imgs.length; i++) {
                    var _src = _this.imgs[i];
                    _this.items[i] = { src: _src };
                    // 创建对象
                    _this.imgobs[i] = new Image();
                    // 改变图片的src
                    _this.imgobs[i].src = _src;
                }
                _this.loadItr = setInterval(_this.checkImgLoad);
            }
        },
        openPhotoSwipe:function () {
            var pswpElement = $('.pswp').get(0);
            this.gallery = new PhotoSwipe(pswpElement, this.PhotoSwipeUI_Default, this.items, {});
            this.gallery.init();
        },
        showSwiper: function () {
            var _this = this;
            setTimeout(function () {
                if (_this.imgs && _this.imgs.length > 1) {
                    var mySwiper = new Swiper('.zhc_container.swiper-container', {
                        autoplay: {
                            delay: 5000,
                            stopOnLastSlide: false,
                            disableOnInteraction: true
                        },
                        navigation: {
                            nextEl: '.zhc_container .swiper-button-next',
                            prevEl: '.zhc_container .swiper-button-prev',
                        },
                        pagination: {
                            el: '.zhc_container .swiper-pagination',
                            type: 'fraction'
                        },
                        spaceBetween: 6,
                        loop: true
                    });
                }
                $('.zhc_container.swiper-container .swiper-slide img').click(function () {
                    _this.openPhotoSwipe();
                });
            }, 40);
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