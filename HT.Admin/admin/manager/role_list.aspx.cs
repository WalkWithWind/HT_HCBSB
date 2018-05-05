using System;
using System.Linq;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.manager
{
    public partial class role_list : ManageBase
    {
        protected string keywords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            keywords = HTRequest.GetQueryString("keywords");
            if (!IsPostBack)
            {
                ChkAdminLevel("manager_role", HTEnums.ActionEnum.View.ToString()); //检查权限
                BindData();
            }
        }

        private void BindData()
        {
            txtKeywords.Text = keywords;
            var list = db.ht_manager_role.ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                list = list.Where(x => x.rolename.Contains(keywords)).ToList();
            }
            rptList.DataSource = list;
            rptList.DataBind();
        }

        /// <summary>
        /// 根据角色类型查询角色名称
        /// </summary>
        /// <param name="roletype">角色类型</param>
        protected string GetTypeName(int roletype)
        {
            string str = "";
            switch (roletype)
            {
                case 1:
                    str = "超级用户";
                    break;
                default:
                    str = "系统用户";
                    break;
            }
            return str;
        }

        //查询操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("role_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("manager_role", HTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    ht_manager_role role = db.ht_manager_role.FirstOrDefault(x => x.id == id);
                    if (role!=null)
                    {
                        db.ht_manager_role.Remove(role);
                        db.SaveChanges();
                        sucCount++;
                    }
                }
            }
            AddAdminLog(HTEnums.ActionEnum.Delete.ToString(), "删除管理员" + sucCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条", Utils.CombUrlTxt("role_list.aspx", "keywords={0}", this.keywords));
        }
    }
}