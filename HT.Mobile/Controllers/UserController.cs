using HT.BLL;
using HT.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class UserController : BaseController
    {

        #region 页面
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var authenticationUser = BLLAuthentication.GetAuthenticationUser();
            return View(authenticationUser);
        }
        /// <summary>
        /// 我的发布
        /// </summary>
        /// <returns></returns>
        public ActionResult Issue()
        {
            return View();
        }
        /// <summary>
        /// 我的钱包
        /// </summary>
        /// <returns></returns>
        public ActionResult Wallet()
        {
            return View();
        }
        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        public ActionResult Withdraw()
        {
            return View();
        }
        /// <summary>
        /// 提现成功
        /// </summary>
        /// <returns></returns>
        public ActionResult WithdrawSuccess()
        {
            return View();
        }
        /// <summary>
        /// 我要赚钱
        /// </summary>
        /// <returns></returns>
        public ActionResult EarnMoney()
        {
            return View();
        }
        /// <summary>
        /// 我的团队
        /// </summary>
        /// <returns></returns>
        public ActionResult Team()
        {
            return View();
        }
        /// <summary>
        /// 我的分销
        /// </summary>
        /// <returns></returns>
        public ActionResult MyDistribution()
        {
            return View();
        }
        /// <summary>
        /// 二级分销
        /// </summary>
        /// <returns></returns>
        public ActionResult SecondaryDistribution(int id)
        {
            return View();
        }
        /// <summary>
        /// 模拟登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult TestLogin(string username, string password)
        {
            if (Request.IsAjaxRequest())
            {
                var user = BLLUser.GetUserByUsername(username);
                if (user == null) return JsonResult(APIErrCode.IsNotFound, "账号未找到");
                if (user.password != Utility.EncryptUtil.DesEncrypt(password, user.salt)) return JsonResult(APIErrCode.PasswordFail, "密码错误");
                BLLAuthentication.LoginAuthenticationTicket(user);
                return JsonResult(APIErrCode.Success, "登陆成功");
            }

            return View();
        }
        /// <summary>
        /// 支付页
        /// </summary>
        /// <param name="id">因mvc路由实是order_no</param>
        /// <returns></returns>
        public ActionResult Pay(string id)
        {
            int user_id = BLLAuthentication.GetAuthenticationUser().id;
            var user = BLLUser.GetUserById(user_id);
            var details = BLLNews.GetNewsDetailsByOrderNo(id);
            ViewBag.RespUser = new Model.Model.RespUser
            {
                id = user.id,
                nickname = user.nickname,
                avatar = user.avatar,
                money = user.money.Value
            };
            return View(details);
        }
        /// <summary>
        /// 支付成功
        /// </summary>
        /// <returns></returns>
        public ActionResult PayResult(string order_no)
        {
            var details = BLLNews.GetNewsDetailsByOrderNo(order_no);
            return View(details.pay_status);
        }
        #endregion 页面
        #region 接口
        /// <summary>
        /// 获取登录人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAuthenticationUser()
        {
            var authenticationUser = BLLAuthentication.GetAuthenticationUser();
            return JsonResult(APIErrCode.Success,"获取成功",authenticationUser);
        }
        #endregion 接口
    }
}