using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrumanWeb.Models;
using TrumanWeb.Services;

namespace TrumanWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService_API _service_api;

        public HomeController(IService_API service)
        {
            _service_api = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Usuario> lista = await _service_api.lista();

            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
