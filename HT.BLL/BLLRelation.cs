using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    public class BLLRelation
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public static int AddRelation(ht_comm_relation relation)
        {
            using (Entities db = new Entities())
            {
                db.ht_comm_relation.Add(relation);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 是否存在关系
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static bool IsExistRelation(ht_comm_relation relation)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.ht_comm_relation.Where(p => true);
                if (!string.IsNullOrWhiteSpace(relation.relation_type)) data = data.Where(p => p.relation_type == relation.relation_type);
                if (!string.IsNullOrWhiteSpace(relation.main_id)) data = data.Where(p => p.main_id == relation.main_id);
                if (!string.IsNullOrWhiteSpace(relation.relation_id)) data = data.Where(p => p.relation_id == relation.relation_id);
                return data.Count() > 0 ? true : false;
            }
        }
        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static int DeleteRelation(ht_comm_relation relation)
        {
            using (Entities db = new Entities())
            {
                var data = db.ht_comm_relation.Where(p => true);
                if (!string.IsNullOrWhiteSpace(relation.relation_type)) data = data.Where(p => p.relation_type == relation.relation_type);
                if (!string.IsNullOrWhiteSpace(relation.main_id)) data = data.Where(p => p.main_id == relation.main_id);
                if (!string.IsNullOrWhiteSpace(relation.relation_id)) data = data.Where(p => p.relation_id == relation.relation_id);
                foreach (var item in data)
                {
                    db.ht_comm_relation.Remove(item);
                }
                return db.SaveChanges();
            }
        }
    }
}
