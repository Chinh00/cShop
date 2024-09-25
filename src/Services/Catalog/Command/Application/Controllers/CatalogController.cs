using Application.UseCases.Commands;
using cShop.Infrastructure.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CatalogController : BaseController
 {
     [HttpPost]
     public async Task<IActionResult> HandleCreateCatalogAsync(Commands.CreateCatalog command, CancellationToken cancellationToken)
     {
         return Ok(await Mediator.Send(command, cancellationToken));
     }
 }