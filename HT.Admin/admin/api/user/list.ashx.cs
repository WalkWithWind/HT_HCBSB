using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.user
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
			var pageResult = HT.BLL.Admin.BLLUser.GetUsersList(pageIndex, pageSize, keyword);

            apiResp.status = true;
            apiResp.result = pageResult;
            apiResp.msg = "查询完成";

            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));

        }

       
    }
}