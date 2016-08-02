using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 通用访问.自定义序列化
{
    public class 时间Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            return DateTime.Parse(token.Value<string>());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime ip = (DateTime)value;
            writer.WriteValue(ip.ToString());
        }
    }
}
