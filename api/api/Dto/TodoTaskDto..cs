using api.Models;

namespace api.Dto;

public class TodoTaskDto
{
    public int id { get; set;  }
    
    public string title { get; set; }
    
    public string description { get; set; }
    
    public DateTime dueDate { get; set; }
    
    public bool isCompleted { get; set;}
    
    public UserDto createdBy { get;set;}
}