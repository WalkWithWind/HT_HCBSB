using HT.Model;
using HT.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HT.Model.Enum;
using HT.Model.Model;
using HT.Mobile.Filter;

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
            if (searchKey.isme.HasValue && searchKey.isme.Value) searchKey.add_userid = BLLAuthentication.GetAuthenticationUser().id; //我的发布
            Model.Model.PageResult<ht_news> pageModel = BLLNews.GetNewsListPageResult(page, rows, searchKey);
            foreach (var item in pageModel.list)
            {
                item.is_praise = BLLRelation.IsExistRelation(item.id.ToString(),BLLUser.GetUserId().ToString(), "praise");
            }
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
            if (model != null)
            {
                model.is_praise = BLLRelation.IsExistRelation(model.id.ToString(),BLLUser.GetUserId().ToString(), "praise");
            }
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(model);
        }
        /// <summary>
        /// 猜你喜欢
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult BaseLikeNewsList(int page = 1, int rows = 5, int id=0, int min=1)
        {
            List<ht_news> list = BLLNews.GetLikeNewsList(page, rows, id, min);

            //foreach (var item in list)
            //{
            //    item.is_praise = BLLRelation.IsExistRelation(item.id.ToString(), BLLUser.GetUserId().ToString(), "praise");
            //}
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = list;
                return Json(apiResp);
            }

            return View(list);
        }
        /// <summary>
        /// 获取发布须知内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BaseHelpDetails(int id)
        {
            var model = BLL.BLLHelp.GetHelp(id);
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
            ViewBag.FootActive = 2;
            return View();
        }
        /// <summary>
        /// 发布货源信息
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostGoods()
        {
            return View();
        }
        /// <summary>
        /// 修改发布货源信息
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostGoods(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 发布项目 货源信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
		[Authorize]
        [CheckFilter]
        public ActionResult PostSubmit(ht_news model)
		{
			string msg = "";
			string orderNo = "";
            var authenticationUser = BLLAuthentication.GetAuthenticationUser();

            model.add_userid = authenticationUser.id;
            model.add_nickname = authenticationUser.nickname;
            model.add_avatar = authenticationUser.avatar;
            if (BLLNews.Add(model, out msg, out orderNo))
			{
				return JsonResult(APIErrCode.Success, "OK", new { order_no = orderNo });
			}
			else
			{
				return JsonResult(APIErrCode.OperateFail,msg);
			}

		}
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [CheckFilter]
        public ActionResult Detail(int id)
        {
            var detail = BLLNews.Get(id);
            var authenticationUser = BLLAuthentication.GetAuthenticationUser();
            if (authenticationUser.id!=detail.add_userid)
            {
                return JsonResult(APIErrCode.OperateFail, "无权操作");
            }
            return JsonResult(APIErrCode.Success, "OK",detail );

        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostDel(int id)
        {
            if (BLLNews.DelNews(id)>0)
            {
                return JsonResult(APIErrCode.Success, "删除成功");
            }
            else
            {
                return JsonResult(APIErrCode.OperateFail, "删除失败");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ht_news"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ht_news model)
        {
            if (BLLNews.Edit(model))
            {
                return JsonResult(APIErrCode.Success, "编辑成功");
            }
            else
            {
                return JsonResult(APIErrCode.OperateFail, "编辑失败");
            }
        }


        /// <summary>
        /// 发布车源信息
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostCars()
        {
            return View();
        }
        /// <summary>
        /// 修改发布车源信息
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostCars(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        /// <summary>
        /// 发布招聘司机
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostRecruit()
        {
            return View();
        }
        /// <summary>
        /// 修改发布招聘司机
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostRecruit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        /// <summary>
        /// 发布司机求职
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostJob()
        {
            return View();
        }
        /// <summary>
        ///  修改发布司机求职
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostJob(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        /// <summary>
        /// 发布车辆出售
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostCarSell()
        {
            return View();
        }
        /// <summary>
        /// 修改发布车辆出售
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostCarSell(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 发布车辆求购
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostCarBuy()
        {
            return View();
        }
        /// <summary>
        /// 修改发布车辆求购
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostCarBuy(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        /// <summary>
        /// 发布通用模板
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult PostTemplate()
        {
            return View();
        }
        /// <summary>
        /// 修改发布通用模板
        /// </summary>
        /// <returns></returns>
        [CheckFilter]
        public ActionResult EditPostTemplate(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        #endregion 发布中心

        #region 货源信息
        /// <summary>
        /// 货源列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsSource()
        {
            ViewBag.FootActive = 1;
            return View();
        }
        /// <summary>
        /// 货源详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsSourceDetails()
        {
            return View();
        }
        #endregion 货源信息

        #region 车源信息
        /// <summary>
        /// 车源列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CarSource()
        {
            return View();
        }
        /// <summary>
        /// 车源详情
        /// </summary>
        /// <returns></returns>
        public ActionResult CarSourceDetails()
        {
            return View();
        }
        #endregion 车源信息

        #region 招聘信息
        /// <summary>
        /// 招聘列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Recruit()
        {
            return View();
        }
        /// <summary>
        /// 招聘详情
        /// </summary>
        /// <returns></returns>
        public ActionResult RecruitDetails()
        {
            return View();
        }
        #endregion 招聘信息

        #region 求职信息
        /// <summary>
        /// 求职列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Job()
        {
            return View();
        }
        /// <summary>
        /// 求职详情
        /// </summary>
        /// <returns></returns>
        public ActionResult JobDetails()
        {
            return View();
        }
        #endregion 求职信息

        #region 车辆出售信息
        /// <summary>
        /// 出售列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CarSell()
        {
            ViewBag.FootActive = 3;
            return View();
        }
        /// <summary>
        /// 出售详情
        /// </summary>
        /// <returns></returns>
        public ActionResult CarSellDetails()
        {
            return View();
        }
        #endregion 出售信息

        #region 车辆求购信息
        /// <summary>
        /// 求购列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CarBuy()
        {
            return View();
        }
        /// <summary>
        /// 求购详情
        /// </summary>
        /// <returns></returns>
        public ActionResult CarBuyDetails()
        {
            return View();
        }
        #endregion 求购信息

        #region 通用模板
        /// <summary>
        /// 通用模板列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Template()
        {
            return View();
        }
        /// <summary>
        /// 通用模板详情
        /// </summary>
        /// <returns></returns>
        public ActionResult TemplateDetails()
        {
            return View();
        }
        #endregion 通用模板
    }
}