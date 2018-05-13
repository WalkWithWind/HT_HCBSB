using HT.BLL;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登陆页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 广搞列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult AdList(string code)
        {
            List<ht_ad> model = BLLAd.GetAdList(code);

            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(apiResp);
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult CateList(int cid)
        {
            List<ht_category> model = BLLCategory.GetCateList(cid);
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(apiResp);
        }
    }
}