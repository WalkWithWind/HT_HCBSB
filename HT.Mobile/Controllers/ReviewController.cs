using HT.BLL;
using HT.Model;
using HT.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class ReviewController : BaseController
    {

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        #region 留言 回复 Api
        /// <summary>
        /// 留言
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="reviewType"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddReview(ht_review review)
        {
            Model.ht_review model = new Model.ht_review();


            AuthenticationUser loginInfo = HT.BLL.BLLUser.GetLoginUserInfo();

            if (loginInfo == null)
            {
                apiResp.msg = "请先登录";
                apiResp.code = (int)HT.Model.Enum.APIErrCode.UserIsNotLogin;
                return Json(apiResp);
            }

            model.avatar = loginInfo.avatar;
            model.userid = loginInfo.id;
            model.nickname = loginInfo.nickname;
            model.status = 0;
            model.add_time = DateTime.Now;
            model.review_content = review.review_content;
            model.news_id = review.news_id;
            model.review_type = review.review_type;
            model.review_id = review.review_id;
            if (HT.BLL.BLLReview.AddReview(model) > 0)
            {
                apiResp.status = true;
                apiResp.result = model;
                apiResp.msg = "评论完成";
            }
            else
            {
                apiResp.msg = "评论出错";
                apiResp.code = (int)HT.Model.Enum.APIErrCode.OperateFail;
            }
            return Json(apiResp);
        }
        /// <summary>
        /// 留言列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>

        public ActionResult ReviewList(int page,int rows,HT.Model.ht_review searchKey)
        {
            Model.Model.PageResult<Model.ht_review> pageModel = BLLReview.GetReviewList(page, rows, searchKey);
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = pageModel;
                return Json(apiResp);
            }
            return View(pageModel);
        }

        #endregion
    }
}