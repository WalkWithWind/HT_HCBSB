using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{

    /// <summary>
    /// 微信AccessToken实体
    /// </summary>
    [Serializable]
    public class WXOAuthAccessToken
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔 snsapi_base snsapi_userinfo
        /// </summary>
        public string scope { get; set; }

        /// <summary>
        /// UnionId 只有绑定微信开放平台时才会有值
        /// </summary>
        public string unionid { get; set; }

    }
}
