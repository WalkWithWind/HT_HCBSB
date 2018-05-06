using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{
    /// <summary>
    /// 微信用户基本信息
    /// </summary>
    [Serializable]
    public class WeixinUserInfo
    {
        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public List<string> privilege { get; set; }
        /// <summary>
        /// UnionId
        /// </summary>
        public string unionid { get; set; }

        //openid	 用户的唯一标识
        //nickname	 用户昵称
        //sex	 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        //province	 用户个人资料填写的省份
        //city	 普通用户个人资料填写的城市
        //country	 国家，如中国为CN
        //headimgurl	 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        //privilege	 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
    }
}
