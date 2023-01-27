using static Unity.Duell.Server.Models.IO.Player;

namespace Unity.Duell.Server.Models.IO
{
    public class SetMovesRequest
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public PlayerMoves Moves { get; set; }

    }
}
