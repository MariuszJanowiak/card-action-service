using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;
using CardActionService.Infrastructure.Data.Models;
using CardActionService.Infrastructure.Mappers;

namespace CardActionService.Infrastructure.Data;

public class KafkaCardDataProvider : ICardDataProvider
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _usersWithCards;

    public KafkaCardDataProvider()
    {
        _usersWithCards = LoadDataFromKafka();
    }

    public Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
    {
        if (_usersWithCards.TryGetValue(userId, out var cardsForUser)
            && cardsForUser.TryGetValue(cardNumber, out var cardDetails))
        {
            return Task.FromResult<CardDetails?>(cardDetails);
        }

        return Task.FromResult<CardDetails?>(null);
    }

    private Dictionary<string, Dictionary<string, CardDetails>> LoadDataFromKafka()
    {
        // Temporary data
        // Place for Kafka source
        var rawData = new List<KafkaCardDto>
        {
            new()
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