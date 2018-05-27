namespace HT.Model.Model
{
    /// <summary>
    /// 模板消息
    /// </summary>
    public class TemplateMessage
    {
        /// <summary>
        /// 用户OpenId
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 模板数据
        /// </summary>
        public dynamic data { get; set; }
        /// <summary>
        ///模板消息
        /// </summary>
        /// <param name="openId">用户OpenId</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">跳转链接</param>
        /// <param name="data">模板数据</param>
        public TemplateMessage(string openId, string templateId, string url, dynamic data = null)
        {

            this.touser = openId;
            this.template_id = templateId;
            this.url = url;
            this.data = data;
        }
        /// <summary>
        /// 模板消息
        /// </summary>
        public TemplateMessage()
        {

        }


    }
}
