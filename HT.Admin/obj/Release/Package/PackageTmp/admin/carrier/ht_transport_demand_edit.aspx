<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_transport_demand_edit.aspx.cs" Inherits="HT.Admin.admin.carrier.ht_transport_demand_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑承运需求</title>
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
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">

            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>承运需求管理</span>
            <i class="arrow"></i>
            <span>编辑承运需求</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="status" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">待审核</asp:ListItem>
                            <asp:ListItem Value="2">已审核</asp:ListItem>
                            <asp:ListItem Value="3" Enabled="false">竞标中</asp:ListItem>
                            <asp:ListItem Value="4" Enabled="false">已签订合同</asp:ListItem>
                            <asp:ListItem Value="5" Enabled="false">已完成</asp:ListItem>
                            <asp:ListItem Value="6" Enabled="false">已取消</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>船舶</dt>
                <dd>
                    <asp:TextBox ID="boatid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>港口</dt>
                <dd>
                    <asp:TextBox ID="harborid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>码头</dt>
                <dd>
                    <asp:TextBox ID="quayid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>始发时间</dt>
                <dd>
                    <asp:TextBox ID="starttime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>终到时间</dt>
                <dd>
                    <asp:TextBox ID="endtime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>停泊港口</dt>
                <dd>
                    <asp:TextBox ID="berthport" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>终到港</dt>
                <dd>
                    <asp:TextBox ID="terminalharborid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>终到码头</dt>
                <dd>
                    <asp:TextBox ID="terminalquayid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>是否为回头船</dt>
                <dd>
                    <asp:TextBox ID="isback" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>里程</dt>
                <dd>
                    <asp:TextBox ID="mileage" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>装运方式</dt>
                <dd>
                    <asp:TextBox ID="shipment" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>剩余吨位</dt>
                <dd>
                    <asp:TextBox ID="remaintonnage" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶说明</dt>
                <dd>
                    <asp:TextBox ID="remark" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
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




