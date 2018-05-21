using HT.Model;
using System;
using System.Collections.Generic;
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
        private static IQueryable<ht_news> GetNewsData(Entities db, ht_news searchKey,bool isOrder=false,bool isRecommend=false)
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
            if (isOrder == false) return data;
            var orderData = data.OrderByDescending(p => p.set_top);
            if(isRecommend) orderData = orderData.ThenByDescending(p => p.praise_num);
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
                var newsDetail = db.ht_news.Find(id);
                if (newsDetail != null)
                {
                    newsDetail.view_num++;
                    db.SaveChanges();
                }
                return newsDetail;
            }
        }
        /// <summary>
        /// 获取信息列表返回数据
        /// </summary>
        /// <returns></returns>
        public static Model.Model.PageResult<ht_news> GetNewsListPageResult(int page, int rows, ht_news searchKey,bool isRecommend)
        {
            Model.Model.PageResult<ht_news> pageModel = new Model.Model.PageResult<ht_news>();
            using (Entities db = new Entities())
            {
                pageModel.total = GetNewsData(db, searchKey).Count();
                pageModel.list = GetNewsData(db, searchKey, true,isRecommend).Skip((page - 1) * rows).Take(rows).ToList();
            }
            pageModel.totalpage = (int)Math.Ceiling((decimal)pageModel.total / (decimal)rows);//总页数

            return pageModel;
        }

        #endregion 信息查询

        #region 热门推荐（猜你喜欢）
        /// <summary>
        /// 热门推荐
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static List<ht_news> GetLikeNewsList(int page, int rows,int id, int min)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                ht_news searchKey = db.ht_news.FirstOrDefault(p => p.id == id);
                var data = db.ht_news.Where(p => true);
                if (searchKey == null)
                {
                    return data.OrderByDescending(p => p.set_top)
                        .ThenByDescending(p => p.praise_num)
                        .ThenByDescending(p => p.update_time).ToList();
                }
                //排除id
                if (searchKey.id != 0) data = data.Where(p => p.id != searchKey.id);
                //分类一致
                if (searchKey.cateid != 0) data = data.Where(p => p.cateid == searchKey.cateid);
                if (!string.IsNullOrWhiteSpace(searchKey.use_type))
                {
                    if (data.Where(p => p.use_type == searchKey.use_type).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.use_type == searchKey.use_type);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.car_style))
                {
                    if (data.Where(p => p.car_style == searchKey.car_style).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.car_style == searchKey.car_style);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.car_length))
                {
                    if (data.Where(p => p.car_length == searchKey.car_length).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.car_length == searchKey.car_length);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.goods_type))
                {
                    if (data.Where(p => p.goods_type == searchKey.goods_type).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.goods_type == searchKey.goods_type);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.start_province))
                {
                    if (data.Where(p => p.start_province == searchKey.start_province).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.start_province == searchKey.start_province);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.start_city))
                {
                    if (data.Where(p => p.start_city == searchKey.start_city).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.start_city == searchKey.start_city);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.start_district))
                {
                    if (data.Where(p => p.start_district == searchKey.start_district).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.start_district == searchKey.start_district);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.stop_province))
                {
                    if (data.Where(p => p.stop_province == searchKey.stop_province).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.stop_province == searchKey.stop_province);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.stop_city))
                {
                    if (data.Where(p => p.stop_city == searchKey.stop_city).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.stop_city == searchKey.stop_city);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.stop_district))
                {
                    if (data.Where(p => p.stop_district == searchKey.stop_district).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.stop_district == searchKey.stop_district);
                }
                return data.OrderByDescending(p => p.set_top)
                    .ThenByDescending(p => p.praise_num)
                    .ThenByDescending(p => p.update_time).ToList();
            }
        }
        /// <summary>
        /// 热门推荐（猜你喜欢）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="id">排除当前记录</param>
        /// <param name="min">最少返回数</param>
        /// <returns></returns>
        public static List<ht_news> GetRecommendNewsList(int page, int rows,int id, int min = 1)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                ht_news searchKey = db.ht_news.FirstOrDefault(p => p.id == id);
                var data = db.ht_news.Where(p => true);
                if (searchKey == null) {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                }
                //排除id
                if (searchKey.id != 0) data = data.Where(p => p.id!= searchKey.id);
                //分类一致
                if (searchKey.cateid != 0) data = data.Where(p => p.cateid == searchKey.cateid);
                if (!string.IsNullOrWhiteSpace(searchKey.use_type)) {
                    if (data.Where(p => p.use_type == searchKey.use_type).Count() < min) {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.use_type == searchKey.use_type);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.car_style))
                {
                    if (data.Where(p => p.car_style == searchKey.car_style).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.car_style == searchKey.car_style);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.car_length))
                {
                    if (data.Where(p => p.car_length == searchKey.car_length).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.car_length == searchKey.car_length);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.goods_type))
                {
                    if (data.Where(p => p.goods_type == searchKey.goods_type).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.goods_type == searchKey.goods_type);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.start_province))
                {
                    if (data.Where(p => p.start_province == searchKey.start_province).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.start_province == searchKey.start_province);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.start_city))
                {
                    if (data.Where(p => p.start_city == searchKey.start_city).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.start_city == searchKey.start_city);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.start_district))
                {
                    if (data.Where(p => p.start_district == searchKey.start_district).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.start_district == searchKey.start_district);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.stop_province))
                {
                    if (data.Where(p => p.stop_province == searchKey.stop_province).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.stop_province == searchKey.stop_province);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.stop_city))
                {
                    if (data.Where(p => p.stop_city == searchKey.stop_city).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.stop_city == searchKey.stop_city);
                }
                if (!string.IsNullOrWhiteSpace(searchKey.stop_district))
                {
                    if (data.Where(p => p.stop_district == searchKey.stop_district).Count() < min)
                    {
                        return data.OrderByDescending(p => p.set_top)
                            .ThenByDescending(p => p.praise_num)
                            .ThenByDescending(p => p.update_time).ToList();
                    }
                    data = data.Where(p => p.stop_district == searchKey.stop_district);
                }
                return data.OrderByDescending(p => p.set_top)
                    .ThenByDescending(p => p.praise_num)
                    .ThenByDescending(p => p.update_time).ToList();
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
			orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(111111, 999999);
			using (Entities db = new Entities())
			{
				try
				{
					model.add_time = DateTime.Now;
					model.order_no = orderNo;
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
	}
}
