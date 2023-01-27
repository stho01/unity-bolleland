namespace Unity.Duell.Server.Models.IO
{
    public class StartGameRequest
    {
        public Player GameStarter { get; set; }
        public Player Opponent { get; set; }
    }
}
