using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using HT.Admin.Models;
using HT.Utility;

namespace HT.Admin.admin.manager
{
    public partial class manager_log : ManageBase
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
                BindData();
            }
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

        private void BindData()
        {
            page = HTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = keywords;
            var list = db.ht_manager_log.OrderByDescending(x => x.addtime).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                list =
                    list.Where(
                        x =>
                            x.actiontype.Contains(keywords) || 
                            x.remark.Contains(keywords) ||
                            x.username.Contains(keywords)).ToList();
            }
            totalCount = list.Count;
            list = list.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            rptList.DataSource = list;
            rptList.DataBind();
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("manager_log.aspx", "keywords={0}&page={1}", keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, pageUrl, 8);
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", txtKeywords.Text));
        }

        //设置分页数量
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
            Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("manager_log", HTEnums.ActionEnum.Delete.ToString()); //检查权限
            var list = db.ht_manager_log.Where(x => SqlFunctions.DateDiff("day", x.addtime,DateTime.Now) > 7).ToList();
            db.ht_manager_log.RemoveRange(list);
            int result = db.SaveChanges();
            AddAdminLog(HTEnums.ActionEnum.Delete.ToString(), "删除管理日志" + result + "条"); //记录日志
            JscriptMsg("删除日志" + result + "条", Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", keywords));
        }
    }
}