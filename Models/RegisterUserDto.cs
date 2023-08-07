using System.ComponentModel.DataAnnotations;

namespace AuctionApi.Models
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public decimal Balance { get; set; } = 0;
        public int RoleId { get; set; } = 3;

    }
}
