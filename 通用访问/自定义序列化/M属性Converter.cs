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
    public class M属性Converter : JsonConverter
    {
        //_方法.形参列表.Find(q => q.名称 == __名称).元数据.
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(M属性));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
        {
            var __结果 = new M属性();
            JObject __jo = JObject.Load(reader);
            __结果.名称 = __jo["名称"].Value<string>();
            //__结果.值 = __jo["值"].ToString();
            __结果.元数据 = __jo["元数据"].ToObject<M元数据>();
            __结果.角色 = (E角色)__jo["角色"].Value<int>();
            //IPAddress address = jo["Address"].to<IPAddress>(serializer);
            //int port = jo["Port"].Value<int>();
            return __结果;
        }

        public override void WriteJson(JsonWriter __writer, object __value, JsonSerializer __serializer)
        {
            var __对象 = (M属性)__value;
            __writer.WriteStartObject();
            __writer.WritePropertyName("名称");
            __writer.WriteValue(__对象.名称);
            //__writer.WritePropertyName("值");
            //if (__对象.元数据 != null)
            //{
            //    if (__对象.元数据.结构 == E数据结构.点 && (__对象.元数据.类型 == "string" || __对象.元数据.类型 == "字符串"))
            //    {
            //        __writer.WriteValue(__对象.值);
            //    }
            //    else
            //    {
            //        __writer.WriteRawValue(__对象.值);
            //    }
            //}
            //else
            //{
            //    if (__对象.值.StartsWith("[") || __对象.值.StartsWith("{"))
            //    {
            //        __writer.WriteRawValue(__对象.值);
            //    }
            //    else
            //    {
            //        __writer.WriteValue(__对象.值);
            //    }
            //}
            __writer.WritePropertyName("元数据");
            __serializer.Serialize(__writer, __对象.元数据);
            __writer.WritePropertyName("角色");
            __writer.WriteValue(__对象.角色);
            __writer.WriteEndObject();
        }

    }
}
