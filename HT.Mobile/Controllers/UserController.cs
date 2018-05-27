using HT.BLL;
using HT.Model;
using HT.Model.Enum;
using HT.Utility;
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
            var details = BLLNews.GetNewsDetailsByOrderNo(id);
            if(details.pay_status == 1) //已支付
            {
                return PayResult(id);
            }
            int user_id = BLLAuthentication.GetAuthenticationUser().id;
            var user = BLLUser.GetUserById(user_id);
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
        public ActionResult PayResult(string id)
        {
            var details = BLLNews.GetNewsDetailsByOrderNo(id);
            return View(details.pay_status);
        }
        /// <summary>
        /// 完善信息
        /// </summary>
        /// <param name="id">目标页</param>
        /// <returns></returns>
        public ActionResult Mobile(string url)
        {
            var authenticationUser = BLLAuthentication.GetAuthenticationUser();
            ViewBag.Url = url;
            return View(authenticationUser);
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
        /// <summary>
        /// 获取验证码 5分钟
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCode(string mobile)
        {
            if(!MyRegex.IsPhone(mobile)) return JsonResult(APIErrCode.PhoneFormatError, "手机格式错误");
            var authenticationUser = BLLAuthentication.GetAuthenticationUser();
            var code =  HT.Utility.Utils.Number(6);
            new XCache().Add("Code"+ authenticationUser.openid, code, 5);//写入缓存
            return JsonResult(APIErrCode.Success, "获取验证码成功", code);
        }
        /// <summary>
        /// 完善手机
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult PostMobile(string mobile,string code)
        {
            if (!MyRegex.IsPhone(mobile)) return JsonResult(APIErrCode.PhoneFormatError, "手机格式错误");

            var authenticationUser = BLLAuthentication.GetAuthenticationUser();
            var obj = new XCache().Get("Code" + authenticationUser.openid);//写入缓存
            if(obj == null) return JsonResult(APIErrCode.CheckCodeErr, "验证码已过期");
            if(obj.ToString().ToUpper() != code.Trim().ToUpper()) return JsonResult(APIErrCode.CheckCodeErr, "验证码错误");
            ht_user user = BLLUser.GetUserByOpenid(authenticationUser.openid);
            if (user == null)
            {
                user = new ht_user();
                user.username = user.openid;
                user.openid = authenticationUser.openid;
                user.salt = Utils.GetSalt();
                user.password = EncryptUtil.DesEncrypt("123456", user.salt);
                user.points = 0;
                user.money = 0;
                if (authenticationUser.parent_id.HasValue)
                {
                    user.parent_id = authenticationUser.parent_id;
                    ht_user parentUser = BLLUser.GetUserById(authenticationUser.parent_id.Value);
                    if (parentUser != null && parentUser.parent_id.HasValue)
                    {
                        user.pparent_id = parentUser.parent_id;
                    }
                }
            }
            user.mobile = mobile;
            user.avatar = authenticationUser.avatar;
            user.nickname = authenticationUser.nickname;
            if (BLLUser.PostUser(user) > 0) {
                BLLAuthentication.LoginAuthenticationTicket(user);
                return JsonResult(APIErrCode.Success, "提交成功");
            }
            return JsonResult(APIErrCode.CheckCodeErr, "提交失败");
        }
        /// <summary>
        /// 余额支付提交
        /// </summary>
        /// <returns></returns>
        public ActionResult PostMoneyPay(string order_no)
        {
            string msg = "";
            if (BLLNews.PayNews(order_no, "余额", "",out msg) ==0) return JsonResult(APIErrCode.OperateFail, msg);
            return JsonResult(APIErrCode.Success, "支付完成");
        }
        /// <summary>
        /// 我的团队
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult TeamList(int page,int rows)
        {
            if (Request.IsAjaxRequest())
            {
                int total = 0;
                decimal totalMoney = 0;
                int totalPeopleNum = 0;
                List<ht_commission> teamlist = BLLDistribution.GetCommussionList(page, rows, BLLUser.GetUserId(), out total,out totalMoney,out totalPeopleNum);
                apiResp.result = new
                {
                    list = teamlist,
                    total = total,
                    total_money = totalMoney,
                    total_people_num = totalPeopleNum
                };
                apiResp.msg = "查询完成";
                apiResp.status = true;
                return Json(apiResp);
            }
            return View();
        }

        /// <summary>
        /// 团队详情
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public ActionResult TeamChildList(int page,int rows,int parentid)
        {
            if (Request.IsAjaxRequest())
            {
                int total = 0;
                var list = BLLDistribution.GetCommussionByChild(page, rows, parentid, out total); apiResp.result = new
                {
                    list = list,
                    total = total
                };
                apiResp.msg = "查询完成";
                apiResp.status = true;
                return Json(apiResp);
            }
            return View();
        }




        #endregion 接口
    }
}