using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
