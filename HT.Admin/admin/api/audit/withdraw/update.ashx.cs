using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.audit.withdraw
{
    /// <summary>
    /// update 的摘要说明
    /// </summary>
    public class update : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ids = context.Request["ids"];
            int status = int.Parse(context.Request["status"]);
            List<int> intArray = ids.Split(',').Select(p => Convert.ToInt32(p)).ToList();
            apiResp.status = HT.BLL.Admin.BLLUser.UpdateStatus(intArray, status);
            if (apiResp.status)
            {
                apiResp.msg = "ok";
                apiResp.status = true;
            }
            else
            {
                apiResp.msg = "操作失败";
            }
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }


    }
}