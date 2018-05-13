using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLAd
    {

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="code">标识代码</param>
        /// <returns></returns>
        public static List<ht_ad> GetAdList(string code)
        {
            using (Entities db = new Entities())
            {
                return db.ht_ad.Where(p => p.code == code).OrderBy(p=>p.sort).ToList();
            }
        }
    }
}
