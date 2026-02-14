using FluentValidation;
using ValidationException = TaskFlow.Application.Common.Exception.ValidationException;

namespace TaskFlow.Application.Common.Extensions;

public static class ValidatorExtensions {
    
    public static async Task ValidateAndThrowCustomAsync<T>(this IValidator<T> validator, T entity) {
        var result = await validator.ValidateAsync(entity);

        if (!result.IsValid) {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            
            throw new ValidationException(errors);
        }
    }
}