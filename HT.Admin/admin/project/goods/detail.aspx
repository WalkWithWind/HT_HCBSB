<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="HT.Admin.admin.project.goods.detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>货源详情</title>
    <link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="/admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" src="/scripts/webuploader/webuploader.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/uploader.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="/admin/js/common.js?v=1.0"></script>
    <script type="text/javascript" charset="utf-8" src="/scripts/vue/vue.min.js"></script>
</head>
<body  class="mainbody">

    <div class=".detaildiv">
        <!--导航栏-->
        <div class="location">

            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>货源管理</span>
            <i class="arrow"></i>
            <span>货源详情</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">货源详情</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>是否启用</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <table id="rblState">
	                        <tr>
		                        <td><input id="rblState_0" type="radio" name="rblState" value="2" checked="checked" /><label for="rblState_0">是</label></td><td><input id="rblState_1" type="radio" name="rblState" value="1" /><label for="rblState_1">否</label></td>
	                        </tr>
                        </table>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>广告位</dt>
                <dd>
                    <div class="rule-single-select">
                        <select name="ddlcode" id="ddlcode" datatype="*" errormsg="请选择广告位" sucmsg=" ">
	                        <option value="">请选择广告位...</option>
	                        <option selected="selected" value="pc_index_banner">PC-首页banner</option>

                        </select>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>标题</dt>
                <dd>
                    <input name="txtTitle" type="text" value="PC首页banner" id="txtTitle" class="input normal" datatype="*0-100" sucmsg=" " />
                </dd>
            </dl>
            <dl>
                <dt>图片</dt>
                <dd>
                    <input name="txtImg_url" type="text" value="/upload/201711/02/153203f049044ea8ae8bfd9d14682c82.jpg" id="txtImg_url" class="input normal upload-path" datatype="*" />
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl>
                <dt>链接</dt>
                <dd>
                    <input name="txtLink_url" type="text" value="#" id="txtLink_url" class="input normal" datatype="*" sucmsg=" " />
                    <span class="Validform_checktip">必须以http://开头</span></dd>
            </dl>
            <dl>
                <dt>排序数字</dt>
                <dd>
                    <input name="txtSortId" type="text" value="97" id="txtSortId" class="input small" datatype="n" sucmsg=" " />
                    <span class="Validform_checktip">*数字，越小越向前</span></dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <%--<input type="submit" name="btnSubmit" value="提交保存" id="btnSubmit" class="btn" />--%>
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </div>
     
</body>
</html>


<script type="text/javascript">


    var url = '/admin/api/project/detail.ashx';

    var commVm = new Vue({
        el: '.detaildiv',
        data: {
            id: GetParm('id'),
            newsData: null,
        },
        methods: {
            init: function () {
                this.loadDetail();
            },
            loadDetail: function (){
                var reqData = {
                    id  : this.id
                };
                $.ajax({
                    type: 'post',
                    url: url,
                    data: reqData,
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.status) {
                            this.newsData = resp.result;
                            console.log('_this.data', this.newsData);
                        }
                        else {

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
