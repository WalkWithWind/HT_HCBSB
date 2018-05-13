using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Model
{
    public class PageResult<T>
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 分页数
        /// </summary>
        public int totalpage { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> list { get; set; }
    }
}
