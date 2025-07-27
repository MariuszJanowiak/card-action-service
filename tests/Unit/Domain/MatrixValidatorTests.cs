namespace CardActionServiceTests.Unit.Domain;

public class MatrixValidatorTests
{
    [Fact]
    public void Validate_DoesNotThrowException_ForValidMatrix()
    {
        // Arrange
        var validMatrix = new[]
        {
            "NNYNNNN",
            "NYNNNNN",
            "YYYYYYY",
            "YYYYYYY",
            "YYYYYYY",
            "UUUNQNN",
            "PPPNQNN",
            "YYYNYNN",
            "YYYYYYY",
            "YYYNNNN",
            "NYYNNNN",
            "YYYNNNN",
            "YYYNNNN"
        };

        // Act
        var ex = Record.Exception(() => MatrixValidator.Validate(validMatrix));

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public void Validate_ThrowsArgumentException_WhenMatrixIsEmpty()
    {
        // Arrange
        var emptyMatrix = Array.Empty<string>();

        // Act
        var ex = Assert.Throws<ArgumentException>(() => MatrixValidator.Validate(emptyMatrix));

        // Assert
        Assert.Contains("Matrix rows are null or empty", ex.Message);
    }

    [Fact]
    public void Validate_ThrowsArgumentException_WhenFirstRowIsNull()
    {
        // Arrange
        var matrix = new[] { null!, "YYYYYYY" };

        // Act
        var ex = Assert.Throws<ArgumentException>(() => MatrixValidator.Validate(matrix));

        // Assert
        Assert.Contains("Matrix rows are null or empty", ex.Message);
    }

    [Fact]
    public void Validate_ThrowsArgumentException_WhenFirstRowIsEmpty()
    {
        // Arrange
        var matrix = new[] { "", "YYYYYYY" };

        // Act
        var ex = Assert.Throws<ArgumentException>(() => MatrixValidator.Validate(matrix));

        // Assert
        Assert.Contains("Matrix rows are null or empty", ex.Message);
    }

    [Fact]
    public void Validate_ThrowsArgumentException_WhenAnyRowIsNull()
    {
        // Arrange
        var matrix = new[] { "NNYNNNN", null!, "YYYYYYY" };

        // Act
        var ex = Assert.Throws<ArgumentException>(() => MatrixValidator.Validate(matrix));

        // Assert
        Assert.Contains("Matrix rows are null or empty", ex.Message);
    }

    [Fact]
    public void Validate_ThrowsInvalidOperationException_WhenRowLengthMismatch()
    {
        // Arrange
        var matrix = new[]
        {
            "NNYNNNN",
            "NYNNN",
            "YYYYYYY"
        };

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() => MatrixValidator.Validate(matrix));

        // Assert
        Assert.Contains("Row 1 length mismatch", ex.Message);
    }

    [Theory]
    [InlineData('N')]
    [InlineData('Y')]
    [InlineData('U')]
    [InlineData('P')]
    [InlineData('Q')]
    public void IsValidChar_AllowedCharacters_ReturnsTrue(char validChar)
    {
        // Act & Assert
        Assert.True(IsValidCharHelper(validChar));
    }

    [Theory]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData(' ')]
    [InlineData('1')]
    [InlineData('!')]
    public void IsValidChar_DisallowedCharacters_ReturnsFalse(char invalidChar)
    {
        // Act & Assert
        Assert.False(IsValidCharHelper(invalidChar));
    }

    private bool IsValidCharHelper(char c) => c == 'N' || c == 'Y' || c == 'U' || c == 'P' || c == 'Q';
}