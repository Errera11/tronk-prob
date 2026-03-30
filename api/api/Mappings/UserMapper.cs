using api.Dto;
using api.Models;
using AutoMapper;

namespace api.Mappings;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.email))
            .ForMember(d => d.Password, opt => opt.MapFrom(s => s.password));

        CreateMap<User, UserDto>()
            .ForMember(d => d.id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.createdAt, opt => opt.MapFrom(s => s.CreatedAt));

    }
}