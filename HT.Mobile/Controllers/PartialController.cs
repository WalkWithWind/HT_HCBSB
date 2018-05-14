using HT.BLL;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class PartialController : BaseController
    {
        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }
        #region 广告
        /// <summary>
        /// 广告列表
        /// </summary>
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
            return View(model);
        }
        /// <summary>
        /// 对应模板展示
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public ActionResult Template(string id)
        {
            return PartialView(id);
        }
        #endregion 广告
    }
}