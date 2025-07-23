namespace CardActionService.Domain.Validators;

public static class MatrixValidator
{
    public static void Validate(string[] rows)
    {
        if (rows is not { Length: > 0 } || rows[0] == null)
            throw new ArgumentException("Matrix rows are null or empty.");

        var expectedLength = rows[0].Length;

        for (int rowIndex = 1; rowIndex < rows.Length; rowIndex++)
        {
            if (rows[rowIndex].Length != expectedLength)
                throw new InvalidOperationException(
                    $"Row {rowIndex} length mismatch: {rows[rowIndex].Length} != {expectedLength}");
        }
    }
}