using bettersociety.Dtos.Login;
using bettersociety.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the user by username or email
                var user = await _userManager.FindByNameAsync(loginDto.Email) ?? await _userManager.FindByEmailAsync(loginDto.Password);
                //var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or email." });
                }

                // Check password
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Optionally generate a JWT token or authentication cookie here
                    return Ok(new { message = "Login successful!" });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid password." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login.", details = ex.Message });
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful." });
        }
    }
}
