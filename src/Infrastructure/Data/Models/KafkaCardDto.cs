namespace CardActionService.Infrastructure.Data.Models;

public class KafkaCardDto
{
    public string? CardNumber { get; set; }
    public int? CardType { get; set; }
    public int? CardStatus { get; set; }
    public bool? IsPinSet { get; set; }
}