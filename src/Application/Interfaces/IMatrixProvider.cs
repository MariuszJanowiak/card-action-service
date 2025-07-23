using CardActionService.Domain.Enums;

namespace CardActionService.Application.Interfaces;

public interface IMatrixProvider
{
    EnActionFlag[,] RuleMatrix { get; }
    string[] ActionNames { get; }
}