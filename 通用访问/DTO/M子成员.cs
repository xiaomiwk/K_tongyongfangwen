namespace 通用访问.DTO
{
    public class M子成员
    {
        public string 名称 { get; set; }

        public M元数据 元数据 { get; set; }

        public M子成员()
        {
        }

        public M子成员(string __名称, M元数据 __元数据)
        {
            this.名称 = __名称;
            this.元数据 = __元数据;
        }

        public M子成员(string __名称, string __类型)
        {
            this.名称 = __名称;
            this.元数据 = new M元数据(__类型);
        }

        public M子成员(string __名称, string __类型,E数据结构 __结构)
        {
            this.名称 = __名称;
            this.元数据 = new M元数据(__类型, __结构);
        }
    }
}
