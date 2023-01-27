namespace Unity.Duell.Server.Models.IO
{
    public class RoundResult
    {
        public List<RoundStep> Steps { get; internal set; }
        public int Player1HpStart { get; internal set; }
        public int Player2HpStart { get; internal set; }
        public int Player1HpEnd { get; internal set; }
        public int Player2HpEnd { get; internal set; }
    }
}