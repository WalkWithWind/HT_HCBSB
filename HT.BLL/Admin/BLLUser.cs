using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL.Admin
{
    public class BLLUser
    {/// <summary>
     /// 获取列表
     /// </summary>
     /// <param name="pageIndex"></param>
     /// <param name="pageSize"></param>
     /// <param name="keyword"></param>
     /// <returns></returns>

        public static HT.Model.Model.PageResult<ht_user> GetUsersList(int pageIndex, int pageSize,string keyword)
        {
            HT.Model.Model.PageResult<ht_user> pageModel = new HT.Model.Model.PageResult<ht_user>();
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var unDelList = db.ht_user.Where(r => true);
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    unDelList = unDelList.Where(r => r.mobile.Contains(keyword.Trim()) || r.nickname.Contains(keyword.Trim()));
                }
                int total = unDelList.Count();

                pageModel.totalpage = (int)Math.Ceiling((decimal)total / (decimal)pageSize);//总页数
                pageModel.total = total;
                pageModel.list = unDelList.OrderByDescending(p => p.id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                return pageModel;
            }

        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ht_user GetUser(int id)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.ht_user.Find(id);
            }
        }
    }
}
