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
                unDelList = unDelList.Where(p => p.isdelete == 0);
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

        public static HT.Model.Model.PageResult<ht_user_money_log> GetUserMoneyLogList(int pageIndex,int pageSize,int type,int? status =null)
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

                if (status.HasValue)
                {
                    userMoneyList = userMoneyList.Where(r => r.status == status);
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

                    if (status == (int)Model.Enum.WithDraw.NotPassAudit)
                    {
                        db.ht_user.Find(item.userid).money -= item.money;

                        ht_user_money_log model = new ht_user_money_log();
                        model.addtime = DateTime.Now;
                        model.userid = item.userid;
                        model.remark = "余额提现审核未通过" ;
                        model.money = item.money * -1;
                        model.type = (int)Model.Enum.UserMoneyDetails.RefundMoney;
                        model.status = 0;
                        db.ht_user_money_log.Add(model);
                    }
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteUser(List<int> ids)
        {
            using (Entities db = new Entities())
            {
                foreach (var item in ids)
                {
                    var user = db.ht_user.Find(item);
                    if (user != null) db.ht_user.Remove(user);
                }
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DisableUser(List<int> ids,int disable)
        {
            using (Entities db = new Entities())
            {
                db.ht_user.Where(p => ids.Contains(p.id)).ToList().ForEach(item =>
                {
                    item.isdisable = disable;
                });
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static int UpdateUser(int id,string mobile, decimal money)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var user = db.ht_user.Find(id);
                if (user != null)
                {
                    user.mobile = mobile;
                    user.money = money;
                }
                return db.SaveChanges();
            }
        }
    }
}
