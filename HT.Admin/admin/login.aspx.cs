using System;
using System.Linq;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin
{
    public partial class login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserName.Text = Utils.GetCookie("RememberName");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userPwd = txtPassword.Text.Trim();
            if (userName.Equals("") || userPwd.Equals(""))
            {
                msgtip.InnerHtml = "请输入用户名或密码";
                return;
            }
            if (Session["AdminLoginCount"] == null)
            {
                Session["AdminLoginCount"] = 1;
            }
            else
            {
                Session["AdminLoginCount"] = Convert.ToInt32(Session["AdminLoginCount"]) + 1;
            }
            //判断登录错误次数
            if (Session["AdminLoginCount"] != null && Convert.ToInt32(Session["AdminLoginCount"]) > 5)
            {
                msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
                return;
            }
            ht_manager temp = db.ht_manager.FirstOrDefault(x => x.username == userName);
            if (temp == null)
            {
                msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            string pwd = EncryptUtil.DesEncrypt(txtPassword.Text, temp.salt);
            temp = db.ht_manager.FirstOrDefault(x => x.username == userName && x.password == pwd);
            if (temp == null)
            {
                msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            if (temp.islock == 1)
            {
                msgtip.InnerHtml = "用户已被禁用 , 登录失败 ! ";
                return;
            }
            Session[HTKeys.SESSION_ADMIN_INFO] = temp;
            Session.Timeout = 45;
            Utils.WriteCookie("RememberName", temp.username, 14400);
            Utils.WriteCookie("AdminName", "HT", temp.username);
            Utils.WriteCookie("AdminPwd", "HT", temp.password);
            Response.Redirect("index.aspx");
        }
    }
}