using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Home")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
