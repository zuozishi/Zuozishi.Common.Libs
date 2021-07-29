using System.Collections.Generic;
using System.Linq;

namespace Zuozishi.Common.Libs.Extensions
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 数组转分隔符字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="SplitChar">分隔符</param>
        /// <returns></returns>
        public static string ToString<T>(this IEnumerable<T> array, string SplitChar)
        {
            if (array == null || array.Count() == 0 || SplitChar == null) return "";
            var pts = array.Where(o => o.ToString().Length > 0).ToArray();
            if (pts.Length == 0) return "";
            string str = "";
            foreach (var item in pts)
            {
                str += item.ToString() + SplitChar;
            }
            int len = SplitChar.Length;
            return str.Substring(0, str.Length - len);
        }

        /// <summary>
        /// 查找元素所在的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> array, T item)
        {
            if (array == null) return -1;
            int index = 0;
            foreach (var ele in array)
            {
                if (ele.Equals(item))
                    return index;
                index += 1;
            }
            return -1;
        }
    }
}
