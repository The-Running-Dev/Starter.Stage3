using System;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Starter.Framework.Extensions
{
    /// <summary>
    /// Extension methods to the Object type
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToJson(this object data, bool format = false)
        {
            var jss = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var formatting = (format) ? Formatting.Indented : Formatting.None;

            return JsonConvert.SerializeObject(data, formatting, jss);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<object> ToNameValueList(this Type type)
        {
            var pairs =
                Enum.GetValues(type).Cast<object>()
                    .Select(value => new
                    {
                        Name = ((Enum)value).GetDescription(),
                        Value = (int) value
                    }).ToList();
            
            pairs.Append(new
            {
                Name = string.Empty,
                Value = -1
            });

            return pairs.OrderBy(pair => pair.Name);
        }
    }
}