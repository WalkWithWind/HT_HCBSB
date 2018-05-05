<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_order_edit.aspx.cs" Inherits="HT.Admin.admin.order.ht_order_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑订单列表</title>
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
            <span>订单列表管理</span>
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
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>订单编号</dt>
                <dd>
                    <asp:TextBox ID="orderno" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主用户</dt>
                <dd>
                    <asp:TextBox ID="captainuid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货主用户</dt>
                <dd>
                    <asp:TextBox ID="shipperuid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>承运需求</dt>
                <dd>
                    <a runat="server" id="carrierid">查看</a>
                </dd>
            </dl>
            <dl>
                <dt>货运需求</dt>
                <dd>
                    <a runat="server" id="freightid">查看</a>
                </dd>
            </dl>
            <dl>
                <dt>订单状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="orderstatus" RepeatDirection="Horizontal" Enabled="false">
                            <asp:ListItem Value="1" Selected="True">已下单</asp:ListItem>
                            <asp:ListItem Value="2">托运中</asp:ListItem>
                            <asp:ListItem Value="3">已完成</asp:ListItem>
                            <asp:ListItem Value="4">已取消</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>货主评价状态</dt>
                <dd>
                    <asp:TextBox ID="appraisestatus" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>支付方式</dt>
                <dd>
                    <asp:TextBox ID="payid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>支付状态</dt>
                <dd>
                    <asp:TextBox ID="paystatus" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>预付款支付时间</dt>
                <dd>
                    <asp:TextBox ID="payimpresttime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>尾款支付时间</dt>
                <dd>
                    <asp:TextBox ID="payfinaltime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>预付款比例</dt>
                <dd>
                    <asp:TextBox ID="imprestrate" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>尾款比例</dt>
                <dd>
                    <asp:TextBox ID="finalrate" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>预付款金额</dt>
                <dd>
                    <asp:TextBox ID="imprestamount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>尾款金额</dt>
                <dd>
                    <asp:TextBox ID="finalamount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主的承运船舶</dt>
                <dd>
                    <asp:TextBox ID="boatid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶识别码</dt>
                <dd>
                    <asp:TextBox ID="boatcode" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货物开始托运时间</dt>
                <dd>
                    <asp:TextBox ID="starttime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>订单完成时间</dt>
                <dd>
                    <asp:TextBox ID="finishtime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货运需求货量</dt>
                <dd>
                    <asp:TextBox ID="weight" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>中标</dt>
                <dd>
                    <asp:TextBox ID="bid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>成交价格</dt>
                <dd>
                    <asp:TextBox ID="bidprice" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>到港状态</dt>
                <dd>
                    <asp:TextBox ID="isarrive" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主确认到港时间</dt>
                <dd>
                    <asp:TextBox ID="arrivetime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>验港状态</dt>
                <dd>
                    <asp:TextBox ID="checkstatus" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>验港时间</dt>
                <dd>
                    <asp:TextBox ID="checktime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船主验港货量</dt>
                <dd>
                    <asp:TextBox ID="checkweight" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>验港备注</dt>
                <dd>
                    <asp:TextBox ID="checkremark" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货物单价</dt>
                <dd>
                    <asp:TextBox ID="unitprice" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>滞期费用</dt>
                <dd>
                    <asp:TextBox ID="delayamount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>其他费用</dt>
                <dd>
                    <asp:TextBox ID="otheramount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货主应付的尾款</dt>
                <dd>
                    <asp:TextBox ID="payablefinalamount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货主实际需支付的尾款</dt>
                <dd>
                    <asp:TextBox ID="realfinalamount" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>尾款账单状态</dt>
                <dd>
                    <asp:TextBox ID="issend" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>尾款账单发送时间</dt>
                <dd>
                    <asp:TextBox ID="sendtime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>订单生成时间</dt>
                <dd>
                    <asp:TextBox ID="addtime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
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




