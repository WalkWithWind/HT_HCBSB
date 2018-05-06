<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="HT.Admin.admin.project.goods.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>广告管理</title>
    <link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="/admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/laypage1.2/skin/laypage.css" rel="stylesheet" type="text/css" />
</head>

<body class="mainbody">


        <div class="maindiv">

            <!--导航栏-->
            <div class="location">
                <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
                <a href="/admin/center.aspx" class="home"><i></i><span>首页</span></a>
                <i class="arrow"></i>
                <span>货源管理</span>
            </div>
            <!--/导航栏-->

            <!--工具栏-->
             <div class="toolbar-wrap">
                <div id="floatHead" class="toolbar">
                    <div class="box-wrap">
                        <div class="l-list">
                            <ul class="icon-list">
                                <li><a class="edit" href="ht_ads_edit.aspx?action=Add"><i></i><span>新增</span></a></li>
                                <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                                <li>
                                    <a onclick="return ExePostBack(&#39;btnDelete&#39;);" id="btnDelete" class="del" href="javascript:__doPostBack(&#39;btnDelete&#39;,&#39;&#39;)"><i></i><span>删除</span></a></li>
                            </ul>
                        <%--    <div class="menu-list ">
                                <div class="rule-single-select">
                                    <select name="ddlPlaceCode" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ddlPlaceCode\&#39;,\&#39;\&#39;)&#39;, 0)" id="ddlPlaceCode">
	                                    <option selected="selected" value="">请选择广告位...</option>
	                                    <option value="pc_index_banner">PC-首页banner</option>
                                    </select>
                                </div>
                            </div>--%>
                        </div>
                        <div class="r-list">
                            <input name="txtKeywords" type="text" v-model="keyword" id="txtKeywords" class="keyword" />
                            <a id="lbtnSearch" class="btn-search" v-on:click="Search()">查询</a>
                        </div>
                    </div>
                </div>
            </div>
            <!--/工具栏-->



            <!--文字列表-->
             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">

                    <tr>
                            <th align="center" width="3%">选择</th>
                            <th align="center" width="12%">标题</th>
                            <th align="center" width="10%">发布时间</th>
                            <th align="center" width="10%">出发地</th>
                            <th align="center" width="10%">目的地</th>
                            <th align="center" width="10%">装车时间</th>
                            <th align="center" width="10%">运费金额</th>
                            <th align="center" width="10%">联系人</th>
                            <th align="center" width="10%">联系电话</th>
                            <th align="center" width="10%">状态</th>
                            <th align="center" width="5%">操作</th>
                    </tr>

                    <tr v-for="item in dataList">

                        <td align="center">
                            <span class="checkall" style="vertical-align: middle;"><input id="rptList_chkId_0" type="checkbox" name="rptList$ctl01$chkId" /></span>
                            <input type="hidden" name="rptList$ctl01$hidId" id="rptList_hidId_0" value="3" />
                        </td>

                        <td align="center" v-text="item.title"></td>
                        <td align="center">{{item.add_time}}</td>
                        <td align="center">
                            {{item.start_province}}-
                            {{item.start_city}}-
                            {{item.start_district}}
                        </td>

                        <td align="center">
                            {{item.stop_province}}-
                            {{item.stop_city}}-
                            {{item.stop_district}}
                        </td>
                        <td align="center">{{item.use_time}}</td>
                        <td align="center">{{item.freight}}</td>
                        <td align="center">{{item.contact_name}}</td>
                        <td align="center">{{item.contact_phone}}</td>
                        <td align="center">{{item.status}}</td>

                        <td align="center">
                            <a :href="'detail.aspx?id=' + item.id " >详情</a>
                        </td>
                    </tr>


                    <tr v-if="dataList.length==0">
                        <td align=\"center\" colspan=11">暂无记录</td>
                    </tr>

                
                </table>            <!--/文字列表-->

            <!--内容底部-->
              <div class="line20"></div>

              <div id="pagelist">
                            </div>            <!--/内容底部-->        </div>
</body>
</html>


<script type="text/javascript" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="/scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="/admin/js/common.js"></script>
<script type="text/javascript"  charset="utf-8" src="/scripts/vue/vue.min.js"></script>
<script type="text/javascript"  charset="utf-8" src="/scripts/laypage1.2/laypage.js"></script>



<script type="text/javascript">


    var url = '/admin/api/project/list.ashx';

    var commVm = new Vue({
        el: '.maindiv',
        data: {
            dataList: [],
            total:0,
            pageindex: 1,
            pagesize: 1,
            keyword: '',
            cateId: 1,
            totalpage:0
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
                    cate_id:_this.cateId
                };

                $.ajax({
                    type: 'post',
                    url: url,
                    data: reqData,
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.status) {
                            _this.total = resp.result.total;
                            _this.dataList = resp.result.list
                            _this.totalpage = resp.result.totalpage;
                            console.log('_this.dataLis', _this.dataList);

                            laypage({
                                cont: document.getElementById('pagelist'), //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                                pages: _this.totalpage, //通过后台拿到的总页数
                                curr: _this.pageindex, //当前页
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
                            console.log('获取数据出错');
                        }
                    }
                });
            },

            changePage: function () {
                var _this = this;
                _this.loadData();
            },
            Search: function () {
                var _this = this;
                _this.loadData();
            }


            //loadMore: function () {
            //    if (this.log.list.length >= this.log.total) return;
            //    this.log.page++;
            //    this.loadData();
            //},
        }
    });

    $(function () {
        commVm.init();



    });

</script>
