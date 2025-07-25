using CardActionService.Application.Interfaces;
using CardActionService.Domain.Models;
using CardActionService.Domain.Enums;
using CardActionService.Infrastructure.Data.Models;
using CardActionService.Infrastructure.Mappers;

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

                    var dto = new SampleCardDto
                    {
                        CardNumber = cardNumber,
                        CardType = (int)cardType,
                        CardStatus = (int)cardStatus,
                        IsPinSet = isPinSet
                    };

                    var card = CardDataMapper.Map(dto);

                    cardsForUser[card.CardNumber] = card;
                    cardCounter++;
                }
            }

            var userId = $"User{userNumber}";
            usersWithCards[userId] = cardsForUser;
        }

        return usersWithCards;
    }
}