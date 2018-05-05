using HT.Admin.Models;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HT.Admin.admin.configuration
{
    public partial class ht_ads_list : ManageBase
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected string keyword;

        protected string code;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.keyword = HTRequest.GetQueryString("keyword");
            this.code = HTRequest.GetQueryString("code");
            this.pageSize = GetPageSize(10); //每页数量
            ChkAdminLevel("ht_ads_list", HTEnums.ActionEnum.View.ToString()); //检查权限
            if (!Page.IsPostBack)
            {
                TreeBindP();
                RptBind();
            }
        }

        #region 绑定位置=================================
        private void TreeBindP()
        {
            var list = db.ht_ad_category.OrderBy(s => s.sort).ToList();
            this.ddlPlaceCode.Items.Clear();
            this.ddlPlaceCode.Items.Add(new ListItem("请选择广告位...", ""));
            foreach (var tiem in list)
            {
                this.ddlPlaceCode.Items.Add(new ListItem(tiem.title, tiem.code));
            }
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind()
        {
            try
            {
                this.page = HTRequest.GetQueryInt("page", 1);
                var shujuzhi = db.ht_ad.Where(s => s.id != 0);
                if (keyword != "")
                {
                    shujuzhi = shujuzhi.Where(s => s.title.Contains(keyword));
                    txtKeywords.Text = keyword;
                }
                if (code != "")
                {
                    shujuzhi = shujuzhi.Where(s => s.code == code);
                    ddlPlaceCode.SelectedValue = code;
                }
                //数据数量  一定要放到绑定前面
                int count = shujuzhi.Count();
                rptList.DataSource = shujuzhi.OrderByDescending(s => s.code).ThenByDescending(s => s.id).Skip(this.pageSize * (page - 1)).Take(this.pageSize).ToList();
                rptList.DataBind();
                //绑定页码
                txtPageNum.Text = this.pageSize.ToString();
                string pageUrl = Utils.CombUrlTxt("ht_ads_list.aspx", "page={0}&keyword={1}&code={2}", "__id__", keyword, this.code);
                PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, count, pageUrl, 8);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("ht_ads_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("ht_ads_list_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("ht_ads_list.aspx", "keyword={0}&code={1}", keyword, this.code));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("ht_ads_list", HTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    var model = db.ht_ad.Where(s => s.id == id).FirstOrDefault();
                    if (model != null)
                    {
                        try
                        {
                            db.ht_ad.Remove(model);
                            db.SaveChanges();
                            sucCount++;
                        }
                        catch (Exception)
                        {
                            errorCount++;
                        }
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("ht_ads_list.aspx", "keyword={0}&code={1}", keyword, this.code));
        }
        //筛选广告位
        protected void ddlPlaceCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("ht_ads_list.aspx", "keyword={0}&code={1}", keyword, ddlPlaceCode.SelectedValue));
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("ht_ads_list.aspx", "keyword={0}&code={1}", txtKeywords.Text, code));
        }
    }
}