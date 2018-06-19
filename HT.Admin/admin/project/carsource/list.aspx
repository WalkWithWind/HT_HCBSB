<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="HT.Admin.admin.project.carsource.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<title>车源管理</title>
	<link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
	<link href="/admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/pagination.css" rel="stylesheet" type="text/css" />
	<link href="/scripts/datepicker/skin/whyGreen/datepicker.css" rel="stylesheet" />
</head>
<body class="mainbody">
    <div class="maindiv">
		<!--导航栏-->
		<div class="location">
			<a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
			<a href="/admin/center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<span>车源管理</span>
		</div>
		<!--/导航栏-->

		<!--工具栏-->
		<div class="toolbar-wrap">
			<div id="floatHead" class="toolbar">
				<div class="box-wrap">

					<div class="l-list">

						<ul class="icon-list">
							<li><a class="all" v-on:click="selectAllChange()" ><i></i><span>{{selectAllText}}</span></a></li>
                            <% if (ChkAdminLevelBool("carsource_list", HT.Utility.HTEnums.ActionEnum.Delete.ToString()))
                                { %>
							<li><a class="del" v-on:click="del()" ><i></i><span>删除</span></a></li>
                            <%} %>
                            <% if (ChkAdminLevelBool("carsource_list", HT.Utility.HTEnums.ActionEnum.Audit.ToString()))
                                { %>
							<li><a class="del" v-on:click="updateStatus()" ><i></i><span>审核</span></a></li>
                            <%} %>
							
						</ul>

                        <div class="menu-list">
                            <div class="rule-single-select">
                                 <select onchange="onSelectVal(this)">
                                     <option value="">请选择审核状态</option>
                                     <option value="-1">待支付</option>
                                     <option value="0">待审核</option>
                                     <option value="1">审核通过</option>
                                     <option value="2">审核不通过</option>
                                     <option value="3">已过期</option>
                                </select>						
                            </div>
							<input id="txtFromDate" class="input-date" onclick="WdatePicker()"  @keyup.enter="search"/><span class="float-right">&nbsp;至&nbsp;</span><input id="txtToDate" @keyup.enter="search" class="input-date" onclick="WdatePicker()"  />
                        </div>

						   
					</div>
					<div class="r-list">
						<input name="txtKeywords" type="text" v-model="keyword" v-on:keyup.13="search()" id="txtKeywords" class="keyword" />
						<a id="lbtnSearch" class="btn-search" v-on:click="search()">查询</a>
					</div>
				</div>
			</div>
		</div>
		<!--/工具栏-->



		<!--文字列表-->
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">

			<tr>
				<th align="center" width="5%">选择</th>
				<th align="center" width="8%">联系人</th>
				<th align="center" width="8%">联系电话</th>
				<th align="center" width="10%">发布时间</th>
				<th align="center" width="10%">置顶金额</th>
				<th align="center" width="10%">打赏金额</th>
				<th align="center" width="15%">出发地</th>
				<th align="center" width="15%">目的地</th>
				<th align="center" width="8%">状态</th>
				
                <% if (ChkAdminLevelBool("review_list", HT.Utility.HTEnums.ActionEnum.View.ToString()))
                    { %>
				<th align="center" width="6%">留言</th>
                <%} %>
				<th align="center">操作</th>
			</tr>

			<tr v-for="item in dataList">

				<td align="center">
					<span class="checkall" style="vertical-align: middle;">
						<input id="rptList_chkId_0" type="checkbox" v-model="item.checked" /></span>
				</td>

				<%--<td align="center" v-text="item.title"></td>--%>
                
				<td align="center">{{item.contact_name}}</td>
				<td align="center">{{item.contact_phone}}</td>
				<td align="center">{{item.add_time|date}}</td>
				<td align="center">
                    {{item.set_top_money+'元'}}
                    <span v-show="item.set_top==0">没有置顶</span>
                    <span v-show="item.set_top==1">（分类置顶）</span>
                    <span v-show="item.set_top==2">（全站置顶）</span>
				</td>
				<td align="center">
                    {{item.reward_money+'元'}}
				</td>
				<td align="center">
                    {{item.start_city|cityFormart(item.start_district,item.start_province,'出发地')}}
				</td>

				<td align="center">
                    {{item.stop_city|cityFormart(item.stop_district,item.stop_province,'到达地')}}
				</td>
				<td align="center">
                    <span v-if="item.status==0 && item.pay_status==0">待支付</span>
                    <span v-if="item.status==0 && item.pay_status==1">待审核</span>
                    <span v-if="item.status==1">审核通过</span> 
                    <span v-if="item.status==2">审核不通过</span>
                    <span v-if="item.status==3" style="color:#d8d4d4">已过期</span>
				</td>
                <% if (ChkAdminLevelBool("review_list", HT.Utility.HTEnums.ActionEnum.View.ToString()))
                    { %>
				<td align="center">
					<a v-if="item.review_s0" style="color:red;" v-bind:href="'/admin/review/list.aspx?type=comment&nid='+ item.id">留言({{item.review_s0}})</a>
					<a v-if="!item.review_s0" v-bind:href="'/admin/review/list.aspx?type=comment&nid='+ item.id">留言</a>
				</td>
                <%} %>
				<td align="center">
					<a v-bind:href="'detail.aspx?id='+ item.id">详情</a>
				</td>
			</tr>


			<tr v-if="dataList.length==0">
				<td align="center" colspan="11">暂无记录</td>
			</tr>


		</table>
		<!--/文字列表-->

		<!--内容底部-->
		<div class="line20"></div>
		<div class="pagelist">
			<div class="l-btns">
				<span>显示</span><input name="txtPageNum" type="text" v-model="pagesize" v-on:change="changePage()" onkeypress="if (WebForm_TextBoxKeyHandler(event) == false) return false;" id="txtPageNum" class="pagenum" onkeydown="return checkNumber(event);" /><span>条/页</span>
			</div>
			<div class="default"><span>共{{total}}记录</span><span style=" padding: 0px;border: 0px;margin:0px;" id="pageDiv"></span></div>
			
		</div>
		<!--/内容底部-->
    </div>
</body>

    <script type="text/javascript" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/scripts/vue/vue.min.js"></script>
	<script type="text/javascript" src="/scripts/artdialog/dialog-plus-min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
	<script src="/scripts/laypage/1.2/laypage.js"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/common.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/vueFilter.js"></script>
	<script src="/scripts/datepicker/WdatePicker.js"></script>


    <script type="text/javascript">

        var url = "/admin/api/project/list.ashx";
        var delUrl = "/admin/api/project/delete.ashx";
        function onSelectVal(obj) {
            commVm._data.status = $(obj).val();
            commVm.selectVal();
        }



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
                    //console.log('this.status', this.status);
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

            //
        });

</script>



</html>
