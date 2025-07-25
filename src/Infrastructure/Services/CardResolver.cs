using CardActionService.Application.Interfaces;
using CardActionService.Domain.Enums;
using CardActionService.Domain.Models;

namespace CardActionService.Infrastructure.Services;

public class CardResolver(IMatrixProvider matrix) : ICardResolver
{
    public List<string> ResolveMatrixAction(CardDetails card)
    {
        var result = new List<string>();
        var statusIndex = (int)card.CardStatus;
        var type = card.CardType;
        var pinSet = card.IsPinSet;

        for (var action = 0; action < matrix.ActionNames.Length; action++)
        {
            var rule = matrix.RuleMatrix[action, statusIndex];

            if (rule == EnActionFlag.No)
                continue;
            if (rule == EnActionFlag.UnlessNoPin && !pinSet)
                continue;
            if (rule == EnActionFlag.IfPinSet && !pinSet)
                continue;
            if (rule == EnActionFlag.IfPinNotSet && pinSet)
                continue;

            if (action == 4 && (type == EnCardType.Prepaid || type == EnCardType.Debit))
                continue;

            result.Add(matrix.ActionNames[action]);
        }

        return result;
    }
}