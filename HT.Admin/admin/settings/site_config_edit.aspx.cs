using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.settings
{
    public partial class site_config_edit :ManageBase
    {
        protected void Page_Load(object sender , EventArgs e)
        {
            if( !IsPostBack )
            {
                ChkAdminLevel("site_config" , HTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();
            }
        }

        private void ShowInfo()
        {
            webname.Text = SiteConfig["webname"];
            webpath.Text = SiteConfig["webpath"];
            webcompany.Text = SiteConfig["webcompany"];
            weburl.Text = SiteConfig["weburl"];
            filepath.Text = SiteConfig["filepath"];
            fileextension.Text = string.Join(",", SiteConfig["fileextensions"]);
            attachsize.Text = SiteConfig["attachsize"];
            imgsize.Text = SiteConfig["imgsize"];
            imgmaxheight.Text = SiteConfig["imgmaxheight"];
            imgmaxwidth.Text = SiteConfig["imgmaxwidth"];
            telphone.Text = SiteConfig["telphone"];
            address.Text = SiteConfig["address"];
            email.Text = SiteConfig["email"];
            transportmargin.Text = SiteConfig["transportmargin"];
            freightmarginmax.Text = SiteConfig["freightmarginmax"];
            freightmarginmin.Text = SiteConfig["freightmarginmin"];
            freightmarginrate.Text = SiteConfig["freightmarginrate"];
            shareprofitrate.Text = SiteConfig["shareprofitrate"];
            webcopyright.Text = SiteConfig["webcopyright"];
			txtDistributionLevel1.Text= SiteConfig["distribution_level1"];
			txtDistributionLevel2.Text = SiteConfig["distribution_level2"];
			txtAppId.Text = SiteConfig["wx_appid"];
			txtAppSecret.Text = SiteConfig["wx_appsecret"];
			txtMchId.Text = SiteConfig["wx_mchid"];
			txtMchSecret.Text = SiteConfig["wx_mchsecret"];
			txtCerPath.Text = SiteConfig["wx_cerpath"];


		}

        protected void btnSubmit_Click(object sender , EventArgs e)
        {
            ChkAdminLevel("site_config" , HTEnums.ActionEnum.Edit.ToString()); //检查权限
            var list = db.ht_sys_config.ToList();
            list.Find(x => x.xkey == "webname").xvalue = webname.Text;
            list.Find(x => x.xkey == "webpath").xvalue = webpath.Text;
            list.Find(x => x.xkey == "webcompany").xvalue = webcompany.Text;
            list.Find(x => x.xkey == "weburl").xvalue = weburl.Text;
            list.Find(x => x.xkey == "filepath").xvalue = filepath.Text;
            list.Find(x => x.xkey == "fileextensions").xvalue = fileextension.Text;
            list.Find(x => x.xkey == "attachsize").xvalue = attachsize.Text;
            list.Find(x => x.xkey == "imgsize").xvalue = imgsize.Text;
            list.Find(x => x.xkey == "imgmaxheight").xvalue = imgmaxheight.Text;
            list.Find(x => x.xkey == "imgmaxwidth").xvalue = imgmaxwidth.Text;
            list.Find(x => x.xkey == "telphone").xvalue = telphone.Text;
            list.Find(x => x.xkey == "address").xvalue = address.Text;
            list.Find(x => x.xkey == "email").xvalue = email.Text;
            list.Find(x => x.xkey == "transportmargin").xvalue = transportmargin.Text;
            list.Find(x => x.xkey == "freightmarginmin").xvalue = freightmarginmin.Text;
            list.Find(x => x.xkey == "freightmarginmax").xvalue = freightmarginmax.Text;
            list.Find(x => x.xkey == "freightmarginrate").xvalue = freightmarginrate.Text;
            list.Find(x => x.xkey == "webcopyright").xvalue = webcopyright.Text;
            list.Find(x => x.xkey == "shareprofitrate").xvalue = shareprofitrate.Text;
			list.Find(x => x.xkey == "distribution_level1").xvalue = txtDistributionLevel1.Text;
			list.Find(x => x.xkey == "distribution_level2").xvalue = txtDistributionLevel2.Text;
			list.Find(x => x.xkey == "wx_appid").xvalue = txtAppId.Text;
			list.Find(x => x.xkey == "wx_appsecret").xvalue = txtAppSecret.Text;
			list.Find(x => x.xkey == "wx_mchid").xvalue = txtMchId.Text;
			list.Find(x => x.xkey == "wx_mchsecret").xvalue = txtMchSecret.Text;
			list.Find(x => x.xkey == "wx_cerpath").xvalue = txtCerPath.Text;
			db.SaveChanges();
            JscriptMsg("修改系统配置成功！" , "site_config_edit.aspx");
        }
    }
}