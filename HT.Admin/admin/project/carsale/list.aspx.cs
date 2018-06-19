using HT.Admin.Models;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HT.Admin.admin.project.carsale
{
    public partial class list : ManageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("carsale_list", HTEnums.ActionEnum.View.ToString());
        }
    }
}