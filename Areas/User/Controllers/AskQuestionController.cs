using Azure;
using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Areas.User.Mappers;
using bettersociety.Data;
using bettersociety.Extensions;
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
    [Route("u/ask-question")]
    [Authorize]
    public class AskQuestionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAskQuestionRepository _askQuestionRepository;
        private readonly IQuestionXrefTagsRepository _questionXrefTagsRepository;
        private readonly UserManager<AppUser> _userManager;

        public AskQuestionController(ApplicationDbContext dbContext,
            IAskQuestionRepository askQuestionRepository,
            IQuestionXrefTagsRepository questionXrefTagsRepository)
        {
            _context = dbContext;
            _askQuestionRepository = askQuestionRepository;
            _questionXrefTagsRepository = questionXrefTagsRepository;
        }

        [HttpGet("")] // This ensures /u/ask-question calls Index()
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

            return Ok(question.ToQuestionsDto(_context));
        }

        //[HttpGet]
        [HttpGet("GetCategories")] // Avoids conflict with Index()
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

        [HttpPost("CreatePost")]
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
            var loggedInUserId = HttpContext.User.GetUserId();

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
                    tagNew = new Tags
                    {
                        TagName = tagName,
                        CreatedBy = loggedInUserId
                    };
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
                    TagId = tag.Id,
                    CreatedBy = loggedInUserId
                };
                QxrefTags.Add(qt);
            }
            #endregion

            await _questionXrefTagsRepository.CreateRangeAsync(QxrefTags);

            return CreatedAtAction(nameof(GetPostById), new { id = questionModel.Id }, questionModel.ToQuestionsDto(_context));
        }

        [HttpPost]
        [Route("CreatePostWithAttachment")]
        public async Task<IActionResult> CreatePostWithAttachment([FromForm] CreateQuestionPostDto createQuestionDto, IFormFile? fileAttachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createQuestionDto == null || createQuestionDto.QuestionData == null)
            {
                return BadRequest("Invalid request format.");
            }

            try
            {
                var askQuestionDto = createQuestionDto.QuestionData;
                var tagNames = createQuestionDto.TagNames ?? new List<string>(); // Ensure it's not null

                var questionModel = await askQuestionDto.ToQuestionsFromAskQuestiontDto(HttpContext, _userManager, _askQuestionRepository);
                await _askQuestionRepository.CreateAsync(questionModel);

                int QuestionId = questionModel.Id;
                var loggedInUserId = HttpContext.User.GetUserId();

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
                        tagNew = new Tags
                        {
                            TagName = tagName,
                            CreatedBy = loggedInUserId
                        };
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
                        TagId = tag.Id,
                        CreatedBy = loggedInUserId
                    };
                    QxrefTags.Add(qt);
                }
                #endregion

                #region Processing Attachments
                if (fileAttachment != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    /* Path Traversal Attack Protection (Critical Security Concern)
                        - An attacker could attempt to exploit the file path using ../ or other path traversal patterns. 
                        - Solution: Use Path.GetFileName() to sanitize the file name. */
                    var sanitizedFileName = Path.GetFileName(fileAttachment.FileName);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(sanitizedFileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileAttachment.CopyToAsync(stream);
                    }

                    string attachmentUrl = $"/uploads/{fileName}";

                    // Save the file path in the Attachments model
                    var attachment = new Attachments
                    {
                        QuestionId = QuestionId,
                        Attachment = attachmentUrl
                    };

                    _context.Attachments.Add(attachment);
                    await _context.SaveChangesAsync();
                }
                #endregion

                await _questionXrefTagsRepository.CreateRangeAsync(QxrefTags);

                return CreatedAtAction(nameof(GetPostById), new { id = questionModel.Id }, questionModel.ToQuestionsDto(_context));
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
