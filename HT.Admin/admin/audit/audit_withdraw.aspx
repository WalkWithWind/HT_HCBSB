<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="audit_withdraw.aspx.cs" Inherits="HT.Admin.admin.audit.audit_withdraw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<title>提现审核</title>
	<link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
	<link href="/admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/pagination.css" rel="stylesheet" type="text/css" />
	<link href="/scripts/datepicker/skin/whyGreen/datepicker.css" rel="stylesheet" />
</head>
<body class="maindiv">

</body>
    
    <script type="text/javascript" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/scripts/vue/vue.min.js"></script>
	<script type="text/javascript" src="/scripts/artdialog/dialog-plus-min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
	<script type="text/javascript" src="/scripts/laypage/1.2/laypage.js?v=1012"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/common.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/vueFilter.js"></script>


    <script type="text/javascript">

        var url = "/admin/api/audit/withdraw/list.ashx";

        var commVm = new Vue({
            el: '.maindiv',
            data: {
                selectAll: false,
                selectAllText: "全选",
                dataList: [],
                total: 0,
                pageindex: 1,
                pagesize: 10,
                keyword: "",
                status: "",
                fromDate: "",
                toDate: "",
                cateId: GetParm('id'),
                totalPage: 0
            },
            methods: {
                init: function () {
                    var _this = this;
                    _this.loadData();
                },

                loadData: function () {
                    var _this = this;
                    var reqData = {
                        pageindex: _this.pageindex,
                        pagesize: _this.pagesize,
                        keyword: _this.keyword,
                        cate_id: _this.cateId,
                        status: _this.status,
                        fromdate: $("#txtFromDate").val(),
                        todate: $("#txtToDate").val()
                    };
                    //console.log('reqData', reqData);
                    $.ajax({
                        type: 'post',
                        url: url,
                        data: reqData,
                        dataType: 'json',
                        success: function (resp) {
                            if (resp.status) {
                                _this.total = resp.result.total;
                                _this.dataList = resp.result.list
                                _this.totalPage = resp.result.totalpage;
                                _this.selectAll = false,
                                    _this.selectAllText = "全选";
                                laypage({
                                    cont: document.getElementById('pageDiv'), //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                                    pages: _this.totalPage, //通过后台拿到的总页数
                                    curr: _this.pageindex, //当前页
                                    layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'],
                                    jump: function (obj, first) { //触发分页后的回调
                                        if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                            _this.pageindex = obj.curr;
                                            _this.loadData();
                                        }
                                    },
                                    first: '首页', //若不显示，设置false即可
                                    last: '尾页', //若不显示，设置false即可
                                    prev: '<', //若不显示，设置false即可
                                    next: '>' //若不显示，设置false即可
                                });



                            }
                            else {

                            }
                        }
                    });
                },

                changePage: function () {
                    var _this = this;
                    _this.loadData();
                },
                search: function () {
                    var _this = this;
                    _this.pageindex = 1;
                    _this.loadData();
                },
                selectAllChange: function () {
                    this.selectAll = !this.selectAll;
                    if (this.selectAll) {
                        //全选
                        this.selectAllText = "取消";

                    } else {
                        //全不选
                        this.selectAllText = "全选";
                    }
                    for (var i = 0; i < this.dataList.length; i++) {

                        this.dataList[i].checked = this.selectAll;
                    }

                },
                selectVal: function () {
                    var _this = this;
                    _this.loadData();
                },
                del: function () {
                    var _this = this;
                    if (_this.getSelectIds() == "") {
                        this.showMsg("请选择需要删除的记录");
                        return false;
                    }

                    parent.dialog({
                        title: '提示',
                        content: "确认删除?",
                        okValue: '确定',
                        ok: function () {

                            $.ajax({
                                type: 'post',
                                url: delUrl,
                                data: { ids: _this.getSelectIds() },
                                dataType: 'json',
                                success: function (resp) {
                                    if (resp.status) {
                                        _this.showMsg("删除成功");
                                        _this.pageindex = 1;
                                        _this.loadData();

                                    }
                                    else {
                                        _this.showMsg(resp.msg);
                                    }
                                }
                            });

                        },
                        cancelValue: '取消',
                        cancel: function () { }
                    }).showModal();



                },
                showMsg: function (msg) {

                    parent.dialog({
                        title: '提示',
                        content: msg,
                        okValue: '确定',
                        ok: function () { }
                    }).showModal();

                },
                getSelectIds: function () {
                    var arry = [];
                    for (var i = 0; i < this.dataList.length; i++) {
                        if (this.dataList[i].checked) {
                            arry.push(this.dataList[i].id);
                        }
                    }
                    return arry.join(',');
                },
                updateStatus: function () {
                    var _this = this;

                    var ids = _this.getSelectIds();

                    if (!ids) {
                        this.showMsg("请选择需要审核的记录");
                        return false;
                    }

                    var html = "<div class=\"menu-list\"><div class=\"rule-single-select\" style=\"text-align:center;\"><select style=\"padding: 5px; \" class=\"ddstatus\" id=\"ddstatus\"><option value=\"1\">审核通过</option><option value=\"2\">审核不通过</option></select></div></div >";

                    parent.dialog({
                        title: '提示',
                        content: html,
                        width: 200,
                        height: 150,
                        okValue: '确定',
                        ok: function () {
                            var ddsttaus = $(this.node).find('.ddstatus').val()
                            $.ajax({
                                type: 'post',
                                url: '/admin/api/project/updatestatus.ashx',
                                data: { ids: ids, status: ddsttaus },
                                dataType: 'json',
                                success: function (resp) {
                                    if (resp.status) {
                                        _this.showMsg("操作成功");
                                    }
                                    else {
                                        _this.showMsg(resp.msg);
                                    }
                                    _this.loadData();
                                }
                            });
                        },
                        cancelValue: '取消',
                        cancel: function () {

                        }
                    }).showModal();
                }

            }
        });

        $(function () {
            commVm.init();
        });

     </script>
</html>
