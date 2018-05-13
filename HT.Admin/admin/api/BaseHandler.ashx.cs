using HT.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HT.Utility;

namespace HT.Admin.admin.api
{
    /// <summary>
    /// BaseHandler 的摘要说明
    /// </summary>
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {

        public ApiResponse apiResp = new ApiResponse();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Expires = 0;
            //if (!bllUser.IsLogin)
            //{
            //    apiResp.code = (int)APIErrCode.UserIsNotLogin;
            //    apiResp.msg = "请先登录";
            //    context.Response.Write(ZentCloud.Common.JSONHelper.ObjectToJson(apiResp));
            //    return;
            //}
            //else
            //{
            //    CurrentUserInfo = bllUser.GetCurrentUserInfo();
            //}
            try
            {
                this.GetType().GetMethod("ProcessRequest").Invoke(this, new[] { context });
            }
            catch (Exception ex)
            {
                //apiResp.code = (int)APIErrCode.OperateFail;
                if (ex.InnerException != null)
                {
                    apiResp.msg = ex.InnerException.Message;
                }
                else
                {
                    apiResp.msg = ex.Message;
                }
                context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}