using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{
    public class ApiResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 状态码，对应枚举 APIErrCode
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 系统信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 业务层结果
        /// </summary>
        public dynamic result { get; set; }
    }
}
