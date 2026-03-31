using api.Dto;
using api.Models;
using AutoMapper;

namespace api.Mappings;

public class TodoTaskMapper: Profile
{
    public TodoTaskMapper()
    {
        CreateMap<CreateTodoTaskDto, TodoTask>()
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.description))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.title))
            .ForMember(d => d.DueDate, opt => opt.MapFrom(s => s.dueDate));
        

        CreateMap<TodoTask, TodoTaskDto>()
            .ForMember(d => d.id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.createdBy, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.title, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.dueDate, opt => opt.MapFrom(s => s.DueDate))
            .ForMember(d => d.isCompleted, opt => opt.MapFrom(s => s.IsCompleted));
        
    }
}