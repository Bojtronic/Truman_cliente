﻿using Microsoft.AspNetCore.Mvc;

namespace TrumanWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
    }
}