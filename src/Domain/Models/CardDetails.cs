using CardActionService.Domain.Enums;

namespace CardActionService.Domain.Models
{
    /// <summary>
    /// Represents user card data and status
    /// </summary>
    public class CardDetails
    {
        public Guid UserId { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public EnCardType Type { get; set; }
        public EnCardStatus Status { get; set; }
    }
}