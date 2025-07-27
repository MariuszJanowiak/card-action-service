namespace CardActionServiceTests.Unit.Validators;

public class CardRequestValidatorTests
{
    private readonly CardRequestValidator _validator;

    public CardRequestValidatorTests()
    {
        var envMock = new Mock<IWebHostEnvironment>();
        envMock.Setup(e => e.EnvironmentName).Returns("Production");

        _validator = new CardRequestValidator(envMock.Object);
    }

    [Fact]
    public void UserId_Empty_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "", CardNumber = "4532015112830366" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void UserId_Null_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = null!, CardNumber = "4532015112830366" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void UserId_Whitespace_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "   ", CardNumber = "4532015112830366" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void CardNumber_InvalidLength_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "User2", CardNumber = "12345" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CardNumber);
    }

    [Fact]
    public void CardNumber_Null_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "User2", CardNumber = null! };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CardNumber);
    }

    [Fact]
    public void CardNumber_NonDigits_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "User2", CardNumber = "4532AB112830366" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CardNumber);
    }

    [Fact]
    public void CardNumber_ValidLuhn_ShouldNotHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "User1", CardNumber = "4532015112830366" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CardNumber);
    }

    [Fact]
    public void CardNumber_ValidLengthButInvalidLuhn_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CardRequest { UserId = "User1", CardNumber = "4532015112830367" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CardNumber);
    }

    [Fact]
    public void ValidModel_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var model = new CardRequest { UserId = "User1", CardNumber = "4532015112830366" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UserId);
        result.ShouldNotHaveValidationErrorFor(x => x.CardNumber);
    }
}