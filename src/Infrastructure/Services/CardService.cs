using CardActionService.Application.Interfaces;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardService : ICardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _allUserCards;

    public CardService(ICardDataProvider dataProvider)
    {
        _allUserCards = dataProvider.GetAllUserCards();
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
}