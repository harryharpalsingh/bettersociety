using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Controllers
{
    [Route("Contact")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
