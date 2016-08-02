using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utility.扩展
{
    /// <remarks>次类必须标注可序列化属性[Serializable]</remarks>
    public static class H复制对象
    {
        public static object 深复制(object 源对象)
        {
            //浅复制
            //this.MemberwiseClone();

            var ms = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(ms, 源对象);
            ms.Seek(0, 0);
            var value = bf.Deserialize(ms);
            ms.Close();
            return value;
        }

        public static T 深复制<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
