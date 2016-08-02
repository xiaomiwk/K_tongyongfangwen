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
    public class M方法执行结果Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(M方法执行结果));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
        {
            var __结果 = new M方法执行结果();
            JObject jo = JObject.Load(reader);
            __结果.成功 = jo["成功"].Value<bool>();
            __结果.描述 = jo["描述"].Value<string>();
            __结果.返回值 = jo["返回值"] == null ? "" : jo["返回值"].ToString();
            return __结果;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var __对象 = (M方法执行结果)value;
            writer.WriteStartObject();
            writer.WritePropertyName("成功");
            writer.WriteValue(__对象.成功);
            writer.WritePropertyName("描述");
            writer.WriteValue(__对象.描述);
            writer.WritePropertyName("返回值");
            if (__对象.返回值 != null && (__对象.返回值.StartsWith("[") || __对象.返回值.StartsWith("{")))
            {
                writer.WriteRawValue(__对象.返回值);
            }
            else
            {
                writer.WriteValue(__对象.返回值);
            }
            writer.WriteEndObject();
        }

    }
}
