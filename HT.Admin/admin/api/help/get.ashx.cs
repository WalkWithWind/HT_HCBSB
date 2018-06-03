using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.help
{
    /// <summary>
    /// get 的摘要说明
    /// </summary>
    public class get : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request["id"];

            int idInt = 0;

            if(!int.TryParse(id,out idInt))
            {
                apiResp.msg = "参数不正确";
                apiResp.status = true;
                context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
                return;
            }
            apiResp.result = BLL.Admin.BLLHelp.GetHelp(idInt);
            apiResp.status = true;
            apiResp.msg = "查询完成";
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}