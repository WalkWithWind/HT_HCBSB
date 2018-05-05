using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HT.Model;

namespace HT.Utility
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig
    {
        private readonly Entities _db= new Entities();

        public SystemConfig()
        {
            
        }

        public dynamic this[int index]
        {
            get
            {
                ht_sys_config config = _db.ht_sys_config.FirstOrDefault(x => x.id == index);
                return config != null ? config.xvalue : null;
            }
        }

        public dynamic this[string xkey]
        {
            get
            {
                ht_sys_config config = _db.ht_sys_config.FirstOrDefault(x => x.xkey == xkey);
                return config != null ? config.xvalue : null;
            }
        }
    }
}
