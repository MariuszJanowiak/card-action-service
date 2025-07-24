using Microsoft.AspNetCore.Mvc;
using CardActionService.Application.Interfaces;
using CardActionService.Infrastructure.Services;
using CardActionService.Api.Requests;

namespace CardActionService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CardController(
        ICardService cardService,
        CardResolver cardResolver,
        ICardResponseFactory responseFactory,
        ILogger<CardController> logger)
        : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Get([FromQuery] CardRequest request)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for request: {@Request}", request);
                return BadRequest(ModelState);
            }

            var cardDetails = await cardService.GetCardDetails(request.UserId, request.CardNumber);
            if (cardDetails == null)
            {
                logger.LogInformation("Card not found: UserId={UserId}, CardNumber={CardNumber}", request.UserId,
                    request.CardNumber);
                return NotFound("Card not found");
            }

            logger.LogInformation("Resolving actions for Card: {@CardDetails}", cardDetails);
            var actions = cardResolver.ResolveMatrixAction(cardDetails);
            logger.LogInformation("Resolved actions: {@Actions}", actions);

            var response = responseFactory.CreateCardResponse(cardDetails, actions);

            return Ok(response);
        }
    }
}