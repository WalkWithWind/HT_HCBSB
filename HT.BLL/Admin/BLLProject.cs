using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using HT.Model;

namespace HT.BLL.Admin
{
    public class BLLProject
    {

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cateId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        public static HT.Model.Model.PageResult<ht_news> GetNewsList(int pageIndex,int pageSize,int cateId,string status,string keyword,string fromDate="",string toDate="")
        {
            HT.Model.Model.PageResult<ht_news> pageModel = new HT.Model.Model.PageResult<ht_news>();
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
				var unDelList = db.ht_news.Where(r => true);
				if (cateId > 0)
				{
                    unDelList = unDelList.Where(r => r.cateid == cateId);
				}
				if (!string.IsNullOrWhiteSpace(keyword))
				{
                    unDelList = unDelList.Where(r => r.title.Contains(keyword.Trim()));
				}
				if (!string.IsNullOrWhiteSpace(status))
				{
					int statusInt = int.Parse(status);
                    if (statusInt > 0)
                    {
                        unDelList = unDelList.Where(r => r.status == statusInt);
                    }
                    else
                    {
                        if (statusInt == 0)
                        {
                            unDelList = unDelList.Where(r => r.status == 0 && r.pay_status == 1);
                        }
                        else
                        {
                            unDelList = unDelList.Where(r => r.status == 0 && r.pay_status == 0);
                        }
                    }
                    
				}
				if (!string.IsNullOrWhiteSpace(fromDate))
				{
					DateTime dtFrom = DateTime.Parse(fromDate);
                    unDelList = unDelList.Where(r => r.add_time >= dtFrom);
				}
				if (!string.IsNullOrWhiteSpace(toDate))
				{
					DateTime dtTo = DateTime.Parse(toDate).AddDays(1).AddSeconds(-1);
                    unDelList = unDelList.Where(r => r.add_time <=dtTo);
				}
                int total = unDelList.Count();

                pageModel.totalpage =(int)Math.Ceiling((decimal)total / (decimal)pageSize);//总页数
                pageModel.total = total;
                pageModel.list = unDelList.OrderByDescending(p=>p.id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

                foreach (var item in pageModel.list)
                {
                    item.review_s0 = db.ht_review
                        .Where(p => p.news_id == item.id && p.status == 0)
                        .Count();
                }
            }
            return pageModel;
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ht_news GetNew(int id)
        {
			
			using (Entities db = new Entities())
            {
				db.Configuration.ProxyCreationEnabled = false;
				return db.ht_news.Find(id);
            }

        }
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids">多个id用,分隔</param>
		/// <returns></returns>
		public static bool Delete(string ids)
		{
			var arry = ids.Split(',').Select(p=>Convert.ToInt32(p));
			using (Entities db = new Entities())
			{

                #region 支付过的删除时备份一下
                foreach(ht_news item in db.ht_news.Where(p => p.pay_status == 1 && arry.Contains(p.id)))
                {
                    ht_news_del bk = new ht_news_del()
                    {
                        news_id = item.id,
                        cateid = item.cateid,
                        cate = item.cate,
                        title = item.title,
                        description = item.description,
                        contact_name = item.contact_name,
                        contact_phone = item.contact_phone,
                        validity_num = item.validity_num,
                        validity_unit = item.validity_unit,
                        start_province = item.start_province,
                        start_city = item.start_city,
                        start_district = item.start_district,
                        start_address = item.start_address,
                        stop_province = item.stop_province,
                        stop_city = item.stop_city,
                        stop_district = item.stop_district,
                        stop_address = item.stop_address,
                        tags = item.tags,
                        use_type = item.use_type,
                        use_img = item.use_img,
                        car_length = item.car_length,
                        car_style = item.car_style,
                        goods_type = item.goods_type,
                        goods_weight = item.goods_weight,
                        goods_weight_unit = item.goods_weight_unit,
                        freight = item.freight,
                        use_time = item.use_time,
                        use_mode = item.use_mode,
                        pay_method = item.pay_method,
                        other_remark = item.other_remark,
                        set_top = item.set_top,
                        set_top_money = item.set_top_money,
                        reward_money = item.reward_money,
                        recruit_num = item.recruit_num,
                        imgs = item.imgs,
                        add_userid = item.add_userid,
                        add_nickname = item.add_nickname,
                        add_avatar = item.add_avatar,
                        add_time = item.add_time,
                        update_userid = item.update_userid,
                        update_nickname = item.update_nickname,
                        update_time = item.update_time,
                        audit_userid = item.audit_userid,
                        audit_nickname = item.audit_nickname,
                        audit_time = item.audit_time,
                        status = item.status,
                        pay = item.pay,
                        pay_status = item.pay_status,
                        pay_time = item.pay_time,
                        pay_trade_no = item.pay_trade_no,
                        order_no = item.order_no,
                        view_num = item.view_num,
                        praise_num = item.praise_num,
                        share_num = item.share_num,
                        is_delete = item.is_delete,
                        total = item.total
                    };
                    db.ht_news_del.Add(bk);
                }
                #endregion 支付过的删除时备份一下
                db.SaveChanges();//保存备份
                int result = db.ht_news.Where(p =>arry.Contains(p.id)).Delete();
				return result > 0;
			}

		}
		/// <summary>
		/// 批量更新审核状态
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="status">0待审核1审核通过2审核不通过3.已过期</param>
		/// <returns></returns>
		public static bool UpdateStatus(List<int> ids,int status)
		{

			using (Entities db = new Entities())
            {
                int result = db.ht_news.Where(p => ids.Contains(p.id)).Update(t => new ht_news { status = status, audit_time=DateTime.Now, audit_userid=0 });

				if (result > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

        /// <summary>
        /// 查询所有项目
        /// </summary>
        /// <returns></returns>
        public static int CheckExpire()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.ht_news.Where(r => true);
                data = data.Where(p => (p.validity_unit == "月" && DbFunctions.AddMonths(p.add_time, p.validity_num.Value) < DateTime.Now) || (p.validity_unit == "天" && DbFunctions.AddDays(p.add_time, p.validity_num.Value) < DateTime.Now));

                foreach (var item in data)
                {
                    item.status = 3;
                }
                return db.SaveChanges();
            }

        }

	}
}
