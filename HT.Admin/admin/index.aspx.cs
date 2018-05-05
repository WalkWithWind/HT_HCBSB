using System;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin
{
    public partial class index : ManageBase
    {
        protected ht_manager admin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                admin = Manager;
            }
        }

        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[HTKeys.SESSION_ADMIN_INFO] = null;
            Utils.WriteCookie("AdminName", "HT", -14400);
            Utils.WriteCookie("AdminPwd", "HT", -14400);
            Response.Redirect("login.aspx");
        }
    }
}