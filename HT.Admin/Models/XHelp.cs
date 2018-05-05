using System;
using System.Linq;
using HT.Model;

namespace HT.Admin.Models
{
    /// <summary>
    /// 后台帮助类
    /// </summary>
    public class XHelp
    {

        private readonly Entities _db = new Entities();

        #region 配置信息管理 =======================================================
        /// <summary>
        /// 获取广告分类名称
        /// </summary>
        /// <param name="code">标识</param>
        /// <returns></returns>
        public string get_ht_ad_category_title(object code)
        {
            try
            {
                var codeZhi = code.ToString();
                var model = _db.ht_ad_category.FirstOrDefault(s => s.code == codeZhi);
                if (model == null)
                {
                    return "";
                }
                return model.title;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 获取支付方式分类名称
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public string get_ht_payment_title(object id)
        {
            try
            {
                var zhi = Convert.ToInt32(id);
                var model = _db.ht_payment.FirstOrDefault(s => s.id == zhi);
                if (model == null)
                {
                    return "";
                }
                return model.title;
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion

        
    }
}