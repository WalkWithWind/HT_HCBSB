using HT.Model;
using HT.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                return db.ht_user.Find(id);
            }
        }
        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="username">username</param>
        /// <returns></returns>
        public static ht_user GetUserByUsername(string username)
        {
            using (Entities db = new Entities())
            {
                return db.ht_user.FirstOrDefault(p => p.username == username);
            }
        }

        /// <summary>
        /// 新增或更新账号
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static int PostUser(ht_user user)
        {
            using (Entities db = new Entities())
            {
                db.ht_user.AddOrUpdate(user);
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
            AuthenticationUser loginInfo = GetLoginUserInfo();
            if (loginInfo == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            AuthenticationUser loginInfo = GetLoginUserInfo();

            if (loginInfo != null)
            {
                return loginInfo.id;
            }
            return 0;
        }

        public static List<ht_user_money_log> GetUserMoneyLogData(int page,int rows,int userId,out int total)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.ht_user_money_log.Where(p => true);
                if (userId > 0) data = data.Where(p => p.userid == userId);

                total = data.Count();
                return data.OrderByDescending(p => p.addtime).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }

        public static bool AddUserMoneyLogData(int userId,decimal money,string remark,int  type)
        {
            using (Entities db = new Entities())
            {
                ht_user_money_log model = new ht_user_money_log();
                model.addtime = DateTime.Now;
                model.userid = userId;
                model.remark = remark;
                model.money = -money;
                model.type = type;
                db.ht_user_money_log.Add(model);
                return db.SaveChanges() > 0 ? true : false;
            }
        }


        public static decimal GetToauditTotalMoney(int userId,int type,int status)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.ht_user_money_log.Where(p => true);
                data = data.Where(p => p.userid == userId);
                data = data.Where(p => p.type == type);
                data = data.Where(p => p.status == status);
                return (decimal)data.ToList().Sum(p => p.money);
            }
        }
    }
}
