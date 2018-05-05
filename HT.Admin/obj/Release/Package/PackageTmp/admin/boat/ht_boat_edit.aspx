<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_boat_edit.aspx.cs" Inherits="HT.Admin.admin.boat.ht_boat_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑船舶信息</title>
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
            <span>船舶信息管理</span>
            <i class="arrow"></i>
            <span>编辑船舶信息</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">认证信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>船舶状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="status" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">待审核</asp:ListItem>
                            <asp:ListItem Value="2">已审核</asp:ListItem>
                            <asp:ListItem Value="3">已禁用</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>船舶名称</dt>
                <dd>
                    <asp:TextBox ID="name" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶识别码</dt>
                <dd>
                    <asp:TextBox ID="idcode" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶类型</dt>
                <dd>
                    <asp:TextBox ID="classifid" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船运类型</dt>
                <dd>
                    <asp:TextBox ID="transclassid" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>所属公司名称</dt>
                <dd>
                    <asp:TextBox ID="companyname" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶吨位</dt>
                <dd>
                    <asp:TextBox ID="tonnage" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶载重</dt>
                <dd>
                    <asp:TextBox ID="payload" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶长度</dt>
                <dd>
                    <asp:TextBox ID="longueur" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶宽度</dt>
                <dd>
                    <asp:TextBox ID="wide" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>船舶吃水面积</dt>
                <dd>
                    <asp:TextBox ID="draftarea" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>封面图</dt>
                <dd>
                    <asp:Image ID="thumbnail" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>图片</dt>
                <dd>
                    <div class="photo-list">
                        <ul>
                            <asp:Repeater ID="rpt_boat_image" runat="server">
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

        <div class="tab-content" style="display: none">
            <dl>
                <dt>认证状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="approvestatus" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">待认证</asp:ListItem>
                            <asp:ListItem Value="2">已认证</asp:ListItem>
                            <asp:ListItem Value="3">认证失败</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>运输资格证图片</dt>
                <dd>
                    <asp:Image ID="transcertimg" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>船舶证书</dt>
                <dd>
                    <asp:Image ID="boatcertimg" runat="server" />
                </dd>
            </dl>
            <dl>
                <dt>配员资格证图片</dt>
                <dd>
                    <asp:Image ID="allotimg" runat="server" />
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




