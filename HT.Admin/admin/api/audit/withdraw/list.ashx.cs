using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.audit.withdraw
{
    /// <summary>
    /// list 的摘要说明
    /// </summary>
    public class list : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int page = !string.IsNullOrEmpty(context.Request["pageindex"]) ? int.Parse(context.Request["pageindex"]) : 1;
            int rows = !string.IsNullOrEmpty(context.Request["pagesize"]) ? int.Parse(context.Request["pagesize"]) : 10;
            int total = 0;
            var list = BLL.BLLUser.GetUserMoneyLogData(page,rows,0, (int)Model.Enum.UserMoneyDetails.WithDraw, out total);
            apiResp.result = new {
                list =list,
                total = total
            };
            apiResp.status = true;
            apiResp.msg = "查询完成";
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}