using System;
using System.Linq;
using HT.Model;
using HT.Utility;

namespace HT.Admin.Models
{
    /// <summary>
    /// 管理基类
    /// </summary>
    public class ManageBase:PageBase
    {

        public ManageBase()
        {
            Load += ManageBase_Load;
        }

        public void ManageBase_Load(object sender, EventArgs e)
        {
            if (!IsAdminLogin)
            {
                Response.Redirect("login.aspx");
            }
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="navname">菜单名称</param>
        /// <param name="actiontype">操作类型</param>
        public void ChkAdminLevel(string navname, string actiontype)
        {
            ht_manager model = Manager;
            bool result = Exist(Convert.ToInt32(model.roleid), navname, actiontype);
            if (!result)
            {
                string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\")";
                Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
                Response.End();
            }
        }
        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="navname">菜单名称</param>
        /// <param name="actiontype">操作类型</param>
        public void ChkAdminLevelRejson(string navname, string actiontype)
        {
            ht_manager model = Manager;
            bool result = Exist(Convert.ToInt32(model.roleid), navname, actiontype);
            if (!result)
            {
                Response.Write("{\"status\":0,\"msg\":\"您没有管理该页面的权限，请勿非法操作！\"}");
                Response.End();
            }
        }

        /// <summary>
        /// 检查是否有权限
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="navname">菜单名称</param>
        /// <param name="actiontype">操作类型</param>
        /// <returns>是否有权限</returns>
        private bool Exist(int roleid, string navname, string actiontype)
        {
            ht_manager_role role = db.ht_manager_role.FirstOrDefault(x => x.id == roleid);
            if (role == null)
            {
                return false;
            }
            if (role.roletype == 1)
            {
                return true;
            }
            ht_manager_role_value model = role.ht_manager_role_value.FirstOrDefault(x => x.navname == navname && x.actiontype == actiontype);
            return model != null;
        }

        /// <summary>
        /// 写管理员日志
        /// </summary>
        /// <param name="actiontype">操作类型</param>
        /// <param name="remark">日志内容</param>
        public bool AddAdminLog(string actiontype, string remark)
        {
            try
            {
                ht_manager_log log = new ht_manager_log
                {
                    username = Manager.username,
                    userid = Manager.id,
                    actiontype = actiontype,
                    remark = remark,
                    userip = HTRequest.GetIP(),
                    addtime = DateTime.Now
                };
                db.ht_manager_log.Add(log);
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