<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_config_edit.aspx.cs" Inherits="HT.Admin.admin.settings.site_config_edit" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>系统参数设置</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script src="../../scripts/jquery/Validform.extend.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
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
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>参数设置</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div id="floatHead" class="content-tab-wrap">
            <div class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a class="selected" href="javascript:;">基本参数设置</a></li>
                        <li><a href="javascript:;">文件上传设置</a></li>
                        <li><a href="javascript:;">规则设置</a></li>
						 <li><a href="javascript:;">微信配置</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>主站名称</dt>
                <dd>
                    <asp:TextBox ID="webname" runat="server" CssClass="input normal" datatype="*2-255" sucmsg=" " />
                    <span class="Validform_checktip">*任意字符，控制在255个字符内</span>
                </dd>
            </dl>
            <dl>
                <dt>主站域名</dt>
                <dd>
                    <asp:TextBox ID="weburl" runat="server" CssClass="input normal" datatype="url" sucmsg=" " />
                    <span class="Validform_checktip">*主站域名 , 以“http://”开头</span>
                </dd>
            </dl>
            <dl>
                <dt>公司名称</dt>
                <dd>
                    <asp:TextBox ID="webcompany" runat="server" CssClass="input normal" />
                </dd>
            </dl>
            <dl>
                <dt>版权信息</dt>
                <dd>
                    <asp:TextBox ID="webcopyright" runat="server" CssClass="input normal" />
                </dd>
            </dl>
            <dl>
                <dt>客服电话</dt>
                <dd>
                    <asp:TextBox ID="telphone" runat="server" CssClass="input normal" ignore="ignore" />
                </dd>
            </dl>
            <dl>
                <dt>地址</dt>
                <dd>
                    <asp:TextBox ID="address" runat="server" CssClass="input normal" ignore="ignore" />
                </dd>
            </dl>
            <dl>
                <dt>邮箱</dt>
                <dd>
                    <asp:TextBox ID="email" runat="server" CssClass="input normal" ignore="ignore" />
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <dl>
                <dt>站点安装目录</dt>
                <dd>
                    <asp:TextBox ID="webpath" runat="server" CssClass="input txt" datatype="*" sucmsg=" " />
                    <span class="Validform_checktip">*安装路径 , 默认为"/"</span>
                </dd>
            </dl>
            <dl>
                <dt>文件上传目录</dt>
                <dd>
                    <asp:TextBox ID="filepath" runat="server" CssClass="input txt" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip">*文件保存的目录名，自动创建根目录下</span>
                </dd>
            </dl>
            <dl>
                <dt>文件上传类型</dt>
                <dd>
                    <asp:TextBox ID="fileextension" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " />
                    <span class="Validform_checktip">*以英文的逗号分隔开，如：“zip,rar”</span>
                </dd>
            </dl>
            <dl>
                <dt>附件上传大小</dt>
                <dd>
                    <asp:TextBox ID="attachsize" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
                    KB
      <span class="Validform_checktip">*超过设定的文件大小不予上传，0不限制</span>
                </dd>
            </dl>
            <dl>
                <dt>图片上传大小</dt>
                <dd>
                    <asp:TextBox ID="imgsize" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
                    KB
      <span class="Validform_checktip">*超过设定的图片大小不予上传，0不限制</span>
                </dd>
            </dl>
            <dl>
                <dt>图片最大尺寸</dt>
                <dd>
                    <asp:TextBox ID="imgmaxwidth" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
                    ×<asp:TextBox ID="imgmaxheight" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
                    px
      <span class="Validform_checktip">*左边高度，右边宽度，超出自动裁剪，0为不受限制</span>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <dl>
                <dt>船主保证金</dt>
                <dd>
                    <asp:TextBox ID="transportmargin" runat="server" CssClass="input txt" datatype="/^[0-9]+(\.[0-9]{1,2})?$/" sucmsg=" " />
                    <span class="Validform_checktip">*元</span>
                </dd>
            </dl>
            <dl>
                <dt>货主保证金最小值</dt>
                <dd>
                    <asp:TextBox ID="freightmarginmin" runat="server" CssClass="input txt" datatype="/^[0-9]+(\.[0-9]{1,2})?$/" sucmsg=" " />
                    <span class="Validform_checktip">*元</span>
                </dd>
            </dl>
            <dl>
                <dt>货主保证金最大值</dt>
                <dd>
                    <asp:TextBox ID="freightmarginmax" runat="server" CssClass="input txt" datatype="/^[0-9]+(\.[0-9]{1,2})?$/" sucmsg=" " />
                    <span class="Validform_checktip">*元</span>
                </dd>
            </dl>
            <dl>
                <dt>货主保证金比例</dt>
                <dd>
                    <asp:TextBox ID="freightmarginrate" runat="server" CssClass="input txt" datatype="numrange" sucmsg=" " min="0" max="100" />
                    <span class="Validform_checktip">%</span>
                </dd>
            </dl>
            <dl>
                <dt>分润金额</dt>
                <dd>
                    <asp:TextBox ID="shareprofitrate" runat="server" CssClass="input txt" datatype="/^[0-9]+(\.[0-9]{1,2})?$/" sucmsg=" " />
                    <span class="Validform_checktip">*每吨货所分润的金额 , 元/吨</span>
                </dd>
            </dl>
			    <dl>
                <dt>分佣一级比例(百分比)</dt>
                <dd>
                    <asp:TextBox ID="txtDistributionLevel1" runat="server" CssClass="input txt" datatype="/^[0-9]+(\.[0-9]{1,2})?$/" sucmsg=" " />
                    <span class="Validform_checktip">*返给上一级的用户比例百分比(0-100)</span>
                </dd>
            </dl>
			   <dl>
                <dt>分佣二级比例(百分比)</dt>
                <dd>
                    <asp:TextBox ID="txtDistributionLevel2" runat="server" CssClass="input txt" datatype="/^[0-9]+(\.[0-9]{1,2})?$/" sucmsg=" " />
                    <span class="Validform_checktip">*返给上二级的用户比例百分比(0-100)</span>
                </dd>
            </dl>
        </div>


	 <div class="tab-content" style="display: none">
            <dl>
                <dt>AppId</dt>
                <dd>
                    <asp:TextBox ID="txtAppId" runat="server" CssClass="input txt"  sucmsg=" " />
                    <span class="Validform_checktip">AppId</span>
                </dd>
            </dl>
            <dl>
                <dt>AppSecret</dt>
                <dd>
                   <asp:TextBox ID="txtAppSecret" runat="server" CssClass="input txt"  sucmsg=" " />
                    <span class="Validform_checktip">AppSecret</span>
                </dd>
            </dl>
            <dl>
                <dt>商户号(MCHID)</dt>
                <dd>
                    <asp:TextBox ID="txtMchId" runat="server" CssClass="input txt"  sucmsg=" " />
                    <span class="Validform_checktip">微信支付商户号</span>
                </dd>
            </dl>
            <dl>
                <dt>支付密钥</dt>
                <dd>
                    <asp:TextBox ID="txtMchSecret" runat="server" CssClass="input txt" MaxLength="32" />
                    <span class="Validform_checktip">支付密钥</span>
                </dd>
            </dl>
			   <dl>
                <dt>证书路径</dt>
                <dd>
                    <asp:TextBox ID="txtCerPath" runat="server" CssClass="input txt" />
                    <span class="Validform_checktip">证书路径</span>
                </dd>
            </dl>
        </div>
        <!--内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-wrap">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
