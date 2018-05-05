using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HT.Model;

namespace HT.API.Models
{
    /// <summary>
    /// 公共处理
    /// </summary>
    public sealed class XUtil
    {

        /// <summary>
        /// 获取显示的名称
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public static string ShowName(ht_user user)
        {
            string uname = (user.usertype == 2 || user.usertype == 4) ? user.company : user.nickname;
            return !string.IsNullOrEmpty(uname)
                ? uname
                : (!string.IsNullOrEmpty(user.realname) ? user.realname : user.mobile);
        }

        /// <summary>
        /// 显示头像处理
        /// </summary>
        /// <param name="Avatar"></param>
        /// <returns></returns>
        public static string ShowAvatar(string Avatar)
        {
            if (string.IsNullOrWhiteSpace(Avatar))
                return "/images/img/default_avatar.png";
            return Avatar;
        }

        /// <summary>
        /// 显示认证状态
        /// </summary>
        /// <param name="stauts"></param>
        /// <returns></returns>
        public static string ShowCertifiedStauts(int stauts)
        {
            if (stauts == 1) return "未认证";
            if (stauts == 2) return "待审核";
            if (stauts == 3) return "已认证";
            return "";
        }
    }
}