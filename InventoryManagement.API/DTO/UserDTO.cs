using InventoryManagement.Domain.Entities.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.DTO
{
    public class UserDto
    {
        [Required]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{10}")]
        public string PhoneNumber { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
