using HT.Admin.Models;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HT.Admin.admin.project.common
{
    public partial class list : ManageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("common_list", HTEnums.ActionEnum.View.ToString());
        }
    }
}