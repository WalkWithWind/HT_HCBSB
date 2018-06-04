using HT.Model;
using System;
using Newtonsoft.Json;
using HT.Model.Model;
using System.Web.Security;
using System.Web;

namespace HT.BLL
{
    public class BLLAuthentication
    {
        /// <summary>
        /// 生成form身份验证票证
        /// </summary>
        /// <param name="user">用户信息</param>
        public static void LoginAuthenticationTicket(ht_user user)
        {
            var userData = JsonConvert.SerializeObject(new AuthenticationUser() {
                id = user.id,
                openid = user.openid,
                nickname = user.nickname,
                mobile = user.mobile,
                avatar = user.avatar
            });
            var expires = DateTime.Now.AddHours(2);
            var ticket = new FormsAuthenticationTicket(1, user.openid, DateTime.Now, expires, true, userData);
            // 加密
            var hashTicket = FormsAuthentication.Encrypt(ticket);
            // 生成cookie 
            var userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            userCookie.Expires = expires;
            // 身份验证票据Cookie输出到客户端 
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static AuthenticationUser GetAuthenticationUser()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return null;
            var userData = FormsAuthentication.Decrypt(cookie.Value).UserData;
            return JsonConvert.DeserializeObject<AuthenticationUser>(userData);
        }
    }
}
