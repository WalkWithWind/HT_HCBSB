using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.review
{
    /// <summary>
    /// list 的摘要说明
    /// </summary>
    public class list : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = !string.IsNullOrEmpty(context.Request["pageindex"]) ? int.Parse(context.Request["pageindex"]) : 1;
            int pageSize = !string.IsNullOrEmpty(context.Request["pagesize"]) ? int.Parse(context.Request["pagesize"]) : 10;
            string keyword = context.Request["keyword"];
            string news_id = context.Request["news_id"];
            string type = context.Request["type"];
            string status = context.Request["status"];
            string review_id = context.Request["review_id"];
            var pageResult = HT.BLL.Admin.BLLReview.GetReviewsList(pageIndex, pageSize, news_id, type, status, keyword, review_id);

            apiResp.status = true;
            apiResp.result = pageResult;
            apiResp.msg = "查询完成";

            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}