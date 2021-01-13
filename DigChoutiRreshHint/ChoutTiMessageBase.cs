namespace DigChoutiRreshHint
{
    public class ChoutTiMessageBase
    {
        public Resp[] resp { get; set; }
    }

    public class Resp
    {
        public int id { get; set; }
        public bool enable { get; set; }
        public string content { get; set; }
        public long createTime { get; set; }
        public long activeTime { get; set; }
    }
}