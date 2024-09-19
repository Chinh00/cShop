using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cShop.Infrastructure.Controller;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{   
    private ISender _mediator; 
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}