using System.ComponentModel.DataAnnotations;

namespace api.Dto;

public class CreateUserDto
{
     [EmailAddress(ErrorMessage = "Must be a valid email address")]
     [Required] public string email { get; set; }

     [Required] public string password { get; set; }
}