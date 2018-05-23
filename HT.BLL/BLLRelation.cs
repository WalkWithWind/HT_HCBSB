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
        public static bool IsExistRelation(string mainId,string relationId,string relationType)
        {
            using (Entities db = new Entities())
            {
                return db.ht_comm_relation.FirstOrDefault(p => p.main_id == mainId && p.relation_id == relationId && p.relation_type == relationType) != null ? true : false;
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
                ht_comm_relation model = db.ht_comm_relation.FirstOrDefault(p => p.main_id == relation.main_id && p.relation_id == relation.relation_id && p.relation_type == relation.relation_type);

                if (model != null)
                {
                    db.ht_comm_relation.Remove(model);
                    return db.SaveChanges();
                }
                return 0;
            }
        }
    }
}
