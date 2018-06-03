using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL.Admin
{
    public class BLLHelp
    {

        public static ht_help GetHelp(int id)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.ht_help.FirstOrDefault(p => p.id == id);
            }
        }

        public static bool UpdateContents(int id,string context)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var model = db.ht_help.Find(id);

                if (model != null)
                {
                    model.contents = context;
                    model.update_time = DateTime.Now;
                }
                else
                {
                    model = new ht_help();
                    model.title = "发布须知";
                    model.update_time = DateTime.Now;
                    model.contents = context;
                    model.add_time = DateTime.Now;
                    model.sort = 1;
                    db.ht_help.Add(model);
                }
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
