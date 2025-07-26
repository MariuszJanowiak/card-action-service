using CardActionService.Domain.Models;

namespace CardActionService.Application.Interfaces;

public interface ICardResolver
{
    List<string> ResolveMatrixAction(CardDetails card);
}