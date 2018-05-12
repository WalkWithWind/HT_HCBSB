using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static HT.Model.Model.PageResult<ht_news> GetNewsList(int pageIndex,int pageSize,int cateId,string keyword)
        {
            HT.Model.Model.PageResult<ht_news> pageModel = new HT.Model.Model.PageResult<ht_news>();

            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var all = db.ht_news.AsEnumerable();

                if (cateId>0)
                {
                    var cateList = db.ht_news.Where(r => r.cateid==cateId);
                    all = all.Intersect(cateList);
                }
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var keywordList = db.ht_news.Where(r => r.title.Contains(keyword.Trim()));
                    all = all.Intersect(keywordList);
                }
                pageModel.totalpage =(int)Math.Ceiling((decimal)all.Count() / (decimal)pageSize);//总页数
                pageModel.total = all.Count();
                pageModel.list = all.OrderByDescending(p=>p.id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                return pageModel;
            }

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
                return db.ht_news.FirstOrDefault(p => p.id == id);
            }
        }

    }
}
