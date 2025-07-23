using CardActionService.Domain.Models;

namespace CardActionService.Application.Interfaces;

public interface ICardService
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
}