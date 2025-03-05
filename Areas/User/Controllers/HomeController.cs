using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Areas.User.Controllers
{
    [Area("User")]
    [Route("u")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
