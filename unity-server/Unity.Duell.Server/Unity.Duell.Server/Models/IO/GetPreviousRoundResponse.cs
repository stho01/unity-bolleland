namespace Unity.Duell.Server.Models.IO
{
    public class GetPreviousRoundResponse : BaseResponse
    {
        public RoundResult Round { get; internal set; }
    }
}
