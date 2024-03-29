﻿using System.Text;
using System.Text.Json;

namespace Zuozishi.Common.Libs.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Json格式化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PrettyJson(this string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                using var stream = new MemoryStream();
                var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                doc.WriteTo(writer);
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GUID
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GUID(int length = 32)
        {
            string res = "";
            while (length >= res.Length)
            {
                res += Guid.NewGuid().ToString().Replace("-", "");
            }
            return res.Length > length ? res.Substring(0, length) : res;
        }
    }
}
