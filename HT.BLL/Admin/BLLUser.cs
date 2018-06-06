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

        /// <summary>
        /// 获取提现记录列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static HT.Model.Model.PageResult<ht_user_money_log> GetUserMoneyLogList(int pageIndex,int pageSize,int type)
        {
            HT.Model.Model.PageResult<ht_user_money_log> pageModel = new HT.Model.Model.PageResult<ht_user_money_log>();
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var userMoneyList = db.ht_user_money_log.Where(r => true);

                if (type>0)
                {
                    userMoneyList = userMoneyList.Where(r => r.type == type);
                }
                int total = userMoneyList.Count();

                pageModel.totalpage = (int)Math.Ceiling((decimal)total / (decimal)pageSize);//总页数
                pageModel.total = total;
                pageModel.list = userMoneyList.OrderByDescending(p => p.id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
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
                db.ht_user_money_log.Where(p => ids.Contains(p.id)).ToList().ForEach(item => {
                    item.status = status;
                });
                if (db.SaveChanges() > 0)
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
