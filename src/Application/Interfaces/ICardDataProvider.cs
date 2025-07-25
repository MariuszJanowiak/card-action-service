using CardActionService.Domain.Models;

namespace CardActionService.Application.Interfaces
{
    public interface ICardDataProvider
    {
        Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber);
    }
}