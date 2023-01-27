namespace Unity.Duell.Server.Models.IO
{
    public class StartGameResponse : BaseResponse
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid OpponentId { get; set; }
    }
}
