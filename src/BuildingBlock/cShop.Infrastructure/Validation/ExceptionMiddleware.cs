using System.Text.Json;
using cShop.Core.Domain;

namespace cShop.Infrastructure.Validation;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            if (e is ValidationException ve)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ve.ValidationModel.StatusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(ve.ValidationModel));
            }
            else
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize(ResultModel<string>.Create(e.Message)));
            }
        }


    }
}