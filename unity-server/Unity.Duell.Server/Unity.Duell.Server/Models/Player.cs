namespace Unity.Duell.Server.Models.IO
{
    public partial class Player
    {
        public string Email { get; set; }
        public Guid Id { get; set; }        
        public PlayerMoves Moves { get; set; }
        public int Hp { get; set; }
    }
}