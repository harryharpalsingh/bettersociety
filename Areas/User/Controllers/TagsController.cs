using bettersociety.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            try
            {
                var tags = await _context.Tags
                    .AsNoTracking()
                    .Select(tag => new
                    {
                        tag.Id,
                        tag.TagName
                    })
                    .ToListAsync();

                if (tags == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    Status = 1,
                    Tags = tags
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred!",
                    Details = ex.Message
                });
            }
        }
    }
}
