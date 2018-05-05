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
    public partial class ht_help_edit : ManageBase
    {
        protected string action = HTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("ht_help_list", HTEnums.ActionEnum.View.ToString()); //检查权限
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
                var model = db.ht_help.Where(s => s.id == id).FirstOrDefault();
                if (model == null)
                {
                    JscriptMsg("信息不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo();
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            var model = db.ht_help.Where(s => s.id == id).FirstOrDefault();
            if (model != null)
            {
                title.Text = model.title;
                contents.Text = model.contents;
                sort.Text = model.sort.ToString();
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                ht_help model = new ht_help();
                model.title = title.Text;
                model.contents = contents.Text;
                model.sort = Convert.ToInt32(sort.Text);
                model.update_time = DateTime.Now;
                db.ht_help.Add(model);
                db.SaveChanges();
                AddAdminLog(HTEnums.ActionEnum.Add.ToString(), "添加帮助中心:" + model.title); //记录日志
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
                var model = db.ht_help.Where(s => s.id == id).FirstOrDefault();
                model.title = title.Text;
                model.contents = contents.Text;
                model.sort = Convert.ToInt32(sort.Text);
                model.update_time = DateTime.Now;
                db.SaveChanges();
                AddAdminLog(HTEnums.ActionEnum.Add.ToString(), "修改帮助中心:" + model.title); //记录日志
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
                ChkAdminLevel("ht_help_list", HTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("保存成功！", "ht_help_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("ht_help_list", HTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("保存成功！", "ht_help_list.aspx");
            }
        }
    }
}