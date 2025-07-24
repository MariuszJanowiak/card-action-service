using CardActionService.Domain.Models;

namespace CardActionService.Application.Interfaces;

public interface ICardResponseFactory
{
    object CreateCardResponse(CardDetails card, List<string> actions);
}