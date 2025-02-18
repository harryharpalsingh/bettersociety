using System.ComponentModel.DataAnnotations;

namespace bettersociety.Dtos.Signup
{
    public class SignupDto
    {
        /*
            * The [Required] attribute indicates that a value is mandatory for this property when the model is validated (often used in model validation for forms).
            * It does not allow the property to be null, but it enforces validation at runtime.
            * It’s used for data validation to ensure the user has provided a value for this property
         */

        /* string? -> [?] Marks the property as nullable, allowing it to hold a null value. */

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
        //[RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
