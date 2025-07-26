using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Parsers;
using CardActionService.Domain.Validators;

namespace CardActionService.Domain.Providers;

public class MatrixProvider : IMatrixProvider
{
    private readonly string[] _rawMatrixRows =
    [
        "NNYNNNN", // ACTION1
        "NYNNNNN", // ACTION2
        "YYYYYYY", // ACTION3
        "YYYYYYY", // ACTION4
        "YYYYYYY", // ACTION5
        "UUUNPNN", // ACTION6
        "QQQNPNN", // ACTION7
        "YYYNYNN", // ACTION8
        "YYYYYYY", // ACTION9
        "YYYNNNN", // ACTION10
        "NYYNNNN", // ACTION11
        "YYYNNNN", // ACTION12
        "YYYNNNN" // ACTION13
    ];

    public MatrixProvider()
    {
        MatrixValidator.Validate(_rawMatrixRows);
        RuleMatrix = MatrixParser.Parse(_rawMatrixRows);
        ActionNames = Enumerable.Range(1, _rawMatrixRows.Length)
            .Select(index => $"ACTION{index}")
            .ToArray();
    }
    
    public EnActionFlag[,] RuleMatrix { get; }
    public string[] ActionNames { get; }
}