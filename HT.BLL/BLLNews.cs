using HT.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLNews
    {
        #region 信息查询
        /// <summary>
        /// 构造查询IQueryable
        /// </summary>
        /// <param name="db">查询条件</param>
        /// <param name="searchKey">查询条件</param>
        /// <param name="searchKey">查询条件</param>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        private static IQueryable<ht_news> GetNewsData(Entities db, ht_news searchKey,bool isOrder=false)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.ht_news.Where(p => true);
            if (searchKey.cateid != 0) data = data.Where(p => p.cateid == searchKey.cateid);
            if (!string.IsNullOrWhiteSpace(searchKey.start_province)) data = data.Where(p => p.start_province == searchKey.start_province);
            if (!string.IsNullOrWhiteSpace(searchKey.start_city)) data = data.Where(p => p.start_city == searchKey.start_city);
            if (!string.IsNullOrWhiteSpace(searchKey.start_district)) data = data.Where(p => p.start_district == searchKey.start_district);
            if (!string.IsNullOrWhiteSpace(searchKey.stop_province)) data = data.Where(p => p.stop_province == searchKey.stop_province);
            if (!string.IsNullOrWhiteSpace(searchKey.stop_city)) data = data.Where(p => p.stop_city == searchKey.stop_city);
            if (!string.IsNullOrWhiteSpace(searchKey.stop_district)) data = data.Where(p => p.stop_district == searchKey.stop_district);
            if (!string.IsNullOrWhiteSpace(searchKey.use_type)) data = data.Where(p => p.use_type == searchKey.use_type);
            if (!string.IsNullOrWhiteSpace(searchKey.car_length)) data = data.Where(p => p.car_length == searchKey.car_length);
            if (!string.IsNullOrWhiteSpace(searchKey.car_style)) data = data.Where(p => p.car_style == searchKey.car_style);
            if (!string.IsNullOrWhiteSpace(searchKey.goods_type)) data = data.Where(p => p.goods_type == searchKey.goods_type);
            if (!string.IsNullOrWhiteSpace(searchKey.goods_type)) data = data.Where(p => p.goods_type == searchKey.goods_type);
            if(searchKey.expire.HasValue && searchKey.expire == 1)
            {
                data = data.Where(p => (p.validity_unit == "月" && DbFunctions.AddMonths(p.add_time,p.validity_num.Value) < DateTime.Now) || (p.validity_unit == "天" && DbFunctions.AddDays(p.add_time, p.validity_num.Value) < DateTime.Now));
            }
            else if(searchKey.expire.HasValue && searchKey.expire == 0)
            {
                data = data.Where(p => (p.validity_unit == "月" && DbFunctions.AddMonths(p.add_time, p.validity_num.Value) > DateTime.Now) || (p.validity_unit == "天" && DbFunctions.AddDays(p.add_time, p.validity_num.Value) > DateTime.Now));
            }
            if (searchKey.status.HasValue) data = data.Where(p => p.status == searchKey.status);
            if (searchKey.pay_status.HasValue) data = data.Where(p => p.pay_status == searchKey.pay_status);
            if (searchKey.add_userid != 0) data = data.Where(p => p.add_userid == searchKey.add_userid);

            if (isOrder == false) return data;
            var orderData = data.OrderByDescending(p => p.set_top);
            if(searchKey.recommend.HasValue && searchKey.recommend.Value) orderData = orderData.ThenByDescending(p => p.praise_num);
            orderData = orderData.ThenByDescending(p => p.update_time);
            return orderData;
        }
        /// <summary>
        /// 获取信息列表
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="row">每页显示数</param>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        public static List<ht_news> GetNewsList(int page, int rows, ht_news searchKey)
        {
            using (Entities db = new Entities())
            {
                return GetNewsData(db,searchKey,true).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }
        /// <summary>
        /// 获取信息总数
        /// </summary>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        public static int GetNewsCount(ht_news searchKey)
        {
            using (Entities db = new Entities())
            {
                return GetNewsData(db,searchKey).Count();
            }
        }
        /// <summary>
        /// 获取信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ht_news GetNewsDetails(int id)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var newsDetail = db.ht_news.FirstOrDefault(p=>p.id == id);
                if (newsDetail != null)
                {
                    newsDetail.view_num++;
                    db.SaveChanges();
                }
                return newsDetail;
            }
        }
        /// <summary>
        /// 订单号获取信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ht_news GetNewsDetailsByOrderNo(string order_no)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.ht_news.FirstOrDefault(p=>p.order_no == order_no);
            }
        }
        /// <summary>
        /// 获取信息列表返回数据
        /// </summary>
        /// <returns></returns>
        public static Model.Model.PageResult<ht_news> GetNewsListPageResult(int page, int rows, ht_news searchKey)
        {
            Model.Model.PageResult<ht_news> pageModel = new Model.Model.PageResult<ht_news>();
            using (Entities db = new Entities())
            {
                pageModel.total = GetNewsData(db, searchKey).Count();
                pageModel.list = GetNewsData(db, searchKey, true).Skip((page - 1) * rows).Take(rows).ToList();
            }
            pageModel.totalpage = (int)Math.Ceiling((decimal)pageModel.total / (decimal)rows);//总页数

            return pageModel;
        }

        #endregion 信息查询

        #region 热门推荐（猜你喜欢）
        private static IQueryable<ht_news> GetLikeNewsData(Entities db, int id, int min)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.ht_news.Where(p => true);
            ht_news searchKey = db.ht_news.FirstOrDefault(p => p.id == id);
            if (searchKey == null) return data; 
            //排除id
            if (searchKey.id != 0) data = data.Where(p => p.id != searchKey.id);
            //分类一致
            if (searchKey.cateid != 0) data = data.Where(p => p.cateid == searchKey.cateid);
            if (!string.IsNullOrWhiteSpace(searchKey.use_type))
            {
                if (data.Where(p => p.use_type == searchKey.use_type).Count() < min) return data;
                data = data.Where(p => p.use_type == searchKey.use_type);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.car_style))
            {
                if (data.Where(p => p.car_style == searchKey.car_style).Count() < min) return data;
                data = data.Where(p => p.car_style == searchKey.car_style);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.car_length))
            {
                if (data.Where(p => p.car_length == searchKey.car_length).Count() < min) return data;
                data = data.Where(p => p.car_length == searchKey.car_length);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.goods_type))
            {
                if (data.Where(p => p.goods_type == searchKey.goods_type).Count() < min) return data;
                data = data.Where(p => p.goods_type == searchKey.goods_type);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.start_province))
            {
                if (data.Where(p => p.start_province == searchKey.start_province).Count() < min) return data;
                data = data.Where(p => p.start_province == searchKey.start_province);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.start_city))
            {
                if (data.Where(p => p.start_city == searchKey.start_city).Count() < min) return data;
                data = data.Where(p => p.start_city == searchKey.start_city);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.start_district))
            {
                if (data.Where(p => p.start_district == searchKey.start_district).Count() < min) return data;
                data = data.Where(p => p.start_district == searchKey.start_district);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.stop_province))
            {
                if (data.Where(p => p.stop_province == searchKey.stop_province).Count() < min) return data;
                data = data.Where(p => p.stop_province == searchKey.stop_province);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.stop_city))
            {
                if (data.Where(p => p.stop_city == searchKey.stop_city).Count() < min) return data;
                data = data.Where(p => p.stop_city == searchKey.stop_city);
            }
            if (!string.IsNullOrWhiteSpace(searchKey.stop_district))
            {
                if (data.Where(p => p.stop_district == searchKey.stop_district).Count() < min) return data;
                data = data.Where(p => p.stop_district == searchKey.stop_district);
            }
            return data;
        }
        /// <summary>
        /// 猜你喜欢
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static List<ht_news> GetLikeNewsList(int page, int rows,int id, int min)
        {
            using (Entities db = new Entities())
            {
                var data = GetLikeNewsData(db, id, min).OrderByDescending(p => p.set_top)
                    .ThenByDescending(p => p.praise_num)
                    .ThenByDescending(p => p.update_time);

                return data.Skip((page - 1) * rows).Take(rows).ToList();
            }
        }
		#endregion 热门推荐（猜你喜欢）

		/// <summary>
		/// 发布项目
		/// </summary>
		/// <param name="model">模型</param>
		/// <param name="msg">提示消息</param>
		/// <param name="orderNo">订单号,成功时返回</param>
		/// <returns>成功 失败</returns>
		public static bool Add(ht_news model,out string msg,out string orderNo)
		{
			msg = "";
			orderNo = "B"+DateTime.Now.ToString("yyyyMMddHHmmssffffff") + new Random().Next(111111, 999999);
			using (Entities db = new Entities())
			{
				try
				{
					model.add_time = DateTime.Now;
                    model.update_time = model.add_time;
                    model.order_no = orderNo;
                    model.status = 0;
                    if (!model.set_top.HasValue) model.set_top = 0;
                    model.pay_status = 0;
                    model.is_delete = 0;
                    db.ht_news.Add(model);
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
        /// 点赞数加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int AddPraise(int id, ht_comm_relation relation)
        {
            using (Entities db = new Entities())
            {
                db.ht_comm_relation.Add(relation);
                db.ht_news.Find(id).praise_num++;
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 点赞数减
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeletePraise(int id, ht_comm_relation relation)
        {
            using (Entities db = new Entities())
            {
                ht_comm_relation model = db.ht_comm_relation.FirstOrDefault(p => p.main_id == relation.main_id && p.relation_id == relation.relation_id && p.relation_type == relation.relation_type);
                if (model == null) return 0;
                db.ht_comm_relation.Remove(model);
                db.ht_news.Find(id).praise_num--;
                int rusult = db.SaveChanges();
                return rusult;
            }
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelNews(int id)
        {
            using (Entities db = new Entities())
            {
                var details = db.ht_news.Find(id);
                #region 支付过的删除时备份一下
                if (details.pay_status == 1)
                {
                    ht_news_del bk = new ht_news_del()
                    {
                        news_id = details.id,
                        cateid = details.cateid,
                        cate = details.cate,
                        title = details.title,
                        description = details.description,
                        contact_name = details.contact_name,
                        contact_phone = details.contact_phone,
                        validity_num = details.validity_num,
                        validity_unit = details.validity_unit,
                        start_province = details.start_province,
                        start_city = details.start_city,
                        start_district = details.start_district,
                        start_address = details.start_address,
                        stop_province = details.stop_province,
                        stop_city = details.stop_city,
                        stop_district = details.stop_district,
                        stop_address = details.stop_address,
                        tags = details.tags,
                        use_type = details.use_type,
                        use_img = details.use_img,
                        car_length = details.car_length,
                        car_style = details.car_style,
                        goods_type = details.goods_type,
                        goods_weight = details.goods_weight,
                        goods_weight_unit = details.goods_weight_unit,
                        freight = details.freight,
                        use_time = details.use_time,
                        use_mode = details.use_mode,
                        pay_method = details.pay_method,
                        other_remark = details.other_remark,
                        set_top = details.set_top,
                        set_top_money = details.set_top_money,
                        reward_money = details.reward_money,
                        recruit_num = details.recruit_num,
                        imgs = details.imgs,
                        add_userid = details.add_userid,
                        add_nickname = details.add_nickname,
                        add_avatar = details.add_avatar,
                        add_time = details.add_time,
                        update_userid = details.update_userid,
                        update_nickname = details.update_nickname,
                        update_time = details.update_time,
                        audit_userid = details.audit_userid,
                        audit_nickname = details.audit_nickname,
                        audit_time = details.audit_time,
                        status = details.status,
                        pay = details.pay,
                        pay_status = details.pay_status,
                        pay_time = details.pay_time,
                        pay_trade_no = details.pay_trade_no,
                        order_no = details.order_no,
                        view_num = details.view_num,
                        praise_num = details.praise_num,
                        share_num = details.share_num,
                        is_delete = details.is_delete,
                        total = details.total
                    };
                    db.ht_news_del.Add(bk);
                }
                #endregion 支付过的删除时备份一下
                db.ht_news.Remove(details);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 余额支付
        /// </summary>
        /// <param name="order_no">订单号</param>
        /// <param name="pay">支付方式</param>
        /// <param name="pay_trade_no">交易号</param>
        /// <returns></returns>
        public static int PayNews(string order_no,string pay,string pay_trade_no,out string msg)
        {
            msg = "支付失败";
            using (Entities db = new Entities())
            {
                var details = db.ht_news.FirstOrDefault(p => p.order_no == order_no);
                if(details.pay_status ==1)
                {
                    msg = "已支付过";
                    return 0;
                }

                ht_user_money_log log = new ht_user_money_log();
                log.userid = details.add_userid;
                log.type =1;
                log.money = -details.total;
                log.remark = string.Format("余额支出{0}元", details.total);
                log.addtime = DateTime.Now;
                db.ht_user_money_log.Add(log);

                ht_user user = db.ht_user.Find(details.add_userid);
                user.money = user.money - details.total;
                details.pay_status = 1;
                details.pay_time = DateTime.Now;
                details.pay = pay;
                details.pay_trade_no = pay_trade_no;
                // var tran = db.Database.BeginTransaction();
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 微信支付成功
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="tradeNo">交易号</param>
        /// <returns></returns>
        public static bool WXPaySuccess(string orderNo,string tradeNo)
        {
          
            using (Entities db = new Entities())
            {

                    var details = db.ht_news.FirstOrDefault(p => p.order_no == orderNo);
                    if (details.pay_status == 1)
                    {

                        return false;
                    }
                    ht_user_money_log log = new ht_user_money_log();
                    log.userid = details.add_userid;
                    log.type = 0;
                    log.money = details.total;
                    log.remark =string.Format( "微信支付充值{0}元",details.total);
                    log.addtime = DateTime.Now;
                    db.ht_user_money_log.Add(log);

                    ht_user_money_log log2 = new ht_user_money_log();
                    log2.userid = details.add_userid;
                    log2.type = 0;
                    log2.money = -details.total;
                    log2.remark = string.Format("微信支付支出{0}元", details.total);
                    log2.addtime = DateTime.Now;
                    db.ht_user_money_log.Add(log2);

                    details.pay_status = 1;
                    details.pay_time = DateTime.Now;
                    details.pay = "微信";
                    details.pay_trade_no = tradeNo;
                    return db.SaveChanges() > 0;


            }
        }


    }
}
