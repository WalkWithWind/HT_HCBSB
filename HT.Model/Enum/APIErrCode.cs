using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Model.Enum
{
    public enum APIErrCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 用户未登录
        /// </summary>
        UserIsNotLogin = 10010,
        /// <summary>
        /// 关键参数不完整
        /// </summary>
        PrimaryKeyIncomplete = 10011,
        /// <summary>
        /// 操作失败
        /// </summary>
        OperateFail = 10012,
        /// <summary>
        /// 重复
        /// </summary>
        IsRepeat = 10013,
        /// <summary>
        /// 指定内容未找到
        /// </summary>
        ContentNotFound = 10014,
        /// <summary>
        /// 没有找到（不指定特殊对象）
        /// </summary>
        IsNotFound = 10015,
        /// <summary>
        /// 验证码错误
        /// </summary>
        CheckCodeErr = 10016,
        /// <summary>
        /// 没有关注
        /// </summary>
        NoFollow = 10017,
        /// <summary>
        /// 注册失败
        /// </summary>
        RegisterFailure = 10018,
        /// <summary>
        /// 姓名为空
        /// </summary>
        UserNameIsEmpty = 10019,
        /// <summary>
        /// 邮箱已被注册
        /// </summary>
        EmailIsHave = 10020,
        /// <summary>
        /// 手机已被注册
        /// </summary>
        PhoneIsHave = 10021,
        /// <summary>
        /// 手机格式错误
        /// </summary>
        PhoneFormatError = 10022,
        /// <summary>
        /// 添加关系失败
        /// </summary>
        AddCommRelationError = 10023,
        /// <summary>
        /// 积分不足
        /// </summary>
        IntegralProblem = 10024,
        /// <summary>
        /// 未开启
        /// </summary>
        NotStart = 10025,
        /// <summary>
        /// 已结束
        /// </summary>
        IsEnd = 10026,
        /// <summary>
        /// 奖品全部中完了
        /// </summary>
        AwardIsOver = 10027,
        /// <summary>
        /// 次数超限
        /// </summary>
        CountIsOver = 10028,
        /// <summary>
        /// 抽奖活动已存在中奖记录
        /// </summary>
        LotteryHaveRecord = 10029,
        /// <summary>
        /// 权限不足
        /// </summary>
        InadequatePermissions = 10030,
        /// <summary>
        /// 存在历史账号
        /// </summary>
        HaveHistoryAcount = 10031,
        /// <summary>
        /// 已经是好友
        /// </summary>
        HasFriend = 10032
    }
}
