using System;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Utility;

namespace HT.Admin.admin.settings
{
    public partial class nav_list : ManageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChkAdminLevel("sys_navigation", HTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        private void RptBind()
        {
            Navigation nav = new Navigation();
            rptList.DataSource = nav.GetList(0);
            rptList.DataBind();
        }

        protected void rptList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal litFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                litFirst.Text = classLayer == 1 ? LitImg1 : string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
            }
        }
    }
}