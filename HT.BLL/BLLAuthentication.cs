using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                avatar = user.avatar
            });
            var expires = DateTime.Now.AddMinutes(30);
            var ticket = new FormsAuthenticationTicket(1, user.username, DateTime.Now, expires, true, userData);
            // 加密
            var hashTicket = FormsAuthentication.Encrypt(ticket);
            // 生成cookie 
            var userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            userCookie.Expires = expires;
            // 身份验证票据Cookie输出到客户端 
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
    }
}
