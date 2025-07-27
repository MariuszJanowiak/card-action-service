namespace CardActionServiceTests.Unit.Domain;

public class CardResolverTests
{
    private readonly CardResolver _resolver;

    public CardResolverTests()
    {
        var matrixProvider = new MatrixProvider();
        _resolver = new CardResolver(matrixProvider);
    }

    [Theory]
    [InlineData(EnCardType.Credit, EnCardStatus.Active, true,
        new[]
        {
            "ACTION1", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11",
            "ACTION12", "ACTION13"
        })]
    [InlineData(EnCardType.Prepaid, EnCardStatus.Ordered, false,
        new[] { "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13" })]
    public void ResolveMatrixAction_ValidCard_ReturnsExpectedActions(
        EnCardType cardType,
        EnCardStatus cardStatus,
        bool isPinSet,
        string[] expectedActions)
    {
        // Arrange
        var card = new CardDetails("TestCardNumber", cardType, cardStatus, isPinSet);

        // Act
        var actions = _resolver.ResolveMatrixAction(card);

        // Assert
        Assert.NotNull(actions);
        Assert.Equal(expectedActions.OrderBy(x => x), actions.OrderBy(x => x));
    }

    [Fact]
    public void ResolveMatrixAction_NullCard_ThrowsArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => _resolver.ResolveMatrixAction(null!));
    }

    [Fact]
    public void ResolveMatrixAction_InvalidCardStatus_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var invalidStatus = (EnCardStatus)(-1);
        var card = new CardDetails("TestCardNumber", EnCardType.Credit, invalidStatus, true);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _resolver.ResolveMatrixAction(card));
    }
}