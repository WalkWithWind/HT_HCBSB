<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_user_recruit_edit.aspx.cs" Inherits="HT.Admin.admin.labour_services.ht_user_recruit_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑招聘信息</title>
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
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">

            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>招聘信息管理</span>
            <i class="arrow"></i>
            <span>编辑招聘信息</span>
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
                            <asp:ListItem Value="1" Selected="True">未审核</asp:ListItem>
                            <asp:ListItem Value="2">已审核</asp:ListItem>
                            <asp:ListItem Value="3">审核失败</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>是否推荐</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="ishot" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                            <asp:ListItem Value="2">是</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>标题</dt>
                <dd>
                    <asp:TextBox ID="title" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>招聘分类</dt>
                <dd>
                    <asp:TextBox ID="classid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>招聘公司名称</dt>
                <dd>
                    <asp:TextBox ID="companyname" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司所在省/市/区</dt>
                <dd>
                    <asp:TextBox ID="gs_dz" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司所在详细地址</dt>
                <dd>
                    <asp:TextBox ID="address" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司性质</dt>
                <dd>
                    <asp:TextBox ID="companynature" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司规模</dt>
                <dd>
                    <asp:TextBox ID="companyscale" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司联系人</dt>
                <dd>
                    <asp:TextBox ID="companycontact" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司联系手机</dt>
                <dd>
                    <asp:TextBox ID="companymobile" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>公司简介</dt>
                <dd>
                    <asp:TextBox ID="companysummary" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>福利标签</dt>
                <dd>
                    <asp:TextBox ID="wealtag" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>招聘省/市/区</dt>
                <dd>
                    <asp:TextBox ID="zp_dz" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>招聘详细地址</dt>
                <dd>
                    <asp:TextBox ID="recruitaddress" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>学历要求</dt>
                <dd>
                    <asp:TextBox ID="education" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>经验要求</dt>
                <dd>
                    <asp:TextBox ID="experience" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>年龄要求</dt>
                <dd>
                    <asp:TextBox ID="age" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>薪资要求</dt>
                <dd>
                    <asp:TextBox ID="salary" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>职位简介</dt>
                <dd>
                    <asp:TextBox ID="positionsummary" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
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




