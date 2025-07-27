using CardActionService.Application.Contract;
using CardActionService.Application.Interfaces;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardResponseFactory(IHostEnvironment env, ILogger<CardResponseFactory> logger) : ICardResponseFactory
{
    public CardResponse CreateCardResponse(CardDetails cardDetails, List<string> actions)
    {
        // This section can be extended in the future to include
        // environment-specific behaviors such as enhanced debug logging,
        // detailed tracing, or dynamic response shaping depending on the context.
        if (env.IsDevelopment())
        {
            logger.LogInformation("Card details: Type={CardType}, Status={CardStatus}, IsPinSet={IsPinSet}",
                cardDetails.CardType,
                cardDetails.CardStatus,
                cardDetails.IsPinSet);
        }

        var summary = new CardSummary(
            cardDetails.CardType.ToString(),
            cardDetails.CardStatus.ToString(),
            cardDetails.IsPinSet
        );

        return new CardResponse(summary, actions);
    }
}