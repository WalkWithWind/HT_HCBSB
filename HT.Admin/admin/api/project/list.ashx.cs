using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.project
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
            string cateId = context.Request["cate_id"];
			string status = context.Request["status"];
			string fromDate = context.Request["fromdate"];
			string toDate = context.Request["todate"];
			var pageResult = HT.BLL.Admin.BLLProject.GetNewsList(pageIndex, pageSize, int.Parse(cateId),status, keyword,fromDate,toDate);

            apiResp.status = true;
            apiResp.result = pageResult;
            apiResp.msg = "查询完成";

            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));

        }

       
    }
}