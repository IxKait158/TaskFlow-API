using FluentValidation;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Application.DTOs.Tasks;

namespace TaskFlow.Application.Validators.Tasks;

public class UpdateTaskItemValidator : AbstractValidator<UpdateTaskItemDto> {
    public UpdateTaskItemValidator(IUserRepository userRepository) {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status is invalid.")
            .When(x => x.Status.HasValue);
        
        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Task Priority is invalid.")
            .When(x => x.Priority.HasValue);
        
        RuleFor(x => x.Deadline)
            .Must(date => date > DateTime.UtcNow).WithMessage("Deadline must be in the future.")
            .When(x => x.Deadline.HasValue);
        
        RuleFor(x => x.AssigneeId)
            .NotEmpty().WithMessage("Owner Id is required.")
            .When(x => x.AssigneeId.HasValue);
    }
}