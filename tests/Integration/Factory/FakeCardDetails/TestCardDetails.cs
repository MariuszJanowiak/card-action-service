namespace CardActionServiceTests.Integration.Factory.FakeCardDetails;

public record TestCardDetails(
    string CardNumber,
    EnCardStatus CardStatus,
    EnCardType CardType,
    bool IsPinSet
) : CardDetails(CardNumber, CardType, CardStatus, IsPinSet);