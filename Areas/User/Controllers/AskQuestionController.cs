using Azure;
using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Areas.User.Mappers;
using bettersociety.Data;
using bettersociety.Helpers;
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
        private readonly IAskQuestionRepository _askQuestionRepository;
        private readonly IQuestionXrefTagsRepository _questionXrefTagsRepository;
        private readonly UserManager<AppUser> _userManager;

        public AskQuestionController(ApplicationDbContext dbContext, IAskQuestionRepository askQuestionRepository, IQuestionXrefTagsRepository questionXrefTagsRepository)
        {
            _context = dbContext;
            _askQuestionRepository = askQuestionRepository;
            _questionXrefTagsRepository = questionXrefTagsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            var question = await _askQuestionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question.ToQuestionsDto());
        }

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
        public async Task<IActionResult> CreatePost([FromBody] CreateQuestionPostDto createQuestionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createQuestionDto == null || createQuestionDto.QuestionData == null)
            {
                return BadRequest("Invalid request format.");
            }

            var askQuestionDto = createQuestionDto.QuestionData;
            var tagNames = createQuestionDto.TagNames ?? new List<string>(); // Ensure it's not null

            var questionModel = await askQuestionDto.ToQuestionsFromAskQuestiontDto(HttpContext, _userManager, _askQuestionRepository);
            await _askQuestionRepository.CreateAsync(questionModel);

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
                    await _context.SaveChangesAsync(); // Ensure EF Core generates the ID
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
