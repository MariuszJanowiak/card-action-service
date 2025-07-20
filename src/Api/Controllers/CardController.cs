using Microsoft.AspNetCore.Mvc;
using CardActionService.Application.Interfaces;
using CardActionService.Infrastructure.Services;
using CardActionService.Api.Requests;
using CardActionService.Api.Responses;

namespace CardActionService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly CardResolver _cardResolver;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardService cardService, CardResolver cardResolver, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _cardResolver = cardResolver;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CardResponse), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Get([FromQuery] CardRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for request: {@Request}", request);
                return BadRequest(ModelState);
            }

            var cardDetails = await _cardService.GetCardDetails(request.UserId, request.CardNumber);
            if (cardDetails == null)
            {
                _logger.LogInformation("Card not found: UserId={UserId}, CardNumber={CardNumber}", request.UserId, request.CardNumber);
                return NotFound("Card not found");
            }

            _logger.LogInformation("Resolving actions for Card: {@CardDetails}", cardDetails);

            var actions = _cardResolver.Resolve(cardDetails);

            _logger.LogInformation("Resolved actions: {@Actions}", actions);

            var response = new CardResponse(
                new CardSummary(
                    cardDetails.CardType.ToString(),
                    cardDetails.CardStatus.ToString(),
                    cardDetails.IsPinSet
                ),
                actions
            );

            return Ok(response);
        }
    }
}