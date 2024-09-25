using cShop.Contracts.Services.Catalog;
using cShop.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Authorize]
public class CatalogController : BaseController
 {
     [HttpPost]
     public async Task<IActionResult> HandleCreateCatalogAsync(Command.CreateCatalog command, CancellationToken cancellationToken)
     {
         return Ok(await Mediator.Send());
     }
 }