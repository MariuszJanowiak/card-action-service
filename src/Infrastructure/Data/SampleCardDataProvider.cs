using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Data;

public class SampleCardDataProvider : ICardDataProvider
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _usersWithCards;

    public SampleCardDataProvider()
    {
        _usersWithCards = CreateSampleUsersWithCards();
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

    private Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUsersWithCards()
    {
        var usersWithCards = new Dictionary<string, Dictionary<string, CardDetails>>();

        for (var userNumber = 1; userNumber <= 3; userNumber++)
        {
            var cardsForUser = new Dictionary<string, CardDetails>();
            var cardCounter = 1;

            foreach (var cardType in Enum.GetValues<EnCardType>())
            {
                foreach (var cardStatus in Enum.GetValues<EnCardStatus>())
                {
                    var cardNumber = $"Card{userNumber}{cardCounter}";
                    var isPinSet = cardCounter % 2 == 0;

                    var card = new CardDetails(
                        cardNumber,
                        cardType,
                        cardStatus,
                        isPinSet
                    );

                    cardsForUser[cardNumber] = card;
                    cardCounter++;
                }
            }

            var userId = $"User{userNumber}";
            usersWithCards[userId] = cardsForUser;
        }

        return usersWithCards;
    }
}