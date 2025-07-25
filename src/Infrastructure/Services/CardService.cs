using CardActionService.Application.Interfaces;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardService(ICardDataProvider dataProvider) : ICardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _allUserCards = dataProvider.GetAllUserCards();

    public async Task<CardDetails> GetCardDetails(string userId, string cardNumber)
    {
        await Task.Delay(1000);

        if (!_allUserCards.TryGetValue(userId, out var cardsForUser))
            throw new KeyNotFoundException($"User '{userId}' not found.");

        if (!cardsForUser.TryGetValue(cardNumber, out var cardDetails))
            throw new KeyNotFoundException($"Card '{cardNumber}' not found for user '{userId}'.");

        cardDetails.DomainValidation();

        return cardDetails;
    }
}