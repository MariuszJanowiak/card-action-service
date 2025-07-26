using CardActionService.Application.Interfaces;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardService(ICardDataProvider dataProvider) : ICardService
{
    public async Task<CardDetails> GetCardDetails(string userId, string cardNumber)
    {
        var cardDetails = await dataProvider.GetCardDetailsAsync(userId, cardNumber);

        if (cardDetails == null)
            throw new KeyNotFoundException($"Card '{cardNumber}' not found for user '{userId}'.");

        cardDetails.DomainValidation();

        return cardDetails;
    }
}