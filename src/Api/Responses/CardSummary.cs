namespace CardActionService.Api.Responses;

public class CardSummary(string cardType, string cardStatus, bool isPinSet)
{
    public string CardType { get; } = cardType;
    public string CardStatus { get; } = cardStatus;
    public bool IsPinSet { get; } = isPinSet;
}