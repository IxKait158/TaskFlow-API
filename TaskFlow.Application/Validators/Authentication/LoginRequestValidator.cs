using FluentValidation;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Application.DTOs.Authentication;

namespace TaskFlow.Application.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest> {
    public LoginRequestValidator(IUserRepository userRepository) {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}