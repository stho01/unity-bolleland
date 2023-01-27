using Microsoft.AspNetCore.Mvc;
using Unity.Duell.Server.Models.IO;
using Unity.Duell.Server.Services;

namespace Unity.Duell.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameServerController : ControllerBase
    {
        private readonly ILogger<GameServerController> _logger;
        private readonly IGameStateService _gameStateService;

        public GameServerController(ILogger<GameServerController> logger, IGameStateService gameStateService)
        {
            _logger = logger;
            _gameStateService = gameStateService;
        }

        [HttpPost("StartGame")]
        public StartGameResponse StartGame(StartGameRequest request)
        {
            _logger.LogInformation($"Starting new game for {request.GameStarter.Email}, inviting {request.Opponent.Email}");
            return new StartGameResponse { GameId = Guid.NewGuid() };
        }

        [HttpGet("GetGameState")]
        public GameStateResponse GetGameState(string id)
        {
            return _gameStateService.GetGameState(id);
        }
    }
}