namespace CardActionService.Api.Responses;

public class CardResponse(CardSummary card, IEnumerable<string> actions)
{
    public CardSummary Card { get; } = card;
    public IEnumerable<string> Actions { get; } = actions;
}