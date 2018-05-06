using HT.BLL;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Filter
{
    public class OAuthFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                ht_user user = BLLUser.GetUserById(1);
                BLLAuthentication.LoginAuthenticationTicket(user);
                return;
                string currentUrl =  filterContext.HttpContext.Request.Url.ToString();//当前绝对地址

                string callBackUrl = filterContext.HttpContext.Request.Url.Scheme + "://" +filterContext.HttpContext.Request.Url.Authority+ "/WX/OAuthCallback";//当前绝对地址
                var  oauthUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect",
                        "11111",
                        callBackUrl,
                        "snsapi_userinfo",
                        HttpUtility.UrlEncode(currentUrl)
                    );
                //访问授权链接
                filterContext.HttpContext.Response.Redirect(oauthUrl);
            }
        }
    }
}