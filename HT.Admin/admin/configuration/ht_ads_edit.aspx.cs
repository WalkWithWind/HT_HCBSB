using HT.Admin.Models;
using HT.Model;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HT.Admin.admin.configuration
{
    public partial class ht_ads_edit : ManageBase
    {
        protected string action = HTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("ht_ads_list", HTEnums.ActionEnum.View.ToString()); //检查权限
            string _action = HTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == HTEnums.ActionEnum.Edit.ToString())
            {
                this.action = HTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = HTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                var model = db.ht_ad.Where(s => s.id == id).FirstOrDefault();
                if (model == null)
                {
                    JscriptMsg("信息不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                TreeBindP();
                if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo();
                }
            }
        }
        #region 绑定位置=================================
        private void TreeBindP()
        {
            var list = db.ht_ad_category.OrderBy(s => s.sort).ToList();
            this.ddlcode.Items.Clear();
            this.ddlcode.Items.Add(new ListItem("请选择广告位...", ""));
            foreach (var tiem in list)
            {
                this.ddlcode.Items.Add(new ListItem(tiem.title, tiem.code));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo()
        {
            var model = db.ht_ad.Where(s => s.id == id).FirstOrDefault();
            if (model != null)
            {
                rblState.SelectedValue = model.status.ToString();
                ddlcode.SelectedValue = model.code;
                txtTitle.Text = model.title;
                txtImg_url.Text = model.img_url;
                txtLink_url.Text = model.url;
                txtSortId.Text = model.sort.ToString();
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                ht_ad model = new ht_ad();
                model.title = txtTitle.Text;
                model.code = ddlcode.SelectedValue;
                //检测数量
                var code_model = db.ht_ad_category.Where(s => s.code == model.code).FirstOrDefault();
                if (code_model == null)
                {
                    return false;
                }
                var list_count = db.ht_ad.Where(s => s.code == model.code).ToList().Count;
                if (list_count >= code_model.num)
                {
                    JscriptMsg("此分类已达到最大上线", "");
                    return false;
                }
                model.img_url = txtImg_url.Text;
                model.url = txtLink_url.Text;
                model.sort = Convert.ToInt32(txtSortId.Text);
                model.remarks = "";
                model.status = Convert.ToInt32(rblState.SelectedValue);
                db.ht_ad.Add(model);
                db.SaveChanges();
                AddAdminLog(HTEnums.ActionEnum.Add.ToString(), "添加广告位:" + model.title); //记录日志
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                var model = db.ht_ad.Where(s => s.id == id).FirstOrDefault();
                model.title = txtTitle.Text;
                model.code = ddlcode.SelectedValue;
                model.img_url = txtImg_url.Text;
                model.url = txtLink_url.Text;
                model.sort = Convert.ToInt32(txtSortId.Text);
                model.remarks = "";
                model.status = Convert.ToInt32(rblState.SelectedValue);
                db.SaveChanges();
                AddAdminLog(HTEnums.ActionEnum.Add.ToString(), "修改广告位:" + model.title); //记录日志
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("ht_ads_list", HTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("保存成功！", "ht_ads_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("ht_ads_list", HTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("保存成功！", "ht_ads_list.aspx");
            }
        }
    }
}