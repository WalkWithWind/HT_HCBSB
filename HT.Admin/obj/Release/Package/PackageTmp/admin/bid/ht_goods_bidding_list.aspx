<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_goods_bidding_list.aspx.cs" Inherits="HT.Admin.admin.bid.ht_goods_bidding_list" %>

<%@ Import Namespace="HT.Utility" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>报价竞标列表</title>
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
            <span>报价竞标列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="box-wrap">
                    <div class="l-list">
                        <ul class="icon-list">
                            <%--<li><a class="add" href="ht_goods_bidding_edit.aspx?action=<%=HTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>--%>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        </ul>
                        <div class="menu-list ">
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddliswin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddliswin_SelectedIndexChanged">
                                    <asp:ListItem Text="是否中标" Value ="0"></asp:ListItem>
                                    <asp:ListItem Text="否" Value ="1"></asp:ListItem>
                                    <asp:ListItem Text="是" Value ="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%--<div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" placeholder="标题" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="lbtnSearch_Click">查询</asp:LinkButton>
                    </div>--%>
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
                        <th align="center" width="10%">用户</th>
                        <th align="center" width="10%">手机号</th>
                        <th align="center" width="10%">承运需求</th>
                        <th align="center" width="10%">货运需求</th>
                        <th align="center" width="10%">报价</th>
                        <th align="center" width="10%">是否中标</th>
                        <th align="center" width="10%">中标时间</th>
                        <th align="center" width="10%">报价时间</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_user_model(Eval("userid"),"name")%></td>
                    <td align="center"><%#Eval("mobile")%></td>
                    <td align="center"><a href="../carrier/ht_transport_demand_edit.aspx?action=Edit&id=<%#Eval("bid")%>">查看详情</a></td>
                    <td align="center"><a href="../carrier/ht_freight_demand_edit.aspx?action=Edit&id=<%#Eval("pid")%>">查看详情</a></td>
                    <td align="center"><%#Eval("price")%></td>
                    <td align="center"><%#Eval("iswin").ToString()=="1"?"否":"是"%></td>
                    <td align="center"><%#Eval("wintime")%></td>
                    <td align="center"><%#Eval("addtime")%></td>
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



