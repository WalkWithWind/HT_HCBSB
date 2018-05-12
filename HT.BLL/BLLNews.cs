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

        /// <summary>
        /// 构造查询IQueryable
        /// </summary>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        private static IQueryable<ht_news> GetNewsData(ht_news searchKey)
        {
            using (Entities db = new Entities())
            {
                var data = db.ht_news.Where(p => true);
                if (searchKey.cateid != 0) data = data.Where(p => p.cateid == searchKey.cateid);
                if (string.IsNullOrWhiteSpace(searchKey.start_province)) data = data.Where(p => p.start_province == searchKey.start_province);
                if (string.IsNullOrWhiteSpace(searchKey.start_city)) data = data.Where(p => p.start_city == searchKey.start_city);
                if (string.IsNullOrWhiteSpace(searchKey.start_district)) data = data.Where(p => p.start_district == searchKey.start_district);
                if (string.IsNullOrWhiteSpace(searchKey.stop_province)) data = data.Where(p => p.stop_province == searchKey.stop_province);
                if (string.IsNullOrWhiteSpace(searchKey.stop_city)) data = data.Where(p => p.stop_city == searchKey.stop_city);
                if (string.IsNullOrWhiteSpace(searchKey.stop_district)) data = data.Where(p => p.stop_district == searchKey.stop_district);

                return data;
            }
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
            return GetNewsData(searchKey).Skip((page - 1) * rows).Take(rows).ToList();
        }
        /// <summary>
        /// 获取信息总数
        /// </summary>
        /// <param name="searchKey">查询条件</param>
        /// <returns></returns>
        public static int GetNewsCount(ht_news searchKey)
        {
            return GetNewsData(searchKey).Count();
        }
        /// <summary>
        /// 获取信息列表返回数据
        /// </summary>
        /// <returns></returns>
        public static Model.Model.PageResult<ht_news> GetNewsListPageResult(int page, int rows, ht_news searchKey)
        {
            Model.Model.PageResult<ht_news> pageModel = new Model.Model.PageResult<ht_news>();
            try
            {
                pageModel.total = BLLNews.GetNewsCount(searchKey);
                pageModel.list = BLLNews.GetNewsList(page, rows, searchKey);
                pageModel.totalpage = (int)Math.Ceiling((decimal)pageModel.total / (decimal)rows);//总页数
                pageModel.status = 1;
            }
            catch (Exception ex)
            {
                pageModel.status = 0;
                pageModel.msg = ex.Message;
            }
            return pageModel;
        }
    }
}
