using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Data;
using bettersociety.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Areas.User.Controllers
{
    [Area("User")]
    [Route("/u/all-post")]
    public class AllPostController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IPostDetailRepository _postDetailRepository;

        public AllPostController(ApplicationDbContext dbcontext) //, IPostDetailRepository postDetailRepository
        {
            _dbContext = dbcontext;
            //_postDetailRepository = postDetailRepository;
        }

        //[HttpGet("")] // This ensures /u/ask-question calls Index()
        // GET: /u/all-post/{slug}
        [HttpGet("{slug}")]
        public IActionResult Index(string slug)
        {
            // Serve the view with the slug, or preload metadata
            ViewBag.Slug = slug;
            return View();
        }

        [HttpPost("get-post-detail-by-slug")]
        public IActionResult GetPostDetailBySlug([FromBody] PostDetailDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (postDto == null || string.IsNullOrEmpty(postDto.Slug))
            {
                return BadRequest(new { Message = "Invalid slug provided." });
            }

            try
            {
                // First, fetch the entity from the database (EF Core can translate this)
                var postEntity = _dbContext.Questions.FirstOrDefault(p => p.Slug == postDto.Slug);

                if (postEntity == null)
                {
                    return NotFound(new { Message = "Post not found!" });
                }

                // Now, map the entity to the DTO in memory
                var postDtoResponse = postEntity.ToQuestionsDto(_dbContext);

                return Ok(postDtoResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }


    }
}
