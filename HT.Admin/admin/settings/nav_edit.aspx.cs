using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;
using Menu = HT.Admin.Models.Menu;

namespace HT.Admin.admin.settings
{
    public partial class nav_edit : ManageBase
    {
       private string action = HTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = HTRequest.GetQueryString("action");
            id = HTRequest.GetQueryInt("id");
            if (!string.IsNullOrEmpty(_action) && _action == HTEnums.ActionEnum.Edit.ToString())
            {
                action = HTEnums.ActionEnum.Edit.ToString();//修改类型
                if (id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                ht_navigation nav = db.ht_navigation.FirstOrDefault(x => x.id == id);
                if (nav == null)
                {
                    JscriptMsg("导航不存在或已被删除！", "back");
                    return;
                }
            }
            if (!IsPostBack)
            {
                ChkAdminLevel("sys_navigation", HTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(); //绑定导航菜单
                ActionTypeBind();// 绑定操作权限类型
                if (action == HTEnums.ActionEnum.Edit.ToString())
                {
                    ShowInfo();
                }
                else
                {
                    if (id > 0)
                    {
                        ddlParentId.SelectedValue = id.ToString();
                    }
                    cbIsShow.Checked = true;
                    txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate");
                }
            }
        }

        private void TreeBind()
        {
            Navigation navigation = new Navigation();
            List<Menu> list = navigation.GetList(0);
            ddlParentId.Items.Clear();
            ddlParentId.Items.Add(new ListItem("无父级导航", "0"));
            foreach (Menu item in list)
            {
                int clayer = item.classlayer;
                string title = item.title;
                if (clayer == 1)
                {
                    ddlParentId.Items.Add(new ListItem(title, item.id.ToString()));
                }
                else
                {
                    title = "├ " + title;
                    title = Utils.StringOfChar(clayer - 1, "　") + title;
                    ddlParentId.Items.Add(new ListItem(title, item.id.ToString()));
                }
            }
        }

        private void ActionTypeBind()
        {
            cblActionType.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
            {
                cblActionType.Items.Add(new ListItem(kvp.Value + "(" + kvp.Key + ")", kvp.Key));
            }
        }

        private void ShowInfo()
        {
            ht_navigation model = db.ht_navigation.FirstOrDefault(x => x.id == id);
            ddlParentId.SelectedValue = model.parentid.ToString();
            txtSortId.Text = model.sortid.ToString();
            txtName.Text = model.name;
            txtName.Focus(); //设置焦点，防止JS无法提交
            txtTitle.Text = model.title;
            txtSubTitle.Text = model.subtitle;
            txtIconUrl.Text = model.iconurl;
            txtLinkUrl.Text = model.linkurl;
            txtRemark.Text = model.remark;
            cbIsShow.Checked = Convert.ToInt32(model.isshow) == 1;
            string[] rightlist = model.actiontype.Split(',');
            for (int i = 0; i < cblActionType.Items.Count; i++)
            {
                foreach (string item in rightlist)
                {
                    if (String.Equals(item, cblActionType.Items[i].Value, StringComparison.CurrentCultureIgnoreCase))
                    {
                        cblActionType.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("sys_navigation", HTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改导航菜单成功！", "nav_list.aspx", "parent.loadMenuTree");
            }
            else //添加
            {
                ChkAdminLevel("sys_navigation", HTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加导航菜单成功！", "nav_list.aspx", "parent.loadMenuTree");
            }
        }

        private bool DoAdd()
        {
            try
            {
                ht_navigation navigation = new ht_navigation
                {
                    name = txtName.Text.Trim(),
                    title = txtTitle.Text.Trim(),
                    subtitle = txtSubTitle.Text.Trim(),
                    sortid = Convert.ToInt32(txtSortId.Text.Trim()),
                    iconurl = txtIconUrl.Text.Trim(),
                    linkurl = txtLinkUrl.Text.Trim(),
                    parentid = Convert.ToInt32(ddlParentId.SelectedValue),
                    addtime = DateTime.Now,
                    remark = txtRemark.Text.Trim(),
                    isshow = cbIsShow.Checked ? 1 : 2
                };
                //操作权限类型
                string rightstr = string.Empty;
                for (int i = 0; i < cblActionType.Items.Count; i++)
                {
                    if (cblActionType.Items[i].Selected && Utils.ActionType().ContainsKey(cblActionType.Items[i].Value))
                    {
                        rightstr += cblActionType.Items[i].Value + ",";
                    }
                }
                navigation.actiontype = Utils.DelLastComma(rightstr);
                db.ht_navigation.Add(navigation);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool DoEdit(int oid)
        {
            try
            {
                ht_navigation navigation = db.ht_navigation.FirstOrDefault(x => x.id == oid);
                navigation.name = txtName.Text.Trim();
                navigation.sortid = Convert.ToInt32(txtSortId.Text.Trim());
                navigation.title = txtTitle.Text.Trim();
                navigation.subtitle = txtSubTitle.Text.Trim();
                navigation.iconurl = txtIconUrl.Text.Trim();
                navigation.remark = txtRemark.Text.Trim();
                navigation.linkurl = txtLinkUrl.Text.Trim();
                navigation.parentid = Convert.ToInt32(ddlParentId.SelectedValue);
                navigation.isshow = cbIsShow.Checked ? 1 : 2;
                //操作权限类型
                string rightstr = string.Empty;
                for (int i = 0; i < cblActionType.Items.Count; i++)
                {
                    if (cblActionType.Items[i].Selected && Utils.ActionType().ContainsKey(cblActionType.Items[i].Value))
                    {
                        rightstr += cblActionType.Items[i].Value + ",";
                    }
                }
                navigation.actiontype = Utils.DelLastComma(rightstr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}