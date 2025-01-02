using bettersociety.Dtos.Signup;
using bettersociety.Interfaces;
using bettersociety.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    [Route("Signup")]
    public class SignupController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public SignupController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto signupDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = signupDto.Username,
                    Email = signupDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, signupDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        //return RedirectToAction("Index", "Home");
                        //return Ok("User created successfully");
                        return Ok(new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
