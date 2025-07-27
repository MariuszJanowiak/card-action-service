using CardActionService.Domain.Parsers;

namespace CardActionServiceTests.Unit.Domain;

public class MatrixParserTests
{
    [Fact]
    public void Parse_ValidMatrix_ReturnsExpectedEnumMatrix()
    {
        // Arrange
        string[] rawMatrix = new[]
        {
            "NY",
            "QU"
        };

        // Act
        var matrix = MatrixParser.Parse(rawMatrix);

        // Assert
        Assert.Equal(EnActionFlag.No, matrix[0, 0]);
        Assert.Equal(EnActionFlag.Yes, matrix[0, 1]);
        Assert.Equal(EnActionFlag.IfPinSet, matrix[1, 0]);
        Assert.Equal(EnActionFlag.UnlessNoPin, matrix[1, 1]);
    }

    [Fact]
    public void Parse_InvalidChar_ThrowsInvalidOperationException()
    {
        // Arrange
        string[] rawMatrix = new[]
        {
            "NY",
            "ZX"
        };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => MatrixParser.Parse(rawMatrix));
        Assert.Contains("Invalid char 'Z' at row 1, col 0", ex.Message);
    }
}