
namespace 通用访问.DTO
{
    public class M通知
    {
        /// <summary>
        /// 普遍, 重要, 严重
        /// </summary>
        public E通知重要性 重要性 { get; set; }

        public E角色 角色 { get; set; }

        public string 对象 { get; set; }

        public string 概要 { get; set; }

        public string 详细 { get; set; }

        public override string ToString()
        {
            return string.Format("{0}  [{1}]  {2}  {3}", 重要性, 对象, 概要, 详细);
        }

    }
}
