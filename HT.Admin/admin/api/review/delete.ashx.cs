using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.review
{
    /// <summary>
    /// delete 的摘要说明
    /// </summary>
    public class delete : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ids = context.Request["ids"];
            apiResp.status = HT.BLL.Admin.BLLReview.Delete(ids);
            if (apiResp.status)
            {
                apiResp.msg = "ok";
            }
            else
            {
                apiResp.msg = "删除失败";
            }
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}