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
    public partial class sms_template_edit :ManageBase
    {
        private int id;
        protected void Page_Load(object sender , EventArgs e)
        {
            string action = HTRequest.GetQueryString("action");
            id = HTRequest.GetQueryInt("id");
            if( !string.IsNullOrEmpty(action) && action == HTEnums.ActionEnum.Edit.ToString() )
            {
                action = HTEnums.ActionEnum.Edit.ToString();//修改类型
                if( id == 0 )
                {
                    JscriptMsg("传输参数不正确！" , "back");
                    return;
                }
            }
            if( !IsPostBack )
            {
                ChkAdminLevel("sms_template" , HTEnums.ActionEnum.View.ToString()); //检查权限
                if( action == HTEnums.ActionEnum.Edit.ToString() )
                {
                    ShowInfo(id);
                }
            }
        }

        private void ShowInfo(int oid)
        {
            ht_sms_template model = db.ht_sms_template.FirstOrDefault(x => x.id == oid);
            if( model != null )
            {
                txtRemark.Text = model.title;
                txtTemp.Text = model.contents;
                lblCode.Text = model.code;
            }
        }

        protected void btnSubmit_Click(object sender , EventArgs e)
        {
            ChkAdminLevel("sms_template" , HTEnums.ActionEnum.Edit.ToString()); //检查权限
            ht_sms_template model = db.ht_sms_template.FirstOrDefault(x => x.id == id);
            if( model != null )
            {
                model.title = txtRemark.Text;
                model.contents = txtTemp.Text;
                model.code = lblCode.Text;
                db.SaveChanges();
                JscriptMsg("修改短信模版成功！" , "sms_template_list.aspx");
            }
        }
    }
}