using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HT.API.Models
{
    /// <summary>
    /// APP接口统一标准模型
    /// </summary>
    [DataContract(Name = "ModelResponse")]
    public class ApiResult<T> where T : class, new()
    {
        /// <summary>
        /// 接口状态: 0.成功 1.失败
        /// </summary>
        [DataMember(Name = "ErrCode")]
        public int Status { get; set; }

        /// <summary>
        /// 错误提示消息
        /// </summary>
        [DataMember(Name = "ErrMsg")]
        public string Message { get; set; }

        /// <summary>
        /// 实体模型
        /// </summary>
        [DataMember(Name = "Response")]
        public T Result { get; set; }

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public ApiResult()
        {
            Status = 1;
            Message = "";
            Result = new T();
        }
    }
}