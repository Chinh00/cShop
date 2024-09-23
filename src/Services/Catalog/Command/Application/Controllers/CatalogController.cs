using cShop.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CatalogController : BaseController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> HandleCreateCatalogAsync()
    {
        return Ok();
    }
}