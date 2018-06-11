using System;

namespace MicroMessenger
{

    /// <summary>
    /// MD5 签名类
    /// </summary>
    public class MD5SignUtil
    {
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="content">要加密的内容</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static String Sign(String content, String key)
        {
            String signStr = "";
            signStr = content + "&key=" + key;
            return MD5Util.GetMD5(signStr, "UTF-8").ToUpper();

        }


        /// <summary>
        /// 通知验签
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="sign">签名</param>
        /// <param name="parnerKey">密钥</param>
        /// <returns></returns>
        public static bool VerifySignature(String content, String sign,
                String parnerKey)
        {
            String signStr = content + "&key=" + parnerKey;
            String calculateSign = MD5Util.GetMD5(signStr,"UTF-8").ToUpper();
            String tenpaySign = sign.ToUpper();
            return (calculateSign == tenpaySign);
        }
    }

}
