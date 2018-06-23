using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.user
{
    /// <summary>
    /// delete 的摘要说明
    /// </summary>
    public class delete : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ids = context.Request["ids"];
            List<int> intArray = ids.Split(',').Select(p => Convert.ToInt32(p)).ToList();
            int count = HT.BLL.Admin.BLLUser.DeleteUser(intArray);
            if (count > 0)
            {
                apiResp.status = true;
                apiResp.msg = "ok";
            }
            else
            {
                apiResp.msg = "删除用户出错";
            }
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}