using AutoMapper;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Mapping;

public class ProjectMappingProfile : Profile {
    public ProjectMappingProfile() {
        CreateMap<CreateProjectDto, Project>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<UpdateProjectDto, Project>()
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore()) // Явная проверка для Guid?
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Project, ProjectDetailsDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm")));
        
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm")));
    }
}