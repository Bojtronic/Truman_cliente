using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrumanWeb.Models;
using TrumanWeb.Services;

namespace TrumanWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IService_API<Producto> producto_service; // Inyectar el servicio con el tipo Producto
        private readonly IService_API<Categoria> categoria_service; // Inyectar el servicio con el tipo Producto

        public ProductosController(IService_API<Producto> _producto_service, IService_API<Categoria> _categoria_service)
        {
            producto_service = _producto_service;
            categoria_service = _categoria_service;
        }

        // Acción para listar los productos
        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await producto_service.Lista("productos"); // Obtener la lista de productos

            return View(lista);
        }

        // Acción para mostrar los detalles de un producto
        public async Task<IActionResult> Detalle(int id)
        {
            Producto producto = await producto_service.Obtener(id, "productos"); // Obtener producto por ID

            if (producto == null)
            {
                return NotFound(); // Si no se encuentra el producto, devolver 404
            }

            return View(producto);
        }

        // Acción para mostrar el formulario de creación de un producto
        public async Task<IActionResult> Crear()
        {
            // Obtener la lista de categorías
            List<Categoria> listaCategorias = await categoria_service.Lista("categorias");

            // Manejar el caso donde no haya categorías disponibles
            int? categoriaIdPorDefecto = listaCategorias.Any() ? listaCategorias.First().IdCategoria : (int?)null;

            var modelo = new ProductoConCategoriaViewModel
            {
                Producto = new Producto
                {
                    NombreProducto = string.Empty,
                    DescripcionProducto = string.Empty,
                    Precio = 0.0M,
                    Stock = 0,
                    IdCategoria = categoriaIdPorDefecto, 
                    AlertaStock = 10, 
                    UrlImagen = string.Empty,
                    Activo = true 
                },
                Categorias = listaCategorias
            };

            return View(modelo);
        }


        [HttpPost]
        public async Task<IActionResult> Crear(ProductoConCategoriaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                bool resultado = await producto_service.Guardar(modelo.Producto, "productos");

                if (resultado)
                {
                    return RedirectToAction(nameof(Index)); // Redirigir si se guarda correctamente
                }

                ModelState.AddModelError(string.Empty, "Error al guardar el producto");
            }

            modelo.Categorias = await categoria_service.Lista("categorias"); // Recargar categorías si falla la validación
            return View(modelo);
        }


        // Acción para mostrar el formulario de edición de un producto
        public async Task<IActionResult> Editar(int id)
        {
            Producto producto = await producto_service.Obtener(id, "productos"); // Obtener el producto para editar

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
                bool resultado = await producto_service.Editar(producto, "productos"); // Llamada al servicio para editar el producto

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
            bool resultado = await producto_service.Eliminar(id, "productos"); // Llamada al servicio para eliminar el producto

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

        public class ProductoConCategoriaViewModel
        {
            public Producto Producto { get; set; } // Producto a crear
            public List<Categoria> Categorias { get; set; } // Lista de categorías disponibles
        }

    }
}
