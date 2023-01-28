namespace Unity.Duell.Server.Models.IO
{
    public class Game
    {
        public Guid Id { get; internal set; }
        public List<Player> Players { get; set; }
        public GameState State { get; internal set; }
        public List<RoundResult> Rounds { get; internal set; } = new List<RoundResult>();
    }
}