using AutoMapper;
using FluentValidation;
using TaskFlow.Application.Common.Exception;
using TaskFlow.Application.Common.Extensions;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Implementations;

public class ProjectService : IProjectService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    
    private readonly IMapper _mapper;
    
    private readonly IValidator<CreateProjectDto> _createProjectValidator;

    public ProjectService(
        IUnitOfWork unitOfWork, 
        ICurrentUserService currentUserService, 
        IMapper mapper,
        IValidator<CreateProjectDto> createProjectValidator)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        
        _mapper = mapper;
        
        _createProjectValidator = createProjectValidator;
    }
    
    public async Task<ProjectDetailsDto> CreateProjectAsync(CreateProjectDto createRequest) {
        await _createProjectValidator.ValidateAndThrowCustomAsync(createRequest);
        
        var project = _mapper.Map<Project>(createRequest);
        
        var userId = _currentUserService.UserId;
        project.OwnerId = userId;
        
        var owner = await _unitOfWork.UsersRepository.GetByIdAsync(userId);
        if (owner == null) 
            throw new UnauthorizedAccessException("User not found");
        
        project.Owner = owner;
        
        await _unitOfWork.ProjectsRepository.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProjectDetailsDto>(project);
    }

    public async Task<ProjectDto> GetProjectAsync(Guid projectId) {
        var project = await _unitOfWork.ProjectsRepository.GetByIdWithDetailsAsync(projectId);
        if (project == null) 
            throw new NotFoundException(nameof(Project), projectId);
        
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<IReadOnlyList<TaskItemDto>> GetProjectTasksAsync(Guid projectId) {
        var tasks = await _unitOfWork.TaskItemsRepository.GetTaskItemsInProjectAsync(projectId);

        return _mapper.Map<List<TaskItemDto>>(tasks);
    }

    public async Task<ProjectDto> UpdateProjectAsync(Guid projectId, UpdateProjectDto updateRequest) {
        var project = await _unitOfWork.ProjectsRepository.GetByIdWithDetailsAsync(projectId);
        if (project == null)
            throw new NotFoundException(nameof(Project), projectId);

        if (updateRequest.OwnerId.HasValue) {
            var ownerExist = await _unitOfWork.UsersRepository.GetByIdAsync(updateRequest.OwnerId.Value);
            if (ownerExist == null)
                throw new NotFoundException(nameof(User), updateRequest.OwnerId);

            project.OwnerId = updateRequest.OwnerId.Value;
        }
        
        _mapper.Map(updateRequest, project);
        
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task DeleteProjectAsync(Guid projectId) {
        var project = await _unitOfWork.ProjectsRepository.GetByIdAsync(projectId);

        if (project == null) 
            throw new NotFoundException(nameof(Project), projectId);
        
        if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin") 
            throw new ForbiddenException("You cannot delete someone else's project");
        
        await _unitOfWork.ProjectsRepository.DeleteAsync(project);
        await _unitOfWork.SaveChangesAsync();
    }
}