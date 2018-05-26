using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLPinpai
    {
        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="pid">上级id</param>
        /// <returns></returns>
        public static List<ht_pinpai> GetPinpaiList()
        {
            using (Entities db = new Entities())
            {
                return db.ht_pinpai.OrderBy(p => p.sort).ToList();
            }
        }
    }
}
