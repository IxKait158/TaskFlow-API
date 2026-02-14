using AutoMapper;
using FluentValidation;
using TaskFlow.Application.Common.Exception;
using TaskFlow.Application.Common.Extensions;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Implementations;

public class TaskItemService : ITaskItemService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;
    
    private readonly IValidator<CreateTaskItemDto> _createTaskItemValidator;
    private readonly IValidator<UpdateTaskItemDto> _updateTaskItemValidator;

    public TaskItemService(
        IUnitOfWork unitOfWork, 
        ICurrentUserService currentUserService, 
        IMapper mapper,
        IValidator<CreateTaskItemDto> createTaskItemValidator,
        IValidator<UpdateTaskItemDto> updateTaskItemValidator) 
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        
        _mapper = mapper;
        
        _createTaskItemValidator = createTaskItemValidator;
        _updateTaskItemValidator = updateTaskItemValidator;
    }
    
    public async Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto createRequest) {
        await _createTaskItemValidator.ValidateAndThrowCustomAsync(createRequest);

        if (await _unitOfWork.ProjectsRepository.GetByIdAsync(createRequest.ProjectId) == null)
            throw new NotFoundException(nameof(Project), createRequest.ProjectId);

        if (createRequest.AssigneeId.HasValue) {
            var ownerExist = await _unitOfWork.UsersRepository.GetByIdAsync(createRequest.AssigneeId.Value);

            if (ownerExist == null)
                throw new NotFoundException(nameof(User), createRequest.AssigneeId);
        }
        
        var taskItem = _mapper.Map<TaskItem>(createRequest);

        await _unitOfWork.TaskItemsRepository.AddAsync(taskItem);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskItemDto>(taskItem);
    }
    
    public async Task<TaskItemDto> GetTaskItemAsync(Guid taskId) {
        var taskItem = await _unitOfWork.TaskItemsRepository.GetTaskItemWithDetailsAsync(taskId);
        
        if (taskItem == null) 
            throw new NotFoundException(nameof(TaskItem), taskId);
        
        return _mapper.Map<TaskItemDto>(taskItem);
    }
    
    public async Task<TaskItemDto> UpdateTaskItemAsync(Guid taskId, UpdateTaskItemDto updateRequest) {
        await _updateTaskItemValidator.ValidateAndThrowCustomAsync(updateRequest);

        var taskItem = await _unitOfWork.TaskItemsRepository.GetTaskItemWithDetailsAsync(taskId);
        if (taskItem == null)
            throw new NotFoundException(nameof(TaskItem), taskId);

        if (updateRequest.AssigneeId.HasValue) {
            var ownerExist = await _unitOfWork.UsersRepository.GetByIdAsync(updateRequest.AssigneeId.Value);
            if (ownerExist == null)
                throw new NotFoundException(nameof(User), updateRequest.AssigneeId);

            taskItem.AssigneeId = updateRequest.AssigneeId.Value;
        }

        if (taskItem.AssigneeId != _currentUserService.UserId &&
            taskItem.Project!.OwnerId != _currentUserService.UserId)
            throw new ForbiddenException("Only the project owner or the task performer can change the task.");
        
        _mapper.Map(updateRequest, taskItem);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskItemDto>(taskItem);
    }
    
    public async Task DeleteTaskItemAsync(Guid taskId) {
        var taskItem = await _unitOfWork.TaskItemsRepository.GetByIdAsync(taskId);
        
        if (taskItem == null) 
            throw new NotFoundException(nameof(TaskItem), taskId);
        
        if (taskItem.AssigneeId != _currentUserService.UserId &&
            taskItem.Project!.OwnerId != _currentUserService.UserId) 
            throw new ForbiddenException("Only owner of project or assignee of task can update the task");
        
        await _unitOfWork.TaskItemsRepository.DeleteAsync(taskItem);
        await _unitOfWork.SaveChangesAsync();
    }
}