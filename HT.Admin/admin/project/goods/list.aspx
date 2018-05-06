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
    <link href="/css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="/scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/common.js"></script>
    <script  type="text/javascript"  charset="utf-8" src="/scripts/vue/vue.min.js"></script>
</head>

<body class="mainbody">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>广告管理</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
         <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="box-wrap">
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a class="add" href="ht_ads_edit.aspx?action=Add"><i></i><span>新增</span></a></li>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li>
                                <a onclick="return ExePostBack(&#39;btnDelete&#39;);" id="btnDelete" class="del" href="javascript:__doPostBack(&#39;btnDelete&#39;,&#39;&#39;)"><i></i><span>删除</span></a></li>
                        </ul>
                        <div class="menu-list ">
                            <div class="rule-single-select">
                                <select name="ddlPlaceCode" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ddlPlaceCode\&#39;,\&#39;\&#39;)&#39;, 0)" id="ddlPlaceCode">
	                                <option selected="selected" value="">请选择广告位...</option>
	                                <option value="pc_index_banner">PC-首页banner</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="r-list">
                        <input name="txtKeywords" type="text" id="txtKeywords" class="keyword" />
                        <a id="lbtnSearch" class="btn-search" href="javascript:__doPostBack(&#39;lbtnSearch&#39;,&#39;&#39;)">查询</a>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--文字列表-->
         <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                <tr>
                        <th align="center" width="5%">选择</th>
                        <th align="center" width="10%">标题</th>
                        <th align="center" width="10%">所在位置</th>
                        <th align="center" width="20%">图片</th>
                        <th align="center" width="5%">排序</th>
                        <th align="center" width="5%">状态</th>
                        <th align="center" width="5%">操作</th>
                </tr>
                <tr>
                    <td align="center">
                        <span class="checkall" style="vertical-align: middle;"><input id="rptList_chkId_0" type="checkbox" name="rptList$ctl01$chkId" /></span>
                        <input type="hidden" name="rptList$ctl01$hidId" id="rptList_hidId_0" value="3" />
                    </td>
                    <td align="center">PC首页banner</td>
                    <td align="center">PC-首页banner</td>
                    <td align="center">
                        <img src="/upload/201711/02/153203f049044ea8ae8bfd9d14682c82.jpg" width="100" />
                    </td>
                    <td align="center">97</td>
                    <td align="center">有效</td>
                    <td align="center"><a href="ht_ads_edit.aspx?action=Edit&id=3">修改</a></td>
                </tr>
            
                <tr>
                    <td align="center">
                        <span class="checkall" style="vertical-align: middle;"><input id="rptList_chkId_1" type="checkbox" name="rptList$ctl02$chkId" /></span>
                        <input type="hidden" name="rptList$ctl02$hidId" id="rptList_hidId_1" value="2" />
                    </td>
                    <td align="center">PC-首页banner</td>
                    <td align="center">PC-首页banner</td>
                    <td align="center">
                        <img src="/upload/201711/02/377730dd38314cc0ac9bffa915b19dfa.jpg" width="100" />
                    </td>
                    <td align="center">96</td>
                    <td align="center">有效</td>
                    <td align="center"><a href="ht_ads_edit.aspx?action=Edit&id=2">修改</a></td>
                </tr>
            
                <tr>
                    <td align="center">
                        <span class="checkall" style="vertical-align: middle;"><input id="rptList_chkId_2" type="checkbox" name="rptList$ctl03$chkId" /></span>
                        <input type="hidden" name="rptList$ctl03$hidId" id="rptList_hidId_2" value="1" />
                    </td>
                    <td align="center">PC-首页banner-1</td>
                    <td align="center">PC-首页banner</td>
                    <td align="center">
                        <img src="/upload/201711/02/d30a6521d146408289d954705d72fa5c.jpg" width="100" />
                    </td>
                    <td align="center">95</td>
                    <td align="center">有效</td>
                    <td align="center"><a href="ht_ads_edit.aspx?action=Edit&id=1">修改</a></td>
                </tr>
            
                
            </table>
        <!--/文字列表-->
        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><input name="txtPageNum" type="text" value="10" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;txtPageNum\&#39;,\&#39;\&#39;)&#39;, 0)" onkeypress="if (WebForm_TextBoxKeyHandler(event) == false) return false;" id="txtPageNum" class="pagenum" onkeydown="return checkNumber(event);" /><span>条/页</span>
            </div>
            <div id="PageContent" class="default"></div>
        </div>        <!--/内容底部-->
</body>
</html>





<script type="text/javascript">


    var commVm = new Vue({
        el: '.mainbody',
        data: {
            dataList: [],
            total:0,
            pageindex: 1,
            pagesize: 20,
            keyword:''
        },
        methods: {
            init: function () {
                this.loadData();
            },
      
            loadData: function () {
                var _this = this;


                var reqData = {
                    pageindex: _this.pageindex,
                    pagesize: _this.pagesize,
                    keyword: _this.keyword
                };

                $.ajax({
                    type: 'post',
                    url: '/Serv/API/Flow/List.ashx',
                    data: reqData,
                    dataType: 'json',
                    success: function (resp) {

                        if (resp.status) {
                            _this.total = resp.result.totalcount;
                            _this.dataList = _this.dataList.concat(resp.result.list);
                           
                            //console.log(_this.groupLogs);
                        }
                        else {

                        }
                    }
                });
            },


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
