using FluentValidation.Results;

namespace cShop.Infrastructure.Validation;

public class ValidationException : Exception
{
    public readonly ValidationModel ValidationModel;
    public ValidationException(ICollection<ValidationFailure> validationFailures)
    {
        ValidationModel = new ValidationModel(validationFailures);
    }
    
}