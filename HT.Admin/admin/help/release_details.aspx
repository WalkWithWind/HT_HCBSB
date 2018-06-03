<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  CodeBehind="release_details.aspx.cs" Inherits="HT.Admin.admin.help.release_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
	<meta name="apple-mobile-web-app-capable" content="yes" />
    <title>发布须知</title>
	<link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
	<link href="/admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/wangEditor-3.1.1/wangEditor.min.css" rel="stylesheet" type="text/css" />
    <style>
        .areaDiv {
            max-width:700px;
            margin:20px 10px;
        }


        .page-footer {
            margin-left: 10px;
        }
    </style>
</head>
<body class="mainbody">
	<div class="maindiv">
		<!--导航栏-->
		<div class="location">
			<a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
			<a href="/admin/center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<span>发布须知</span>
		</div>
		<!--/导航栏-->


		<!--文字列表-->
        <div class="areaDiv">


            <div id="txtdiv" style="border:1px solid gray;min-height:240px">
            </div>

        </div>


        <div class="page-footer">
            <div class="btn-wrap">
                <input type="submit" @click="updateStatus()" name="btnSubmit" value="提交保存" id="btnSubmit" class="btn" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
        </div>

	
    </div>
</body>
    <script type="text/javascript" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/scripts/vue/vue.min.js"></script>
	<script type="text/javascript" src="/scripts/artdialog/dialog-plus-min.js"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
	<script type="text/javascript" charset="utf-8" src="/admin/js/common.js"></script>
    <script type="text/javascript" charset="utf-8" src="/scripts/wangEditor-3.1.1/wangEditor.min.js"></script>


    <script type="text/javascript">

        var url = '/admin/api/help/get.ashx';
        var upateurl = '/admin/api/help/update.ashx';

        var commVm = new Vue({
            el: '.maindiv',
            data: {
                id: GetParm('id'),
                desc: '',
                editor:'',
            },
            methods: {
                init: function () {
                    var _this = this;
                    _this.loadData();



                    var E = window.wangEditor
                    _this.editor = new E('#txtdiv')
                    _this.editor.customConfig.menus = [
                        'head',  // 标题
                        'bold',  // 粗体
                        'fontSize',  // 字号
                        'fontName',  // 字体
                        'italic',  // 斜体
                        'underline',  // 下划线
                        'strikeThrough',  // 删除线
                        'foreColor',  // 文字颜色
                        'backColor',  // 背景颜色
                        'link',  // 插入链接
                        'list',  // 列表
                        'justify',  // 对齐方式
                        'quote',  // 引用
                        'emoticon',  // 表情
                        //'image',  // 插入图片
                        'table',  // 表格
                        //'video',  // 插入视频
                        'code',  // 插入代码
                        'undo',  // 撤销
                        //'redo'  // 重复
                    ];
                    _this.editor.create()

                },

                loadData: function () {
                    var _this = this;
                    var reqData = {
                        id: _this.id
                    };
                    $.ajax({
                        type: 'post',
                        url: url,
                        data: reqData,
                        dataType: 'json',
                        success: function (resp) {
                            if (resp.status && resp.result) {
                                _this.editor.txt.html(resp.result.contents);
                                
                            }
                        }
                    });
                },

                showMsg: function (msg) {

                    parent.dialog({
                        title: '提示',
                        content: msg,
                        okValue: '确定',
                        ok: function () { }
                    }).showModal();

                },
                updateStatus: function () {
                    var _this = this;


                    var desc = _this.editor.txt.html()

                    if (!desc) {
                        _this.showMsg('请输入内容');
                        return;
                    }
                    var reqData = {
                        id: _this.id,
                        contents: desc
                    };

                    $.ajax({
                        type: 'post',
                        url: upateurl,
                        data: reqData,
                        dataType: 'json',
                        success: function (resp) {
                            if (resp.status) {
                                _this.showMsg('操作完成');
                            } else {
                                _this.showMsg(resp.msg);
                            }
                        }
                    });

                }

            }
        });

        $(function () {
            commVm.init();
        });



    </script>


</html>
