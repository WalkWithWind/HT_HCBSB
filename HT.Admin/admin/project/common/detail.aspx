<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="HT.Admin.admin.project.common.detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>详情</title>
    <link href="/scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
	<link href="../../skin/default/style.css" rel="stylesheet" />
    <script type="text/javascript" charset="utf-8" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/scripts/artdialog/dialog-plus-min.js"></script>
	<script src="../../js/laymain.js"></script>
	<script src="../../js/common.js"></script>
     <style>
        .imgs{
            width:100px;
            height:100px;
        }
        .divimg{
            float:left;
            margin-left:10px;
        }
    </style>
</head>


    
<body class="mainbody">

	<div class="maindiv">

		 <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="/admin/center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>通用模板详情</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div id="floatHead" class="content-tab-wrap">
            <div class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a class="selected" href="javascript:;">详情</a></li>
                        
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
          <%--  <dl>
                <dt>标题</dt>
                <dd>
                   {{newsData.title}}
                </dd>
            </dl>--%>

            <dl>
                <dt>联系人</dt>
                <dd>
                  {{newsData.contact_name}}
                          
                </dd>
            </dl>
			<dl>
                <dt>联系电话</dt>
                <dd>
                  {{newsData.contact_phone}}
                </dd>
            </dl>
			 <dl>
                <dt>发布时间</dt>
                <dd>
                   {{newsData.add_time}}
                </dd>
            </dl>
		

            <dl>
                <dt>有效期</dt>
                <dd>
                    {{newsData.validity_num}}{{newsData.validity_unit}}
                </dd>
            </dl>


             <dl>
                <dt>标签</dt>
                <dd>
                    {{newsData.tags}}
                </dd>
            </dl>

            <dl>
                <dt>图片</dt>
                <dd>
                    <div class="divimg" v-for="img in imgs">
                        <img class="imgs" v-bind:src="img"/>
                    </div>
                </dd>
            </dl>


            <dl>
                <dt>其他补充</dt>
                <dd>
                  {{newsData.other_remark}}
                </dd>
            </dl>

            

             <dl>
                <dt>置顶金额</dt>
                <dd>
                  {{newsData.reward_money}}  {{newsData.set_top_money}}
                </dd>
            </dl>

            

            <dl>
                <dt>打赏金额</dt>
                <dd>
                  {{newsData.reward_money}} （元）
                </dd>
            </dl>
			
			<dl>
                <dt>审核状态</dt>
                <dd>
                   <span v-if="newsData.status==0">待审核</span>
                   <span v-if="newsData.status==1">审核通过</span>
                   <span v-if="newsData.status==2">审核不通过</span>
                </dd>
            </dl>
        </div>
      
     


	
        <!--内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-wrap">
				<input type="button" class="btn" value="审核通过" v-show="newsData.status==0" v-on:click="updateStatus(1)"/>
				<input type="button" class="btn yellow" value="审核不通过" v-show="newsData.status==0" v-on:click="updateStatus(2)"/>
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
        </div>
        <!--/工具栏-->



	</div>
   
       
   
</body>
</html>

<script type="text/javascript" charset="utf-8" src="/scripts/vue/vue.min.js"></script>
<script type="text/javascript">
    
    var url = '/admin/api/project/detail.ashx';
	 var updateUrl = '/admin/api/project/updatestatus.ashx';
    var commVm = new Vue({
        el: '.maindiv',
        data: {
            id: GetParm('id'),
            newsData: {},
            imgs:[]
        },
        methods: {
            init: function () {
                this.loadDetail();
            },
            loadDetail: function (){
                var reqData = {
                    id  : this.id
				};
				var _this = this;
                $.ajax({
                    type: 'post',
                    url: url,
                    data: reqData,
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.status) {
                            _this.newsData = resp.result;
                            //console.log('_this.data', this.newsData);
                            if (_this.newsData.imgs) {
                                _this.imgs = _this.newsData.imgs.split(',');
                            }
                        }
                        else {

                        }
                    }
                });
			},
			updateStatus: function (status) {
				var _this = this;

				parent.dialog({
				title: '提示',
				content: "确认更改状态?",
				okValue: '确定',
				ok: function () {
						
					$.ajax({
					type: 'post',
						url: updateUrl,
						data: { ids: _this.id, status: status },
						dataType: 'json',
						success: function (resp) {
							if (resp.status) {
								_this.showMsg("操作成功");
								
								_this.loadDetail();

							}
						    else {
							    _this.showMsg(resp.msg);
						    }
					    }
				    });
				},
				cancelValue: '取消',
                cancel: function () {

                }
				}).showModal();
			},
			showMsg: function (msg) {

				    parent.dialog({
						title: '提示',
						content: msg,
						okValue: '确定',
						ok: function () { }
					}).showModal();

			},


        }
    });

    $(function () {
        commVm.init();
    });



</script>
