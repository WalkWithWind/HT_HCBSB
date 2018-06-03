using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
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
    }
}
