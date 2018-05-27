using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Model;

namespace HT.BLL
{
    public class BLLDistribution
    {


        public static List<ht_commission> GetCommussionList(int page,int rows,int userId,out int total,out decimal totalMoney, out int totalPeopleNum)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var list = db.ht_commission.Where(p => true);
                if (userId > 0)
                {
                    list = list.Where(p => p.userid == userId);
                }
                total = list.Count();
                totalMoney = list.Sum(p => (decimal)p.total_money);
                totalPeopleNum = list.Select(p => p.source_userid).Distinct().Count();
                return list.OrderByDescending(p => p.add_time).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }

        public static List<ht_commission> GetCommussionByChild(int page,int rows,int parentUserId,out int total)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var list = db.ht_commission.Where(p => true);

                if (parentUserId > 0)
                {
                    list = list.Where(p => p.source_pre_userid == parentUserId);
                }
                total = list.Count();
                return list.OrderByDescending(p => p.add_time).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }


        public static List<ht_distribution_log> GetMyDistributionList(int page,int rows,int userId,out int total)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var list = db.ht_distribution_log.Where(p => true);
                if (userId > 0)
                {
                    list = list.Where(p => p.userid == userId);
                }
                total = list.Count();
                return list.OrderByDescending(p => p.add_time).Skip((page - 1) * rows).Take(rows).ToList();
            }
        }

    }
}
