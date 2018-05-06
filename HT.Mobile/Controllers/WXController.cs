using HT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HT.Model.Model;
using HT.BLL;
using HT.Model;
using System.Web.Security;

namespace HT.Mobile.Controllers
{
    public class WXController : Controller
    {
        // GET: WX
        public ActionResult OAuthCallback()
        {

            string code = Request["code"];
            string appId = Request["appid"];//授权给开放平台时返回此参数 默认授权无此参数
            string state = Request["state"];//state 原样返回

            string pageUrl = HttpUtility.UrlDecode(state);
            string appSecret = "";
            string getAccessTokenUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                appId,
                appSecret,
                code);
            string accessTokenSource = RequestUtil.Get(getAccessTokenUrl);
            WXOAuthAccessToken accessTokenModel = JsonConvert.DeserializeObject<WXOAuthAccessToken>(accessTokenSource);

            ht_user user = BLLUser.GetUserByOpenid(accessTokenModel.openid);
            if (user != null)
            {
                BLLAuthentication.LoginAuthenticationTicket(user);
                return Redirect(pageUrl);
            }
            else
            {
                user = new ht_user();
                user.addtime = DateTime.Now;
                user.parent_id = 0;
                user.points = 0;
                user.money = 0;
                user.issubscribe = 0;
                user.salt = Utils.GetCheckCode(6); //获得6位的salt加密字符串
                user.password = EncryptUtil.DesEncrypt("123456", user.salt);

            }
            string wxUserInfoSourceJson = RequestUtil.Get(string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}",
                        accessTokenModel.access_token,
                        accessTokenModel.openid
                    ));
            WeixinUserInfo wxUserInfo = JsonConvert.DeserializeObject<WeixinUserInfo>(wxUserInfoSourceJson);
            user.username = user.openid;
            user.openid = wxUserInfo.openid;
            user.nickname = wxUserInfo.nickname;
            user.avatar = wxUserInfo.headimgurl;
			if (BLLUser.AddUser(user)>0)
			{
				BLLAuthentication.LoginAuthenticationTicket(user);

			}
            return Redirect(pageUrl);
        }
    }
}