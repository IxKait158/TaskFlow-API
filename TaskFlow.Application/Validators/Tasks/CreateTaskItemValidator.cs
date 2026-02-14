using FluentValidation;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.DTOs.Tasks;

namespace TaskFlow.Application.Validators.Tasks;

public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemDto> {
    public CreateTaskItemValidator(IUnitOfWork unitOfWork) {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
        
        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Task Priority is invalid.")
            .When(x => x.Priority.HasValue);
        
        RuleFor(x => x.Deadline)
            .Must(date => date > DateTime.UtcNow).WithMessage("Deadline must be in the future.")
            .When(x => x.Deadline.HasValue);

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project ID is required");
        
        RuleFor(x => x.AssigneeId)
            .NotEmpty().WithMessage("Assignee ID is required")
            .When(x => x.AssigneeId.HasValue);
    }
}