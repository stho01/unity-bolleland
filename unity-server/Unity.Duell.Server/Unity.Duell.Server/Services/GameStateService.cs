using Unity.Duell.Server.Models.IO;

namespace Unity.Duell.Server.Services
{
    public interface IGameStateService
    {
        GameStateResponse GetGameState(string id);
    }

    public class GameStateService : IGameStateService
    {
        public GameStateResponse GetGameState(string id)
        {
            return new GameStateResponse { GameState = GameState.NewRound };
        }
    }
}
