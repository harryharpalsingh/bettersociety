using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
