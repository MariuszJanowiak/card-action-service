namespace CardActionService.Application.Contract;

public class CardSummary(string cardType, string cardStatus, bool isPinSet)
{
    public string CardType { get; } = cardType;
    public string CardStatus { get; } = cardStatus;
    public bool IsPinSet { get; } = isPinSet;
}
