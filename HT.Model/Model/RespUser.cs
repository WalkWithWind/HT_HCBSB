using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{
    [Serializable]
    public class RespUser
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string avatar { get; set; }
        public decimal money { get; set; }
    }
}
