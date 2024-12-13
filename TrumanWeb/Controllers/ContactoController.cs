using Microsoft.AspNetCore.Mvc;

namespace TrumanWeb.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
