using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Utility;

namespace HT.Admin.admin.configuration
{
    public partial class sms_template_list :ManageBase
    {
        protected void Page_Load(object sender , EventArgs e)
        {
            if( !IsPostBack )
            {
                ChkAdminLevel("sms_template" , HTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        private void RptBind()
        {
            var list = db.ht_sms_template.ToList();
            rptList.DataSource = list;
            rptList.DataBind();
        }
    }
}