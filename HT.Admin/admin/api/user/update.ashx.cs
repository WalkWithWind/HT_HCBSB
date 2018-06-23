using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HT.Admin.admin.api.user
{
    /// <summary>
    /// update 的摘要说明
    /// </summary>
    public class update : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request["id"];
            string mobile = context.Request["mobile"];
            string money = context.Request["money"];

            if (!IsPhoneNo(mobile))
            {
                apiResp.msg = "号码不合法";
                context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
                return;

            }

            int count = HT.BLL.Admin.BLLUser.UpdateUser(int.Parse(id), mobile,decimal.Parse(money));
            if (count > 0)
            {
                apiResp.status = true;
                apiResp.msg = "ok";
            }
            else
            {
                apiResp.msg = "编辑用户信息出错";
            }
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
        }

        public bool IsPhoneNo(string str_handset)
        {
            return Regex.IsMatch(str_handset, "^(0\\d{2,3}-?\\d{7,8}(-\\d{3,5}){0,1})|(((13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$");
        }
    }
}