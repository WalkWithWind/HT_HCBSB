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
    public partial class ht_single_page_list : ManageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChkAdminLevel("ht_single_page_list", HTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        private void RptBind()
        {
            var list = db.ht_single_page.ToList();
            rptList.DataSource = list;
            rptList.DataBind();
        }
    }
}