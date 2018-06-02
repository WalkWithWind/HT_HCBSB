using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMessenger
{
    /// <summary>
    /// 微信支付请求对象
    /// </summary>
   public class WXPayRequest
    {
        /// <summary>
        /// 公众号 appid
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 数据包
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 签名方式 MD5
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        /// 支付签名
        /// </summary>
        public string paySign { get; set; }
        /// <summary>
        /// 支付微信地址
        /// </summary>
        public string codeUrl { get; set; }
    }
}
