﻿using Microsoft.AspNetCore.Mvc;

namespace TrumanWeb.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
