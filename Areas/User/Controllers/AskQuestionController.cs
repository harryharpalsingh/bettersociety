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
        private readonly IQuestionXrefTagsRepository _questionXrefTagsRepository;
        private readonly UserManager<AppUser> _userManager;

        public AskQuestionController(ApplicationDbContext dbContext,
            IBlogPostRepository blogPostRepository,
            IQuestionXrefTagsRepository questionXrefTagsRepository)
        {
            _context = dbContext;
            _blogPostRepository = blogPostRepository;
            _questionXrefTagsRepository = questionXrefTagsRepository;
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
        public async Task<IActionResult> CreatePost([FromBody] CreateBlogPostDto createBlogDto, IEnumerable<int> tagIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionModel = createBlogDto.ToQuestionsFromCreatePostDto(HttpContext, _userManager);
            await _blogPostRepository.CreateAsync(questionModel);

            int QuestionId = questionModel.Id;

            List<QuestionsXrefTags> tags = new List<QuestionsXrefTags>();
            foreach (int id in tagIds)
            {
                QuestionsXrefTags qt = new QuestionsXrefTags();
                qt.QuestionId = QuestionId;
                qt.TagId = id;
                tags.Add(qt);
            }

            await _questionXrefTagsRepository.CreateRangeAsync(tags);

            return CreatedAtAction(nameof(GetPostById), new { id = questionModel.Id }, questionModel.ToQuestionsDto());
        }
    }
}
