using HT.Admin.Models;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HT.Admin.admin.review
{
    public partial class list : ManageBase
    {
        public string type; 
        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("review_list", HTEnums.ActionEnum.View.ToString());

            type = this.Request["type"];
            if (string.IsNullOrWhiteSpace(type)) type = "comment";
        }
    }
}