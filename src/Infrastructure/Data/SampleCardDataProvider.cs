using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Data;

public class SampleCardDataProvider : ICardDataProvider
{
    public Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards()
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
                        cardNumber,
                        cardType,
                        cardStatus,
                        isPinSet
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