using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
