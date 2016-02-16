using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 通用访问.自定义序列化
{
    public class DictionaryConverter<K,V> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<K,V>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
        {
            var __结果 = new Dictionary<K, V>();
            JArray ja = JArray.Load(reader);
            foreach (JObject jo in ja)
            {
                K __key = jo["Key"].ToObject<K>(serializer);
                V __value = jo["Value"].ToObject<V>(serializer);
                __结果[__key] = __value;
            }
            //IPAddress address = jo["Address"].ToObject<IPAddress>(serializer);
            //int port = jo["Port"].Value<int>();
            return __结果;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var __dic = (Dictionary<K, V>)value;
            writer.WriteStartArray();
            foreach (var __kv in __dic)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Key");
                serializer.Serialize(writer, __kv.Key);
                writer.WritePropertyName("Value");
                writer.WriteValue(__kv.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
