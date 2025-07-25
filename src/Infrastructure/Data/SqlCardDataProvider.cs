using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Data
{
    public class SqlCardDataProvider : ICardDataProvider
    {
        // DB Simulator
        // Place for SQL source
        private readonly Dictionary<string, Dictionary<string, CardDetails>> _usersWithCards;

        public SqlCardDataProvider()
        {
            _usersWithCards = CreateSampleUsersWithCards();
        }

        public Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
        {
            if (_usersWithCards.TryGetValue(userId, out var cardsForUser) &&
                cardsForUser.TryGetValue(cardNumber, out var cardDetails))
            {
                return Task.FromResult<CardDetails?>(cardDetails);
            }

            return Task.FromResult<CardDetails?>(null);
        }

        private Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUsersWithCards()
        {
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
}