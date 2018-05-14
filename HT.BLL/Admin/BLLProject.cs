using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static HT.Model.Model.PageResult<ht_news> GetNewsList(int pageIndex,int pageSize,int cateId,string status,string keyword,string fromDate="",string toDate="")
        {
            HT.Model.Model.PageResult<ht_news> pageModel = new HT.Model.Model.PageResult<ht_news>();
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
				var unDelList = db.ht_news.Where(r => r.is_delete == 0);
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
                    unDelList = unDelList.Where(r => r.status == statusInt);
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
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids">多个id用,分隔</param>
		/// <returns></returns>
		public static bool Delete(string ids)
		{
			var arry = ids.Split(',');
			using (Entities db = new Entities())
			{

				foreach (var id in arry)
				{
					int idInt = int.Parse(id);
					db.ht_news.Single(p => p.id ==idInt ).is_delete = 1;
				}
				return db.SaveChanges() == arry.Length;
			}

		}
		/// <summary>
		/// 批量更新审核状态
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="status">0待审核1审核通过2审核不通过</param>
		/// <returns></returns>
		public static bool UpdateStatus(List<int> ids,int status)
		{

			using (Entities db = new Entities())
			{
				 db.ht_news.Where(p => ids.Contains(p.id)).ToList().ForEach(p=>p.status=status);
				if (db.SaveChanges()>0)
				{
					return true;
				}
				else
				{
					return false;
				}
				

			}

		}

	}
}
