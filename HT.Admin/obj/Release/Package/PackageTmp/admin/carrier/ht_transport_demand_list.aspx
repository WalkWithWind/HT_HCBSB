<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ht_transport_demand_list.aspx.cs" Inherits="HT.Admin.admin.carrier.ht_transport_demand_list" %>

<%@ Import Namespace="HT.Utility" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>承运列表</title>
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
            <span>承运列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="box-wrap">
                    <div class="l-list">
                        <ul class="icon-list">
                            <%--<li><a class="add" href="ht_transport_demand_edit.aspx?action=<%=HTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>--%>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <%--<li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>--%>
                        </ul>
                        <div class="menu-list ">
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                    <asp:ListItem Text="承运状态" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="待审核" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="竞标中" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="已签订合同" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="已完成" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="已取消" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlisback" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlisback_SelectedIndexChanged">
                                    <asp:ListItem Text="回头船状态" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="是" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlshipment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlshipment_SelectedIndexChanged">
                                    <asp:ListItem Text="装运方式" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="整船" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="拼船" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%--<div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" placeholder="" />
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
                        <th align="center" width="7%">用户名称</th>
                        <th align="center" width="7%">船舶名称</th>
                        <th align="center" width="7%">港口名称</th>
                        <th align="center" width="7%">码头名称</th>
                        <th align="center" width="7%">始发时间</th>
                        <th align="center" width="7%">终到时间</th>
                        <th align="center" width="5%">终到港</th>
                        <th align="center" width="5%">终到码头</th>
                        <th align="center" width="5%">装运方式</th>
                        <th align="center" width="5%">回头船</th>
                        <th align="center" width="7%">添加时间</th>
                        <th align="center" width="5%">状态</th>
                        <th align="center" width="5%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_user_model(Eval("userid"), "name")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_boat_model(Eval("boatid"), "name")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_harbor_title(Eval("harborid"))%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_quay_title(Eval("quayid"))%></td>
                    <td align="center"><%#Eval("starttime")%></td>
                    <td align="center"><%#Eval("endtime")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_harbor_title(Eval("terminalharborid"))%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_quay_title(Eval("terminalquayid"))%></td>
                    <td align="center"><%#Eval("shipment").ToString() == "1" ? "整船" : "拼船"%></td>
                    <td align="center"><%#Eval("isback").ToString() == "1" ? "否" : "是"%></td>
                    <td align="center"><%#Eval("addtime")%></td>
                    <td align="center"><%#new HT.Admin.Models.XHelp().get_ht_transport_demand_status(Eval("status"))%></td>
                    <td align="center"><a href="ht_transport_demand_edit.aspx?action=<%#HTEnums.ActionEnum.Edit%>&id=<%#Eval("id")%>">查看</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <td align="center" colspan="14">数量:<span style="color: red"><%=count%></span>条</td>
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



