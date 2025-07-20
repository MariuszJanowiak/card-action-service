using System.ComponentModel.DataAnnotations;

namespace CardActionService.Api.Requests
{
    public class CardRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CardNumber { get; set; } = string.Empty;
    }
}