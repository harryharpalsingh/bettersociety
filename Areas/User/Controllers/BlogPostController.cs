using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Mappers;
using bettersociety.Data;
using bettersociety.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Areas.User.Controllers
{
    [Area("User")]
    public class BlogPostController : Controller
    {
        private readonly ApplicationDbContext _appDbcontext;

        public BlogPostController(ApplicationDbContext appDbContext)
        {
            _appDbcontext = appDbContext;
        }

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

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var question = _appDbcontext.Questions.Find(id);
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

            var questionModel = createBlogDto.ToQuestionsFromCreateBlogPostDto();
            await _appDbcontext.Questions.AddAsync(questionModel);
            await _appDbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = questionModel.Id }, questionModel.ToQuestionsDto());

        }
    }
}
