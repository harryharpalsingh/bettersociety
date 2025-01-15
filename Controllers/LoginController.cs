using bettersociety.Dtos.Login;
using bettersociety.Interfaces;
using bettersociety.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
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
                    var _Token = _tokenService.CreateToken(user);
                    Response.Cookies.Append("XSRF-TOKEN", _Token, new CookieOptions
                    {
                        HttpOnly = true,          // Prevent JavaScript access
                        Secure = true,            // Use only over HTTPS
                        SameSite = SameSiteMode.Strict, // Prevent CSRF
                        Expires = DateTime.UtcNow.AddHours(1) // Set an expiry
                    });

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

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Clear the authentication cookie
            Response.Cookies.Delete("XSRF-TOKEN");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect to homepage or login page after logout
        }
    }
}
