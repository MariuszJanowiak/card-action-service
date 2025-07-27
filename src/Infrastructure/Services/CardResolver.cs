using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardResolver : ICardResolver
{
    private readonly IMatrixProvider _matrix;

    public CardResolver(IMatrixProvider matrix)
    {
        _matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        ValidateMatrix();
    }

    public List<string> ResolveMatrixAction(CardDetails card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (_matrix.ActionNames == null || _matrix.RuleMatrix == null)
            throw new InvalidOperationException("MatrixProvider data is not properly initialized.");

        var statusIndex = (int)card.CardStatus;

        if (statusIndex < 0 || statusIndex >= _matrix.RuleMatrix.GetLength(1))
            throw new ArgumentOutOfRangeException(nameof(card.CardStatus),
                $"CardStatus index {statusIndex} is out of range.");

        if (_matrix.RuleMatrix.GetLength(0) != _matrix.ActionNames.Length)
            throw new InvalidOperationException("Mismatch between ActionNames length and RuleMatrix rows count.");

        var result = new List<string>();
        var type = card.CardType;
        var pinSet = card.IsPinSet;

        for (var action = 0; action < _matrix.ActionNames.Length; action++)
        {
            var rule = _matrix.RuleMatrix[action, statusIndex];

            if (rule == EnActionFlag.No)
                continue;

            if (rule == EnActionFlag.UnlessNoPin && !pinSet) // U
                continue;

            if (rule == EnActionFlag.IfPinNotSet && pinSet) // P
                continue;

            if (rule == EnActionFlag.IfPinSet && !pinSet) // Q
                continue;

            //Exclude action 5 (index 4) for Prepaid or Debit card types
            if (action == 4 && (type == EnCardType.Prepaid || type == EnCardType.Debit))
                continue;

            result.Add(_matrix.ActionNames[action]);
        }

        return result;
    }

    private void ValidateMatrix()
    {
        if (_matrix.RuleMatrix == null)
            throw new InvalidOperationException("RuleMatrix cannot be null.");

        if (_matrix.ActionNames == null)
            throw new InvalidOperationException("ActionNames cannot be null.");

        if (_matrix.RuleMatrix.GetLength(0) != _matrix.ActionNames.Length)
            throw new InvalidOperationException("RuleMatrix rows count must match ActionNames length.");

        if (_matrix.RuleMatrix.GetLength(0) == 0 || _matrix.RuleMatrix.GetLength(1) == 0)
            throw new InvalidOperationException("RuleMatrix must have positive dimensions.");
    }
}