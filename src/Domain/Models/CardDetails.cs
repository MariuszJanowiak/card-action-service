using CardActionService.Domain.Enums;

namespace CardActionService.Domain.Models
{
    /// <summary>
    /// Represents user card data and status
    /// </summary>
    public record CardDetails(
        string CardNumber,
        EnCardType CardType,
        EnCardStatus CardStatus,
        bool IsPinSet
    );
}