namespace Unity.Duell.Server.Models.IO
{
    public class FrameResult
    {
        public Move Attack { get; internal set; }
        public Move Defence { get; internal set; }
        public int DefenderHpBefore { get; internal set; }
        public int DefenderHpAfter { get; internal set; }
        public Guid AttackerId { get; internal set; }
        public Guid DefenderId { get; internal set; }
    }
}