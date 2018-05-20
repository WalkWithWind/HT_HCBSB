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
        public ActionResult AddRelation(string news_id)
        {
            ht_comm_relation model = new ht_comm_relation();

            AuthenticationUser loginInfo = HT.BLL.BLLUser.GetLoginUserInfo();

            if (loginInfo == null)
            {
                apiResp.msg = "请先登录";
                apiResp.code = (int)HT.Model.Enum.APIErrCode.UserIsNotLogin;
                return Json(apiResp);
            }
            model.add_time = DateTime.Now;
            model.relation_type = "praise";
            model.main_id = news_id;
            model.relation_id = loginInfo.id.ToString();
            if (HT.BLL.BLLRelation.AddRelation(model) > 0)
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

    }
}