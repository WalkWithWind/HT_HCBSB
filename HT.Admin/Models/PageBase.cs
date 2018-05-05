using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using HT.Model;
using HT.Utility;

namespace HT.Admin.Models
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class PageBase : Page
    {
        /// <summary>
        /// DB Entity
        /// </summary>
        protected Entities db = new Entities();

        /// <summary>
        /// 站点配置
        /// </summary>
        protected SystemConfig SiteConfig = new SystemConfig();

        public PageBase()
        {

        }

        protected override void OnPreInit(EventArgs e)
        {

            #region 是否启用全站HTTPS

            if (Convert.ToInt32(SiteConfig["usehttps"]) == 2)
            {
                Uri uri = Request.Url;
                string url = string.Empty;
                if (!uri.AbsoluteUri.StartsWith("https:"))
                {
                    url = uri.AbsoluteUri.Replace("http:", "https:");
                }
                if (!string.IsNullOrEmpty(url))
                {
                    Response.Redirect(url);
                    return;
                }
            }

            #endregion

            base.OnPreInit(e);
        }

        /// <summary>
        /// 管理员信息
        /// </summary>
        public ht_manager Manager
        {
            get
            {
                if (!IsAdminLogin)
                {
                    return null;
                }
                ht_manager model = Session[HTKeys.SESSION_ADMIN_INFO] as ht_manager;
                //获取最新的数据
                model = db.ht_manager.FirstOrDefault(x => x.id == model.id);
                return model;
            }
        }

        /// <summary>
        /// 管理员是否已登录
        /// </summary>
        public bool IsAdminLogin
        {
            get
            {
                if (Session[HTKeys.SESSION_ADMIN_INFO] != null)
                {
                    return true;
                }
                string name = Utils.GetCookie("AdminName", "HT");
                string pwd = Utils.GetCookie("AdminPwd", "HT");
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd))
                {
                    ht_manager model = db.ht_manager.FirstOrDefault(x => x.username == name && x.password == pwd);
                    if (model == null)
                    {
                        return false;
                    }
                    Session[HTKeys.SESSION_ADMIN_INFO] = model;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 通用JS调用
        /// </summary>
        /// <param name="script"></param>
        protected void DoScript(string script)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "jquery", "<script>" + script + "</script>");
        }
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        protected void JscriptMsg(string msgtitle, string url)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
    }
}