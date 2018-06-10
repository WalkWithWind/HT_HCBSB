using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Enum
{
    public enum WithDraw
    {
        /// <summary>
        /// 待审核
        /// </summary>
        ToAudit = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        PassAudit =1 ,

        /// <summary>
        /// 审核不通过
        /// </summary>
        NotPassAudit = 2,

        /// <summary>
        /// 已打款
        /// </summary>
        MadeMoney =3,


    }
}
