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
    public class RelationController : BaseController
    {
        // GET: Relation
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="news_id"></param>
        /// <returns></returns>
        public ActionResult AddRelation(ht_comm_relation relation)
        {
            ht_comm_relation model = new ht_comm_relation();

            AuthenticationUser loginInfo = BLLUser.GetLoginUserInfo();

            if (loginInfo == null)
            {
                apiResp.msg = "请先登录";
                apiResp.code = (int)HT.Model.Enum.APIErrCode.UserIsNotLogin;
                return Json(apiResp);
            }
            model.add_time = DateTime.Now;
            model.relation_type = "praise";
            model.main_id = relation.main_id;
            model.relation_id = loginInfo.id.ToString();
            if (BLLNews.AddPraise(int.Parse(relation.main_id), model)> 0)
            {
                apiResp.status = true;
                apiResp.msg = "点赞成功";
            }
            else
            {
                apiResp.msg = "点赞出错";
                apiResp.code = (int)HT.Model.Enum.APIErrCode.OperateFail;
            }
            return Json(apiResp);
        }
        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public ActionResult DeleteRelation(ht_comm_relation relation)
        {
            relation.relation_type = "praise";
            relation.relation_id = BLLUser.GetUserId().ToString();
            if (BLLNews.DeletePraise(int.Parse(relation.main_id), relation)>0)
            {
                apiResp.msg = "取消点赞成功";
                apiResp.status = true;
            }
            else
            {
                apiResp.msg = "取消点赞出错";
                apiResp.code = (int)HT.Model.Enum.APIErrCode.OperateFail;
            }
            return Json(apiResp);
        }


    }
}