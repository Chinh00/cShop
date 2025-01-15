namespace Identity.Api.Middlewares;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string? _domain;

    public CustomMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _domain = configuration.GetValue<string>("Identity:IssuerUri");
    }
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Host = new HostString(_domain);

        await _next(context);
    }

}