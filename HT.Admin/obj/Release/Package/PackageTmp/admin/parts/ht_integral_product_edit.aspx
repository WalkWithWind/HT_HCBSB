<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_integral_product_edit.aspx.cs" Inherits="HT.Admin.admin.parts.ht_integral_product_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑积分商品信息</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" src="../../scripts/webuploader/webuploader.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            $(".upload-img").InitUploader({ sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf" });
            $(".upload-album").InitUploader({ btntext: "批量上传", multiple: true, water: true, thumbnail: true, filesize: "10240", sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf" });
            //初始化编辑器
            var editor = KindEditor.create('.editor', {
                width: '100%',
                height: '350px',
                resizeType: 1,
                uploadJson: '/tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: '/tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });

            //追加
            $("#btnaddhtml").click(function () {
                var html = "";
                html += '<tr>'
                html += '    <th style="text-align: center;">'
                html += '        <input name="key" type="text" value="" datatype="*">'
                html += '    </th>'
                html += '    <td style="text-align: center;">'
                html += '        <input name="value" type="text" value="" datatype="*">'
                html += '    </td>'
                html += '    <td style="text-align: center;">'
                html += '        <input name="sort" type="text" style="width: 50px;"  value="99" datatype="n">'
                html += '    </td>'
                html += '    <td style="text-align: center;">'
                html += '        <input name="id" type="hidden" value="0">'
                html += '        <input type="button" value="移除" onclick="delrow(this)" />'
                html += '    </td>'
                html += '</tr>'
                $("#gradetb").append(html)
            });
        });
        //商品价格移除
        function delrow(obj) {
            $(obj).parent().parent().remove();
        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">

            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>积分商品信息管理</span>
            <i class="arrow"></i>
            <span>编辑积分商品信息</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li><a href="javascript:;">详细信息</a></li>
                        <li><a href="javascript:;">产品参数</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>是否下架</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="status" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                            <asp:ListItem Value="2">是</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>封面图片</dt>
                <dd>
                    <asp:Image ID="thumbnail_img" runat="server" CssClass="upload-path upload-images" Width="200" Height="200" />
                    <asp:TextBox ID="thumbnail" runat="server" CssClass="input normal upload-path" Style="display: none"></asp:TextBox>
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl>
                <dt>积分商品标题</dt>
                <dd>
                    <asp:TextBox ID="title" runat="server" CssClass="input normal" sucmsg=" " datatype="*"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>简介</dt>
                <dd>
                    <asp:TextBox ID="summary" runat="server" CssClass="input normal" sucmsg=" " datatype="*" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>所需兑换积分</dt>
                <dd>
                    <asp:TextBox ID="redeempoint" runat="server" CssClass="input normal" sucmsg=" " datatype="n"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>库存数量</dt>
                <dd>
                    <asp:TextBox ID="stock" runat="server" CssClass="input normal" sucmsg=" " datatype="n"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>品牌</dt>
                <dd>
                    <asp:TextBox ID="brand" runat="server" CssClass="input normal" sucmsg=" " datatype="*"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>型号</dt>
                <dd>
                    <asp:TextBox ID="model" runat="server" CssClass="input normal" sucmsg=" " datatype="*"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>尺码</dt>
                <dd>
                    <asp:TextBox ID="size" runat="server" CssClass="input normal" sucmsg=" " datatype="*"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>轮播图</dt>
                <dd>
                    <div class="upload-box upload-album"></div>
                    <input type="hidden" name="hidFocusPhoto" id="hidFocusPhoto" runat="server" class="focus-photo" />
                    <div class="photo-list">
                        <ul>
                            <asp:Repeater ID="rpt_image" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <input type="hidden" name="hid_photo_name" value="<%#Eval("id")%>|<%#Eval("url")%>|<%#Eval("url")%>" />
                                        <div class="img-box" onclick="setFocusImg(this);">
                                            <img src="<%#Eval("url")%>" bigsrc="<%#Eval("url")%>" />
                                        </div>
                                        <a href="javascript:;" onclick="delImg(this);">删除</a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </dd>
            </dl>
        </div>

        <div class="tab-content" style="display: none">
            <dl>
                <dt>产品详情</dt>
                <dd>
                    <asp:TextBox ID="details" runat="server" CssClass="input normal editor"></asp:TextBox>
                </dd>
            </dl>
        </div>

        <div class="tab-content" style="display: none">
            <dl>
                <dt>产品参数</dt>
                <dd>
                    <div class="table-container">
                        <table border="0" cellspacing="0" cellpadding="0" class="border-table" style="width: 300px;">
                            <tbody id="gradetb">
                                <tr>
                                    <th width="30%" style="text-align: center;">参数名称</th>
                                    <td width="30%" align="center">参数内容</td>
                                    <td width="10%" align="center">排序</td>
                                    <td width="10%" align="center">操作</td>
                                </tr>
                                <asp:Repeater runat="server" ID="rpt_nature">
                                    <ItemTemplate>
                                        <tr>
                                            <th style="text-align: center;">
                                                <input name="key" type="text" value="<%#Eval("xkey") %>" datatype="*">
                                            </th>
                                            <td style="text-align: center;">
                                                <input name="value" type="text" value="<%#Eval("xvalue") %>" datatype="*">
                                            </td>
                                            <td style="text-align: center;">
                                                <input name="sort" type="text" style="width: 50px;" value="<%#Eval("sort") %>" datatype="n">
                                            </td>
                                            <td style="text-align: center;">
                                                <input name="id" type="hidden" value="<%#Eval("id") %>">
                                                <input type="button" value="移除" onclick="delrow(this)" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <div>
                        <input type="button" value="增加" id="btnaddhtml" />
                    </div>
                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->

    </form>
</body>
</html>




