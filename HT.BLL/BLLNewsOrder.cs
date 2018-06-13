using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLNewsOrder
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static bool Add(ht_news_order model, out string msg, out string orderNo)
        {
            msg = "";
            orderNo = "T" + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + new Random().Next(111111, 999999);
            using (Entities db = new Entities())
            {
                try
                {
                    model.add_time = DateTime.Now;
                    model.order_no = orderNo;
                    model.pay_status = 0;
                    db.ht_news_order.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;

                }
                return false;

            }
        }

        /// <summary>
        /// 微信支付成功
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="tradeNo">交易号</param>
        /// <returns></returns>
        public static bool WXPaySuccess(string orderNo, string tradeNo)
        {
            using (Entities db = new Entities())
            {
                var order = db.ht_news_order.FirstOrDefault(p => p.order_no == orderNo);
                if (order.pay_status == 1)
                {
                    return false;
                }

                var news = db.ht_news.FirstOrDefault(p => p.id == order.news_id);

                ht_user_money_log log = new ht_user_money_log();
                log.userid = order.add_userid;
                log.type = (int)Model.Enum.UserMoneyDetails.SetTop;
                log.money = order.money;
                log.remark = string.Format("微信支付充值{0}元", order.money);
                log.addtime = DateTime.Now;
                db.ht_user_money_log.Add(log);

                ht_user_money_log log2 = new ht_user_money_log();
                log2.userid = order.add_userid;
                log.type = (int)Model.Enum.UserMoneyDetails.SetTop;
                log2.money = -order.money;
                log2.remark = string.Format("{0}支出{1}元", order.type, order.money);
                log2.addtime = DateTime.Now;
                db.ht_user_money_log.Add(log2);

                order.pay_status = 1;
                order.pay_time = DateTime.Now;
                order.pay_trade_no = tradeNo;

                news.set_top = Convert.ToInt32(order.value);
                news.set_top_money += order.money;
                news.total += order.money;

                return db.SaveChanges() > 0;
            }
        }
    }
}
