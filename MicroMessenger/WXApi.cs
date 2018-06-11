using HT.Model.Model;
using HT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MicroMessenger
{
    public class WXApi
    {
        /// <summary>
        /// 获取微信JSAPI支付对象
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="totalAmount">订单金额</param>
        /// <param name="appId">微信公众号AppId</param>
        /// <param name="mchId">微信商户号</param>
        /// <param name="key">支付密钥</param>
        /// <param name="openId">用户OpenId</param>
        /// <param name="ip">用户的IP</param>
        /// <param name="notifyUrl">通知地址</param>
        /// <param name="body">订单内容</param>
        /// <param name="tradeType">交易类型</param>
        /// <returns></returns>
        public static WXPayRequest GetBrandWcPayRequest(string orderId, decimal totalAmount, string appId, string mchId, string key, string openId, string ip, string notifyUrl, out bool isSuccess, string body = "", string tradeType = "")
        {
            isSuccess = false;
            string backStr = "";
            try
            {

                string nonStr = CommonUtil.CreateNoncestr();//随机串
                #region 获取微信支付预支付ID
                //第一次签名
                SortedDictionary<string, string> dicStep1 = new SortedDictionary<string, string>();
                dicStep1.Add("appid", appId);
                dicStep1.Add("body", !string.IsNullOrEmpty(body) ? body : string.Format("订单号:{0}", orderId));
                dicStep1.Add("mch_id", mchId);
                dicStep1.Add("nonce_str", nonStr);
                dicStep1.Add("out_trade_no", orderId);
                dicStep1.Add("openid", openId);
                dicStep1.Add("spbill_create_ip", ip);
                dicStep1.Add("total_fee", (totalAmount * 100).ToString("F0"));
                dicStep1.Add("notify_url", notifyUrl);
                if (!string.IsNullOrEmpty(tradeType))
                {
                    dicStep1.Add("trade_type", tradeType);
                }
                else
                {
                    dicStep1.Add("trade_type", "JSAPI");
                }
                string strTemp1 = CommonUtil.FormatBizQueryParaMap(dicStep1, false);
                string sign = MD5SignUtil.Sign(strTemp1, key);
                //dicStep1 = (from entry in dicStep1
                //            orderby entry.Key ascending
                //            select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
                dicStep1.Add("sign", sign);
                string postData = CommonUtil.ArrayToXml(dicStep1);
                string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(postData);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = requestBytes.Length;
                System.IO.Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
                backStr = sr.ReadToEnd();
                sr.Close();
                res.Close();
                var result = System.Xml.Linq.XDocument.Parse(backStr);
                var returnCode = result.Element("xml").Element("return_code").Value;
                if (returnCode.ToUpper()=="FAIL")
                {
                    return null;
                }
                string preId = "";
                var rusultCode = result.Element("xml").Element("result_code").Value;

                if (returnCode.ToUpper().Equals("SUCCESS") && (rusultCode.ToUpper().Equals("SUCCESS")))
                {
                    preId = result.Element("xml").Element("prepay_id").Value;
                }
                #endregion
                #region 生成微信支付请求
                WXPayRequest wxPayReq = new WXPayRequest();
                string timesStamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
                wxPayReq.appId = appId;
                wxPayReq.nonceStr = nonStr;
                wxPayReq.package = "prepay_id=" + preId;
                wxPayReq.signType = "MD5";
                wxPayReq.timeStamp = timesStamp;
                //第二次签名
                SortedDictionary<string, string> dicStep2 = new SortedDictionary<string, string>();
                dicStep2.Add("appId", wxPayReq.appId);
                dicStep2.Add("timeStamp", wxPayReq.timeStamp);
                dicStep2.Add("nonceStr", wxPayReq.nonceStr);
                dicStep2.Add("package", wxPayReq.package);
                dicStep2.Add("signType", "MD5");
                string strTemp2 = CommonUtil.FormatQueryParaMap(dicStep2);
                string paySign = MD5SignUtil.Sign(strTemp2, key);
                wxPayReq.paySign = paySign;
                isSuccess = true;
                return wxPayReq;
                #endregion
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"E:\WXPay\Error.txt", true, Encoding.GetEncoding("UTF-8")))
                {
                    sw.WriteLine(backStr);
                }
                //return System.Xml.Linq.XDocument.Parse(backStr).Element("xml").Element("return_msg").Value;
                return null;

            }
        }


        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="accessToken">AccessToken</param>
        /// <param name="templateMsg">模板消息</param>
        /// <returns></returns>
        public static bool SendTemplateMessage(string accessToken, TemplateMessage templateMsg)
        {
            //官方文档 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277
            return SendTemplateMessage(accessToken, JSONHelper.ObjectToJson(templateMsg));
        }
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="acceccToken">AccessToken</param>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        private static bool SendTemplateMessage(string acceccToken, string json)
        {
            string resultJson = RequestUtil.Post(string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", acceccToken), json);
            WXApiResult result = JSONHelper.JsonToObject<WXApiResult>(resultJson);
            return result.errcode == 0;
        }



    }
}
