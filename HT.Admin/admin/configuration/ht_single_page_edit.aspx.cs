using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.configuration
{
    public partial class ht_single_page_edit : ManageBase
    {
        private int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = HTRequest.GetQueryString("action");
            id = HTRequest.GetQueryInt("id");
            if (!string.IsNullOrEmpty(action) && action == HTEnums.ActionEnum.Edit.ToString())
            {
                action = HTEnums.ActionEnum.Edit.ToString();//修改类型
                if (id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                var model = db.ht_single_page.Where(s => s.id == id).FirstOrDefault();
                if (model == null)
                {
                    JscriptMsg("修改失败！", "");
                    return;
                }
            }
            if (!IsPostBack)
            {
                ChkAdminLevel("ht_single_page_edit", HTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == HTEnums.ActionEnum.Edit.ToString())
                {
                    ShowInfo(id);
                }
            }
        }

        private void ShowInfo(int oid)
        {
            try
            {
                var model = db.ht_single_page.Where(s => s.id == id).FirstOrDefault();
                if (model != null)
                {
                    txttitle.Text = model.title;
                    lblcode.Text = model.code;
                    txtcontent.Text = model.content;
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("ht_single_page_edit", HTEnums.ActionEnum.Edit.ToString()); //检查权限
            try
            {
                var model = db.ht_single_page.Where(s => s.id == id).FirstOrDefault();
                model.title = txttitle.Text;
                model.content = txtcontent.Text;
                model.update_time = DateTime.Now;
                db.SaveChanges();
                JscriptMsg("保存成功！", "ht_single_page_list.aspx");
            }
            catch (Exception)
            {
                JscriptMsg("修改失败！", "");
            }
        }
    }
}