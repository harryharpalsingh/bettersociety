using bettersociety.Data;
using bettersociety.Mappers;
using bettersociety.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace bettersociety.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
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

        //[HttpGet]
        //public async Task<IActionResult> GetQA()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Ok(new
        //        {
        //            Status = 0,
        //            Message = "Model state is not valid!"
        //        });
        //    }

        //    var questions = await _dbContext.Questions.ToListAsync();

        //    return Ok(new
        //    {
        //        Status = 1,
        //        TotalQuestions = questions.Count,
        //        Questions = questions
        //    });
        //}

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
                .Include(q => q.Answers) // Eagerly load related data
                .ToList()
                .Select(static q => q.ToQuestionsDto());

            return Ok(new
            {
                Status = 1,
                Questions = questions
            });
        }

        [HttpGet("{id}")]
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
                Questions = questions.ToQuestionsDto()
            });
        }

        //[HttpPost]
        //public IActionResult GetAll([FromBody] RequestModel prm)
        //{
        //    if (prm == null)
        //        return BadRequest("Invalid request.");

        //    // Process the parameters and fetch data
        //    var questions = _dbContext.Questions.ToList();

        //    // Example response structure
        //    return Ok(new
        //    {
        //        d = new
        //        {
        //            Status = 1,
        //            DataSet = JsonConvert.SerializeObject(new { Table = questions })
        //        }
        //    });
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
