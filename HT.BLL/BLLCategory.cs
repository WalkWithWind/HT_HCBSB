using HT.Model;
using System.Collections.Generic;
using System.Linq;

namespace HT.BLL
{
    public class BLLCategory
    {

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="pid">上级id</param>
        /// <returns></returns>
        public static List<ht_category> GetCateList(int cid)
        {
            using (Entities db = new Entities())
            {
                return db.ht_category.Where(p => p.cid == cid).OrderBy(p => p.sort).ToList();
            }
        }
    }
}
