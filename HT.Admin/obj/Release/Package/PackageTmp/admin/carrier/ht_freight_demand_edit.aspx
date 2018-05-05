<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_freight_demand_edit.aspx.cs" Inherits="HT.Admin.admin.carrier.ht_freight_demand_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑货运需求</title>
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
            <span>货运需求管理</span>
            <i class="arrow"></i>
            <span>编辑货运需求</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li><a href="javascript:;">轮播图</a></li>
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
                            <asp:ListItem Value="3">审核失败</asp:ListItem>
                            <asp:ListItem Value="4" Enabled="false">竞标中</asp:ListItem>
                            <asp:ListItem Value="5" Enabled="false">已完成</asp:ListItem>
                            <asp:ListItem Value="6" Enabled="false">已签订电子合同</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>起运港口</dt>
                <dd>
                    <asp:TextBox ID="startharbor" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>起运码头</dt>
                <dd>
                    <asp:TextBox ID="startquay" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>卸货港</dt>
                <dd>
                    <asp:TextBox ID="endharbor" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>卸货码头</dt>
                <dd>
                    <asp:TextBox ID="endquay" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货物名称</dt>
                <dd>
                    <asp:TextBox ID="name" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货物类型</dt>
                <dd>
                    <asp:TextBox ID="classid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>货量</dt>
                <dd>
                    <asp:TextBox ID="weight" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>单价</dt>
                <dd>
                    <asp:TextBox ID="price" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>受载时间</dt>
                <dd>
                    <asp:TextBox ID="shippingtime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>装运方式</dt>
                <dd>
                    <asp:TextBox ID="shippingmode" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>装运要求</dt>
                <dd>
                    <asp:TextBox ID="shipment" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>装货时限</dt>
                <dd>
                    <asp:TextBox ID="loaddays" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>卸货时限</dt>
                <dd>
                    <asp:TextBox ID="unloaddays" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>吃水深度</dt>
                <dd>
                    <asp:TextBox ID="draft" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>仓容体积</dt>
                <dd>
                    <asp:TextBox ID="storage" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>滞期费:元/吨/天</dt>
                <dd>
                    <asp:TextBox ID="demurrage" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>港口建设费</dt>
                <dd>
                    <asp:TextBox ID="portfee" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>其他杂费说明</dt>
                <dd>
                    <asp:TextBox ID="otherfee" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>支付方式</dt>
                <dd>
                    <asp:TextBox ID="payid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>发票类型</dt>
                <dd>
                    <asp:TextBox ID="invoicetype" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>发票抬头</dt>
                <dd>
                    <asp:TextBox ID="invoicetitle" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>其他说明</dt>
                <dd>
                    <asp:TextBox ID="remark" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>起港比例</dt>
                <dd>
                    <asp:TextBox ID="startrate" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>验港比例</dt>
                <dd>
                    <asp:TextBox ID="checkrate" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>最高限价</dt>
                <dd>
                    <asp:TextBox ID="maxprice" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>保证金</dt>
                <dd>
                    <asp:TextBox ID="margin" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>支付状态</dt>
                <dd>
                    <asp:TextBox ID="paystatus" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>支付时间</dt>
                <dd>
                    <asp:TextBox ID="paytime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>开标时间</dt>
                <dd>
                    <asp:TextBox ID="opentime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
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
                                        <img src="<%#Eval("imgurl")%>"/> 
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
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->

    </form>
</body>
</html>




