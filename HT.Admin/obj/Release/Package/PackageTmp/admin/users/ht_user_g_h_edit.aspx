<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_user_g_h_edit.aspx.cs" Inherits="HT.Admin.admin.users.ht_user_g_h_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑个人货主信息</title>
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
            <span>个人货主信息管理</span>
            <i class="arrow"></i>
            <span>编辑个人货主信息</span>
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
                <dt>用户状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="status" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">未审核</asp:ListItem>
                            <asp:ListItem Value="2">已审核</asp:ListItem>
                            <asp:ListItem Value="3">已禁用</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
                        <dl>
                <dt>用户类型</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="usertype" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">个人货主</asp:ListItem>
                            <asp:ListItem Value="2">企业货主</asp:ListItem>
                            <asp:ListItem Value="3">个人船主</asp:ListItem>
                            <asp:ListItem Value="4">企业船主</asp:ListItem>
                            <asp:ListItem Value="5">门店会员</asp:ListItem>
                            <asp:ListItem Value="6">无门店会员</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>用户名</dt>
                <dd>
                    <asp:TextBox ID="username" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <%--<dl>
                <dt>密码</dt>
                <dd>
                    <asp:TextBox ID="password" runat="server" CssClass="input normal" sucmsg=" " TextMode="Password" datatype="*6-20"></asp:TextBox>
                </dd>
            </dl>--%>
            <dl>
                <dt>昵称</dt>
                <dd>
                    <asp:TextBox ID="nickname" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>手机号</dt>
                <dd>
                    <asp:TextBox ID="mobile" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>邮箱</dt>
                <dd>
                    <asp:TextBox ID="email" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>QQ</dt>
                <dd>
                    <asp:TextBox ID="qq" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>微信号</dt>
                <dd>
                    <asp:TextBox ID="wechat" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>地址</dt>
                <dd>
                    <asp:TextBox ID="address" runat="server" CssClass="input normal" sucmsg=" " ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
        </div>

        <div class="tab-content" style="display: none">
            <dl>
                <dt>认证状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList runat="server" ID="certified_stauts" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">未认证</asp:ListItem>
                            <asp:ListItem Value="2">已提交待审核</asp:ListItem>
                            <asp:ListItem Value="3">已认证</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>姓名</dt>
                <dd>
                    <asp:TextBox ID="realname" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>身份证号</dt>
                <dd>
                    <asp:TextBox ID="idcard" runat="server" CssClass="input normal upload-path" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>身份证正面</dt>
                <dd>
                    <a id="idcard_pros_img" href="javascript:;" target="_blank" runat="server">查看</a>
                </dd>
            </dl>
            <dl>
                <dt>身份证反面</dt>
                <dd>
                    <a id="idcard_cons_img" href="javascript:;" target="_blank" runat="server">查看</a>
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




