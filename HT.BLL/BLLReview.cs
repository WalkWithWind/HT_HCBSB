using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLReview
    {

       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="review"></param>
       /// <returns></returns>
        public  static int AddReview(ht_review review)
        {
            using (Entities db = new Entities())
            {
                db.ht_review.Add(review);
                return db.SaveChanges();  
            }
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public static Model.Model.PageResult<ht_review> GetReviewList(int page,int rows, ht_review searchKey)
        {
            Model.Model.PageResult<ht_review> pageModel = new Model.Model.PageResult<ht_review>();
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.ht_review.Where(p => true);

                var alldata = data;

                if (!string.IsNullOrWhiteSpace(searchKey.review_type)) data = data.Where(p => p.review_type == searchKey.review_type);
                if (searchKey.news_id != 0) data = data.Where(p => p.news_id == searchKey.news_id);
                data = data.OrderByDescending(p => p.add_time);
                pageModel.list =  data.Skip((page - 1) * rows).Take(rows).ToList();
                foreach (var item in pageModel.list)
                {
                    item.reply_list = alldata.Where(p => p.review_id == item.id && p.review_type == "reply").ToList();
                }
                pageModel.total = data.Count();
            }
            pageModel.totalpage = (int)Math.Ceiling((decimal)pageModel.total / (decimal)rows);//总页数
            return pageModel;
        }



    }
}
