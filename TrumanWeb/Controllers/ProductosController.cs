using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrumanWeb.Models;
using TrumanWeb.Services;

namespace TrumanWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IService_API<Producto> _service_api; // Inyectamos el servicio con el tipo Producto

        public ProductosController(IService_API<Producto> service)
        {
            _service_api = service;
        }

        // Acción para listar los productos
        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await _service_api.Lista("productos"); // Obtener la lista de productos

            return View(lista);
        }

        // Acción para mostrar los detalles de un producto
        public async Task<IActionResult> Detalle(int id)
        {
            Producto producto = await _service_api.Obtener(id, "productos"); // Obtener producto por ID

            if (producto == null)
            {
                return NotFound(); // Si no se encuentra el producto, devolver 404
            }

            return View(producto);
        }

        // Acción para mostrar el formulario de creación de un producto
        public IActionResult Crear()
        {
            return View();
        }

        // Acción para guardar un nuevo producto
        [HttpPost]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                bool resultado = await _service_api.Guardar(producto, "productos"); // Llamada al servicio para guardar el producto

                if (resultado)
                {
                    return RedirectToAction(nameof(Index)); // Redirigir a la lista de productos si se guarda correctamente
                }

                ModelState.AddModelError(string.Empty, "Error al guardar el producto");
            }

            return View(producto);
        }

        // Acción para mostrar el formulario de edición de un producto
        public async Task<IActionResult> Editar(int id)
        {
            Producto producto = await _service_api.Obtener(id, "productos"); // Obtener el producto para editar

            if (producto == null)
            {
                return NotFound(); // Si no se encuentra el producto, devolver 404
            }

            return View(producto);
        }

        // Acción para actualizar el producto editado
        [HttpPost]
        public async Task<IActionResult> Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                bool resultado = await _service_api.Editar(producto, "productos"); // Llamada al servicio para editar el producto

                if (resultado)
                {
                    return RedirectToAction(nameof(Index)); // Redirigir a la lista de productos si se edita correctamente
                }

                ModelState.AddModelError(string.Empty, "Error al editar el producto");
            }

            return View(producto);
        }

        // Acción para eliminar un producto
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool resultado = await _service_api.Eliminar(id, "productos"); // Llamada al servicio para eliminar el producto

            if (resultado)
            {
                return RedirectToAction(nameof(Index)); // Redirigir a la lista de productos si se elimina correctamente
            }

            return NotFound(); // Si no se encuentra el producto, devolver 404
        }

        

        // Acción para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
