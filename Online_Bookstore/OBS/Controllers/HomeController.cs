using Microsoft.AspNetCore.Mvc;

namespace OnlineBookstore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
