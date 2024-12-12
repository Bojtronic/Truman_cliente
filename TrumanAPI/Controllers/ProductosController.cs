using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TrumanAPI.Models;

namespace TrumanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductosController(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        // GET: api/Productos
        [HttpGet]
        public IActionResult GetAllProductos()
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var productos = context.Query<Producto>(@"
                    SELECT idProducto, idCategoria, nombreProducto,
                           descripcionProducto, precio, stock,
                           alertaStock, urlImagen, activo, fechaRegistro
                    FROM Productos");
                return Ok(productos);
            }
        }

        // GET: api/Productos/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductoById(int id)
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var producto = context.QueryFirstOrDefault<Producto>(@"
                    SELECT idProducto, idCategoria, nombreProducto,
                           descripcionProducto, precio, stock,
                           alertaStock, urlImagen, activo, fechaRegistro
                    FROM Productos WHERE idProducto = @Id", new { Id = id });

                if (producto == null)
                    return NotFound(new { Mensaje = "Producto no encontrado" });

                return Ok(producto);
            }
        }

        // POST: api/Productos
        [HttpPost]
        public IActionResult CreateProducto([FromBody] Producto model)
        {
            if (model == null)
                return BadRequest(new { Mensaje = "Modelo de producto inválido" });

            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = @"
                    INSERT INTO Productos (idCategoria, nombreProducto, descripcionProducto, precio, stock, alertaStock, urlImagen, activo)
                    VALUES (@CategoriaId, @Nombre, @Descripcion, @Precio, @Stock, @AlertaStock, @UrlImagen, @Activo);
                    SELECT CAST(SCOPE_IDENTITY() as INT);";

                var id = context.ExecuteScalar<int>(sql, new
                {
                    model.IdCategoria,
                    model.NombreProducto,
                    model.DescripcionProducto,
                    model.Precio,
                    model.Stock,
                    model.AlertaStock,
                    model.UrlImagen,
                    model.Activo
                });

                model.IdProducto = id;
                return CreatedAtAction(nameof(GetProductoById), new { id }, model);
            }
        }

        // PUT: api/Productos
        [HttpPut]
        public IActionResult UpdateProducto([FromBody] Producto model)
        {
            if (model == null)
                return BadRequest(new { Mensaje = "Modelo de producto inválido" });

            using (var context = new SqlConnection(GetConnectionString()))
            {
                // Verificar si el producto existe usando el id del modelo
                var existingProducto = context.QueryFirstOrDefault<Producto>(@"
                    SELECT * FROM Productos WHERE idProducto = @Id", new { Id = model.IdProducto });

                if (existingProducto == null)
                    return NotFound(new { Mensaje = "Producto no encontrado" });

                // Actualizar el producto usando los valores del modelo
                var sql = @"
                    UPDATE Productos 
                    SET idCategoria = @CategoriaId, nombreProducto = @Nombre, descripcionProducto = @Descripcion, 
                        precio = @Precio, stock = @Stock, alertaStock = @AlertaStock, urlImagen = @UrlImagen, activo = @Activo 
                    WHERE idProducto = @Id";

                var rowsAffected = context.Execute(sql, new
                {
                    Id = model.IdProducto,
                    model.IdCategoria,
                    model.NombreProducto,
                    model.DescripcionProducto,
                    model.Precio,
                    model.Stock,
                    model.AlertaStock,
                    model.UrlImagen,
                    model.Activo
                });

                if (rowsAffected > 0)
                    return NoContent(); // Actualización exitosa
                else
                    return BadRequest(new { Mensaje = "Error al actualizar el producto" }); // Caso de error
            }
        }

        // DELETE: api/Productos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = "DELETE FROM Productos WHERE idProducto = @Id";
                var rowsAffected = context.Execute(sql, new { Id = id });

                if (rowsAffected > 0)
                    return NoContent();
                else
                    return NotFound(new { Mensaje = "Producto no encontrado" });
            }
        }
    }
}
