using HT.Model;
using HT.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 发布中心
        /// </summary>
        /// <returns></returns>
        public ActionResult PostMessage()
        {
            return View();
        }
        //货源列表
        public ActionResult GoodsSource(ht_news searchKey,int page=1,int rows=5)
        {
            Model.Model.PageResult<ht_news> pageModel = BLLNews.GetNewsListPageResult(page, rows, searchKey);
            if (Request.IsAjaxRequest())
            {

            }
            return View(pageModel);
        }
        //货源详情
        public ActionResult GoodsSourceDetails()
        {
            return View();
        }
        

    }
}