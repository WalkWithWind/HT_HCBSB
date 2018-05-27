
namespace HT.Model.Model
{
  public  class WXApiResult
    {
        /// <summary>
        /// 错误码
        /// 0 表示成功
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }
    }
}
