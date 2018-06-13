
// 发布
var vue = new Vue({
    el: '.main',
    data: {
        model: {
            cateid: 6,//类型 6车辆求购
            cate: "车辆求购",//车辆求购
            validity_num: "",//有效期
            validity_unit: "天",//有效期单位 天,月
            start_province: "",//车辆所在地省份
            start_city: '', //车辆所在地城市
            start_district: '', //车辆所在地区域
            use_type: '',//品牌
            recruit_num:"",//车龄
            car_style: '',//车型
            goods_weight: "",//马力
            other_remark: "",//其它补充
            contact_name: "",//联系人
            contact_phone: "",//联系电话
            set_top: "",//置顶类型  空不置顶 1分类 2全站
            set_top_money: 0,//置顶金额
            reward_money: 0,//打赏金额
            total: 0//需支付金额
        },
        carStyleSelect: [],//选中的车型
        useTypeData: [],//品牌列表
        carStyleData: [],//车型列表
        rewardMoneyData: [],//打赏金额列表
        top_cate_select: false,//是否选中分类置顶
        top_all_select: false,//是否选中全站置顶
        reward_select: false,//是否选中赏福利
        top_cate_money: 0,//分类置顶金额
        top_all_money: 0,//全站置顶金额
        validity_unit_day_money: 0,// 发布费用 元/天
        validity_unit_month_money: 0,// 发布费用 元/月
        select: {
            showCityStart: false
        }
    },
    watch: {
        'model.validity_num': function (val, oldval) {
            this.calcTotal();
        },
        'model.validity_unit': function (val, oldval) {
            this.calcTotal();
        },
        'model.reward_money': function (val, oldval) {
            this.calcTotal();
        },
        'model.set_top_money': function (val, oldval) {
            this.calcTotal();
        }
    },
    methods: {
        init: function () {
            this.loadCateData('car_style', 16);//车型
            this.loadCateData('reward_money', 55);//打赏福利列表

            this.loadConfigData('top_cate_money');//分类置顶金额
            this.loadConfigData('top_all_money');//全站置顶金额
            this.loadConfigData('pub_amount_day');//发布费用 元/天
            this.loadConfigData('pub_amount_month');//发布费用 元/月

            this.getDetail();

        },
        loadCateData: function (code, cid) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: '/Home/CateList',
                data: { cid: cid },
                dataType: 'json',
                success: function (resp) {
                    if (resp.status) {
                        if (code == 'use_type') { _this.useTypeData = resp.result };
                        if (code == 'car_style') { _this.carStyleData = resp.result };
                        if (code == 'reward_money') { _this.rewardMoneyData = resp.result };

                    }
                }
            });
        },
        loadConfigData: function (configName) {
            var _this = this;
            $.ajax({
                type: 'post',
                url: '/Config/Get',
                data: { configName: configName },
                dataType: 'json',
                success: function (resp) {
                    if (resp.status) {
                        if (configName == 'top_cate_money') { _this.top_cate_money = parseFloat(resp.result); _this.topCate(); };
                        if (configName == 'top_all_money') { _this.top_all_money = parseFloat(resp.result) };
                        if (configName == 'pub_amount_day') { _this.validity_unit_day_money = parseFloat(resp.result) };
                        if (configName == 'pub_amount_month') { _this.validity_unit_month_money = parseFloat(resp.result) };
                        _this.calcTotal();
                        //_this.getDetail();

                    }
                }
            });
        },//配置
        calcTotal: function () {//计算总金额
            var _this = this;

            _this.model.total = 0;
            if (_this.model.set_top_money > 0) {
                _this.model.total += parseFloat(_this.model.set_top_money);
            }
            if (_this.model.validity_unit == "天") {
                if (_this.model.validity_num > 0) {
                    _this.model.total += _this.validity_unit_day_money * parseFloat(_this.model.validity_num);

                }
            } else if (_this.model.validity_unit == "月") {
                if (_this.model.validity_num > 0) {
                    _this.model.total += _this.validity_unit_month_money * parseFloat(_this.model.validity_num);

                }
            }
            if (_this.model.reward_money > 0) {
                _this.model.total += parseFloat(_this.model.reward_money);
            }
            console.log(["model", _this.model]);


        },
        topCate: function () {//分类置顶点击
            var _this = this;
            _this.top_cate_select = !_this.top_cate_select;
            if (_this.top_cate_select) {
                _this.top_all_select = false;
                _this.model.set_top_money = _this.top_cate_money;
                _this.model.set_top = "1";

            } else {
                _this.model.set_top_money = 0;
                _this.model.set_top = "";
            }
        },
        topAll: function () {//全站置顶点击
            var _this = this;
            _this.top_all_select = !_this.top_all_select;
            if (_this.top_all_select) {
                _this.top_cate_select = false;
                _this.model.set_top_money = _this.top_all_money;
                _this.model.set_top = "2";
            } else {
                _this.model.set_top_money = 0;
                _this.model.set_top = "";
            }
        },
        rewardClick: function () {//打赏福利点击
            var _this = this;
            _this.reward_select = !_this.reward_select;
            if (_this.reward_select) {

            } else {
                _this.model.reward_money = 0;

            }
        },
        checkInput: function () {//检查输入
            //return true;
            var _this = this;
            if (_this.model.validity_num == "" || _this.model.validity_num <= 0) {
                alert("请输入有效期");
                return false;

            }
            if (!_this.model.use_type) {
                alert("请选择品牌");
                return false;

            }
            if (_this.carStyleSelect.length == 0) {
                alert("请选择车型");
                return false;

            }

            if (_this.model.goods_weight == "") {
                alert("请输入马力");
                return false;

            }
            if (_this.model.recruit_num == "") {
                alert("请输入车辆使用年限");
                return false;

            }
            if (_this.model.start_city == "") {
                alert("请选择车辆所在地");
                return false;

            }

            if (_this.model.contact_name == "") {
                alert("请输入联系人");
                return false;

            }
            if (_this.model.contact_phone == "") {
                alert("请输入联系电话");
                return false;
            }
            return true;

        },
        submit: function () {//提交
            var _this = this;
            if (!_this.checkInput()) {
                return false;
            }
            _this.model.car_style = _this.carStyleSelect.join(',');
            //confirm("提示", "确定发布", "发布", "取消", function () {
                $.ajax({
                    type: 'post',
                    url: '/Project/Edit',
                    data: _this.model,
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.status) {
                            window.location.href = "/User/Pay/" + _this.model.order_no;
                        } else {
                            alert(resp.msg);
                        }
                    }
                });

            //}, function () {

            //    layer.closeAll();

            //})



        },
        useTypeClick: function (item) {//品牌选择
            if (this.useTypeSelectData.indexOf(item.title) >= 0) {
                // 删除
                for (var i = 0; i < this.useTypeSelectData.length; i++) {
                    if (this.useTypeSelectData[i] == item.title) {
                        this.useTypeSelectData.splice(i, 1);
                    }
                }
            } else {
                //if (this.carLenSelect.length >= 5) {
                //    alert("最多选择5个标签");
                //    return false;
                //}
                this.useTypeSelectData.push(item.title);
            }

        },
        carStyleClick: function (item) {//车型选择
            if (this.carStyleSelect.indexOf(item.title) >= 0) {
                // 删除
                for (var i = 0; i < this.carStyleSelect.length; i++) {
                    if (this.carStyleSelect[i] == item.title) {
                        this.carStyleSelect.splice(i, 1);
                    }
                }
            } else {
                //if (this.carLenSelect.length >= 5) {
                //    alert("最多选择5个标签");
                //    return false;
                //}
                this.carStyleSelect.push(item.title);
            }



        },
        getDetail: function () {
            var _this = this;
            $.ajax({
                type: 'post',
                url: '/Project/Detail',
                data: { id: id },
                dataType: 'json',
                success: function (resp) {
                    if (resp.status) {
                        _this.model = resp.result;
                        _this.carStyleSelect = _this.model.car_style.split(',');
                        if (_this.model.set_top == 1) {
                            _this.topCate();
                        }
                        if (_this.model.set_top == 2) {
                            _this.topAll();
                        }
                        if (_this.model.reward_money > 0) {
                            $(".pg_sec10 dl dd").addClass("active");
                            $(".pg_sec10 dl dd .pt4").show();
                        }



                    }
                }
            });

        }

    }
});
vue.init();