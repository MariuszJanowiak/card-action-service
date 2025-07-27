using CardActionService.Application.Contract;
using CardActionService.Domain.Models;

namespace CardActionService.Application.Interfaces;

public interface ICardResponseFactory
{
    CardResponse CreateCardResponse(CardDetails card, List<string> actions);
}