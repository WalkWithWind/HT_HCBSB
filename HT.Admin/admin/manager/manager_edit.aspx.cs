using System;
using System.Linq;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.manager
{
    public partial class manager_edit : ManageBase
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        private string action = HTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = HTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == HTEnums.ActionEnum.Edit.ToString())
            {
                this.action = HTEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager_list", HTEnums.ActionEnum.View.ToString()); //检查权限
                RoleBind(ddlRoleId,Convert.ToInt32(Manager.roletype));
                if (action==HTEnums.ActionEnum.Edit.ToString())
                {
                    ShowInfo(id);
                }
            }
        }

        private void RoleBind(DropDownList ddl, int roleType)
        {
            var list = db.ht_manager_role.Where(x => x.id > 0);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择角色...", ""));
            foreach (ht_manager_role item in list)
            {
                if (Convert.ToInt32(item.roletype) >= roleType)
                {
                    ddl.Items.Add(new ListItem(item.rolename, item.id.ToString()));
                }
            }
        }

        private void ShowInfo(int oid)
        {
            ht_manager model = db.ht_manager.FirstOrDefault(x => x.id == oid);
            ddlRoleId.SelectedValue = model.roleid.ToString();
            cbIsLock.Checked = model.islock == 0;
            txtUserName.Text = model.username;
            txtUserName.Attributes.Remove("ajaxurl");
            if (!string.IsNullOrEmpty(model.password))
            {
                txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
            }
            txtRealName.Text = model.nickname;
            txtTelephone.Text = model.mobile;
        }

        private bool DoAdd()
        {
            ht_manager manager = db.ht_manager.FirstOrDefault(x => x.username == txtUserName.Text);
            if (manager!=null)
            {
                return false;
            }
            var role = db.ht_manager_role.FirstOrDefault(x=>x.id==Manager.roleid);
            if (role != null)
            {
                ht_manager model = new ht_manager
                {
                    roleid = Convert.ToInt32(ddlRoleId.SelectedValue),
                    islock = cbIsLock.Checked ? 1 : 0,
                    username = txtUserName.Text,
                    salt = Utils.GetCheckCode(10),
                    roletype = role.roletype,
                    mobile = txtTelephone.Text,
                    nickname = txtRealName.Text,
                    addtime = DateTime.Now
                };
                model.password = EncryptUtil.DesEncrypt(txtPassword.Text.Trim(), model.salt);
                db.ht_manager.Add(model);
                db.SaveChanges();
            }
            return true;
        }

        private bool DoEdit(int oid)
        {
            ht_manager manager = db.ht_manager.FirstOrDefault(x => x.id == oid);
            if (manager==null)
            {
                return false;
            }
            manager.roleid = Convert.ToInt32(ddlRoleId.SelectedValue);
            var role = db.ht_manager_role.FirstOrDefault(x => x.id == Manager.roleid);
            if (role != null)
            {
                manager.roletype = role.roletype;
            }
            manager.islock = cbIsLock.Checked ? 0 : 1;
            //判断密码是否更改
            if (txtPassword.Text.Trim() != defaultpassword)
            {
                //获取用户已生成的salt作为密钥加密
                manager.password = EncryptUtil.DesEncrypt(txtPassword.Text.Trim(), manager.salt);
            }
            manager.nickname = txtRealName.Text;
            manager.mobile = txtTelephone.Text;
            db.SaveChanges();
            AddAdminLog(HTEnums.ActionEnum.Edit.ToString(), "修改管理员:" + manager.username);
            return true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("manager_list", HTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改管理员信息成功！", "manager_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("manager_list", HTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加管理员信息成功！", "manager_list.aspx");
            }
        }
    }
}