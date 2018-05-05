namespace HT.Utility
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Add(string key, object value);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间,以分钟为单位</param>
        void Add(string key, object value, int expire);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object Get(string key);
    }
}
