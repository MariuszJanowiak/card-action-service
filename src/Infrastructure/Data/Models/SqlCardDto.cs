using CardActionService.Domain.Enums;

namespace CardActionService.Infrastructure.Data.Models;

public class SqlCardDto
{
    public string? CardNumber { get; set; }
    public EnCardType CardType { get; set; }
    public EnCardStatus CardStatus { get; set; }
    public bool? IsPinSet { get; set; }
}