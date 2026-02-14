using FluentValidation;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Application.DTOs.Authentication;

namespace TaskFlow.Application.Validators.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest> {
    public RegisterRequestValidator(IUserRepository userRepository) {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}