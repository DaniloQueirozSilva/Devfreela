using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
