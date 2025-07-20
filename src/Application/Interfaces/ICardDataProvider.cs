using CardActionService.Domain.Models;

namespace CardActionService.Application.Interfaces;

public interface ICardDataProvider
{
    Dictionary<string, Dictionary<string, CardDetails>> GetAllUserCards();
}