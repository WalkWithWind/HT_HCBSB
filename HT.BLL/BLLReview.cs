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
        public static List<ht_review> GetReviewList(int page,int rows, ht_review searchKey)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.ht_review.Where(p => true);
                if (!string.IsNullOrWhiteSpace(searchKey.review_type)) data = data.Where(p => p.review_type == searchKey.review_type);
                if (searchKey.news_id != 0) data = data.Where(p => p.news_id == searchKey.news_id);
                data = data.OrderByDescending(p => p.add_time);
                return data.ToList();
            }
        }



    }
}
