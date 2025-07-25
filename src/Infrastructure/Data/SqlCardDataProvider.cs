using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;
using CardActionService.Infrastructure.Data.Models;
using CardActionService.Infrastructure.Mappers;

namespace CardActionService.Infrastructure.Data;

public class SqlCardDataProvider : ICardDataProvider
{
    public Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards()
    {
        Console.WriteLine("PLACEHOLDER: Pretending to query SQL database");

        var rawData = new List<SqlCardDto>
        {
            new SqlCardDto
            {
                CardNumber = "DbCard001",
                CardType = EnCardType.Debit,
                CardStatus = EnCardStatus.Active,
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

        usersWithCards["UserDb1"] = cardsForUser;
        return usersWithCards;
    }
}