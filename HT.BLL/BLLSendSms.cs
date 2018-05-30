using HT.Model;
using HT.Model.Const;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLSendSms
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        private static Dictionary<string, string> errDic = new Dictionary<string, string>() {
                                { "0", "提交成功"},
                                {"101", "无此用户"},
                                {"102", "密码错"},
                                {"103", "提交过快"},
                                {"104", "系统忙"},
                                {"105", "敏感短信"},
                                {"106", "消息长度错"},
                                {"107", "包含错误的手机号码"},
                                {"108", "手机号码个数错"},
                                {"109", "无发送额度"},
                                {"110", "不在发送时间内"},
                                {"111", "超出该账户当月发送额度限制"},
                                {"112", "无此产品，用户没有订购该产品"},
                                {"113", "extno格式错"},
                                {"114", "缺说明"},
                                {"115", "自动审核驳回"},
                                {"116", "签名不合法，未带签名"},
                                {"117", "IP地址认证错"},
                                {"118", "用户没有相应的发送权限"},
                                {"119", "用户已过期"},
                                {"120", "测试内容不是白名单"},
                                {"000", "未知错误"}
                            };
        /// <summary>
        /// 获取短信配置
        /// </summary>
        /// <returns></returns>
        public static ht_sms_config GetSMSConfig()
        {
            using (Entities db = new Entities())
            {
                return db.ht_sms_config.FirstOrDefault();
            }
        }
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="callname"></param>
        /// <returns></returns>
        public static string GetTemplate(string callname)
        {
            using (Entities db = new Entities())
            {
                ht_sms_template temp = db.ht_sms_template.FirstOrDefault(p => p.code == callname);
                return temp == null ? null : temp.contents;
            }
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <param name="callname"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SendMsg(string mobile,string code, string callname,int expire, out string msg)
        {
            msg = "发送成功";
            //检查是否过期
            string cookie = Utils.GetCookie(Keys.Cookie_SMS_Mobile);
            if (cookie == mobile){
                msg = "已发送过短信，请"+ expire + "分钟后再试！";
                return false;
            }
            string template = GetTemplate(callname); //取得短信内容
            if (template == null)
            {
                msg = "发送失败，短信模板不存在，请联系管理员！";
                return false;
            }
            //替换标签
            template = template.Replace("{code}", code);
            template = template.Replace("{expire}", "2");
            ht_sms_config smsConfig = GetSMSConfig();
            if (string.IsNullOrEmpty(smsConfig.smsurl) || string.IsNullOrEmpty(smsConfig.smsuser) || string.IsNullOrEmpty(smsConfig.smspwd))
            {
                msg = "短信配置参数有误，请完善后再提交！";
                return false;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("account", smsConfig.smsuser);
            dic.Add("pswd", smsConfig.smspwd);
            dic.Add("mobile", mobile);
            dic.Add("msg", template);
            dic.Add("needstatus", "true");
            string result = "";
            try
            {
                result = RequestUtil.HttpPost(smsConfig.smsurl, dic);

                var rArr = result.Split(Environment.NewLine.ToCharArray());
                string[] strArr = rArr[0].Split(',');
                if (strArr.Length < 2)
                {
                    msg = "返回值错误";
                    return false;
                }
                if (strArr[1] != "0") {
                    msg = errDic[strArr[1]];
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            Utils.WriteCookie(Keys.Cookie_SMS_Mobile, mobile, expire); //规定时间内无重复发送
            return true;
        }
    }
}
