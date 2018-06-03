using HT.BLL;
using HT.Model.Enum;
using HT.Model.Model;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Filter
{
    public class CheckFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuthenticationUser curUser = BLLAuthentication.GetAuthenticationUser();
            if (string.IsNullOrWhiteSpace(curUser.mobile))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var json = JsonConvert.SerializeObject(new ApiResponse
                    {
                        code = (int)APIErrCode.MobileIsNull,
                        msg = "信息未完善",
                        status = false
                    });
                    filterContext.HttpContext.Response.Write(json);
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    string currentUrl = filterContext.HttpContext.Request.Url.PathAndQuery.ToString();//当前绝对地址
                    //访问授权链接
                    filterContext.HttpContext.Response.Redirect("/User/Mobile?url=" + HttpUtility.UrlEncode(currentUrl));
                }
            }
        }
    }
}