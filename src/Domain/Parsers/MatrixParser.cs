using CardActionService.Domain.Enums;
using CardActionService.Domain.Mappers;

namespace CardActionService.Domain.Parsers;

public static class MatrixParser
{
    public static EnActionFlag[,] Parse(string[] rows)
    {
        var rowCount = rows.Length;
        var columnCount = rows[0].Length;
        var matrix = new EnActionFlag[rowCount, columnCount];

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var line = rows[rowIndex];
            for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                var character = line[columnIndex];
                if (!ActionFlagMapper.SymbolMap.TryGetValue(character, out var flag))
                    throw new InvalidOperationException(
                        $"Invalid char '{character}' at row {rowIndex}, col {columnIndex}");

                matrix[rowIndex, columnIndex] = flag;
            }
        }

        return matrix;
    }
}