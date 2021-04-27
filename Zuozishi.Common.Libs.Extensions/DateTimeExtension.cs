using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuozishi.Common.Libs.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取时间戳（毫秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimestampMilliseconds(this DateTime time) => (time.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        /// <summary>
        /// 获取时间戳（秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimestampSeconds(this DateTime time) => (time.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }
}
