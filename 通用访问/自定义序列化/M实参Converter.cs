using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using 通用访问.DTO;

namespace 通用访问.自定义序列化
{
    public class M实参Converter : JsonConverter
    {
        //_方法.形参列表.Find(q => q.名称 == __名称).元数据.
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(M实参));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
        {
            JObject __jo = JObject.Load(reader);
            var __名称 = __jo["名称"].Value<string>();
            var __值 = __jo["值"].ToString();
            return new M实参 { 名称 = __名称, 值 = __值 };
        }

        public override void WriteJson(JsonWriter __writer, object __value, JsonSerializer __serializer)
        {
            var __对象 = (M实参)__value;
            __writer.WriteStartObject();
            __writer.WritePropertyName("名称");
            __writer.WriteValue(__对象.名称);
            __writer.WritePropertyName("值");

            if (__对象.值 != null && (__对象.值.TrimStart().StartsWith("{") || __对象.值.TrimStart().StartsWith("[")))
            {
                __writer.WriteRawValue(__对象.值);
            }
            else
            {
                __writer.WriteValue(__对象.值);
            }
            __writer.WriteEndObject();
        }

    }
}
