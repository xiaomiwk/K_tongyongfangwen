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
    public class M接收事件Converter : JsonConverter
    {
        //_方法.形参列表.Find(q => q.名称 == __名称).元数据.
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(M接收事件));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
        {
            var __结果 = new M接收事件();
            JObject __jo = JObject.Load(reader);
            __结果.对象名称 = __jo["对象名称"].Value<string>();
            __结果.事件名称 = __jo["事件名称"].Value<string>();
            if (__jo["实参列表"] != null)
            {
                foreach (JObject __jo1 in __jo["实参列表"].ToArray())
                {
                    var __名称 = __jo1["名称"].Value<string>();
                    var __值 = __jo1["值"].ToString();
                    __结果.实参列表.Add(new M实参 { 名称 = __名称, 值 = __值 });
                }
            }
            //IPAddress address = jo["Address"].to<IPAddress>(serializer);
            //int port = jo["Port"].Value<int>();
            return __结果;
        }

        public override void WriteJson(JsonWriter __writer, object __value, JsonSerializer __serializer)
        {
            var __对象 = (M接收事件)__value;
            __writer.WriteStartObject();
            __writer.WritePropertyName("对象名称");
            __writer.WriteValue(__对象.对象名称);
            __writer.WritePropertyName("事件名称");
            __writer.WriteValue(__对象.事件名称);
            __writer.WritePropertyName("实参列表");
            __writer.WriteStartArray();
            if (__对象.实参列表 != null)
            {
                foreach (var __kv in __对象.实参列表)
                {
                    __writer.WriteStartObject();
                    __writer.WritePropertyName("名称");
                    __writer.WriteValue(__kv.名称);
                    __writer.WritePropertyName("值");

                    if (__kv.值.TrimStart().StartsWith("{") || __kv.值.TrimStart().StartsWith("["))
                    {
                        __writer.WriteRawValue(__kv.值);
                    }
                    else
                    {
                        __writer.WriteValue(__kv.值);
                    }
                    __writer.WriteEndObject();
                }
            }
            __writer.WriteEndArray();
            __writer.WriteEndObject();
        }

    }
}
