using CardActionService.Application.Interfaces;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Data;

public class KafkaCardDataProvider : ICardDataProvider
{
    public Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards()
    {
        Console.WriteLine("PLACEHOLDER: Pretending to fetch data from Kafka");

        return new Dictionary<string, Dictionary<string, CardDetails>>();
    }
}