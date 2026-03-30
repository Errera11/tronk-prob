using api.Models;

namespace api.Dto;

public class TodoTaskDto
{
    public int id { get; }
    
    public string title { get; }
    
    public string description { get; }
    
    public DateTime dueDate { get; }
    
    public bool isCompleted { get; }
    
    public User createdBy { get; }
}