using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _allUserCards;

    public CardService()
    {
        _allUserCards = GenerateSampleUserCards();
    }

    public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
    {
        await Task.Delay(1000);

        if (!_allUserCards.TryGetValue(userId, out var cardsForUser))
            return null;

        if (!cardsForUser.TryGetValue(cardNumber, out var cardDetails))
            return null;

        return cardDetails;
    }

    private static Dictionary<string, Dictionary<string, CardDetails>> GenerateSampleUserCards()
    {
        var usersWithCards = new Dictionary<string, Dictionary<string, CardDetails>>();

        for (int userNumber = 1; userNumber <= 3; userNumber++)
        {
            var cardsForUser = new Dictionary<string, CardDetails>();
            int cardCounter = 1;

            foreach (var cardType in Enum.GetValues<EnCardType>())
            {
                foreach (var cardStatus in Enum.GetValues<EnCardStatus>())
                {
                    string cardNumber = $"Card{userNumber}{cardCounter}";
                    bool isPinSet = cardCounter % 2 == 0;

                    var card = new CardDetails(
                        CardNumber: cardNumber,
                        CardType: cardType,
                        CardStatus: cardStatus,
                        IsPinSet: isPinSet
                    );

                    cardsForUser[cardNumber] = card;
                    cardCounter++;
                }
            }

            string userId = $"User{userNumber}";
            usersWithCards[userId] = cardsForUser;
        }

        return usersWithCards;
    }
}
