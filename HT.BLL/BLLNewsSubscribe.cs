using EntityFramework.Extensions;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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
                    if (db.ht_news_subscribe.FirstOrDefault(p=>p.start_province ==model.start_province 
                    && p.start_city ==model.start_city && p.start_district==model.start_district 
                    && p.stop_province ==model.stop_province && p.stop_city ==model.stop_city 
                    && p.stop_district == model.stop_district) !=null)
                    {
                        msg = "该线路已订阅";
                        return false;
                    }
                    if (db.ht_news_subscribe.Where(p => p.add_userid == model.add_userid).Count() >= 10)
                    {
                        msg = "最多能订阅10条线路";
                        return false;
                    }
                    model.add_time = DateTime.Now;
                    model.rcount = 0;
                    Expression<Func<ht_news, bool>> ncountWhere = s =>
                            s.status == 1 && s.cateid==1 && 
                            (model.start_province + model.start_city + model.start_district)
                            .StartsWith(s.start_province + s.start_city + s.start_district)
                            &&
                            (model.stop_province + model.stop_city + model.stop_district)
                            .StartsWith(s.stop_province + s.stop_city + s.stop_district);

                    model.ncount = db.ht_news
                        .Where(ncountWhere.Compile()).Select(p => p.id).Count();

                    db.ht_news_subscribe.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        //线程插入线路关系
                        new Thread(p => {
                            using (Entities dbt = new Entities())
                            {
                                List<ht_news_subscribe_relation> listRelation = dbt.ht_news
                                    .Where(ncountWhere.Compile())
                                    .Select(x => new ht_news_subscribe_relation
                                    {
                                        news_id = x.id,
                                        subscribe_id = model.id,
                                        is_look = 1
                                    }).ToList();
                                if (listRelation.Count > 0)
                                {
                                    dbt.ht_news_subscribe_relation.AddRange(listRelation);
                                    dbt.SaveChanges();
                                }
                            }
                        }).Start();
                        return true;
                    }
                    return false;
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
