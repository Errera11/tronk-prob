namespace api.Dto;

public class UpdateTaskDto
{
    public string title { get; set; }
    
    public string description { get; set; }
    
    public DateTime dueDate { get; set; }
    
    public bool is_completed { get; set; } = false;
}