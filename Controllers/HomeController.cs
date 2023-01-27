using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDintermediario
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}