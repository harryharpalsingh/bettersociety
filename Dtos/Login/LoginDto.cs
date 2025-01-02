using System.ComponentModel.DataAnnotations;

namespace bettersociety.Dtos.Login
{
    public class LoginDto
    {
        //[Required]
        //public string? Username { get; set; }

        [Required]
        //[EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
