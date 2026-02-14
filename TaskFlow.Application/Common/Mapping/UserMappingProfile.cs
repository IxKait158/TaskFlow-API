using AutoMapper;
using TaskFlow.Application.DTOs.User;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Mapping;

public class UserMappingProfile : Profile {
    public UserMappingProfile() {
        CreateMap<User, UserDto>();
    }
}