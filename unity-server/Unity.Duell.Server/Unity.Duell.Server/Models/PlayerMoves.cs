namespace Unity.Duell.Server.Models.IO
{
    public partial class Player
    {
        public class PlayerMoves 
        {
            public Move Attack1 { get; set; }
            public Move Attack2 { get; set; }
            public Move Attack3 { get; set; }
            public Move Defence1 { get; set; }
            public Move Defence2 { get; set; }
            public Move Defence3 { get; set; }
        }
    }
}