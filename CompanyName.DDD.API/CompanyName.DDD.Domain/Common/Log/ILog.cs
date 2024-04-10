namespace CompanyName.DDD.Domain.Common.Log
{
    public interface ILog
    {
        void Send(BodyLog body);
    }
}