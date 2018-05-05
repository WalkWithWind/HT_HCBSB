<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_order_dissent_edit.aspx.cs" Inherits="HT.Admin.admin.order.ht_order_dissent_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑订单异议</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" src="../../scripts/webuploader/webuploader.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });

        function sub() {
            var hid_update = $("#hid_update").val();
            if (hid_update == 2) {
                var platformresult = $("#platformresult").val();
                if (platformresult == "0") {
                    alert("请选择平台处理结果");
                    return false;
                }
            }
        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">

            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>订单异议管理</span>
            <i class="arrow"></i>
            <span>编辑订单</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">异议图片</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>订单编号</dt>
                <dd>
                    <asp:TextBox ID="orderid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主用户</dt>
                <dd>
                    <asp:TextBox ID="userid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货主用户</dt>
                <dd>
                    <asp:TextBox ID="captainuid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货主的异议申请说明</dt>
                <dd>
                    <asp:TextBox ID="applyremark" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>异议类型</dt>
                <dd>
                    <asp:TextBox ID="classid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主对异议的处理说明</dt>
                <dd>
                    <asp:TextBox ID="dealremark" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主处理异议的金额</dt>
                <dd>
                    <asp:TextBox ID="dealamount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>联系方式</dt>
                <dd>
                    <asp:TextBox ID="contact" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>状态</dt>
                <dd>
                    <asp:TextBox ID="status" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>平台是否介入</dt>
                <dd>
                    <asp:TextBox ID="isplatform" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>平台处理结果</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="platformresult" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">未判定</asp:ListItem>
                            <asp:ListItem Value="1">货主赢</asp:ListItem>
                            <asp:ListItem Value="2">船主赢</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>平台处理状态</dt>
                <dd>
                    <asp:TextBox ID="platformstatus" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>平台处理时间</dt>
                <dd>
                    <asp:TextBox ID="dealtime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <dl>
                <dt>轮播图</dt>
                <dd>
                    <div class="photo-list">
                        <ul>
                            <asp:Repeater ID="rpt_image" runat="server">
                                <ItemTemplate>
                                    <li>
                                      <img src="<%#Eval("imgurl")%>" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" OnClientClick="return sub()" />
                <asp:HiddenField ID="hid_update" runat="server" Value="1" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->

    </form>
</body>
</html>




