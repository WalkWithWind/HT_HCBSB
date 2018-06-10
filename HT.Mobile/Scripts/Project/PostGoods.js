
// 发布
var vue = new Vue({
    el: '.main',
    data: {
        model: {
            cateid: 1,//类型 1货源
            cate: "有货找车",//货源
            validity_num: "",//有效期
            validity_unit: "天",//有效期单位 天,月
            start_province: "",//出发地省份
            start_city: '', //出发地城市
            start_district: '', //出发地区域
            stop_province: '',//目的地省份
            stop_city: '',//目的地城市
            stop_district: '',//目的地区域
            use_type: '',//选择的用车类型
            car_length: '', //选择的车长
            car_style: '',//选择的车型
            goods_type: "",//选择的货物类型
            goods_weight: "",//货物重量体积类
            goods_weight_unit: "吨",//重量体积类型
            freight: "",//运费金额
            use_time: "",//装车时间
            use_mode: "",//选择的装卸方式
            pay_method: "全现金",//选择的付款方式
            other_remark: "",//其它补充
            contact_name: "",//联系人
            contact_phone: "",//联系电话
            set_top: 0,//置顶类型  空不置顶 1分类 2全站
            set_top_money: 0,//置顶金额
            reward_money: 0,//打赏金额
            total: 0//需支付金额
        },
        carLenSelect: [],//选中的车长
        carStyleSelect: [],//选中的车型
        useTypeData: [],//用车类型列表
        carLengthData: [],//车长列表
        carStyleData: [],//车型列表
        goodsTypeData: [],//货物类型列表
        useModeData: [],//装卸方式列表
        payTypeData: [],//付款方式列表
        rewardMoneyData: [],//打赏金额列表
        top_cate_select: false,//是否选中分类置顶
        top_all_select: false,//是否选中全站置顶
        reward_select: false,//是否选中赏福利
        top_cate_money: 0,//分类置顶金额
        top_all_money: 0,//全站置顶金额
        validity_unit_day_money: 0,// 发布费用 元/天
        validity_unit_month_money: 0,// 发布费用 元/月
        select: {
            showCityStart: false,
            showCityStop: false
        }
    },
    watch: {
        'model.validity_num': function (val, oldval) {
            this.calcTotal();
        },
        //'model.freight': function (val, oldval) {
        //		this.calcTotal();
        //	},
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
    created: function() {
        this.init();
    },
    methods: {
        init: function () {
            this.loadCateData('use_type', 1);//用车类型
            this.loadCateData('car_length', 4);//车长
            this.loadCateData('car_style', 16);//车型列表
            this.loadCateData('goods_type', 27);//货物类型列表
            this.loadCateData('use_mode', 40);//装卸方式列表
            this.loadCateData('pay_type', 47);//付款方式列表
            this.loadCateData('reward_money', 55);//打赏福利列表

            this.loadConfigData('top_cate_money');//分类置顶金额
            this.loadConfigData('top_all_money');//全站置顶金额
            this.loadConfigData('pub_amount_day');//发布费用 元/天
            this.loadConfigData('pub_amount_month');//发布费用 元/月

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
                        if (code == 'car_length') { _this.carLengthData = resp.result; };
                        if (code == 'car_style') { _this.carStyleData = resp.result };
                        if (code == 'goods_type') { _this.goodsTypeData = resp.result };
                        if (code == 'use_mode') { _this.useModeData = resp.result };
                        if (code == 'pay_type') { _this.payTypeData = resp.result };
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
                        if (configName == 'top_cate_money') { _this.top_cate_money = parseFloat(resp.result);_this.topCate(); };                          
                        if (configName == 'top_all_money') { _this.top_all_money = parseFloat(resp.result) };
                        if (configName == 'pub_amount_day') { _this.validity_unit_day_money = parseFloat(resp.result) };
                        if (configName == 'pub_amount_month') { _this.validity_unit_month_money = parseFloat(resp.result) };


                    }
                }
            });
        },//配置
        calcTotal: function () {//计算总金额
            var _this = this;

            _this.model.total = 0;
            //if (_this.model.freight > 0) {
            //	_this.model.total += parseFloat(_this.model.freight);
            //}
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
            //console.log(["model", _this.model]);
        },
        topCate: function () {//分类置顶点击
            var _this = this;
            _this.top_cate_select = !_this.top_cate_select;
            if (_this.top_cate_select) {
                _this.top_all_select = false;
                _this.model.set_top_money = _this.top_cate_money;
                _this.model.set_top = 1;

            } else {
                _this.model.set_top_money = 0;
                _this.model.set_top = 0;
            }
        },
        topAll: function () {//全站置顶点击
            var _this = this;
            _this.top_all_select = !_this.top_all_select;
            if (_this.top_all_select) {
                _this.top_cate_select = false;
                _this.model.set_top_money = _this.top_all_money;
                _this.model.set_top = 2;
            } else {
                _this.model.set_top_money = 0;
                _this.model.set_top = 0;
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
            if (_this.model.start_city == "") {
                alert("请选择出发城市");
                return false;

            }
            if (_this.model.stop_city == "") {
                alert("请选择到达城市");
                return false;

            }
            if (_this.model.goods_weight == "") {
                alert("请输入货物重量体积");
                return false;

            }
            if (_this.model.freight == "") {
                alert("请输入运费");
                return false;

            }
            if (_this.model.use_time == "") {
                alert("请输入装车时间");
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
            _this.model.car_length = _this.carLenSelect.join(',');
            _this.model.car_style = _this.carStyleSelect.join(',');
            //confirm("提示", "确定发布", "发布", "取消", function () {
                $.ajax({
                    type: 'post',
                    url: '/Project/PostSubmit',
                    data: _this.model,
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.status) {
                            window.location.href = "/User/Pay/" + resp.result.order_no;
                        } else {
                            alert(resp.msg);
                        }
                    }
                });

            //}, function () {
            //    layer.closeAll();
            //})
        },
        carLengthClick: function (item) {//车长选择
            if (this.carLenSelect.indexOf(item.title) >= 0) {
                // 删除
                for (var i = 0; i < this.carLenSelect.length; i++) {
                    if (this.carLenSelect[i] == item.title) {
                        this.carLenSelect.splice(i, 1);
                    }
                }
            } else {
                if (this.carLenSelect.length >= 3) {
                    alert("最多选择3个车长");
                    return false;
                }
                this.carLenSelect.push(item.title);
            }
            console.log(["this.carLenSelect", this.carLenSelect]);



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
                if (this.carStyleSelect.length >= 3) {
                    alert("最多选择3个车型");
                    return false;
                }
                this.carStyleSelect.push(item.title);
            }
        }
    }
});