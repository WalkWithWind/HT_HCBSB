using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;

namespace HT.Admin.admin.manager
{
    public partial class role_edit : ManageBase
    {
        private string action = HTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

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
                ht_manager_role temp = db.ht_manager_role.FirstOrDefault(x => x.id == id);
                if (temp == null)
                {
                    JscriptMsg("角色不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager_role", HTEnums.ActionEnum.View.ToString()); //检查权限
                RoleTypeBind(); //绑定角色类型
                NavBind(); //绑定导航
                if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(id);
                }
            }
        }

        #region 角色类型=================================
        private void RoleTypeBind()
        {
            ht_manager model = Manager;
            ddlRoleType.Items.Clear();
            ddlRoleType.Items.Add(new ListItem("请选择类型...", ""));
            if (model.roletype < 2)
            {
                ddlRoleType.Items.Add(new ListItem("超级用户", "1"));
            }
            ddlRoleType.Items.Add(new ListItem("系统用户", "2"));
        }
        #endregion

        #region 导航菜单=================================
        private void NavBind()
        {
            Navigation nav = new Navigation();
            rptList.DataSource = nav.GetList(0);
            rptList.DataBind();
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            ht_manager_role model = db.ht_manager_role.FirstOrDefault(x => x.id == _id);
            txtRoleName.Text = model.rolename;
            ddlRoleType.SelectedValue = model.roletype.ToString();
            //管理权限
            if (model.ht_manager_role_value != null)
            {
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    string navName = ((HiddenField)rptList.Items[i].FindControl("hidName")).Value;
                    CheckBoxList cblActionType = (CheckBoxList)rptList.Items[i].FindControl("cblActionType");
                    for (int n = 0; n < cblActionType.Items.Count; n++)
                    {
                        ht_manager_role_value modelt = model.ht_manager_role_value.ToList().Find(x => x.navname == navName && x.actiontype == cblActionType.Items[n].Value.ToString());
                        if (modelt != null)
                        {
                            cblActionType.Items[n].Selected = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                ht_manager_role model = new ht_manager_role
                {
                    rolename = txtRoleName.Text.Trim(),
                    roletype = int.Parse(ddlRoleType.SelectedValue),
                    addtime = DateTime.Now
                };

                //管理权限
                List<ht_manager_role_value> ls = new List<ht_manager_role_value>();
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    string navName = ((HiddenField)rptList.Items[i].FindControl("hidName")).Value;
                    CheckBoxList cblActionType = (CheckBoxList)rptList.Items[i].FindControl("cblActionType");
                    for (int n = 0; n < cblActionType.Items.Count; n++)
                    {
                        if (cblActionType.Items[n].Selected)
                        {
                            ls.Add(new ht_manager_role_value
                            {
                                navname = navName,
                                actiontype = cblActionType.Items[n].Value,
                                addtime = DateTime.Now
                            });
                        }
                    }
                }
                model.ht_manager_role_value = ls;
                db.ht_manager_role.Add(model);
                db.SaveChanges();
                AddAdminLog(HTEnums.ActionEnum.Add.ToString(), "添加管理角色:" + model.rolename); //记录日志
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
                //先删除该角色所有的权限
                List<ht_manager_role_value> rolevalues = db.ht_manager_role_value.Where(x => x.roleid == _id).ToList();
                db.ht_manager_role_value.RemoveRange(rolevalues);
                db.SaveChanges();

                //重新设置权限
                ht_manager_role model = db.ht_manager_role.FirstOrDefault(x => x.id == _id);
                model.rolename = txtRoleName.Text.Trim();
                model.roletype = int.Parse(ddlRoleType.SelectedValue);

                //管理权限
                List<ht_manager_role_value> ls = new List<ht_manager_role_value>();
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    string navName = ((HiddenField)rptList.Items[i].FindControl("hidName")).Value;
                    CheckBoxList cblActionType = (CheckBoxList)rptList.Items[i].FindControl("cblActionType");
                    for (int n = 0; n < cblActionType.Items.Count; n++)
                    {
                        if (cblActionType.Items[n].Selected)
                        {
                            ls.Add(new ht_manager_role_value
                            {
                                ht_manager_role = model,
                                navname = navName,
                                actiontype = cblActionType.Items[n].Value,
                                addtime = DateTime.Now
                            });
                        }
                    }
                }
                model.ht_manager_role_value = ls;
                db.SaveChanges();
                AddAdminLog(HTEnums.ActionEnum.Edit.ToString(), "修改管理角色:" + model.rolename); //记录日志
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 美化列表=================================
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //美化导航树结构
                Literal litFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                litFirst.Text = classLayer == 1 ? LitImg1 : string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
                //绑定导航权限资源
                string[] actionTypeArr = ((HiddenField)e.Item.FindControl("hidActionType")).Value.Split(',');
                CheckBoxList cblActionType = (CheckBoxList)e.Item.FindControl("cblActionType");
                cblActionType.Items.Clear();
                foreach (string item in actionTypeArr)
                {
                    if (Utils.ActionType().ContainsKey(item))
                    {
                        cblActionType.Items.Add(new ListItem(" " + Utils.ActionType()[item] + " ", item));
                    }
                }
            }
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == HTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("manager_role", HTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改管理角色成功！", "role_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("manager_role", HTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加管理角色成功！", "role_list.aspx");
            }
        }
    }
}