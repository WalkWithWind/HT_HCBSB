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

        #region 信息基础接口 Api
        /// <summary>
        /// 信息列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult BaseNewsList(ht_news searchKey, int page = 1, int rows = 5)
        {
            Model.Model.PageResult<ht_news> pageModel = BLLNews.GetNewsListPageResult(page, rows, searchKey);

            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = pageModel;
                return Json(apiResp);
            }
            return View(pageModel);
        }
        /// <summary>
        /// 获取信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BaseNewsDetails(int id)
        {
            HT.Model.ht_news model = BLLNews.GetNewsDetails(id);
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(model);
        }
        #endregion 信息基础接口 Api

        #region 发布中心
        /// <summary>
        /// 发布中心
        /// </summary>
        /// <returns></returns>
        public ActionResult PostMessage()
        {
            return View();
        }
        #endregion 发布中心

        #region 货源信息
        /// <summary>
        /// 货源列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GoodsSource(ht_news searchKey,int page=1,int rows=5)
        {
            if(searchKey.cateid==0) searchKey.cateid = 1;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 货源详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GoodsSourceDetails(int id)
        {
            HT.Model.ht_news model = BLLNews.GetNewsDetails(id);

            ViewBag.YouLikes = BLLNews.GetYouLikeNewsList(1, 3, model, 1);

            return View(model);
        }

        #endregion 货源信息

    }
}