using CardActionService.Domain.Enums;
using CardActionService.Domain.Exceptions;
using CardActionService.Domain.Models;
using CardActionService.Infrastructure.Data.Models;

namespace CardActionService.Infrastructure.Mappers;

public static class CardDataMapper
{
    public static CardDetails Map(SqlCardDto dto)
    {
        if (dto == null)
            throw new DomainValidationException("Card data cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.CardNumber))
            throw new DomainValidationException("CardNumber is required.");

        if (!Enum.IsDefined(typeof(EnCardType), dto.CardType))
            throw new DomainValidationException("Invalid CardType.");

        if (!Enum.IsDefined(typeof(EnCardStatus), dto.CardStatus))
            throw new DomainValidationException("Invalid CardStatus.");

        return new CardDetails(
            dto.CardNumber,
            dto.CardType,
            dto.CardStatus,
            dto.IsPinSet ?? false
        );
    }

    public static CardDetails Map(SampleCardDto dto)
    {
        if (dto == null)
            throw new DomainValidationException("Card data cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.CardNumber))
            throw new DomainValidationException("CardNumber is required.");

        if (!Enum.IsDefined(typeof(EnCardType), dto.CardType))
            throw new DomainValidationException("Invalid CardType.");

        if (!Enum.IsDefined(typeof(EnCardStatus), dto.CardStatus))
            throw new DomainValidationException("Invalid CardStatus.");

        return new CardDetails(
            dto.CardNumber,
            (EnCardType)dto.CardType,
            (EnCardStatus)dto.CardStatus,
            dto.IsPinSet ?? false
        );
    }

    public static CardDetails Map(KafkaCardDto dto)
    {
        if (dto == null)
            throw new DomainValidationException("Card data cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.CardNumber))
            throw new DomainValidationException("CardNumber is required.");

        if (!dto.CardType.HasValue || !Enum.IsDefined(typeof(EnCardType), dto.CardType.Value))
            throw new DomainValidationException("Invalid CardType.");

        if (!dto.CardStatus.HasValue || !Enum.IsDefined(typeof(EnCardStatus), dto.CardStatus.Value))
            throw new DomainValidationException("Invalid CardStatus.");

        return new CardDetails(
            dto.CardNumber,
            (EnCardType)dto.CardType.Value,
            (EnCardStatus)dto.CardStatus.Value,
            dto.IsPinSet ?? false
        );
    }
}