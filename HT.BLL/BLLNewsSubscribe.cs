using EntityFramework.Extensions;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLNewsSubscribe
    {
        /// <summary>
        /// 获取订阅列表
        /// </summary>
        /// <returns></returns>
        public static Model.Model.PageResult<ht_news_subscribe> GetSubscribeListPageResult(int curUserid)
        {
            Model.Model.PageResult<ht_news_subscribe> pageModel = new Model.Model.PageResult<ht_news_subscribe>();
            using (Entities db = new Entities())
            {
                pageModel.list = db.ht_news_subscribe.Where(p=>p.add_userid == curUserid).ToList();
                pageModel.total = pageModel.list.Count;
            }
            pageModel.totalpage = 1;//总页数

            return pageModel;
        }
        /// <summary>
        /// 添加订阅线路
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool AddSubscribe(ht_news_subscribe model, out string msg)
        {
            msg = "";
            using (Entities db = new Entities())
            {
                try
                {
                    model.add_time = DateTime.Now;
                    db.ht_news_subscribe.Add(model);
                    return db.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                return false;
            }
        }
        /// <summary>
        /// 删除订阅线路
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DelSubscribe(string ids)
        {
            var arry = ids.Split(',').Select(p => Convert.ToInt32(p));
            using (Entities db = new Entities())
            {
                int result = db.ht_news_subscribe.Where(p => arry.Contains(p.id)).Delete();
                return result > 0;
            }
        }
    }
}
