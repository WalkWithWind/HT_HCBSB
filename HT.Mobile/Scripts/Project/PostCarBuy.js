
// 发布
var vue = new Vue({
    el: '.main',
    data: {
        model: {
            cateid: 6,//类型 6车辆求购
            cate: "车辆出售",//车辆求购
            validity_num: "",//有效期
            validity_unit: "天",//有效期单位 天,月
            start_province: "",//工作地区省份
            start_city: '', //工作地区城市
            stop_province: '',//籍贯省份
            stop_city: '',//籍贯
            use_type: '',//品牌
            recruit_num:"",//车龄
            car_length: '', //排放标准
            car_style: '',//车型
            use_mode: "个人户",//车辆户型
            good_type: "能",//能否提档
            goods_weight: "",//马力
            //goods_weight_unit: "吨",//重量体积类型
            freight: "",//出售价格
            use_time: "",//行驶证登记时间
            imgs: "",//图片上传
            //pay_method: "",//选择的付款方式
            other_remark: "",//其它补充
            contact_name: "",//联系人
            contact_phone: "",//联系电话
            set_top: "1",//置顶类型 1分类2 全站
            set_top_money: 0,//置顶金额
            reward_money: 0,//打赏金额
            total: 0//需支付金额
        },
        useTypeData: [],//品牌列表
        carLengthData: [],//排放标准
        carStyleData: [],//驾驶类型列表
        imgsData: [],//上传图片
        //goodsTypeData: [],//驾驶路线列表
        //useModeData: [],//装卸方式列表
        //payTypeData: [],//付款方式列表
        rewardMoneyData: [],//打赏金额列表
        top_cate_select: false,//是否选中分类置顶
        top_all_select: false,//是否选中全站置顶
        top_cate_money: 0,//分类置顶金额
        top_all_money: 0,//全站置顶金额
        top_type: 0,//置顶类型 1分类 2全站 0不置顶
        validity_unit_day_money: 0,// 发布费用 元/天
        validity_unit_month_money: 0,// 发布费用 元/月
        select: {
            startProvinceTab: true,
            stopProvinceTab: true
        },
        cityData: dsy
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
    methods: {
        init: function () {

            this.loadCateData('use_type', 106);//品牌
            this.loadCateData('car_length', 111);//排放标准
            this.loadCateData('car_style', 16);//车型
            //this.loadCateData('good_type', 78);//驾驶路线
            //         this.loadCateData('use_mode', 40);//装卸方式列表
            //this.loadCateData('pay_type', 47);//付款方式列表
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
                        if (code == 'car_length') { _this.carLengthData = resp.result };
                        if (code == 'car_style') { _this.carStyleData = resp.result };
                        //if (code == 'good_type') { _this.goodsTypeData = resp.result };
                        //                  if (code == 'use_mode') { _this.useModeData = resp.result };
                        //if (code == 'pay_type') { _this.payTypeData = resp.result };
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
                        if (configName == 'top_cate_money') { _this.top_cate_money = parseFloat(resp.result) };
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
        checkInput: function () {//检查输入
            //return true;
            var _this = this;
            if (_this.model.validity_num == "" || _this.model.validity_num <= 0) {
                alert("请输入有效期");
                return false;

            }
            if (_this.model.use_type == "") {
                alert("请选择品牌");
                return false;

            }
            if (_this.model.car_style == "") {
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
            //if (_this.model.use_time == "") {
            //    alert("请选择行驶证登记时间");
            //    return false;

            //}
            //if (_this.model.car_length == "") {
            //    alert("请选择排放标准");
            //    return false;

            //}
            //if (_this.imgsData.length ==0) {
            //    alert("请上传车辆图片");
            //    return false;

            //}

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
            confirm("提示", "确定发布", "发布", "取消", function () {
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

            }, function () {

                layer.closeAll();

            })



        },
        showCity: function (code) {
            var _title = code == 'start' ? '车辆所在地' : '';
            layer.open({
                type: 1,
                title: _title,
                content: $('.' + code + '_box'),
                offset: 'lb',
                area: ['100%', '500px'],
                shade: 0.5,
                scrollbar: false,
                anim: 2
            });
        },
        selectProvince: function (code, item) {
            var _this = this;
            if (code == 'start') {
                if (_this.model.start_province != item) _this.model.start_city = '';
                _this.model.start_province = item;
                _this.select.startProvinceTab = false;

            } else {
                if (_this.model.stop_province != item) _this.model.stop_city = '';
                _this.model.stop_province = item;
                _this.select.stopProvinceTab = false;

            }
        },
        selectCity: function (code, item) {
            var _this = this;
            if (code == 'start') {
                _this.model.start_city = item;
            } else {
                _this.model.stop_city = item;
            }
            layer.closeAll();
        },
        selectTabProvince: function (code) {
            var _this = this;
            if (code == 'start') {
                _this.select.startProvinceTab = true;
            } else {
                _this.select.stopProvinceTab = true;
            }
        },
        selectTabCity: function (code) {
            var _this = this;
            if (code == 'start') {
                if (_this.model.start_province == '') return;
                _this.select.startProvinceTab = false;
            } else {
                if (_this.model.stop_province == '') return;
                _this.select.stopProvinceTab = false;
            }
        }



    }
});
vue.init();