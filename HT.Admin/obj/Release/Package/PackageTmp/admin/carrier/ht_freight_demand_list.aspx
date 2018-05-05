<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_freight_demand_list.aspx.cs" Inherits="HT.Admin.admin.carrier.ht_freight_demand_list" %>

<%@ Import Namespace="HT.Utility" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>货运需求列表</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>货运需求列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="box-wrap">
                    <div class="l-list">
                        <ul class="icon-list">
                            <%--<li><a class="add" href="ht_freight_demand_edit.aspx?action=<%=HTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>--%>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <%--<li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>--%>
                        </ul>
                        <div class="menu-list ">
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                    <asp:ListItem Text="状态" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="待审核" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="审核失败" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="竞标中" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="已完成" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="已签订电子合同" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlshippingmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlshippingmode_SelectedIndexChanged">
                                    <asp:ListItem Text="装运方式" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="整船" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="拼船" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlclassid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlclassid_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" placeholder="货物名称" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="lbtnSearch_Click">查询</asp:LinkButton>
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
                        <th align="center" width="10%">货物名称</th>
                        <th align="center" width="10%">货物类型</th>
                        <th align="center" width="5%">货量</th>
                        <th align="center" width="5%">单价</th>
                        <th align="center" width="10%">受载时间</th>
                        <th align="center" width="5%">装运方式</th>
                        <th align="center" width="5%">港口建设费</th>
                        <th align="center" width="5%">保证金</th>
                        <th align="center" width="5%">中标</th>
                        <th align="center" width="5%">状态</th>
                        <th align="center" width="7%">添加时间</th>
                        <th align="center" width="5%">用户名称</th>
                        <th align="center" width="5%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td align="center"><%#Eval("name")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_freight_goods_title(Eval("classid"))%></td>
                    <td align="center"><%#Eval("weight")%></td>
                    <td align="center"><%#Eval("price")%></td>
                    <td align="center"><%#Eval("shippingtime")%></td>
                    <td align="center"><%#Eval("shippingmode").ToString()=="1"?"整船":"拼船"%></td>
                    <td align="center"><%#Eval("portfee").ToString()=="1"?"竞价包含":"竞价不包含"%></td>
                    <td align="center"><%#Eval("margin")%></td>
                    <td align="center"><%#Eval("wid")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_freight_demand_status(Eval("status"))%></td>
                    <td align="center"><%#Eval("addtime")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_user_model(Eval("userid"),"name")%></td>
                    <td align="center"><a href="ht_freight_demand_edit.aspx?action=<%#HTEnums.ActionEnum.Edit%>&id=<%#Eval("id")%>">修改</a></td>
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
    </form>
</body>
</html>



