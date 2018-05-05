using HT.Admin.Models;
using HT.Model;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HT.Admin.admin.cate
{
    public partial class cate_list : ManageBase
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected int cid = 0;
        protected string pageTitle = "分类名列表";
        protected void Page_Load(object sender, EventArgs e)
        {
            cid = HTRequest.GetQueryInt("cid");
            #region 异步提交=================================
            string action = this.Request["action"];
            if (!string.IsNullOrWhiteSpace(action))
            {
                if (action == "add")
                {
                    add();
                }
                else if (action == "edit")
                {
                    edit();
                }
                return;
            }
            #endregion 异步提交=================================
            pageSize = GetPageSize(10); //每页数量
            ChkAdminLevel("cate_list", HTEnums.ActionEnum.View.ToString()); //检查权限
            if (!Page.IsPostBack)
            {
                bindBase(cid);
                RptBind();
            }
        }
        #region 异步提交=================================
        #region 新增=================================
        private void add()
        {
            ChkAdminLevelRejson("cate_list", HTEnums.ActionEnum.Add.ToString());
            //int pid = HTRequest.GetFormInt("pid");
            cid = HTRequest.GetFormInt("cid");
            string title = HTRequest.GetFormString("title");
            int sort = HTRequest.GetFormInt("sort",99);
            ht_category model = new ht_category();
            model.pid = 0;
            model.cid = cid;
            model.title = title;
            model.sort = sort;
            db.ht_category.Add(model);
            if (db.SaveChanges() > 0)
            {
                Response.Write("{\"status\":1,\"msg\":\"提交完成\"}");
                Response.End();
            }
            else
            {
                Response.Write("{\"status\":0,\"msg\":\"提交失败\"}");
                Response.End();
            }
        }
        #endregion 新增=================================
        #region 编辑=================================
        private void edit()
        {
            ChkAdminLevelRejson("cate_list", HTEnums.ActionEnum.Edit.ToString());
            int id = HTRequest.GetFormInt("id");
            string title = HTRequest.GetFormString("title");
            int sort = HTRequest.GetFormInt("sort", 99);
            ht_category model = db.ht_category.FirstOrDefault(p=>p.id== id);
            model.title = title;
            model.sort = sort;
            if (db.SaveChanges() > 0)
            {
                Response.Write("{\"status\":1,\"msg\":\"修改完成\"}");
                Response.End();
            }
            else
            {
                Response.Write("{\"status\":0,\"msg\":\"修改失败\"}");
                Response.End();
            }
        }
        #endregion 编辑=================================
        #endregion 异步提交=================================

        #region 检查页信息=================================
        private void bindBase(int cid)
        {
            if (cid == 0) return;
            ht_category cCate = db.ht_category.FirstOrDefault(p => p.id == cid);
            if (cCate!=null) pageTitle = cCate.title + "列表";
        }
        #endregion
        #region 数据绑定=================================
        private void RptBind()
        {
            try
            {
                this.page = HTRequest.GetQueryInt("page", 1);
                var shujuzhi = db.ht_category.Where(s => s.cid== cid);
                //数据数量  一定要放到绑定前面
                int count = shujuzhi.Count();
                rptList.DataSource = shujuzhi.OrderBy(s => s.sort).ThenBy(s => s.id).Skip(this.pageSize * (page - 1)).Take(this.pageSize).ToList();
                rptList.DataBind();
                //绑定页码
                txtPageNum.Text = this.pageSize.ToString();
                string pageUrl = Utils.CombUrlTxt("cate_list.aspx", "page={0}&cid={1}", "__id__", cid.ToString());
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
            if (int.TryParse(Utils.GetCookie("cate_list_"+cid+"_page_size"), out _pagesize))
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
                    Utils.WriteCookie("cate_list_" + cid + "_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("cate_list.aspx", "cid={0}", cid.ToString()));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("cate_list", HTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    var model = db.ht_category.Where(s => s.id == id).FirstOrDefault();
                    if (model != null)
                    {
                        try
                        {
                            db.ht_category.Remove(model);
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
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("cate_list.aspx", "cid={0}", cid.ToString()));
        }
    }
}