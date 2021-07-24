using System.ComponentModel.DataAnnotations;

namespace API.Models.Dtos
{
    public class UserAuthDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}