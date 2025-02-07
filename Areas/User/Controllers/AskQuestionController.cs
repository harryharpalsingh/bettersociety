using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Areas.User.Mappers;
using bettersociety.Data;
using bettersociety.Mappers;
using bettersociety.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class AskQuestionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly UserManager<AppUser> _userManager;

        public AskQuestionController(ApplicationDbContext dbContext, IBlogPostRepository blogPostRepository)
        {
            _context = dbContext;
            _blogPostRepository = blogPostRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            var question = await _blogPostRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question.ToQuestionsDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreateBlogPostDto createBlogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionModel = createBlogDto.ToQuestionsFromCreateBlogPostDto(HttpContext, _userManager);
            await _blogPostRepository.CreateAsync(questionModel);
            return CreatedAtAction(nameof(GetPostById), new { id = questionModel.Id }, questionModel.ToQuestionsDto());
        }
    }
}
