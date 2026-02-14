using FluentValidation;
using TaskFlow.Application.DTOs.Projects;

namespace TaskFlow.Application.Validators.Projects;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto> {
    public CreateProjectValidator() {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}