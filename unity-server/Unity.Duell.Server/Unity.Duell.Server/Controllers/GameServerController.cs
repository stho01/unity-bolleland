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
            _logger.LogInformation($"Starting new game");
            return _gameStateService.StartNewGame();
        }

        [HttpGet("GetGameState")]
        public GameStateResponse GetGameState(Guid id, Guid playerId)
        {
            _logger.LogInformation($"Getting game state for {id}");
            return _gameStateService.GetGameState(id, playerId);
        }

        [HttpPost("SetMoves")]
        public SetMovesResponse SetMoves(SetMovesRequest request)
        {
            _logger.LogInformation($"Setting moves for {request}");
            return _gameStateService.SetMoves(request);
        }

        [HttpGet("GetPreviousRound")]
        public GetPreviousRoundResponse GetPreviousRound(Guid id)
        {
            _logger.LogInformation($"Getting previous round for {id}");
            return _gameStateService.GetPreviousRound(id);

        }
    }
}