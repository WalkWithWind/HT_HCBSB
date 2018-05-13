using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Utility
{
    /// <summary>
    /// 日期扩展类
    /// </summary>
    public static class DateExt
    {

        /// <summary>
        /// 时间戳转换为日期
        /// </summary>
        /// <param name="stamp">时间戳</param>
        /// <returns></returns>
        public static DateTime ToDate(this int stamp)
        {
            DateTime date = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long time = Convert.ToInt64(stamp + "0000000");
            TimeSpan span = new TimeSpan(time);
            return date.Add(span);
        }

        /// <summary>
        /// 日期转换为时间戳
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>时间戳</returns>
        public static int ToStamp(this DateTime date)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(date - startTime).TotalSeconds;
        }

        public static string ToRmk(this DateTime date)
        {
            System.TimeSpan ND = DateTime.Now - date;
            int minutes = (int)ND.TotalMinutes;

            if (minutes == 0)
            {
                return "刚刚";
            }
            else if (minutes >= 1 && minutes < 60)
            {
                return minutes.ToString() + "分钟前";
            }
            else if (minutes >= 60 && minutes < 60 * 24)
            {
                return Math.Floor((decimal)minutes / 60).ToString() + "小时前";
            }
            else if (minutes >= 60 * 24 && minutes < 60 * 24 * 30)
            {
                return Math.Floor((decimal)minutes / 60 / 24).ToString() + "天前";
            }
            else if (minutes > 60 * 24 * 30 && minutes < 60 * 24 * 30 * 12)
            {
                return Math.Floor((decimal)minutes / 60 / 24 / 30).ToString() + "月前";
            }
            else
                return date.Year.ToString() + "年";
        }

    }
}
