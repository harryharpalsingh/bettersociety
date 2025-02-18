using Azure;
using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Areas.User.Mappers;
using bettersociety.Data;
using bettersociety.Mappers;
using bettersociety.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //[HttpGet]
        //public async Task<IActionResult> GetCategories()
        //{
        //    try
        //    {
        //        var categories = await _context.QuestionCategories.
        //            AsNoTracking()
        //            .Where(category => category.Deleted == 0)
        //            .Select(category => new
        //            {
        //                category.Id,
        //                category.Category
        //            })
        //            .ToListAsync();

        //        if (categories.Count == 0)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(new
        //        {
        //            Status = 1,
        //            Categories = categories
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            Message = "An error occurred!",
        //            Details = ex.Message
        //        });
        //    }
        //}

        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                IQueryable<object> categoriesQuery = _context.QuestionCategories
                    .AsNoTracking()
                    .Where(category => category.Deleted == 0)
                    .Select(category => new
                    {
                        category.Id,
                        category.Category
                    });

                if (!categoriesQuery.Any())
                {
                    return NotFound();
                }

                return Ok(new
                {
                    Status = 1,
                    Categories = categoriesQuery
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

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreateBlogPostDto createBlogDto, IEnumerable<string> tagNames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionModel = createBlogDto.ToQuestionsFromCreatePostDto(HttpContext, _userManager);
            await _blogPostRepository.CreateAsync(questionModel);

            int QuestionId = questionModel.Id;

            #region Processing Tags
            // Create a list to hold the tags
            var tagsList = new List<Tags>();
            // Fetch all existing tags in one query to avoid N+1 queries
            var existingTags = await _context.Tags.Where(t => tagNames.Contains(t.TagName)).ToListAsync();

            foreach (var tagName in tagNames)
            {
                // Check if the tag exists, if not, create it
                var tagNew = existingTags.FirstOrDefault(t => t.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
                if (tagNew == null)
                {
                    tagNew = new Tags { TagName = tagName };
                    _context.Tags.Add(tagNew);
                    existingTags.Add(tagNew); // Add newly created tag to the list for future use
                }

                tagsList.Add(tagNew);
            }
            #endregion

            #region Proecssing Question Xref Tags
            List<QuestionsXrefTags> QxrefTags = new List<QuestionsXrefTags>();
            foreach (var tag in tagsList)
            {
                QuestionsXrefTags qt = new QuestionsXrefTags
                {
                    QuestionId = QuestionId,
                    TagId = tag.Id
                };
                QxrefTags.Add(qt);
            }
            #endregion

            await _questionXrefTagsRepository.CreateRangeAsync(QxrefTags);

            return CreatedAtAction(nameof(GetPostById), new { id = questionModel.Id }, questionModel.ToQuestionsDto());
        }
    }
}
