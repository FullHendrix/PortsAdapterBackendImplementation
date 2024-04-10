using System.Text.Json;
namespace CompanyName.DDD.Domain.Common.Log.LogImplementations
{
    public class StandartOuputLog : ILog
    {
        public void Send(BodyLog body)
        {
            var stringLox = JsonSerializer.Serialize(body);
            Console.WriteLine(stringLox);
        }
    }
}