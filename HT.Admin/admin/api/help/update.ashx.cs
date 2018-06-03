using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.help
{
    /// <summary>
    /// update 的摘要说明
    /// </summary>
    public class update : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request["id"];
            string contents = context.Request["contents"];
            int idInt = 0;

            if (!int.TryParse(id, out idInt))
            {
                apiResp.msg = "参数不正确";
                apiResp.status = true;
                context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
                return;
            }

            if (BLL.Admin.BLLHelp.UpdateContents(idInt, contents))
            {
                apiResp.msg = "操作成功";
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