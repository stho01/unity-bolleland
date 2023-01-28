namespace Unity.Duell.Server.Models.IO
{
    public class FramePlayer
    {
        public Guid Id { get; internal set; }
        public Move Attack { get; internal set; }
        public Move Defence { get; internal set; }
        public int Hp { get; internal set; }
    }
}