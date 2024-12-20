using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    [Route("Signup")]
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
