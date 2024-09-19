using Application.UseCases.Command;
using cShop.Infrastructure.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class BasketController : BaseController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBasket(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new CreateBasketCommand(id), cancellationToken));
    }

}