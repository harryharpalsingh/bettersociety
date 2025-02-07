using bettersociety.Dtos.Signup;
using bettersociety.Interfaces;
using bettersociety.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

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

                var existingUser = await _userManager.FindByEmailAsync(signupDto.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { Message = "Email is already registered." });
                }

                var createdUser = await _userManager.CreateAsync(appUser, signupDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        //return RedirectToAction("Index", "Home");
                        //return Ok("User created successfully");

                        //return Ok(new NewUserDto
                        //{
                        //    UserName = appUser.UserName,
                        //    Email = appUser.Email
                        //});

                        return StatusCode(201, "User created successfully");
                    }
                    else
                    {
                        //return StatusCode(500, roleResult.Errors);
                        return BadRequest(new { Errors = createdUser.Errors.Select(e => e.Description) });

                        //Similarly, for role assignment failure:
                        //return BadRequest(new { Errors = roleResult.Errors.Select(e => e.Description) });
                    }
                }
                else
                {
                    return BadRequest(new { Errors = createdUser.Errors.Select(e => e.Description) });
                }
            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex);
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
