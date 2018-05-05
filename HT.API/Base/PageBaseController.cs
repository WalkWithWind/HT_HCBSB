using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using HT.Model;
using HT.Utility;

namespace HT.API.Base
{
    /// <summary>
    /// 普通控制器基类
    /// </summary>
    public class PageBaseController : Controller
    {
        /// <summary>
        /// DB Entity
        /// </summary>
        public Entities db { get; private set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public SystemConfig sysconfig { get; private set; }

        /// <summary>
        /// 请求筛选上下文
        /// </summary>
        public ActionExecutingContext Context
        {
            get; private set;
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {

            Context = context;
            sysconfig = new SystemConfig();
            db = new Entities();

            #region 是否启用全站HTTPS

            if (Convert.ToInt32(sysconfig["usehttps"]) == 2)
            {
                Uri uri = context.HttpContext.Request.Url;
                string url = string.Empty;
                if (uri != null)
                {
                    if (!uri.AbsoluteUri.StartsWith("https:"))
                    {
                        url = uri.AbsoluteUri.Replace("http:", "https:");
                    }
                }
                if (!string.IsNullOrEmpty(url))
                {
                    context.Result = new RedirectResult(url);
                    return;
                }
            }

            #endregion

            base.OnActionExecuting(context);

        }

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public PageBaseController()
        {
            db = new Entities();
            sysconfig = new SystemConfig();
        }
    }
}