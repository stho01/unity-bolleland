namespace Unity.Duell.Server.Models.IO
{
    public abstract class BaseResponse
    {
        public Result Result { get; set; } = new Result();
    }
}