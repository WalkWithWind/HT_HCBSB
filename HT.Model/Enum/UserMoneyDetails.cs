using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Enum
{
    public enum UserMoneyDetails
    {
        /// <summary>
        /// 项目支付
        /// </summary>
        PayNews = 1,
        /// <summary>
        /// 提现
        /// </summary>
        WithDraw = 2,

        /// <summary>
        /// 提现审核未通过 退回余额
        /// </summary>
        RefundMoney

    }
}
