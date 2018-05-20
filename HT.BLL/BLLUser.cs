using HT.Model;
using HT.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HT.BLL
{
    public class BLLUser
    {
        public static object SerializerToJson { get; private set; }

        /// <summary>
        /// 根据openid获取用户信息
        /// </summary>
        /// <param name="openid">微信OpenId</param>
        /// <returns></returns>
        public static ht_user GetUserByOpenid(string openid)
        {
            using (Entities db = new Entities())
            {
                return db.ht_user.FirstOrDefault(p => p.openid == openid);
            }
        }
        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public static ht_user GetUserById(int id)
        {
            using (Entities db = new Entities())
            {
                return db.ht_user.FirstOrDefault(p => p.id == id);
            }
        }

        /// <summary>
        /// 新增账号
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static int AddUser(ht_user user)
        {
            using (Entities db = new Entities())
            {
                db.ht_user.Add(user);
                db.SaveChanges();

                return user.id;
            }
        }
        /// <summary>
        /// 完善手机和姓名
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="phone">手机</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public static bool EditUserPhoneAndName(int id,string phone,string name)
        {
            using (Entities db = new Entities())
            {
                var user = db.ht_user.FirstOrDefault(p => p.id == id);
                user.mobile = phone;
                user.realname = name;
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// 获取当前登录用户的信息,从Form票证中获得
        /// </summary>
        /// <returns></returns>
        public static AuthenticationUser GetLoginUserInfo()
        {
            if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                return null;
            }
            var cookieValue = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value;//加密的cookie票据
            var userDataSource = FormsAuthentication.Decrypt(cookieValue).UserData;
            return JsonConvert.DeserializeObject<AuthenticationUser>(userDataSource);
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            AuthenticationUser loginInfo = HT.BLL.BLLUser.GetLoginUserInfo();
            if (loginInfo == null)
            {
                return false;
            }
            return true;
        }


    }
}
