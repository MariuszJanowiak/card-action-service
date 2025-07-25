using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;
using CardActionService.Infrastructure.Data.Models;
using CardActionService.Infrastructure.Mappers;

namespace CardActionService.Infrastructure.Data;

public class KafkaCardDataProvider : ICardDataProvider
{
    public Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards()
    {
        Console.WriteLine("PLACEHOLDER: Pretending to fetch data from Kafka");

        var rawData = new List<KafkaCardDto>
        {
            new KafkaCardDto
            {
                CardNumber = "KafkaCard001",
                CardType = (int)EnCardType.Credit,
                CardStatus = (int)EnCardStatus.Active,
                IsPinSet = true
            }
        };

        var usersWithCards = new Dictionary<string, Dictionary<string, CardDetails>>();
        var cardsForUser = new Dictionary<string, CardDetails>();

        foreach (var dto in rawData)
        {
            var card = CardDataMapper.Map(dto);
            cardsForUser[card.CardNumber] = card;
        }

        usersWithCards["UserKafka1"] = cardsForUser;

        return usersWithCards;
    }
}