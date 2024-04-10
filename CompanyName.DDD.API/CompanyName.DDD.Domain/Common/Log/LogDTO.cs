namespace CompanyName.DDD.Domain.Common.Log
{
    public record BodyLog(Guid ProcessId, string Environment, string Level, string Information, object Body);
}