using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrumanWeb.Models;
using TrumanWeb.Services;

namespace TrumanWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService_API<Producto> _service_api; // Inyectamos el servicio con el tipo Producto
        private readonly IService_API<Categoria> _categoria_service; // Servicio para las categorías

        public HomeController(IService_API<Producto> service, IService_API<Categoria> categoriaService)
        {
            _service_api = service;
            _categoria_service = categoriaService;
        }

        // Acción para mostrar la página de inicio con el catálogo de productos
        public async Task<IActionResult> Index()
        {
            List<Producto> listaProductos = await _service_api.Lista("productos"); // Obtener la lista de productos
            List<Categoria> listaCategorias = await _categoria_service.Lista("categorias"); // Obtener lista de categorías

            // Crear un modelo compuesto para pasarlo a la vista
            var modelo = new HomeViewModel
            {
                Productos = listaProductos,
                Categorias = listaCategorias
            };

            return View(modelo); // Pasar los productos y categorías a la vista
        }
    }

    // Modelo para la vista principal que contiene productos y categorías
    public class HomeViewModel
    {
        public List<Producto> Productos { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}
