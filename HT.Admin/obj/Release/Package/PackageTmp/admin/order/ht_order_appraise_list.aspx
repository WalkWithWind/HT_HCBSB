<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_order_appraise_list.aspx.cs" Inherits="HT.Admin.admin.order.ht_order_appraise_list" %>

<%@ Import Namespace="HT.Utility" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>订单评价列表</title>
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
            <span>订单评价列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="box-wrap">
                    <div class="l-list">
                        <ul class="icon-list">
                            <%--<li><a class="add" href="ht_order_appraise_edit.aspx?action=<%=HTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>--%>
                            <%--<li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>--%>
                            <%--<li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>--%>
                        </ul>
                        <div class="menu-list ">
                        </div>
                    </div>
                    <div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" placeholder="评价内容" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="lbtnSearch_Click">查询</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--文字列表-->
        <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th align="center" width="5%">选择</th>
                        <th align="center" width="10%">订单编号</th>
                        <th align="center" width="10%">货主用户</th>
                        <th align="center" width="10%">船主用户</th>
                        <th align="center" width="10%">评价内容</th>
                        <th align="center" width="10%">发货速度评分</th>
                        <th align="center" width="10%">服务质量评分</th>
                        <th align="center" width="10%">货物损坏情况评分</th>
                        <th align="center" width="10%">添加时间</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_order_model(Eval("orderid"),"orderno")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_user_model(Eval("userid"),"name")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_user_model(Eval("captainuid"),"name")%></td>
                    <td align="center"><%#Eval("contents")%></td>
                    <td align="center"><%#Eval("deliveryspeed")%></td>
                    <td align="center"><%#Eval("servicequality")%></td>
                    <td align="center"><%#Eval("goodsdamage")%></td>
                    <td align="center"><%#Eval("addtime")%></td>
                </tr>
                <tr>
                    <td colspan="9">
                        <asp:Repeater runat="server" ID="rpt_img">
                            <ItemTemplate>
                                <a href="<%#Eval("imgurl") %>" target="_blank">
                                    <img src="<%#Eval("imgurl") %>" width="100" height="100" /></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
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



