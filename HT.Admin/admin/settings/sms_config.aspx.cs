using System;
using System.Linq;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.settings
{
    public partial class sms_config : ManageBase
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChkAdminLevel("sms_config", HTEnums.ActionEnum.View.ToString()); //检查权限
                ht_sms_config sms = db.ht_sms_config.FirstOrDefault();
                txtLink.Text = sms.smsurl;
                txtName.Text = sms.smsuser;
                txtPwd.Attributes["value"] = txtPwd.Attributes["value"] = defaultpassword;
                txtPwd.Text = EncryptUtil.DesDecrypt(sms.smspwd, "haitao");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sms_config", HTEnums.ActionEnum.Edit.ToString()); //检查权限
            ht_sms_config sms = db.ht_sms_config.FirstOrDefault();
            sms.smsurl = txtLink.Text;
            sms.smsuser = txtName.Text;
            if (txtPwd.Text.Trim() != defaultpassword)
            {
                sms.smspwd = EncryptUtil.DesEncrypt(txtPwd.Text, "haitao");
            }
            db.SaveChanges();
            JscriptMsg("保存成功", "");
        }
    }
}