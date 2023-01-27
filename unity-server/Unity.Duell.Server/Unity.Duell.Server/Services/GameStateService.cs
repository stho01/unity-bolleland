using System.Numerics;
using Unity.Duell.Server.Models;
using Unity.Duell.Server.Models.IO;

namespace Unity.Duell.Server.Services
{
    public interface IGameStateService
    {
        GameStateResponse GetGameState(Guid gameId, Guid playerId);
        GetPreviousRoundResponse GetPreviousRound(Guid gameId);
        SetMovesResponse SetMoves(SetMovesRequest request);
        StartGameResponse StartNewGame();
    }

    public class GameStateService : IGameStateService
    {
        private static IDictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

        public GameStateResponse GetGameState(Guid gameId, Guid playerId)
        {
            int numberOfPlayersWithMoves = 0;
            if (!_games.TryGetValue(gameId, out var game))
            {
                return new GameStateResponse() { Result = new Result { ResultCode = ResultCode.NotFound, Description = "Game not found" } };
            }
            var player = game.Players.FirstOrDefault(player => player.Id == playerId);
            if (player == null)
            {
                return new GameStateResponse() { Result = new Result { ResultCode = ResultCode.NotFound, Description = "Player not found" } };

            }
            if (player.Moves == null) 
            {
                return new GameStateResponse { GameState = GameState.WaitingForPlayer, NumberOfRoundsPlayed = game.Rounds.Count };
            }

            foreach (var playerInGame in game.Players)
            {
                if (playerInGame.Moves != null)
                {
                    numberOfPlayersWithMoves++;
                }
            }

            if (numberOfPlayersWithMoves != 2)
            {
                game.State = GameState.WaitingForOpponent;
            }

            return new GameStateResponse { GameState = game.State, NumberOfRoundsPlayed = game.Rounds.Count };
        }

        public SetMovesResponse SetMoves(SetMovesRequest request)
        {
            int numberOfPlayersWithMoves  = 0;
            if (!_games.TryGetValue(request.GameId, out var game))
            {
                return new SetMovesResponse(){ Result = new Result { ResultCode = ResultCode.NotFound, Description = "Game not found" }};
            }
            var player = game.Players.FirstOrDefault(player => player.Id == request.PlayerId);
            if (player == null)
            {
                return new SetMovesResponse() { Result = new Result { ResultCode = ResultCode.NotFound, Description = "Player not found" } };

            }
            player.Moves = request.Moves;
            foreach (var playerInGame in game.Players)
            {
                if (playerInGame.Moves != null)
                {
                    numberOfPlayersWithMoves++;
                }                
            }

            if (numberOfPlayersWithMoves != 2)
            {
                game.State = GameState.WaitingForOpponent;
            }
            else
            {
                var roundResult = CalculateRound(game.Players[0], game.Players[1]);
                game.Rounds.Add(roundResult);
                if (roundResult != null && (roundResult.Player1HpEnd < 0 || roundResult.Player2HpEnd < 0))
                {
                    game.State = GameState.Finished;
                }
                else
                {
                    game.Players[0].Moves = null;
                    game.Players[1].Moves = null;                    
                }
            }
            return new SetMovesResponse();
        }

        private RoundResult CalculateRound(Player player1, Player player2)
        {
            RoundResult result = new RoundResult();
            result.Player1HpStart = player1.Hp;
            result.Player2HpStart = player1.Hp;
            result.Steps = new List<RoundStep>();
            
            RoundStep step1 = new RoundStep();
            var defenderHpAfterAttack = CalculateHpAfterAttack(player1.Moves.Attack1, player2.Moves.Defence1, player2.Hp);
            step1.Frame1 = new FrameResult()
            {
                Attack = player1.Moves.Attack1,
                Defence = player2.Moves.Defence1,
                DefenderHpBefore = player2.Hp,
                DefenderHpAfter = defenderHpAfterAttack,
                AttackerId = player1.Id,
                DefenderId = player2.Id
            };
            player2.Hp = defenderHpAfterAttack;

            defenderHpAfterAttack = CalculateHpAfterAttack(player2.Moves.Attack1, player1.Moves.Defence1, player1.Hp);
            step1.Frame2 = new FrameResult()
            {
                Attack = player2.Moves.Attack1,
                Defence = player1.Moves.Defence1,
                DefenderHpBefore = player1.Hp,
                DefenderHpAfter = defenderHpAfterAttack,
                AttackerId = player2.Id,
                DefenderId = player1.Id
            };
            player1.Hp = defenderHpAfterAttack;
            result.Steps.Add(step1);

            RoundStep step2 = new RoundStep();
            defenderHpAfterAttack = CalculateHpAfterAttack(player1.Moves.Attack2, player2.Moves.Defence2, player2.Hp);
            step2.Frame1 = new FrameResult()
            {
                Attack = player1.Moves.Attack2,
                Defence = player2.Moves.Defence2,
                DefenderHpBefore = player2.Hp,
                DefenderHpAfter = defenderHpAfterAttack,
                AttackerId = player1.Id,
                DefenderId = player2.Id
            };
            player2.Hp = defenderHpAfterAttack;

            defenderHpAfterAttack = CalculateHpAfterAttack(player2.Moves.Attack2, player1.Moves.Defence2, player1.Hp);
            step2.Frame2 = new FrameResult()
            {
                Attack = player2.Moves.Attack2,
                Defence = player1.Moves.Defence2,
                DefenderHpBefore = player1.Hp,
                DefenderHpAfter = defenderHpAfterAttack,
                AttackerId = player2.Id,
                DefenderId = player1.Id
            };
            player1.Hp = defenderHpAfterAttack;
            result.Steps.Add(step2);

            RoundStep step3 = new RoundStep();
            defenderHpAfterAttack = CalculateHpAfterAttack(player1.Moves.Attack3, player2.Moves.Defence3, player2.Hp);
            step3.Frame1 = new FrameResult()
            {
                Attack = player1.Moves.Attack3,
                Defence = player2.Moves.Defence3,
                DefenderHpBefore = player2.Hp,
                DefenderHpAfter = defenderHpAfterAttack,
                AttackerId = player1.Id,
                DefenderId = player2.Id
            };
            player2.Hp = defenderHpAfterAttack;

            defenderHpAfterAttack = CalculateHpAfterAttack(player2.Moves.Attack3, player1.Moves.Defence3, player1.Hp);
            step3.Frame2 = new FrameResult()
            {
                Attack = player2.Moves.Attack3,
                Defence = player1.Moves.Defence3,
                DefenderHpBefore = player1.Hp,
                DefenderHpAfter = defenderHpAfterAttack,
                AttackerId = player2.Id,
                DefenderId = player1.Id
            };
            player1.Hp = defenderHpAfterAttack;
            result.Steps.Add(step3);

            result.Player1HpEnd = player1.Hp;
            result.Player2HpEnd = player2.Hp;

            return result;
        }

        private int CalculateHpAfterAttack(Move attacker, Move defender, int defenderHp)
        {
            int hp = defenderHp;
            if (attacker == Move.High && defender != Move.High)
            {
                hp -= 10;
            }
            else if (attacker == Move.Medium && defender != Move.Medium)
            {
                hp -= 10;
            }
            else if (attacker == Move.Low && defender != Move.Low)
            {
                hp -= 10;
            }
            return hp;
        }       

        public StartGameResponse StartNewGame()
        {
            Game game = new Game();
            game.Id = Guid.NewGuid();
            game.Players = new List<Player>() { new Player() { Id = Guid.NewGuid(), Hp = 100 }, new Player { Id = Guid.NewGuid(), Hp = 100 } };
            game.State = GameState.WaitingForPlayer;
            _games.Add(game.Id, game);
            return new StartGameResponse
            {
                GameId = game.Id,                
                PlayerId = game.Players[0].Id, 
                OpponentId = game.Players[1].Id
            };
        }

        public GetPreviousRoundResponse GetPreviousRound(Guid gameId)
        {
            if (!_games.TryGetValue(gameId, out var game))
            {
                return new GetPreviousRoundResponse() { Result = new Result { ResultCode = ResultCode.NotFound, Description = "Game not found" } };
            }

            if (game.Rounds != null && game.Rounds.Count > 0) 
            {
                return new GetPreviousRoundResponse()
                {
                    Round = game.Rounds.Last()
                };
            }
            return new GetPreviousRoundResponse() { Result = new Result() { ResultCode = ResultCode.NotFound, Description = "No round found" } };
        }
    }
}
