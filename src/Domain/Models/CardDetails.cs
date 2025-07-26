using CardActionService.Domain.Enums;
using CardActionService.Domain.Exceptions;

namespace CardActionService.Domain.Models;

public record CardDetails(
    string CardNumber,
    EnCardType CardType,
    EnCardStatus CardStatus,
    bool IsPinSet)
{
    public void DomainValidation()
    {
        if (string.IsNullOrEmpty(CardNumber) || CardNumber.Length < 3 || CardNumber.Length > 19)
            throw new DomainValidationException("Invalid card number length.");

        // Additional validation
        if (CardStatus == EnCardStatus.Blocked)
            throw new DomainValidationException("Card is blocked.");
    }
}