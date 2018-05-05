using System;
using System.Linq;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.manager
{
    public partial class manager_list : ManageBase
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected string keywords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            keywords = HTRequest.GetQueryString("keywords");
            pageSize = GetPageSize(10); //每页数量
            if (!IsPostBack)
            {
                ChkAdminLevel("manager_list", HTEnums.ActionEnum.View.ToString()); //检查权限
                BindData();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            page =  HTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = keywords;
            var list = db.ht_manager.Where(x => x.id > 0).OrderBy(x => x.id).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                list =
                    list.Where(
                        x =>
                            x.nickname.Contains(keywords) || x.username.Contains(keywords) ||
                            x.mobile.Contains(keywords)).ToList();
            }
            totalCount = list.Count;
            list = list.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            rptList.DataSource = list;
            rptList.DataBind();
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("finance_log_list.aspx", "Keywords={0}&page={1}",keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, pageUrl, 8);
        }

        private int GetPageSize(int defaultSize)
        {
            int pagesize;
            if (int.TryParse(Utils.GetCookie("manager_page_size", "HTPage"), out pagesize))
            {
                if (pagesize > 0)
                {
                    return pagesize;
                }
            }
            return defaultSize;
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("manager_list", HTEnums.ActionEnum.Delete.ToString());
            int sucCount = 0;
            int errorCount = 0;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    ht_manager model = db.ht_manager.FirstOrDefault(x => x.id == id);
                    if (model!=null)
                    {
                        db.ht_manager.Remove(model);
                        db.SaveChanges();
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(HTEnums.ActionEnum.Delete.ToString(), "删除管理员" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", keywords));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", txtKeywords.Text));
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out pagesize))
            {
                if (pagesize > 0)
                {
                    Utils.WriteCookie("manager_page_size", "HTPage", pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", keywords));
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="roleid">角色id</param>
        protected string GetRoleName(int roleid)
        {
            ht_manager_role role = db.ht_manager_role.FirstOrDefault(x => x.id == roleid);
            return role != null ? role.rolename : "";
        }
    }
}