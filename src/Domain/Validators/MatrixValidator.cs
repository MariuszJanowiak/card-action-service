namespace CardActionService.Domain.Validators;

public static class MatrixValidator
{
    public static void Validate(string[] rows)
    {
        if (rows == null || rows.Length == 0 || rows[0] == null || rows[0] == "")
            throw new ArgumentException("Matrix rows are null or empty.");

        var expectedLength = rows[0].Length;

        for (var rowIndex = 1; rowIndex < rows.Length; rowIndex++)
        {
            if (rows[rowIndex] == null)
                throw new ArgumentException("Matrix rows are null or empty.");

            if (rows[rowIndex].Length != expectedLength)
                throw new InvalidOperationException(
                    $"Row {rowIndex} length mismatch: {rows[rowIndex].Length} != {expectedLength}");
        }
    }
}