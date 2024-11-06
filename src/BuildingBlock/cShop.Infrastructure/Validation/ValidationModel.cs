using System.Net;
using FluentValidation.Results;

namespace cShop.Infrastructure.Validation;

public class ValidationModel
{
    
    public int StatusCode { get; init; } = (int)HttpStatusCode.BadRequest;
    public string Message { get; init; } = string.Empty;
    public List<ValidationError> Errors { get; init; }

    public ValidationModel(ICollection<ValidationFailure> errors)
    {
       Errors = errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)).ToList();
        
    }
    
}
