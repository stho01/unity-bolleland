namespace Unity.Duell.Server.Models.IO
{
    public class GameStateResponse : BaseResponse
    {
        public GameState GameState { get; set; }
        public int NumberOfRoundsPlayed { get; set; }   
    }
}
