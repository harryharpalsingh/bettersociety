using bettersociety.Areas.User.Dtos;
using bettersociety.Data;
using bettersociety.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Controllers
{
    public class postController : Controller
    {
        private readonly ApplicationDbContext _context;

        public postController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPostBySlug([FromBody] PostDetailDto postDto)
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
                var postEntity = _context.Questions.FirstOrDefault(p => p.Slug == postDto.Slug);

                if (postEntity == null)
                {
                    return NotFound(new { Message = "Post not found!" });
                }

                // Now, map the entity to the DTO in memory
                var postDtoResponse = postEntity.ToQuestionsDto(_context);
    
                return Ok(postDtoResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
