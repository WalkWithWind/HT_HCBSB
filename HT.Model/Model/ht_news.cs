using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model
{
    public partial class ht_news
    {
        /// <summary>
        /// 是否点赞
        /// </summary>
        public bool is_praise { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public int? expire { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool? recommend { get; set; }
        /// <summary>
        /// 是否我发布
        /// </summary>
        public bool? isme { get; set; }
    }
}
