using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.SessionState;
using HT.Admin.Models;
using HT.Model;
using HT.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HT.Admin.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {
        private readonly Entities _db = new Entities();
        private readonly Navigation _nav = new Navigation();
        private readonly SystemConfig _config = new SystemConfig();

        public void ProcessRequest(HttpContext context)
        {
            string action = HTRequest.GetQueryString("action");
            switch (action)
            {
                case "get_navigation_list":
                    get_navigation_list(context);
                    break;
                case "navigation_validate":
                    navigation_validate(context);
                    break;
                case "username_validate":
                    username_validate(context);
                    break;
                case "mobile_validate":
                    mobile_validate(context);
                    break;
                case "manager_validate":
                    manager_validate(context);
                    break;
                case "site_config_validate":
                    site_config_validate(context);
                    break;
            }
            context.Response.ContentType = "text/plain";
        }

        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="context"></param>
        private void mobile_validate(HttpContext context)
        {
            string mobile = HTRequest.GetString("param");
            string oldmobile = HTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(mobile))
            {
                context.Response.Write(NavInfo("手机号不可为空", "n"));
                return;
            }
            if (String.Equals(mobile, oldmobile, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Write(NavInfo("该手机号可使用", "y"));
                return;
            }
            ht_user user = _db.ht_user.FirstOrDefault(x => x.mobile == mobile);
            context.Response.Write(user != null
                ? NavInfo("该手机号已被占用，请更换", "n")
                : NavInfo("该手机号可使用", "y"));
        }

        /// <summary>
        /// 站点配置Key验证
        /// </summary>
        /// <param name="context"></param>
        private void site_config_validate(HttpContext context)
        {
            string navname = HTRequest.GetString("param");
            string oldname = HTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(navname))
            {
                context.Response.Write(NavInfo("程序调用名称", "n"));
                return;
            }
            if (String.Equals(navname, oldname, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Write(NavInfo("该名称已被占用", "y"));
                return;
            }
            ht_sys_config nav = _db.ht_sys_config.FirstOrDefault(x => x.xkey == navname);
            context.Response.Write(nav != null
                ? NavInfo("该调用名称已被占用", "n")
                : NavInfo("可以使用", "y"));
        }

        /// <summary>
        /// 后台用户名验证
        /// </summary>
        private void manager_validate(HttpContext context)
        {
            string username = HTRequest.GetString("param");
            string oldname = HTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write(NavInfo("用户名不可为空", "n"));
                return;
            }
            if (string.Equals(username, oldname, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Write(NavInfo("该用户名可使用", "y"));
                return;
            }
            ht_manager user = _db.ht_manager.FirstOrDefault(x => x.username == username);
            context.Response.Write(user != null
                ? NavInfo("该用户名已被占用，请更换", "n")
                : NavInfo("该用户名可使用", "y"));
        }


        /// <summary>
        /// 验证用户名是否重复
        /// </summary>
        private void username_validate(HttpContext context)
        {
            string username = HTRequest.GetString("param");
            string oldname = HTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write(NavInfo("用户名不可为空", "n"));
                return;
            }
            if (String.Equals(username, oldname, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Write(NavInfo("该用户名可使用", "y"));
                return;
            }
            ht_user user = _db.ht_user.FirstOrDefault(x => x.username == username);
            context.Response.Write(user != null
                ? NavInfo("该用户名已被占用，请更换", "n")
                : NavInfo("该用户名可使用", "y"));
        }

        /// <summary>
        ///  验证导航菜单别名是否重复
        /// </summary>
        private void navigation_validate(HttpContext context)
        {
            string navname = HTRequest.GetString("param");
            string oldname = HTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(navname))
            {
                context.Response.Write(NavInfo("该导航别名不可为空", "n"));
                return;
            }
            if (String.Equals(navname, oldname, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Write(NavInfo("该导航别名可使用", "y"));
                return;
            }
            ht_navigation nav = _db.ht_navigation.FirstOrDefault(x => x.name == navname);
            context.Response.Write(nav != null
                ? NavInfo("该导航别名已被占用，请更换", "n")
                : NavInfo("该导航别名可使用", "y"));
        }

        /// <summary>
        /// 获取后台导航字符串
        /// </summary>
        private void get_navigation_list(HttpContext context)
        {
            ht_manager manager = new ManageBase().Manager;
            if (manager == null)
            {
                return;
            }
            ht_manager_role role = _db.ht_manager_role.FirstOrDefault(x => x.id == manager.roleid);
            if (role == null)
            {
                return;
            }
            List<ht_navigation> list = _db.ht_navigation.Where(x => x.id > 0).OrderBy(x => x.sortid).ThenBy(x => x.id).ToList();
            List<ht_manager_role_value> values = _db.ht_manager_role_value.Where(x => x.roleid == manager.roleid).ToList();
            List<Menu> menus = new List<Menu>();
            _nav.GetChilds(list, ref menus, 0, 0);
            get_navigation_childs(context, menus, 0, Convert.ToInt32(role.roletype), values);
        }

        private void get_navigation_childs(HttpContext context, List<Menu> menus, int parentid, int roletype, List<ht_manager_role_value> rolevalues)
        {
            List<Menu> list = menus.Where(x => x.parentid == parentid).ToList();
            bool isWrite = false; //是否输出开始标签
            int i = 0;
            foreach (Menu item in list)
            {
                //是否在界面上显示菜单
                bool isshow = item.isshow != 2;
                if (isshow && roletype > 1)
                {
                    string[] actiontypes = item.actiontype.Split(',');
                    foreach (string actiontype in actiontypes)
                    {
                        if (actiontype.Equals("Show"))
                        {
                            ht_manager_role_value rolevalue =
                                rolevalues.Find(x => x.navname == item.name && x.actiontype == "Show");
                            if (rolevalue == null)
                            {
                                isshow = false;
                            }
                        }
                    }
                }
                //如果没有权限则不显示
                if (!isshow)
                {
                    if (isWrite && i == list.Count - 1 && parentid > 0)
                    {
                        context.Response.Write("</ul>\n");
                    }
                    continue;
                }

                //如果是顶级导航
                if (parentid == 0)
                {
                    context.Response.Write("<div class=\"list-group\">\n");
                    context.Response.Write("<h1 title=\"" + item.subtitle + "\">");
                    if (!string.IsNullOrEmpty(item.iconurl.Trim()))
                    {
                        context.Response.Write("<img src=\"" + item.iconurl + "\" />");
                    }
                    context.Response.Write("</h1>\n");
                    context.Response.Write("<div class=\"list-wrap\">\n");
                    context.Response.Write("<h2>" + item.title + "<i></i></h2>\n");
                    //调用自身迭代
                    get_navigation_childs(context, menus, item.id, roletype, rolevalues);
                    context.Response.Write("</div>\n");
                    context.Response.Write("</div>\n");
                }
                else //下级导航
                {
                    if (!isWrite)
                    {
                        isWrite = true;
                        context.Response.Write("<ul>\n");
                    }
                    context.Response.Write("<li>\n");
                    context.Response.Write("<a navid=\"" + item.name + "\"");
                    if (!string.IsNullOrEmpty(item.linkurl))
                    {
                        context.Response.Write(" href=\"" + item.linkurl + "\" target=\"mainframe\"");
                    }
                    if (!string.IsNullOrEmpty(item.iconurl))
                    {
                        context.Response.Write(" icon=\"" + item.iconurl + "\"");
                    }
                    context.Response.Write(" target=\"mainframe\">\n");
                    context.Response.Write("<span>" + item.title + "</span>\n");
                    context.Response.Write("</a>\n");
                    //调用自身迭代
                    get_navigation_childs(context, menus, item.id, roletype, rolevalues);
                    context.Response.Write("</li>\n");

                    if (i == (list.Count - 1))
                    {
                        context.Response.Write("</ul>\n");
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// 导航菜单通用返回消息格式
        /// </summary>
        /// <param name="info">消息</param>
        /// <param name="status">状态</param>
        private string NavInfo(string info, string status)
        {
            var result = new
            {
                info,
                status
            };
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 检查用户权限
        /// </summary>
        private bool CheckRight(int roleid, string navname, string actiontype)
        {
            ht_manager_role role = _db.ht_manager_role.FirstOrDefault(x => x.id == roleid);
            if (role != null)
            {
                if (role.roletype == 1)
                {
                    return true;
                }
                ht_manager_role_value model = role.ht_manager_role_value.FirstOrDefault(x => x.navname == navname && x.actiontype == actiontype);
                if (model != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        private void AddAdminLog(int userid, string username, string actiontype, string remark)
        {
            ht_manager_log log = new ht_manager_log
            {
                userid = userid,
                username = username,
                actiontype = actiontype,
                remark = remark,
                userip = HTRequest.GetIP(),
                addtime = DateTime.Now
            };
            _db.ht_manager_log.Add(log);
            _db.SaveChanges();
        }


        /// <summary>
        /// 返回消息通用格式
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="url">跳转地址</param>
        private string BackInfo(int status, string msg, string url = "")
        {
            var result = new
            {
                status,
                msg,
                url
            };
            return JsonConvert.SerializeObject(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}