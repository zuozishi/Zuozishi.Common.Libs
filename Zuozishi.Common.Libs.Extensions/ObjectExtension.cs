﻿using System.Text.Json;

namespace Zuozishi.Common.Libs.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 使用Json序列化克隆Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CloneUseJson<T>(this Object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
