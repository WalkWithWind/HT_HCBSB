using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.user
{
    /// <summary>
    /// disable 的摘要说明
    /// </summary>
    public class disable : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ids = context.Request["ids"];
            string disable = context.Request["disable"];
            List<int> intArray = ids.Split(',').Select(p => Convert.ToInt32(p)).ToList();
            int count = HT.BLL.Admin.BLLUser.DisableUser(intArray,int.Parse(disable));
            if (count > 0)
            {
                apiResp.status = true;
                apiResp.msg = "ok";
            }
            else
            {
                apiResp.msg = "禁用用户出错";
            }
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }
    }
}