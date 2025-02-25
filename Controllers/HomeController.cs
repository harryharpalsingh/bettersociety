using bettersociety.Data;
using bettersociety.Mappers;
using bettersociety.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Controllers
{
    //[Route("Home")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IEnumerable<Questions> objQuestions = _dbContext.Questions.OrderByDescending(q => q.Id);
            return View(objQuestions);
        }

        [HttpGet]
        public IActionResult GetQA()
        {
            if (!ModelState.IsValid)
            {
                return Ok(new
                {
                    Status = 0,
                    Message = "Model state is not valid!"
                });
            }

            var questions = _dbContext.Questions
                //.Include(q => q.Answers) // Eagerly load related data
                .AsNoTracking() //Improves performance (no tracking needed for read-only queries)
                .Select(q => q.ToQuestionsDto(_dbContext))
                .ToList();

            ////Explicit loading
            //var questions = _dbContext.Questions.ToList();
            //foreach (var question in questions)
            //{
            //    _dbContext.Entry(question).Collection(q => q.Answers).Load(); // Explicitly loads Answers
            //}

            return Ok(new
            {
                Status = 1,
                Questions = questions
            });
        }

        //[HttpGet("{id}")]
        [HttpGet("GetQAById/{id:int}")]
        public IActionResult GetQAById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new
                {
                    Status = 0,
                    Message = "Model state is not valid!"
                });
            }

            var questions = _dbContext.Questions.Find(id);
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Status = 1,
                Questions = questions.ToQuestionsDto(_dbContext)
            });
        }
    }
}
