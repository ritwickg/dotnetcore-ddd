namespace InventoryManagement.Domain.Entities
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public User User { get; set; }
    }
}
