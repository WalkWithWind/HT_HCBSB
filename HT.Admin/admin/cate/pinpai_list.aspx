﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pinpai_list.aspx.cs" Inherits="HT.Admin.admin.cate.pinpai_list" %>

<%@ Import Namespace="HT.Utility" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>分类列表</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/layer/3.1.0/theme/default/layer.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" src="/scripts/layer/3.1.0/layer.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <style>
        .hide {
            display: none;
        }
    </style>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>品牌列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="box-wrap">
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a class="add" href="javascript:;"><i></i><span>新增</span></a></li>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--文字列表-->
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th align="center" width="5%">选择</th>
                        <th align="center" width="10%">编号</th>
                        <th align="center" width="10%">首字母</th>
                        <th align="center" width="20%">图片</th>
                        <th align="center" width="25%">标题</th>
                        <th align="center" width="20%">排序</th>
                        <th align="center" width="10%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td align="center"><%#Eval("id")%></td>
                    <td align="center"><%#Eval("initial")%></td>
                    <td align="center">
                        <img style="height: 60px;" src="<%#Eval("img") %>" />
                    </td>
                    <td align="center"><%#Eval("title")%></td>
                    <td align="center"><%#Eval("sort")%></td>
                    <td align="center">
                        <a class="re_edit" data-id="<%#Eval("id")%>" data-title="<%#Eval("title")%>" data-sort="<%#Eval("sort")%>" 
                            data-initial="<%#Eval("initial")%>" data-img="<%#Eval("img")%>" 
                            href="javascript:;">修改</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"15\">暂无记录</td></tr>" : ""%>
            </table>
            </FooterTemplate>
        </asp:Repeater>
        <!--/文字列表-->
        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
        <div class="hide addDialog">
            <input type="hidden" class="id" name="id" value="" />
            <dl>
                <dt>名称：</dt>
                <dd>
                    <input type="text" class="title input" name="title" placeholder="名称" value="" /></dd>
            </dl>
            <dl>
                <dt>首字母：</dt>
                <dd>
                    <input type="text" class="initial input" name="initial" placeholder="首字母" value="" /></dd>
            </dl>
            <dl>
                <dt>图片：</dt>
                <dd>
                    <input class="txtImgUrl input upload-path" />
                    <div class="upload-box upload-img"></div>
                    &nbsp;<span class="linkImgUrl" style="color:#2A72C5;" onclick="showImg()">查看</span>
                </dd>
            </dl>
            <dl>
                <dt>排序：</dt>
                <dd>
                    <input type="text" class="sort input" name="sort" placeholder="排序" sucmsg="" datatype="n" value="99" />

                </dd>
            </dl>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        
        $('.add').click(function () {
            var dhtml = $('.addDialog').html();
            var d = dialog({
                title: '新增',
                content: dhtml,
                okValue: '提交',
                ok: function () {
                    var _thisDialog = this;
                    var _cid = $(_thisDialog.node).find('.cid').val();
                    var _title = $(_thisDialog.node).find('.title').val();
                    var _initial = $(_thisDialog.node).find('.initial').val();
                    var _img = $(_thisDialog.node).find('.txtImgUrl').val();
                    var _sort = $(_thisDialog.node).find('.sort').val();
                    $.ajax({
                        type: 'POST',
                        url: '/admin/cate/pinpai_list.aspx',
                        dataType: 'json',
                        data: { action: 'add', cid: _cid, title: _title, initial: _initial,img:_img, sort: _sort },
                        success: function (data) {
                            parent.jsprint(data.msg, '');
                            if (data.status == 1) {
                                _thisDialog.remove();
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                        }
                    });
                    return false;
                    //this.remove();
                }
            });
            d.show();
            //初始化上传控件
            $(".ui-dialog-body .upload-img").InitUploader({ filesize: "10240", sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf", filetypes: "gif,jpg,png,bmp" });
        });
        $('.re_edit').click(function () {
            var _oid = $(this).attr('data-id');
            var _otitle = $(this).attr('data-title');
            var _oinitial = $(this).attr('data-initial');
            var _oimg = $(this).attr('data-img');
            var _osort = $(this).attr('data-sort');
            var dhtml = $('.addDialog').html();
            var d = dialog({
                title: '编辑',
                content: dhtml,
                okValue: '提交',
                ok: function () {
                    var _thisDialog = this;
                    var _id = $(_thisDialog.node).find('.id').val();
                    var _cid = $(_thisDialog.node).find('.cid').val();
                    var _title = $(_thisDialog.node).find('.title').val();
                    var _initial = $(_thisDialog.node).find('.initial').val();
                    var _img = $(_thisDialog.node).find('.txtImgUrl').val();
                    var _sort = $(_thisDialog.node).find('.sort').val();
                    $.ajax({
                        type: 'POST',
                        url: '/admin/cate/pinpai_list.aspx',
                        dataType: 'json',
                        data: { action: 'edit', id: _id, cid: _cid, title: _title, initial: _initial,img:_img, sort: _sort },
                        success: function (data) {
                            parent.jsprint(data.msg, '');
                            if (data.status == 1) {
                                _thisDialog.remove();
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                        }
                    });
                    return false;
                    //this.remove();
                }
            });
            $(d.node).find('.id').val(_oid);
            $(d.node).find('.title').val(_otitle);
            $(d.node).find('.initial').val(_oinitial);
            $(d.node).find('.txtImgUrl').val(_oimg);
            $(d.node).find('.sort').val(_osort);
            d.show();
            //初始化上传控件
            $(".ui-dialog-body .upload-img").InitUploader({ filesize: "10240", sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf", filetypes: "gif,jpg,png,bmp" });
        });
    })
    function showImg() {
        var src = $(".ui-dialog-body .txtImgUrl").val();
        if (!src) {
            layer.msg('请上传图片');
            return;
        }
        layer.open({
            type: 1,
            title: '查看图片',
            shadeClose: true,
            shade: 0.8,
            area: ['800px', '90%'],
            content: '<img src=' + src + ' width="100%"  />'
        });
    }
</script>


