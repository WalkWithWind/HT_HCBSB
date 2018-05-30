using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.BLL
{
    /// <summary>
    ///配置
    /// </summary>
    public class BLLConfig
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string Get(string configName)
        {
            using (Entities db = new Entities())
            {
              return  db.ht_sys_config.FirstOrDefault(p => p.xkey == configName).xvalue;
            }
        }



    }
}
