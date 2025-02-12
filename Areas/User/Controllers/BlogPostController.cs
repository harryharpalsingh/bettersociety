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
    public class BlogPostController : Controller
    {
        private readonly ApplicationDbContext _appDbcontext;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly UserManager<AppUser> _userManager;

        public BlogPostController(ApplicationDbContext appDbContext, IBlogPostRepository blogPostRepository)
        {
            _appDbcontext = appDbContext;
            _blogPostRepository = blogPostRepository;
        }

        //[Route("User/BlogPost/Index")] // Fixed route.
        public IActionResult Index()
        {
            return View();
        }

        //Get
        public async Task<IActionResult> Create()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPostOfUser()
        {
            var blogPosts = await _blogPostRepository.GetQuestionsAsync();

            var blogPostDto = blogPosts.Select(b => b.ToQuestionsDto());

            return View(blogPosts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] int id)
        {
            var question = await _blogPostRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question.ToQuestionsDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostDto createBlogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionModel = createBlogDto.ToQuestionsFromCreatePostDto(HttpContext, _userManager);
            await _blogPostRepository.CreateAsync(questionModel);
            //await _appDbcontext.Questions.AddAsync(questionModel);
            //await _appDbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBlogPostById), new { id = questionModel.Id }, questionModel.ToQuestionsDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostDto updateBlogPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionsModel = await _blogPostRepository.UpdateAsync(updateBlogPostDto);
            if (questionsModel == null)
            {
                return NotFound();
            }

            return Ok(questionsModel.ToQuestionsDto());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var questionsModel = await _blogPostRepository.DeleteAsync(id);
            if (questionsModel == null)
            {
                return null;
            }

            return NoContent();
        }
    }
}
