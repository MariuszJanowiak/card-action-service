using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Data;

public class SqlCardDataProvider : ICardDataProvider
{
    public Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards()
    {
        Console.WriteLine("PLACEHOLDER: Pretending to query SQL database");

        return new Dictionary<string, Dictionary<string, CardDetails>>
        {
            {
                "UserDb1", new Dictionary<string, CardDetails>
                {
                    {
                        "DbCard001", new CardDetails(
                            CardNumber: "DbCard001",
                            CardType: EnCardType.Debit,
                            CardStatus: EnCardStatus.Active,
                            IsPinSet: true
                        )
                    }
                }
            }
        };
    }
}