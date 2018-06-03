using System;
using System.Collections.Generic;
using System.Linq;
using HT.Model;

namespace HT.BLL
{
    /// <summary>
    /// 分销
    /// </summary>
    public class BLLDistribution
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="userId"></param>
        /// <param name="total"></param>
        /// <param name="totalMoney"></param>
        /// <param name="totalPeopleNum"></param>
        /// <returns></returns>
        public static List<ht_commission> GetCommussionList(int page, int rows, int userId, out int total, out decimal totalMoney, out int totalPeopleNum)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var list = db.ht_commission.Where(p => true);
                if (userId > 0)
                {
                    list = list.Where(p => p.userid == userId);
                }
                total = list.Count();
                totalMoney = list.Sum(p => (decimal)p.total_money);
                totalPeopleNum = list.Select(p => p.source_userid).Distinct().Count();
                return list.OrderByDescending(p => p.add_time).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="parentUserId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static List<ht_commission> GetCommussionByChild(int page, int rows, int parentUserId, out int total)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var list = db.ht_commission.Where(p => true);

                if (parentUserId > 0)
                {
                    list = list.Where(p => p.source_pre_userid == parentUserId);
                }
                total = list.Count();
                return list.OrderByDescending(p => p.add_time).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="userId"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        public static List<ht_distribution_log> GetMyDistributionList(int page, int rows, int userId, out int total)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var list = db.ht_distribution_log.Where(p => true);
                if (userId > 0)
                {
                    list = list.Where(p => p.userid == userId);
                }
                total = list.Count();
                return list.OrderByDescending(p => p.add_time).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }

        /// <summary>
        /// 分佣
        /// </summary>
        /// <param name="newsId">newsId</param>
        /// <returns></returns>
        public bool Maid(int newsId)
        {

            using (Entities db = new Entities())
            {
                try
                {
                    var model = db.ht_news.Single(p => p.id == newsId);
                    var sourceUser = db.ht_user.Single(p => p.id == model.add_userid);
                    ht_user preUserLevel1 = null;//上一级
                    ht_user preUserLevel2 = null;//上两级
                    if (sourceUser.parent_id != null && sourceUser.parent_id != 0)
                    {
                        preUserLevel1 = db.ht_user.Single(p => p.id == sourceUser.parent_id);
                    }
                    if (preUserLevel1 != null && preUserLevel1.parent_id != 0)
                    {
                        preUserLevel2 = db.ht_user.Single(p => p.id == preUserLevel1.parent_id);
                    }
                    if (preUserLevel1 == null && preUserLevel2 == null)//一二级都没有
                    {
                        return true;
                    }
                    decimal level1Rate = decimal.Parse(db.ht_sys_config.Single(p => p.xkey == "distribution_level1").xvalue) / 100;//上一级分佣比例
                    decimal level2Rate = decimal.Parse(db.ht_sys_config.Single(p => p.xkey == "distribution_level2").xvalue) / 100;//上两级分佣比例
                    decimal level1Amount = Math.Round(model.total.Value * level1Rate, 2);//分给上一级的金额
                    decimal level2Amount = Math.Round(model.total.Value * level2Rate, 2);//分给上两级的金额
                    if (preUserLevel1 != null)//给上一级分佣
                    {
                        preUserLevel1.money += level1Amount;
                        ht_user_money_log level1Log = new ht_user_money_log();
                        level1Log.addtime = DateTime.Now;
                        level1Log.userid = preUserLevel1.id;
                        level1Log.money = level1Amount;
                        level1Log.type = 1;
                        level1Log.remark = string.Format("一级分佣获得{0}元", level1Amount);
                        db.ht_user_money_log.Add(level1Log);

                        ht_distribution_log disLevel1Log = new ht_distribution_log();
                        disLevel1Log.userid = preUserLevel1.id;
                        disLevel1Log.title = "一级推荐人订单返利";
                        disLevel1Log.order_no = model.order_no;
                        disLevel1Log.money = level1Amount;
                        disLevel1Log.source_userid = model.add_userid;
                        disLevel1Log.source_pre_userid = preUserLevel1.id;
                        disLevel1Log.news_id = model.id;
                        disLevel1Log.add_time = DateTime.Now;
                        db.ht_distribution_log.Add(disLevel1Log);

                        ht_commission comLevel1 = db.ht_commission.SingleOrDefault(p => p.userid == preUserLevel1.id && p.source_userid == model.add_userid);
                        if (comLevel1 != null)
                        {
                            comLevel1.total_money += level1Amount;
                        }
                        else
                        {
                            comLevel1 = new ht_commission();
                            comLevel1.userid = preUserLevel1.id;
                            comLevel1.add_time = DateTime.Now;
                            comLevel1.source_userid = model.add_userid;
                            comLevel1.source_user_nick = sourceUser.nickname;
                            comLevel1.source_user_avatar = sourceUser.avatar;
                            comLevel1.source_pre_userid = preUserLevel1.id;
                            comLevel1.total_money = level1Amount;
                            db.ht_commission.Add(comLevel1);

                        }

                    }

                    if (preUserLevel2 != null)//给上两级分佣
                    {
                        preUserLevel2.money += level2Amount;
                        ht_user_money_log level2Log = new ht_user_money_log();
                        level2Log.addtime = DateTime.Now;
                        level2Log.userid = preUserLevel2.id;
                        level2Log.money = level2Amount;
                        level2Log.type = 1;
                        level2Log.remark = string.Format("二级分佣获得{0}元", level2Amount);
                        db.ht_user_money_log.Add(level2Log);

                        ht_distribution_log disLevel2Log = new ht_distribution_log();
                        disLevel2Log.userid = preUserLevel2.id;
                        disLevel2Log.title = "二级推荐人订单返利";
                        disLevel2Log.order_no = model.order_no;
                        disLevel2Log.money = level2Amount;
                        disLevel2Log.source_userid = model.add_userid;
                        disLevel2Log.source_pre_userid = preUserLevel1.id;
                        disLevel2Log.news_id = model.id;
                        disLevel2Log.add_time = DateTime.Now;
                        db.ht_distribution_log.Add(disLevel2Log);

                        ht_commission comLevel2 = db.ht_commission.SingleOrDefault(p => p.userid == preUserLevel2.id && p.source_userid == model.add_userid);
                        if (comLevel2 != null)
                        {
                            comLevel2.total_money += level2Amount;
                        }
                        else
                        {
                            comLevel2 = new ht_commission();
                            comLevel2.userid = preUserLevel2.id;
                            comLevel2.add_time = DateTime.Now;
                            comLevel2.source_userid = model.add_userid;
                            comLevel2.source_user_nick = sourceUser.nickname;
                            comLevel2.source_user_avatar = sourceUser.avatar;
                            comLevel2.source_pre_userid = preUserLevel1.id;
                            comLevel2.total_money = level2Amount;
                            db.ht_commission.Add(comLevel2);

                        }

                    }

                   
                     return db.SaveChanges() > 0;
                    

                }
                catch (Exception ex)
                {
                    //日志
                    return false;
                }
            }


        }

    }
}
