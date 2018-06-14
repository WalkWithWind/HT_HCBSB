using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.project
{
    /// <summary>
    /// check 的摘要说明
    /// </summary>
    public class check : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int count = HT.BLL.Admin.BLLProject.CheckExpire();
            apiResp.msg = "操作" + count + "条项目为已过期";
            apiResp.status = true;
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }

    }
}