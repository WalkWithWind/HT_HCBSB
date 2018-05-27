using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{
    /// <summary>
    /// AccessToken 实体
    /// </summary>
    public class WXAccessToken
    {
        /// <summary>
        /// 错误码 0表示成功 其它表示失败
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// AccessToken
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 过期时间 单位(秒)
        /// </summary>
        public int expires_in { get; set; }
    }
}
