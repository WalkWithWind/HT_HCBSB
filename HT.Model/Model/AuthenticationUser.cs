using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{
    /// <summary>
    /// 登陆用户基本信息
    /// </summary>
    [Serializable]
    public class AuthenticationUser
    {

        public int id { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public string avatar { get; set; }
    }
}
