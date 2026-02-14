using AutoMapper;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Mapping;

public class TaskItemMappingProfile : Profile {
    public TaskItemMappingProfile() {
        CreateMap<CreateTaskItemDto, TaskItem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<UpdateTaskItemDto, TaskItem>()
            .ForMember(dest => dest.AssigneeId, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<TaskItem, TaskItemDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm")));
    }
}