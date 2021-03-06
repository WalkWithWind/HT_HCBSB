﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="HT.Admin.admin.user.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<title>用户管理</title>
	<link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
	<link href="/admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/pagination.css" rel="stylesheet" type="text/css" />
	<link href="/scripts/datepicker/skin/whyGreen/datepicker.css" rel="stylesheet" />
</head>
<body class="mainbody">
    <div class="maindiv" v-bind:class="['show']">
		<!--导航栏-->
		<div class="location">
			<a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
			<a href="/admin/center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<span>用户管理</span>
		</div>
		<!--/导航栏-->

		<!--工具栏-->
		<div class="toolbar-wrap">
			<div id="floatHead" class="toolbar">
				<div class="box-wrap">
					<div class="l-list">
						<ul class="icon-list">
							<li><a class="all" v-on:click="selectAllChange()" ><i></i><span>{{selectAllText}}</span></a></li>
                            
                            <%--<li><a class="del" v-on:click="edit()" ><i></i><span>编辑</span></a></li>--%>
                            <li><a class="del" v-on:click="disable(1)" ><i></i><span>禁用</span></a></li>
                            <li><a class="del" v-on:click="disable(0)" ><i></i><span>启用</span></a></li>
                            <li><a class="del" v-on:click="del()" ><i></i><span>删除</span></a></li>
						</ul>
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
				<th align="center" width="15%">头像</th>
				<th align="center" width="15%">昵称</th>
				<th align="center" width="15%">手机</th>
				<th align="center" width="10%">余额</th>
				<th align="center" width="15%">加入时间</th>
				<th align="center" width="10%">是否关注</th>
				<th align="center" width="10%">是否禁用</th>
				<th align="center" width="5%">操作</th>
			</tr>

			<tr v-for="item in dataList">
				<td align="center">
					<span class="checkall" style="vertical-align: middle;">
						<input id="rptList_chkId_0" type="checkbox" v-model="item.checked" /></span>
				</td>
				<td align="center"><img v-bind:src="item.avatar" width="50" /></td>
				<td align="center" v-text="item.nickname"></td>
				<td align="center" v-text="item.mobile"></td>
				<td align="center">{{item.money+'元'}}</td>
				<td align="center">{{item.addtime|date}}</td>
				<td align="center">
                    <span v-show="item.issubscribe==0" style="color:red;">未关注</span>
                    <span v-show="item.issubscribe==1">已关注</span>
				</td>
				<td align="center">
                    <span v-show="item.isdisable==0" style="color:green;">正常</span>
                    <span v-show="item.isdisable==1" style="color:red;">禁用</span>
				</td>
                <td>
                    <a href="javascript:;" v-on:click="edit(item)">编辑</a>
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


        <%--弹出层--%>

        <div class="layerMove" v-show="showLayer">
          <div class="layerBody">
            <div class="layerTitle">编辑用户信息</div>
            <div class="layerContent">
                  <table>
                      <tr style="height:40px;line-height:40px;">
                          <td style="width:30%">昵称</td>
                          <td style="width:70%">
                              {{userinfo.nickname}}
                          </td>
                      </tr>
                      <tr style="height:40px;line-height:40px;">
                          <td style="width:30%">手机</td>
                          <td style="width:70%">
                              <input type="text" v-model="tempinfo.mobile"/>
                          </td>
                      </tr>
                      <tr style="height:40px;line-height:40px;">
                          <td style="width:30%">余额</td>
                          <td style="width:70%">
                              <input type="number" v-model="tempinfo.money"/>
                          </td>
                      </tr>
                  </table> 
<%--                <div class="productItem">
                </div>--%>
            </div>
            <div class="layerBottom">
              <a class="btn  btn-primary" v-on:click="cencaledit()">取消</a>
              <a class="btn  btn-primary" v-on:click="confirmedit()">确定</a>
            </div>
        
          </div>
      </div>

    </div>
</body>
     <script type="text/javascript" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/scripts/vue/vue.min.js"></script>
	<script type="text/javascript" src="/scripts/artdialog/dialog-plus-min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
	<script type="text/javascript" src="/scripts/laypage/1.2/laypage.js?v=1012"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/common.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/vueFilter.js"></script>
	<script src="/scripts/datepicker/WdatePicker.js"></script>
	<script type="text/javascript">

        var url = "/admin/api/user/list.ashx";
        function onSelectVal(obj) {
            commVm._data.status = $(obj).val();
            commVm.selectVal();
        }
        var commVm = new Vue({
            el: '.maindiv',
            data: {
                showLayer:false,
                selectAll: false,
                selectAllText: "全选",
                dataList: [],
                total: 0,
                pageindex: 1,
                pagesize: 10,
                keyword: "",
                totalPage: 0,
                userinfo: {},
                tempinfo: {}
            },
            mounted: function () {
                this.init();
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
                        keyword: _this.keyword
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


                edit: function (item) {
                    var _this = this;
                    _this.tempinfo = {
                        mobile: item.mobile,
                        money: item.money
                    };
                    _this.userinfo = item;
                    console.log(' _this.userinfo', _this.userinfo);
                    _this.showLayer = true;
                },

                confirmedit: function () {
                    var _this = this;
                    var reqData = {
                        id: _this.userinfo.id,
                        mobile: _this.tempinfo.mobile,
                        money: _this.tempinfo.money
                    };
                    $.ajax({
                        type: 'post',
                        url: '/admin/api/user/update.ashx',
                        data: reqData,
                        dataType: 'json',
                        success: function (resp) {
                            _this.showLayer = false;
                            if (resp.status) {
                                _this.showMsg("编辑成功");
                                _this.userinfo.mobile = _this.tempinfo.mobile;
                                _this.userinfo.money = _this.tempinfo.money;
                            }
                            else {
                                _this.showMsg(resp.msg);
                            }
                        }
                    });
                },

                cencaledit: function () {
                    this.showLayer = false;
                },

                del: function () {
                    var _this = this;
                    var ids = _this.getSelectIds();
                    if (!ids) {
                        this.showMsg("请选择需要删除的记录");
                        return false;
                    }
                    parent.dialog({
                        title: '提示',
                        content: "确认删除",
                        okValue: '确定',
                        ok: function () {
                            var reqData = {
                                ids: ids
                            };
                            $.ajax({
                                type: 'post',
                                url: '/admin/api/user/delete.ashx',
                                data: reqData,
                                dataType: 'json',
                                success: function (resp) {
                                    if (resp.status) {
                                        _this.showMsg("删除成功");
                                        //_this.pageindex = 1;
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
                

                disable: function (value) {

                    var _this = this;

                    var ids = _this.getSelectIds();


                    var text = "禁用";

                    if (value == 0) text = "启用";

                    if (!ids) {
                        this.showMsg("请选择需要" + text + "的记录");
                        return false;
                    }

                    parent.dialog({
                        title: '提示',
                        content: "确认" + text + "?",
                        okValue: '确定',
                        ok: function () {
                            var reqData = {
                                ids: ids,
                                disable: value
                            };
                            $.ajax({
                                type: 'post',
                                url: '/admin/api/user/disable.ashx',
                                data: reqData,
                                dataType: 'json',
                                success: function (resp) {
                                    if (resp.status) {
                                        _this.showMsg(text + "成功");
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
                }
            }
        });
</script>
</html>
