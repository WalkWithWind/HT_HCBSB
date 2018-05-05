<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_user_resume_edit.aspx.cs" Inherits="HT.Admin.admin.labour_services.ht_user_resume_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑简历信息</title>
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
            <span>简历信息管理</span>
            <i class="arrow"></i>
            <span>编辑简历信息</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li><a href="javascript:;">工作经验</a></li>
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
                <dt>标题</dt>
                <dd>
                    <asp:TextBox ID="title" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>求职岗位</dt>
                <dd>
                    <asp:TextBox ID="classid" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>求职所在省/市/区</dt>
                <dd>
                    <asp:TextBox ID="qz_dz" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>居住省/市/区</dt>
                <dd>
                    <asp:TextBox ID="jz_dz" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>居住地址</dt>
                <dd>
                    <asp:TextBox ID="liveaddress" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>姓名</dt>
                <dd>
                    <asp:TextBox ID="name" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>性别</dt>
                <dd>
                    <asp:TextBox ID="sex" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>年龄</dt>
                <dd>
                    <asp:TextBox ID="age" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>手机号</dt>
                <dd>
                    <asp:TextBox ID="mobile" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>个性标签</dt>
                <dd>
                    <asp:TextBox ID="tags" runat="server" CssClass="input normal" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>工作年限</dt>
                <dd>
                    <asp:TextBox ID="workyear" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>

            <dl>
                <dt>薪资要求</dt>
                <dd>
                    <asp:TextBox ID="salary" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>毕业院校</dt>
                <dd>
                    <asp:TextBox ID="school" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>学历</dt>
                <dd>
                    <asp:TextBox ID="education" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>入学时间</dt>
                <dd>
                    <asp:TextBox ID="admissiontime" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>毕业时间</dt>
                <dd>
                    <asp:TextBox ID="graduationdate" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>所学专业</dt>
                <dd>
                    <asp:TextBox ID="major" runat="server" CssClass="input normal" ReadOnly="true"></asp:TextBox>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <dl>
                <dt>工作经验</dt>
                <dd>
                    <div class="table-container">
                        <table border="0" cellspacing="0" cellpadding="0" class="border-table" style="width: 800px;">
                            <tbody id="gradetb">
                                <tr>
                                    <td width="10%" align="center">开始时间</td>
                                    <td width="10%" align="center">结束时间</td>
                                    <td width="10%" align="center">行业</td>
                                    <td width="10%" align="center">职位</td>
                                    <td width="10%" align="center">薪资水平</td>
                                    <td width="50%" align="center">内容</td>
                                </tr>
                                <asp:Repeater runat="server" ID="rpt_exprience">
                                    <ItemTemplate>
                                        <tr>
                                            <th style="text-align: center;">
                                                <input name="key" type="text" value="<%#Eval("startdate") %>">
                                            </th>
                                            <td style="text-align: center;">
                                                <input name="value" type="text" value="<%#Eval("enddate") %>">
                                            </td>
                                            <td style="text-align: center;">
                                                <input name="value" type="text" value="<%#Eval("industry") %>">
                                            </td>
                                            <td style="text-align: center;">
                                                <input name="value" type="text" value="<%#Eval("position") %>">
                                            </td>
                                            <td style="text-align: center;">
                                                <input name="value" type="text" value="<%#Eval("salary") %>">
                                            </td>
                                            <td style="text-align: center;">
                                                <textarea><%#Eval("details") %></textarea>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
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




