using System.Diagnostics.CodeAnalysis;
using CardActionService.Api.Requests;
using CardActionService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardActionService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class CardController(
    ICardService cardService,
    ICardResolver cardResolver,
    ICardResponseFactory responseFactory,
    ILogger<CardController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public async Task<IActionResult> Get([FromQuery] CardRequest request)
    {
        var cardDetails = await cardService.GetCardDetails(request.UserId, request.CardNumber);

        if (cardDetails == null)
        {
            logger.LogWarning("Card not found for UserId: {UserId}, CardNumber: {CardNumber}", request.UserId, request.CardNumber);
            return NotFound(new ProblemDetails { Title = "Card not found." });
        }

        logger.LogInformation("Resolving actions for Card: {@CardDetails}", cardDetails);

        var actions = cardResolver.ResolveMatrixAction(cardDetails);
        logger.LogInformation("Resolved actions: {@Actions}", actions);

        var response = responseFactory.CreateCardResponse(cardDetails, actions);

        return Ok(response);
    }
}