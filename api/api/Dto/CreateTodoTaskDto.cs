using System.ComponentModel.DataAnnotations;

namespace api.Dto;

public class CreateTodoTaskDto
{
    [Required] public string title { get; set; }
    
    public string description { get; set; }
    
    [Required] public DateTime dueDate { get; set; }
}