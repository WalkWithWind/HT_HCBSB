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
            string status = context.Request["status"];

            int? auditStatus = null;

            if (!string.IsNullOrEmpty(status))
            {
                auditStatus = int.Parse(status);
            }
            var pageResult = HT.BLL.Admin.BLLUser.GetUserMoneyLogList(page, rows,(int)Model.Enum.UserMoneyDetails.WithDraw, auditStatus);
            foreach (var item in pageResult.list)
            {
                HT.Model.ht_user user = BLL.BLLUser.GetUserById((int)item.userid);
                if (user == null) continue;
                item.nickname = user.nickname;
                item.mobile = user.mobile;
                item.avatar = user.avatar;
            }
            apiResp.result = pageResult;
            apiResp.status = true;
            apiResp.msg = "查询完成";
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}