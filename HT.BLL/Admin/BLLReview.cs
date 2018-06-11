using EntityFramework.Extensions;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL.Admin
{
    public class BLLReview
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        public static HT.Model.Model.PageResult<ht_review> GetReviewsList(int pageIndex, int pageSize, string news_id, string type, string status, string keyword, string review_id)
        {
            HT.Model.Model.PageResult<ht_review> pageModel = new HT.Model.Model.PageResult<ht_review>();
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var unDelList = db.ht_review.Where(r => true);
                if (!string.IsNullOrWhiteSpace(news_id))
                {
                    int newsid = int.Parse(news_id);
                    unDelList = unDelList.Where(r => r.news_id == newsid);
                }
                if (!string.IsNullOrWhiteSpace(type))
                {
                    unDelList = unDelList.Where(r => r.review_type == type);
                }
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    unDelList = unDelList.Where(r => r.nickname == keyword || r.review_content.StartsWith(keyword));
                }
                if (!string.IsNullOrWhiteSpace(status))
                {
                    int statusInt = int.Parse(status);
                    unDelList = unDelList.Where(r => r.status == statusInt);
                }
                if (!string.IsNullOrWhiteSpace(review_id))
                {
                    int review_id_num = Convert.ToInt32(review_id);
                    unDelList = unDelList.Where(r => r.review_id == review_id_num);
                }
                int total = unDelList.Count();

                pageModel.totalpage = (int)Math.Ceiling((decimal)total / (decimal)pageSize);//总页数
                pageModel.total = total;
                pageModel.list = unDelList.OrderByDescending(p => p.id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                return pageModel;
            }

        }
        /// <summary>
        /// 批量更新审核状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="status">0待审核1审核通过2审核不通过</param>
        /// <returns></returns>
        public static bool UpdateStatus(List<int> ids, int status)
        {

            using (Entities db = new Entities())
            {
                int result = db.ht_review.Where(p => ids.Contains(p.id)).Update(t => new ht_review { status = status });
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
        /// 删除
        /// </summary>
        /// <param name="ids">多个id用,分隔</param>
        /// <returns></returns>
        public static bool Delete(string ids)
        {
            var arry = ids.Split(',').Select(p => Convert.ToInt32(p));
            using (Entities db = new Entities())
            {
                int result = db.ht_review.Where(p => arry.Contains(p.id)).Delete();
                return result > 0;
            }

        }
    }
}
