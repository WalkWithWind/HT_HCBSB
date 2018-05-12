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
        /// <summary>
        /// 发布货源信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PostGoods()
        {
            return View();
        }
        /// <summary>
        /// 发布车源信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PostCars()
        {
            return View();
        }
        /// <summary>
        /// 发布招聘司机
        /// </summary>
        /// <returns></returns>
        public ActionResult PostRecruit()
        {
            return View();
        }
        /// <summary>
        /// 发布司机求职
        /// </summary>
        /// <returns></returns>
        public ActionResult PostJob()
        {
            return View();
        }
        /// <summary>
        /// 发布车辆出售
        /// </summary>
        /// <returns></returns>
        public ActionResult PostCarSell()
        {
            return View();
        }
        /// <summary>
        /// 发布车辆求购
        /// </summary>
        /// <returns></returns>
        public ActionResult PostCarBuy()
        {
            return View();
        }
        /// <summary>
        /// 发布通用模板
        /// </summary>
        /// <returns></returns>
        public ActionResult PostTemplate()
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
            if(model!=null)
                ViewBag.YouLikes = BLLNews.GetYouLikeNewsList(1, 3, model, 1);

            return View(model);
        }
        #endregion 货源信息

        #region 车源信息
        /// <summary>
        /// 车源列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult CarSource(ht_news searchKey, int page = 1, int rows = 5)
        {
            if (searchKey.cateid == 0) searchKey.cateid = 2;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 车源详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarSourceDetails(int id)
        {
            return GoodsSourceDetails(id);
        }
        #endregion 车源信息

        #region 招聘信息
        /// <summary>
        /// 招聘列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult Recruit(ht_news searchKey, int page = 1, int rows = 5)
        {
            if (searchKey.cateid == 0) searchKey.cateid = 3;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 招聘详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RecruitDetails(int id)
        {
            return GoodsSourceDetails(id);
        }
        #endregion 招聘信息

        #region 求职信息
        /// <summary>
        /// 求职列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult Job(ht_news searchKey, int page = 1, int rows = 5)
        {
            if (searchKey.cateid == 0) searchKey.cateid = 4;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 求职详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult JobDetails(int id)
        {
            return GoodsSourceDetails(id);
        }
        #endregion 求职信息

        #region 车辆出售信息
        /// <summary>
        /// 出售列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult CarSell(ht_news searchKey, int page = 1, int rows = 5)
        {
            if (searchKey.cateid == 0) searchKey.cateid = 5;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 出售详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarSellDetails(int id)
        {
            return GoodsSourceDetails(id);
        }
        #endregion 出售信息

        #region 车辆求购信息
        /// <summary>
        /// 求购列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult CarBuy(ht_news searchKey, int page = 1, int rows = 5)
        {
            if (searchKey.cateid == 0) searchKey.cateid = 6;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 求购详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarBuyDetails(int id)
        {
            return GoodsSourceDetails(id);
        }
        #endregion 求购信息

        #region 通用模板
        /// <summary>
        /// 通用模板列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult Template(ht_news searchKey, int page = 1, int rows = 5)
        {
            if (searchKey.cateid == 0) searchKey.cateid = 7;
            return BaseNewsList(searchKey, page, rows);
        }
        /// <summary>
        /// 通用模板详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TemplateDetails(int id)
        {
            return GoodsSourceDetails(id);
        }
        #endregion 通用模板
    }
}